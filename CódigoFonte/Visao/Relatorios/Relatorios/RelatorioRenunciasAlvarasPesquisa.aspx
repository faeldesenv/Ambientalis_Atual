<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioRenunciasAlvarasPesquisa.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioRenunciasAlvarasPesquisa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataVencimentoRenunciaDe.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataVencimentoRenunciaAte.ClientID %>"));
        });
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Renúncias de Alvarás de Pesquisa
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" Runat="Server">
    <div style="padding-top: 10px; padding-bottom: 15px;">
        <div style="width: 150px; text-align: left; font-family: 'Open Sans'; color: black; font-size: 13px;">
            Exibir as Colunas:
        </div>
        <div style="border: 1px solid white; padding: 10px;">
            <div>
                <asp:CheckBoxList ID="ckbColunas" runat="server" CssClass="chekListRelatorios" RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="0" CellSpacing="0" Font-Names="Open Sans" Font-Size="13px" ForeColor="Black">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <table style="width: 100%; margin-right: 0px;">
        <tr>
            <td width="30%" align="right">
                Grupo Econômico:
            </td>
            <td>
                <asp:DropDownList ID="ddlGrupoRenuncias" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" onselectedindexchanged="ddlGrupoRenuncias_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Empresa:
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEmpresaRenuncias" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoRenuncias" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td align="right">
                Vencimento de:
            </td>
            <td>
                <asp:TextBox ID="tbxDataVencimentoRenunciaDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                &nbsp;até&nbsp;
                <asp:TextBox ID="tbxDataVencimentoRenunciaAte" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Estado:
            </td>
            <td>
                <asp:DropDownList ID="ddlEstadoRenuncias" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">&nbsp;</td>
            <td align="center">
                <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="Button" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" Width="150px" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField DataField="GetGrupoEconomico" HeaderText="Grupo Econômico" />
            <asp:BoundField DataField="GetNomeEmpresa" HeaderText="Empresa" />
            <asp:BoundField DataField="GetNumeroProcessoDNPM" HeaderText="Nº do Processo DNPM" />
            <asp:BoundField DataField="Numero" HeaderText="Nº do Alvará" />
            <asp:BoundField DataField="GetDataVencimentoRenunciaAlvara" HeaderText="Vencimento" />                     
            <asp:BoundField DataField="GetEstadoRenuncia" HeaderText="Estado" />                     
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

