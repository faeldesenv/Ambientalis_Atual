<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="Processos.aspx.cs" Inherits="Site_Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Style.css" rel="stylesheet" type="text/css" />    
    <style type="text/css">
        .style2
        {
            height: 20px;
        }
    </style>
    <script type="text/javascript">        

        function AlterarTooltip() {
            alert($("#<%=hfTipoOutros.ClientID %>").val());
            var valor = $("#<%=hfTipoOutros.ClientID %>").val();
            if (valor = "emp") {
                $("#<%=ibtnAdicionarOutros.ClientID %>").attr("onmouseover", "tooltip.show('Adiciona um outro vencimento fora do processo')");

            } else if (valor = "proc") {
                $("#<%=ibtnAdicionarOutros.ClientID %>").attr("onmouseover", "tooltip.show('Adiciona um outro vencimento dentro do processo')");
            }
        }

        function marcarEmailsGrupo() {
            $('.chkMarcarEmailsGrupo').change(function () {
                var estacochk = new Boolean();
                estadochk = $(this).attr('checked');

                $('.chkEmailsGruposEconomicosCont').children().find('input').each(function () {
                    if (estadochk) {
                        $(this).attr('checked', 'checked');
                    } else {
                        $(this).removeAttr('checked');
                    }
                });
            });
        }

        function marcarEmailsEmpresa() {
            $('.chkMarcarEmailsEmpresa').change(function () {
                var estacochk = new Boolean();
                estadochk = $(this).attr('checked');

                $('.chkEmailsEmpresasCont').children().find('input').each(function () {
                    if (estadochk) {
                        $(this).attr('checked', 'checked');
                    } else {
                        $(this).removeAttr('checked');
                    }
                });
            });
        }       

        function marcarEmailsConsultora() {
            $('.chkMarcarEmailsConsultora').change(function () {
                var estacochk = new Boolean();
                estadochk = $(this).attr('checked');

                $('.chkEmailsConsultorasCont').children().find('input').each(function () {
                    if (estadochk) {
                        $(this).attr('checked', 'checked');
                    } else {
                        $(this).removeAttr('checked');
                    }
                });
            });
        }

        function marcarVencimentosPeriodicos() {
            alert('foi');
            $('.chkMarcarTodosVencimentosPeriodicos').change(function () {
                var estacochk = new Boolean();
                estadochk = $(this).attr('checked');

                $('.chkMarcarVencimentosPeriodicos').children().find('input').each(function () {
                    if (estadochk) {
                        $(this).attr('checked', 'checked');
                    } else {
                        $(this).removeAttr('checked');
                    }
                });
            });
        } 

        function marcarDesmarcarClientes(chk) {
            var checkar = $(chk).is(':checked');
            for (var i = 0; i < document.getElementsByClassName('chkSelecionarCliente').length; i++) {
                document.getElementsByClassName('chkSelecionarCliente')[i].children[0].checked = checkar;
            }
        }

        function marcarDesmarcarClientesVerificandoPermissao(chk) {
            var checkar = $(chk).is(':checked');
            for (var i = 0; i < document.getElementsByClassName('chkSelecionarCliente').length; i++) {
                if (document.getElementsByClassName('chkSelecionarCliente')[i].children[0].disabled == false) {
                    document.getElementsByClassName('chkSelecionarCliente')[i].children[0].checked = checkar;
                }
            }
        }

        function marcarDesmarcarGridVerificandoPermissao(chk, classeDoCheckDasLinhas) {
            //var checkar = $(chk).attr('checked') == "checked" ? true : false;
            var checkar = $(chk).is(':checked');
            for (var i = 0; i < document.getElementsByClassName(classeDoCheckDasLinhas).length; i++) {
                if (document.getElementsByClassName(classeDoCheckDasLinhas)[i].children[0].disabled == false) {
                    document.getElementsByClassName(classeDoCheckDasLinhas)[i].children[0].checked = checkar;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">        
    <asp:Label ID="lblNotficacoes" runat="server" />
    <asp:ModalPopupExtender ID="lblNotficacoes_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal_super_super" 
        CancelControlID="cancelNots" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpNotificacoes" TargetControlID="lblNotficacoes">
    </asp:ModalPopupExtender>
    
    <asp:Label ID="lblProrrogacao" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="ModalPopupExtenderlblProrrogacao" runat="server" BackgroundCssClass="simplemodal_super_super" 
        CancelControlID="fecharProrrogacao" PopupControlID="popProrrogacao" TargetControlID="lblProrrogacao">
    </asp:ModalPopupExtender>
    
    <asp:Label ID="lblHistorico" runat="server" />
    <asp:ModalPopupExtender ID="ModalPopupExtenderhistorico_ModalPopupExtender" runat="server" 
        BackgroundCssClass="simplemodal_super_super" CancelControlID="fecharObs" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpObs" TargetControlID="lblHistorico">
    </asp:ModalPopupExtender>
    
    <asp:Label ID="lblEnvioHistoricoTotal" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblEnvioHistoricoTotal_popupextender" runat="server" BackgroundCssClass="simplemodal_super_super" CancelControlID="fecharEnviarEmailHistorico"
        PopupControlID="divPopUpEnviarEmailHistorico" TargetControlID="lblEnvioHistoricoTotal">
    </asp:ModalPopupExtender>

    <asp:Label ID="lblAlteracaoStatus" runat="server" />
    <asp:ModalPopupExtender ID="lblAlteracaoStatus_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal_super_super_super" CancelControlID="fecharAlteracaoStatus"
        DynamicServicePath="" Enabled="True" PopupControlID="divPopUpAlteracaoStatus" TargetControlID="lblAlteracaoStatus">
    </asp:ModalPopupExtender>

    <asp:Label ID="lblVencimentos" runat="server" Text="" />
    <asp:ModalPopupExtender ID="btnPopUpVencimentos_popupextender" runat="server" BackgroundCssClass="simplemodal_super_super"
        CancelControlID="fecharVencimento" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpVencimentos" TargetControlID="lblVencimentos">
    </asp:ModalPopupExtender>

    <asp:Button ID="btnPopUpProcesso" runat="server" Height="16px" Style="visibility: hidden" Text="Button" Width="47px" />
    <asp:ModalPopupExtender ID="lkbOpcoesProcessoNovo_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fecharProcesso" DynamicServicePath=""
        Enabled="True" PopupControlID="popup_Processo" TargetControlID="btnPopUpProcesso">
    </asp:ModalPopupExtender>

    <asp:Button ID="btnPopUplicenca" runat="server" Height="16px" Style="visibility: hidden" Text="Button" Width="47px" />
    <asp:ModalPopupExtender ID="btnPopUplicenca_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
        CancelControlID="fecharLicenca" DynamicServicePath="" Enabled="True" PopupControlID="popup_licenca" TargetControlID="btnPopUplicenca">
    </asp:ModalPopupExtender>

    <asp:Button ID="btnPopCondicionante" runat="server" Height="16px" Style="visibility: hidden" Text="Button" Width="47px" />
    <asp:ModalPopupExtender ID="lkbOpcoesCondicionante_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="btnPopCondicionante" 
        PopupControlID="popup_Condicionante" BackgroundCssClass="simplemodal" CancelControlID="fecharCondicionante">
    </asp:ModalPopupExtender>

    <asp:Button ID="btnPopUpRenovacao" runat="server" Height="16px" Style="visibility: hidden" Text="Button" Width="47px" />
    <asp:ModalPopupExtender ID="btnPopUpRenovacao_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fecharRenovacao" DynamicServicePath=""
        Enabled="True" PopupControlID="divPopUpRenovacao" TargetControlID="btnPopUpRenovacao">
    </asp:ModalPopupExtender>

    <asp:Button ID="btnPopUpOutros" runat="server" Height="16px" Style="visibility: hidden" Text="Button" Width="47px" />
    <asp:ModalPopupExtender ID="divPopUpOutros_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
        CancelControlID="fecharOutros" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpOutros" TargetControlID="btnPopUpOutros">
    </asp:ModalPopupExtender>
    
    <asp:Label ID="lblPopUpNotificacoes" runat="server"></asp:Label>
     <asp:ModalPopupExtender ID="lblPopUpNotificacoes_ModalPopupExtender" runat="server"
       BackgroundCssClass="simplemodal" CancelControlID="fecharNotificacoes" PopupControlID="popup_notificacoes" TargetControlID="lblPopUpNotificacoes">
     </asp:ModalPopupExtender>
    
    <asp:Label ID="lblCTF" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCTF_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
       CancelControlID="fecharCadastroTecnicoFederal" DynamicServicePath="" Enabled="True" TargetControlID="lblCTF" PopupControlID="divPopUpCadastroTecnicoFederal">
    </asp:ModalPopupExtender>
    
    <asp:Label ID="lblRenovacaoVencimentosPeriodicos" runat="server"></asp:Label>                        
    <asp:ModalPopupExtender ID="lblRenovacaoVencimentosPeriodicos_popupextender" runat="server" BackgroundCssClass="simplemodal" 
       CancelControlID="fechar_vencimentos_periodicos" PopupControlID="divPopUpVencimentosPeriodicos" TargetControlID="lblRenovacaoVencimentosPeriodicos">
    </asp:ModalPopupExtender>
    
    <asp:Label ID="lblRenovacaoVencimentosPeriodicosDatas" runat="server" Text=""></asp:Label>
    <asp:ModalPopupExtender ID="lblRenovacaoVencimentosPeriodicosDatas_popupextender" runat="server" BackgroundCssClass="simplemodal" 
       CancelControlID="fecharRenovacaoPeriodicosDatas" PopupControlID="divRenovacaoPeriodicosDatas" TargetControlID="lblRenovacaoVencimentosPeriodicosDatas">
    </asp:ModalPopupExtender>
    
    <asp:Label ID="lblAbrirContratos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblAbrirContratos_popupextender" runat="server" BackgroundCssClass="simplemodal" 
      CancelControlID="CancelarPopUpContrato" PopupControlID="divPopUpContratos" TargetControlID="lblAbrirContratos">
    </asp:ModalPopupExtender>
    
    <asp:Label ID="lblAbrirSelecaoContratos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblAbrirSelecaoContratos_popupextender" runat="server" BackgroundCssClass="simplemodal" 
      CancelControlID="fecharSelecaoContratos" PopupControlID="divPopUpSelecaoContratos" TargetControlID="lblAbrirSelecaoContratos">
    </asp:ModalPopupExtender>

    <asp:Label ID="lblUploadArquivos" runat="server" Text="" />
    <asp:ModalPopupExtender ID="lblUploadArquivos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fechar_arquivos_teste" 
        DynamicServicePath="" Enabled="True" PopupControlID="arquivos_teste" TargetControlID="lblUploadArquivos"></asp:ModalPopupExtender>
    <div>
        <div>
            <table width="100%">
                <tr>
                    <td width="35%">
                        Selecione o Grupo Econômico:<br />
                        <asp:DropDownList ID="ddlGrupoEconomicos" runat="server" CssClass="DropDownList"
                            Height="25px" Width="90%" OnSelectedIndexChanged="ddlGrupoEconomicos_SelectedIndexChanged"
                            AutoPostBack="True" DataTextField="Nome" DataValueField="Id" onmouseout="tooltip.hide();"
                            onmouseover="tooltip.show('Ao selecionar o grupo econômico, os processos de todas as suas empresas serão exibidos')">
                        </asp:DropDownList>
                    </td>
                    <td width="35%">
                        Filtre pela Empresa (opcional):<br />
                        <asp:UpdatePanel runat="server" ID="upEmpresa" ChildrenAsTriggers="False" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="DropDownList" Height="25px"
                                    Width="90%" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged" AutoPostBack="True"
                                    DataTextField="Nome" DataValueField="Id" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Ao selecionar a empresa, apenas os processos desta empresa serão exibidos')">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicos" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                     <td width="30%" align="left">
                        Status da Empresa:<br />
                        <asp:DropDownList ID="ddlStatusEmpresa" Height="25px" Width="90%" CssClass="DropDownList" runat="server"  OnSelectedIndexChanged="ddlGrupoEconomicos_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True">Todos</asp:ListItem>
                            <asp:ListItem>Ativo</asp:ListItem>
                            <asp:ListItem>Inativo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="containe_arvore">
            <asp:UpdatePanel ID="upOrgaos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="botao_orgao">
                        <div class="titulo_orgao">
                            Processos Municipais
                        </div>
                        <asp:DataList ID="dtlMunicipal" runat="server" DataKeyField="Id" OnEditCommand="dtlMunicipal_EditCommand">
                            <ItemTemplate>
                                <div class="content_botao_orgao">
                                    <div class="" style="float: left;">
                                        <asp:LinkButton ID="lkbTituloOrgao" CommandName="Edit" runat="server"><%# DataBinder.Eval(Container.DataItem,"Nome") %></asp:LinkButton>
                                    </div>
                                    <div style="float: right; margin-left: 4px">
                                        <asp:LinkButton ID="lkbProcessoOrgao" CommandName="Edit" runat="server"><%# BindQuantidade(Container.DataItem)%></asp:LinkButton>
                                    </div>
                                    <div class="ZerarClear">
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="botao_orgao">
                        <div class="titulo_orgao">
                            Processos Estaduais
                        </div>
                        <asp:DataList ID="dtlEstadual" runat="server" DataKeyField="Id" OnEditCommand="dtlEstadual_EditCommand">
                            <ItemTemplate>
                                <div class="content_botao_orgao">
                                    <div class="" style="float: left;">
                                        <asp:LinkButton ID="lkbTituloOrgao" CommandName="Edit" runat="server"><%# DataBinder.Eval(Container.DataItem,"Nome") %></asp:LinkButton>
                                    </div>
                                    <div style="float: right; margin-left: 4px">
                                        <asp:LinkButton ID="lkbProcessoOrgao" CommandName="Edit" runat="server"><%# BindQuantidade(Container.DataItem)%></asp:LinkButton>
                                    </div>
                                    <div class="ZerarClear">
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="botao_orgao">
                        <div class="titulo_orgao">
                            Processos Federais
                        </div>
                        <asp:DataList ID="dtlFederal" runat="server" DataKeyField="Id" OnEditCommand="dtlFederal_EditCommand">
                            <ItemTemplate>
                                <div class="content_botao_orgao">
                                    <div class="" style="float: left;">
                                        <asp:LinkButton ID="lkbTituloOrgao" CommandName="Edit" runat="server"><%# DataBinder.Eval(Container.DataItem,"Nome") %></asp:LinkButton>
                                    </div>
                                    <div style="float: right; margin-left: 4px">
                                        <asp:LinkButton ID="lkbProcessoOrgao" CommandName="Edit" runat="server"><%# BindQuantidade(Container.DataItem)%></asp:LinkButton>
                                    </div>
                                    <div class="ZerarClear">
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="botao_orgao">
                        <div class="titulo_orgao">
                            Cadastro Técnico Federal
                        </div>
                        <asp:DataList ID="dtlCTF" runat="server" DataKeyField="Id" OnEditCommand="dtlCTF_EditCommand">
                            <ItemTemplate>
                                <div class="content_botao_orgao">
                                    <div class="" style="float: left;">
                                        <asp:LinkButton ID="lkbTituloOrgao" CommandName="Edit" runat="server">Processos</asp:LinkButton>
                                    </div>
                                    <div style="float: right; margin-left: 4px">
                                        <asp:LinkButton ID="lkbProcessoOrgao" CommandName="Edit" runat="server"><%# BindQuantidade(Container.DataItem)%></asp:LinkButton>
                                    </div>
                                    <div class="ZerarClear">
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="botao_orgao">
                        <div class="titulo_orgao">
                            Outros
                        </div>
                        <asp:DataList ID="dtlOutros" runat="server" DataKeyField="Id" OnEditCommand="dtlOutros_EditCommand">
                            <ItemTemplate>
                                <div class="content_botao_orgao">
                                    <div class="" style="float: left;">
                                        <asp:LinkButton ID="lkbTituloOrgao" CommandName="Edit" runat="server"><%# DataBinder.Eval(Container.DataItem,"Nome") %></asp:LinkButton>
                                    </div>
                                    <div style="float: right; margin-left: 4px">
                                        <asp:LinkButton ID="lkbProcessoOrgao" CommandName="Edit" runat="server"><%# BindQuantidade(Container.DataItem)%></asp:LinkButton>
                                    </div>
                                    <div class="ZerarClear">
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <asp:HiddenField ID="hfIdGrupoEconomico" runat="server" />
                    <asp:HiddenField ID="hfIdEmpresa" runat="server" />
                    <asp:HiddenField ID="hfTipoProcesso" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicos" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <table width="100%" cellpadding="5" cellspacing="5">
                <tr>
                    <td width="30%" valign="top">
                        <div class="container_arvore">
                            <asp:UpdatePanel ID="upArvore" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="padding5">
                                        <div class="barra_opcoes_ferramentas" id="divOpcoesCadastro" runat="server">
                                            <div id="opcoesCadastroProcessos" runat="server">
                                                Processo:
                                            <asp:LinkButton ID="lkbOpcoesProcessoNovo" runat="server" OnClick="lkbOpcoesProcessoNovo_Click"
                                                OnClientClick="tooltip.hide();" OnInit="lkbOpcoesProcessoNovo_Init" onmouseout="tooltip.hide();"
                                                onmouseover="tooltip.show('Cadastra um novo processo ambiental para uma empresa do grupo')">Novo</asp:LinkButton>
                                            &nbsp;|
                                            <asp:LinkButton ID="lkbOpcoesProcessoEditar" runat="server" OnClick="lkbOpcoesProcessoEditar_Click"
                                                OnClientClick="tooltip.hide();" OnInit="lkbOpcoesProcessoEditar_Init" onmouseout="tooltip.hide();"
                                                onmouseover="tooltip.show('Edita o processo selecionado na árvore')">Editar</asp:LinkButton>
                                            &nbsp;|
                                            <asp:LinkButton ID="lkbOpcoesProcessoExcluir" runat="server" OnClick="lkbOpcoesProcessoExcluir_Click"
                                                OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o processo selecionado na árvore')">Excluir</asp:LinkButton>
                                            </div>
                                            <div id="opcoesCadastroOutros" runat="server">
                                                Outros:
                                            <asp:LinkButton ID="lkbOpcoesOutrosNovo" runat="server" OnClick="lkbOpcoesOutrosNovo_Click1"
                                                OnClientClick="tooltip.hide();" OnInit="lkbOpcoesOutrosNovo_Init1" onmouseout="tooltip.hide();"
                                                onmouseover="tooltip.show('Cadastra outros vencimentos fora do processo a serem controladas as datas de vencimento')">Novo</asp:LinkButton>
                                            </div>
                                            <div id="opcoesCadastroCadTecnico" runat="server">
                                                Cadastro Técnico Federal:
                                            <asp:LinkButton ID="lkbOpcoesNovaCTF" runat="server" OnClick="lkbOpcoesNovaCTF_Click"
                                                OnClientClick="tooltip.hide();" OnInit="lkbOpcoesNovaCTF_Init" onmouseout="tooltip.hide();"
                                                onmouseover="tooltip.show('Insere um novo Cadastro Técnico Federal para a empresa')">Nova</asp:LinkButton>
                                            &nbsp;|
                                            <asp:LinkButton ID="lkbOpcoesEditarCTF" runat="server" OnClick="lkbOpcoesEditarCTF_Click"
                                                OnClientClick="tooltip.hide();" OnInit="lkbOpcoesEditarCTFr_Init" onmouseout="tooltip.hide();"
                                                onmouseover="tooltip.show('Edita o Cadastro Técnico Federal selecionado')">Editar</asp:LinkButton>
                                            &nbsp;|
                                            <asp:LinkButton ID="lkbOpcoesExcluirCTF" runat="server" OnClick="lkbOpcoesExcluirCTF_Click"
                                                OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o Cadastro Técnico Federal selecionado')">Excluir</asp:LinkButton>
                                            </div>
                                            <div id="opcoesCadastroLicenca" runat="server">
                                                Licença:
                                                <asp:LinkButton ID="lkbOpcoesLicencaNova" runat="server" OnClick="lkbOpcoesLicencaNova_Click"
                                                    OnClientClick="tooltip.hide();" OnInit="lkbOpcoesLicencaNova_Init" onmouseout="tooltip.hide();"
                                                    onmouseover="tooltip.show('Cadastra uma nova licença para o processo selecionado')">Nova</asp:LinkButton>
                                                &nbsp;|
                                                <asp:LinkButton ID="lkbOpcoesLicencaEditar" runat="server" OnClick="lkbOpcoesLicencaEditar_Click"
                                                    OnClientClick="tooltip.hide();" OnInit="lkbOpcoesLicencaEditar_Init" onmouseout="tooltip.hide();"
                                                    onmouseover="tooltip.show('Abre a licença selecionada para edição de dados')">Editar</asp:LinkButton>
                                                &nbsp;|
                                                <asp:LinkButton ID="lkbOpcoesLicencaExcluir" runat="server" OnClick="lkbOpcoesLicencaExcluir_Click"
                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui a licença selecionada')">Excluir</asp:LinkButton>
                                            </div>                                             
                                        </div>
                                        <asp:Label ID="lblNomeOrgao" runat="server" Font-Bold="True"></asp:Label>
                                        <asp:TreeView ID="trvProcessos" runat="server" ImageSet="Arrows" OnSelectedNodeChanged="trvProcessos_SelectedNodeChanged"
                                            OnInit="trvProcessos_Init">
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                                                NodeSpacing="0px" VerticalPadding="0px" />
                                            <ParentNodeStyle Font-Bold="False" />
                                            <SelectedNodeStyle Font-Underline="True" ForeColor="#006600" HorizontalPadding="0px"
                                                VerticalPadding="0px" />
                                        </asp:TreeView>
                                        <asp:HiddenField ID="hfIdOrgao" runat="server" />
                                        <asp:HiddenField ID="hfTipoOutros" runat="server" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dtlMunicipal" EventName="EditCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicos" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="dtlFederal" EventName="EditCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="dtlEstadual" EventName="EditCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="dtlCTF" EventName="EditCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="dtlOutros" EventName="EditCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="trvProcessos" EventName="SelectedNodeChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>                        
                    </td>
                    <td width="70%" valign="top">
                        <div class="container_arvore">
                            <div class="padding5">
                                <div align="left">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:MultiView ID="mvwProcessos" runat="server">
                                                <asp:View ID="View1" runat="server">
                                                    <table cellpadding="2" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td align="left" colspan="2" style="width: 100%">
                                                                <div class="barra_titulos">
                                                                    <asp:Label ID="lblProcesso" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="2" style="width: 100%" width="40%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Consultoria:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblConsultoria" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Empresa:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblEmpresaProcesso" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%" class="style2">
                                                                <strong>Data de Abertura</strong>
                                                            </td>
                                                            <td width="60%" class="style2">
                                                                <asp:Label ID="lblDataAbertura" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Número do Processo:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblNumero" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" width="40%">
                                                                <strong>Detalhamento:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblObs" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2" height="20" style="width: 100%" valign="bottom">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View2" runat="server">
                                                    <table cellpadding="2" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td align="left" colspan="2" style="width: 100%">
                                                                <div class="barra_titulos">
                                                                    <asp:Label ID="lblLicenca" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="2" style="width: 100%" width="40%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Empresa:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblEmpresaLicenca" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Número da Licença:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblNumeroLicenca" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Data de Retirada:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblDataRetirada" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Data de Validade:</strong>&nbsp;
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblDataValidade" runat="server"></asp:Label>
                                                                &nbsp;=
                                                                <asp:Label ID="lblDiasValidade" runat="server"></asp:Label>
                                                                &nbsp;dias após a retirada.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Data Limite para Renovação:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblDataLimiteRenovacao" runat="server"></asp:Label>
                                                                &nbsp;= 120 dias antes da validade.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Próxima Notificação:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblDataAviso" runat="server"></asp:Label>
                                                                &nbsp;
                                                                <asp:LinkButton ID="lkbVenc0" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                    OnClick="lkbVenc0_Click">Ver mais</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Estado:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblEstadoLicenca" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Cidade:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblCidadeLicenca" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                &nbsp;
                                                            </td>
                                                            <td width="60%">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" width="40%">
                                                                <strong>Descrição:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblDescricao" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" width="40%">
                                                                &nbsp;
                                                            </td>
                                                            <td width="60%">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" width="40%">
                                                                <strong>Anexos:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:LinkButton ID="lbtnDownloadLicenca" runat="server" OnClick="lbtnDownloadLicenca_Click" OnInit="lbtnDownloadLicenca_Init">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top" width="40%">
                                                            </td>
                                                            <td width="60%">
                                                                &nbsp;
                                                                <asp:LinkButton ID="lkbCondicionantesPadroes" runat="server" OnClick="lkbCondicionantesPadroes_Click"
                                                                    OnLoad="lkbCondicionantesPadroes_Load" OnPreRender="lkbCondicionantesPadroes_PreRender"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Importa as condicionantes padrão definidas no tipo de licença selecionado')">Importar Condicionantes Padrões</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2" valign="top" width="40%">
                                                                <div class="barra_titulos">
                                                                    <strong><em>
                                                                        <asp:ImageButton ID="ibtnAdicionarCondicionanteLicenca" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                            OnClick="ibtnAdicionarCondicionanteLicenca_Click" OnInit="ibtnAdicionarCondicionanteLicenca_Init"
                                                                            OnPreRender="ibtnAdicionarCondicionanteLicenca_PreRender" onmouseout="tooltip.hide();"
                                                                            onmouseover="tooltip.show('Adiciona novas condicionantes à licença')" style="height: 20px" />
                                                                        Condicionantes</em></strong>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2" style="width: 100%" valign="top" width="40%">
                                                                <asp:GridView ID="grvCondicionantes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                                                    OnInit="grvCondicionantes_Init" OnRowDeleting="grvCondicionantes_RowDeleting"
                                                                    OnRowEditing="grvCondicionantes_RowEditing" Width="100%" OnRowCancelingEdit="grvCondicionantes_RowCancelingEdit"
                                                                    OnPageIndexChanging="grvCondicionantes_PageIndexChanging">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <%# bindStatus(Container.DataItem) %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="135px" />
                                                                            <ItemStyle Width="135px" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Numero" HeaderText="Número" ><HeaderStyle Width="110px" /><ItemStyle Width="110px" /></asp:BoundField>
                                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" ><ItemStyle Width="400px" /></asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Data de Vencimento">
                                                                            <HeaderStyle Width="110px" />
                                                                            <ItemTemplate>
                                                                                <%# bindDataVencimento(Container.DataItem) %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Data da Próxima Notificação">
                                                                            <HeaderStyle Width="100px" />
                                                                            <ItemTemplate>
                                                                                <%# bindDataAviso(Container.DataItem) %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Prorrogações">
                                                                           <ItemTemplate>
                                                                                <%# bindProrrogacoes(Container.DataItem) %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Anexos"><ItemTemplate><asp:ImageButton ID="ibtnAbrirDownload" runat="server" ImageUrl="~/imagens/icone_anexo.png" CommandArgument='<%#Eval("Id") %>' ToolTip="Ver Arquivos" 
                                                                                    Visible="<%#BindingVisivelPorPermissao(Container.DataItem) %>" Enabled="<%#BindingVisivelPorPermissao(Container.DataItem) %>" OnClick="ibtnAbrirDownload_Click" OnInit="ibtnAbrirDownload_Init" /></ItemTemplate><HeaderStyle Width="50px" HorizontalAlign="Center"  /><ItemStyle HorizontalAlign="Center" /></asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Renovar"><ItemTemplate><asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Cancel" ImageUrl="<%# BindImagemRenovacao(Container.DataItem) %>"
                                                                                    Enabled="<%# BindEnableRenovacao(Container.DataItem) %>" OnPreRender="ibtnRenovar_PreRender"
                                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova a condicionante, caso ela seja periódica')" 
                                                                                    Visible="<%#BindingVisivelPorPermissao(Container.DataItem) %>"/></ItemTemplate><HeaderStyle Width="45px" HorizontalAlign="Center" /><ItemStyle HorizontalAlign="Center" /></asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Editar"><ItemTemplate><asp:ImageButton ID="imgAbrir1" runat="server" AlternateText="." CommandName="Edit"
                                                                                    ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre a condicionante para a edição de dados')"
                                                                                    OnPreRender="imgAbrir_PreRender" Visible="<%#BindingVisivelPorPermissao(Container.DataItem) %>" Enabled="<%#BindingVisivelPorPermissao(Container.DataItem) %>"/></ItemTemplate><HeaderStyle Width="22px" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <ItemTemplate>
                                                                                 <asp:ImageButton Visible="<%#BindingVisivelPorPermissao(Container.DataItem) %>" Enabled="<%#BindingVisivelPorPermissao(Container.DataItem) %>" ID="ibtnExcluirCondicionante" runat="server" CommandName="Delete"
                                                                                    ImageUrl="~/imagens/excluir.gif" OnPreRender="ibtnExcluirCondicionante_PreRender"
                                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui a(s) condicionante(s) selecionada(s)')" />
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                               Excluir
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" HorizontalAlign="Center" /><HeaderStyle Width="70px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View3" runat="server">
                                                    <table cellpadding="2" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td align="left" style="width: 100%">
                                                                <div class="barra_titulos">
                                                                    <strong><em>
                                                                        <asp:ImageButton ID="ibtnAdicionarOutros" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                            OnInit="lkbOpcoesOutrosNovo_Init" OnClick="lkbOpcoesOutrosNovo_Click" OnPreRender="ibtnAdicionarOutros_PreRender"
                                                                            onmouseout="tooltip.hide();" />
                                                                    </em></strong>
                                                                    <asp:Label ID="lblTituloOutros" runat="server" Font-Bold="True"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 100%" width="40%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 100%" width="40%">
                                                                <asp:GridView ID="grvOutros" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                                                    OnInit="grvOutros_Init" OnRowDeleting="grvOutros_RowDeleting" OnRowEditing="grvOutros_RowEditing"
                                                                    Width="100%" OnRowCancelingEdit="grvOutros_RowCancelingEdit" OnPageIndexChanging="grvOutros_PageIndexChanging">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                        <asp:TemplateField HeaderText="Empresa">
                                                                            <ItemTemplate>
                                                                                <%# bindEmpresaOutros(Container.DataItem) %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Data de Vencimento">
                                                                            <HeaderStyle Width="110px" />
                                                                            <ItemTemplate>
                                                                                <%# bindDataVencimentoOutros(Container.DataItem) %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Data de Aviso">
                                                                            <HeaderStyle Width="100px" />
                                                                            <ItemTemplate>
                                                                                <%# bindDataAvisoOutros(Container.DataItem) %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Anexos"><ItemTemplate><asp:ImageButton ID="ibtnAbrirDownloadOutros" runat="server" ToolTip="Ver Arquivos" ImageUrl="~/imagens/icone_anexo.png" CommandArgument='<%#Eval("Id") %>' 
                                                                                    Visible="<%#BindingVisivelOutrosPorPermissao(Container.DataItem) %>" Enabled="<%#BindingVisivelOutrosPorPermissao(Container.DataItem) %>" 
                                                                                    OnClick="ibtnAbrirDownloadOutros_Click" OnInit="ibtnAbrirDownloadOutros_Init" /></ItemTemplate><HeaderStyle Width="80px" /></asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Renovar"><ItemTemplate><asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Cancel" ImageUrl="<%# BindImagemRenovacaoOutros(Container.DataItem) %>"
                                                                                    Enabled="<%# BindEnableRenovacaoOutros(Container.DataItem) %>" OnPreRender="ibtnRenovar_PreRender1"
                                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova o vencimento caso ele seja periódico')" Visible="<%#BindingVisivelOutrosPorPermissao(Container.DataItem) %>" /></ItemTemplate><HeaderStyle Width="45px" /></asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Editar"><ItemTemplate><asp:ImageButton ID="imgAbrir2" runat="server" AlternateText="." CommandName="Edit"
                                                                                    ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre o vencimento para edição dos dados')"
                                                                                    OnPreRender="imgAbrir2_PreRender" OnInit="imgAbrir2_Init" Visible="<%#BindingVisivelOutrosPorPermissao(Container.DataItem) %>" Enabled="<%#BindingVisivelOutrosPorPermissao(Container.DataItem) %>" /></ItemTemplate><HeaderStyle Width="22px" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnExcluir0" Visible="<%#BindingVisivelOutrosPorPermissao(Container.DataItem) %>" Enabled="<%#BindingVisivelOutrosPorPermissao(Container.DataItem) %>" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif" OnPreRender="ibtnExcluir_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o(s) vencimento(s) selecionado(s)')" />
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                Excluir
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                             <HeaderStyle Width="45px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 100%" width="40%">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View4" runat="server">
                                                    <table cellpadding="2" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td align="left" colspan="2" style="width: 100%">
                                                                <div class="barra_titulos">
                                                                    Cadastro Técnico Federal
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="2" style="width: 100%" width="40%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Empresa:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblEmpresaCTF" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Senha:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblSenhaCTF" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Atividades:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblAtividadesCTF" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Número da Licença:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblNumeroLicencaCTF" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Validade da Licença:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblValidadeLicencaCTF" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                &nbsp;
                                                            </td>
                                                            <td width="60%">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" width="40%">
                                                                <strong>Detalhamento:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblObservacoesCTF" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <strong>Anexos:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:LinkButton ID="lbtnDownloadCTF" runat="server" OnClick="lbtnDownloadCTF_Click" OnInit="lbtnDownloadCTF_Init">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" width="40%">
                                                                &nbsp;
                                                            </td>
                                                            <td width="60%">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2" style="width: 100%" valign="top" width="40%">
                                                                <asp:TabContainer ID="TabContainer3" runat="server" ActiveTabIndex="2" Width="100%"><asp:TabPanel ID="TabPanel8" runat="server" HeaderText="Validade"><HeaderTemplate>Entrega do Relatório Anual</HeaderTemplate><ContentTemplate><div style="width: 100%; height: 10px;"></div><table cellpadding="2" style="width: 100%;"><tr><td>Data da Entrega: <asp:Label ID="lblDataEntregaCTF" runat="server" Font-Bold="True"></asp:Label><div style="width: 100%; height: 10px;">Data do próximo aviso: <asp:Label ID="lblDataProximoAvisoRelAnualCTF" runat="server" Font-Bold="True"></asp:Label>&#160;- <asp:LinkButton ID="lkbVenc2" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                                OnClick="lkbVenc2_Click">Ver mais</asp:LinkButton></div></td></tr><tr><td align="right"><asp:Button ID="btnRenovarRelatorio" runat="server" CssClass="Button" Text="Renovar" Width="170px" OnClick="btnRenovarRelatorio_Click" OnInit="btnRenovarRelatorio_Init"
                                                                                            OnPreRender="btnRenovarRelatorio_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova a data de entrega do Relatório Anual por um novo período')" /></td></tr><tr><td>&#160;&#160; </td></tr></table></ContentTemplate></asp:TabPanel><asp:TabPanel ID="TabPanel9" runat="server" HeaderText="Pagamento da Taxa Trimestral"><ContentTemplate><div style="width: 100%; height: 10px;"></div><table cellpadding="2" style="width: 100%;"><tr><td>Data do Pagamento: <asp:Label ID="lblDataPagamentoCTF" runat="server" Font-Bold="True"></asp:Label><div style="width: 100%; height: 10px;">Data do próximo aviso: <asp:Label ID="lblDataProximoAvisoTaxaCTF" runat="server" Font-Bold="True"></asp:Label>&#160;-&#160; <asp:LinkButton ID="lkbVenc1" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                                OnClick="lkbVenc1_Click">Ver mais</asp:LinkButton></div></td></tr><tr><td align="right"><asp:Button ID="btnRenovarTaxa" runat="server" CssClass="Button" Text="Renovar" Width="170px"
                                                                                            OnClick="btnRenovarTaxa_Click" OnInit="btnRenovarTaxa_Init" OnPreRender="btnRenovarTaxa_PreRender"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova a data de pagamento da Taxa Trimestral por um novo período')" /></td></tr><tr><td>&#160;&#160; </td></tr></table></ContentTemplate></asp:TabPanel><asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Certificado de Regularidade"><ContentTemplate><div style="width: 100%; height: 10px;"></div><table cellpadding="2" style="width: 100%;"><tr><td>Data da Vencimento: <asp:Label ID="lblDataEntregaCertificadoCTF" runat="server" Font-Bold="True"></asp:Label><div style="width: 100%; height: 10px;">Data do próximo aviso: <asp:Label ID="lblDataProximoAvisoCertificadoCTF" runat="server" Font-Bold="True"></asp:Label>&#160;-. <asp:LinkButton ID="lkbVenc3" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')" OnClick="lkbVenc3_Click">Ver mais</asp:LinkButton></div></td></tr><tr><td align="right"><asp:Button ID="btnRenovarCertificado" runat="server" CssClass="Button" Text="Renovar" Width="170px" OnClick="btnRenovarCertificado_Click" OnInit="btnRenovarCertificado_Init"
                                                                                            OnPreRender="btnRenovarCertificado_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova a data de vencimento do Certificado de Regularidade por um novo período')" /></td></tr><tr><td>&#160;&#160; </td></tr></table></ContentTemplate></asp:TabPanel></asp:TabContainer>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                            </asp:MultiView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="trvProcessos" EventName="SelectedNodeChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="lkbOpcoesExcluirCTF" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomicos" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="dtlEstadual" EventName="EditCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="dtlFederal" EventName="EditCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="dtlMunicipal" EventName="EditCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="dtlOutros" EventName="EditCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="dtlCTF" EventName="EditCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="lkbCondicionantesPadroes" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="grvCondicionantes" EventName="RowDeleting" />
                                            <asp:AsyncPostBackTrigger ControlID="grvOutros" EventName="RowDeleting" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="ZerarClear">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Barra">
    Meio Ambiente
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="popups">
    <div id="divPopUpRenovacao" style="width: 596px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div>
            <div id="fecharRenovacao" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                RENOVAÇÃO</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upRenovacao" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" style="width: 30%">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDataDias" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDiasValidadeRenovacao" runat="server" CssClass="TextBox" Width="100px"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data do próximo vencimento')"></asp:TextBox>
                                    <asp:TextBox ID="tbxDataValidadeRenovacao" runat="server" CssClass="TextBox" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Data do próximo vencimento')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataValidadeRenovacao_CalendarExtender" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="tbxDataValidadeRenovacao">
                                    </asp:CalendarExtender>
                                    <asp:Button ID="btnSalvarRenovacao" runat="server" CssClass="Button" OnClick="btnSalvarRenovacao_Click"
                                        Text="Renovar" Width="170px" OnInit="btnSalvarRenovacao_Init" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Renova o vencimento')" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Status:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatusRenovacao" runat="server" CssClass="DropDownList"
                                        DataTextField="Nome" DataValueField="Id" Width="170px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckbUtilizarUltimasNotificacoes" runat="server" Checked="True" Text="Utilizar dias de notificações iguais ao anterior"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cria automaticamente as mesmas notificações do vencimento anterior, levando em consideração a nova data de vencimento')" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                    <asp:HiddenField ID="hfTipoRenovacao" runat="server" />
                                    <asp:HiddenField ID="hfIdItemRenovacao" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="popup_Processo" style="width: 650px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div>
            <div id="fecharProcesso" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                PROCESSO</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopProcesso" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    Empresa:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEmpresaProcesso" runat="server" CssClass="DropDownList" DataTextField="Nome" DataValueField="Id" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    Tipo de Orgão Ambiental:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoOrgaoProcesso" runat="server" CssClass="DropDownList" DataTextField="Nome" DataValueField="Id" 
                                        OnSelectedIndexChanged="ddlTipoOrgaoProcesso_SelectedIndexChanged" Width="250px" AutoPostBack="True">
                                        <asp:ListItem Value="0">Estadual</asp:ListItem>
                                        <asp:ListItem Value="1">Federal</asp:ListItem>
                                        <asp:ListItem Value="2">Municipal</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <div id="divEstadoProcesso" runat="server" visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    Estado:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlEstadoProcesso" runat="server" CssClass="DropDownList" DataTextField="Nome"
                                                        DataValueField="Id" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlEstadoLicenca_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divCidadeProcesso" runat="server" visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    Cidade:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlCidadeProcesso" runat="server" CssClass="DropDownList" DataTextField="Nome"
                                                        DataValueField="Id" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlCidadeProcesso_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    Orgão Ambiental:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlOrgaoProcesso" runat="server" CssClass="DropDownList" DataTextField="Nome"
                                        DataValueField="Id" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Consultora:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlConsultoraProcesso" runat="server" CssClass="DropDownList"
                                        DataTextField="Nome" DataValueField="Id" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Numero:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxNumeroProcesso" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Data de Abertura:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataAberturaProcesso" runat="server" CssClass="TextBox" MaxLength="10"
                                        Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataAberturaProcesso_CalendarExtender" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="tbxDataAberturaProcesso">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp; Detalhamento:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxObservacaoProcesso" runat="server" CssClass="TextBox" Height="56px"
                                        TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btnAbrirContratos" runat="server" CssClass="Button" 
                                        onclick="btnAbrirContratos_Click" oninit="btnAbrirContratos_Init" 
                                        Text="Contratos" Visible="False" Width="170px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="HfIdProcesso" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                    <asp:Button ID="btnSalvarProcesso" runat="server" CssClass="Button" OnClick="btnSalvarProcesso_Click"
                                        OnInit="btnSalvarProcesso_Init" Text="Salvar" Width="170px" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Salva o novo processo ou altera os dados do processo carregado')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpContratos" style="width: 700px; display: block; top: 0px;
        left: 0px;" class="pop_up_super">
        <div id="CancelarPopUpContrato" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Contratos</div>
        <div>
            <div>
                <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                      
                                          <asp:UpdatePanel ID="UPListagemContratosDiversos" runat="server" 
                                              UpdateMode="Conditional">
                                           <ContentTemplate>
                                             <asp:GridView ID="gvContratos" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="None" OnRowDeleting="gvContratos_RowDeleting"
                                            PageSize="4" Width="100%" OnPageIndexChanging="gvContratos_PageIndexChanging"
                                            OnInit="gvContratos_Init">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
 
                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                                <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                <asp:TemplateField HeaderText="Objeto">
                                                  <ItemTemplate>
                                                    <%# bindingObjeto(Container.DataItem) %>
                                                  </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                   <ItemTemplate>
                                                    <%# bindingStatusContrato(Container.DataItem) %>
                                                  </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DataAbertura" DataFormatString="{0:d}" 
                                                    HeaderText="Data de Abertura">
                                                <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField> 
                                                <asp:TemplateField>
                                                  <ItemTemplate>
                                                          <asp:ImageButton ID="ibtnExcluirContratos" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluirContratos_PreRender" ToolTip="Excluir" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                      Excluir
                                                    </HeaderTemplate>
                                                    <HeaderStyle Width="45px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                        <asp:Label ID="lblQuantidadeContratosProcesso" runat="server"></asp:Label>
                                           </ContentTemplate>
                                          </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSelecionarMaisContratos" runat="server" CssClass="Button" OnClick="btnSelecionarMaisContratos_Click"
                                        Text="Selecionar Mais" Width="170px" OnInit="btnSelecionarMaisContratos_Init" />
                                </td>
                            </tr>
                        </table>   
            </div>
        </div>
    </div>
    <div id="divPopUpSelecaoContratos" style="width: 700px; display: block; top: 0px;
        left: 0px;" class="pop_up_super_super_super">
        <div id="fecharSelecaoContratos" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Seleção de Contratos</div>
        <div>
            <div>
                <asp:UpdatePanel ID="UPSelecaoContratos" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="False">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="35%">
                                    Objeto:
                                </td>
                                <td width="65%">
                                    <asp:TextBox ID="tbxObjetoContratoDiverso" runat="server" CssClass="TextBox"
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="35%">
                                    Número:
                                </td>
                                <td width="65%">
                                    <asp:TextBox ID="tbxNumeroContratoDiverso" runat="server" CssClass="TextBox"
                                        Width="200px"></asp:TextBox>
                                    <asp:LinkButton ID="lxbPesquisarContratos" runat="server" OnClick="lxbPesquisarContratos_Click"><img alt="" src="../imagens/visualizar20x20.png" style="border:0px"/> Pesquisar</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="gdvContratosSelecao" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="None"
                                            PageSize="4" Width="100%" OnPageIndexChanging="gdvContratosSelecao_PageIndexChanging"
                                            OnInit="gdvContratosSelecao_Init">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarContratosDiversos" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        Sel.<input id="ckbSelecionar1" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarContratosDiversos')" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle Width="45px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                <asp:TemplateField HeaderText="Objeto">
                                                  <ItemTemplate>
                                                    <%# bindingObjeto(Container.DataItem) %>
                                                  </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                   <ItemTemplate>
                                                    <%# bindingStatusContrato(Container.DataItem) %>
                                                  </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DataAbertura" DataFormatString="{0:d}" 
                                                    HeaderText="Data de Abertura">
                                                <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>                                                
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                        <asp:Label ID="lblSemContratos" runat="server" Text="0 contrato(s) encontrados" Visible="false"></asp:Label>                                        
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSalvarContratoDiverso" runat="server" 
                                        CssClass="Button" OnClick="btnSalvarContratoDiverso_Click" 
                                        OnInit="btnSalvarContratoDiverso_Init" Text="Salvar" Width="170px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>                        
                        <asp:AsyncPostBackTrigger ControlID="lxbPesquisarContratos" 
                            EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="popup_Condicionante" style="width: 770px; display: block" class="pop_up">
        <div>
            <div class="barra_titulo">
                CONDICIONANTE</div>
            <div id="fecharCondicionante" class="btn_cancelar_popup">
            </div>
            <asp:UpdatePanel ID="upCamposCondicionante" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right" style="width: 30%">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Licença:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLicencaCondicionante" runat="server" CssClass="DropDownList"
                                    Width="300px" DataTextField="Numero" DataValueField="Id">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Status:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatusCondicionante" runat="server" CssClass="DropDownList"
                                    Width="300px" DataTextField="Nome" DataValueField="Id" AutoPostBack="True" OnInit="ddlStatusCondicionante_Init"
                                    OnSelectedIndexChanged="ddlStatusCondicionante_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Número da Condicionante:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxNumeroCondicionante" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%">
                                Descrição:&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescricaoCondicionante" runat="server" CssClass="TextBox" Width="450px"
                                    MaxLength="1000"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Prazo para Atendimento (dias):
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDiasPrazoCondicionante" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td>
                                <asp:LinkButton ID="lkbProrrogacaoCondicionante" runat="server" Font-Size="8pt" OnClick="lkbProrrogacao_Click"
                                    OnClientClick="tooltip.hide();" OnInit="lkbProrrogacao_Init" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Visualiza o vencimento atual e todas as suas renovações anteriores')">Abrir Prorrogações</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Data de Atendimento:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDataAtendimentoCondicionante" runat="server" CssClass="TextBox"
                                    Width="100px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quando o vencimento é atendido')"></asp:TextBox>
                                <asp:CalendarExtender ID="tbxDataAtendimentoCondicionante_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataAtendimentoCondicionante">
                                </asp:CalendarExtender>
                                &nbsp;Protocolo de Atendimento:
                                <asp:TextBox ID="tbxProtocoloCondicionante" runat="server" CssClass="TextBox" Width="100px"
                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Número do Protocolo de atendimento')"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Arquivo:
                            </td>
                            <td>
                                <asp:Button ID="btnUploadCondicionante" runat="server" CssClass="ButtonUpload" Height="22px"
                                    Width="170px" Text="    Inserir / Visualizar Anexos" 
                                    onmouseout="tooltip.hide();" 
                                    onmouseover="tooltip.show('Anexa arquivos ao vencimento')" OnClick="btnUploadCondicionante_Click" OnInit="btnUploadCondicionante_Init" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Detalhamento:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxObservacoesCondicionante" runat="server" CssClass="TextBox" Height="37px"
                                    TextMode="MultiLine" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="cbxPeriodicaCondicionante" runat="server" Text="Periodica" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Uma condicionante periódica pode ser renovada quantas vezes for necessário após cada cumprimento.')" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td>
                                <asp:LinkButton ID="lkbVencimentoCondicionante" runat="server" Font-Size="8pt" OnClick="lkbVencimentoCondicionante_Click"
                                    OnInit="lkbVencimentoCondicionante_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o vencimento atual e todas as suas renovações anteriores')">Visualizar Vencimentos</asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="lkbObservacoesCondicionante" runat="server" Font-Size="8pt" OnClick="lkbObservacoesCondicionante_Click"
                                    OnInit="lkbObservacoes_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <strong>
                                    <asp:ImageButton ID="ibtnAddNotificacaoCondicionante" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                        OnClick="ibtnAddNotificacaoCondicionante_Click" OnInit="ibtnAddNotificacaoCondicionante_Init"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de aviso de vencimento por e-mail')" />
                                    Notificação:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <div class="container_over_hide_table">
                                <div class="container_grids">
                                    <asp:GridView ID="grvNotificacaoCondicionante" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                        GridLines="None" OnRowDeleting="grvNotificacaoCondicionante_RowDeleting" Width="100%"
                                        AllowPaging="True" OnPageIndexChanging="grvNotificacaoCondicionante_PageIndexChanging"
                                        PageSize="2">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                            <asp:TemplateField HeaderText="E-mails" ItemStyle-CssClass="td_style_display">
                                                <ItemTemplate>
                                                    <div class="td_scroll_teste">
                                                        <%# DataBinder.Eval(Container.DataItem,"Emails") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Excluir">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnExcluir6" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                        OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Excluir
                                                </HeaderTemplate>
                                                <HeaderStyle Width="45px" />
                                                <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Strikeout="False" Font-Underline="False" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </div>
                                    </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:HiddenField ID="hfIdCondicionante" runat="server" />
                                &nbsp;<asp:Button ID="btnSalvarCondicionante" runat="server" CssClass="Button" OnClick="btnSalvarCondicionante_Click"
                                    Text="Salvar" Width="170px" OnInit="btnSalvarCondicionante_Init" onmouseout="tooltip.hide();"
                                    onmouseover="tooltip.show('Salva os dados da condicionante')" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="popProrrogacao" style="width: 866px; display: block; top: 0px; left: 0px;"
        class="pop_up_super_super">
        <div>
            <div id="fecharProrrogacao" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Prorrogação de Prazo</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upProrrogacoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="ibtnAddObs">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" width="20%" style="width: 80%">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    Prorrogação (dias):
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="tbxPrazoAdicional" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    Data de Protocolo:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="tbxDataProtocoloAdd" runat="server" CssClass="TextBox" onmouseout="tooltip.hide();"
                                                        onmouseover="tooltip.show('Data em que foi protocolado o pedido de prorrogação de prazo')"
                                                        Width="100px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="tbxDataProtocoloAdicional_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataProtocoloAdd">
                                                    </asp:CalendarExtender>
                                                    &nbsp;Protocolo de Prorrogação:
                                                    <asp:TextBox ID="tbxProtocoloAdicional" runat="server" CssClass="TextBox" onmouseout="tooltip.hide();"
                                                        onmouseover="tooltip.show('Número do Protocolo de pedido de prorrogação de prazo')"
                                                        Width="100px"></asp:TextBox>&nbsp; <strong>
                                                            <asp:Button ID="btnAddProrrogacao" runat="server" Text="Salvar" CssClass="Button"
                                                                OnClick="btnAddProrrogacao_Click" OnInit="btnAddProrrogacao_Init" />
                                                        </strong>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>Histórico:</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <div class="container_grids" style="height: 200px; overflow: auto;">
                                            <asp:GridView ID="grvProrrogacoes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                                PageSize="3" Width="100%" OnRowDeleting="grvProrrogacoes_RowDeleting">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="DataProtocoloAdicional" HeaderText="Data do Protocolo"
                                                        DataFormatString="{0:d}" />
                                                    <asp:BoundField DataField="PrazoAdicional" HeaderText="Dias de Prorrogação" />
                                                    <asp:TemplateField HeaderText="Nova Data de Vencimento">
                                                        <ItemTemplate>
                                                            <%# bindNovaData(Container.DataItem) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProtocoloAdicional" HeaderText="Protocolo" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibtnExcluir6" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" OnInit="ibtnExcluir6_Init" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            Excluir
                                                        </HeaderTemplate>
                                                        <HeaderStyle Width="45px" />
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
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle">
                                        <asp:HiddenField ID="hfTipoProrrogacao" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle">
                                        <asp:Button ID="btnFechar" runat="server" CssClass="Button" Text="Fechar" Width="220px"
                                            OnClick="btnFechar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAddProrrogacao" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="popup_licenca" style="width: 770px; display: block" class="pop_up">
        <div>
            <div class="barra_titulo">
                LICENÇA</div>
            <div id="fecharLicenca" class="btn_cancelar_popup">
            </div>
            <asp:UpdatePanel ID="upLicenca" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%">
                                Tipo de Licença:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlTipoLicenca" runat="server" CssClass="DropDownList" DataTextField="Nome"
                                    DataValueField="Id" Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%">
                                Processo:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProcessoLicenca" runat="server" CssClass="DropDownList"
                                    DataTextField="Numero" DataValueField="Id" Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%">
                                Atividade:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDescricaoLicenca" runat="server" CssClass="TextBox" Width="98%"
                                    MaxLength="250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Número da Licença:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxNumeroLicenca" runat="server" CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Dias de Validade:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDiasValidadeLicenca" runat="server" CssClass="TextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Data de Retirada:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxDataRetiradaLicenca" runat="server" CssClass="TextBox" MaxLength="10"
                                    Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="tbxDataRetiradaLicenca_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataRetiradaLicenca">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Estado:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLicencaCadastroEstado" runat="server" CssClass="DropDownList"
                                    OnSelectedIndexChanged="ddlLicencaCadastroEstado_SelectedIndexChanged" Width="250px"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Cidade:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLicencaCadastroCidade" runat="server" Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Arquivos:
                            </td>
                            <td>
                                <asp:Button ID="btnUploadLicenca" runat="server" CssClass="ButtonUpload" Height="22px"
                                    Width="170px" Text="     Inserir / Visualizar Anexos" OnClick="btnUploadLicenca_Click" OnInit="btnUploadLicenca_Init" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td>
                                <asp:LinkButton ID="lkbVencimentoLicenca" runat="server" Font-Size="8pt" OnClick="lkbVencimentoLicenca_Click"
                                    OnInit="lkbVencimentoLicenca_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o vencimento atual e todas as suas renovações anteriores')">Visualizar Vencimentos</asp:LinkButton>                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <strong>
                                    <asp:ImageButton ID="ibtnAddNotificacaoLicenca" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                        OnClick="ibtnAddNotificacaoLicenca_Click" OnInit="ibtnAddNotificacaoLicenca_Init"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de aviso de vencimento por e-mail')" />
                                    Notificação:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <div class="container_over_hide_table">
                                <div class="container_grids">
                                    <asp:GridView ID="grvNotificacaoLicenca" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                        GridLines="None" OnRowDeleting="grvNotificacaoLicenca_RowDeleting" PageSize="2"
                                        Width="100%" AllowPaging="True" OnPageIndexChanging="grvNotificacaoLicenca_PageIndexChanging">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />                                           
                                            <asp:TemplateField HeaderText="E-mails" ItemStyle-CssClass="td_style_display">
                                                <ItemTemplate>
                                                    <div class="td_scroll_teste">
                                                        <%# DataBinder.Eval(Container.DataItem,"Emails") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Excluir">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnExcluir7" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                        OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Excluir
                                                </HeaderTemplate>
                                                <HeaderStyle Width="45px" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </div>
                                    </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:HiddenField ID="HfIdLicenca" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                &nbsp;
                                <asp:Button ID="btnSalvarLicenca" runat="server" CssClass="Button" Text="Salvar"
                                    Width="171px" OnClick="btnSalvarLicenca_Click" OnInit="btnSalvarLicenca_Init"
                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cria a nova licença ou altera os dados da licença carregada')" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="divPopUpOutros" style="width: 770px; display: block" class="pop_up">
        <div>
            <div id="fecharOutros" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                OUTROS
            </div>
            <div>
                <div>
                    <asp:UpdatePanel ID="upPopUpOutros" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rbtnProcessoOutros" runat="server" AutoPostBack="True" GroupName="Tipo"
                                            OnCheckedChanged="rbtnProcessoOutros_CheckedChanged" Text="Processo" Visible="False" />
                                        <asp:RadioButton ID="rbtnGeralOutros" runat="server" AutoPostBack="True" Checked="True"
                                            GroupName="Tipo" OnCheckedChanged="rbtnGeralOutros_CheckedChanged" Text="Geral"
                                            Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Status:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatusOutros" runat="server" CssClass="DropDownList" Width="300px"
                                            AutoPostBack="True" OnInit="ddlStatusOutros_Init" OnSelectedIndexChanged="ddlStatusOutros_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:MultiView ID="mvOutros" runat="server">
                                            <asp:View ID="vwProcOutros" runat="server">
                                                <div id="divTipoProcesso">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="right" style="width: 30%">
                                                                Empresa:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlEmpresaOutros" runat="server" AutoPostBack="True" CssClass="DropDownList"
                                                                    OnSelectedIndexChanged="ddlEmpresaOutros_SelectedIndexChanged" Width="300px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 30%">
                                                                Orgão Ambiental:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlOrgaoOutros" runat="server" AutoPostBack="True" CssClass="DropDownList"
                                                                    OnSelectedIndexChanged="ddlOrgaoOutros_SelectedIndexChanged" Width="300px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 30%">
                                                                Processo:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlProcessoOutros" runat="server" AutoPostBack="True" CssClass="DropDownList"
                                                                    Width="300px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwGeralOutros" runat="server">
                                                <div id="div1">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="right" style="width: 30%">
                                                                Empresa:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlEmpresaGeralOutros" runat="server" CssClass="DropDownList"
                                                                    Width="300px" OnSelectedIndexChanged="ddlEmpresaGeralOutros_SelectedIndexChanged"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 30%">
                                                                Orgão Ambiental:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlOrgaoGeralOutros" runat="server" CssClass="DropDownList"
                                                                    Width="300px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" style="width: 30%">
                                                                Consultoria:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlConsultoriaOutros" runat="server" CssClass="DropDownList"
                                                                    Width="300px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style2">
                                        Descrição:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbxDescricaoOutros" runat="server" CssClass="TextBox" Width="511px"
                                            MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Data Abertura/Recebimento:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbxDataRecebimentoOutros" runat="server" CssClass="TextBox" MaxLength="10"
                                            Width="100px"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxDataRecebimentoOutros_CalendarExtender" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="tbxDataRecebimentoOutros">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Dias de Validade:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbxDiasValidadeOutros" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lkbProrrogacaoOutros" runat="server" Font-Size="8pt" OnClick="lkbProrrogacaoOutros_Click"
                                            OnClientClick="tooltip.hide();" OnInit="lkbProrrogacao_Init" onmouseout="tooltip.hide();"
                                            onmouseover="tooltip.show('Visualiza o vencimento atual e todas as suas renovações anteriores')">Abrir Prorrogações</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Data de Atendimento:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbxDataProtocoloOutros" runat="server" CssClass="TextBox" Width="100px"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quando o vencimento é atendido')"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxDataProtocoloOutros_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataProtocoloOutros">
                                        </asp:CalendarExtender>
                                        &nbsp;Protocolo de Atendimento:
                                        <asp:TextBox ID="tbxProtocoloOutros" runat="server" CssClass="TextBox" Width="100px"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Número do Protocolo de atendimento')"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Detalhamento:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbxObservacoesOutros" runat="server" CssClass="TextBox" Height="32px"
                                            TextMode="MultiLine" Width="437px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Arquivos:
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUploadOutros" runat="server" CssClass="ButtonUpload" Height="22px"
                                            Width="170px" Text="    Inserir / Visualizar Anexos" 
                                            onmouseout="tooltip.hide();" 
                                            onmouseover="tooltip.show('Anexa arquivos ao vencimento')" OnClick="btnUploadOutros_Click" OnInit="btnUploadOutros_Init" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbxPeriodicaOutros" runat="server" AutoPostBack="True" OnCheckedChanged="cbxPeriodicaOutros_CheckedChanged"
                                            Text="Periodica" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Um vencimento periódico pode ser renovado quantas vezes for necessário após cada cumprimento')" />
                                        &nbsp;
                                        <asp:LinkButton ID="lkbVencimentoOutros" runat="server" Font-Size="8pt" OnClick="lkbVencimentoOutros_Click"
                                            OnInit="lkbVencimentoOutros_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o vencimento atual e todas as suas renovações anteriores')">Visualizar Vencimentos</asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="lkbObservacoesOutros" runat="server" Font-Size="8pt" OnClick="lkbObservacoesOutros_Click"
                                            OnInit="lkbObservacoes_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <strong>
                                            <asp:ImageButton ID="ibtnAddNotificacaoOutros" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                OnClick="ibtnAddNotificacaoOutros_Click" OnInit="ibtnAddNotificacaoOutros_Init"
                                                onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de aviso de vencimento por e-mail')" />
                                            Notificação:</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <div class="container_over_hide_table">
                                        <div class="container_grids">
                                            <asp:GridView ID="grvNotificacaoOutros" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                GridLines="None" OnRowDeleting="grvNotificacaoOutros_RowDeleting" Width="100%"
                                                AllowPaging="True" OnPageIndexChanging="grvNotificacaoOutros_PageIndexChanging"
                                                PageSize="2">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                    <asp:TemplateField HeaderText="E-mails" ItemStyle-CssClass="td_style_display">
                                                <ItemTemplate>
                                                    <div class="td_scroll_teste">
                                                        <%# DataBinder.Eval(Container.DataItem,"Emails") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Excluir">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibtnExcluir3213" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            Excluir
                                                        </HeaderTemplate>
                                                        <HeaderStyle Width="45px" />
                                                        <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                            Font-Strikeout="False" Font-Underline="False" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </div>
                                            </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnSalvarOutros" runat="server" CssClass="Button" OnClick="btnSalvarOutros_Click"
                                            OnInit="btnSalvarOutros_Init" Text="Salvar" Width="170px" onmouseout="tooltip.hide();"
                                            onmouseover="tooltip.show('Salva os dados do vencimento')" />
                                        <asp:HiddenField ID="hfArquivoAtendimentoOutros" runat="server" />
                                        <asp:HiddenField ID="hfArquivoProrrogacaoOutros" runat="server" />
                                        <asp:HiddenField ID="hfIdOutros" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEmpresaOutros" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlOrgaoOutros" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="rbtnProcessoOutros" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="rbtnGeralOutros" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div id="popup_notificacoes" style="width: 867px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div>
            <div id="fecharNotificacoes" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Notificações</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopNotificacoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 25%">
                                    <asp:Label ID="Label2" runat="server" Text="Dias de aviso:" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Dias de antecedência com que será avisado do vencimento')"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="cblDias" runat="server" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>E-mails para envio:</strong>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <hr style="height: -12px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left" width="33%">
                                                <input id="chkMarcarEmailsGrupo" type="checkbox" class="chkMarcarEmailsGrupo" checked="checked" />&nbsp;
                                                <strong>Grupo Econômico: </strong>
                                            </td>
                                            <td align="left" width="33%" valign="top">
                                                <input id="chkMarcarEmailsEmpresa" type="checkbox" class="chkMarcarEmailsEmpresa"
                                                    checked="checked" />&nbsp; <strong>Empresa:</strong>
                                            </td>
                                            <td align="left" width="33%">
                                                <input id="chkMarcarEmailsConsultora" type="checkbox" class="chkMarcarEmailsConsultora"
                                                    checked="checked" />&nbsp; <strong>Consultoria: </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="chkGruposEconomicos" runat="server" CssClass="chkEmailsGruposEconomicosCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td align="left">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="chkEmpresas" runat="server" CssClass="chkEmailsEmpresasCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td align="left" width="44%">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="chkCOnsultoras" runat="server" CssClass="chkEmailsConsultorasCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <hr style="height: -12px" />
                                    <strong>Outros (separar com ;)</strong>
                                    <asp:TextBox ID="tbxOutrosEmails" runat="server" CssClass="TextBox" Height="56px"
                                        TextMode="MultiLine" Width="100%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="hfIdNotificacao" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                    <asp:Button ID="btnSalvarNotificacao" runat="server" CssClass="Button" Text="Salvar"
                                        Width="170px" OnClick="btnSalvarNotificacao_Click" OnInit="btnSalvarNotificacao_Init"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva as notificações com os dias e os e-mails selecionados')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpCadastroTecnicoFederal" style="width: 650px; display: block; top: 0px;
        left: 0px;" class="pop_up">
        <div>
            <div id="fecharCadastroTecnicoFederal" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                CADASTRO TÉCNICO FEDERAL
            </div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopCadastroTecnicoFederal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    Empresa:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEmpresaCTF" runat="server" CssClass="DropDownList" DataTextField="Nome"
                                        DataValueField="Id" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpresaCTF_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 30%">
                                    Consultora:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlConsultoria" runat="server" CssClass="DropDownList" Width="170px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Senha:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxSenhaCTF" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Atividades:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxAtividadesCTF" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Número da Licença:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxNumeroLicencaCTF" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Validade da Licença:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataValidadeLicenca" runat="server" CssClass="TextBox" MaxLength="10"
                                        Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataValidadeLicenca_CalendarExtender" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="tbxDataValidadeLicenca">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp; Detalhamento:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxObservacaoCTF" runat="server" CssClass="TextBox" Width="419px"
                                        MaxLength="250"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;Arquivos:
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:Button ID="bntUploadCTF" runat="server" CssClass="ButtonUpload" Height="22px"
                                        Text="    Inserir / Visualizar Anexos" Width="170px" OnClick="bntUploadCTF_Click" OnInit="bntUploadCTF_Init" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbObservacoes" runat="server" Font-Size="8pt" OnClick="lkbObservacoes_Click"
                                        OnInit="lkbObservacoes_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TabContainer ID="tcCTF" runat="server" ActiveTabIndex="0" Width="100%" Height="123px"
                                        ScrollBars="Auto">
                                        <asp:TabPanel ID="tp1CTF" runat="server" HeaderText="Validade">
                                            <HeaderTemplate>Entrega do Relatório Anual</HeaderTemplate>
                                            <ContentTemplate>
                                                <div style="width: 100%; height: 10px;"></div>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="right" style="width: 30%">Data limite para a Entrega: </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbxDataEntregaRelatorioCTF" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                                            <asp:CalendarExtender ID="tbxDataEntregaRelatorioCTF_CalendarExtender" runat="server" Enabled="True"
                                                                Format="dd/MM/yyyy" TargetControlID="tbxDataEntregaRelatorioCTF"></asp:CalendarExtender>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 30%">Status: </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlEstatusRelatorioAnual" runat="server" CssClass="DropDownList"  DataTextField="Nome" DataValueField="Id" Width="170px">

                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 30%">&nbsp;&nbsp;</td>
                                                        <td align="left">
                                                            <asp:LinkButton ID="lkbVencimentoRelatorioAnual" runat="server" Font-Size="8pt" OnInit="lkbVencimentoOutros_Init"
                                                                OnClick="lkbVencimentoRelatorioAnual_Click">Visualizar Vencimentos</asp:LinkButton>

                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnAddNotificacaoRelatCTF" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                    OnClick="ibtnAddNotificacaoRelatorioCTF_Click" OnInit="ibtnAddNotificacaoRelatorioCTF_Init"
                                                                    Style="height: 19px" />Notificação: 
                                                            </strong>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <div class="container_over_hide_table">
                                                            <div class="container_grids">
                                                                <asp:GridView ID="grvNotificacaoRelatorioCTF" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" OnRowDeleting="grvNotificacaoRelatorioCTF_RowDeleting" Width="100%"
                                                                    AllowPaging="True" OnPageIndexChanging="grvNotificacaoRelatorioCTF_PageIndexChanging"
                                                                    PageSize="3">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                        <asp:TemplateField HeaderText="E-mails" ItemStyle-CssClass="td_style_display">
                                                                            <ItemTemplate>
                                                                                <div class="td_scroll_teste">
                                                                                    <%# DataBinder.Eval(Container.DataItem,"Emails") %>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <HeaderTemplate>
                                                                                Excluir
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                               <asp:ImageButton ID="ibtnExcluir" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"  OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="45px" />
                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </div>
                                                                </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="tp2CTF" runat="server" HeaderText="Pagamento da Taxa Trimestral">
                                            <HeaderTemplate>Pagamento da Taxa Trimestral</HeaderTemplate>
                                            <ContentTemplate>
                                                <div style="width: 100%; height: 10px;"></div>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="right" style="width: 30%">Data de Vencimento: </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbxDataPagamentoCTF" runat="server" CssClass="TextBox" Width="100px">
                                                            </asp:TextBox>
                                                            <asp:CalendarExtender ID="tbxDataPagamentoCTF_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="tbxDataPagamentoCTF" Enabled="True">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 30%">Status:</td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlEstatusTaxaTrimestral" runat="server" CssClass="DropDownList" DataTextField="Nome" DataValueField="Id" Width="170px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 30%">&nbsp;&nbsp;</td>
                                                        <td align="left">
                                                            <asp:LinkButton ID="lkbVencimentoTaxaTrimestral" runat="server" Font-Size="8pt" OnInit="lkbVencimentoOutros_Init"
                                                                OnClick="lkbVencimentoTaxaTrimestral_Click">Visualizar Vencimentos</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnAddNotificacaoPagamentoCTF" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                    OnClick="ibtnAddNotificacaoPagamentoCTF_Click" OnInit="ibtnAddNotificacaoPagamentoCTF_Init" />Notificação: 
                                                            </strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <div class="container_grids">
                                                                <asp:GridView ID="grvNotificacaoPagamentoCTF" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" OnRowDeleting="grvNotificacaoPagamentoCTF_RowDeleting" Width="100%"
                                                                    AllowPaging="True" OnPageIndexChanging="grvNotificacaoPagamentoCTF_PageIndexChanging"
                                                                    PageSize="4">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                        <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <HeaderTemplate>
                                                                                <asp:ImageButton ID="ibtnExcluir4" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif" OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                                                                <input id="Checkbox4" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacaoTaxaCTF')" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacaoTaxaCTF" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Width="45px" />
                                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                                Font-Underline="False" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="tp3CTF" runat="server" HeaderText="Certificado de Regularidade">
                                            <HeaderTemplate>Certificado de Regularidade</HeaderTemplate>
                                            <ContentTemplate>
                                                <div style="width: 100%; height: 10px;"></div>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="right" style="width: 30%">Data da Vencimento: </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbxDataEntregaCertificadoCTF" runat="server" CssClass="TextBox"
                                                                onmouseout="tooltip.hide();" onmouseover="tooltip.show('Este certificado é valido por 3 meses, a partir da data de sua emissão.')"
                                                                Width="100px">
                                                            </asp:TextBox>
                                                            <asp:CalendarExtender ID="tbxDataEntregaCertificadoCTF_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataEntregaCertificadoCTF"
                                                                    Enabled="True">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 30%">Status: </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlEstatusCertificado" runat="server" CssClass="DropDownList"
                                                                DataTextField="Nome" DataValueField="Id" Width="170px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width: 30%">&nbsp;&nbsp;</td>
                                                        <td align="left">
                                                            <asp:LinkButton ID="lkbVencimentoCertificadoRegularidade" runat="server" Font-Size="8pt"
                                                                OnInit="lkbVencimentoOutros_Init" OnClick="lkbVencimentoCertificadoRegularidade_Click">Visualizar Vencimentos</asp:LinkButton>

                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnAddNotificacaoCertificadoCTF" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                    OnClick="ibtnAddNotificacaoCertificadoCTF_Click" OnInit="ibtnAddNotificacaoCertificadoCTF_Init"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de aviso de vencimento por e-mail')" />Notificação: 

                                                            </strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <div class="container_grids">
                                                                <asp:GridView ID="grvNotificacaoCertificadoCTF" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" OnRowDeleting="grvNotificacaoCertificadoCTF_RowDeleting" Width="100%"
                                                                    AllowPaging="True" OnPageIndexChanging="grvNotificacaoCertificadoCTF_PageIndexChanging"
                                                                    PageSize="4">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                        <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <HeaderTemplate>
                                                                                <asp:ImageButton ID="ibtnExcluir5" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                    OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                                                                <input id="Checkbox5" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacaoCertificadoCTF')" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacaoCertificadoCTF" />
                                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    </asp:TabContainer>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="hfTipoVencimentoCTF" runat="server" />
                                    <asp:HiddenField ID="HfIdCTF" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                    <asp:Button ID="btnSalvarCTF" runat="server" CssClass="Button" Text="Salvar" Width="170px"
                                        OnClick="btnSalvarCTF_Click" OnInit="btnSalvarCTF_Init" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Cria o novo Cadastro Técnico Federal ou altera o Cadastro Técnico Federal carregado')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpVencimentos" style="width: 697px; display: block; top: 0px; left: 0px;"
        class="pop_up_super_super">
        <div>
            <div id="fecharVencimento" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Vencimentos</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upVencimentos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" style="width: 30%">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vencimentos:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVencimentos" runat="server" AutoPostBack="True" CssClass="DropDownList"
                                        Width="170px" DataTextField="Data" DataTextFormatString="{0:d}" DataValueField="Id"
                                        OnSelectedIndexChanged="ddlVencimentos_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="ibtnRemoverVencimento" runat="server" ImageUrl="~/imagens/excluir.gif"
                                        OnClick="ibtnRemoverVencimento_Click" ToolTip="Remover Vencimento" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Status:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatusVencimentos" runat="server" CssClass="DropDownList"
                                        Width="170px" DataTextField="Nome" DataValueField="Id">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>Notificações:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_over_hide_table">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvNotificacaoVencimentos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" PageSize="3" Width="100%" OnPageIndexChanging="grvNotificacaoVencimentos_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                                <asp:TemplateField HeaderText="E-mails" ItemStyle-CssClass="td_style_display">
                                                <ItemTemplate>
                                                    <div class="td_scroll_teste">
                                                        <%# DataBinder.Eval(Container.DataItem,"Emails") %>
                                                    </div>
                                                </ItemTemplate>
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
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="hfIDVencimento" runat="server" />
                                    <asp:HiddenField ID="hfTypeVencimento" runat="server" />
                                    <asp:Button ID="btnSalvarVencimento" runat="server" CssClass="Button" OnClick="btnSalvarVencimento_Click"
                                        Text="Salvar" Width="170px" OnInit="btnSalvarVencimento_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpNotificacoes" style="width: 866px; display: block; top: 0px; left: 0px;"
        class="pop_up_super_super">
        <div>
            <div id="cancelNots" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Notificações</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="UpdatePanelNot" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" width="40%">
                                    Vencimento:
                                </td>
                                <td width="60%">
                                    <asp:Label ID="lblDataVencimento" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>Notificações:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_over_hide_table">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvNotificacoes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                            PageSize="3" Width="100%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                                <asp:TemplateField HeaderText="E-mails" ItemStyle-CssClass="td_style_display">
                                                <ItemTemplate>
                                                    <div class="td_scroll_teste">
                                                        <%# DataBinder.Eval(Container.DataItem,"Emails") %>
                                                    </div>
                                                </ItemTemplate>
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
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpObs" style="width: 866px; display: block; top: 0px; left: 0px;"
        class="pop_up_super_super">
        <div>
            <div id="fecharObs" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Registros em Históricos</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upObs" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlObservacoes" runat="server" DefaultButton="ibtnAddObs">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" width="20%">
                                        Título:
                                    </td>
                                    <td width="60%">
                                        <asp:TextBox ID="tbxTituloObs" runat="server" CssClass="TextBox" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="20%">
                                        Descrição:
                                    </td>
                                    <td width="60%">
                                        <asp:TextBox ID="tbxObservacaoObs" runat="server" CssClass="TextBox" Width="90%"
                                            Height="45px" TextMode="MultiLine"></asp:TextBox>
                                        <strong>
                                            <asp:ImageButton ID="ibtnAddObs" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                OnClick="ibtnAddObs_Click" />
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <strong>Histórico:</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <div class="container_grids" style="height: 200px; overflow: auto;">
                                            <asp:GridView ID="grvHistoricos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                                PageSize="3" Width="100%">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="DataPublicacao" HeaderText="Data do Registro" />
                                                    <asp:BoundField DataField="Alteracao" HeaderText="Título" />
                                                    <asp:BoundField DataField="Observacao" HeaderText="Descrição" />
                                                </Columns>
                                                <EditRowStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnEnviarHistoricoGeral" runat="server" Text="Enviar Histórico Por Email"
                                            CssClass="Button" OnClick="btnEnviarHistoricoGeral_Click" OnInit="btnEnviarHistoricoGeral_Init" />
                                        <asp:HiddenField ID="hfIDObs" runat="server" />
                                        <asp:HiddenField ID="hfTypeObs" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpAlteracaoStatus" style="width: 805px; display: block; top: 0px;
        left: 0px;" class="pop_up_super_super_super">
        <div>
            <div id="fecharAlteracaoStatus" class="btn_cancelar_popup" style="display: none">
            </div>
            <div class="barra_titulo">
                Alteração de Histórico</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopStatus" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td align="left">
                                    <strong>Você alterou o Histórico de Vencimento - Caso deseja enviar um email com esta
                                        alteração, preencha os campos abaixo e clique em Enviar/Salvar: </strong>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" class="TextBox">
                                    <asp:Label ID="lblAlteracao" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>Selecione os emails que receberão uma notificação com esta alteração:</strong>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left" width="33%">
                                                <input id="Checkbox7" type="checkbox" class="chkMarcarEmailsGrupo" checked="checked" />&nbsp;
                                                <strong>Grupo Econômico: </strong>
                                            </td>
                                            <td align="left" width="33%" valign="top">
                                                <input id="Checkbox10" type="checkbox" class="chkMarcarEmailsEmpresa" checked="checked" />&nbsp;
                                                <strong>Empresa:</strong>
                                            </td>
                                            <td align="left" width="33%">
                                                <input id="Checkbox11" type="checkbox" class="chkMarcarEmailsConsultora" checked="checked" />&nbsp;
                                                <strong>Consultoria: </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="ckbGrupos" runat="server" CssClass="chkEmailsGruposEconomicosCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td align="left">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="ckbEmpresas" runat="server" CssClass="chkEmailsEmpresasCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td align="left" width="44%">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="ckbConsultoria" runat="server" CssClass="chkEmailsConsultorasCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <hr style="height: -12px" />
                                    <strong>Registro em Histórico</strong> &nbsp;<asp:TextBox ID="tbxHistoricoAlteracao"
                                        runat="server" CssClass="TextBox" Height="56px" TextMode="MultiLine" Width="100%"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:CheckBox ID="chkEnviarEmail" runat="server" Text="Enviar e-mail com alteração de status"
                                        Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="hfTypeAlteracao" runat="server" />
                                    <asp:HiddenField ID="hfIdAlteracao" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                    <asp:Button ID="btnAlterarStatus" runat="server" CssClass="Button" Text="Enviar/Salvar"
                                        Width="170px" OnClick="btnAlterarStatus_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva as notificações com os dias e os e-mails selecionados')" />
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpEnviarEmailHistorico" style="width: 866px; display: block; top: 0px;
        left: 0px;" class="pop_up_super_super">
        <div>
            <div id="fecharEnviarEmailHistorico" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Enviar Histórico</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upEnvioHistorico" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>Os registros históricos serão enviados para o(s) e-mail(s) selecionados abaixo:
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <strong>Selecione os emails para envio:</strong>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <hr style="height: -12px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left" width="33%">
                                                <input id="Checkbox12" type="checkbox" class="chkMarcarEmailsGrupo" checked="checked" />&nbsp;
                                                <strong>Grupo Econômico: </strong>
                                            </td>
                                            <td align="left" width="33%" valign="top">
                                                <input id="Checkbox13" type="checkbox" class="chkMarcarEmailsEmpresa" checked="checked" />&nbsp;
                                                <strong>Empresa:</strong>
                                            </td>
                                            <td align="left" width="33%">
                                                <input id="Checkbox14" type="checkbox" class="chkMarcarEmailsConsultora" checked="checked" />&nbsp;
                                                <strong>Consultoria: </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="chkGruposHistorico" runat="server" CssClass="chkEmailsGruposEconomicosCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td align="left">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="chkEmpresaHistorico" runat="server" CssClass="chkEmailsEmpresasCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td align="left" width="44%">
                                                <div style="width: 95%; height: 66px; overflow: auto; background-color: #fff; border: 1px solid #E8E9E9;
                                                    border-radius: 4px; text-align: left; padding: 3px; font-size: 12px; margin: 2px;
                                                    -moz-box-shadow: 0 1px 1px #dddddd; -ms-box-shadow: 0 1px 1px #dddddd; -webkit-box-shadow: 0 1px 1px #DDD;
                                                    box-shadow: 0 1px 1px #DDD;">
                                                    <asp:CheckBoxList ID="chkConsultoraHistorico" runat="server" CssClass="chkEmailsConsultorasCont">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <hr style="height: -12px" />
                                    <strong>Outros (separar com ;)</strong>
                                    <asp:TextBox ID="tbxEmailsHistorico" runat="server" CssClass="TextBox" Height="56px"
                                        TextMode="MultiLine" Width="100%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                    <asp:Button ID="btnEnviarHistorico" runat="server" CssClass="Button" Text="Enviar"
                                        Width="170px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Envia o histórico para os e-mails selecionados')"
                                        OnClick="btnEnviarHistorico_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="divPopUpVencimentosPeriodicos" style="width: 700px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
        <div id="fechar_vencimentos_periodicos" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Renovação de Vencimentos Periódicos</div>
        <div>
        <asp:UpdatePanel ID="upVencimentosPeriodicos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="periodicos_sem_vencimentos" runat="server">
                <asp:Repeater ID="rptRenovacoes" runat="server" onitemcommand="rptRenovacoes_ItemCommand" oninit="rptRenovacoes_Init">
                 <ItemTemplate>
                   <table width="100%">
                    <tr>
                      <td colspan="2"><strong><asp:Label ID="lblItemDaRenovacao" runat="server" Text="<%# BindNomeItemDaRenovacao(Container.DataItem) %>"></asp:Label></strong></td>                      
                    </tr>
                       
                    <tr>
                        <td colspan="2">
                            Este vencimento já atingiu sua data de expiração.<br /> Para 
                            renová-lo informe a quantidade de dias de renovação do vencimento.</td>
                    </tr>
                    <tr>
                        <td width="17%" align="right">
                            Dias de Renovação:</td>
                        <td>
                            <asp:TextBox ID="tbxDiasRenovacaoPeriodicos" runat="server" CssClass="TextBox" Text="<%# BindDiasRenovacao(Container.DataItem) %>"></asp:TextBox>&nbsp;&nbsp;
                            
                            <asp:ImageButton ID="ibtnAddDiasRenovacaoPeriodicos" runat="server" ImageUrl="~/imagens/icone_adicionar.png"                         
                                 CommandName="SelComand" CommandArgument="<%# BindIdItemRenovacao(Container.DataItem) + ';' + BindTipoItemRenovacao(Container.DataItem) %>" />                                        
                        </td>
                    </tr>
                    <tr>
                      <td></td>
                      <td></td>
                    </tr>
                    <tr>
                      <td></td>
                      <td></td>
                    </tr>
                    <tr>
                      <td></td>
                      <td></td>
                    </tr>
                </table>                
                 </ItemTemplate>
                </asp:Repeater>
            </div>            
             </ContentTemplate>             
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="divRenovacaoPeriodicosDatas" style="width: 700px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
        <div id="fecharRenovacaoPeriodicosDatas" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Renovação de Vencimentos Periódicos</div>
        <div>
        <asp:UpdatePanel ID="upDatasRenovacao" runat="server" UpdateMode="Conditional">
        <ContentTemplate>            
            <div id="periodicos_com_vencimentos" runat="server">                
                     <div>
                      <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" colspan="2">
                                    Selecione a abaixo as renovações que deseja criar para este vencimento:
                                </td>
                            </tr>                            
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div style="max-height:300px; overflow:auto; border-radius: 4px; background-color: White; border: 1px solid #a6a5a5;">
                                    <input id="marcarTodosPeriodicos" type="checkbox" class="chkMarcarEmailsEmpresa" />&nbsp;
                                    <strong>Todas:</strong>
                                        <asp:CheckBoxList ID="chkVencimentosPeriodicos" runat="server" CssClass="chkEmailsEmpresasCont">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="hfIdItemVencimentoPeriodico" runat="server" />
                                    <asp:HiddenField ID="hfDiasRenovacaoPeriodicos" runat="server" />
                                    <asp:HiddenField ID="hfIdTipoVencimentoPeriodico" runat="server" />                                    
                                </td>
                            </tr>
                        </table>
                     </div> 
            </div>
            <div style="text-align:right;">
              <asp:Button ID="btnRenovarVencimentosPeriodicos" runat="server" CssClass="Button" Text="Renovar" Width="170px" 
                onclick="btnRenovarVencimentosPeriodicos_Click" oninit="btnRenovarVencimentosPeriodicos_Init" />
            </div>
             </ContentTemplate>             
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="arquivos_teste" style="width: 550px; height:440px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
        <div id="fechar_arquivos_teste" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Arquivos</div>
        <div>
            <asp:UpdatePanel ID="UPFrameArquivos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <iframe runat="server" name="conteudo" width="545px" height="435px" marginwidth="0" marginheight="0" scrolling="no" frameborder="0" id="conteudo" ></iframe> 
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
