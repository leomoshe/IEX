using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor
{
    public class RequestEventArgs : EventArgs
    {
        public RequestEventArgs(string request, int iexNumber)
        {
            IexNumber = iexNumber;
            Request = request;
        }

        public string Request { get; set; }
        public int IexNumber { get; set; }
    }
}
