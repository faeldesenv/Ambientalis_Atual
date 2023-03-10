<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="PesquisarEmpresa.aspx.cs" Inherits="Cliente_PesquisarClientes" %>
 <%@ Register src="../MBOX/MBOX.ascx" tagname="MBOX" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Pesquisa de Empresa</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="filtros">
        <table width="100%">
            <tr>
                <td width="20%" align="right">
                    Nome:
                </td>
                <td width="25%" align="left">
                    <asp:TextBox ID="tbxNome" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                </td>
                <td width="20%" align="right">
                    Grupo Econômico:
                </td>
                <td width="25%">
                    <asp:DropDownList ID="ddlGrupoEconomico" runat="server" CssClass="DropDownList" 
                        Width="95%">
                    </asp:DropDownList>
                </td>
                <td rowspan="5" width="10%" valign="bottom">
                    <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" OnClick="btnPesquisar_Click"
                        Text="Pesquisar" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa as empresas cadastradas, de acordo com o(s) filtro(s) escolhido(s)')" />
                </td>
            </tr>
            <tr>
                <td class="labelFiltros">
                    &nbsp;Responsável: 
                </td>
                <td class="controlFiltros">
                    <asp:TextBox ID="tbxResponsavel" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                </td>
                <td class="labelFiltros">
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstados" runat="server" AutoPostBack="True" CssClass="DropDownList"
                        OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged" Width="95%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="labelFiltros">
                    CNPJ/CPF:</td>
                <td class="controlFiltros">
                    <asp:TextBox ID="tbxCnpjCpf" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                </td>
                <td class="labelFiltros">
                    Cidade:
                </td>
                <td>
                    <asp:UpdatePanel ID="upCidade" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCidades" runat="server" CssClass="DropDownList" Width="95%">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEstados" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="labelFiltros">
                    Satus:</td>
                <td class="controlFiltros">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList" Width="95%">
                        <asp:ListItem Value="0">-- Todos --</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">Ativo</asp:ListItem>
                        <asp:ListItem Value="2">Inativo</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="labelFiltros">
                    &nbsp;</td>
                <td>
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
                            onselectedindexchanged="ddlQuantidaItensGrid_SelectedIndexChanged" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de empresas que serão exibidas em cada página do resultado da pesquisa')">
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
    <div id="conteudo_sistema">
        <div id="grid">
            <asp:UpdatePanel ID="UpdateGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyField="Id" GridLines="None" OnDeleteCommand="dgr_DeleteCommand"
                        OnEditCommand="dgr_EditCommand" OnPageIndexChanged="dgr_PageIndexChanged" 
                        Width="100%" ForeColor="#333333">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                            NextPageText="" Mode="NumericPages" CssClass="GridPager" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Código"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Nome" HeaderText="Nome"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Responsavel" HeaderText="Responsável"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Telefone">
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefone" runat="server" CssClass="Label" Text="<%# bindTelefone(Container.DataItem) %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Grupo Econômico">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrupoEconomico" runat="server" CssClass="Label" Text="<%# bindGrupoEconomico(Container.DataItem) %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Tipo Pessoa">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoPessoa" runat="server" CssClass="Label" Text="<%# bindTipoPessoa(Container.DataItem) %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CPF/CNPJ">
                                <ItemTemplate>
                                    <asp:Label ID="lblCPF" runat="server" CssClass="Label" Text="<%# bindCpfCnpj(Container.DataItem) %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Razão Social">
                                <ItemTemplate>
                                    <asp:Label ID="Label181" runat="server" CssClass="Label" Text="<%# bindRazaoSocial(Container.DataItem) %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Edit.">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAbrir0" runat="server" AlternateText="." CommandName="Edit"
                                        ImageUrl="../imagens/icone_editar.png" ToolTip="Abrir" 
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre a empresa para edição dos dados')"/>
                                </ItemTemplate>
                                <HeaderStyle Width="22px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnExcluir0" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                        OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui a(s) empresa(s) selecionada(s)')" />
                                    <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientes(this)" />
                                </HeaderTemplate>
                                <HeaderStyle Width="45px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                            VerticalAlign="Top" CssClass="GridHeader" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGrid" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
