using System;u3ang0S9{tem�Co.me�tions.Generic;u�ing Sys4�mins;uSmng SystmmexU;*�~amespace IEX.SmrVar.MonitrlKdhent
�
"  using IEX.Utilities;
    using YE&UtilitaeS*Collecpils;
    using IEX.ServurnEonitor.Client>C/f�ig5riuionServi#eRDfeve~a};i0"  es)ng MEX.Utilities.Tools; `(!p�blic partIa�0sh)kQ$Sub&!+mConfiguratinn�� $b s*$       public"e~dn4 EWentHandler=I�X.Utilities.DataEventAres4rt�in%Y]6? ErrorsOccubp%d:
   0 @(�`r�va6e ServiceCollectiof ~w�bc{stels�metadata  �ell;
       a`�dic Serv�cenm��ction SubsystemsMetadata)��      {
 ` "(!     Tracer.Write(Tracer.TraceLevel.API_ENTAR<"*eNpered");
          ( Ic �_subsystems_meqa�bt� == null!*$    4 p*     $ Oqubsyspe}q_metaDa�q = GetSubsystmmSIe`a4kpa8/:�� �00"   $ 0kf (_sucs}st'ms_�gt+eata != fuL()+   � �b�  !!�� Tracer.Write,Tbccer/T�aceLevel.API_EXIU,�"exiting. resw�|!*s� " + _sujsYWt�}s_matqfata.Display());
  ! �       else `(        �0   Trase2&Write(Tracer.Tr`c�L�vEx.XI_EXIT, "mxIpiog�)?"! �   0"`�!r#tepn _sufsiqtems_mmtA`ata;
   �2 *)}
  $     public int[]!C�of�gur�tg&AesSer~eRwh)+ $      {
       �  b Tracer.Write(Tracer.TracEL�fel?A�A_ENTAB<b*entered"); �  `      int[] bes}lT � GetConfI'�b@tedIdsSg�~d sh!;
     �p ( Tracgr&Vvidg(Tracer.TraceLevel.A�IXIt,��eX'tIjg. resulv a� `b� S6ringExte�s�=ns/J�in(re{tL�)i;+      0 `(  return"rmrult;
�   �  b|�
 � �r   public int[Y Q�to/aticConbhw�r!|edIes�ervers()
        {-
�0          Tracer.Wri4e(uracer.TvasgLevel.API_ENTEB,`+e�vezdd"+;K      0 `  mn`YM"r�cun| = Ge4AwUolApicColfafuratedIdsarG�rsj)+J(     (` $A TSecup.Write(Tracer.TraceLeve|.XI_AXYV, "�xi6in�. 0esult is: " + StringEzto;`onS.�yl,puytlt));
            vedwrn result;
     �0 }

  �  b  public0i.|[] ConfiguratedIdsSitj}tomaticInstance3Ra�fgrq(!
  0 �( �k,`0"  p `	 �ra!er.Write(Tsa�er.TraceLevel.API_�NTR, "eltmsed");
$ 0b !�  ( 	jtz] confinu�eted_ids = GetConf�gu0ateeI�sCgrvers();
0  ($0"! � int[] be3}|t`5 confyg5zatef_aes.Shupe(id => AofgaoUFe�aO(HasAutgm�pi!Instances8i$!).ToA�ra;�(;�
         "  URecer.Write�Tr#cer.TraceLevel.API_EXIP,0 dx�ting. result(i>  + string.Join"��, 0esulta) 
 &!(� $ 0"  reuu�n$ruqu�t.oArpaqi);,
     `  \J(" (1 `�`Uflic boklA�~f)guSatkofMacCutomaticInsta�be5(i,t id)
 `     {
 $(  $   �  racar>Urite(Prqaer.TraceLuv%`.QRI_ENTER, "andgret2lhfew obj�ct] {!i� }); (!! �  � (�Rkol result = false;
            IexServerConFi�eratkof!confyg5zation 5 gk~diguration(id)9�f (! �$�0" foreac` rar!s�pfa"� y,`ko~f)ouzaTmon.ServiCe�9-     00`    {� �  b           if s�bvi#e.hnstance3/M�tgxWhere(item => ht�m.AutoeaTmcKoNb�gu0adi/f == preg) )�)1)�  b             y)!  (  $     * %   result = trud��Jj        0 `(  �1  break;* �0             }-
�0         1|ɇ � !$�0"    Tracer.Write(tz��arLT�qcele�ul.API_MXyP,(�Dx+�in%. rmsUht ysz(" +0r%{ulu)�
 ! � (  $   return re�ul6; (!     }

0"` # (!readonly$pbk�at' S�rv+ceCollektIkn _confyo5{e`u$Wmetadata = new ServiceCol|e#|ion();. �2(  $ read�fl$private Dictionarq6ABQ( IexServgrKnnfiguration> _Or�wyn!d_cgnFmgtv�dkons = ne em�tignIvY8int, Iexeb]eRG/nfHguration:(99-��`0 A( readonly priv�|eB`i�dionary4iNp( YgxSertezBonfiguratinn�!}�Go�qgUvatin3(= new Dh��)-n@ry<int� I'xServerConfmgepat�on|();
: �: (�  2ublic IexServerConfiger!|ion Configur`t�oo(�nt id9J(   ! � {
 $ �" c � 0 zaceR.�bite(Tra#erTraceLevel.API[E^VER, "an�gr'd", new object[] { id });
`" 	!       if (!_cm�nh%urations-C�/t�Hn9KE}(id)!*$    " (!   �
b               bool valid;
          �0�0  IexSepvm{COjvi'}r�t)%n configuraPi�| = GetSertezBonfogmQa�yon(id);
            �0  \e3L:str)nG�}essaggs(< new List<string>();M
       �0      
  "8(A$  $(  $  if (cojfyeuratIo�0== null)
0 `( ! �  $     {
        �  b   �0   �ql@d 1 Fe|se3+      (  $    ! �" r�jiNc mmcS%oE ��st�in%.Formap(6Alfigqrqvion '{ =g(Ko$"fN�ndl", id);
        �0        " \sacer.Write(Tracer.T2acDLevel.ERROR, mewsqe%�;,H                    mmsSeges.Add(message);
     $ 0"     " u `(     �0    �0else
                 0 `~ali$ =ValifQ|�0SubsystemsMetadata(), configuration, ref messages);
                if (!valid && ErrorsOccurred != null)
                    ErrorsOccurred(null, new IEX.Utilities.DataEventArgs<string[]>(messages.ToArray()));
                _configurations.Add(id, configuration);
                IexServerConfiguration clone = IEX.Utilities.Tools.Serializer.DataContractDeserialize<IexServerConfiguration>(IEX.Utilities.Tools.Serializer.DataContractSerialize(configuration));
                _original_configurations.Add(id, clone);
                if (_configurations[id] != null)
                    _configurated_metadata.Union(_configurations[id].Services, SubsystemsMetadata());
            }
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting. result is: " + _configurations[id]);
            return _configurations[id];
        }

        public void DeleteConfiguration(int id)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id });
            if (_configurations.ContainsKey(id))
            {
                _configurations.Remove(id);
                _original_configurations.Remove(id);
                _configurated_metadata.Clear();
                foreach (KeyValuePair<int, IexServerConfiguration> pair_configuration in _configurations)
                {
                    if (pair_configuration.Value != null)
                        _configurated_metadata.Union(pair_configuration.Value.Services, SubsystemsMetadata());
                }
            }
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. Configurations are: " + string.Join(",", _configurations.Keys));
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting. Configurated Metadata is: " + _configurated_metadata);
        }

        public bool ValidConfiguration(int id)
        {
            return _configurations.ContainsKey(id);
        }

        private bool Validate(ServiceCollection metadata, IexServerConfiguration configuration, ref List<string> messages)
        {            
            bool result = true;
            if (configuration.Services == null)
            {
                result = false;
                Tracer.Write(Tracer.TraceLevel.ERROR, "Service not found.");
                messages.Add("Service not found.");
            }
            else
            {
                foreach (ServiceInfo service_info in configuration.Services)
                {
                    Service service = metadata.Find(item => item.ServiceName == service_info.Name);
                    if (service == null)
                    {
                        result = false;
                        string message = string.Format("{0} service not found.", service_info.Name);
                        Tracer.Write(Tracer.TraceLevel.ERROR, message);
                        messages.Add(message);
                    }
                    else
                    {
                        if (Validate(service, service_info, ref messages) == false)
                            result = false;
                    }
                }
            }
            return result;
        }

        private bool Validate(Service service, ServiceInfo service_info, ref List<string> messages)
        {
            System.Diagnostics.Debug.Assert(service.ServiceName == service_info.Name, string.Format("Name of service configuration '{0}' doesn't match with the service '{1}'.", service_info.Name, service.ServiceName));
            bool result = true;
            foreach (InstanceInfo instance_info in service_info.Instances)
            {
                Implementation implementation = service.Implementations.Find(item => item.ImplementationName == instance_info.Implementation);
                if (implementation == null)
                {                    
                    result = false;
                    string message = string.Format("Implementation '{0}' of instance '{1}' not found under service '{2}'.", instance_info.Implementation, instance_info.Name, service.ServiceName);
                    Tracer.Write(Tracer.TraceLevel.ERROR, message);
                    messages.Add(message);
                }
                else
                {
                    if (Validate(service.ServiceName, implementation, instance_info, ref messages) == false)
                        result = false;
                }
            }

            return result;
        }

        private bool Validate(string service_name, Implementation implementation, InstanceInfo instance_info, ref List<string> messages)
        {
            bool result = true;
            int cnt_default_values = 0;
            implementation.Parameters.ForEach((item) => { if (item.DefaultValue != null) cnt_default_values++; });
            if (implementation.Parameters == null || instance_info.Parameters.Count < implementation.Parameters.Count - cnt_default_values || instance_info.Parameters.Count > implementation.Parameters.Count)
            {
                // TODO: consider removing service, rather than requiring user to delete it manually or delete entire file.
                // Number of parameters of service 'IEX.Server.DLNAService.IDLNA' in instance 'DLNA Service' doesn't match with implementation 'IEX.Server.DLNAService.DLNA'.
                // <Service Name="IEX.Server.DLNAService.IDLNA" DisplayName="DLNA Service">
                // TODO: the problem with cleaning up the iex.xml is that it might be on another server...
                //XDocument iexXml = XDocument.Load("");
                //iexXml.Elements(iexXml.Root.Name.Namespace + "Service").FirstOrDefault(s => s.Attribute(iexXml.Root.Name.Namespace + "Name").Value == service_name && s.Attribute(iexXml.Root.Name.Namespace + "DisplayName").Value == instance_info.Name).Remove();
                //iexXml.Save("");
                
                string message = string.Format("Number of parameters of service '{0}' in instance '{1}' doesn't match with implementation '{2}'.", service_name, instance_info.Name, implementation.ImplementationName);
                Tracer.Write(Tracer.TraceLevel.ERROR, message);
                messages.Add(message);
                result = false;
            }
            // Validate parameters type conversion
            return result;
        }

        public Service SubsystemsMetadata(string service_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { service_name });
            Service result = null;
            ServiceCollection services = SubsystemsMetadata();
            result = services.Find(item => item.ServiceName == service_name);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public Implementation SubsystemsMetadata(string service_name, string implementation_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { service_name, implementation_name });
            Implementation result = null;
            Service service = SubsystemsMetadata(service_name);
            if (service != null && service.Implementations != null)
                result = service.Implementations.Find(item => implementation_name == item.ImplementationName);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public int SubsystemsMetadataIndex(string service_name, string implementation_name, string parameter_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { service_name, implementation_name, parameter_name });
            int result = -1;
            Implementation implementation = SubsystemsMetadata(service_name, implementation_name);
            if (implementation != null && implementation.Parameters != null)
                result = implementation.Parameters.FindIndex(item => item.ParameterName == parameter_name);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public Parameter SubsystemsMetadata(string service_name, string implementation_name, string parameter_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { service_name, implementation_name, parameter_name });
            Parameter result = null;
            Implementation implementation = SubsystemsMetadata(service_name, implementation_name);
            if (implementation != null && implementation.Parameters != null)
                result = implementation.Parameters.Find(item => item.ParameterName == parameter_name);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public Service SubsystemsMetadataServiceByImplementationName(string implementation_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { implementation_name });
            Service result = null;
            ServiceCollection subsystems_metadata = SubsystemsMetadata();
            var services = from item in subsystems_metadata
                          let implementations = item.Implementations
                          where
                          implementations.Any(implementation => implementation.ImplementationName == implementation_name)
                          select item;
            if (services.Any())
                result = services.ElementAt(0);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public void Configuration(int id, IexServerConfiguration configuration, bool save = false)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered", new object[] { id, configuration, save });
            if (save)
            {
                SetServerConfiguration(id, configuration);
                DeleteConfiguration(id);
            }
            if (!_configurations.ContainsKey(id))
            {
                _configurations.Add(id, configuration);
                IexServerConfiguration clone = IEX.Utilities.Tools.Serializer.DataContractDeserialize<IexServerConfiguration>(IEX.Utilities.Tools.Serializer.DataContractSerialize(configuration));
                _original_configurations.Add(id, clone);
            }
            else
                _configurations[id] = configuration;
            if (_configurations[id] != null)
                _configurated_metadata.Union(_configurations[id].Services, SubsystemsMetadata());
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "exiting");
        }

        public void Configuration(int id, string configuration, bool save = false)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered", new object[] { id, configuration, save });
            IexServerConfiguration iex_server_configuration = ConfigurationDeserialize(configuration);
            Configuration(id, iex_server_configuration, save);
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "exiting");
        }
        
        public bool Configuration(int id, string service_name, string implementation_name, string instance_name, string parameter_name, string value)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, service_name, implementation_name, instance_name, parameter_name, value });
            bool result = false;
            int parameter_index = SubsystemsMetadataIndex(service_name, implementation_name, parameter_name);
            if (parameter_index != -1)
            {
                IexServerConfiguration configuration = Configuration(id);
                int index_srv = configuration.Index(service_name);
                if (index_srv != -1)
                {
                    int index_instance = configuration.Services[index_srv].Index(implementation_name, instance_name);
                    if (index_instance != -1)
                    {
                        configuration.Services[index_srv].Instances[index_instance].Parameters[parameter_index].Value = value;
                        result = true;
                    }
                }
            }
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public string Configuration(int id, string service_name, string implementation_name, string instance_name, string parameter_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, service_name, implementation_name, instance_name, parameter_name });
            string result = string.Empty;
            int parameter_index = SubsystemsMetadataIndex(service_name, implementation_name, parameter_name);
            if (parameter_index != -1)
            {
                IexServerConfiguration configuration = Configuration(id);
                int index_srv = configuration.Index(service_name);
                if (index_srv != -1)
                {
                    int index_instance = configuration.Services[index_srv].Index(implementation_name, instance_name);
                    if (index_instance != -1)
                    {
                        result = configuration.Services[index_srv].Instances[index_instance].Parameters[parameter_index].Value;
                    }
                }
            }
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public string IrProjectName(int id)
        {
            IexServerConfiguration configuration = Configuration(id);
            string service_name = "StbService.SubSystems.IIR";
            var items = from service in configuration.Services
                                                       from instance in service.Instances
                                                       where service.Name == service_name
                                                       select new { Implementation = instance.Implementation, Instance = instance.Name };
            if (items.Count() != 1)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("{0} is an invalid number of IR projects", items.Count()));
                return string.Empty;
            }
            var item = items.First();
            return Configuration(id, service_name, item.Implementation, item.Instance, "Project Name");
        }

        public bool Changed(int id)
        {
            if (_original_configurations.ContainsKey(id))
                return !IexServerConfiguration.Compare(Configuration(id), _original_configurations[id]);
            return true;
        }

        public InstanceInfoCollection GetInstancesInfo(int id, string service_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id , service_name });
            InstanceInfoCollection result = null;
            IexServerConfiguration configuration = Configuration(id);
            ServiceInfo service = configuration.Find(service_name);
            if (service != null)
                result = service.Instances;
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + StringExtensions.Join(result));
            return result;
        }

        public string[] GetInstancesName(int id, string service_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, service_name});
            string[] result = null;
            InstanceInfoCollection instances_info = GetInstancesInfo(id, service_name);
            if (instances_info != null)
                result = instances_info.Select(instance_info => instance_info.Name).ToArray();
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + StringExtensions.Join(result));
            return result;
        }
        
        private InstanceInfo CreateInstanceInfo(int id, Implementation implementation)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, implementation });
            InstanceInfo result = null;
            Service service = SubsystemsMetadataServiceByImplementationName(implementation.ImplementationName);
            string instance_name = GetName(id, service.ServiceName, service.DisplayName);
            string[] values = null;
            if (implementation.AutomaticConfiguration)
                values = GetDefaultValues(id, implementation.ImplementationName);
            result = new InstanceInfo(instance_name, implementation, values);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public InstanceInfo AddInstance(int id, string implementation_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, implementation_name });
            Service service = SubsystemsMetadataServiceByImplementationName(implementation_name);
            Implementation implementation = SubsystemsMetadata(service.ServiceName, implementation_name);
            InstanceInfo instance_info = AddInstance(id, implementation);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + instance_info);
            return instance_info;
        }

        public InstanceInfo AddInstance(int id, Implementation implementation)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, implementation });
            InstanceInfo instance_info = CreateInstanceInfo(id, implementation);
            IexServerConfiguration configuration = Configuration(id);
            Service service = SubsystemsMetadataServiceByImplementationName(implementation.ImplementationName);
            configuration.Add(service.ServiceName, instance_info);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + instance_info);
            return instance_info;
        }

        public string[] Values(int id, string provider_assembly_qualified_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, provider_assembly_qualified_name });
            string[] result = new string[0];
            if (provider_assembly_qualified_name.StartsWith("IEX.Server.ServiceManagement.ServiceDependency,"))
            {
                int start = provider_assembly_qualified_name.LastIndexOf('{');
                int end = provider_assembly_qualified_name.LastIndexOf('}');
                string type = provider_assembly_qualified_name.Substring(start+1, end-start-1);
                result = GetInstancesName(id, type);
            }
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + StringExtensions.Join(result));
            return result;
        }

        public bool ValidateName(int id, string service_name, string instance_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, service_name, instance_name });
            bool result = true;
            string[] names = GetInstancesName(id, service_name);
            if (names != null)
                result = names.IndexWhere(name => name == instance_name) == -1;
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public string GetName(int id, string service_name, string base_name = "default")
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, service_name, base_name });
            string result = null;
            int key = 0;
            while (ValidateName(id, service_name, key == 0 ? base_name : string.Format("{0} {1}", base_name, key)) == false) { key++; }
            result = key == 0 ? base_name : string.Format("{0} {1}", base_name, key);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }
        
        public ServiceCollection Confi'up@|ddMetafa|`() �(     {
            return _c/nfHgurateD_�utadata;
        }

      ( Pqblic ServIceSo.lesu)�n ConfiguratedMetadetq*List<int> ids)
        {
       �1  raBer.Write(Tracer.TraceL%te�/Ih[��TE, "�nt'red",0n% object[� [b�ts });M
           f�ru!ch (int id in ids)0�`(           �_c-nfigurated_metad`t�.Uniof(cknfiguratinhqdi&Servicms$SubsystemsMetaEa`qh!);
            Tr!ceS.Write(Tracer.TraceLevel.DEBUG, "e�it+ng. result is: " + _coof�guRa�ud_metadata!3-.$    $ 0"   redurn _configurated_metadaua�
        }

      ( P�rlic ServiceC�ll'ction Configuratmdmatadcti)int id)
   ! �  �
b   �� bb" (ce4}rn Configuratef�mu�da6a(new int[]{ id }.TnL�st());
   0 `( }
 `( �8 public v�yd Co.fiFuratedMetadataze�qt()
  0 `(  {
    �1 �` _configuratet_-mtadata.Clear();
        }

      ! �ublic void SubsysteosEdtAd�daRes%p(�
b       {�
 b   ! �    i& (~seb3qs4Em�Ometadata != null)
        " (!{
                _subs{s|dm{_Mada$ita.Clgaz));M� b! �          �su systeMs�}etadata = null;
            }
        }

        public void ConfigurationsReset()
        {
            _configurations.Clear();
            _original_configurations.Clear();
        }

        public void Reset()
        {
            ConfiguratedMetadataReset();
            SubsystemsMetadataReset();
            ConfigurationsReset();
        }
    }
}

