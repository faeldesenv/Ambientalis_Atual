<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="PesquisarUsuarios.aspx.cs" Inherits="Usuario_PesquisarUsuarios" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Pesquisa de usuários</p>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="campos_pesquisa_usuarios">
        <table style="width: 100%">
            <tr>
                <td align="right" width="50">
                    Nome:
                </td>
                <td align="left" width="45%">
                    <asp:TextBox ID="tbxNome" runat="server" Width="80%" CssClass="TextBox"></asp:TextBox>
                </td>
                <td align="right" width="40">
                    Login:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxLogin" runat="server" Width="80%" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    E-mail:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxEmail" runat="server" Width="80%" CssClass="TextBox"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Pesquisar" OnClick="btnPesquisar_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa os usuários cadastrados, de acordo com os filtros escolhidos')" />
                </td>
            </tr>
        </table>
        <div class="contador_itens_grid">
            <table width="100%">
                <tr>
                    <td width="90%" align="right">
                        Quantidade de itens por pagina:</td>
                    <td>
            <asp:DropDownList ID="ddlQuantidaItensGrid" runat="server" 
                CssClass="DropDownList" Width="100%" AutoPostBack="True" 
                            onselectedindexchanged="ddlQuantidaItensGrid_SelectedIndexChanged" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de usuários que serão exibidos em cada página do resultado da pesquisa')">
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
        <div id="grid_usuarios">
            <asp:UpdatePanel ID="upGridUsuarios" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None" OnDeleteCommand="dgr_DeleteCommand"
                        OnEditCommand="dgr_EditCommand" OnItemDataBound="dgr_ItemDataBound" OnPageIndexChanged="dgr_PageIndexChanged"
                        Width="100%" OnPreRender="dgr_PreRender">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                            Mode="NumericPages" CssClass="GridPager" NextPageText="" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Nome" HeaderText="Nome"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Login" HeaderText="Login"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Email" HeaderText="E-Mail"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Edit.">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAbrir" runat="server" AlternateText="." CommandName="Edit"
                                        ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre o usuário para edição dos dados')" 
                                        onprerender="imgAbrir_PreRender" />
                                </ItemTemplate>
                                <HeaderStyle Width="22px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnExcluir" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                        OnPreRender="ibtnExcluir_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui os usuários selecionados')" />
                                    <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientes(this)" />
                                </HeaderTemplate>
                                <HeaderStyle Width="45px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                            VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <div style="text-align: right">
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </div>
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
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
