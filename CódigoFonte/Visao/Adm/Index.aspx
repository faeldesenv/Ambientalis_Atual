<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Site_IndexDNPM" %>

<%--<tirarisso></tirarisso>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%--<tirarisso></tirarisso>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div style="width:auto; height:auto; margin:120px; text-align:center;">
        <img alt="." src="../imagens/logo_index.png"/>
    </div>    
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="Barra">
    <p>administrativo</p>
</asp:Content>



