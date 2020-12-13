using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.App.Views
{
    using System.Windows.Forms;
    public class StreamViewer: PictureBox
    {
        public StreamViewer()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Width = 240;
            this.Height = 150;
        }
        IEX.NetworkMediaSource.NetworkSource _stream;
        private string _host_id = null;
        public string HostId
        {
            set
            {
                if (_host_id == null)
                {
                    _host_id = value;
                    Run();
                }
            }
        }

        private string _server_id = null;
        public string ServerId 
        {
            set
            {
                if (_server_id == null)
                {
                    _server_id = value;
                    Run();
                }
            }
        }

        private void Run()
        {
            if (_host_id == null || _server_id == null)
                return;
            string url = string.Format("http://{0}:808{1}", _host_id, _server_id.Replace("IEX_", string.Empty));
            _stream = new IEX.NetworkMediaSource.NetworkSource(url);
            _stream.Start(FrameHandler);
        }

        private void FrameHandler(object sender, IEX.NetworkMediaSource.ImageEventArgs args)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((EventHandler<IEX.NetworkMediaSource.ImageEventArgs>)FrameHandler, sender, args);
            else
                this.Image = args.Image;
        }
    }
}
