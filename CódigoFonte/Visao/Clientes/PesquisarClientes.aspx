<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="PesquisarClientes.aspx.cs" Inherits="Clientes_PesquisarClientes" %>
<%@ Register src="../MBOX/MBOX.ascx" tagname="MBOX" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
    <p>
        pesquisa de cliente</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div id="filtros">
        <table width="100%">
            <tr>
                <td width="20%" align="right">
                    Nome/Razão Social:
                </td>
                <td width="25%" align="left">
                    <asp:TextBox ID="tbxNomeRazao" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                </td>
                <td width="20%" align="right">
                    Estado:</td>
                <td width="25%">
                    <asp:DropDownList ID="ddlEstados" runat="server" AutoPostBack="True" CssClass="DropDownList"
                        OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged" Width="95%">
                    </asp:DropDownList>
                </td>
                <td rowspan="4" width="10%" valign="bottom">
                    <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" OnClick="btnPesquisar_Click"
                        Text="Pesquisar" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa os clientes cadastrados, de acordo com o(s) filtro(s) escolhido(s)')" />
                </td>
            </tr>
            <tr>
                <td class="labelFiltros">
                    &nbsp;CNPJ/CPF: 
                </td>
                <td class="controlFiltros">
                    <asp:TextBox ID="tbxCnpjCpf" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                </td>
                <td class="labelFiltros">
                   Cidade:
                </td>
                <td>
                
                    <asp:UpdatePanel ID="UPCidade" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCidades" runat="server" CssClass="DropDownList" Width="95%">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEstados" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                
                    </td>
            </tr>
            <tr>
                <td class="labelFiltros">
                    Status:</td>
                <td class="controlFiltros">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList" Width="95%">
                        <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                        <asp:ListItem Value="1">Ativos</asp:ListItem>
                        <asp:ListItem Value="2">Inativos</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="labelFiltros">                    
                </td>
                <td>
                    
                </td>
            </tr>
            </table>
    </div>
    <div class="contador_itens_grid">
            <table width="99%">
                <tr>
                    <td width="90%" align="right">
                        Quantidade de itens por pagina:</td>
                    <td>
            <asp:DropDownList ID="ddlQuantidaItensGrid" runat="server" CssClass="DropDownList" Width="100%" AutoPostBack="True" 
              onselectedindexchanged="ddlQuantidaItensGrid_SelectedIndexChanged" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de clientes que serão exibidos em cada página do resultado da pesquisa')">
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
        <div>
            <asp:UpdatePanel ID="UPClientes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgrClientes" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CellPadding="4" DataKeyField="Id" Font-Bold="False" 
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="False" ForeColor="#333333" GridLines="None" Height="45px" 
                        ondeletecommand="dgrClientes_DeleteCommand" 
                        OnEditCommand="dgrClientes_EditCommand" 
                        OnItemDataBound="dgrClientes_ItemDataBound" 
                        OnPageIndexChanged="dgrClientes_PageIndexChanged" PageSize="8" Width="100%">
                        <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" Font-Size="Small" 
                            ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" 
                            NextPageText="" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Nome/Razão Social">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTitulo" runat="server" 
                                        NavigateUrl="<%# bindingUrl(Container.DataItem) %>" 
                                        Text="<%# bindingTitulo(Container.DataItem) %>"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CNPJ/CPF">
                                <ItemTemplate>
                                    <%# bindingCnpjCpf(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <ItemTemplate>
                                    <%# bindingStatusCliente(Container.DataItem) %>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                    Width="150px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Cidade">
                                <ItemTemplate>
                                    <%# bindingCidade(Container.DataItem) %>
                                </ItemTemplate>
                                <HeaderStyle Width="180px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Estado">
                                <ItemTemplate>
                                    <%# bindingEstadoSigla(Container.DataItem) %>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" 
                                    Width="180px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Edit.">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAbrir0" runat="server" AlternateText="." 
                                        CommandName="Edit" ImageUrl="../imagens/icone_editar.png" 
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Abre o cliente para edição dos dados')" Visible="<%#BindingVisivel(Container.DataItem) %>" Enabled="<%#BindingVisivel(Container.DataItem) %>"/>
                                </ItemTemplate>
                                <HeaderStyle Width="25px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" Visible="<%#BindingVisivel(Container.DataItem) %>" Enabled="<%#BindingVisivel(Container.DataItem) %>" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnExcluir1" runat="server" CommandName="Delete" 
                                        ImageUrl="~/imagens/excluir.gif" onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Exclui o(s) cliente(s) selecionado(s)')" 
                                        OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                    <input id="ckbSelecionar0" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientesVerificandoPermissao(this)" />
                                </HeaderTemplate>
                                <HeaderStyle Width="45px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="True" 
                            ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
</asp:Content>

