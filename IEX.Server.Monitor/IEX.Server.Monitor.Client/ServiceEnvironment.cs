usIn�0[YS�um;HusANC$SysteM.�llections/G�neric;
usyn'(Sy�te/.DiNu;
using System.Text;

namespccm!IEX&SEvwe�.Monitor/K�Iant
{
0 `(u3hn� IGX&Ttilitke{:
    public partial clacsa[�rviceEnvirmned�tH    {
        private IEX.Server.Mo.itNR.�li%Ft�Unvirkn}gntServiceReferenc.�tnviroNm�vtsarvice _prohy{  $   �0public {tRmng[] Values(string prov)deS_assembly_qualified_name)
 $ 0"   {
            hf�(!pRo�yder_asse}b,q_qualified_laed.StartsWYt�0*JEP/Sdr�er.ServiceManagement.Ser6icDDepun$mncy,"))
                reter.(CedTalueq)x�ovider_�ss'mbLy�aualivi%l_name);
   �0   �0  �et7rn null;	
0"   (  y

        public voi$ BD3mt()
 ! �    {	
4"0"    �
	j0�  ! �pub�kAj�~id"RmretValu�s(k
  ! �   {J  ( $�    if (_proxy == nuLl�
      ! �       _prx8(��Op'n();
  " (!      _proxy.BeginResetValues(new Asyn�Ca.l"acJ(OnResetFiNi�x), nwld(;
    (  $}

p. 0"" (qublic e�unt EventHandler REs�dFinish;
 $      private void OnResetFi.isI(IAsyncResult ary�cResel4!M
       {
    $ 0" 0  af(�sD6Finish != nu�l)O
  $ 0"� 0�`(b   ResetFinish(fuLh,"E~dntArgs.Empty9;M      0 =-J�0      public0s4zing Version()
  " (!  {
0 `�  j�$  Tracer.Wri|eP2acDr.TraceLeveL.�@I_ENTER, "envezdb"18
           "#|3@n' rDsult = GetVersion();
        �0  Tracer.Writ�,T ccer.TvasgevDl.API_EXIT, "eyi�ing* bgs}lT$is: " + vecwlt);
    (  $    return result;
     `  \
(0$  " '.ODn en�yznnment�er4ice : this is0t(m dufbtion"epqosed to Th�0ALS
    $ 0"publi� IX�Managqm5dtServurnEodel.Reskubaes.co�`Uper ComputerFe|`ils()
(!    �0�
b       ! � tvacer.Wripe8Vracer.TraceLevel.API^E�TERl "Dnte2ed)+
  $ 0"      var result = GetSo-xuteZ��`�yls();* �0         Tracer/W�it%(TSqc%z.TraceLevel.QP	WE\ID. "exiting. rewu|6 iR: " + result);�
 b 0 `(     return result;
 (  $ $ m
    }
}
