<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="Site_Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   <script type="text/javascript">
       $(function () {
           addDrag();
       });

       function addDrag() {
           $("#<%= divPopAviso.ClientID %>").draggable({ containment: "#content", cancel: "#divPopAvisoCorpo" });
       }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <div style="text-align:right; ">
        <div style="width: auto; height: auto; margin: 120px; text-align: center;">
            <img alt="." src="../imagens/logo_index.png" />
        </div>
        <div id="divPopAviso" runat="server" style=" background-color: #d4dfdb; position:absolute; right:10px; *margin-top:10px; top:10px; width: 400px; z-index: 1000; padding:5px; border-radius:5px;behavior: url(../Utilitarios/htc/PIE.htc);">
            <div style="float: right;" id="divPopAvisoFechar" onclick='javascript:$("#<%= divPopAviso.ClientID %>").hide();'>
                <img alt="." src="../imagens/x.png" style="height: 20px; cursor: pointer" title="Fechar" />
            </div>
            <div style="float:left;">
                <img alt="AVISO" src="../imagens/icone_aviso.png" />
            </div>
            <div style="float: left; margin-left:5px; margin-top:7px; font-size: small;">
                <b>SUSTENTAR AVISA</b><br />
            </div>
            <div style="border-top: 2px solid #687a60; width: 100%; float: left;"></div>

                <div id="divPopAvisoCorpo" style="float: left;
                background-color: #eff8f5; behavior: url(../Utilitarios/htc/PIE.htc); border-radius:4px; padding:4px; text-align:justify;margin-top:5px;">
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Label ID="lblAviso" runat="server"></asp:Label>
                </asp:Panel>
            </div>            
        </div>
        <asp:Label ID="lblAuxSenha" runat="server"></asp:Label>
        <asp:ModalPopupExtender ID="lblAuxSenha_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
            DynamicServicePath="" Enabled="True" PopupControlID="popUpTrocarSenha" TargetControlID="lblAuxSenha"
            CancelControlID="btnTrocarDepois">
        </asp:ModalPopupExtender>
        </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Barra">
    <p>
        SUSTENTAR - COMERCIAL</p>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="popups">
    <uc1:MBOX ID="MBOX1" runat="server" />
</asp:Content>
