<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Relatorios.aspx.cs" Inherits="Relatorios_Relatorios" %>

<%@ Register Assembly="CafalseTools" Namespace="CafalseTools" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="btnVoltar" runat="server" Text="VOLTAR AO SISTEMA"  
        CssClass="Button" onclick="btnVoltar_Click"/>
    <div id="relatorio" style="width: 800px">
        
    
        <rsweb:ReportViewer ID="rpvRelatoriosComercial" runat="server" Width="100%" 
            Height="29cm" DocumentMapWidth="" 
            onreporterror="rpvRelatoriosComercial_ReportError">
        </rsweb:ReportViewer>
        
    
    </div>
    </form>
</body>
</html>
