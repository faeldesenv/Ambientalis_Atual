<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioVencimentosDiversos.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioVencimentosDiversos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataDeVencimentoDiverso.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataAteVencimentoDiverso.ClientID %>"));
        });
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    vencimentos diversos
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
            <td align="right" width="30%">
                Grupo Econômico:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlGrupoEconomicoVencimentoDiverso" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" onselectedindexchanged="ddlGrupoEconomicoVencimentoDiverso_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Empresa:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEmpresaVencimentoDiverso" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicoVencimentoDiverso" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Descrição:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxDescricaoVencimentoDiverso" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Tipo de vencimento:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlTipoVencimentoDiverso" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" onselectedindexchanged="ddlTipoVencimentoDiverso_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Status do vencimento:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlStatusVencimentoDiverso" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoVencimentoDiverso" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td align="right" width="30%">
                Data de vencimento de:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxDataDeVencimentoDiverso" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                &nbsp;até&nbsp;
                <asp:TextBox ID="tbxDataAteVencimentoDiverso" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;Vencimentos Periódicos:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlPeriodicosVencimentosDiverso" runat="server" CssClass="DropDownList" Width="30%">
                    <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
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
            <asp:BoundField DataField="GetDataDateVencimento" DataFormatString="{0:d}" HeaderText="Vencimento" />    
            <asp:BoundField DataField="GetGrupoEconomico" HeaderText="Grupo Econômico" />
            <asp:BoundField DataField="GetNomeEmpresa" HeaderText="Empresa" />
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
            <asp:BoundField DataField="GetTipoDiverso" HeaderText="Tipo" />
            <asp:BoundField DataField="GetStatusAtualVencimento" HeaderText="Status" />                     
            <asp:BoundField DataField="GetPeriodico" HeaderText="Períodico" />                    
                             
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

