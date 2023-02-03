<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true"
    CodeFile="PesquisarGruposEconomicos.aspx.cs" Inherits="Cliente_PesquisarClientes" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<script type="text/javascript">

    function marcarDesmarcarLiberarClientes(chk) {
        var checkar = $(chk).attr('checked') == "checked" ? true : false;
        for (var i = 0; i < document.getElementsByClassName('chkLiberarCliente').length; i++) {
            document.getElementsByClassName('chkLiberarCliente')[i].children[0].checked = checkar;
        }
    }

    function DesmarcarLiberarClientes() {        
        for (var i = 0; i < document.getElementsByClassName('chkLiberarCliente').length; i++) {
            document.getElementsByClassName('chkLiberarCliente')[i].children[0].checked = false;
        }
    }
</script>
    <style type="text/css">
        .style2
        {
            font-size: 12pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Pesquisa de grupos econômicos</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="conteudo_sistema">
        <div id="filtros">
            <table width="100%">
                <tr>
                    <td width="20%" align="right" class="style2">
                        <strong>Sistema:</strong></td>
                    <td width="25%" class="controlFiltros">
                        <asp:DropDownList ID="ddlSistema" runat="server" CssClass="DropDownList" 
                            Width="95%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Escolhe se é cliente do Sistema Sustentar ou um cliente da Ambientalis')">
                            <asp:ListItem Value="0">Sustentar</asp:ListItem>
                            <asp:ListItem Value="1">Ambientalis</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros" width="20%">
                        Responsavel:
                    </td>
                    <td width="25%" class="controlFiltros">
                        <asp:TextBox ID="tbxResponsavel" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                    <td rowspan="5" width="10%" valign="bottom">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="Button" Text="Pesquisar" OnClick="btnPesquisar_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa os grupos econômicos cadastrados de acordo com o(s) filtro(s) escolhido(s)')" />
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        Nome:
                    </td>
                    <td class="controlFiltros">
                        <asp:TextBox ID="tbxNome" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                    <td class="labelFiltros">
                        CNPJ/CPF:
                    </td>
                    <td class="controlFiltros">
                        <asp:TextBox ID="tbxCnpjCpf" runat="server" CssClass="TextBox" Width="95%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        &nbsp;&nbsp;Status:
                    </td>
                    <td class="controlFiltros">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList" Width="95%">
                            <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                            <asp:ListItem Value="1">Ativo</asp:ListItem>
                            <asp:ListItem Value="2">Inativo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros">
                        Estado:
                    </td>
                    <td class="controlFiltros">
                        <asp:DropDownList ID="ddlEstados" runat="server" Width="95%" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged1" CssClass="DropDownList">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        Pendências de ativação:
                    </td>
                    <td class="controlFiltros">
                        <asp:DropDownList ID="ddlPendenciasAtivacao" runat="server" CssClass="DropDownList"
                            Width="95%">
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros">
                        <asp:Label ID="lblLiberar" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="liberar_modal" runat="server" 
                            BackgroundCssClass="simplemodal" 
                            CancelControlID="fechar_escolha_administrador" 
                            PopupControlID="escolha_administrador" TargetControlID="lblLiberar">
                        </asp:ModalPopupExtender>
                        
                        <asp:Label ID="lblAux" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="modal_ativacao" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="div_fechar_popup_ativacao" DynamicServicePath="" Enabled="True"
                            PopupControlID="pop_up_ativacao" TargetControlID="lblAux">
                        </asp:ModalPopupExtender>
                        Cidade:
                    </td>
                    <td class="controlFiltros">
                        &nbsp;
                        <asp:DropDownList ID="ddlCidades" runat="server" CssClass="DropDownList" Width="95%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="labelFiltros">
                        Cancelado:</td>
                    <td class="controlFiltros">
                        <asp:DropDownList ID="ddlCancelado" runat="server" CssClass="DropDownList" 
                            Width="95%">
                            <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                            <asp:ListItem Value="1">Sim</asp:ListItem>
                            <asp:ListItem Value="2">Não</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="labelFiltros">
                       
                    </td>
                    <td class="controlFiltros">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="contador_itens_grid">
            <table width="100%">
                <tr>
                    <td width="90%" align="right">
                        Quantidade de itens por pagina:
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
        <div id="grid">
            <asp:UpdatePanel ID="upPesquisa" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dgrClientes" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyField="Id" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" Height="45px"
                        OnDeleteCommand="dgrClientes_DeleteCommand" OnEditCommand="dgrClientes_EditCommand"
                        OnItemDataBound="dgrClientes_ItemDataBound" OnPageIndexChanged="dgr_PageIndexChanged"
                        PageSize="8" Width="100%" ForeColor="#333333">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                            NextPageText="" Mode="NumericPages" CssClass="GridPager" />
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
                            <asp:BoundColumn DataField="Responsavel" HeaderText="Responsável"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CNPJ/CPF">
                                <ItemTemplate>
                                    <%# bindingCnpjCpf(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Cidade">
                                <ItemTemplate>
                                    <%# bindingCidade(Container.DataItem) %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ATIVAÇÃO">
                                <ItemTemplate>
                                    <asp:Button ID="btnAtivarLogus" runat="server" CssClass="Button" OnClick="btnAtivarLogus_Click"
                                        OnInit="btnAtivarLogus_Init" Text="Logus" Width="90px" Height="23px" Style="padding: 2px;
                                        font-size: 8pt;" CommandName="Cancel" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'
                                        OnDataBinding="btnAtivarLogus_DataBinding1" 
                                        onprerender="btnAtivarLogus_PreRender" />
                                    <asp:Button ID="btnAtivarAmbientalis" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'
                                        CssClass="Button" OnClick="btnAtivarAmbientalis_Click" OnInit="btnAtivarLogus_Init"
                                        OnPreRender="btnAtivarLogus_PreRender" Text="Ambientalis" Width="90px" Height="23px"
                                        Style="padding: 2px; font-size: 8pt;" CommandName="Sort" />
                                </ItemTemplate>
                                <HeaderStyle Width="230px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Ativação em massa">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbLiberar" runat="server" CssClass="chkLiberarCliente" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnLiberar" runat="server" 
                                        ImageUrl="~/imagens/ok18x18.png" onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Ativa o(s) grupo(s) econômico(s) selecionado(s)')" 
                                        onclick="ibtnLiberar_Click" OnClientClick="tooltip.hide();" oninit="ibtnLiberar_Init"/>
                                    <input id="ckbLiberarCliente" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarLiberarClientes(this)" />
                                </HeaderTemplate>
                                <HeaderStyle Width="45px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Edit.">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAbrir0" runat="server" AlternateText="." CommandName="Edit"
                                        ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre o grupo econômico para edição dos dados')" />
                                </ItemTemplate>
                                <HeaderStyle Width="22px" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                            VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:Label ID="lblQuantidade" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGrid" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="popups">
    <div id="pop_ups_grupo_economico">
        <div id="pop_up_ativacao" class="pop_up" style="width: 500px">
            <div id="div_fechar_popup_ativacao" class="btn_cancelar_popup">
            </div>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAtivar">
                <asp:UpdatePanel ID="upAtivacao" runat="server" 
    ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td align="right" style="width: 25%">
                                    Senha de Ativação*:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxSenhaAtivacao" runat="server" TextMode="Password" 
                                    Width="60%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Obrigatório!"
                                    ControlToValidate="tbxSenhaAtivacao" ValidationGroup="rfvAtivacao" 
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfIdAdministrador" runat="server" Value="0" />
                                    <asp:HiddenField ID="hfIGrupo" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAtivar" runat="server" CssClass="Button" Text="Ativar" Width="150px"
                                    OnClick="btnAtivar_Click" ValidationGroup="rfvAtivacao" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnAtivar" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <div id="escolha_administrador" class="pop_up" style="width: 500px">
            <div id="fechar_escolha_administrador" class="btn_cancelar_popup">
            </div>            
                <asp:UpdatePanel ID="upEscolhaAdm" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%" cellspacing="5">
                            <tr>
                                <td align="left">
                                    Selecione por qual administrador deseja realizar a ativação:
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnAdministradorLogus" runat="server" CssClass="Button" 
                                        Text="Logus" Width="150px" onclick="btnAdministradorLogus_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnAdministradorAmbientalis" runat="server" CssClass="Button" 
                                        Text="Ambientalis" Width="150px" 
                                        onclick="btnAdministradorAmbientalis_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfAdministradorSelecionado" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdministradorAmbientalis" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnAdministradorLogus" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>            
        </div>
    </div>

</asp:Content>
