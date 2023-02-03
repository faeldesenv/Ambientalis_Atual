<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioOutrosVencimentosAmbientais.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioOutrosVencimentosAmbientais" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataVencimentoDeOutrosVencimentos.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataVencimentoAtehOutrosVencimentos.ClientID %>"));            
        });

    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Outros Vencimentos Ambientais
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
            <td align="right" style="width: 30%">
                Grupo Econômico:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlGrupoEconomicoOutrosVencimentos" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" onselectedindexchanged="ddlGrupoEconomicoOutrosVencimentos_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Empresa:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="upEmpresasOutrosVencimentos" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEmpresaOutrosVencimentos" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoOutrosVencimentos" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                Data de Vencimento:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxDataVencimentoDeOutrosVencimentos" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                &nbsp;até
                <asp:TextBox ID="tbxDataVencimentoAtehOutrosVencimentos" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Tipo:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlTipoOutrosVencimentos" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;Vencimentos Periódicos:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlOutrosPeriodicos" runat="server" CssClass="DropDownList" Width="30%">
                    <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="1">Sim</asp:ListItem>
                    <asp:ListItem Value="2">Não</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Vencimentos com Prorrogação de Prazo:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlOutrosProrrogacaoPrazo" runat="server" CssClass="DropDownList" Width="30%">
                    <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="1">Sim</asp:ListItem>
                    <asp:ListItem Value="2">Não</asp:ListItem>
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
            <asp:BoundField DataField="GetNomeGrupoEconomico" HeaderText="Grupo Econômico" />
            <asp:BoundField DataField="GetNomeEmpresa" HeaderText="Empresa" />
            <asp:BoundField DataField="GetTipo" HeaderText="Tipo" />            
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
            <asp:BoundField DataField="GetDataUltimoVencimento" HeaderText="Vencimento"/>
            <asp:BoundField DataField="GetDescPeriodico" HeaderText="Períodico" />
            <asp:BoundField DataField="GetQtdProrrogacoes" HeaderText="Prorrogações" DataFormatString="{0:d}" />
            <asp:BoundField DataField="GetNumeroProcesso" HeaderText="Nº Processo" />
            <asp:BoundField DataField="GetNomeOrgaoAmbiental" HeaderText="Órgão Ambiental" />            
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

