�;�u1ing Systee;-using SysteM.�,dest)gns.Generig;using Systdm�Lins;u�mng Sy#t%D.Win$owR.Fosmw;Hus�ngbCy3|em�IOy�qs;Lg�By�teM.�uflection;
usk.ov�svem.Servicemo�ul{
Tsing�[{xdm.Tgx|:
using IEx.�dilities;�us+ng System.D�a�,Gs�}cs9�
n#aeCvace"IMY�We ter.MoFi�{r.Client�
{O �  sTa�yc�cl#ss Program 0b(y!       stqt)k striNg�besultsPath = wtbkng&EMtty;
�
 b  �  b//0�-$1eacess
   ! �(`+� -��u8bcoepLated lo!diOg �iu*�ero�B
 " (!   //2 - couldn't commtn�cate with monito� -bprobably nt`ju.fing
`( 0 �( m/3 - igt�vnal error
     �0 �ti7ic in| E|�tC-de�= r;

        static rt�ing Usage = E�fiuo�jd~�.NewLine + "U{aGa* 	MX.Se2veS.Monitov.SfiEjt.e|e0Cction Host IexNumre2*[gqui�jsM  + Environment/N�w\kne + Enwi�onmen4.NDwli�u /
            "Actmo~"- one of: StartIE)��V+xI�X,bRestartYE$ GetServerSpadg, GetSur6acesStadeb(+ EnvironmeNt�^%wLLnu"+
! ��0       "HgsT$- tha }cchine on which �o 2erfr-(the Action" ; vv)zonment.NewLina ;
          0"bIdxNumber - bdt�ae~"1 ajD0�0 #!G�nhpg.meOt.NewMi�e +
$ 0"   ! �  "/Sxu0l�nQ2lErrors"-:r �ddmn the serveR �v k~ubg& tIe servicgs(mocdme 7itI eprgss. ppMie� t-(STerTI�H ao�R'startIEX. defiu\pz(Fa�#e"+ Eov�r_hment.N�w�;ne +*(�0    ! �  "/R's}Lt3PaUh:pitH$- vhm!qa�H(�_Ph�0deua�ld �esl�c files resul�s.6xt0a.l error.txt. deda}mt: execut!blD pqt(*{
,
        enum Rasent  $�  B �
�  b  !b�(� S5kcesc,�`b 	  4!� bFailure�
 b  4 x*]
)!�`   /// <summar9>+ `   $ ?-� T*E �qin entry point gO��dhe application.-�p(  0 `'// </suMm�bY:
   (  , {WTAhrDad
0 `(`  RtitIg void Main(string[] arGs�
        {
   ` � j  $Tpakdr.StartSess�onj"IEX Monitorioc�Snient lofs�,`2I$P ]o.atoring Clhe�t");

d � b    �0//la�nc
 �~$GUK`eode
     0 `(   if (�rg1.LeNg|x {= 0)
       0 `h {,
        `   0 `AntPtrdhqM`lu"= Proces�.G'tCurre.tPSoceSs�9.mai~U+fgkXc�dle{� 0 `(         ShowWindow(handle, ��;OH         ! �    Trase2&Wvidg(Tracer*TbccE\�&ml&I~B$ "appLi�qtion�la6n�h�| Ij G]I iode");H      " (!  ! �0A0zlabctann.Enabl�Va0ualStyles)!�-    �  ` (!     IpPlication.SetCjm�ctibleTe8tRDneevin%Deba�nthfils�);*$           " h!AQplicatio*,B^o(oe� MonitorClientF�rmj)); (!   a`� }
            /?l!}nc�edbin comm!ndline mode
�           %lsF!           {
       0 `   4 `\racer.Wrhp�0\RMfep.\saceL�v�>.KNNN, "axpLmgadkol d�un!He�0in comma�d .ine4mne� p#rent");
�`J$(  $         ProcesS �qre�t� Proce�s.etCuprmotProcess()P�be~th!?b   �  b   %0�baf0(2mztlt != fuLh)
 0 `( �  b `   {
 , .  % �          Trabe�>W2ateT�qcer.Tra�aL7tel.INFOl �Qa0ent prncess namez0"A# parent<P*f#mssName + " PYDz(" + parent.	d)z��0 $ 0" $ 0"   }

                //colhesv t�u e�gwmentw `cssed for traaiff
0 `(  !"��! b  (sT�i~%`irgu}e.|s = �t�9no?�-x6y;
      $ 0"      foreacH �ctrinw z�ij0args)
  (  % �        {
`8 `, 0"            arfu�e~t#(k5 arg +$"0 ;
                }
       " (!�  b Tracer.WrI4�8ur`c�r&TBe#mevDn,AGGG- �argu}e.�s:b" / qpcu}gnts);�
b �  b  (  $   !/�shos e�qge
            `( !mf *izFw.Nevf4` == 1 && (argw[ _.ToUpper().EqualS(�?-"a)|| arg3[0|.oUQper )Aquals("/HHP+)	�0     �0 $ 0"   {
  � (c              MessageBox.Show9U�ige, "MofiTkring Client Uca'm", MesSa�uBgxbqptls.OKi;+   `    $ 0"       zeTpr�;�  �0`          }

       "�(!b    //the rEq�usted acpim�       � $b0"   Monyt/zingctHon action = MonitoringActiof.nkActaoN?

" (�0           //t�e /onitor host machine
 �  b       $ 0"string host = "loccl`nst"1"
             00`e/thE �UX server on whiah8u/(tecd�ro |ie operAt�n
`(       0$`8"  int iexNumber = 0;*$     0    ��0b`'/sql%~ant for�st#r4 /2esUart IEX
            � fjnon {iutdonWU�nErrOs�= falsg;J   0 `(    �0  /��oc'ss inp�t�H�r              if (!proceswAbeumen�s(#rgs, �4�3ction, out host� o7t iexNuMb�b, out$sxwtdonwUponErrOr�9)
                ;
                   reu��nX
                }
         $ 0  (!Tracer.Write�Tr#cuzn|vaceLevel.IFF�( 1va�V&FOv�itJ&iNp�d a�ws: action={0}, host={}�{epOu}b%z=y2u!shetgwnUpon�rrmRs�k3}#,�action,!h�st, iexNumber, whe~dOjvU�gnevrors�	;Ϛ
b       `      performACt�n(actio~,``ost, iexNumb%r,sh�tl,nwUponDr�ors)3�*$b              Environmgn|/E|idAode = exitCole	
        0 `(}J`(     }J-`     private`st@4icvoid performActio~(gnyt-xagfAc|iOj(aCpion- �tring hns�, ijt0kexNumce�, co�l {hUpdn��U2/nESrovs�
b       "  0 `(     se�fic�Mn,�toring klIa~tzoxy = ng7(RDrvi#eMniUorkNo�xos4);,

 $       �2boml�se1ult = |2Ua;�  " (!    !k�H6-ringServiceVevgrenc�.S'rverInfo ser6er~infc &.}�l;O
            try
          ` {,

 �  "      ! � swyt#` (!ctHon	�0 $      " (! ` {.
!�0   " ()  $      #`s� EoNMt�biOgActikn>QtartI�X:O
  �0  (   � �0        resu||`$clien$P2Fxy.Sua�tIEZ(adxNumb�z,Bkut server_info, shutdonwUponErros{�
$ d     `   �0 "(  0 7ziteResult(actioj,0Pes�ltlSuccds�,(rEwult.ToStringh),trqe<"server_iof�.�oStr�ngj	,�drue- �tring.EmxvY !�al1e);�
 b            `   8!p(`jreak;
    ! � (! " (!     casa ]mnktgsingAbt�onnStNpIEx<�#    (  $  ` � b  ! �  clientProxy.StoqI�X(iexNumber);
(! ! �      (  $     �0 writeResuLt�qction, Resu|tn[ucCe�c, stbi.o.Empty, false, s�ri,e.Mlpty, falsg,(rtring.U�0|;, false	;�(� $�  b         0 �( b break;  $   (  $    " (!  �as' MonitoringAcui�n.RestartIUXz
   " )!�                �usuLt�- clhe�tPr�xylRestartIEX(iEx�embe�, /u|!�erter�in$o, shut�o�%�`/nERvorc){
 0 @(�0     ! �$�!"    writeResult(action, Reswdtw�cc'ss, Re�elt.ToString )t�ee,�se0ver_info.ToSt2inF(), trwe$!stvi~e&EM�tyl nllCc);
   0 `(0 `(            (fRqsk{
 $ "�0        ( $ gisE$MonitOr�~gAction.Ee|Be2~erState:
  $ 0"                  MmNa�ringServiceReference.ServerInfo(qE~w�rS6ate = alAd��Pr-xy.GetServerY�fcjXgxNumber);J   !��          " (!   wr�tglr�l|+akuion, Result.Success, IEX&UVmdhui�s.Tools.Serialize�.D#taCoNt�qcURe�icla{U(�}rverState),$t`wm- strhn�.Empty, f�ls', string>E-xty, false)�
b         "$()@�0`(    breac;-      $ 0"      =
,0`h( 0�p(    (  +/upf`|� Th�0exit Co�u to$in�)k#te serfe2(loaded buT �yth errors
 ! �     $ 0"0 `av"h!sesult)
    �  b        {
              (  $  exitCOd�0= 0;�
                            }
          ! �atch .EvgpointNotFoundException) 0"  p `	   {
    �0          //cou|d./t commun�Ca�u with(tXa`eonitobJ(�` b      ! � exitGotg = 2;
    $`02�`(b    wRi�urm�Gh|)actkof% vacwlt.Failure, string.Empty, false, sTr�~g.Empt9, Gadwe8�2IEP $+$`u�Jumber + :�S�ud,'t commu�mc3vu 7ath IEX MgnIpor on " + host + ". M`k�"sMr��it's r}�Nm,g", true!;� b        $ m
  " (!      catch �E8!ePt�on exs)M            {
    (  $        //uneppEctuf internal dr�or
        b (   ! �xitCcdU&= 3;
          `$ " writeResulu(�cpil �usuot�G�ylure- �triNg�UmxtQ� bclsD, string.Ao`~y,�false, exc.TkSdpinm() true);
 $ 0"       mJ(     0 =

        private statmk0@kod PvocessArgumun4{(stringY](`rgs, out MonitoringAct)onaatann,(oUp string host- �5t Hnt iexNu}b%z, owt(wo?f shutdonwTp�nErrors)%
�4  ,  &;
   �0    0 `aexNumber =  ;M            host = string.Um0|y;
            abt�of $Enoi�oringActinn�NoAction;
  �0! �     shutdofsu$mndrso�s = false;

 `       � /mco,leBt all errkr0o�ss#ee{
         " (isU,s4zing>!��r�bMe�sa%es(= jew List<string>�);O

           `if(args.Length � k�            {
               @e�CorMessages.Add "e|pecting at`le`s�03 xaRemeters but reaeawed   "!�rgs.Length);J        ! �    w�ytein�etEr�oz1qr2grMe3saFes);
         ! � `  �5tuSn f!lsD;
  ` 0`( �  ?
M
      � hk //actio�
b! �       0t2p�   ! �      {�  b   $ 0�  b�0 action = getAcTi�~FroiQdxhng(argsS }j\rim())3*$           �
b           catch
      ! �   {
                errmrEdssages.Ald&Qa|iOj: couldn�t &eteb-)fD the rgq}ds�edCaction: " + args[0] + "� m7st be"ofd md:)rpartIEX, Stop	EX RestartIEx,�WE`S%zvezSTete, GetServikES�date");
      `    }

            /.h�sd	H8*!     0 `(host = args[1];

     � 2bhi /iex numbe�
b     (` $ try
 " (! $ 0"  {
                iexNumber = Ij�.Rarse(argc[rU);
        $ 0"  $ yd (iexNumre"(� 1Bx| �exumber(> <)
`  0��(bb   $ 0y�
 b          " (!    errorImcQeges.Add("IexNumb$r�itz�numbeb -�ct be betuemo 1 and 8");
    00phh(      }
    0 `(    }
   `       catrh�            z�   " (!         ezr�vM'3saFes.Ad�(&Uz�embew:�vhe passed0i%p numbeR:�2 + asg�[2] + " wasn't recogLi�td as0a`degal inue�er. must be bevwm`n030i.L$8");
    ($ 4"   }


     0 @(�0!/�valId�de �pt+on�l 2arameteRs�`arameter�
b           foreach (rt�inf �rg in arfs�
$ 0"        {	0�a�8    `     //results path parameter
      0 `(      if (erw,T�Wp:lr-.Coft�mn1("/RESULTSPAVH2#)	�0            (  
    0"` a    �  �0 rmsuh�cP!t�=barg.Cu"{trilc  1);*	� "       0 `( "    &�f#%Directory.Mxiw�c sesultsPath))
  (  $  (  $        {
     " (!     $ 0�! � " msrgrmassages.Add("Resul4sP@u)��Uhe passed path is not a valid dirgc<�b�0!;
 (!            ! �   $ 0-/ch�fg$to de�au.t�lo!atmo~"for writing thd �rbo0{ Pk0vhe file
$(0%!�� �0    " (0�p( resuhtcRa�h  Path.�etirectoryName(Assem`lq/F��E:ecutanGESs�}"ly	)L�s!tiF.	?,
  ��  b   0 a(� `  ]�0  `         ( ]	
* �0      ! �! � if (`r�.ToUpper(+.Mpuals("/SHUTDOWNuP�^ErB�["))
       p `	     {
    `� b      `   shutdonwUp�nE0rors = trud;�
   !$�0"     $ m
    �0   0 `u

            //if re�ul6s path`waRngt Qassud`r)teto epe `mruatory
 $          hf�(�qsen�sP#th.Equcl{)stsi�gA�pty))
  (! �      {
        `      rds�ltsPath = Path.GetFizdctovy^cme(Assembly.GetEx�cu7i�gAsre�bly().Locati�n)y
     �  b   }
J   ! �     //errors
            if (grznrMecs!oes.Coun� >b0)
            z�  �  b     �0   writeInputErRo�c(errosM�sca'Ms�+-
�0     ! �      re|uRk �alse;
         ! �}
 $       $ 0peturn trud;�

    � vYI
$ 0"! � privaTe�Ct�dic void rIpeResult(Monito�hn!Aa6ann(aAp�nnn Result result, string retu2nV@lue, jgJH�p�4urOValueRequired, rtvin% state, bgoL$stqt%Zequired, string m�ss#gE,�rno� messawemquired)
       �k
" (!    ( @$TSace2.WSite(T�ac'r�Tr#ceLevel.@P�_ENTER, "enua�uf");
M
           SvraogBuilder$vuawLt�eilder = new St2In��u�>deb(i3)�` b      resultBuimd�r>Bp�efeLine("Actikn" ,* action.\ospring()(;�
        $0 resultJuIhdar>Sp0mndLine("Result: " + res�ltlToString());

            if (returnValueRequired)
        �  b{
  (  $      $ 0 rmrultBuilder.AppendLine�"R'turn: " + returnValue.\ospring());
   " (!     }

     �  j  $If�83ue�upe�eired)�
 B �2 (!    {-
�0          " � r�sultBu�ld'r.AppendLine("State:$"y)k(     " (!    � r's}lTFuilder&APtentL)fe(3taUe);
   �0 $ 0"  �-
�       �0   if (messageRequired)
          $ k
`* (!  �  r `(  res5ltcualLaR*AppendLine("ERr�b: " + message);�  j  $   ! �}M
+ " (!       //u|�Qr' t(e @ofrole `h � b�` �Hnbvesult == Result.Failure || (revuzoValueRequiret f. returnValUe�Uquals("fals�"�bQtzhngCompaziSkn/C�rbe.|C}lTqreIgnoreCase)))
 ! �        ;
  ! �    ! � 8 #cnsol�.W0iteLine "fEIL"!;- �( B$    0 =
  (  $      elsE�0        " (z
! �      0 `(   Console.WriteLine("PASS");
     �  j  $}

   (  $     qsylg (StreamWriter writer = new StreamWriter(Padh~C/Efine(�es7ltrP�th, "pu{4dP.�jt")))
         (  w-*�402"  `  �0   write2.WSitdL�nm)resultBuilder.ToStr{n/!)!�-b    " (!! ��O� 0 `(   �0  Ur�cer.Wrm�e�Fzacer.TraceLevel.API]EXIT, "exiting. result is: " + result�ui.der.ToString());
        }

 �  B �0private spadkc void writeInputErrors(List<string:$u`porMus3iges)
�  c �  {
        ! � Trasa�&Up�tuhTrakeR*Tr!cemmvAh~Ch_dLTMS, "ente�e``99

            StrineB}hlde2 rDsultbu�|der = feW$String@uamder();
J           res5l4cuHlder.AppmnDHine("Parameqe�q Errors"�;H
(  $0 `(   $fpeach (string error in errorMessaggs!
       " (! z�(  $  (  $      resultB}iH`up*C`zdndLioe�errgr	?
  ! �0 `,!0�}

   ! �    $ bgs}lTFualDar.qp�}ndLine(Usage)9�
   ` �`B ��?/updapm0Vle console
      ) �$  �on1mlm/riUeLine("FAIL");

    2 h)   $/?uziTa the resumt� to A`�Y��
  `` #�1   usijg0*StreamWritGr�fr)tgS( �uw St2eaLWriturhXath.Com�in'(resultwPqvh, "error/t�v"!()
            �-
�0              writurn_riteli�u
vmrultBuild%r.u/StSing(i(;�
0 `(        }
    0 `(    �ra!urn_2itD(Tracer.TraceLevel.API_EXIT- �e�ytkno/ rus5dt ks2!&�;"0esultBuilder.To[uR�ng());
      ! �

        //parse the passed string t� |+e action enum
$ 0"   !`�)~ape0qtatic Moni|oRm~g�kt+on getct�n�0oString(striog�ac�io,)
(  ,  4 9!           siPgx"(actinn�TgTxpEv())J        $ 0y
$ 0"       " (! case "QTISTIEX":	
0"                  return MonitoringAction.Star�IE;* �0 (  $        case "STOPIEX"*J($ 0"               redu2f Monitobi.oAktIkn.StopIEX;
  `           $Cq�u "BE\ABE	�P":
       �  b(! �  $  return MonitoringCc|lo~,RestartIEX;
           $ 0" c�ce "ETrGR^DRSTATE":
     `       `    return EoNmtoriog�cTlo�.Ee|RerverState;
            }
0 `(  � �b bthrog nmwExcepvigo("unknown action�);O
        }


      �0[ystee.rsn|hm5.	GteropServices.DllImport("user3�.d.l")]-
�0  " (!private s|aTmc extern Boolean!S�owWifdOs(IntPtr hWnd, Kn|22 oC�dShow);
    }

   0p5jlhc�static class Pr�ce1sExtensio�s�J j! {
        private static string FindI�laqfProcessName(int Pi�9
  0 `(  {�
 b $ 0& 0"  v!2 QSocdS��aoe = P�cess.GetProgecqById(pid).ProcessJe}u8�  �1       Va�0procec�%{ iN!ee = Procgs{?O%\TrocessesByName(process�am');
  "�<!0"    wtbkng processIndexdNcmm!= null�J       ! � fr` var index < �; iod�x < prnc�ssesByName.Length; knllp- `(         {
    0 `(! �  �  62ocDssIndexdName"=(�nl'X$== 0`? Qrocgs{Oame z pSoce{rn�md /("A& + index;
 `    $ 0"     va� p0�ce9si` = new$@u8nOtmioce�oU��uh"PSkkuQw", "ID Procers�, processInded�Pme);
          �  b 0i&(((anT-probe�sId.NexTV�|ue() }= Qid)
           `   {
   0 `(    $ 0" " ( b�&}:e xsocessIndexdNa-e;,
          p `	  }
0 `(        }

@,     ! � `ruU5zn processMntgxdName;
        }

        privatm!S�atic PrncassbFind�idso�I.deYedProcessName(string indexedPrkc�qsLamD)
   `    �
�  b  " (!  var pqr%ftId 5 N�7 Drfk�}c.cmBou.te[(Trocasc , "Creating Pso�ess ID", indexeeP�ocessName);
    �  b    return PRo�uss.Get@r/kes3Byhd((int)p!�AO�[d.Nex|VH5�:	#�    ! � }

        publac wtatic Process Parent(th�s rwb%{s proceq3!#  $     {
 �  b    �0 return F)nd�Hd `oeHnluX!hPbi�uq1Name(FindIndexedXrOgews_c�e(process.Id));
        =
  �}H=
