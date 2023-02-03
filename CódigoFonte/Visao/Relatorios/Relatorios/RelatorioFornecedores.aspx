<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioFornecedores.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioFornecedores" %>

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
    Fornecedores
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
                Nome/Razão Social:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxNomeRazaoFornecedor" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                CNPJ/CPF:
            </td>
            <td align="left">
                <asp:TextBox ID="tbxCnpjCpfFornecedor" runat="server" CssClass="TextBox" Width="50%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Atividade:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlAtividadeFornecedor" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Status:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlStatusFornecedor" runat="server" CssClass="DropDownList" Width="50%">
                    <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="1">ATIVOS</asp:ListItem>
                    <asp:ListItem Value="2">INATIVOS</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Estado:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlEstadoFornecedor" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" onselectedindexchanged="ddlEstadoFornecedor_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Cidades:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="UPCidadeFornecedor" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlCidadeFornecedor" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEstadoFornecedor" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
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
            <asp:BoundField DataField="GetNomeRazaoSocial" HeaderText="Nome/Razão Social" />
            <asp:BoundField DataField="GetNumeroCNPJeCPFComMascara" HeaderText="CPF/CNPJ" />
            <asp:BoundField DataField="GetDescricaoStatus" HeaderText="Status" />
            <asp:BoundField DataField="GetDescricaoAtividade" HeaderText="Atividade" />
            <asp:BoundField DataField="GetCidade" HeaderText="Cidade" />                     
            <asp:BoundField DataField="GetSiglaEstado" HeaderText="Estado" />                     
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

