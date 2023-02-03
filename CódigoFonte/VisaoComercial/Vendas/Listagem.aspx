<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true" CodeFile="Listagem.aspx.cs" Inherits="Vendas_Listagem" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
Listagem de Vendas
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="conteudo_sistema" style="min-height:300px;">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td class="labelFiltros" width="20%">
                        Revenda:
                    </td>
                    <td width="25%" class="controlFiltros">
                        <asp:DropDownList ID="ddlRevenda" runat="server" CssClass="DropDownList" 
                            Width="95%">
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros" width="20%">
                        Estado:
                    </td>
                    <td width="25%" class="controlFiltros">
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="DropDownList" 
                            Width="95%" onselectedindexchanged="ddlEstado_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td rowspan="3" width="10%" valign="bottom">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Pesquisar" 
                            onclick="btnPesquisar_Click"/>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        &nbsp;&nbsp;Vendidos de:
                    </td>
                    <td class="controlFiltros">
                        <asp:TextBox ID="tbxDataDe" runat="server" CssClass="TextBox" Width="42%"></asp:TextBox>
                        <asp:CalendarExtender ID="tbxDataDe_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataDe">
                        </asp:CalendarExtender>
                        &nbsp;&nbsp; até:&nbsp;<asp:TextBox 
                            ID="tbxDataAte" runat="server" CssClass="TextBox" Width="41%"></asp:TextBox>
                        <asp:CalendarExtender ID="tbxDataAte_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataAte">
                        </asp:CalendarExtender>
                    </td>
                    <td class="labelFiltros">
                        Cidade:
                    </td>
                    <td class="controlFiltros">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCidade" runat="server" CssClass="DropDownList" 
                                    Width="95%">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlEstado" 
                                    EventName="SelectedIndexChanged" />
                            </triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
                </table>
        </div>
        <div class="contador_itens_grid">
            <table width="100%">
                <tr>
                    <td width="90%" align="right">
                        Quantidade de itens por pagina:</td>
                    <td>
            <asp:DropDownList ID="ddlQuantidaItensGrid" runat="server" 
                CssClass="DropDownList" Width="100%" AutoPostBack="True" onmouseout="tooltip.hide();" 
                            onmouseover="tooltip.show('Quantidade de grupos que serão exibidos em cada página do resultado da pesquisa')" 
                            onselectedindexchanged="ddlQuantidaItensGrid_SelectedIndexChanged">
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
                <asp:ListItem>1000</asp:ListItem>
                <asp:ListItem Value="1">-- Todos --</asp:ListItem>
            </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div id="grid">
            <asp:UpdatePanel ID="upPesquisa" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyField="Id" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" Height="45px"

                        PageSize="8" Width="100%" ForeColor="#333333" 
                        onpageindexchanged="dgr_PageIndexChanged">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                            NextPageText="" Mode="NumericPages" CssClass="GridPager"/>
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Revenda">
                                <ItemTemplate>
                                   <%# BindRevenda(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Cliente">
                                <ItemTemplate>
                                   <%# BindCliente(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CPF / CNPJ">
                                <ItemTemplate>
                                   <%# BindCPF(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Cadastro da Indicação do Cliente">
                                <ItemTemplate>
                                   <%# BindCadastro(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Data da Venda">
                                <ItemTemplate>
                                   <%# BindData(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Comissão (%)">
                                <ItemTemplate>
                                   <%# BindComissao(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Carencia" HeaderText="Carência"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Situação">
                                <ItemTemplate>
                                   <%# BindSituacao(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                            VerticalAlign="Top" CssClass="GridHeader" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGrid" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
</asp:Content>

