<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorioComercial.master" AutoEventWireup="true" CodeFile="RelatorioProspectos.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioProspectos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataCadastroProspectosDe.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataCadastroProspectosAte.ClientID %>"));
        });
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Prospectos
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
            <td Width="20%" align="right">
                Revenda:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlRevendaProspectos" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Estado:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlEstadoProspectos" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" onselectedindexchanged="ddlEstadoProspectos_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Cidade:
            </td>
            <td align="left">
                <asp:UpdatePanel ID="UPCidadeProspectos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlCidadeProspectos" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEstadoProspectos" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                Data de Cadastro de:
            </td>
            <td>
                <asp:TextBox ID="tbxDataCadastroProspectosDe" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                &nbsp;até
                <asp:TextBox ID="tbxDataCadastroProspectosAte" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Status:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList" Width="50%">
                    <asp:ListItem Selected="True" Value="0">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="1">ATIVOS</asp:ListItem>
                    <asp:ListItem Value="2">INATIVOS</asp:ListItem>
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
            <asp:BoundField DataField="GetRevenda" HeaderText="Revenda" /> 
            <asp:BoundField DataField="Nome" HeaderText="Nome/Razão Social" />
            <asp:BoundField DataField="GetNumeroCNPJeCPFComMascara" HeaderText="CNPJ/CPF" /> 
            <asp:BoundField DataField="Responsavel" HeaderText="Responsável" /> 
            <asp:BoundField DataField="GetStatus" HeaderText="Status" /> 
            <asp:BoundField DataField="DataCadastro" HeaderText="Data de Cadastro" DataFormatString="{0:d}" /> 
            <asp:BoundField DataField="GetCidade" HeaderText="Cidade" /> 
            <asp:BoundField DataField="GetEstado" HeaderText="Estado" /> 
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

