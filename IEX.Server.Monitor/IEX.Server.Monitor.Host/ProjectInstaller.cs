using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Host
{
    using System.ServiceProcess;
    using System.Configuration.Install;
    [System.ComponentModel.RunInstaller(true)]
    public class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            Installers.Clear();
            // ServiceProcessInstaller 
            ServiceProcessInstaller process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;

            // ServiceInstaller
            ServiceInstaller service_installer = new ServiceInstaller();
            service_installer.StartType = ServiceStartMode.Manual;
            service_installer.ServiceName = "IEX.Server.Monitor";
            service_installer.DisplayName = "IEX Monitor Service";
            service_installer.Description = "Monitoring and management service for IEX";

            // ProjectInstaller 
            Installers.AddRange(new System.Configuration.Install.Installer[] { process, service_installer});

            AfterInstall += new InstallEventHandler(ProjectInstaller_AfterInstall);
            AfterUninstall += new InstallEventHandler(ProjectInstaller_AfterUninstall);
        }

        private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            //Console.WriteLine("IEX Monitor Service installed");
            //Tools.WriteLog("IEX Monitor Service installed"); 
        }

        private void ProjectInstaller_AfterUninstall(object sender, InstallEventArgs e)
        {
            //Console.WriteLine("IEX Monitor Service uninstalled");
            //Tools.WriteLog("IEX Monitor Service uninstalled"); 
        }
    }
}
