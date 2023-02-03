<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="PesquisarTipoLicencas.aspx.cs" Inherits="Licenca_PesquisarTipoLicencas" %>
<%@ Register src="../MBOX/MBOX.ascx" tagname="MBOX" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Pesquisa de tipo de licença</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="conteudo_sistema">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td class="labelFiltros" width="20%">
                        Nome/Sigla:
                    </td>
                    <td width="25%" class="controlFiltros">
                        <asp:TextBox ID="tbxNome" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                    <td class="labelFiltros" width="20%">
                        &nbsp;</td>
                    <td width="25%">
                        &nbsp;</td>
                    <td width="10%" valign="bottom">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Pesquisar" OnClick="btnPesquisar_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa os tipos de licença cadastrados, de acordo com o(s) filtro(s) escolhido(s)')" />
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
                CssClass="DropDownList" Width="100%" AutoPostBack="True" 
                            onselectedindexchanged="ddlQuantidaItensGrid_SelectedIndexChanged" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de tipos de licença que serão exibidos em cada página do resultado da pesquisa')" >
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
            <asp:UpdatePanel ID="UpdateGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None" OnDeleteCommand="dgr_DeleteCommand"
                        OnEditCommand="dgr_EditCommand" OnPageIndexChanged="dgr_PageIndexChanged" Width="100%"
                        OnItemDataBound="dgr_ItemDataBound">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" 
                        HorizontalAlign="Center" Mode="NumericPages" NextPageText="" CssClass="GridPager"/>
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Nome" HeaderText="Nome"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Sigla" HeaderText="Sigla">
                                <HeaderStyle Width="250px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="diasValidadePadrao" 
                                HeaderText="Dias de Validade Padrão">
                                <HeaderStyle Width="300px" Font-Bold="True" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Edit.">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAbrir0" runat="server" AlternateText="." CommandName="Edit"
                                        ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre o tipo de licença para alteração dos dados')" 
                                        Enabled="<%#BindingVisivel(Container.DataItem) %>" Visible="<%#BindingVisivel(Container.DataItem) %>"/>
                                </ItemTemplate>
                                <HeaderStyle Width="22px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" Enabled="<%#BindingVisivel(Container.DataItem) %>" Visible="<%#BindingVisivel(Container.DataItem) %>" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnExcluir" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                        OnPreRender="ibtnExcluir_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui os tipos de licença selecionados')"  />
                                    <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientesVerificandoPermissao(this)" />
                                </HeaderTemplate>
                                <HeaderStyle Width="45px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" 
                        HorizontalAlign="Left" VerticalAlign="Top" CssClass="GridHeader" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
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
