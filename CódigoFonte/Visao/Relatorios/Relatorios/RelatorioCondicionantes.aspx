<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioCondicionantes.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioCondicionantes" %>

<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataVencimentoDeCondicionantes.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataVencimentoAtehCondicionantes.ClientID %>"));            
        });        
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Condicionantes
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
            <td align="right" style="width: 30%">
                Grupo Econômico:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlGrupoEconomicoCondicionantes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoEconomicoCondicionantes_SelectedIndexChanged" 
                    Width="50%" CssClass="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Empresa:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="upEmpresasCondicionantes" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEmpresaCondicionantes" runat="server" Width="50%" CssClass="DropDownList">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoCondicionantes" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                Data de vencimento:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxDataVencimentoDeCondicionantes" runat="server" Width="22%" CssClass="TextBox">
                </asp:TextBox>
                &nbsp;até
                <asp:TextBox ID="tbxDataVencimentoAtehCondicionantes" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Órgão Ambiental:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlOrgaosAmbientaisCondicionantes" runat="server" Width="50%" CssClass="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Status:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlStatusCondicionante" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Estado:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlEstadoCondicionante" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;Condicionantes Periódicas:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlCondicionantePeriodica" runat="server" CssClass="DropDownList" Width="30%">
                    <asp:ListItem Value="0" Selected="True">-- Todas --</asp:ListItem>
                    <asp:ListItem Value="1">Sim</asp:ListItem>
                    <asp:ListItem Value="2">Não</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Condicionantes com Prorrogação de Prazo:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlCondicionanteProrrogacaoPrazo" runat="server" CssClass="DropDownList" Width="30%">
                    <asp:ListItem Value="0" Selected="True">-- Todas --</asp:ListItem>
                    <asp:ListItem Value="1">Sim</asp:ListItem>
                    <asp:ListItem Value="2">Não</asp:ListItem>
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
            <asp:BoundField DataField="GetEmpresa" HeaderText="Empresa" />
            <asp:BoundField DataField="GetDescricaoCondicionante" HeaderText="Condicionante" />
            <asp:BoundField DataField="GetDescricaoLicenca" HeaderText="Licença" />
            <asp:BoundField DataField="GetStatus" HeaderText="Status" />
            <asp:BoundField DataField="GetDataVencimento" HeaderText="Vencimento" />
            <asp:BoundField DataField="GetPeriodica" HeaderText="Períodica" />
            <asp:BoundField DataField="GetQtdProrrogacoes" HeaderText="Prorrogações" />
            <asp:BoundField DataField="GetNumeroProcesso" HeaderText="Nº Processo" />
            <asp:BoundField DataField="GetOrgaoAmbiental" HeaderText="Órgão Ambiental" />
            <asp:BoundField DataField="GetEstadoCondicionante" HeaderText="Estado" />            
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

