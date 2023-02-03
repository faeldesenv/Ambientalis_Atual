<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="Permissoes.aspx.cs" Inherits="Permissoes_Permissoes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () { CriarEventos(); });

        function CriarEventos() {
            
            $('#<%=ckbTodosUsuariosVisualizacao.ClientID %>').change(function () { marcarDesmarcarOutrosUsuarios() }); 
            $('#<%=ckblUsuariosVisualizacao.ClientID %>').change(function () {
                $('#<%=ckbTodosUsuariosVisualizacao.ClientID %>').attr("checked", false);
            });
        }

        function marcarDesmarcarOutrosUsuarios() {            
            if ($('#<%=ckbTodosUsuariosVisualizacao.ClientID %>').is(':checked')) {
                $("#<%= ckblUsuariosVisualizacao.ClientID %> input:checkbox").each(function () {
                    this.checked = false;
                });
            }
        }        
    </script>
    <style type="text/css">
        .usuarios_visualizacao {
            float: left;
            width: 50%;
            text-align: left;
        }

        .usuarios_edicao {
            float: right;
            width: 49%;
            text-align: left;
        }

        .div_modulo_permissao {
            border-radius: 5px;
            margin-bottom: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    Controle de Permissões
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblUsuariosVisualizacaoExtender" runat="server" Text=""></asp:Label>
    <asp:ModalPopupExtender ID="lblUsuariosVisualizacaoExtender_popupextender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fechar_edicao_usuarios_visualizacao"
        PopupControlID="edicao_usuarios_visualizacao" TargetControlID="lblUsuariosVisualizacaoExtender">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblUsuariosEdicaoExtender" runat="server" Text=""></asp:Label>
    <asp:ModalPopupExtender ID="lblUsuariosEdicaoExtender_popupextender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fechar_edicao_usuario_edicao"
        PopupControlID="edicao_usuario_edicao" TargetControlID="lblUsuariosEdicaoExtender">
    </asp:ModalPopupExtender>
    <div id="conteudo_permissoes">
        <div id="permissoes_modulo_geral" class="div_modulo_permissao" runat="server">
            <div class="barra_titulos" style="width: 98%;">
                Módulo Geral
            </div>
            <div>
                <asp:UpdatePanel ID="UPConfigsModGeral" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="usuarios_visualizacao">
                            <strong>Usuários Visualizadores:</strong>
                            <asp:Label ID="lblUsuariosVisualizacaoModuloGeral" runat="server"></asp:Label>
                            <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoModGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                onmouseover="tooltip.show('Exibe os usuários de visualização do módulo geral para edição')" OnClick="ibtnEditarUsuariosVisualizacaoModGeral_Click" OnInit="ibtnEditarUsuariosVisualizacaoModGeral_Init" />
                        </div>
                        <div class="usuarios_edicao">
                            <strong><asp:Label ID="lblTituloUsuariosEdicaoModuloGeral" runat="server"></asp:Label></strong>
                            <asp:Label ID="lblUsuarioEdicaoModuloGeral" runat="server"></asp:Label>
                            <asp:ImageButton ID="ibtnEditarUsuarioEdicaoModGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" 
                                onmouseover="tooltip.show('Exibe o usuário de edição do módulo geral para edição')" OnClick="ibtnEditarUsuarioEdicaoModGeral_Click" OnInit="ibtnEditarUsuarioEdicaoModGeral_Init" />
                        </div>
                        <div style="clear: both;"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>                
            </div>
        </div>

        <div id="permissoes_modulo_dnpm" class="div_modulo_permissao" runat="server">
            <div class="barra_titulos" style="width: 98%;">
                Módulo ANM
            </div>
            <div style="margin-bottom: 10px; margin-top: 10px;">
                <strong>Configuração:</strong><br />
                <asp:DropDownList ID="ddlTipoConfiguracaoModuloDNPM" runat="server" Width="80%" CssClass="DropDownList" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoConfiguracaoModuloDNPM_SelectedIndexChanged">
                    <asp:ListItem Value="G">Geral</asp:ListItem>
                    <asp:ListItem Value="E">Por Empresa</asp:ListItem>
                    <asp:ListItem Value="P">Por Processo</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width:98%;">
                <asp:UpdatePanel ID="UPConfigsModDNPM" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="usuarios_visualizacao">
                            <div id="div_dnpm_tipo_geral" class="dnpm_tipo_geral" runat="server">
                                <strong>Usuários Visualizadores:</strong>
                                <asp:Label ID="lblUsuariosVisualizacaoDNPMGeral" runat="server"></asp:Label>
                                <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoDNPMGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Exibe os usuários de visualização do módulo DNPM para edição')" OnClick="ibtnEditarUsuariosVisualizacaoDNPMGeral_Click" OnInit="ibtnEditarUsuariosVisualizacaoDNPMGeral_Init" />
                            </div>
                            <div id="div_dnpm_outros_tipos" class="dnpm_outros_tipos" runat="server" visible="false">
                                <div>
                                    <div style="float:left; width:45%;">
                                        <strong>Visualização:</strong>
                                    </div>
                                    <div style="float:right; width:50%; text-align:right;">
                                        Quantidade de itens por página
                                        <asp:DropDownList ID="ddlQuantidaItensGridVisualizacaoDNPM" runat="server" CssClass="DropDownList" Width="20%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGridVisualizacaoDNPM_SelectedIndexChanged">
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>1000</asp:ListItem>                                            
                                        </asp:DropDownList>
                                    </div> 
                                    <div style="clear:both;"></div>                                   
                                </div>                                
                                <div style="margin-top:5px;">
                                    <div id="dnpm_grid_visualizacao_processos" runat="server" visible="false">
                                    <asp:GridView ID="grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosVisualizacaoModuloDNPM_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosVisualizacaoProcessosModuloDNPM_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaVisualizacaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processo">
                                                <ItemTemplate>
                                                    <%# BindProcessoVisualizacaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Usuários Visualizadores">
                                                <ItemTemplate>
                                                    <%# BindUsuariosVisualizacaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoDnpm" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os usuários de visualização do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                        </div>
                                    <div id="dnpm_grid_visualizacao_empresa" runat="server" visible="false">
                                        <asp:GridView ID="grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosVisualizacaoModuloDNPM_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosVisualizacaoEmpresasModuloDNPM_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaVisualizacaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Usuários Visualizadores">
                                                <ItemTemplate>
                                                    <%# BindUsuariosVisualizacaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoDnpm" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os usuários de visualização do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="usuarios_edicao">
                            <div id="div_dnpm_tipo_geral_edit" class="dnpm_tipo_geral" runat="server">
                                <strong><asp:Label ID="lblTituloUsuariosEdicaoModuloDNPM" runat="server"></asp:Label></strong>
                                <asp:Label ID="lblUsuarioEdicaoDNPMGeral" runat="server"></asp:Label>
                                <asp:ImageButton ID="ibtnEditarUsuarioEdicaoDNPMGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Exibe o usuário de edição do módulo geral para edição')" OnClick="ibtnEditarUsuarioEdicaoDNPMGeral_Click" OnInit="ibtnEditarUsuarioEdicaoDNPMGeral_Init" />
                            </div>
                            <div id="div_dnpm_outros_tipos_edit" class="dnpm_outros_tipos" runat="server" visible="false">                                
                                <div>
                                    <div style="float:left; width:45%;">
                                        <strong>Edição:</strong>
                                    </div>
                                    <div style="float:right; width:50%; text-align:right;">
                                        Quantidade de itens por página
                                        <asp:DropDownList ID="ddlQuantidaItensGridEdicaoDNPM" runat="server" CssClass="DropDownList" Width="20%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGridEdicaoDNPM_SelectedIndexChanged">
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>1000</asp:ListItem>                                            
                                        </asp:DropDownList>
                                    </div> 
                                    <div style="clear:both;"></div>                                   
                                </div>                                
                                <div style="margin-top:5px;">
                                    <div id="dnpm_grid_edicao_processos" runat="server" visible="false">
                                    <asp:GridView ID="grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosEdicaoModuloDNPM_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosEdicaoProcessosModuloDNPM_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaEdicaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processo">
                                                <ItemTemplate>
                                                    <%# BindProcessoEdicaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%# BindTituloUsuariosEditores(Container.DataItem)%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# BindUsuariosEdicaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosEdicaoDnpm" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o(s) usuário(s) de edição do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                        </div>
                                    <div id="dnpm_grid_edicao_empresas" runat="server" visible="false">
                                        <asp:GridView ID="grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosEdicaoModuloDNPM_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosEdicaoEmpresasModuloDNPM_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaEdicaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%# BindTituloUsuariosEditores(Container.DataItem)%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# BindUsuariosEdicaoModuloDNPM(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosEdicaoDnpm" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o(s) usuário(s) de edição do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoConfiguracaoModuloDNPM" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGridVisualizacaoDNPM" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGridEdicaoDNPM" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div id="permissoes_modulo_meio_ambiente" class="div_modulo_permissao" runat="server">
            <div class="barra_titulos" style="width: 98%;">
                Módulo Meio Ambiente
            </div>
            <div style="margin-bottom: 10px; margin-top: 10px;">
                <strong>Configuração:</strong><br />
                <asp:DropDownList ID="ddlTipoConfiguracaoModuloMeioAmbiente" runat="server" Width="80%" CssClass="DropDownList" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoConfiguracaoModuloMeioAmbiente_SelectedIndexChanged">
                    <asp:ListItem Value="G">Geral</asp:ListItem>
                    <asp:ListItem Value="E">Por Empresa</asp:ListItem>
                    <asp:ListItem Value="P">Por Processo</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width:98%;">
                <asp:UpdatePanel ID="UPConfigsModMeioAmbiente" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="usuarios_visualizacao">
                            <div id="div_meio_ambiente_tipo_geral" class="meio_ambiente_tipo_geral" runat="server">
                                <strong>Usuários Visualizadores:</strong>
                                <asp:Label ID="lblUsuariosVisualizacaoMeioAmbienteGeral" runat="server"></asp:Label>
                                <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoMeioAmbienteGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Exibe os usuários de visualização do módulo de meio ambiente para edição')" OnClick="ibtnEditarUsuariosVisualizacaoMeioAmbienteGeral_Click" OnInit="ibtnEditarUsuariosVisualizacaoMeioAmbienteGeral_Init" />
                            </div>
                            <div id="div_meio_ambiente_outros_tipos" class="meio_ambiente_outros_tipos" runat="server" visible="false">
                                <div>
                                    <div style="float:left; width:45%;">
                                        <strong>Visualização:</strong>
                                    </div>
                                    <div style="float:right; width:50%; text-align:right;">
                                        Quantidade de itens por página
                                        <asp:DropDownList ID="ddlQuantidaItensGridVisualizacaoMA" runat="server" CssClass="DropDownList" Width="20%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGridVisualizacaoMA_SelectedIndexChanged">
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>1000</asp:ListItem>                                            
                                        </asp:DropDownList>
                                    </div> 
                                    <div style="clear:both;"></div>                                   
                                </div>
                                <div style="margin-top:5px;">
                                    <div id="meio_ambiente_visualizacao_empresas" runat="server" visible="false">
                                        <asp:GridView ID="grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosVisualizacaoModuloMeioAmbiente_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosVisualizacaoEmpresasModuloMeioAmbiente_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaVisualizacaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Usuários Visualizadores">
                                                <ItemTemplate>
                                                    <%# BindUsuariosVisualizacaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoMeioAmbiente" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os usuários de visualização do módulo de Meio Ambiente para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    </div>
                                    <div id="meio_ambiente_visualizacao_processos" runat="server" visible="false">
                                        <strong>Processos Ambientais:</strong>
                                        <asp:GridView ID="grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosVisualizacaoModuloMeioAmbiente_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosVisualizacaoProcessosModuloMeioAmbiente_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaVisualizacaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processo">
                                                <ItemTemplate>
                                                    <%# BindProcessoVisualizacaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Usuários Visualizadores">
                                                <ItemTemplate>
                                                    <%# BindUsuariosVisualizacaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoMeioAmbiente" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os usuários de visualização do módulo de Meio Ambiente para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                        <br /><strong>Cadastros Técnicos Federais:</strong>
                                        <asp:GridView ID="grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosVisualizacaoModuloMeioAmbiente_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosVisualizacaoCadastrosModuloMeioAmbiente_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaVisualizacaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processo">
                                                <ItemTemplate>
                                                    <%# BindProcessoVisualizacaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Usuários Visualizadores">
                                                <ItemTemplate>
                                                    <%# BindUsuariosVisualizacaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoMeioAmbiente" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os usuários de visualização do módulo de Meio Ambiente para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                        <br /><strong>Outros:</strong>
                                        <asp:GridView ID="grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosVisualizacaoModuloMeioAmbiente_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosVisualizacaoOutrosModuloMeioAmbiente_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaVisualizacaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processo">
                                                <ItemTemplate>
                                                    <%# BindProcessoVisualizacaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Usuários Visualizadores">
                                                <ItemTemplate>
                                                    <%# BindUsuariosVisualizacaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoMeioAmbiente" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os usuários de visualização do módulo de Meio Ambiente para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="usuarios_edicao">
                            <div id="div_meio_ambiente_tipo_geral_edit" class="meio_ambiente_tipo_geral" runat="server">
                                <strong><asp:Label ID="lblTituloUsuariosEdicaoModuloMeioAmbiente" runat="server"></asp:Label></strong>
                                <asp:Label ID="lblUsuarioEdicaoMeioAmbienteGeral" runat="server"></asp:Label>
                                <asp:ImageButton ID="ibtnEditarUsuarioEdicaoMeioAmbienteGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Exibe o usuário de edição do módulo geral para edição')" OnClick="ibtnEditarUsuarioEdicaoMeioAmbienteGeral_Click" OnInit="ibtnEditarUsuarioEdicaoMeioAmbienteGeral_Init" />
                            </div>
                            <div id="div_meio_ambiente_outros_tipos_edit" class="meio_ambiente_outros_tipos" runat="server" visible="false">
                                <div>
                                    <div style="float:left; width:45%;">
                                        <strong>Edição:</strong>
                                    </div>
                                    <div style="float:right; width:50%; text-align:right;">
                                        Quantidade de itens por página
                                        <asp:DropDownList ID="ddlQuantidaItensGridEdicaoMA" runat="server" CssClass="DropDownList" Width="20%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGridEdicaoMA_SelectedIndexChanged">
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>1000</asp:ListItem>                                            
                                        </asp:DropDownList>
                                    </div> 
                                    <div style="clear:both;"></div>                                   
                                </div>                                
                                <div style="margin-top:5px;">
                                    <div id="meio_ambiente_edicao_empresas" runat="server" visible="false">
                                        <asp:GridView ID="grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosEdicaoModuloMeioAmbiente_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosEdicaoEmpresasModuloMeioAmbiente_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaEdicaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%# BindTituloUsuariosEditores(Container.DataItem)%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# BindUsuariosEdicaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosEdicaoMeioAmbiente" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o(s) usuário(s) de edição do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    </div>
                                    <div id="meio_ambiente_edicao_processos" runat="server" visible="false">
                                        <strong>Processos Ambientais:</strong>
                                        <asp:GridView ID="grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosEdicaoModuloMeioAmbiente_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosEdicaoProcessosModuloMeioAmbiente_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaEdicaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processo">
                                                <ItemTemplate>
                                                    <%# BindProcessoEdicaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%# BindTituloUsuariosEditores(Container.DataItem)%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# BindUsuariosEdicaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosEdicaoMeioAmbiente" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o(s) usuário(s) de edição do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                        <br /><strong>Cadastros Técnicos Federais:</strong>
                                        <asp:GridView ID="grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosEdicaoModuloMeioAmbiente_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosEdicaoCadastrosModuloMeioAmbiente_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaEdicaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processo">
                                                <ItemTemplate>
                                                    <%# BindProcessoEdicaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%# BindTituloUsuariosEditores(Container.DataItem)%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# BindUsuariosEdicaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosEdicaoMeioAmbiente" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o(s) usuário(s) de edição do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                        <br /><strong>Outros:</strong>
                                        <asp:GridView ID="grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosEdicaoModuloMeioAmbiente_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosEdicaoOutrosModuloMeioAmbiente_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaEdicaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processo">
                                                <ItemTemplate>
                                                    <%# BindProcessoEdicaoModuloMeioAmbiente(Container.DataItem)%>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%# BindTituloUsuariosEditores(Container.DataItem)%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# BindUsuariosEdicaoModuloMeioAmbiente(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosEdicaoMeioAmbiente" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o(s) usuário(s) de edição do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    </div>                             
                                </div>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoConfiguracaoModuloMeioAmbiente" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGridVisualizacaoMA" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGridEdicaoMA" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div id="permissoes_modulo_contratos" class="div_modulo_permissao" runat="server">
            <div class="barra_titulos" style="width: 98%;">
                Módulo Contratos
            </div>
            <div style="margin-bottom: 10px; margin-top: 10px;">
                <strong>Configuração:</strong><br />
                <asp:DropDownList ID="ddlTipoConfiguracaoModuloContratos" runat="server" Width="80%" CssClass="DropDownList" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoConfiguracaoModuloContratos_SelectedIndexChanged">
                    <asp:ListItem Value="G">Geral</asp:ListItem>
                    <asp:ListItem Value="E">Por Empresa</asp:ListItem>
                    <asp:ListItem Value="S">Por Setor</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width:98%;">
                <asp:UpdatePanel ID="UPConfigsModContratos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="usuarios_visualizacao">
                            <div id="div_contratos_tipo_geral" class="contratos_tipo_geral" runat="server">
                                <strong>Usuários Visualizadores:</strong>
                                <asp:Label ID="lblUsuariosVisualizacaoContratosGeral" runat="server"></asp:Label>
                                <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoContratosGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Exibe os usuários de visualização do módulo de contratos para edição')" OnClick="ibtnEditarUsuariosVisualizacaoContratosGeral_Click" OnInit="ibtnEditarUsuariosVisualizacaoContratosGeral_Init" />
                            </div>
                            <div id="div_contratos_outros_tipos" class="contratos_outros_tipos" runat="server" visible="false">
                                <div>
                                    <div style="float:left; width:45%;">
                                        <strong>Visualização:</strong>
                                    </div>
                                    <div style="float:right; width:50%; text-align:right;">
                                        Quantidade de itens por página
                                        <asp:DropDownList ID="ddlQuantidaItensGridVisualizacaoContratos" runat="server" CssClass="DropDownList" Width="20%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGridVisualizacaoContratos_SelectedIndexChanged">
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>1000</asp:ListItem>                                            
                                        </asp:DropDownList>
                                    </div> 
                                    <div style="clear:both;"></div>                                   
                                </div>
                                <div style="margin-top:5px;">     
                                    <div id="contratos_visualizacao_empresas" runat="server" visible="false">                           
                                    <asp:GridView ID="grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosVisualizacaoModuloContratos_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosVisualizacaoEmpresasModuloContratos_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaVisualizacaoModuloContratos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="Usuários Visualizadores">
                                                <ItemTemplate>
                                                    <%# BindUsuariosVisualizacaoModuloContratos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoContratos" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os usuários de visualização do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                        </div>
                                    <div id="contratos_visualizacao_setores" runat="server" visible="false">
                                        <asp:GridView ID="grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosVisualizacaoModuloContratos_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosVisualizacaoSetoresModuloContratos_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>                                            
                                            <asp:TemplateField HeaderText="Setor">
                                                <ItemTemplate>
                                                    <%# BindSetoresVisualizacaoModuloContratos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Usuários Visualizadores">
                                                <ItemTemplate>
                                                    <%# BindUsuariosVisualizacaoModuloContratos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoContratos" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os usuários de visualização do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="usuarios_edicao">
                            <div id="div_contratos_tipo_geral_edit" class="contratos_tipo_geral" runat="server">
                                <strong><asp:Label ID="lblTituloUsuariosEdicaoModuloContratos" runat="server"></asp:Label></strong>
                                <asp:Label ID="lblUsuarioEdicaoContratosGeral" runat="server"></asp:Label>
                                <asp:ImageButton ID="ibtnEditarUsuarioEdicaoContratosGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Exibe o usuário de edição do módulo de contratos para edição')" OnClick="ibtnEditarUsuarioEdicaoContratosGeral_Click" OnInit="ibtnEditarUsuarioEdicaoContratosGeral_Init" />
                            </div>
                            <div id="div_contratos_outros_tipos_edit" class="contratos_outros_tipos" runat="server" visible="false">
                                <div>
                                    <div style="float:left; width:45%;">
                                        <strong>Edição:</strong>
                                    </div>
                                    <div style="float:right; width:50%; text-align:right;">
                                        Quantidade de itens por página
                                        <asp:DropDownList ID="ddlQuantidaItensGridEdicaoContratos" runat="server" CssClass="DropDownList" Width="20%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGridEdicaoContratos_SelectedIndexChanged">
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>1000</asp:ListItem>                                            
                                        </asp:DropDownList>
                                    </div> 
                                    <div style="clear:both;"></div>                                   
                                </div>                                
                                <div style="margin-top:5px;">
                                    <div id="contratos_edicao_empresas" runat="server" visible="false">
                                    <asp:GridView ID="grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosEdicaoModuloContratos_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosEdicaoEmpresasModuloContratos_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaEdicaoModuloContratos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%# BindTituloUsuariosEditores(Container.DataItem)%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# BindUsuariosEdicaoModuloContratos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosEdicaoContratos" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o(s) usuário(s) de edição do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                        </div>
                                    <div id="contratos_edicao_setores" runat="server" visible="false">
                                        <asp:GridView ID="grvConfiguracaoUsuariosEdicaoSetoresModuloContratos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosEdicaoModuloContratos_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosEdicaoSetoresModuloContratos_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosEdicaoSetoresModuloContratos_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>                                            
                                            <asp:TemplateField HeaderText="Setor">
                                                <ItemTemplate>
                                                    <%# BindSetorEdicaoModuloContratos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%# BindTituloUsuariosEditores(Container.DataItem)%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# BindUsuariosEdicaoModuloContratos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosEdicaoContratos" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o(s) usuário(s) de edição do módulo ANM para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoConfiguracaoModuloContratos" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGridVisualizacaoContratos" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGridEdicaoContratos" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div id="permissoes_modulo_diversos" class="div_modulo_permissao" runat="server">
            <div class="barra_titulos" style="width: 98%;">
                Módulo Diversos
            </div>
            <div style="margin-bottom: 10px; margin-top: 10px;">
                <strong>Configuração:</strong><br />
                <asp:DropDownList ID="ddlTipoConfiguracaoModuloDiversos" runat="server" Width="80%" CssClass="DropDownList" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoConfiguracaoModuloDiversos_SelectedIndexChanged">
                    <asp:ListItem Value="G">Geral</asp:ListItem>
                    <asp:ListItem Value="E">Por Empresa</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width:98%;">
                <asp:UpdatePanel ID="UPConfigsModDiversos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="usuarios_visualizacao">
                            <div id="div_diversos_tipo_geral" class="diversos_tipo_geral" runat="server">
                                <strong>Usuários Visualizadores:</strong>
                                <asp:Label ID="lblUsuariosVisualizacaoDiversosGeral" runat="server"></asp:Label>
                                <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoDiversosGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Exibe os usuários de visualização do módulo ANM para edição')" OnClick="ibtnEditarUsuariosVisualizacaoDiversosGeral_Click" OnInit="ibtnEditarUsuariosVisualizacaoDiversosGeral_Init" />
                            </div>
                            <div id="div_diversos_outros_tipos" class="diversos_outros_tipos" runat="server" visible="false">
                                <div>
                                    <div style="float:left; width:45%;">
                                        <strong>Visualização:</strong>
                                    </div>
                                    <div style="float:right; width:50%; text-align:right;">
                                        Quantidade de itens por página
                                        <asp:DropDownList ID="ddlQuantidaItensGridVisualizacaoDiversos" runat="server" CssClass="DropDownList" Width="20%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGridVisualizacaoDiversos_SelectedIndexChanged">
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>1000</asp:ListItem>                                            
                                        </asp:DropDownList>
                                    </div> 
                                    <div style="clear:both;"></div>                                   
                                </div>
                                <div style="margin-top:5px;">
                                    <asp:GridView ID="grvConfiguracaoUsuariosVisualizacaoModuloDiversos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosVisualizacaoModuloDiversos_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosVisualizacaoModuloDiversos_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosVisualizacaoModuloDiversos_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaVisualizacaoModuloDiversos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Usuários Visualizadores">
                                                <ItemTemplate>
                                                    <%# BindUsuariosVisualizacaoModuloDiversos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosVisualizacaoDiversos" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os usuários de visualização do módulo de diversos para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="usuarios_edicao">
                            <div id="div_diversos_tipo_geral_edit" class="diversos_tipo_geral" runat="server">
                                <strong><asp:Label ID="lblTituloUsuariosEdicaoModuloDiversos" runat="server"></asp:Label></strong>
                                <asp:Label ID="lblUsuarioEdicaoDiversosGeral" runat="server"></asp:Label>
                                <asp:ImageButton ID="ibtnEditarUsuarioEdicaoDiversosGeral" runat="server" AlternateText="." ImageUrl="~/imagens/icone_editar.png" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Exibe o usuário de edição do módulo geral para edição')" OnClick="ibtnEditarUsuarioEdicaoDiversosGeral_Click" OnInit="ibtnEditarUsuarioEdicaoDiversosGeral_Init" />
                            </div>
                            <div id="div_diversos_outros_tipos_edit" class="diversos_outros_tipos" runat="server" visible="false">
                                <div>
                                    <div style="float:left; width:45%;">
                                        <strong>Edição:</strong>
                                    </div>
                                    <div style="float:right; width:50%; text-align:right;">
                                        Quantidade de itens por página
                                        <asp:DropDownList ID="ddlQuantidaItensGridEdicaoDiversos" runat="server" CssClass="DropDownList" Width="20%" AutoPostBack="True" OnSelectedIndexChanged="ddlQuantidaItensGridEdicaoDiversos_SelectedIndexChanged">
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>1000</asp:ListItem>                                            
                                        </asp:DropDownList>
                                    </div> 
                                    <div style="clear:both;"></div>                                   
                                </div>                                
                                <div style="margin-top:5px;">
                                    <asp:GridView ID="grvConfiguracaoUsuariosEdicaoModuloDiversos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="3" Width="100%" OnInit="grvConfiguracaoUsuariosEdicaoModuloDiversos_Init"
                                        OnPageIndexChanging="grvConfiguracaoUsuariosEdicaoModuloDiversos_PageIndexChanging" OnRowEditing="grvConfiguracaoUsuariosEdicaoModuloDiversos_RowEditing">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <%# BindEmpresaEdicaoModuloDiversos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%# BindTituloUsuariosEditores(Container.DataItem)%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# BindUsuariosEdicaoModuloDiversos(Container.DataItem)%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alterar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEditarUsuariosEdicaoDiversos" runat="server" AlternateText="." CommandName="Edit" ImageUrl="~/imagens/icone_editar.png"
                                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe o(s) usuário(s) de edição do módulo de diversos para edição')" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both;"></div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoConfiguracaoModuloDiversos" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGridVisualizacaoDiversos" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGridEdicaoDiversos" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div style="text-align: right; margin-top: 20px;">
            <asp:Button ID="btnSalvarPermissoes" runat="server" CssClass="Button" Text="Salvar Permissões" ValidationGroup="rfvSalvarStatus" OnClick="btnSalvarPermissoes_Click" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="edicao_usuarios_visualizacao" class="pop_up" style="width: 500px">
        <div id="fechar_edicao_usuarios_visualizacao" class="btn_cancelar_popup">
        </div>
        <asp:UpdatePanel ID="UPUsuariosVisualizacao" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <script>
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { CriarEventos(); });
                </script>
                <div class="barra_titulo">
                    Usuários Visualizadores do Módulo&nbsp;
                    <asp:Label ID="lblNomeModulo" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:CheckBox ID="ckbTodosUsuariosVisualizacao" runat="server" Text="Todos" />
                    <asp:CheckBoxList ID="ckblUsuariosVisualizacao" runat="server"></asp:CheckBoxList>
                </div>
                <asp:HiddenField ID="hfIdModuloPermissaoVisualizacao" runat="server" />
                <asp:HiddenField ID="hfTipoConfiguracaoVisualizacao" runat="server" />
                <asp:HiddenField ID="hfIdObjetoVisualizacao" runat="server" />
                <asp:HiddenField ID="hfTipoObjetoVisualizacao" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="text-align: right;">
            <asp:Button ID="btnSavarUsuariosVisualizacao" runat="server" CssClass="Button" Text="Salvar" OnClick="btnSavarUsuariosVisualizacao_Click" OnInit="btnSavarUsuariosVisualizacao_Init" />
        </div>
    </div>

    <div id="edicao_usuario_edicao" class="pop_up" style="width: 500px">
        <div id="fechar_edicao_usuario_edicao" class="btn_cancelar_popup">
        </div>
        <asp:UpdatePanel ID="UPUsuarioEdicao" runat="server" UpdateMode="Conditional">
            <ContentTemplate> 
                <script>
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { CriarEventos(); });
                </script>               
                <div class="barra_titulo">
                    <asp:Label ID="lblTituloUsuarioEdicao" runat="server"></asp:Label>&nbsp;
                    <asp:Label ID="lblNomeModuloEdicao" runat="server"></asp:Label>
                </div>
                <div id="gestao_compartilhada" runat="server">
                    <asp:RadioButtonList ID="rblUsuarioEdicao" runat="server"></asp:RadioButtonList>
                </div>
                <div id="gestao_comum" runat="server">                    
                    <asp:CheckBoxList ID="ckblUsuariosEdicao" runat="server"></asp:CheckBoxList>
                </div>
                <asp:HiddenField ID="hfIdModuloPermissaoEdicao" runat="server" />
                <asp:HiddenField ID="hfTipoConfiguracaoEdicao" runat="server" />
                <asp:HiddenField ID="hfIdObjetoEdicao" runat="server" />
                <asp:HiddenField ID="hfTipoObjetoEdicao" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="text-align: right;">
            <asp:Button ID="btnSalvarUsuarioEdicao" runat="server" CssClass="Button" Text="Salvar" OnClick="btnSalvarUsuarioEdicao_Click" OnInit="btnSalvarUsuarioEdicao_Init" />
        </div>
    </div>
</asp:Content>

