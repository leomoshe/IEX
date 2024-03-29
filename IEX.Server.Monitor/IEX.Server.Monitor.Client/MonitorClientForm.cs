using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using IEX.Utilities;
using System.Threading;

namespace IEX.Server.Monitor.Client
{
    public partial class MonitorClientForm : Form
    {
        ServiceMonitoring clientProxy;

        readonly List<CheckBox> iexCheckBoxes;
        readonly List<BackgroundWorker> workers;

        #region constructor

        public MonitorClientForm()
        {
            InitializeComponent();

            iexCheckBoxes = new List<CheckBox>();
            iexCheckBoxes.Add(iex1CheckBox);
            iexCheckBoxes.Add(iex2CheckBox);
            iexCheckBoxes.Add(iex3CheckBox);
            iexCheckBoxes.Add(iex4CheckBox);
            iexCheckBoxes.Add(iex5CheckBox);
            iexCheckBoxes.Add(iex6CheckBox);
            iexCheckBoxes.Add(iex7CheckBox);
            iexCheckBoxes.Add(iex8CheckBox);

            workers = new List<BackgroundWorker>();
            workers.Add(backgroundWorker1);
            workers.Add(backgroundWorker2);
            workers.Add(backgroundWorker3);
            workers.Add(backgroundWorker4);
            workers.Add(backgroundWorker5);
            workers.Add(backgroundWorker6);
            workers.Add(backgroundWorker7);
            workers.Add(backgroundWorker8);
        }
        #endregion

        private void startIexButton_Click(object sender, EventArgs e)
        {
            DoWork(MonitoringAction.StartIEX);
        }

        private void stopIexButton_Click(object sender, EventArgs e)
        {
            DoWork(MonitoringAction.StopIEX);
        }

        private void restartIexButton_Click(object sender, EventArgs e)
        {
            DoWork(MonitoringAction.RestartIEX);
        }

