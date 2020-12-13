using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client
{
    using System.IO;
    using System.Xml.Serialization;
    using IEX.Utilities;
    public class XmlRepository
    {
        public const string DefaultFileName = "LabManager.iexlab";
        public string FullPath { get; set; }
        public XmlRepository()
        {
            string full_path = System.IO.Path.Combine(IEX.Utilities.IEXConfiguration.GetIexInstallationFolder(), DefaultFileName);
            FullPath = full_path;
            if (!File.Exists(full_path))
                Serialize(full_path, new IexLabXmlConfiguration());
        }

        protected IexLabXmlConfiguration Deserialize(string full_path)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { full_path });
            TextReader reader = new StreamReader(full_path);
            XmlSerializer serializer = new XmlSerializer(typeof(IexLabXmlConfiguration));
            IexLabXmlConfiguration configuration = (IexLabXmlConfiguration)serializer.Deserialize(reader);
            reader.Close();
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + configuration);
            return configuration;
        }

        protected void Serialize(string full_path, IexLabXmlConfiguration configuration)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { full_path, configuration });
            TextWriter writer = new StreamWriter(full_path);
            XmlSerializer serializer = new XmlSerializer(typeof(IexLabXmlConfiguration));
            serializer.Serialize(writer, configuration);
            writer.Close();
        }

        protected IexLabXmlConfiguration New(string full_path)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { full_path });
            FullPath = full_path;
            IexLabXmlConfiguration configuration = new IexLabXmlConfiguration();
            Serialize(full_path, configuration);
            return configuration;
        }

        protected IexLabXmlConfiguration Open(string full_path)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { full_path });
            FullPath = full_path;
            IexLabXmlConfiguration configuration = Deserialize(FullPath);
            return configuration;
        }

        protected IexLabXmlConfiguration Save(string full_path)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { full_path });
            FullPath = full_path;
            string dir = Path.GetDirectoryName(FullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            IexLabXmlConfiguration configuration = Deserialize(FullPath);
            return configuration;
        }
    }
}
