<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorioComercial.master" AutoEventWireup="true" CodeFile="RelatorioVendas.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioVendas" %>

<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxVendaDataCadastroDe.ClientID %>"));
            adicionarDatePicker($("#<%= tbxVendaDataCadastroAte.ClientID %>"));
        });
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    vendas
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
            <td width="20%" align="right">
                Revenda:
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlRevendaVenda" runat="server" CssClass="DropDownList" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Data de Cadastro de:
            </td>
            <td>
                <asp:TextBox ID="tbxVendaDataCadastroDe" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                &nbsp;até
                <asp:TextBox ID="tbxVendaDataCadastroAte" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Status:
            </td>
            <td>
                <asp:DropDownList ID="ddlVendastatus" runat="server" CssClass="DropDownList" Width="50%">
                    <asp:ListItem Selected="True" Value="0">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="1">ATIVOS</asp:ListItem>
                    <asp:ListItem Value="2">INATIVOS</asp:ListItem>
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
            <asp:BoundField DataField="GetRevenda" HeaderText="Revenda" />
            <asp:BoundField DataField="GetProspecto" HeaderText="Nome do Prospecto" /> 
            <asp:BoundField DataField="GetCNPJeCPFProspecto" HeaderText="CNPJ/CPF do Prospecto" /> 
            <asp:BoundField DataField="GetDataCadastroProspecto" HeaderText="Data de Cadastro" /> 
            <asp:BoundField DataField="Data" HeaderText="Data da Venda" DataFormatString="{0:d}" />             
            <asp:BoundField DataField="GetValorMensalidade" HeaderText="Mensalidade" />             
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

