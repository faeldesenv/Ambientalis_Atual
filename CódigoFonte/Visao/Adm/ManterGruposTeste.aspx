<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true" CodeFile="ManterGruposTeste.aspx.cs" Inherits="Adm_ManterGruposTeste" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Funcoes.js" type="text/javascript"></script>
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Grupos de teste
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Label ID="lblAlteracaoSenha" runat="server"></asp:Label>
        <asp:ModalPopupExtender ID="modal_alteracao_senha" runat="server" BackgroundCssClass="simplemodal"
            CancelControlID="div_fechar_popup_alteracao_Senha" PopupControlID="alteracao_Senha_pop_up" TargetControlID="lblAlteracaoSenha">
        </asp:ModalPopupExtender>
        <asp:Label ID="lblCadastroGrupo" runat="server"></asp:Label>
        <asp:ModalPopupExtender ID="modalCadastroGrupo_extender" runat="server" BackgroundCssClass="simplemodal"
            CancelControlID="div_fechar_popup_cadastro_grupo" PopupControlID="pop_cadastro_grupo" TargetControlID="lblCadastroGrupo">
        </asp:ModalPopupExtender>                
        <div id="botao_novo_grupo" class="barra_titulos">
            <asp:ImageButton ID="ibtnNovoGrupoTeste" runat="server" ImageUrl="~/imagens/icone_adicionar.png" OnClick="ibtnNovoGrupoTeste_Click" OnInit="ibtnNovoGrupoTeste_Init"
                OnClientClick="tooltip.hide();" Style="width: 20px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cadastra um novo grupo econômico de teste no sistema')" />
            <strong>&nbsp; Novo Grupo de Teste:</strong>
        </div>
        <div class="contador_itens_grid">
            <table width="100%">
                <tr>
                    <td width="90%" align="right">Quantidade de itens por pagina:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlQuantidaItensGrid" runat="server" CssClass="DropDownList"
                            Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGrid_SelectedIndexChanged" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de grupos que serão exibidos em cada página do resulado da pesquisa')">
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
        <div id="grid_grupos_teste">
            <asp:UpdatePanel ID="upGruposTeste" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgrGruposTeste" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyField="Id" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="None" Height="45px" OnDeleteCommand="dgrGruposTeste_DeleteCommand" OnEditCommand="dgrGruposTeste_EditCommand" OnPageIndexChanged="dgrGruposTeste_PageIndexChanged" PageSize="8" Width="100%" OnInit="dgrGruposTeste_Init">
                        <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" NextPageText="" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Nome" HeaderText="Nome"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="E-mail">
                                <ItemTemplate>
                                    <%# bindingEmail(Container.DataItem) %>
                                </ItemTemplate>
                                <HeaderStyle Width="225px" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Responsavel" HeaderText="Nome do Contato">
                                <HeaderStyle Width="210px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Telefone">
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefone" runat="server" CssClass="Label" Text="<%# bindTelefone(Container.DataItem) %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="120px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Módulos">
                                <ItemTemplate>
                                    <%# bindingModulos(Container.DataItem) %>
                                </ItemTemplate>
                                <HeaderStyle Width="300px" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="InicioTeste" HeaderText="Abertura do teste" DataFormatString="{0:d}">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="140px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FimTeste" HeaderText="Encerramento do teste" DataFormatString="{0:d}">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="140px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Login">
                                <ItemTemplate>
                                    <%# bindingLogin(Container.DataItem) %>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Edit.">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAbrir" runat="server" AlternateText="." CommandName="Edit"
                                        ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre o usuário para edição dos dados')" />
                                </ItemTemplate>
                                <HeaderStyle Width="45px" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnExcluir" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                        OnPreRender="ibtnExcluir_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o(s) usuário(s) selecionado(s)')" />
                                    <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientes(this)" />
                                </HeaderTemplate>
                                <HeaderStyle Width="45px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="popups">
        <div id="pop_cadastro_grupo" class="pop_up" style="width: 600px">
            <div id="div_fechar_popup_cadastro_grupo" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Grupo Econômico de Teste
            </div>
            <div>
                <asp:UpdatePanel ID="upCadastroGrupoTeste" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos">
                            <table style="width: 100%">
                                <tr>
                                    <td align="right">Nome*:
                                    </td>
                                    <td align="left" style="margin-left: 40px">
                                        <asp:TextBox ID="tbxNome" runat="server" Width="350px" CssClass="TextBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Obrigatório!"
                                            ValidationGroup="rfvSalvar" ControlToValidate="tbxNome" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">E-mail*:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxEmail" runat="server" Width="350px" CssClass="TextBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Obrigatório!"
                                            ValidationGroup="rfvSalvar" ControlToValidate="tbxEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Nome do Contato:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxContato" runat="server" Width="350px" CssClass="TextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Telefone:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxTelefone" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Início do teste*:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxInicioTeste" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxInicioTeste_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxInicioTeste">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Obrigatório!"
                                            ValidationGroup="rfvSalvar" ControlToValidate="tbxInicioTeste" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Fim do teste*:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxFimTeste" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxFimTeste">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Obrigatório!"
                                            ValidationGroup="rfvSalvar" ControlToValidate="tbxFimTeste" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Módulos*:
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkDnpm" runat="server" Text="DNPM" />&nbsp;&nbsp;
                            <asp:CheckBox ID="chkMeioAmbiente" runat="server" Text="Meio Ambiente" />&nbsp;&nbsp;
                            <asp:CheckBox ID="chkContratos" runat="server" Text="Contratos" />&nbsp;&nbsp;
                            <asp:CheckBox ID="chkDiversos" runat="server" Text="Diversos" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-bottom: 10px;">
                            <div style="font-weight: bold; font-size: 12px; color: #373737; margin-top: 25px; padding-bottom: 3px; border-bottom: 2px solid #383838; margin-bottom: 5px; position: relative;">Usuário</div>
                            <div>
                                <table style="width: 100%">
                                    <tr>
                                        <td align="right" style="width: 30%">Login*:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbxLogin" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Obrigatório!"
                                                ValidationGroup="rfvSalvar" ControlToValidate="tbxLogin" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="senha_cadastro" runat="server">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="right" style="width: 30%">Senha*:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbxSenha" runat="server" Width="200px" TextMode="Password" CssClass="TextBox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Obrigatório!"
                                                ValidationGroup="rfvSalvar" ControlToValidate="tbxSenha" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Confirmação de senha*:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbxConfirmarSenha" runat="server" Width="200px" TextMode="Password" CssClass="TextBox"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Obrigatório!"
                                                ValidationGroup="rfvSalvar" ControlToValidate="tbxConfirmarSenha" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="senha_alteracao" runat="server" visible="false">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="right" style="width: 30%">Senha:
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnAlteracaoSenha" runat="server" CssClass="Button" Text="Alterar Senha"
                                                OnClick="btnAlteracaoSenha_Click" OnInit="btnAlteracaoSenha_Init" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div style="text-align: right;">
                            <asp:HiddenField ID="hfIdGrupo" runat="server" />
                            <asp:Button ID="btnSalvar" runat="server" CssClass="Button" Text="Salvar"
                                ValidationGroup="rfvSalvar" OnClick="btnSalvar_Click" OnInit="btnSalvar_Init" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="alteracao_Senha_pop_up" class="pop_up_super_super" style="width: 500px">
            <div id="div_fechar_popup_alteracao_Senha" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Alteração de Senha
            </div>
            <div id="campos_alteracao_senha">
                <table style="width: 100%">
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lblInformativoSenha02" runat="server" Text="A nova senha deve ter no mínimo 6 dígitos, com no mínimo 2 números e 2 letras"
                                Style="font-weight: 700; font-size: x-small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Nova Senha*:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="tbxNovaSenha" runat="server" Width="200px" TextMode="Password" CssClass="TextBox"
                                onmouseout="tooltip.hide();" onmouseover="tooltip.show('A nova senha deve ter no minímo 6 digitos, com no minímo 2 números e 2 letras.')"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Obrigatório!"
                                ValidationGroup="rfvAlterarSenha" ControlToValidate="tbxNovaSenha" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Confirmar Nova Senha*:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="tbxConfirmarNovaSenha" runat="server" Width="200px" TextMode="Password"
                                CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Obrigatório!"
                                ValidationGroup="rfvAlterarSenha" ControlToValidate="tbxConfirmarNovaSenha" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnAlterarSenha" runat="server" CssClass="Button" Text="Alterar Senha"
                                ValidationGroup="rfvAlterarSenha" OnClick="btnAlterarSenha_Click" />
                            <asp:UpdatePanel ID="upAlterarSenha" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hfIdUsuario" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

