<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Planinfo_Bla._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Report Rendering</title>
    <style type="text/css">

        html, body, form
        {
            width: 100%;
            height: 100%;
            margin: 0px;
            padding: 0px;
        }

        body
        {
            background-color: #585858;
            color: white;
            font-family: Arial;
            font-size: 11px;
        }

        #oReportCell 
        { 
            width: 100%; 
            padding: 2mm;
        }

        td
        {
            vertical-align: top;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="litContent" runat="server" />
    </form>
</body>
</html>
