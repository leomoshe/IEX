using Alchemy;
using Alchemy.Classes;
using Alchemy.Handlers.WebSocket;
using IEX.Utilities;
using IEX.Utilities.Network;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Xml;

namespace IEX.Server.Monitor.Host
{
    public class WebSocketClientManager
    {        
        IMonitoringService _monitoringService = new MonitoringService();        
        WebSocketClient _client;
        Timer _timer;
        string _url;
        int _period;

        public WebSocketClientManager()
        {
            _url = ConfigurationManager.AppSettings["WebSocketURL"];
            _period = int.Parse(ConfigurationManager.AppSettings["WebSocketTryConnectSeconds"]) * 1000;

            SetWebSocketClient();

            _timer = new Timer(timer_Callback, null, 0, _period);
        }

        void SetWebSocketClient()
        {
            _client = new WebSocketClient(_url)
            {
                OnReceive = OnReceive,
                OnConnected = OnConnected,
                OnDisconnect = OnDisconnect
            };
        }

        private void timer_Callback(Object state)
        {
            Tracer.Write(Tracer.TraceLevel.INFO, "Try to connect to WebSocket server in " + _url);

            _client.Connect();
        }
      
        void OnConnected(UserContext context)
        {
            Tracer.Write(Tracer.TraceLevel.INFO, "Connected to WebSocket server in " + _url);
            
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        void OnDisconnect(UserContext context)
        {
            Tracer.Write(Tracer.TraceLevel.INFO, "Disconnected from WebSocket server in " + _url);

            _client.Disconnect();
            _client = null;
            SetWebSocketClient();

            _timer.Change(2500, _period);
        }

        void OnReceive(UserContext context)
        {
            try
            {               
                DataContainer dataReceived = (DataContainer)IEX.Utilities.Tools.Serializer.DeserializeFromBytes(IEX.Utilities.Network.WebSocketHelper.GetBytesTrimEnd(context.DataFrame.AsRaw()), typeof(DataContainer));
                MethodInfo methodInfo = _monitoringService.GetType().GetInterface("IMonitoringService").GetMethod(dataReceived.MethodName);

                if (methodInfo != null)
                {
                    var obj = methodInfo.Invoke(_monitoringService, dataReceived.Params);

                    List<Type> types = new List<Type>();
                    List<object> returnData = new List<object>();
                    foreach (ParameterInfo paramInfo in methodInfo.GetParameters())
                    {
                        if (paramInfo.IsOut)
                        {
                            returnData.Add(dataReceived.Params[paramInfo.Position]);
                            types.Add(dataReceived.Params[paramInfo.Position].GetType());
                        }
                    }

                    if (returnData.Count > 0)
                    {
                        returnData.Insert(0, obj);
                        types.Insert(0, obj.GetType());
                        byte[] dataSend = IEX.Utilities.Tools.Serializer.SerializeToBytes(returnData.ToArray(), types.ToArray());
                        context.Send(dataSend);
                    }
                    else
                    {
                        byte[] dataSend;
                        if (obj != null)
                            dataSend = IEX.Utilities.Tools.Serializer.SerializeToBytes(obj);
                        else
                            dataSend = IEX.Utilities.Tools.Serializer.SerializeToBytes(true);
                        context.Send(dataSend);
                    }
                }
            }
            catch (Exception exp)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, "Exception in WSClientManager", exp);
                MyException myExp = new MyException() { Message = exp.Message };
                byte[] dataSend = IEX.Utilities.Tools.Serializer.SerializeToBytes(myExp);
                context.Send(dataSend);
            }
        }               
    }   
}
