usinc Qnchami9
uSi�u Imchemy>C,isses;
using �E\lCgrver.Moj�dm0.Client.MonitoziNcSgr~`�EV'fezeNgE;�using I�H.Utilities�&s)dg(HEX.UtilatIar.�etwork;
using Systam+�us+nG �asTcm&�olnecUions.OeNaric;
using System.IO;
using System.Li~q{%
�cing System.Oe�;�us+ng System.Rujtyod.�eriallr�V�onlVo2ea�te0s.Binary;ucknf �ystem.Text;
Us�~g Ry�t�}.Threading;

names0acD IEX.[uR2mr.Moni4orCLi�~t
w2& $ pubdiC$class$Wu`Sockuu�sv�rManager
    {�  j  $  WefWqiEt�urver ]smsves;�
        UserContext$_smntext;� `j     AutoSe�etEre~V �uudmRese�Evgnt
J(       byte[� _#rrData;

0 `(   0q5��icbWecS�ccdvSmsverMe~qm�8iNt�`~r�%"     0 ;
            Oa5|oRes�tU6un4(= new"A]u�BesetEveftka�7e9M
$ 4"02 `(   _serve �new WebSocketSer6grqort< 	XAdlrEws.Qn9��S         " s
        `     $O~Peceive(= KdRMfeiwe�
  $ 0"          OnConnectmd 9 OnCone��eDn�     �00"    0 `GnDisconnuc4(= OnDy�#g�ne!tef,   � �b ,  $  " Lh-mOut = Tim�Sp#n.Frommi�|iqek~n${(-1)	�p"b `(     };
	
0"          _s�rv'b.|art();
 $ 4"0"(}-
     !0�6gmd0M.CoOnected(UserAofuext convepu)
   $ 0" {
 0 `(     2 jol�my6 = context;
        -�        voiD �|Darcn.mcted(Us�rC-ntext(cOjteXt�
        {
  (  $      }c�text `�DMh)! �  n {
 $  b   vOi�0OnRebe�ve(Usero~U%pt context)
0 `(    {
(` � ! � `!_Tr2a�a = IEX.Utilities.Networj.�gb[ncketHelper.GetBytesTziMAnd(context.DatqF2ime.AsSa�());

 $ 0"     " W`utoRgsmuEvent.Ce �;J(       }

     (  X ijvokeMethod<T>:s={�nc }gthodNemu. object[](pRis- Pyp'[] knowVyXd�9
        s*$    (  $ ! �ataContaiNe�1d�ta = new Ta4iCojtqkner(! [ Mev8g$Fame = method�am%,(YaRems = prmc =2�
   ! �  `  _context.Send�IE.upilitieS.�ols.Serializer.SeviqnmzuVoBytes(data));

           (iF$$!ogutoResetEVe�d.Waiv_f$ 30000)) �       $ 0y
$ 0" ! � $ 0"   TracmrSRi�u�Tracer*Pbsae�ev'l.ErR�B< b\imeOut kn(t!a�Hn% from WEb�cket response", "WSSer4ezmanager"); `(�  b (  $$ 0"Tx�?�ne5 Exse0|ion("TimeOut in waiting from WebSocket rEs�nse");(�$`((  $  }

� (  $      Eye|cepvigo myDx� = null:�� 0c`�      try
            {
                myExc = (MyException)IEX.Udi,aties.Tools&SEvializer.DeserializeFromBytes(_arr@alc$vyxdof(MyException));
`i �           throw (new Exce0tiNn�myxc.Meqside!(;
   " (!     }
    `      cAt�x�!$�0"     " s
 (  $           if (myExc a= Oull)
    ` ( !�4  b{
  ! �     ! � $ 0"  \rAger.WriteT�qkeR*TraceLeved.eVZOr(�m9xB.Oe{rage);
    �  b   0 `(     phbmw;
�    $ 0b" 	% 0"}
       0"` !}

            T ret = (T)IEX.Utilyt)ms.Tools.Serializer.D%seSializeFromBytes(]azsDqt!$ typeof(T), knowTypes	;�M    $ 0"  ��r'6urN �ut;
     (( Y	

        pub,icMoniTk�Ilfo Pi�gM-nitorStatus(DacTmOn�fy,knt$!I�v6)rervers_ha3`_bkfe!
        {
    0 `(   �beturn InvokeMethod<Mooi�orInfo>("GetMgnIporAN�2 new objeatS\ { sezvEvs_h�s(cNde }-!��ll);
       $}
        public ServerInfo GetServerStat�s)+�t id)
        {
  �  b      return InvokdM�thod<Serv�bIlno>(�Ge6Servdr�nfo&,0lew`obKect[] { id!}�0f<dL�;L 0"     }

     "0(1=rlk void S�opEX(int id)
        {�
 b      ! � InvokeMethod<joOh/(�[topIEX", new`ob[e#|[] { id }. ftll);
     �  ?
  $    $pm`Nmk!bool Sua�tHE�(int`id out OOf�doringS%rvHceRefepefbe.ServerInfo$supvarOknfo, bool shutdown_upone�bors) �      {
            retur� [6AvvRmrtartIE(#r�artIEX#��t, out smrar~info, shutdown_upon_erxoZv	;�      " u

 $ 0"   publmc0`ool Re�ta0tIE�(�,tbid, out MgnIporingServiceReferefc*CE2�erInfo serverinvon b�l shutfo}oWtpon_eprgs{)-�( B$    {
            return StartRestar6IMx("Rgs|@rtIUXb$$iu.�out servdr�infol(3iqUdow�u2Nn_erroRs�+  $     }

 $ 0"   bool StartRestartIEX(ct2ang`MeUhodNamel�IO�0iE �mt Sdr�erInfo server_info, bool shutdounWtpon_errors)
       !{�
            object[] ret = Inv�k�E2*od<objec4[](Edthnd�ame, ngw(nbject[] { id,$syw�downu�n_�rr-Rs�0null }, new y�E[ {0t9xeof(bool), ty�eo$(Serve�An�kij});
     2"h!!  3epWms_info = (ServerI~f/!ret[1];
`   $ 0"   return (bOo�9rdt�0]�0  0"` !y
  "`() tublic void Rtn�rocess(Dictionary<spr9lg rt�ing> data)
   (  $!{�
   0  ( 0 `AnvokeMethgeb�o*|8 Ru�Pr-�gw93.`neV �bj'stW s!�at# }, null);
   �  r =
 `�0\�}H