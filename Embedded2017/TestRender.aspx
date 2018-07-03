<%@ Register Assembly="Microsoft.ReportViewer.WebForms"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestRender.aspx.cs" Inherits="Embedded2017.TestRender" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>TestRender</title>

    <style>

        html, body, form 
        {
            width: 100%; height:100%;
            margin: 0px; padding: 0px;
        }

    </style>

</head>
<body>
    
    <form id="form1" runat="server" >
        <asp:ScriptManager enablepagemethods="true" runat="server"></asp:ScriptManager>              
        <rsweb:ReportViewer ID="rptViewer" runat="server" CssClass="rpviewerparm" BackColor="#60759B" Width="100%" Height="100%" PageCountMode="Actual" />
    </form>

    <!--
    The Report Viewer Web Control HTTP Handler has not been registered in the application's web.config file. 
    Add <add verb="*" path="Reserved.ReportViewerWebControl.axd" 
    type = "Microsoft.Reporting.WebForms.HttpHandler, Microsoft.ReportViewer.WebForms, Version=13.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" /> 
    to the system.web/httpHandlers section of the web.config file, or add 
    <add name="ReportViewerWebControlHandler" preCondition="integratedMode" verb="*" path="Reserved.ReportViewerWebControl.axd" type="Microsoft.Reporting.WebForms.HttpHandler, Microsoft.ReportViewer.WebForms, Version=13.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" /> 
    to the system.webServer/handlers section for Internet Information Services 7 or later.
    
    The Report Viewer Web Control HTTP Handler has not been registered in the application's web.config file. 
    Add <add verb="*" path="Reserved.ReportViewerWebControl.axd" 
    type = "Microsoft.Reporting.WebForms.HttpHandler, Microsoft.ReportViewer.WebForms" /> 
    to the system.web/httpHandlers section of the web.config file, or add 
    <add name="ReportViewerWebControlHandler" preCondition="integratedMode" verb="*" path="Reserved.ReportViewerWebControl.axd" type="Microsoft.Reporting.WebForms.HttpHandler, Microsoft.ReportViewer.WebForms" /> 
    to the system.webServer/handlers section for Internet Information Services 7 or later.
    -->

</body>
</html>
