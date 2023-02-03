<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ADMRelatorios.aspx.cs" Inherits="ADMRelatorios_Relatorios" %>


<%@ Register Assembly="CafalseTools" Namespace="CafalseTools" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="Style.css" rel="stylesheet" type="text/css" />
<head runat="server">
    <title>Relatório</title>
</head>
<body>
    <form id="Form" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="Button1" runat="server" CssClass="Button" 
        Text="VOLTAR AO SISTEMA" 
        PostBackUrl="~/Adm/ADMFiltrosRelatorios.aspx" />
    <div id="relatorio" style="width: 800px">
        
        <rsweb:ReportViewer ID="rpvRelatorios" runat="server" Width="100%">
        </rsweb:ReportViewer>
        
    </div>
    </form>
</body>
</html>