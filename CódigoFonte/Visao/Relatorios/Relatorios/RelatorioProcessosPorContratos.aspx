<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioProcessosPorContratos.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioProcessosPorContratos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Processos por contratos
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
    <table width="100%">
        <tr>
            <td width="30%" align="right">
                Tipo de Processo:
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoProcessoProcessosPorContrato" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoProcessoProcessosPorContrato_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">Grupo Econômico:</td>
            <td>
                <asp:DropDownList ID="ddlGrupoEconomicoContratoPorProcesso" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoEconomicoContratoPorProcesso_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">Empresa:
            </td>
            <td>
                <asp:UpdatePanel ID="upEmpresas" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEmpresaContratoPorProcesso" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoContratoPorProcesso" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoProcessoProcessosPorContrato" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Número do Contrato:
            </td>
            <td>
                <asp:TextBox ID="tbxNumeroContratoPorProcesso" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Objeto do Contrato:
            </td>
            <td>
                <asp:TextBox ID="tbxObjetoContratoPorProcesso" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Status do Contrato:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatusContratoProcessoPorContrato" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="30%" align="right">&nbsp;</td>
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
            <asp:BoundField DataField="Contrato" HeaderText="Contrato" />
            <asp:BoundField DataField="NumeroProcesso" HeaderText="Número do Processo" />
            <asp:BoundField DataField="TipoProcesso" HeaderText="Tipo do Processo" />            
            <asp:BoundField DataField="Empresa" HeaderText="Empresa" />            
            <asp:BoundField DataField="Abertura" HeaderText="Data de Abertura" />  
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

