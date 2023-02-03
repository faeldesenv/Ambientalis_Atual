<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="AtualizacaoDeProcessosDNPM.aspx.cs" Inherits="DNPM_AtualizacaoDeProcessosDNPM" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
    atualização de processos do site a ANM
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="LinkButton1_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" DynamicServicePath="" Enabled="True" 
        PopupControlID="divPopUpAtualizacao" TargetControlID="Label1"></asp:ModalPopupExtender>
    <div id="conteudo_sistema">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td class="labelFiltros" width="30%">
                        Filtro por
                        Grupo Econômico:
                    </td>
                    <td class="style1" width="40%">
                        <asp:DropDownList ID="ddlGrupoEconomico" runat="server" CssClass="DropDownList" Height="25px"
                            Width="100%" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="True"
                            DataTextField="Nome" DataValueField="Id">
                        </asp:DropDownList>
                    </td>
                    <td class="style1"></td>
                    
                </tr>
                <tr>
                    <td class="labelFiltros">
                        Filtro por
                        Empresa:
                    </td>
                    <td class="style1">
                        <asp:UpdatePanel ID="upEmpresa" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="DropDownList"
                                    DataTextField="Nome" DataValueField="Id" Height="25px"
                                    Width="100%">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomico" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    
                </tr>
                <tr>
                    <td class="labelFiltros">
                        &nbsp;
                    </td>
                    <td style="text-align:right;">
                        <asp:Button ID="btnBaixarAtualizacoes" runat="server" CssClass="Button"  OnClick="btnBaixarAtualizacoes_Click" Text="Atualizar Processos do Site da ANM" OnInit="btnBaixarAtualizacoes_Init" />                           
                    </td>
                    
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
    <div id="divPopUpAtualizacao" style="width: 596px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div>
            <div class="barra_titulo">
                Processando...</div>
        </div>
        <div>
            <asp:UpdatePanel ID="upAtualizar" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                <ContentTemplate>
                    <asp:Label ID="lblProcesso" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    <br />
                    <asp:Label ID="lblAguarde" runat="server" Text="Aguarde, o processo pode levar alguns minutos."></asp:Label><br />
                    <div style="text-align:right;">
                    <asp:Button ID="btnParar" CssClass="Button" runat="server" Text="Parar" OnClick="btnParar_Click" OnInit="btnParar_Init"
                        Height="26px" /></div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
    </asp:Timer>
</asp:Content>

