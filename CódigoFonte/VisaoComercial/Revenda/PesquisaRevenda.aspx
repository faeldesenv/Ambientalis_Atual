<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true" CodeFile="PesquisaRevenda.aspx.cs" Inherits="Revenda_Pesquisa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
    Pesquisa de Revendas
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="conteudo_sistema">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td class="labelFiltros" width="20%">
                        Nome:
                    </td>
                    <td width="25%" class="controlFiltros">
                        <asp:TextBox ID="tbxNome" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
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
                    <td rowspan="4" width="10%" valign="bottom">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Pesquisar" OnClick="btnPesquisar_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa os grupos econômicos cadastrados, de acordo com o(s) filtro(s) escolhido(s)')"/>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        &nbsp;&nbsp;CNPJ/CPF:
                                        </td>
                    <td class="controlFiltros">
                        <asp:TextBox ID="tbxCnpjCpf" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                    <td class="labelFiltros">
                        Cidade:
                    </td>
                    <td class="controlFiltros">
                        <asp:UpdatePanel ID="UPCidade" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCidades" runat="server" CssClass="DropDownList" 
                                    Width="95%">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlEstado" 
                                    EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        Responsavel:
                    </td>
                    <td class="controlFiltros">
                        <asp:TextBox ID="tbxResponsavel" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                    <td class="labelFiltros">
                        Status:
                    </td>
                    <td class="controlFiltros">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList" Width="95%">
                         <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                         <asp:ListItem Value="1">ATIVOS</asp:ListItem>
                         <asp:ListItem Value="2">INATIVOS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        Tipo de Parceria:</td>
                    <td class="controlFiltros">
                        <asp:DropDownList ID="ddlTipoParceiro" runat="server" CssClass="DropDownList" 
                            Width="95%">
                            <asp:ListItem Selected="True">-- Todos --</asp:ListItem>
                            <asp:ListItem>Revenda / Agente de Negócios</asp:ListItem>
                            <asp:ListItem>Consultoria Ambiental / Minerária</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros">
                        &nbsp;</td>
                    <td class="controlFiltros">
                        &nbsp;</td>
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
                            onselectedindexchanged="ddlQuantidaItensGrid_SelectedIndexChanged" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de grupos que serão exibidos em cada página do resultado da pesquisa')">
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
                    <asp:DataGrid ID="dgrClientes" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyField="Id" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" Height="45px"
                        OnEditCommand="dgrClientes_EditCommand"
                        OnItemDataBound="dgrClientes_ItemDataBound" OnPageIndexChanged="dgr_PageIndexChanged"
                        PageSize="8" Width="100%" ForeColor="#333333" 
                        ondeletecommand="dgrClientes_DeleteCommand">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                            NextPageText="" Mode="NumericPages" CssClass="GridPager"/>
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Nome">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTitulo" runat="server" NavigateUrl="<%# bindingUrl(Container.DataItem) %>"
                                        Text="<%# bindingTitulo(Container.DataItem) %>"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TipoParceiro" HeaderText="Tipo de Parceria">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Responsavel" HeaderText="Responsável"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CNPJ/CPF">
                                <ItemTemplate>
                                    <%# bindingCnpjCpf(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Razão Social">
                                <ItemTemplate>
                                    <%# bindingRazaoSocial(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Cidade">
                                <ItemTemplate>
                                    <%# bindingCidade(Container.DataItem) %>
                                </ItemTemplate>
                                <HeaderStyle Width="170px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Estado">
                                <ItemTemplate>
                                    <%# bindingEstadoSigla(Container.DataItem) %>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                    Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Ativa">
                                <ItemTemplate>
                                    <%# bindingStatusRevenda(Container.DataItem) %>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                    Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Edit.">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAbrir0" runat="server" AlternateText="." 
                                        CommandName="Edit" ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre o grupo econômico para edição dos dados')" 
                                        onprerender="imgAbrir0_PreRender" />
                                </ItemTemplate>
                                <HeaderStyle Width="25px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnExcluir0" runat="server" CommandName="Delete" 
                                        ImageUrl="~/imagens/excluir.gif" onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Exclui a(s) empresa(s) selecionada(s)')" 
                                        OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                    <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientes(this)" />
                                </HeaderTemplate>
                                <HeaderStyle Width="45px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
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

