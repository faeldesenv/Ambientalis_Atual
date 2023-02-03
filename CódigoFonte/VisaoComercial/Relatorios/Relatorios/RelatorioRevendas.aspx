<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorioComercial.master" AutoEventWireup="true" CodeFile="RelatorioRevendas.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioRevendas" %>

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
    revendas
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
                Tipo de Parceria:
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoParceiro" runat="server" CssClass="DropDownList" Width="50%">
                    <asp:ListItem Selected="True">-- Todos --</asp:ListItem>
                    <asp:ListItem>Revenda / Agente de Negócios</asp:ListItem>
                    <asp:ListItem>Consultoria Ambiental / Minerária</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" Width="20%">
                Estado:
            </td>
            <td>
                <asp:DropDownList ID="ddlEstadoRevendas" runat="server" AutoPostBack="true" CssClass="DropDownList" onselectedindexchanged="ddlEstadoRevendas_SelectedIndexChanged" Width="50%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Cidade:
            </td>
            <td>
                <asp:UpdatePanel ID="UPCidadeRevendas" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlCidadesRevendas" runat="server" CssClass="DropDownList" Width="50%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEstadoRevendas" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">
                Status:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatusRevendas" runat="server" CssClass="DropDownList" Width="50%">
                    <asp:ListItem Selected="True" Value="0">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="1">ATIVAS</asp:ListItem>
                    <asp:ListItem Value="2">INATIVAS</asp:ListItem>
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
            <asp:BoundField DataField="Nome" HeaderText="Nome/Razão Social" />
            <asp:BoundField DataField="GetTipoParceria" HeaderText="Tipo de Parceria" /> 
            <asp:BoundField DataField="GetNumeroCNPJeCPFComMascara" HeaderText="CNPJ/CPF" /> 
            <asp:BoundField DataField="Responsavel" HeaderText="Responsável" /> 
            <asp:BoundField DataField="GetStatus" HeaderText="Status" />             
            <asp:BoundField DataField="GetCidade" HeaderText="Cidade" /> 
            <asp:BoundField DataField="GetEstado" HeaderText="Estado" /> 
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

