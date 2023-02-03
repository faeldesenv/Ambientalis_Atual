<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioLicencasAmbientais.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioLicencasAmbientais" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataLimiteLicencaAmbiental.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataLimiteAtehLicencaAmbiental.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataValidadeAtehLicencaAmbiental.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataValidadeDeLicencaAmbiental.ClientID %>"));
        });

    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Licenças Ambientais
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
                <asp:DropDownList ID="ddlGrupoEconomicoLicencaAmbiental" runat="server" AutoPostBack="True" CssClass="DropDownList" OnSelectedIndexChanged="ddlGrupoEconomicoLicencaAmbiental_SelectedIndexChanged" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Empresa:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="upEmpresasLicencasAmbientais" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEmpresaLicencaAmbiental" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoLicencaAmbiental" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                Tipo de Licença:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlTipoLicencaAmbiental" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Data de Validade:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxDataValidadeDeLicencaAmbiental" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                &nbsp;até
                <asp:TextBox ID="tbxDataValidadeAtehLicencaAmbiental" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Prazo Limite de Renovação:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxDataLimiteLicencaAmbiental" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                &nbsp;até
                <asp:TextBox ID="tbxDataLimiteAtehLicencaAmbiental" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Órgão Ambiental:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlOrgaoAmbientalLicencaAmbiental" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Estado:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlEstadoLicencaAmbiental" runat="server" CssClass="DropDownList" Width="50%">
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
            <asp:BoundField DataField="GetGrupoEconomico" HeaderText="Grupo Econômico" />
            <asp:BoundField DataField="GetEmpresa" HeaderText="Empresa" />
            <asp:BoundField DataField="GetDescricaoLicenca" HeaderText="Licença" />            
            <asp:BoundField DataField="DataRetirada" HeaderText="Retirada" DataFormatString="{0:d}" />
            <asp:BoundField DataField="DiasValidade" HeaderText="Dias de Validade" />
            <asp:BoundField DataField="GetValidade" HeaderText="Validade" />
            <asp:BoundField DataField="PrazoLimiteRenovacao" HeaderText="Renovação" DataFormatString="{0:d}" />
            <asp:BoundField DataField="GetNumeroProcesso" HeaderText="Processo Nº" />
            <asp:BoundField DataField="GetOrgaoAmbiental" HeaderText="Órgão Ambiental" />
            <asp:BoundField DataField="GetEstado" HeaderText="Estado" />
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

