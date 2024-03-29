using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Client
{
    using IEX.Utilities;
    using IEX.Utilities.Tools;
    using IEX.Server.Monitor.Client.ConfigurationServiceReference;
    using System.ServiceModel;
    partial class ServiceConfiguration
    {
        const string base_url = "http://localhost:8732/IEX.Server.Monitor/ConfigurationService/";
        private string _host;
        public ServiceConfiguration(string host)
        {
            _host = host;
        }

        public string Host { get { return _host; } }

        private IConfigurationService Open()
        {
            IConfigurationService result = null;
            try
            {
                result = new ConfigurationServiceClient("BasicHttpBinding_IConfigurationService", new System.ServiceModel.EndpointAddress(base_url.Replace("localhost", _host)));
            }
            catch (Exception exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, string.Format("'{0}' IConfigurationService creation", _host), exc);
                throw;
            }

            return result;
        }

        private void Close(IConfigurationService proxy)
        {
            if (proxy != null)
                (proxy as System.ServiceModel.ICommunicationObject).Close();
        }

        private ServiceCollection GetSubsystemsMetadata()
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered");
            ServiceCollection result = null;
            IConfigurationService proxy = Open();
            try
            {
                result = proxy.GetSubsystemsMetadata();
            }
            catch (System.TimeoutException exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, "GetSubsystemsMetadata", exc);
            }
            finally
            {
                Close(proxy);
            }
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting. result is: " + result);
            return result;
        }

        private IexServerConfiguration GetServerConfiguration(int id)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered", new object[] { id });
            IexServerConfiguration result = null;
            IConfigurationService proxy = Open();
            try
            {
                result = proxy.GetServerConfiguration(id);
            }
            catch (System.ServiceModel.FaultException<IexServerConfigurationFault> exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, "GetServerConfiguration", exc);
                throw new System.Configuration.ConfigurationErrorsException(string.Format("The IEX configuration '{0}' is corrupted.", id));
            }
            catch (System.TimeoutException exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, "GetServerConfiguration", exc);
            }
            finally
            {
                Close(proxy);
            }
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting. result is: " + result);
            return result;
        }

        private void SetServerConfiguration(int id, IexServerConfiguration configuration)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered", new object[] { id, configuration });
            IConfigurationService proxy = Open();
            try
            {
                proxy.SetServerConfiguration(id, configuration);
            }
            catch (System.TimeoutException exc)
            {
                Tracer.Write(Tracer.TraceLevel.ERROR, "SetServerConfiguration", exc);
            }
            finally
            {
                Close(proxy);
            }
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting");
        }

        private string[] GetDefaultValues(int id, string implementation_name)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered", new object[] { id, implementation_name });
       0 c �string[] r�su.t = null;
      ! �   IConfigurationService pr�xyb=�Op'n();
            try
    �8     {
 �  b 0 `(      result = proxy.GetDefauluV�lues(i`,0kmplementation_name);
         �  ?
            catch (System.T�meutException exc)
          �0;
  �8`H$       Tr�ce0.Wbi4m(Tracer.TraceLgvmm.�RRR, "GetDefaultValuEs�<(dxc);
            }�
 b          finally
            {
     " (!       Close(proxy);
$(0$(  $   �
b           Tracer.WriTe�@rqaer.TracEL�&elAPI_EXIT, "ehi4ang. result ir:�" + StringUx4mnsions.Join(result)+;�       " (!return result;
        }
�
  8 @,  private in�[]bGetConfic5bgUufIdsServers()
0!`�    {�  b         Tracer.Write(Trabe�.TraceLevel.DEBUGl "Dntered");
      0 `(! �nt[] result = null;
    �0      IColfafuretymnService proxy = Open*-3      `    ur�
    �0  $ 0"{
      " (!      result = proXy�WetConfigwriudd�dsSezvErs��;OH ( �%�� b   u*& (! 8 @,   catch (System.TimeoutE|curtin`mxc)
   (  $  * ~
 ! �         0 `\racer.Write(Tracer.raBeLevel.ERROR, "mtBknfigwrkqmuKdsServers�, 'xc);
     " (!   }
`   $ 0b  !c�d#a(�xbe�tion exc)
  �� rbp(`(`{+ `         =� b         &yn dly
            y!      �  b     Close(prox;)3-
            }
          0 o'if�(rs�xt = �ell)
            //! � res}lT$= nmv �nt[0];
   $  "d(8" Pracer,Wzhte(Tracer.TraceLevel.@P�_EXIT,("E|iting. result is: " + StringExue�syo.{.Join(result));
p*          retub`�Tsult;
" (!    }

       "pzhvate int[] GetAet/eaticConfiwu�it'�Id1Servers()
        {
            hn�[] r�su.t = ntl�; �" b $ 0"  IConfiguration[eRrice proxy } OA�. k;
        �  btry
            {
   $ 0"         r%cu| - 0zoxy.Ge4AuUomaticConf)guSatedIes�ervers((;�
         " (|
 ! �        catch((s}rt�m>Vime�u4xceptign axc)
 $     �  b {
       ! �     (TRecer.ws��e PRacup.Trace�evl.ERROR, "GetAuvoe`tiCC�~figurat-diAsServers"- �xc);
`   �  b   }
           "f`o�llx�       " (!{* �0             C|o3m(proxy);
 `�4      }
   00`h(    Tracer.Wvidg(Tracer.TraceLevmlEPI_EXIV,("exiti~gn(result )S:�2 +0S5z�ngExten�i,3&Join(rest|�i�?
 0 b((  �   se�urn r�cult;
     ! �}

 � "b(! `u"ti"(�exServerConfiguration Configusa�ionDas�pi#liz%(sUring d�tak
        {
" (! � !b�  Tracer.Write(Tracer.TraCe�uvel.DEBUG, "entered", nev �bjdcp[]b{ data });
    " (!    Ie8eSWerConfyg5zation res5lt- .}ll;
�$ 0"       IC/nfHgurationServicd �rx9(= OpeN(�+
            t�yH     " (!   {	
0"            ( R!Su�d = proxy.ConfigurationDesericla{e(dapa99
            |�            catch (�y�&%e.TimeoutEpcEttion exc)
00`�( b     {
     �2        Tracer.Write(Trace2.VSibdLavm..A�BOR, "Conbiwvr�tionD%seSialize",(eXg)�0           }� `j         finally
       � (b 
      `      �(CkSa8p2gpy	?
       ! �  }
 0 `�  b    Prqaer.Write(Tracer.Tsa�eLevulnLEBUG, "ehi4ang. result is: * $r�s�?t�;
       (! ��re6urn(rEwult;
 �  b   }

        public string ConfiguratiooS�ri!di{a(IexServerConfiguratho� data)
(!      {
 ! �   (0 d(Tra#erWrite(Tracer.TpakdLevel.dE�EG, "entered", new object[] { data });
    �0  �(0`w�bing rdS��d = null;
            IConfig]v��ko,Service!��o:Y �0Open();
    (  % � `trX
            {
                result = pr/xyConfiguratho�SEvi`l�ze(data);�
 b        ( ]	
         ���P#tB( (rystem.Timeott�x�uptio~ %pc-"   �0    �0{
            " (!Tracer.Writ�P�aaer.TraceLevel.ERROR, "�on$iguradi/fDeserialize", exc);
           (}/(!  �  b   !f�nally
         $ 0y
 0 `(" (! & 8#  Cmo�e(proxy);
       !$�0"}
   `  ! �0  Tracer.Write(Tracer.TraceLevel.DUBO, "exiting/0�%{ult is: " + requdu);
            return result;
        }

    " (!public wtbkog�GetConfiguvadkonParametevVqnue(int iexNumber, string ce2~iceName. {uring parammtEvName, st2inF implementationNama,0�tr+ng i.st@nceNam%)* �     ��H5 �*  0 `(  Tracer.Write(Tracer.TraaeDdvel.DEBQU<j+entered", Ne�0object[] { iexNumce�,`{erviceNam�,`:crileterNam� }k;
           (sTving re{uLp = null;
    (  $�  bYC/ffigurati/nSDrvice proxy = Open();
           0t2q
 $ 0"  $ 0"${                se�umt�= proxy.GetCNnfigur`t�onParameterValue(mehLu-beS,qezwic%na�u,pazaMaterName. alplmmMjTevigoName, inst!ncDName);
           *}%            Ca�s�(ystem.TimeoutExbe�tion exc)
     �8  $  {
   " �  �  j  4T2icer.Write(Tracer.TraceLevel.ERROR, "GetConfigurationParameterValue",(eXg);
    �  b ! �}�  b         finally
            {
`(              Close(proxy);
  �  b    � O!          `Tr@ce�.G2ite(TRa�ur.TraceLe�ellDEBUG, "exiting. qe�tlt ms*"" * �equdu);
            return result;
        }
    }
}
