﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true"
    CodeFile="FiltrosRelatoriosRevendas.aspx.cs" Inherits="Relatorios_FiltrosRelatoriosRevendas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Escolha de relatório</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="campos_escolha_relatorio" class="container" style="height: 395px;">
        <div class="div_container" style="width: 30%">
            <b>Escolha o Relatório:</b>
            <asp:TreeView ID="trvRelatoriosComcerciais" runat="server" Width="100%" 
                OnSelectedNodeChanged="trvRelatorios_SelectedNodeChanged" 
                ImageSet="XPFileExplorer" NodeIndent="15">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <Nodes>
                    <asp:TreeNode Text="Relatórios" Value="0">
                        <asp:TreeNode Text="1. Indicação de Clientes" Value="1" Target="_blank" NavigateUrl="~/Relatorios/Relatorios/RelatorioProspectos.aspx"></asp:TreeNode>
                        <asp:TreeNode Text="2. Vendas" Value="0">
                            <asp:TreeNode Text="2.1 Vendas Por Período" Value="2" Target="_blank" NavigateUrl="~/Relatorios/Relatorios/RelatorioVendas.aspx"></asp:TreeNode>
                            <asp:TreeNode Text="2.2 Vendas X Indicação de Clientes" Value="3"></asp:TreeNode>
                            <asp:TreeNode Text="2.3 Volume por Estado" Value="4"></asp:TreeNode>
                            <asp:TreeNode Text="2.4 Vendas por Cidade/Estado" Value="5" Target="_blank" NavigateUrl="~/Relatorios/Relatorios/RelatorioVendasPorCidade.aspx"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="3. Comissão Revenda" Value="6" Target="_blank" NavigateUrl="~/Relatorios/Relatorios/RelatorioComissaoRevenda.aspx"></asp:TreeNode>
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                    HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle CssClass="NoSelecionado" BackColor="#B5B5B5" 
                    Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
        <div class="div_container" style="width: 68%; margin-left: 1%">
            <asp:UpdatePanel ID="UPCamposRelatorios" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:MultiView ID="mtvFiltrosRelatorios" runat="server" ActiveViewIndex="0">
                        <asp:View ID="view_inicial" runat="server">
                            <div style="text-align: center">
                                Escolha algum Relatório ao lado!</div>
                        </asp:View>
                        <asp:View ID="view_prospectos" runat="server">
                            <table width="100%">
                                <tr>
                                    <td width="20%" align="right">
                                        Revenda:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlRevendaProspectos" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Estado:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlEstadoProspectos" runat="server" CssClass="DropDownList"
                                            Width="50%" AutoPostBack="True" OnSelectedIndexChanged="ddlEstadoProspectos_SelectedIndexChanged">
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
                                                <asp:DropDownList ID="ddlCidadeProspectos" runat="server" CssClass="DropDownList"
                                                    Width="50%">
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
                                        <asp:CalendarExtender ID="tbxDataCadastroProspectosDe_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataCadastroProspectosDe">
                                        </asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxDataCadastroProspectosAte" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxDataCadastroProspectosAte_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataCadastroProspectosAte">
                                        </asp:CalendarExtender>
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
                            </table>
                        </asp:View>
                        <asp:View ID="view_vendas" runat="server">
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
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxVendaDataCadastroDe">
                                        </asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxVendaDataCadastroAte" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxVendaDataCadastroAte">
                                        </asp:CalendarExtender>
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
                            </table>
                        </asp:View>
                        <asp:View ID="view_vendas_grafico_barra" runat="server">
                            <table width="100%">
                                <tr>
                                    <td width="20%" align="right">
                                        Revenda:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlVendasRevendaGrafico_1" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Ano:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVendasAno_1" runat="server" CssClass="DropDownList" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_vendas_grafico_pizza" runat="server">
                            <table width="100%">
                                <tr>
                                    <td width="20%" align="right">
                                        Revenda:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlVendasRevendaGrafico_2" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Ano:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVendasAno_2" runat="server" CssClass="DropDownList" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_vendas_cidade" runat="server">
                            <table width="100%">
                                <tr>
                                    <td width="20%" align="right">
                                        Revenda:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlVendasRevendaCidade" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Estado:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlVendasEstados" runat="server" CssClass="DropDownList" Width="50%"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlVendaEstados_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Cidade:
                                    </td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlVendasCidade" runat="server" CssClass="DropDownList" Width="50%">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlVendasEstados" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Data de Cadastro de:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbxVendaDataDe" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxVendaDataDe">
                                        </asp:CalendarExtender>
                                        &nbsp;até
                                        <asp:TextBox ID="tbxVendaDataAte" runat="server" Width="22%" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxVendaDataAte">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="view_comissao_revenda" runat="server">
                            <table width="100%">
                                <tr>
                                    <td width="20%" align="right">
                                        Mês:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlComissaoMesRevenda" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Ano:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlComissaoAnoRevenda" runat="server" CssClass="DropDownList"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <div align="center">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="Button" OnClick="btnExibirRelatório_Click"
                            OnClientClick="aspnetForm.target =’_blank’;" Text="Exibir Relatório" Visible="False"
                            Width="150px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o relatório escolhido com base nos filtros escolhidos')" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="trvRelatoriosComcerciais" EventName="SelectedNodeChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