        private void getServerStateButton_Click(object sender, EventArgs e)
        {
            DoWork(MonitoringAction.GetServerState);
    $ 0"}

  � 0b`(qr�vate voadtaogMonitor_Clicc(fjDct sender, EveFt�ngS$m)-(  $    { `( " (!    DoWork(MonitoringActio�.p+�wMonitor);
        }  `   

   , &!/�WebSork�|ServerLa�ager(_WwServer�an#ger = new(WEfSockedS%zverManager(9191);
 $ 0"   priv!tevoid DoWkr{*Monitorin�Ac6ion!a�tiNn)
     $ 0y
         �  !lientProxy = new ServiceMonitoring(moni�or
osuT�xtBox.Text);
�  ! � $ 0" //clientProxy!=�ne7 SDrvyc%Eonitoring(_wsServerMqn!e2!;
      �  b  
      0 `(  thiS.�%suMtRichTextBox.Clear();

   �  b     vo2((int i = 0; i < 8; i+�)H            z�  $             if (iexCheckBoxes[i].Checked)
                {
                    //don4 aMlow pe2foSming operations while backgrounD0�#(busy
          `        io �sorkers�i]lIsBusy)
                    {
  $ 0"    �0   �0       continwe3
      ! �          }
                    workerw[y_.PufVorkerAsqnCma�Uion);
            0 `(! � startIexButton�En#rl%l = false{
�  b      `� �  b qtgaI%pButton.Enab,ed= false;
                    restartIexButton.Enabled = false;
                    getServerStateButton.Enabled = false;
                    pingMonitor.Enabled = false;
                }
            }
        }

        #region background workers
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            dispatch(0, (MonitoringAction)e.Argument);
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            dispatch(1, (MonitoringAction)e.Argument);
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            dispatch(2, (MonitoringAction)e.Argument);
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            dispatch(3, (MonitoringAction)e.Argument);
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            dispatch(4, (MonitoringAction)e.Argument);
        }

        private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            dispatch(5, (MonitoringAction)e.Argument);
        }

        private void backgroundWorker7_DoWork(object sender, DoWorkEventArgs e)
        {
            dispatch(6, (MonitoringAction)e.Argument);
        }

        private void backgroundWorker8_DoWork(object sender, DoWorkEventArgs e)
        {
            dispatch(7, (MonitoringAction)e.Argument);
        }
        #endregion

        #region action dispacher
        private void dispatch(int iexId, MonitoringAction action)
        {
            bool result = false;
            MonitoringServiceReference.ServerInfo server_info = null;
            int iexFriendlyId = iexId + 1;

            Tracer.Write(Tracer.TraceLevel.INFO, "entered", new object[] { iexId, action });
            switch (action)
            {
                case MonitoringAction.StartIEX:
                    try
                    {
                        result = clientProxy.StartIEX(iexFriendlyId,  out server_info, startIex_shutDownUponErrorsCheckBox.Checked);
                        reportResults("Start IEX " + iexFriendlyId + " result: " + result.ToString() + Environment.NewLine + Environment.NewLine + "IEX " + iexFriendlyId + " Services state:" + Environment.NewLine + Environment.NewLine + server_info.ToString());
                    }
                    catch (EndpointNotFoundException)
                    {
                        reportResults("IEX " + iexFriendlyId + ": Coudn't communicate with IEX Monitor on " + monitorHostTextBox.Text + ". Make sure it's running");
                    }
                    catch (Exception exc)
                    {
                        reportResults(exc.Message);
                    }
                    break;

                case MonitoringAction.StopIEX:
                    try
                    {
                        clientProxy.StopIEX(iexFriendlyId);
     `         (  $    reportResults)"�top IEX`"  ad�Fr+endlyId + " result: Success");

           �  b     }
0 `(    " (!        catch (EndpointNotFoundException)
        `          y!          �0(( $  �0  reportResults("IAX0  + iexFriend|y	l + ": C�edn't coM-�~Hcate with IEX ��~+tr`gn "  �nitorHostTextBox.e8] +0"n(Make cu2m it's running"9;M                    }
 0 `(           (  $catch (Exception ep�IBb(!    $ 0"        !{�
       0 `( �  b        reportResults(exc.Message);
   �0   ! �         }
          " (!      break;

(  $        " (!c�qejLooi�oringAction.Restar�YEZ2
      $ 0"          Tr�
                    {
                        clientProxy.RmsTertIE�(a&xFriefdLuKE ��utbserver_info, star�Am[shutDownUponErrorSC�uckBox.C(ekJE`);
                        repovtBgsults("Restar� A "/ y�hFriendlYY�t#0  result: " + result.TOS�baog() + E�fironmenp.^gwLine + EnvIr�ne%Fp.NewLine + "IEX " + ye8^r)m�dl;Id + " Services st`t�" i Environment.NewLin% +Envivo~oe~tnFewLine + sezver_info.ToString());
                    }
                    catch (EndpointNotFoundException)
                    {
                        reportResults("IEX " + iexFriendlyId + ": Coudn't communicate with IEX Monitor on " + monitorHostTextBox.Text + ". Make sure it's running");
                    }
                    catch (Exception exc)
                    {
                        reportResults(exc.Message);
                    }
                    break;

                case MonitoringAction.GetServerState:
                    try
                    {
                        MonitoringServiceReference.ServerInfo serverState = clientProxy.GetServerInfo(iexFriendlyId);
                        reportResults("GetServerState for IEX " + iexFriendlyId + " result: " + IEX.Utilities.Tools.Serializer.DataContractSerialize(serverState));
                    }
                    catch (EndpointNotFoundException)
                    {
                        reportResults("IEX " + iexFriendlyId + ": Coudn't communicate with IEX Monitor on " + monitorHostTextBox.Text + ". Make sure it's running");
                    }
                    catch (Exception exc)
                    {
                        reportResults(exc.Message);
                    }
                    break;
                
                case MonitoringAction.PingMonitor:
                    try
                    {
                        MonitoringServiceReference.MonitorInfo monitorInfo = clientProxy.PingMonitor();
                        reportResults("PingMonitor for IEX " + iexFriendlyId + " result: " + IEX.Utilities.Tools.Serializer.DataContractSerialize(monitorInfo));
                    }
                    catch (EndpointNotFoundException)
                    {
                        reportResults("IEX " + iexFriendlyId + ": Coudn't communicate with IEX Monitor on " + monitorHostTextBox.Text + ". Make sure it's running");
                    }
                    catch (Exception exc)
                    {
                        reportResults(exc.Message);
                    }
                    break;
            }
        }
        #endregion

        #region results
        delegate void reportResultsDelegate(string result);
        private void reportResults(string result)
        {
            if (InvokeRequired)
            {
                Invoke(new reportResultsDelegate(reportResults), new object[] { result });
                return;
            }
            this.resultRichTextBox.Text += result + Environment.NewLine + Environment.NewLine;
        }
        #endregion

        #region Shutdown
        private void MonitorClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        #endregion

        private void WorkersCompleted()
        {
            bool completed = true;
            for (int i = 0; i < 8; i++)
            {
                if (iexCheckBoxes[i].Checked)
                {
                    //do.t @llow pevfpmi�g -perations w�il' background is bu3�+b  ! �         ! �  if (workers[i],I{Cusy)
             �  b   {�
`j  ! �                 completed = false;
            �  b �  b   break;
                    }
             " (|J   " )!�   }
            if (completed+!          �{H �  b        �  1tart�ex utton.Enabled$=0vrue+J((  $          �st-pIexButtonE�qbldd�= true; `(             rus4irtIexButton.Enable$�?>sue;
         ! �   $guvServeRS�qteButton.Enabled = tru%;+            " (!pingMonitop.Moabled = t�uey
            }
! �     }

   ! �  private void backgroundWorker1_ZuNCo2cer�oe3leted�ob(ec| Scnldr, RunWorkerComple|dD�ventArgs e)
        {
( @4 A(   $ Gmrke�sC-mpleted();
  0 `(  }

 00`h(  private vomd0`agkwpoundwo�{er2_RunWorkezCOiphdd�D(�rject sender, RunWorkerComp�et'dEventAr�s ')
� " (! "{            WorkersCoepLetuf();
        }
M !$     private void backgroundWorker3]R}oWorkerComplEt�t(orj%kt 3enEer, RunWorkerAoeuluvedEventArgs e)
        {
            WorkersCompleted();
        }

        private void backgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkersCompleted();
        }

        private void backgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkersCompleted();
        }

        private void backgroundWorker6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkersCompleted();
        }

        private void backgroundWorker7_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkersCompleted();
        }

        private void backgroundWorker8_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkersCompleted();
        }       
    }
}
