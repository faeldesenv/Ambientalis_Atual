<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="PesquisaVencimentosContrato.aspx.cs" Inherits="Vencimentos_PesquisaVencimentosContrato" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    Pesquisa de contratos  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="filtros_pesquisa">
        <asp:UpdatePanel ID="UPFiltros" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td class="labelFiltros" width="10%">Grupo Econômico:</td>
                        <td class="controlFiltros" width="16%">
                            <asp:DropDownList ID="ddlGrupoEconomico" runat="server" CssClass="DropDownList"
                                Width="95%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlGrupoEconomico_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="labelFiltros" width="20%">
                            <div>
                                <asp:UpdatePanel ID="UPFornecedorCliente" runat="server"
                                    UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="15%">
                                                    <asp:Label ID="lblFornecedorCliente" runat="server" Text="Fornecedor/Cliente"></asp:Label></td>
                                                <td align="left" width="40%">
                                                    <asp:TextBox ID="tbxFornecedorCliente" runat="server" CssClass="TextBox"
                                                        Width="95%" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlComo"
                                            EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </td>

                    </tr>
                    <tr>
                        <td class="labelFiltros" width="10%">Empresa:
                        </td>
                        <td class="controlFiltros" width="16%">
                            <asp:UpdatePanel ID="UPEmpresa" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" AutoPostBack="True"
                                        CssClass="DropDownList" Width="95%">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomico"
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="labelFiltros" width="25%">
                            <table width="100%">
                                <tr>
                                    <td width="15%">Status:
                                    </td>
                                    <td align="left" width="40%">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList"
                                            Width="95%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelFiltros">&nbsp;Como:
                        </td>
                        <td class="controlFiltros">
                            <asp:DropDownList ID="ddlComo" runat="server" CssClass="DropDownList"
                                Width="95%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlComo_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                <asp:ListItem Value="1">Contratante</asp:ListItem>
                                <asp:ListItem Value="2">Contratada</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="labelFiltros">
                            <table width="100%">
                                <tr>
                                    <td width="15%">Número do Contrato:
                                    </td>
                                    <td align="left" width="40%">
                                        <asp:TextBox ID="tbxNumeroContrato" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelFiltros">&nbsp;</td>
                        <td class="controlFiltros">&nbsp;</td>
                        <td class="labelFiltros">
                            <table width="100%">
                                <tr>
                                    <td width="15%"></td>
                                    <td align="right" width="40%">
                                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <div class="contador_itens_grid">
            <table width="100%">
                <tr>
                    <td width="90%" align="right">Quantidade de itens por pagina:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlQuantidaItensGrid" runat="server" CssClass="DropDownList"
                            Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGrid_SelectedIndexChanged"
                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de orgãos ambientais que serão exibidos em cada página do resultado da pesquisa')">
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                            <asp:ListItem Value="1000">1000</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="grid">
        <asp:UpdatePanel ID="UPPesquisa" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="dgr" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    Width="100%" OnPageIndexChanging="dgr_PageIndexChanging" OnRowDeleting="dgr_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Id" Visible="False"></asp:BoundField>
                        <asp:TemplateField HeaderText="Grupo/Empresa">
                            <ItemTemplate>
                                <%# BindGrupoEmpresa(Container.DataItem) %>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Como" HeaderText="Como"></asp:BoundField>
                        <asp:BoundField DataField="Numero" HeaderText="Número"></asp:BoundField>
                        <asp:TemplateField HeaderText="Fornecedor/Cliente">
                            <ItemTemplate>
                                <%# BindFornecedorCliente(Container.DataItem) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <%# BindStatus(Container.DataItem) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data de Abertura">
                            <ItemTemplate>
                                <%# BindDataAbertura(Container.DataItem) %>&nbsp;&nbsp;&nbsp;
                            </ItemTemplate>
                            <HeaderStyle Width="140px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit.">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" AlternateText="."
                                    ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre o contrato para edição dos dados')"
                                    PostBackUrl="<%# BindEditarContrato(Container.DataItem) %>" Enabled="<%#BindingVisivel(Container.DataItem) %>" Visible="<%#BindingVisivel(Container.DataItem) %>" />
                            </ItemTemplate>
                            <HeaderStyle Width="22px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnExcluirCont" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif" Visible="<%#BindingVisivel(Container.DataItem) %>"
                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" OnPreRender="ibtnExcluir_PreRender" />
                            </ItemTemplate>
                            <HeaderStyle Width="45px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                        VerticalAlign="Top" CssClass="GridHeader" />
                    <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

                <br />
                <asp:HiddenField ID="hfQuantidadeExibicao" runat="server" />
                <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGrid" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="dgr" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="dgr" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
