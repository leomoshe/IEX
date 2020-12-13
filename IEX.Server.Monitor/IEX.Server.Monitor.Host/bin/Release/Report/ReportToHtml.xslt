<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:output method="html" indent="yes"/>
  <!--<xsl:strip-space elements="*"/>-->

  <xsl:template match="/" >
    <html>
      <head>
        <xsl:comment>saved from url=(0014)about:internet </xsl:comment>
        <title>
          TestPlanReport
        </title>
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.1.min.js">
          //
          //
        </script>
        <script type="text/javascript">

          $( document ).ready(function() {
          $('td.Exception1').children('ul').children('li').children('a').click(function() {

          $(this).parent().find('ul').toggle();
          });
          });



        </script>
        
        
        <style type="text/css">
          a {
          color: red;
          }
          ul li ul {
          display: none;
          }

          table.Stats
          {
          width: 15%;
          font-weight: bold;
          }

          table.Test
          {
          border: 5px;
          border-color: Black;
          border-style: solid;
          margin: 0;
          padding: 0;
          border-collapse: collapse;
          }

          tr.IsSummary
          {
          background-color: Gray;
          }

          th
          {
          font-weight: bold;
          border: 1px;
          border-color: Black;
          border-style: solid;
          margin: 0;
          padding: 2px;
          }

          td.Test
          {
          border: 1px;
          border-color: Black;
          border-style: solid;
          margin: 0;
          padding: 2px;
          }

          td.Exception1
          {
          border: 1px;
          border-color: Black;
          border-style: solid;
          margin: 0;
          padding: 2px;
          }

          td.Pass
          {
          border: 1px;
          border-color: Black;
          border-style: solid;
          background-color: Lime;
          margin: 0;
          padding: 2px;
          }

          td.Fail
          {
          border: 1px;
          border-color: Black;
          border-style: solid;
          background-color: Red;
          font-weight: bold;
          margin: 0;
          padding: 2px;
          }

          td.IexFail
          {
          border: 1px;
          border-color: Black;
          border-style: solid;
          background-color: Orange;
          font-weight: bold;
          margin: 0;
          padding: 2px;
          }

          td.NotRun
          {
          border: 1px;
          border-color: Black;
          border-style: solid;
          background-color: Orange;
          font-weight: bold;
          margin: 0;
          padding: 2px;
          }
        </style>
      </head>
      <body>
        <table>
          <tr>
            <td>Test Plan:</td>
            <td>
              <xsl:value-of select="Root/TestPlanReport/@ID"/>
            </td>
          </tr>
          <tr>
            <td>Platform:</td>
            <td>
              <xsl:value-of select="Root/TestPlanReport/@Platform"/>
            </td>
          </tr>
          <tr>
            <td>Start Time:</td>
            <td>
              <xsl:value-of select="Root/TestPlanReport/@Start"/>
            </td>
          </tr>
          <tr>
            <td>End Time:</td>
            <td>
              <xsl:value-of select="Root/TestPlanReport/@End"/>
            </td>
          </tr>
        </table>
        <br></br>
        <table class="Stats">
          <tr style="background-color:gray">
            <td>Total:</td>
            <td>
              <xsl:value-of select="Root/TestPlanReport/@Total"/>
            </td>
          </tr>
          <tr style="background-color:lime">
            <td>Passed:</td>
            <td>
              <xsl:value-of select="Root/TestPlanReport/@Passed"/>
            </td>
          </tr>
          <tr style="background-color:red">
            <td>Fail:</td>
            <td>
              <xsl:value-of select="Root/TestPlanReport/@Fail"/>
            </td>
          </tr>
          <tr style="background-color:orange">
            <td>IexFail:</td>
            <td>
              <xsl:value-of select="Root/TestPlanReport/@IexFail"/>
            </td>
          </tr>
          <tr>
            <td>NotRun:</td>
            <td>
              <xsl:value-of select="Root/TestPlanReport/@NotRun"/>
            </td>
          </tr>
        </table>

        <xsl:for-each select="Root/TestPlanReport/Report">
          <p>
            <table class="Test">
              <tr>
                <th>Name</th>
                <th>State</th>
                <th>Result</th>
                <th>Exception</th>
                <th>Message</th>
                <th>StateStartTime</th>
                <th>StateFinishedTime</th>
              </tr>
              <xsl:for-each select="ReportItem">
                <tr>
                  <xsl:attribute name="class">
                    <xsl:if test="@IsSummary = 1">
                      IsSummary
                    </xsl:if>
                  </xsl:attribute>
                  <td class="Test">
                    <xsl:variable name="cdtitle">
                      <xsl:value-of select="Name"/>
                    </xsl:variable>
                    <xsl:choose>
                      <xsl:when test="(contains($cdtitle, 'Step'))">
                        <b>
                          <xsl:value-of select="substring-before($cdtitle,'-')"/>
                        </b>
                        <xsl:value-of select="substring-after($cdtitle,'-')"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="$cdtitle" />
                      </xsl:otherwise>
                    </xsl:choose>
                    
                    
                  </td>
                  <td class="Test">
                    <xsl:value-of select="State" />
                  </td>
                  <td>
                    <xsl:attribute name="class">
                      <xsl:value-of select="Result" />
                    </xsl:attribute>
                    <xsl:value-of select="Result" />
                  </td>
                  <td class="Exception1">
                    <ul class="list">
                      <xsl:for-each select="Exception/*" >
                        <li>
                          <a>
                            <xsl:value-of select="Message" />
                          </a>
                          <br></br>
                          <ul>
                            <xsl:value-of select="Trace" />
                            <br></br>
                          </ul>
                        </li>
                      </xsl:for-each>
                    </ul>

                  </td>
                  <td class="Test">
                    <xsl:value-of select="LastEngineMessage" />
                  </td>
                  <td class="Test">
                    <xsl:value-of select="StateStartTime" />
                  </td>
                  <td class="Test">
                    <xsl:value-of select="StateFinishedTime" />
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </p>
        </xsl:for-each>
      </body>

    </html>
  </xsl:template>

</xsl:stylesheet>