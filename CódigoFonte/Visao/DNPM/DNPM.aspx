<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="DNPM.aspx.cs" Inherits="Site_DNPM" EnableEventValidation="false" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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
       
        
    </script>
    <style type="text/css">
        a img {
            border:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:Label ID="lblRenovacaoVencimentosPeriodicos" runat="server" Text=""></asp:Label>
                        <asp:ModalPopupExtender ID="lblRenovacaoVencimentosPeriodicos_popupextender" 
                            runat="server" BackgroundCssClass="simplemodal" 
                            CancelControlID="fechar_vencimentos_periodicos" 
                            PopupControlID="divPopUpVencimentosPeriodicos" TargetControlID="lblRenovacaoVencimentosPeriodicos">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblRenovacaoVencimentosPeriodicosDatas" runat="server" Text=""></asp:Label>
                        <asp:ModalPopupExtender ID="lblRenovacaoVencimentosPeriodicosDatas_popupextender" 
                            runat="server" BackgroundCssClass="simplemodal" 
                            CancelControlID="fecharRenovacaoPeriodicosDatas" 
                            PopupControlID="divRenovacaoPeriodicosDatas" TargetControlID="lblRenovacaoVencimentosPeriodicosDatas">
                        </asp:ModalPopupExtender>
                          <asp:Label ID="lblNotficacoes" runat="server" />
                        <asp:ModalPopupExtender ID="lblNotficacoes_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal_super_super"
                            CancelControlID="fecharNotificacoes" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpNotificacoes"
                            TargetControlID="lblNotficacoes">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnAbrirContratolbl" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnAbrirContratos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal_super"
                            CancelControlID="CancelarPopUpContrato" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpContratos"
                            TargetControlID="btnAbrirContratolbl">
                        </asp:ModalPopupExtender>
                         <asp:Label ID="lblPopEnviarHistorio" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblPopEnviarHistorio_popupextender" runat="server" 
                            BackgroundCssClass="simplemodal_super_super" CancelControlID="fecharEnviarEmailHistorico" 
                            PopupControlID="divPopUpEnviarEmailHistorico" 
                            TargetControlID="lblPopEnviarHistorio">
                        </asp:ModalPopupExtender>
                          <asp:Label ID="lblProrrogacao" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="ModalPopupExtenderlblProrrogacao" runat="server" 
                            BackgroundCssClass="simplemodal_super_super" CancelControlID="fecharProrrogacao" 
                            PopupControlID="popProrrogacao" 
                            TargetControlID="lblProrrogacao">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblSubstancias" runat="server" />
                        <asp:ModalPopupExtender ID="lblSubstancias_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal_super_super"
                            CancelControlID="btn_fechar_popSubstancia" DynamicServicePath="" Enabled="True"
                            PopupControlID="divPopSubstancia" TargetControlID="lblSubstancias">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblHistorico" runat="server" />
                        <asp:ModalPopupExtender ID="ModalPopupExtenderhistorico_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal_super_super" CancelControlID="fecharObs" DynamicServicePath=""
                            Enabled="True" PopupControlID="divPopUpObs" TargetControlID="lblHistorico">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblAlteracaoStatus" runat="server" />
                        <asp:ModalPopupExtender ID="lblAlteracaoStatus_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal_super_super_super" CancelControlID="fecharAlteracaoStatus"
                            DynamicServicePath="" Enabled="True" PopupControlID="divPopUpAlteracaoStatus"
                            TargetControlID="lblAlteracaoStatus">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblVencimentos" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpVencimentos_popupextender" runat="server" BackgroundCssClass="simplemodal_super_super"
                            CancelControlID="fecharVencimento" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpVencimentos"
                            TargetControlID="lblVencimentos">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnPopUpRenovacao" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpRenovacao_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharRenovacao" DynamicServicePath=""
                            Enabled="True" PopupControlID="divPopUpRenovacao" TargetControlID="btnPopUpRenovacao">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnPopUpGuia" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpGuia_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fecharGuia" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpGuia"
                            TargetControlID="btnPopUpGuia">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblAbrirSelecaoContratos" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblAbrirSelecaoContratos_popupextender" runat="server" BackgroundCssClass="simplemodal" 
                            CancelControlID="fecharSelecaoContratos" PopupControlID="divPopUpSelecaoContratos" 
                            TargetControlID="lblAbrirSelecaoContratos">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblAlvaraPesquisa" runat="server" Text=""></asp:Label>
                        <asp:ModalPopupExtender ID="btnPopUpAlvaraPesquisa_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharCadastroAlvaraPesquisa"
                            PopupControlID="divPopUpAlvaraPesquisa" TargetControlID="lblAlvaraPesquisa">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblPopUpRequerimentoLavra" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblPopUpRequerimentoLavra_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharRequerimentoLavra" DynamicServicePath=""
                            Enabled="True" PopupControlID="divPopUpRequerimentoLavra" TargetControlID="lblPopUpRequerimentoLavra">
                        </asp:ModalPopupExtender>
                        <asp:Label runat="server" ID="lblPopUpConcessaoLavra"></asp:Label>
                        <asp:ModalPopupExtender ID="lblPopUpConcessaoLavra_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharConcessaoLavra" DynamicServicePath=""
                            Enabled="True" PopupControlID="divPopUpConcessaoLavra" TargetControlID="lblPopUpConcessaoLavra">
                        </asp:ModalPopupExtender>
                        <asp:Label runat="server" ID="lblPopUpLicenciamento"></asp:Label>
                        <asp:ModalPopupExtender ID="lblPopUpLicenciamento_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharLicenciamento" DynamicServicePath=""
                            Enabled="True" PopupControlID="divPopUpLicenciamento" TargetControlID="lblPopUpLicenciamento">
                        </asp:ModalPopupExtender>
                        <asp:Label runat="server" ID="lblPopUpExtracao"></asp:Label>
                        <asp:ModalPopupExtender ID="lblPopUpExtracao_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fecharExtracao" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpExtracao"
                            TargetControlID="lblPopUpExtracao">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnPopUpSelecionarRegime" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpSelecionarRegime_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharSelecionarRegime" PopupControlID="divPopUpSelecionarRegime"
                            TargetControlID="btnPopUpSelecionarRegime">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnPopUpCadastroProcessoDNPM" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpCadastroProcessoDNPM_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharCadastroProcessoDNPM"
                            PopupControlID="divPopUpCadastroProcessoDNPM" TargetControlID="btnPopUpCadastroProcessoDNPM">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnSelecionarLicencaHide" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnSelecionarLicencaHide_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal_super" CancelControlID="fecharSelecionarLicenca"
                            PopupControlID="divPopUpSelecionarLicenca" TargetControlID="btnSelecionarLicencaHide">
                        </asp:ModalPopupExtender>
                        <br />
                        <asp:Label ID="btnNotificacoPop" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpbtnNotificacoPop_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal_super_super" CancelControlID="fecharCadastroNotificacao"
                            DynamicServicePath="" Enabled="True" PopupControlID="divPopUpCadastroNotificacao"
                            TargetControlID="btnNotificacoPop">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnPopUpCadastroExigencia" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpCadastroExigencia_ModalPopupExtender" runat="server"
                            BackgroundCssClass="simplemodal_super" CancelControlID="fecharCadastroExigencia"
                            PopupControlID="divPopUpCadastroExigencia" TargetControlID="btnPopUpCadastroExigencia">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnPopUpRequerimentoPesquisa" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpRequerimentoPesquisa_popupextender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharRequerimentoPesquisa"
                            DynamicServicePath="" Enabled="True" PopupControlID="divPopUpRequerimentoPesquisa"
                            TargetControlID="btnPopUpRequerimentoPesquisa">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnPopUpAlvaraPesquisa" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpAlvaraPesquisa_popupextender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharCadastroAlvaraPesquisa" DynamicServicePath=""
                            Enabled="True" PopupControlID="divPopUpAlvaraPesquisa" TargetControlID="btnPopUpAlvaraPesquisa">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnPopUpRequerimentoLavra" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpRequerimentoLavra_popupextender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharRequerimentoLavra" DynamicServicePath=""
                            Enabled="True" PopupControlID="divPopUpRequerimentoLavra" TargetControlID="btnPopUpRequerimentoLavra">
                        </asp:ModalPopupExtender>
                        <br />
                        <asp:Label ID="btnPopUpConcessaoLavra" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpConcessaoLavra_popupextender" runat="server"
                            BackgroundCssClass="simplemodal" CancelControlID="fecharConcessaoLavra" DynamicServicePath=""
                            Enabled="True" PopupControlID="divPopUpConcessaoLavra" TargetControlID="btnPopUpConcessaoLavra">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="lblUploadArquivos" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="lblUploadArquivos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fechar_arquivos_teste" DynamicServicePath="" Enabled="True" PopupControlID="arquivos_teste" TargetControlID="lblUploadArquivos">
                        </asp:ModalPopupExtender>
                        <asp:Label ID="btnPopUpExtracao" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpExtracao_popupextender" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fecharExtracao" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpExtracao"
                            TargetControlID="btnPopUpExtracao">
                        </asp:ModalPopupExtender>
                       
                        <asp:Label ID="btnPopUpLicenciamento" runat="server" Text="" />
                        <asp:ModalPopupExtender ID="btnPopUpLicenciamento_popupextender" runat="server" BackgroundCssClass="simplemodal"
                            CancelControlID="fecharLicenciamento" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpLicenciamento"
                            TargetControlID="btnPopUpLicenciamento">
                        </asp:ModalPopupExtender> 
    <div>
        <div>
            <table width="100%">
                <tr>
                    <td width="30%" align="left">
                        Grupo Econômico:<br />
                        <asp:DropDownList ID="ddlClientes" runat="server" CssClass="DropDownList" Height="25px"
                            Width="90%" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="True"
                            DataTextField="Nome" DataValueField="Id" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Ao selecionar um grupo econômico, todos os processos ANM de todas as suas empresas serão exibidos')">
                        </asp:DropDownList>
                    </td>
                    <td width="30%" align="left">
                         Empresa (opcional):<br />
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="DropDownList" DataTextField="Nome"
                                    DataValueField="Id" Height="25px" Width="90%" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged"
                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Ao selecionar uma empresa, todos os processos ANM desta serão exibidos')">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlClientes" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" width="10%">
                        Processo(opcional):<br />
                        <asp:TextBox ID="tbxNumeroProcesso" runat="server" CssClass="TextBox" Width="100px"
                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Informe um número de processo, caso queira consultar um processo específico')"></asp:TextBox>
                    </td>
                    <td width="25%" align="left">
                        Status da Empresa:<br />
                        <asp:DropDownList ID="ddlStatusEmpresa" Height="25px" Width="90%" CssClass="DropDownList" runat="server"  OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True">Todos</asp:ListItem>
                            <asp:ListItem>Ativo</asp:ListItem>
                            <asp:ListItem>Inativo</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" width="5%">
                         &nbsp;<br />
                        <asp:Button runat="server" Text="Carregar" CssClass="Button" Width="120px" ID="ibtnCarregarArvore"
                            OnClick="ibtnAtualizarArvore_Click" Height="28px" onmouseout="tooltip.hide();"
                            onmouseover="tooltip.show('Carrega a árvore de processos de acordo com os filtros selecionados')">
                        </asp:Button>
                    </td>
                    
                </tr>
            </table>
        </div>
        <div class="containe_arvore">
            <table width="100%" cellpadding="5" cellspacing="5">
                <tr>
                    <td width="30%" valign="top">
                        <div class="container_arvore">
                            <asp:UpdatePanel ID="upArvore" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="padding5">
                                        <div class="barra_opcoes_ferramentas" id="barraOpcoes" runat="server">
                                            <div style="float: left;">
                                                Processo ANM:
                                                <asp:LinkButton ID="lkbOpcoesProcessoNovo" runat="server" OnClick="lkbOpcoesProcessoNovo_Click"
                                                    OnClientClick="tooltip.hide();" OnInit="lkbOpcoesProcessoNovo_Init" onmouseout="tooltip.hide();"
                                                    onmouseover="tooltip.show('Cadastra um novo processo ANM para uma empresa do grupo')">Novo</asp:LinkButton>
                                                &nbsp;|
                                                <asp:LinkButton ID="lkbOpcoesProcessoEditar" runat="server" OnClick="lkbOpcoesProcessoEditar_Click"
                                                    OnClientClick="tooltip.hide();" OnInit="lkbOpcoesProcessoEditar_Init" onmouseout="tooltip.hide();"
                                                    onmouseover="tooltip.show('Abre o processo selecionado na árvore para edição de suas informações')">Editar</asp:LinkButton>
                                                &nbsp;|
                                                <asp:LinkButton ID="lkbOpcoesProcessoExcluir" runat="server" OnClick="lkbOpcoesProcessoExcluir_Click"
                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o processo selecionado na árvore')">Excluir</asp:LinkButton>
                                                <br />
                                                Regime:
                                                <asp:LinkButton ID="lkbRegimeNovo" runat="server" OnClick="lkbRegimeNovo_Click" OnInit="lkbRegimeNovo_Init"
                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cadastra um novo Regime dentro de um processo selecionado na árvore')">Cadastro</asp:LinkButton>
                                                &nbsp;|
                                                <asp:LinkButton ID="lkbRegimeEditar" runat="server" OnClick="lkbRegimeEditar_Click"
                                                    OnClientClick="tooltip.hide();" OnInit="lkbRegimeEditar_Init" onmouseout="tooltip.hide();"
                                                    onmouseover="tooltip.show('Abre o regime selecionado na árvore para edição de suas informações')">Editar</asp:LinkButton>
                                                &nbsp;|
                                                <asp:LinkButton ID="lkbRegimeExcluir" runat="server" OnClick="lkbRegimeExcluir_Click"
                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o regime selecionado na árvore')">Excluir</asp:LinkButton>
                                                <br />
                                                Guia de Utilização:
                                                <asp:LinkButton ID="lkbOpcoesGuiaNovo" runat="server" OnClick="lkbOpcoesGuiaNovo_Click"
                                                    OnClientClick="tooltip.hide();" OnInit="lkbOpcoesGuiaNovo_Init" onmouseout="tooltip.hide();"
                                                    onmouseover="tooltip.show('Cadastra uma nova guia de utilização para o processo selecionado na árvore')">Nova</asp:LinkButton>
                                                &nbsp;|
                                                <asp:LinkButton ID="lkbOpcoesGuiaNovo0" runat="server" OnClick="lkbOpcoesGuiaEditar_Click"
                                                    OnClientClick="tooltip.hide();" OnInit="lkbOpcoesGuiaEditar_Init" onmouseout="tooltip.hide();"
                                                    onmouseover="tooltip.show('Abre a guia de utilização selecionada na árvore para edição de suas informações')">Editar</asp:LinkButton>
                                                &nbsp;|
                                                <asp:LinkButton ID="lkbOpcoesGuiaExcluir" runat="server" OnClick="lkbOpcoesGuiaExcluir_Click"
                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui a guia de utilização selecionada na árvore')">Excluir</asp:LinkButton>
                                                <br />
                                                RAL:
                                                <asp:LinkButton ID="lkbOpcoesRALExcluir" runat="server" OnClick="lkbOpcoesRALExcluir_Click"
                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o RAL selecionado na árvore. Um RAL é criado automaticamente quando é expedida a portaria de lavra, quando é criada uma Guia de Utilização ou quando é criado um regime de licenciamento.')">Excluir</asp:LinkButton>
                                            </div>
                                            <div class="botao_atualizar_arvoreDNPM">
                                                <asp:ImageButton ID="ibtnAtualizarArvore" runat="server" ImageUrl="~/imagens/refresh.png"
                                                    OnClick="ibtnAtualizarArvore_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Atualiza as informações contidas na árvore de processos')" />
                                            </div>
                                        </div>
                                        <asp:TreeView ID="trvProcessos" runat="server" ImageSet="Arrows" OnSelectedNodeChanged="trvProcessos_SelectedNodeChanged"
                                            OnInit="trvProcessos_Init" Style="margin-top: 0px; padding-top: 0px;">
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                                                NodeSpacing="0px" VerticalPadding="0px" />
                                            <ParentNodeStyle Font-Bold="False" />
                                            <SelectedNodeStyle Font-Underline="True" ForeColor="#339933" HorizontalPadding="0px"
                                                VerticalPadding="0px" />
                                        </asp:TreeView>
                                        <asp:HiddenField ID="hfIdProcessoDNPM" runat="server" />
                                        <asp:HiddenField ID="hfIdEmpresa" runat="server" />
                                        <asp:HiddenField ID="hfAlvaraPesquisa" runat="server" />
                                        <asp:HiddenField ID="hfIdGuiaUtilizacao" runat="server" />
                                        <asp:HiddenField ID="hfIdLicenciamento" runat="server" />
                                        <asp:HiddenField ID="hfIdExtracao" runat="server" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ibtnCarregarArvore" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlClientes" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="trvProcessos" 
                                        EventName="SelectedNodeChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="lkbRegimeExcluir" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="lkbOpcoesGuiaExcluir" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="lkbOpcoesRALExcluir" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="lkbOpcoesProcessoExcluir" 
                                        EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="lkbRegimeEditar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="lkbOpcoesProcessoEditar" 
                                        EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="lkbOpcoesGuiaNovo0" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                                              
                         
                    </td>
                    <td width="70%" valign="top">
                        <div class="container_arvore">
                            <div class="padding5">
                                <div align="left">
                                    <asp:UpdatePanel ID="upVisoes" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:MultiView ID="mvwProcessos" runat="server">
                                                <asp:View ID="View1" runat="server">
                                                    <table cellpadding="2" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td align="left" colspan="2" style="width: 100%; height: 35px;">
                                                                <div class="barra_titulos">
                                                                    Processo ANM
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
                                                                <asp:Label ID="lblEmpresa" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Número do Processo:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblNumeroProcesso" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Substância:</strong></td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblSubstancia" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Data de Abertura:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblDataAbertura" runat="server"></asp:Label>
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
                                                                <strong>Apelido da Área:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblIdentificacaoArea" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Tamanho:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblTamanho" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Endereço:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblEndereco" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                <strong>Cidade:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblCidade" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" width="40%">
                                                                <strong>Estado:</strong>
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblEstado" runat="server"></asp:Label>
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
                                                            <td align="right" valign="top" width="40%">
                                                                <strong>Anexos:</strong>&nbsp;
                                                            </td>
                                                            <td width="60%">
                                                                <asp:LinkButton ID="lbtnDownloadProcesso" runat="server" OnClick="lbtnDownloadProcesso_Click"
                                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibi os arquivos anexados ao processo')" OnInit="lbtnDownloadProcesso_Init">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top" width="40%">&nbsp;</td>
                                                            <td align="right" width="60%">
                                                                <a  href="http://sigmine.dnpm.gov.br" target="_blank">
                                                                    <img src="../imagens/sigmine.png" />
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2" height="20" style="width: 100%" valign="bottom">
                                                                <div class="barra_titulos">
                                                                    <asp:Label ID="Label1" runat="server" Text="Licenças Associadas" onmouseout="tooltip.hide();"
                                                                        onmouseover="tooltip.show('Lista de licenças ambientais associadas ao procsso ANM')"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2" height="50" style="width: 100%" valign="top">
                                                                <asp:GridView ID="grvLicencasVisao" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                                                    Width="100%">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                                        <asp:TemplateField HeaderText="Órgão">
                                                                            <ItemTemplate>
                                                                                <%# BindOrgao(Container.DataItem)%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tipo">
                                                                            <ItemTemplate>
                                                                                <%# BindTipo(Container.DataItem)%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="DataRetirada" DataFormatString="{0:d}" HeaderText="Retirada" />
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2" style="width: 100%" valign="top">
                                                                <div class="barra_titulos">
                                                                    <asp:Label ID="Label2" runat="server" Text="Processos no Sistema ANM" onmouseout="tooltip.hide();"
                                                                        onmouseover="tooltip.show('Dados do processo selecionado, consultados direto do site do ANM')"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2" style="width: 100%" valign="top">
                                                                <iframe id="frameprocesso" runat="server" width="100%" height="385px" frameborder="0">
                                                                </iframe>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View2" runat="server">
                                                    <table cellpadding="2" style="width: 100%;">
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Requerimento de Pesquisa
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle" align="right" width="40%">
                                                                Data de Entrada:
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                <asp:Label ID="lblDataEntrada" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" width="40%">
                                                                Anexos:
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                <asp:LinkButton ID="lbtnDownloadRequerimentoPesquisa" runat="server" OnClick="lbtnDownloadRequerimentoPesquisa_Click"
                                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os arquivos anexados ao Requerimento de Pesquisa')" OnInit="lbtnDownloadRequerimentoPesquisa_Init">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Exigências
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="grvRequerimentosPesquisa" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" Width="100%" OnInit="grvRequerimentosPesquisa_Init" OnRowCancelingEdit="grvRequerimentosPesquisa_RowCancelingEdit" OnRowEditing="grvRequerimentosPesquisa_RowEditing">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DataPublicacao" HeaderText="Data de Publicação" DataFormatString="{0:d}" />
                                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                        <asp:BoundField DataField="DiasPrazo" HeaderText="Dias de Prazo" />
                                                                        <asp:TemplateField HeaderText="Vencimento">
                                                                            <ItemTemplate>
                                                                                <%# BindDatadeVencimento(Container.DataItem)%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Renovar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Cancel" Enabled="<%# BindEnableRenovacao(Container.DataItem) %>" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>"
                                                                                    ImageUrl="<%# BindImagemRenovacao(Container.DataItem) %>" ToolTip="<%# BindToolTipoRenovacao(Container.DataItem) %>"
                                                                                    OnPreRender="ibtnRenovar_PreRender" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                                                                    onmouseover="tooltip.show('Renova um Requerimento de pesquisa periódico')" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Arquivo">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnAbrirDownloadExigenciasGuia" runat="server" ImageUrl="~/imagens/icone_anexo.png" CommandName="Edit"
                                                                                    ToolTip="Ver Arquivos" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View3" runat="server">
                                                    <table cellpadding="2" style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <div class="barra_titulos">
                                                                    Alvará de Pesquisa
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Data de Publicação:
                                                                <asp:Label ID="lblDataDePublicacao" runat="server" Font-Bold="True"></asp:Label>
                                                                <br />
                                                                Data de Entrega do Relatório de Pesquisa:
                                                                <asp:Label ID="lblDataEntregaRelatorioPesquisa" runat="server" Font-Bold="True"></asp:Label>
                                                                <br />
                                                                Data de Aprovação do Relatório de Pesquisa:
                                                                <asp:Label ID="lblDataAprovacaoRelatorioPesquisa" runat="server" Font-Bold="True"></asp:Label>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Anexos:
                                                                <asp:LinkButton ID="lbtnDownloadAlvaraPesquisa" runat="server" OnClick="lbtnDownloadAlvaraPesquisa_Click"
                                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os arquivos anexados ao Alvará de Pesquisa')" OnInit="lbtnDownloadAlvaraPesquisa_Init">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="barra_titulos">
                                                                    Exigências
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="grvAlvaraPesquisa" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                                                    Width="100%" OnInit="grvAlvaraPesquisa_Init" OnRowCancelingEdit="grvAlvaraPesquisa_RowCancelingEdit" OnRowEditing="grvAlvaraPesquisa_RowEditing">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DataPublicacao" HeaderText="Data de Publicação" DataFormatString="{0:d}" />
                                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                        <asp:BoundField DataField="DiasPrazo" HeaderText="Dias de Prazo" />
                                                                        <asp:TemplateField HeaderText="Data de Vencimento">
                                                                            <ItemTemplate>
                                                                                <%# BindDatadeVencimento(Container.DataItem)%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Renovar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Cancel" Enabled="<%# BindEnableRenovacao(Container.DataItem) %>" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" 
                                                                                    ImageUrl="<%# BindImagemRenovacao(Container.DataItem) %>" ToolTip="<%# BindToolTipoRenovacao(Container.DataItem) %>"
                                                                                    OnPreRender="ibtnRenovar_PreRender1" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                                                                    onmouseover="tooltip.show('Renova a exigência do Alvará de Pesquisa, caso ela seja periódica')" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Arquivo">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnAbrirDownloadExigenciasGuia" runat="server" ImageUrl="~/imagens/icone_anexo.png" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" CommandName="Edit" ToolTip="Ver Arquivos" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="width: 100%; height: 15px;">
                                                                </div>
                                                                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="4" 
                                                                    Width="100%">
                                                                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Validade">
                                                                        <ContentTemplate>
                                                                            <div style="width: 100%; height: 10px;">
                                                                            </div>
                                                                            <table cellpadding="2" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        Validade:
                                                                                        <asp:Label ID="lblValidadeAlvara" runat="server" Font-Bold="True"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Prazo limite para requerer prorrogação:
                                                                                        <asp:Label ID="lblDataLimiteRenovacaoAlvara" runat="server" Font-Bold="True"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Data da próxima Notificação:
                                                                                        <asp:Label ID="lblDataUltimaNotificacaoAlvaraPesquisaValidade" runat="server" Font-Bold="True"></asp:Label>
                                                                                        &nbsp;-
                                                                                        <asp:LinkButton ID="lkbVenc0" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                            OnClick="lkbVenc0_Click">Ver mais</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:TabPanel>
                                                                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Comunicar Início da Pesquisa">
                                                                        <ContentTemplate>
                                                                            <div style="width: 100%; height: 10px;">
                                                                            </div>
                                                                            <table cellpadding="2" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        Data Limite:
                                                                                        <asp:Label ID="lblNotificacaoPesquisaAlvara" runat="server" Font-Bold="True"></asp:Label><div
                                                                                            style="width: 100%; height: 10px;">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Data da próxima Notificação:
                                                                                        <asp:Label ID="lblDataUltimaNotificacaoAlvaraPesquisaNotificacaoPesquisa" runat="server"
                                                                                            Font-Bold="True"></asp:Label>
                                                                                        &nbsp;-
                                                                                        <asp:LinkButton ID="lkbVenc" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                            OnClick="lkbVenc_Click">Ver mais</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:TabPanel>
                                                                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Taxa Anual por Hectare">
                                                                        <ContentTemplate>
                                                                            <div style="width: 100%; height: 10px;">
                                                                            </div>
                                                                            <table cellpadding="2" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        Data Limite:
                                                                                        <asp:Label ID="lblTaxaAnualHectareAlvara" runat="server" Font-Bold="True"></asp:Label>
                                                                                        <div style="width: 100%; height: 10px;">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Data da próxima Notificação:
                                                                                        <asp:Label ID="lblDataUltimaNotificacaoAlvaraPesquisaTaxaAnualHectare" runat="server"
                                                                                            Font-Bold="True"></asp:Label>
                                                                                        &nbsp;-
                                                                                        <asp:LinkButton ID="lkbVenc1" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                            OnClick="lkbVenc1_Click">Ver mais</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblTaxaAnualHectare" runat="server" Font-Bold="False" Font-Size="8pt"
                                                                                            ForeColor="#990000"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="btnRenovarAlvara" runat="server" CssClass="Button" OnClick="btnRenovarAlvara_Click"
                                                                                            OnClientClick="tooltip.hide();" OnInit="btnRenovar_Init" Text="Renovar" Width="150px"
                                                                                            OnPreRender="btnRenovarAlvara_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renovar a Taxa Anual por Hectare')" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:TabPanel>
                                                                    <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Requerimento de Lavra">
                                                                        <ContentTemplate>
                                                                            <div style="width: 100%; height: 10px;">
                                                                            </div>
                                                                            <table cellpadding="2" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        Data Limite:
                                                                                        <asp:Label ID="lblRequerimentoLavraAlvara" runat="server" Font-Bold="True"></asp:Label><div
                                                                                            style="width: 100%; height: 10px;">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Data da próxima Notificação:
                                                                                        <asp:Label ID="lblDataUltimaNotificacaoAlvaraPesquisaRequerimentoLavra" runat="server"
                                                                                            Font-Bold="True"></asp:Label>
                                                                                        &nbsp;-
                                                                                        <asp:LinkButton ID="lkbVenc2" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                            OnClick="lkbVenc2_Click">Ver mais</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:TabPanel>
                                                                    <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Requerimento de LP Poligonal">
                                                                        <ContentTemplate>
                                                                            <div style="width: 100%; height: 10px;">
                                                                            </div>
                                                                            <table cellpadding="2" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblRequerimentoLPTotalAlvara" runat="server" Font-Bold="True"></asp:Label><div
                                                                                            style="width: 100%; height: 10px;">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Data da próxima Notificação:
                                                                                        <asp:Label ID="lblDataUltimaNotificacaoAlvaraPesquisaRequerimentoLPTotal" runat="server"
                                                                                            Font-Bold="True"></asp:Label>
                                                                                        &nbsp;-
                                                                                        <asp:LinkButton ID="lkbVenc3" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                            OnClick="lkbVenc3_Click">Ver mais</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:TabPanel>
                                                                    <asp:TabPanel ID="TabPanelDP" runat="server" HeaderText="DIPEM">
                                                                        <HeaderTemplate>
                                                                            DIPEM</HeaderTemplate>
                                                                        <ContentTemplate>
                                                                            <table cellpadding="2" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        Data Vencimento:
                                                                                        <asp:Label ID="lblVencimentoDIPEM" runat="server" Font-Bold="True"></asp:Label><div
                                                                                            style="width: 100%; height: 10px;">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Data da próxima Notificação:
                                                                                        <asp:Label ID="lblDataUltimaNotificacaoDIPEM" runat="server" Font-Bold="True"></asp:Label>
                                                                                        &nbsp;-
                                                                                        <asp:LinkButton ID="lkbVenc4" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                            OnClick="lkbVenc4_Click">Ver mais</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="btnRenovarValidadeLicenciamento0" runat="server" CssClass="Button"
                                                                                            OnClick="btnRenovarValidadeLicenciamento0_Click" OnInit="btnRenovar_Init" Text="Renovar"
                                                                                            OnClientClick="tooltip.hide();" Width="150px" OnPreRender="btnRenovarValidadeLicenciamento0_PreRender"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova o DIPEM do Alvará de Pesquisa')" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:TabPanel>
                                                                       <asp:TabPanel ID="TabPanel8" runat="server" HeaderText="Renúncia">
                                                                        <HeaderTemplate>
                                                                            Renúncia</HeaderTemplate>
                                                                        <ContentTemplate>
                                                                            <table cellpadding="2" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        Data limite para renúncia sem apresentar relatório:
                                                                                        <asp:Label ID="lblDataLimiteRenuncia" runat="server" Font-Bold="True"></asp:Label><div
                                                                                            style="width: 100%; height: 10px;">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Data da próxima Notificação:
                                                                                        <asp:Label ID="lblproximaNotificacaoLimiteRenuncia" runat="server" 
                                                                                            Font-Bold="True"></asp:Label>
                                                                                        &nbsp;-
                                                                                        <asp:LinkButton ID="lkbLimiteRenucia" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                            OnClick="lkbLimiteRenucia_Click">Ver mais</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:TabPanel>
                                                                </asp:TabContainer>
                                                                <div style="width: 100%; height: 8px;">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View4" runat="server">
                                                    <table cellpadding="2" style="width: 100%;">
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Requerimento de Lavra
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle" align="right">
                                                                Data de Abertura:
                                                            </td>
                                                            <td valign="middle" width="60%">
                                                                <asp:Label ID="lblDataAberturaLavra" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle">
                                                                Anexos:
                                                            </td>
                                                            <td valign="middle" width="60%">
                                                                <asp:LinkButton ID="lbtnDownloadRequerimentoLavra" runat="server" OnClick="lbtnDownloadRequerimentoLavra_Click"
                                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os arquivos anexados ao Requerimento de Lavra')" OnInit="lbtnDownloadRequerimentoLavra_Init">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Exigências
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="grvDataRequerimentoLavraExigencias" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" Width="100%" OnInit="grvDataRequerimentoLavraExigencias_Init"
                                                                    OnRowCancelingEdit="grvDataRequerimentoLavraExigencias_RowCancelingEdit" OnRowEditing="grvDataRequerimentoLavraExigencias_RowEditing">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DataPublicacao" HeaderText="Data de Publicação" DataFormatString="{0:d}" />
                                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                        <asp:BoundField DataField="DiasPrazo" HeaderText="Dias de Prazo" />
                                                                        <asp:TemplateField HeaderText="Vencimento">
                                                                            <ItemTemplate>
                                                                                <%# BindDatadeVencimento(Container.DataItem)%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Renovar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Cancel" Enabled="<%# BindEnableRenovacao(Container.DataItem) %>" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" 
                                                                                    ImageUrl="<%# BindImagemRenovacao(Container.DataItem) %>" ToolTip="<%# BindToolTipoRenovacao(Container.DataItem) %>"
                                                                                    OnPreRender="ibtnRenovar_PreRender2" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                                                                    onmouseover="tooltip.show('Renova um Requerimento de Lavra periódico')" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Arquivo">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnAbrirDownloadExigenciasGuia" runat="server" ImageUrl="~/imagens/icone_anexo.png" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" 
                                                                                    ToolTip="Ver Arquivos" CommandName="Edit" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View5" runat="server">
                                                    <table cellpadding="2" style="width: 100%;">
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Concessão de Lavra
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle" align="right" width="40%">
                                                                Data de Publicação:
                                                            </td>
                                                            <td valign="middle">
                                                                <asp:Label ID="lblDataPublicacaoConcessaoLavra" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" width="40%">
                                                                Número da Portaria de Lavra:
                                                            </td>
                                                            <td valign="middle">
                                                                <asp:Label ID="lblNumeroPortariaLavra" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" width="40%">
                                                                Data de apresentação do relatório de reavaliação de reserva:
                                                            </td>
                                                            <td valign="middle">
                                                                <asp:Label ID="lblReavaliacaoReserva" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" width="40%">
                                                                Anexos:
                                                            </td>
                                                            <td valign="middle">
                                                                <asp:LinkButton ID="lbtnDownloadConcessaoLavra" runat="server" OnClick="lbtnDownloadConcessaoLavra_Click"
                                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os arquivos anexados à Concessão de Lavra')" OnInit="lbtnDownloadConcessaoLavra_Init">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Exigências
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="grvConcessaoLavraExigencias" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" Width="100%" OnInit="grvConcessaoLavraExigencias_Init" OnRowCancelingEdit="grvConcessaoLavraExigencias_RowCancelingEdit" OnRowEditing="grvConcessaoLavraExigencias_RowEditing">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DataPublicacao" HeaderText="Data de Publicação" DataFormatString="{0:d}" />
                                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                        <asp:BoundField DataField="DiasPrazo" HeaderText="Dias de Prazo" />
                                                                        <asp:TemplateField HeaderText="Vencimento">
                                                                            <ItemTemplate>
                                                                                <%# BindDatadeVencimento(Container.DataItem)%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Renovar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Cancel" ImageUrl="<%# BindImagemRenovacao(Container.DataItem) %>"
                                                                                    ToolTip="<%# BindToolTipoRenovacao(Container.DataItem) %>" OnPreRender="ibtnRenovar_PreRender3"
                                                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renovar uma Concessão de Lavra periódica')"
                                                                                    Enabled="<%# BindEnableRenovacao(Container.DataItem) %>" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Arquivo">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnAbrirDownloadExigenciasGuia" runat="server" ImageUrl="~/imagens/icone_anexo.png"
                                                                                    ToolTip="Ver Arquivos" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" CommandName="Edit"  />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="30" colspan="2" valign="bottom">
                                                                Requerimento de Imissão de Posse:
                                                                <asp:Label ID="lblRequerimentoImissaoConcessaoLavra" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                Data da próxima Notificação:
                                                                <asp:Label ID="lblDataUltimaNotificacaoConcessaoLavra" runat="server" Font-Bold="True"></asp:Label>
                                                                &nbsp;-
                                                                <asp:LinkButton ID="lkbVenc5" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                    OnClick="lkbVenc5_Click">Ver mais</asp:LinkButton>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View6" runat="server">
                                                    <table cellpadding="2" style="width: 100%;">
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Licenciamento
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Número:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:Label ID="lblNumeroLicenciamento" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Data de Abertura:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:Label ID="lblDataAberturaLicenciamento" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Data de Publicação:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:Label ID="lblDataPublicacaoLicenciamento" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Anexos:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:LinkButton ID="lbtnDownloadLicenciamento" runat="server" OnClick="lbtnDownloadLicenciamento_Click"
                                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os arquivos anexados ao regime de licenciamento')" OnInit="lbtnDownloadLicenciamento_Init">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Exigências
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="grvExigenciasLicenciamento" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" Width="100%" OnInit="grvExigenciasLicenciamento_Init" OnRowCancelingEdit="grvExigenciasLicenciamento_RowCancelingEdit" OnRowEditing="grvExigenciasLicenciamento_RowEditing">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DataPublicacao" HeaderText="Data de Publicação" DataFormatString="{0:d}" />
                                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                        <asp:BoundField DataField="DiasPrazo" HeaderText="Dias de Prazo" />
                                                                        <asp:TemplateField HeaderText="Vencimento">
                                                                            <ItemTemplate>
                                                                                <%# BindDatadeVencimento(Container.DataItem)%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Renovar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Cancel" Enabled="<%# BindEnableRenovacao(Container.DataItem) %>" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" 
                                                                                    ImageUrl="<%# BindImagemRenovacao(Container.DataItem) %>" ToolTip="<%# BindToolTipoRenovacao(Container.DataItem) %>"
                                                                                    OnPreRender="ibtnRenovar_PreRender4" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                                                                    onmouseover="tooltip.show('Renova a exigência do licenciamnto, caso ela seja periódica')" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Arquivo">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnAbrirDownloadExigenciasGuia" runat="server" ImageUrl="~/imagens/icone_anexo.png" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" 
                                                                                    ToolTip="Ver Arquivos" CommandName="Edit" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="width: 100%; height: 15px;">
                                                                </div>
                                                                <asp:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="1" 
                                                                    Width="100%">
                                                                    <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="Validade">
                                                                        <ContentTemplate>
                                                                            <div style="width: 100%; height: 10px;">
                                                                            </div>
                                                                            <table cellpadding="2" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        Validade:
                                                                                        <asp:Label ID="lblValidadeLicenciamento" runat="server" Font-Bold="True"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Prazo Limite para Renovação:
                                                                                        <asp:Label ID="lblDatalimiteVencimentoLicenca" runat="server" Font-Bold="True"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Data da próxima Notificação:
                                                                                        <asp:Label ID="lblDataUltimaNotificacaoLicenciamentoValidade" runat="server" Font-Bold="True"></asp:Label>
                                                                                        &nbsp;-
                                                                                        <asp:LinkButton ID="lkbVenc6" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                            OnClick="lkbVenc6_Click">Ver mais</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="btnRenovarValidadeLicenciamento" runat="server" CssClass="Button"
                                                                                            OnClick="btnRenovarValidadeLicenciamento_Click" OnInit="btnRenovar_Init" Text="Renovar"
                                                                                            OnClientClick="tooltip.hide();" Width="150px" OnPreRender="btnRenovarValidadeLicenciamento_PreRender"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova a validade do regime de licenciamento')" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:TabPanel>
                                                                    <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="Entrega de Licença ou Protocolo">
                                                                        <ContentTemplate>
                                                                            <div style="width: 100%; height: 10px;">
                                                                            </div>
                                                                            <table cellpadding="2" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        Data Limite:
                                                                                        <asp:Label ID="lblDataLimiteEntregaLicencaProtocoloLicenciamento" runat="server"
                                                                                            Font-Bold="True"></asp:Label><div style="width: 100%; height: 10px;">
                                                                                            </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Data da próxima Notificação:
                                                                                        <asp:Label ID="lblDataUltimaNotificacaoLicenciamentoEntregaLicencaProtocolo" runat="server"
                                                                                            Font-Bold="True"></asp:Label>
                                                                                        &nbsp;-
                                                                                        <asp:LinkButton ID="lkbVenc7" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                                            OnClick="lkbVenc7_Click">Ver mais</asp:LinkButton>
                                                                                        <div style="width: 100%; height: 8px;">
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:TabPanel>
                                                                </asp:TabContainer>
                                                                <div style="height: 8px;">
                                                                </div>
                                                                <div class="barra_titulos">
                                                                    <asp:Label ID="Label3" runat="server" Text="Licenças Associadas" onmouseout="tooltip.hide();"
                                                                        onmouseover="tooltip.show('Licenças ambientais associadas ao regime de Licenciamento')"></asp:Label>
                                                                </div>
                                                                <div style="height: 8px;">
                                                                </div>
                                                                <div style="width: 100%;">
                                                                    <asp:GridView ID="grvLicenciamentoLicencaAssociadas" runat="server" AutoGenerateColumns="False"
                                                                        CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                        GridLines="None" Width="100%">
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                                            <asp:TemplateField HeaderText="Órgão">
                                                                                <ItemTemplate>
                                                                                    <%# BindOrgao(Container.DataItem)%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Tipo">
                                                                                <ItemTemplate>
                                                                                    <%# BindTipo(Container.DataItem)%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="DataRetirada" DataFormatString="{0:d}" HeaderText="Retirada" />
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
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View7" runat="server">
                                                    <table cellpadding="2" style="width: 100%;">
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Extração
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Número:
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblNumeroExtracao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Data de Publicação:
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblDataPublicacaoExtracao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Data de Abertura:
                                                            </td>
                                                            <td width="60%">
                                                                <asp:Label ID="lblDataAberturaExtracao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Número da Licença:
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblNumerolicencaExtracao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Validade Licença:
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblValidadeLicencaExtracao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Validade:
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblValidadeExtracao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Data da próxima Notificação:
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDataUltimaNotificacaoExtracaoValidade" runat="server" Font-Bold="True"></asp:Label>
                                                                &nbsp;-
                                                                <asp:LinkButton ID="lkbVenc8" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                    OnClick="lkbVenc8_Click">Ver mais</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                Anexos:
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lbtnDownloadExtracao" runat="server" OnClick="lbtnDownloadExtracao_Click"
                                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os arquivos anexados ao regime de extração')" OnInit="lbtnDownloadExtracao_Init">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnRenovarValidadeExtracao" runat="server" CssClass="Button" OnClick="btnRenovarValidadeExtracao_Click"
                                                                    OnInit="btnRenovar_Init" Text="Renovar" Width="150px" OnPreRender="btnRenovarValidadeExtracao_PreRender"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova a validade do regime de Extração')" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Exigências
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="grvExigenciasExtracao" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" Width="100%" OnInit="grvExigenciasExtracao_Init" OnRowCancelingEdit="grvExigenciasExtracao_RowCancelingEdit" OnRowEditing="grvExigenciasExtracao_RowEditing">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DataPublicacao" HeaderText="Data de Publicação" DataFormatString="{0:d}" />
                                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                        <asp:BoundField DataField="DiasPrazo" HeaderText="Dias de Prazo" />
                                                                        <asp:TemplateField HeaderText="Vencimento">
                                                                            <ItemTemplate>
                                                                                <%# BindDatadeVencimento(Container.DataItem)%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Renovar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Cancel" Enabled="<%# BindEnableRenovacao(Container.DataItem) %>" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" 
                                                                                    ImageUrl="<%# BindImagemRenovacao(Container.DataItem) %>" ToolTip="<%# BindToolTipoRenovacao(Container.DataItem) %>"
                                                                                    OnPreRender="ibtnRenovar_PreRender5" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova a exigência da Extração, caso ela seja periódica')" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Arquivo">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnAbrirDownloadExigenciasGuia" runat="server" ImageUrl="~/imagens/icone_anexo.png"
                                                                                   ToolTip="Ver Arquivos" Visible="<%# BindVisibleEdicaoExigencia(Container.DataItem) %>" CommandName="Edit" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="width: 100%; height: 15px;">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View8" runat="server">
                                                    <table cellpadding="2" style="width: 100%;">
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    RAL
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle" align="right" width="40%">
                                                                Data de Vencimento:
                                                            </td>
                                                            <td align="left" valign="middle" width="60%">
                                                                <asp:Label ID="lblDataVencimentoRAL" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" width="40%">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" valign="middle" width="60%">
                                                                <asp:LinkButton ID="lkbVencimentoRAL" runat="server" Font-Size="8pt" OnClick="lkbVencimentoRAL_Click"
                                                                    OnInit="lkbVencimentoLicenciamento_Init" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                                                    onmouseover="tooltip.show('Exibe o histórico de vencimentos do RAL')">Abrir Vencimentos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" width="40%">
                                                                &nbsp;</td>
                                                            <td align="left" valign="middle" width="60%">
                                                                <asp:Button ID="btnUploadRAL" runat="server" CssClass="ButtonUpload" 
                                                                    Height="22px" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" 
                                                                    onmouseover="tooltip.show('Adiciona arquivos ao RAL')" Text="    Upload" OnClick="btnUploadRAL_Click" OnInit="btnUploadRAL_Init" 
                                                                    />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="30" valign="middle" colspan="2">
                                                                <asp:Button ID="btnRenovarValidadeRAL" runat="server" CssClass="Button" OnClick="btnRenovarValidadeRAL_Click"
                                                                    OnClientClick="tooltip.hide();" OnInit="btnRenovar_Init" Text="Renovar" Width="150px"
                                                                    OnPreRender="btnRenovarValidadeRAL_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova o vencimento do RAL')" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" height="30" valign="middle">
                                                                <div class="barra_titulos">
                                                                    <strong>
                                                                        <asp:ImageButton ID="ibtnAddLicencaProcesso0" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                            OnClick="ibtnAddLicencaProcesso0_Click" OnInit="ibtnAddLicencaProcesso0_Init"
                                                                            OnClientClick="tooltip.hide();" OnPreRender="ibtnAddLicencaProcesso0_PreRender"
                                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de vencimento do RAL')" />
                                                                        &nbsp;</strong>Notificações</div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="grvNotificacoesRAL" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" Width="100%" OnRowDeleting="grvNotificacoesRAL_RowDeleting">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                        <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                                                        <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnExcluirRalGrid" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif" Visible="<%# BindVisibleEdicaoNotificacaoRal(Container.DataItem) %>" 
                                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="1px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View9" runat="server">
                                                    <table cellpadding="2" style="width: 100%;">
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Guia de Utilização
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Data de Requerimento:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:Label ID="lblDataRequerimentoGuiaUtilizacao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Data de Emissão:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:Label ID="lblDataEmissaoGuiaUtilizacao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Data de Validade:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:Label ID="lblDataValidadeGuiaUtilizacao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Data Limite para Renovação:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:Label ID="lblDataLimiteGuiaUtilizacao" runat="server" Font-Bold="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Data da próxima Notificação:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:Label ID="lblDataUltimaNotificacaoGuiaUtilizacao" runat="server" Font-Bold="True"></asp:Label>
                                                                &nbsp;-
                                                                <asp:LinkButton ID="lkbVenc9" runat="server" Font-Size="8pt" OnInit="lkbVenc_Init"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza todas as notificações')"
                                                                    OnClick="lkbVenc9_Click">Ver mais</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                Anexos:
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:LinkButton ID="lbtnDownloadGUIA" runat="server" 
                                                                    OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os arquivos anexados na Guia de Utilização')" OnInit="lbtnDownloadGUIA_Init" OnClick="lbtnDownloadGUIA_Click">Ver Arquivos</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" width="40%">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" width="60%">
                                                                <asp:Button ID="btnRenovarValidadeGuia" runat="server" CssClass="Button" OnClick="btnRenovarValidadeGuia_Click"
                                                                    OnClientClick="tooltip.hide();" OnInit="btnRenovar_Init" Text="Renovar" Width="150px"
                                                                    OnPreRender="btnRenovarValidadeGuia_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Renova a validade da Guia de Utilização')" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div class="barra_titulos">
                                                                    Condicionantes
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="grvExigenciasGuiaUtilizacao" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                    GridLines="None" Width="100%" OnInit="grvExigenciasGuiaUtilizacao_Init" OnRowCancelingEdit="grvExigenciasGuiaUtilizacao_RowCancelingEdit" OnRowEditing="grvExigenciasGuiaUtilizacao_RowEditing">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DataPublicacao" HeaderText="Data de Publicação" DataFormatString="{0:d}" />
                                                                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                        <asp:BoundField DataField="DiasPrazo" HeaderText="Dias de Prazo" />
                                                                        <asp:TemplateField HeaderText="Vencimento">
                                                                            <ItemTemplate>
                                                                                <%# BindDatadeVencimento(Container.DataItem)%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Renovar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnRenovar" runat="server" CommandName="Cancel" Enabled="<%# BindEnableRenovacao(Container.DataItem) %>" Visible="<%# BindVisibleEdicaoExigenciaGuiaUtilizacao(Container.DataItem) %>"
                                                                                    ImageUrl="<%# BindImagemRenovacao(Container.DataItem) %>" ToolTip="<%# BindToolTipoRenovacao(Container.DataItem) %>"
                                                                                    OnPreRender="ibtnRenovar_PreRender6" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();"
                                                                                    onmouseover="tooltip.show('Renova a exigência da Guia de Utilização, caso ela seja periódica')" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Arquivo">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ibtnAbrirDownloadExigenciasGuia" runat="server" ImageUrl="~/imagens/icone_anexo.png" Visible="<%# BindVisibleEdicaoExigenciaGuiaUtilizacao(Container.DataItem) %>"
                                                                                    ToolTip="Ver Arquivos" CommandName="Edit" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>                                                
                                            </asp:MultiView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="trvProcessos" EventName="SelectedNodeChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ibtnAtualizarArvore" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ibtnCarregarArvore" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lkbRegimeExcluir" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lkbOpcoesProcessoExcluir" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lkbOpcoesGuiaExcluir" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lkbOpcoesRALExcluir" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlClientes" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Barra">
    ANM - AGÊNCIA NACIONAL DE MINERAÇÃO 
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="popups">
    <div id="divPopUpGuia" style="width: 596px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div>
            <div id="fecharGuia" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Guia de Utilização</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopGuiaUtilizacao" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" width="40%">
                                    Número da Guia:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxNumeroGuia" runat="server" CssClass="TextBox" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Data em que a Guia de Utilização foi requerida')"
                                        Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    Data de Requerimento:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataRequerimentoGuia" runat="server" CssClass="TextBox" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Data em que a Guia de Utilização foi requerida')"
                                        Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataRequerimentoGuia_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataRequerimentoGuia">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    Data de Emissão:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataEmissaoGuia" runat="server" CssClass="TextBox" Width="100px"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data em que a Guia de Utilização foi emitida')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataEmissaoGuia_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy" TargetControlID="tbxDataEmissaoGuia">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    Anexo:
                                </td>
                                <td>
                                    <asp:Button ID="btnUploadGuiaUtilizacao" runat="server" CssClass="ButtonUpload" Height="22px" Text="    Inserir / Visualizar Anexos" Width="170px" 
                                        OnClick="btnUploadGuiaUtilizacao_Click" OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Adiciona o arquivo digital da Guia de Utilização')" OnInit="btnUploadGuiaUtilizacao_Init" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbObservacoes" runat="server" Font-Size="8pt" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Visualiza o histórico de registros')" OnClick="lkbObservacoes_Click"
                                        OnInit="lkbObservacoes_Init">Histórico de Registros</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>
                                        <asp:ImageButton ID="ibtnExigenciaGuia" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                            OnClick="ibtnExigenciaGuia_Click" OnClientClick="tooltip.hide();" OnInit="ibtnExigenciaGuia_Init"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona exigências à Guia de Utlização')" />
                                        Condicionantes</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvExigenciasGuia" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="3" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" PageSize="2" Width="100%" OnInit="grvExigenciasGuia_Init" OnPageIndexChanging="grvExigenciasGuia_PageIndexChanging"
                                            OnRowDeleting="grvExigenciasGuia_RowDeleting" OnRowEditing="grvExigenciasGuia_RowEditing">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DataPublicacao" DataFormatString="{0:d}" HeaderText="Data de Publicação" />
                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                <asp:BoundField DataField="DiasPrazo" HeaderText="Dias de Prazo" />
                                                <asp:TemplateField HeaderText="Data de Vencimento">
                                                    <ItemTemplate>
                                                        <%# BindVencimentoRequerimentoPesquisa(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgArquivo4" runat="server" AlternateText="." CommandName="Edit"
                                                            ImageUrl="~/imagens/icone_editar.png" PostBackUrl="<%# BindAnexoRequerimentoPesquisa(Container.DataItem)%>"
                                                            OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os dados da exigência para edição')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarExigenciasGuiaUtilizacao" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir14" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir14_PreRender" ToolTip="Excluir" />
                                                        <input id="ckbSelecionar6" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarExigenciasGuiaUtilizacao')" />
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
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Data de Vencimento:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxDataVencimentoGuia" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataVencimentoGuia_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataVencimentoGuia">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Status:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatusGuiaUtilizacao" runat="server" CssClass="DropDownList"
                                        DataTextField="Nome" DataValueField="Id" Width="170px" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Situação atual da Guia de Utlização')">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:LinkButton ID="lkbStstusGuia" runat="server" Font-Size="8pt" OnClick="lkbStstusGuia_Click"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico de todos os vencimentos da Guia de Utlização')"
                                        OnInit="lkbStstusGuia_Init">Visualizar Vencimentos</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:ImageButton ID="ibtnNotificacoesPopGuia" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                        OnClick="ibtnNotificacoesPopGuia_Click" OnInit="ibtnNotificacoesPopGuia_Init1"
                                        OnClientClick="tooltip.hide();" Style="width: 20px" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Adiciona notificações por e-mail com aviso de vencimento')" />
                                    <strong>&nbsp; Notificações:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvNotificacoesGuia" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" PageSize="2" Width="100%" OnInit="grvNotificacoesGuia_Init"
                                            OnPageIndexChanging="grvNotificacoesGuia_PageIndexChanging" OnRowDeleting="grvNotificacoesGuia_RowDeleting">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesGuiaUtilizacao" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir30" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir30_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" />
                                                        <input id="ckbSelecionar30" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesGuiaUtilizacao')" />
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
                                <td align="right" colspan="2">
                                    <asp:Label ID="lblResultNotificacoesGuia" runat="server"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSalvarGuia" runat="server" CssClass="Button" OnClick="btnSalvarGuia_Click"
                                        Text="Salvar" Width="170px" OnInit="btnSalvarGuia_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpSelecionarRegime" style="width: 596px; display: block; top: 0px;
        left: 0px;" class="pop_up">
        <div>
            <div id="fecharSelecionarRegime" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Seleção</div>
        </div>
        <div>
            <div align="center">
                <asp:UpdatePanel ID="upSelecionarRegime" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="False">
                    <ContentTemplate>
                        <asp:MultiView ID="mvBotoesRegime" runat="server">
                            <asp:View ID="View10" runat="server">
                                <div class="group_botoes">
                                    <br />
                                    <span style="font-size: 11px;">Autorização de Pesquisa</span><br />
                                    <br />
                                    <asp:Button ID="btnNovoRequerimentoPesquisa" runat="server" CssClass="Button" OnClick="btnNovoRequerimentoPesquisa_Click"
                                        OnClientClick="tooltip.hide();" OnInit="btnNovoRequerimentoPesquisa_Init" Text="Requerimento"
                                        Width="250px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cria a fase de Requerimento de Pesquisa no regime de Autorização de Pesquisa')" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnNovoAlvaraPesquisa" runat="server" CssClass="Button" OnClick="btnNovoAlvaraPesquisa_Click"
                                        OnClientClick="tooltip.hide();" OnInit="btnNovoAlvaraPesquisa_Init" Text="Alvará"
                                        Width="250px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cria a fase de Alvará de Pesquisa no regime de Autorização de Pesquisa')" />
                                </div>
                                <div class="group_botoes">
                                    <br />
                                    <span style="font-size: 11px;">Concessão de Lavra</span><br />
                                    <br />
                                    <asp:Button ID="btnNovoRequerimentoLavra" runat="server" CssClass="Button" OnClick="btnNovoRequerimentoLavra_Click"
                                        OnClientClick="tooltip.hide();" OnInit="btnNovoRequerimentoLavra_Init" Text="Requerimento"
                                        Width="250px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cria a fase de Requerimento de Pesquisa no regime de Concessão de Lavra')" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnNovoConcessaoLavra" runat="server" CssClass="Button" OnClick="btnNovoConcessaoLavra_Click"
                                        OnClientClick="tooltip.hide();" OnInit="btnNovoConcessaoLavra_Init" Text="Concessão"
                                        Width="250px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cria a fase de Concessão no regime de Concessão de Lavra')" />
                                </div>
                            </asp:View>
                            <asp:View ID="View11" runat="server">
                                <asp:Button ID="btnNovoExtracao" runat="server" CssClass="Button" OnClick="btnNovoExtracao_Click"
                                    OnClientClick="tooltip.hide();" OnInit="btnNovoExtracao_Init" Text="Extração"
                                    Width="250px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cria o regime de Extração')" />
                            </asp:View>
                            <asp:View ID="View12" runat="server">
                                <asp:Button ID="btnNovoLicenciamento" runat="server" CssClass="Button" OnClick="btnNovoLicenciamento_Click"
                                    OnClientClick="tooltip.hide();" OnInit="btnNovoLicenciamento_Init" Text="Licenciamento"
                                    Width="250px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Cria o regime de Licenciamento')" />
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpCadastroProcessoDNPM" style="width: 596px; display: block; top: 0px;
        left: 0px;" class="pop_up">
        <div>
            <div id="fecharCadastroProcessoDNPM" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Processo ANM</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upCadastrarProcessosDNPM" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="False">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" width="30%">
                                    Regime:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRegimeDNPM" runat="server" CssClass="DropDownList" Width="200px"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Ao escolher o regime inicial do processo a ser cadastrado, este já será criado dentro do processo. Depois é possível inserir os dados do regime dentro do mesmo. Um regime de Autorização de Pesquisa começara como Requerimento de Pesquisa e pode posteriormente ser evoluído para Alvará de Pesquisa, Requerimento de lavra e Concessão de Lavra')">
                                        <asp:ListItem>Autorização de pesquisa</asp:ListItem>
                                        <asp:ListItem>Extração</asp:ListItem>
                                        <asp:ListItem>Licenciamento</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    Número do Processo:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxNumeroProcessoDNPM" onfocus="$('.txtmaskproc').mask('999999/9999', {clearIfNotMatch: true})" runat="server" CssClass="TextBox txtmaskproc" Width="100px"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Data de Abertura:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataAberturaDNPM" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataAberturaDNPM_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="tbxDataAberturaDNPM">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Empresa:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEmpresaDNPM" runat="server" Width="300px" CssClass="DropDownList"
                                        AutoPostBack="True" OnInit="ddlEmpresaDNPM_Init" OnSelectedIndexChanged="ddlEmpresaDNPM_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Consultoria:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlConsultoriaDNPM" runat="server" Width="170px" CssClass="DropDownList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Apelido da Área:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxIdentificacaoAreaDNPM" runat="server" CssClass="TextBox" Width="300px"
                                        MaxLength="240"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Área (Hectares):
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxTamanhoAreaDNPM" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Substâncias:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxSubstancia" runat="server" CssClass="TextBox" 
                                        MaxLength="240" Width="202px"></asp:TextBox>
                                    <asp:LinkButton ID="lkbSubstancias" runat="server" Font-Size="8pt" OnClick="lkbSubstancias_Click"
                                        OnInit="lkbSubstancias_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Nova Substância')">Nova Substância</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Endereço:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxEnderecoDNPM" runat="server" CssClass="TextBox" Width="350px"
                                        MaxLength="240"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Estado:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEstadoProcessoDNPM" runat="server" Width="170px" OnSelectedIndexChanged="ddlEstadoProcessoDNPM_SelectedIndexChanged"
                                        CssClass="DropDownList" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Cidade:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCidadeProcessoDNPM" runat="server" Width="170px" CssClass="DropDownList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Anexo:
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upUploadProcesso" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnUploadProcessoDNPM" runat="server" CssClass="ButtonUpload" Height="22px"
                                                OnClick="btnUploadProcessoDNPM_Click" Text="     Inserir / Visualizar Anexos" Width="170px" OnInit="btnUploadProcessoDNPM_Init" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Detalhamento:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxObservacoesProcessoDNPM" runat="server" CssClass="TextBox" TextMode="MultiLine"
                                        Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                   &nbsp;
                                </td>
                                <td style="padding-top:4px;">
                                    <asp:Button ID="btnAbrirContratos" runat="server" CssClass="Button"
                                        Text="Contratos" Width="170px" onclick="btnAbrirContratos_Click" 
                                        Visible="False" oninit="btnAbrirContratos_Init" />
                                        
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>
                                        <asp:ImageButton ID="ibtnAddLicencaProcesso" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                            OnClientClick="tooltip.hide();" OnClick="ibtnAddLicencaProcesso_Click" OnInit="ibtnAddLicencaProcesso_Init"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Associa licenças ambientais a este processo ANM')" />
                                        Licenças Associadas:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" bgcolor="White">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvLicencasDNPM" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="2" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" OnRowDeleting="grvLicencasDNPM_RowDeleting" PageSize="2" Width="100%"
                                            OnInit="grvLicencasDNPM_Init" 
                                            OnPageIndexChanging="grvLicencasDNPM_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                <asp:TemplateField HeaderText="Órgão">
                                                    <ItemTemplate>
                                                        <%#
    BindOrgao(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo">
                                                    <ItemTemplate>
                                                        <%# BindTipo(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DataRetirada" DataFormatString="{0:d}" HeaderText="Retirada" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarLicencasProcessoDnpm" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir4" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir4_PreRender" ToolTip="Excluir" />
                                                        <input id="ckbSelecionar5" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarLicencasProcessoDnpm')" />
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
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" colspan="2" style="padding-top:5px;">
                                    <asp:Button ID="btnSalvarProcessoDNPM" runat="server" CssClass="Button" OnClick="btnSalvarProcessoDNPM_Click"
                                        Text="Salvar" Width="170px" OnInit="btnSalvarProcessoDNPM_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEstadoProcessoDNPM" EventName="SelectedIndexChanged" />
                    </Triggers>
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
                                <td align="right">
                                    &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
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
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkDeletarContratos" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluirContratos" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluirContratos_PreRender" ToolTip="Excluir" />
                                                        <input id="ckbSelecionar5" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkDeletarContratos')" />
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
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnSelecionarMaisContratos" runat="server" CssClass="Button" OnClick="btnSelecionarMaisContratos_Click"
                                        Text="Selecionar Mais" Width="170px" 
                                        OnInit="btnSelecionarMaisContratos_Init" />
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
                                        <asp:LinkButton ID="lxbPesquisarContratos" runat="server" 
                                        OnClick="lxbPesquisarContratos_Click"><img alt="" src="../imagens/visualizar20x20.png" style="border:0px"/> Pesquisar</asp:LinkButton>
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
    <div id="divPopUpRequerimentoPesquisa" style="width: 700px; display: block; top: 0px;
        left: 0px;" class="pop_up">
        <div id="fecharRequerimentoPesquisa" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Cadastro de Requerimento de Pesquisa</div>
        <div>
            <div>
                <asp:UpdatePanel ID="upRequerimentoPesquisa" runat="server" UpdateMode="Conditional"
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
                                    Data de Entrada:
                                </td>
                                <td width="65%">
                                    <asp:TextBox ID="tbxDataEntradaRequerimentoPesquisa" runat="server" CssClass="TextBox"
                                        Width="100px" Enabled="False" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data de abertura do processo no regime de Autorização de Pesquisa')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataEntradaRequerimentoPesquisa_CalendarExtender" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="tbxDataEntradaRequerimentoPesquisa">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Anexo:
                                </td>
                                <td>
                                    <asp:Button ID="btnUploadRequerimentopesquisa" runat="server" CssClass="ButtonUpload"
                                        OnClientClick="tooltip.hide();" Height="22px" 
                                        Text="      Inserir / Visualizar Anexos" Width="170px" OnClick="btnUploadRequerimentopesquisa_Click"
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Adiciona arquivos digitalizados ao requerimento de pesquisa')" OnInit="btnUploadRequerimentopesquisa_Init" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    <asp:LinkButton ID="lkbObservacoesRequerimento" runat="server" Font-Size="8pt" 
                                        OnClick="lkbObservacoesRequerimento_Click" OnInit="lkbObservacoes_Init" 
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>
                                        <asp:ImageButton ID="btnAdicionarExigenciaRequerimentoPesquisa" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                            OnClientClick="tooltip.hide();" OnClick="btnAdicionarExigenciaRequerimentoPesquisa_Click"
                                            OnInit="btnAdicionarExigenciaRequerimentoPesquisa_Init" onmouseout="tooltip.hide();"
                                            onmouseover="tooltip.show('Adiciona exigências ao requerimento de pesquisa')" />
                                        Exigências:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvExigenciasRequerimentoPesquisa" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="None" OnRowDeleting="grvExigenciasRequerimentoPesquisa_RowDeleting"
                                            PageSize="4" Width="100%" OnPageIndexChanging="grvExigenciasRequerimentoPesquisa_PageIndexChanging"
                                            OnInit="grvExigenciasRequerimentoPesquisa_Init" OnRowEditing="grvExigenciasRequerimentoPesquisa_RowEditing">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DataPublicacao" DataFormatString="{0:d}" HeaderText="Data de Publicação" />
                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                <asp:BoundField DataField="DiasPrazo" HeaderText="Dias
    de Prazo" />
                                                <asp:TemplateField HeaderText="Data de Vencimento">
                                                    <ItemTemplate>
                                                        <%# BindVencimentoRequerimentoPesquisa(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgArquivo0" runat="server" AlternateText="." CommandName="Edit"
                                                            ImageUrl="~/imagens/icone_editar.png" PostBackUrl="<%# BindAnexoRequerimentoPesquisa(Container.DataItem)%>"
                                                            OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os dados da exigência para edição')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarExigenciasRequerimentoPesquisa" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir5" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir5_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as exigências selecionadas')" />
                                                        <input id="ckbSelecionar1" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarExigenciasRequerimentoPesquisa')" />
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
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="hfRequerimentoPesquisa" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSalvarRequerimentoPesquisa" runat="server" CssClass="Button" OnClick="btnSalvarRequerimentoPesquisa_Click"
                                        Text="Salvar" Width="170px" OnInit="btnSalvarRequerimentoPesquisa_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grvExigenciasRequerimentoPesquisa" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpAlvaraPesquisa" style="width: 770px; display: block; top: 0px; left: 0px;" class="pop_up">
        <div>
            <div id="fecharCadastroAlvaraPesquisa" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Alvará de Pesquisa</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upAlvaraPesquisa" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    Número:
                                </td>
                                <td width="60%">
                                    <asp:TextBox ID="tbxNumeroAlvaraPesquisa" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Data de Publicação:
                                </td>
                                <td width="50%">
                                    <asp:TextBox ID="tbxDataPublicacaoAlvaraPesquisa" runat="server" CssClass="TextBox"
                                        Width="100px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data de publicação do Alvará de Pesquisa')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataPublicacaoAlvaraPesquisa_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataPublicacaoAlvaraPesquisa">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Relatório Entregue em:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataEntragaRelatorioPesquisa" runat="server" CssClass="TextBox"
                                        Width="100px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data em que o Relatório de Pesquisa foi entregue')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataEntragaRelatorioPesquisa_CalendarExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxDataEntragaRelatorioPesquisa" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Relatório Aprovado em:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataAprovacaoRelatorioPesquisa" runat="server" CssClass="TextBox"
                                        Width="100px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data em que o Relatório de Pesquisa foi aprovado')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataAprovacaoRelatorioPesquisa_CalendarExtender" Format="dd/MM/yyyy"
                                        runat="server" Enabled="True" TargetControlID="tbxDataAprovacaoRelatorioPesquisa">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Anexo:
                                </td>
                                <td>
                                    <asp:Button ID="btnUploadAlvaraPesquisa" runat="server" CssClass="ButtonUpload" Height="22px"
                                        Text="     Inserir / Visualizar Anexos" Width="170px" 
                                        OnClick="btnUploadAlvaraPesquisa_Click" onmouseout="tooltip.hide();"                                        
                                        onmouseover="tooltip.show('Adiciona arquivos digitalizados ao Alvará de Pesquisa')" OnInit="btnUploadAlvaraPesquisa_Init" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbObservacoesAlvara" runat="server" Font-Size="8pt" OnClick="lkbObservacoesAlvara_Click"
                                        OnInit="lkbObservacoes_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>
                                        <asp:ImageButton ID="btnAdicionarExigenciaAlvaraPesquisa" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                            OnClick="btnAdicionarExigenciaAlvaraPesquisa_Click" OnInit="btnAdicionarExigenciaAlvaraPesquisa_Init"
                                            onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Adiciona exigências ao Alvará de Pesquisa')" />
                                        Exigências:</strong>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids" style="margin-bottom: 4px;">
                                        <asp:GridView ID="grvExigenciasAlvaraPesquisa" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="None" OnPageIndexChanging="grvExigenciasAlvaraPesquisa_PageIndexChanging"
                                            OnRowDeleting="grvExigenciasAlvaraPesquisa_RowDeleting" PageSize="2" Width="100%"
                                            OnInit="grvExigenciasAlvaraPesquisa_Init1" OnRowEditing="grvExigenciasAlvaraPesquisa_RowEditing">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DataPublicacao" DataFormatString="{0:d}" HeaderText="Data de Publicação" />
                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                <asp:BoundField DataField="DiasPrazo" HeaderText="Dias
 de Prazo" />
                                                <asp:TemplateField HeaderText="Data de Vencimento">
                                                    <ItemTemplate>
                                                        <%# BindVencimentoRequerimentoPesquisa(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgArquivo6" runat="server" AlternateText="." CommandName="Edit"
                                                            ImageUrl="~/imagens/icone_editar.png" PostBackUrl="<%# BindAnexoRequerimentoPesquisa(Container.DataItem)%>"
                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os dados da exigência para edição')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarExigenciasAlvaraPesquisa" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir16" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir16_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as exigências selecionadas')" />
                                                        <input id="ckbSelecionar8" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarExigenciasAlvaraPesquisa')" />
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
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TabContainer ID="TabContainer4" runat="server" ActiveTabIndex="6" Width="100%"
                                        Height="120px" ScrollBars="Auto">
                                        <asp:TabPanel ID="TabPanel10" runat="server" HeaderText="Validade">
                                            <ContentTemplate>
                                                <div style="width: 100%; height: 10px;">
                                                </div>
                                                <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="right">
                                                            Validade (anos):
                                                        </td>
                                                        <td width="60%">
                                                            <asp:TextBox ID="tbxValidadeAlvaraPesquisa" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" valign="top">
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnAddNotificacaoValidadeAlvaraPesquisa4" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                    OnClientClick="tooltip.hide();" OnClick="ibtnAddNotificacaoValidadeAlvaraPesquisa_Click"
                                                                    OnInit="ibtnAddNotificacaoValidadeAlvaraPesquisa_Init" onmouseout="tooltip.hide();"
                                                                    onmouseover="tooltip.show('Adiciona notificações de aviso de vencimento do Alvará de Pesquisa')" />
                                                                Notificações:<div class="container_grids">
                                                                    <asp:GridView ID="grvAlvaraPesquisaValidadeNotificacoesPopUp" runat="server" AllowPaging="True"
                                                                        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                                                        ForeColor="#333333" GridLines="None" PageSize="2" Width="100%" OnInit="grvAlvaraPesquisaValidadeNotificacoesPopUp_Init"
                                                                        OnPageIndexChanging="grvAlvaraPesquisaValidadeNotificacoesPopUp_PageIndexChanging"
                                                                        OnRowDeleting="grvAlvaraPesquisaValidadeNotificacoesPopUp_RowDeleting">
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                            <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                            <asp:TemplateField HeaderText="Excluir">
                                                                                <HeaderTemplate>
                                                                                    <asp:ImageButton ID="ibtnExcluir31" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                        OnPreRender="ibtnExcluir31_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" /><input
                                                                                            id="ckbSelecionar31" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesValidadeAlvaraPesquisa')" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesValidadeAlvaraPesquisa" />
                                                                                </ItemTemplate>
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
                                                            </strong>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanel11" runat="server" HeaderText="Comunicar Início da Pesquisa">
                                            <ContentTemplate>
                                                <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="right">
                                                            Prazo (Dias):
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPrazoNotificacaoPesquisa" runat="server" CssClass="DropDownList"
                                                                Width="60px">
                                                                <asp:ListItem>60</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnAddNotificacaoInicioPesquisa0" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                    OnClick="ibtnAddNotificacaoInicioPesquisa0_Click" OnInit="ibtnAddNotificacaoInicioPesquisa0_Init"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de aviso para comunicação do inpicio da pesquisa')" />Notificações:</strong><div
                                                                        class="container_grids">
                                                                        <asp:GridView ID="grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0" runat="server"
                                                                            AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                                                            EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="2"
                                                                            Width="100%" OnInit="grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0_Init"
                                                                            OnPageIndexChanging="grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0_PageIndexChanging"
                                                                            OnRowDeleting="grvAlvaraPesquisaNotificacaoPesquisaNotificacoes0_RowDeleting">
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                                <asp:TemplateField HeaderText="Excluir">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificicacoesPesquisaNotificacaoAlvaraPesquisa" /></ItemTemplate>
                                                                                    <HeaderTemplate>
                                                                                        <asp:ImageButton ID="ibtnExcluir32" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                            OnPreRender="ibtnExcluir32_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')"
                                                                                            Width="20px" /><input id="ckbSelecionar32" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificicacoesPesquisaNotificacaoAlvaraPesquisa')" /></HeaderTemplate>
                                                                                    <HeaderStyle Width="45px" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
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
                                        <asp:TabPanel ID="TabPanel12" runat="server" HeaderText="Taxa Anual
    por Hectare">
                                            <ContentTemplate>
                                                <div style="width: 100%; height: 10px;">
                                                </div>
                                                <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="right" width="40%">
                                                            Data Vencimento:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDataTaxaHectare" runat="server" Font-Bold="True">Data gerada automaticamente</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="40%">
                                                            Status:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlEstatusTaxaAnual" runat="server" CssClass="DropDownList"
                                                                DataTextField="Nome" DataValueField="Id" OnSelectedIndexChanged="ddlEstadoLicenca_SelectedIndexChanged1"
                                                                Width="170px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="40%">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lkbStstusTaxaAnual" runat="server" Font-Size="8pt" OnClick="lkbStstusTaxaAnual_Click"
                                                                OnClientClick="tooltip.hide();" OnInit="lkbStstusTaxaAnual_Init" onmouseout="tooltip.hide();"
                                                                onmouseover="tooltip.show('Exibe o histórico de vencimentos da taxa anual por hectare')">Visualizar Vencimentos</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnAddNotificacaoTaxaAnualHectareAlvaraPesquisa" runat="server"
                                                                    ImageUrl="~/imagens/icone_adicionar.png" OnClick="ibtnAddNotificacaoTaxaAnualHectareAlvaraPesquisa_Click"
                                                                    OnClientClick="tooltip.hide();" OnInit="ibtnAddNotificacaoTaxaAnualHectareAlvaraPesquisa_Init"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de aviso para pagamento da taxa anual por hectare')" />Notificações:</strong><div
                                                                        class="container_grids">
                                                                        <asp:GridView ID="grvAlvaraPesquisaTaxaAnualHectareNotificacoes0" runat="server"
                                                                            AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                                                            EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="2"
                                                                            Width="100%" OnInit="grvAlvaraPesquisaTaxaAnualHectareNotificacoes0_Init" OnPageIndexChanging="grvAlvaraPesquisaTaxaAnualHectareNotificacoes0_PageIndexChanging"
                                                                            OnRowDeleting="grvAlvaraPesquisaTaxaAnualHectareNotificacoes0_RowDeleting">
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                                <asp:TemplateField HeaderText="Excluir">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesTaxaHectareAlvaraPesquisa" /></ItemTemplate>
                                                                                    <HeaderTemplate>
                                                                                        <asp:ImageButton ID="ibtnExcluir33" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                            OnPreRender="ibtnExcluir33_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" /><input
                                                                                                id="ckbSelecionar33" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesTaxaHectareAlvaraPesquisa')"
                                                                                                type="checkbox" /></HeaderTemplate>
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
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanel13" runat="server" HeaderText="Requerimento
    de Lavra">
                                            <ContentTemplate>
                                                <div style="width: 100%; height: 10px;">
                                                </div>
                                                <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="right">
                                                            Data Limite:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDataLimiteRequerimentoLavra" runat="server" Font-Bold="True">Data gerada automaticamente</asp:Label>&nbsp;=
                                                            1 Ano após a aprovação do relatório de pesquisa.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnAddNotificacaoRequerimentoLavraAlvaraPesquisa" runat="server"
                                                                    ImageUrl="~/imagens/icone_adicionar.png" OnClick="ibtnAddNotificacaoRequerimentoLavraAlvaraPesquisa_Click"
                                                                    OnClientClick="tooltip.hide();" OnInit="ibtnAddNotificacaoRequerimentoLavraAlvaraPesquisa_Init"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de aviso para requerimento de lavra')" />Notificações:
                                                            </strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2">
                                                            <div class="container_grids">
                                                                <asp:GridView ID="grvAlvaraPesquisaRequerimentoLavraNotificacoes0" runat="server"
                                                                    AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                                                    EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="2"
                                                                    Width="100%" OnInit="grvAlvaraPesquisaRequerimentoLavraNotificacoes0_Init" OnPageIndexChanging="grvAlvaraPesquisaRequerimentoLavraNotificacoes0_PageIndexChanging"
                                                                    OnRowDeleting="grvAlvaraPesquisaRequerimentoLavraNotificacoes0_RowDeleting">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                        <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesRequirimentoLavraAlvaraPesquisa" /></ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <asp:ImageButton ID="ibtnExcluir33" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                    OnPreRender="ibtnExcluir33_PreRender1" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" /><input
                                                                                        id="ckbSelecionar33" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesRequirimentoLavraAlvaraPesquisa')"
                                                                                        type="checkbox" /></HeaderTemplate>
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
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanel14" runat="server" HeaderText="Requerimento
    de LP Poligonal">
                                            <HeaderTemplate>
                                                LP Poligonal
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div style="width: 100%; height: 10px;">
                                                </div>
                                                <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="center">
                                                            &nbsp;<asp:Label ID="lblDataLimiteRequerimentoLpTotal" runat="server" Font-Bold="True">Relatório ainda não aprovado.</asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnAddNotificacaoValidadeLPTotal" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                    OnClick="ibtnAddNotificacaoValidadeLPTotal_Click" OnClientClick="tooltip.hide();"
                                                                    OnInit="ibtnAddNotificacaoValidadeLPTotal_Init" onmouseout="tooltip.hide();"
                                                                    onmouseover="tooltip.show('Adiciona notificações de aviso para requerimento da LP Poligonal')" />Notificações:</strong><div
                                                                        class="container_grids">
                                                                        <asp:GridView ID="grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp" runat="server"
                                                                            AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id"
                                                                            EnableModelValidation="True" ForeColor="#333333" GridLines="None" PageSize="2"
                                                                            Width="100%" OnInit="grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp_Init"
                                                                            OnPageIndexChanging="grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp_PageIndexChanging"
                                                                            OnRowDeleting="grvAlvaraPesquisaRequerimentoLPTotalNotificacoesPopUp_RowDeleting">
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                                <asp:TemplateField HeaderText="Excluir">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesLPPoligonalAlavaraPesquisa" /></ItemTemplate>
                                                                                    <HeaderTemplate>
                                                                                        <asp:ImageButton ID="ibtnExcluir34" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                            OnPreRender="ibtnExcluir34_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" /><input
                                                                                                id="ckbSelecionar34" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesLPPoligonalAlavaraPesquisa')" /></HeaderTemplate>
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
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="tbsDipem" runat="server" HeaderText="DIPEM">
                                            <HeaderTemplate>
                                                DIPEM
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="right">
                                                            Validade:
                                                        </td>
                                                        <td width="60%">
                                                            <asp:Label ID="lblDataDIPEM" runat="server" Font-Bold="True">Data gerada automaticamente</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Status:
                                                        </td>
                                                        <td width="60%">
                                                            <asp:DropDownList ID="ddlEstatusDipem" runat="server" CssClass="DropDownList" DataTextField="Nome"
                                                                DataValueField="Id" OnSelectedIndexChanged="ddlEstadoLicenca_SelectedIndexChanged1"
                                                                Width="170px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                        <td width="60%">
                                                            <asp:LinkButton ID="lkbStstusDipem" runat="server" Font-Size="8pt" OnClick="lkbStstusDipem_Click"
                                                                OnClientClick="tooltip.hide();" OnInit="lkbStstusDipem_Init" onmouseout="tooltip.hide();"
                                                                onmouseover="tooltip.show('Exibe o histórico de vencimentos do DIPEM')">Visualizar Vencimentos</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnAddNotificacaoDIPEM" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                    OnClick="ibtnAddNotificacaoDIPEM_Click" OnClientClick="tooltip.hide();" OnInit="ibtnAddNotificacaoDIPEM_Init"
                                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de aviso de vencimento do DIPEM')" />Notificações:</strong><div
                                                                        class="container_grids">
                                                                        <asp:GridView ID="grvAlvaraPesquisaDIPEM" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                            GridLines="None" PageSize="2" Width="100%" OnInit="grvAlvaraPesquisaDIPEM_Init"
                                                                            OnPageIndexChanging="grvAlvaraPesquisaDIPEM_PageIndexChanging" OnRowDeleting="grvAlvaraPesquisaDIPEM_RowDeleting">
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                                <asp:TemplateField HeaderText="Excluir">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesDipemAlvaraPesquisa" /></ItemTemplate>
                                                                                    <HeaderTemplate>
                                                                                        <asp:ImageButton ID="ibtnExcluir35" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                            OnPreRender="ibtnExcluir35_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" /><input
                                                                                                id="ckbSelecionar35" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesDipemAlvaraPesquisa')" /></HeaderTemplate>
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
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanelRenuncia" runat="server" HeaderText="Renúncia">
                                            <ContentTemplate>
                                                  <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="right">
                                                            Data limite para renúncia sem apresentar relatório:
                                                        </td>
                                                        <td width="60%">
                                                            <asp:Label ID="lblDataRenuncia" runat="server" Font-Bold="True">Data gerada automaticamente</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <strong>
                                                                <asp:ImageButton ID="ibtnNotificacaoRenuncia" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                                    OnClick="ibtnNotificacaoRenuncia_Click" 
                                                                OnClientClick="tooltip.hide();" OnInit="ibtnAddNotificacaoDIPEM_Init"
                                                                    onmouseout="tooltip.hide();" 
                                                                onmouseover="tooltip.show('Adiciona notificações de aviso de vencimento do DIPEM')" />Notificações:</strong><div
                                                                        class="container_grids">
                                                                        <asp:GridView ID="gdvRenuncia" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                                            GridLines="None" PageSize="2" Width="100%" OnInit="grvAlvaraPesquisaDIPEM_Init"
                                                                            OnPageIndexChanging="gdvRenuncia_PageIndexChanging" 
                                                                            OnRowDeleting="gdvRenuncia_RowDeleting">
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                                <asp:TemplateField HeaderText="Excluir">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesDipemAlvaraPesquisa" /></ItemTemplate>
                                                                                    <HeaderTemplate>
                                                                                        <asp:ImageButton ID="ibtnExcluir35" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                            OnPreRender="ibtnExcluir35_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" /><input
                                                                                                id="ckbSelecionar35" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesDipemAlvaraPesquisa')" /></HeaderTemplate>
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
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    </asp:TabContainer>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="tbnSalvarAlvaraDePesquisa" runat="server" CssClass="Button" Text="Salvar"
                                        Width="170px" OnClick="tbnSalvarAlvaraDePesquisa_Click" OnInit="tbnSalvarAlvaraDePesquisa_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpRequerimentoLavra" style="width: 700px; display: block; top: 0px;
        left: 0px;" class="pop_up">
        <div>
            <div id="fecharRequerimentoLavra" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Requerimento de Lavra</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopUpRequerimentoLavra" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="False">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    Data de Entrada:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataEntradaRequerimentoLavra" runat="server" CssClass="TextBox"
                                        Width="100px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data em que foi requerida a lavra')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataEntradaRequerimentoLavra_CalendarExtender" Format="dd/MM/yyyy"
                                        runat="server" Enabled="True" TargetControlID="tbxDataEntradaRequerimentoLavra">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Anexo:
                                </td>
                                <td>
                                    <asp:Button ID="btnUploadRequerimentoLavra" runat="server" CssClass="ButtonUpload"
                                        OnClientClick="tooltip.hide();" Height="22px" 
                                        Text="       Inserir / Visualizar Anexos" Width="170px" OnClick="btnUploadRequerimentoLavra_Click"
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Adiciona arquivos ao requerimento de lavra')" OnInit="btnUploadRequerimentoLavra_Init" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td>
                                    <asp:LinkButton ID="lkbObservacoesRequerimentoLavra" runat="server" 
                                        Font-Size="8pt" OnClick="lkbObservacoesRequerimentoLavra_Click" 
                                        OnInit="lkbObservacoes_Init" onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>
                                        <asp:ImageButton ID="ibtnAddNotificacaoRequerimentoLavra" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                            OnClientClick="tooltip.hide();" OnClick="ibtnAddNotificacaoRequerimentoLavra_Click"
                                            OnInit="ibtnAddNotificacaoRequerimentoLavra_Init" onmouseout="tooltip.hide();"
                                            onmouseover="tooltip.show('Adiciona exigências ao requerimento de lavra')" />
                                        Exigências:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvExigenciasRequerimentoLavra" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="None" OnRowDeleting="grvExigenciasRequerimentoLavra_RowDeleting"
                                            PageSize="2" Width="100%" OnInit="grvExigenciasRequerimentoLavra_Init" OnPageIndexChanging="grvExigenciasRequerimentoLavra_PageIndexChanging"
                                            OnRowEditing="grvExigenciasRequerimentoLavra_RowEditing">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DataPublicacao" DataFormatString="{0:d}" HeaderText="Data de Publicação" />
                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                <asp:BoundField DataField="DiasPrazo" HeaderText="Dias
    de Prazo" />
                                                <asp:TemplateField HeaderText="Data de Vencimento">
                                                    <ItemTemplate>
                                                        <%# BindVencimentoRequerimentoPesquisa(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgArquivo1" runat="server" AlternateText="." CommandName="Edit"
                                                            ImageUrl="~/imagens/icone_editar.png" PostBackUrl="<%# BindAnexoRequerimentoPesquisa(Container.DataItem)%>"
                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os dados da exigência para edição')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarExigenciasRequerimentoLavra" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir6" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir6_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as exigências selecionadas')" />
                                                        <input id="Checkbox1" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarExigenciasRequerimentoLavra')" />
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
                                    <asp:HiddenField ID="hfIdRequerimentoLavra" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSalvarRequerimentoLavra" runat="server" CssClass="Button" Text="Salvar"
                                        Width="170px" OnClick="btnSalvarRequerimentoLavra_Click" OnInit="btnSalvarRequerimentoLavra_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpConcessaoLavra" style="width: 700px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div>
            <div id="fecharConcessaoLavra" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Concessão de&nbsp; Lavra</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopUpConcessaoLavra" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" width="30%">
                                    Data de Publicação:
                                </td>
                                <td width="70%">
                                    <asp:TextBox ID="tbxPublicacaoConcessaoLavra" runat="server" CssClass="TextBox" Width="100px"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data em que a portaria de lavra foi publicada')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxPublicacaoConcessaoLavra_CalendarExtender" Format="dd/MM/yyyy"
                                        runat="server" Enabled="True" TargetControlID="tbxPublicacaoConcessaoLavra">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    Número da Portaria de Lavra:
                                </td>
                                <td width="70%">
                                    <asp:TextBox ID="tbxNumeroPortariaLavra" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    Data de apresentação do Relatório de Reavaliação de Reserva:
                                </td>
                                <td width="70%">
                                    <asp:TextBox ID="tbxDataApresentacaoRelatorio" runat="server" CssClass="TextBox"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data em que a portaria de lavra foi publicada')"
                                        Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataApresentacaoRelatorio_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataApresentacaoRelatorio">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    Anexo:
                                </td>
                                <td width="70%">
                                    <asp:Button ID="btnUploadConcessaoLavra" runat="server" CssClass="ButtonUpload" Height="22px"
                                        OnClientClick="tooltip.hide();" Text="     Inserir / Visualizar Anexos" 
                                        Width="170px" OnClick="btnUploadConcessaoLavra_Click"
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Adiciona arquivos à Concessão de Lavra')" OnInit="btnUploadConcessaoLavra_Init" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="30%">
                                    &nbsp;</td>
                                <td width="70%">
                                    <asp:LinkButton ID="lkbObservacoesConcessao" runat="server" Font-Size="8pt" 
                                        OnClick="lkbObservacoesConcessao_Click" OnInit="lkbObservacoes_Init" 
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>
                                        <asp:ImageButton ID="btnAdicionarExigenciaConcessaoLavra" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                            OnClientClick="tooltip.hide();" OnClick="btnAdicionarExigenciaConcessaoLavra_Click1"
                                            OnInit="btnAdicionarExigenciaConcessaoLavra_Init" onmouseout="tooltip.hide();"
                                            
                                        onmouseover="tooltip.show('Adiciona exigências a Concessão de Lavra')" />
                                        Exigências:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvExigenciasConcessaoLavra" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="None" OnRowDeleting="grvExigenciasConcessaoLavra_RowDeleting"
                                            PageSize="3" Width="100%" OnInit="grvExigenciasConcessaoLavra_Init" OnPageIndexChanging="grvExigenciasConcessaoLavra_PageIndexChanging"
                                            OnRowEditing="grvExigenciasConcessaoLavra_RowEditing">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DataPublicacao" DataFormatString="{0:d}" HeaderText="Data de Publicação" />
                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                <asp:BoundField DataField="DiasPrazo" HeaderText="Dias
    de Prazo" />
                                                <asp:TemplateField HeaderText="Data de Vencimento">
                                                    <ItemTemplate>
                                                        <%# BindVencimentoRequerimentoPesquisa(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgArquivo3" runat="server" AlternateText="." CommandName="Edit"
                                                            ImageUrl="~/imagens/icone_editar.png" PostBackUrl="<%# BindAnexoRequerimentoPesquisa(Container.DataItem)%>"
                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os dados da exigência para edição')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarExigenciasConcessaoLavraCadastro" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir10" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir10_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as exigências selecionadas')" />
                                                        <input id="Checkbox2" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGrid(this, 'chkSelecionarExigenciasConcessaoLavraCadastro')" />
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
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="50%" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="50%">
                                    Prazo para requerimento de imissão de posse (dias):
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRequerimentoImissaoPosse" runat="server">
                                        <asp:ListItem>90</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>
                                        <asp:ImageButton ID="btnAdicionarNotificacaoConcessaoLavra" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                            OnClientClick="tooltip.hide();" OnClick="btnAdicionarNotificacaoConcessaoLavra_Click"
                                            OnInit="btnAdicionarNotificacaoConcessaoLavra_Init" onmouseout="tooltip.hide();"
                                            onmouseover="tooltip.show('Adiciona notificações de aviso de requerimento de imissão de posse')" />
                                        Notificações:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids" style="margin-bottom: 5px;">
                                        <asp:GridView ID="grvNotificacaoConcessaoLavra" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="None" OnInit="grvNotificacaoConcessaoLavra_Init"
                                            OnRowDeleting="grvNotificacaoConcessaoLavra_RowDeleting" PageSize="3" Width="100%"
                                            OnPageIndexChanging="grvNotificacaoConcessaoLavra_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesConcessaoLavraCadastro" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir9" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir9_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" />
                                                        <input id="Checkbox3" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesConcessaoLavraCadastro')" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle Width="45px" />
                                                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                        Font-Strikeout="False" Font-Underline="False" />
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
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="hfIdConcessaoLavra" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSalvarConcessaoLavra" runat="server" CssClass="Button" Text="Salvar"
                                        Width="170px" OnClick="btnSalvarConcessaoLavra_Click" OnInit="btnSalvarConcessaoLavra_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpLicenciamento" style="width: 700px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div>
            <div id="fecharLicenciamento" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Licenciamento</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopUpLicenciamento" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    Número:
                                </td>
                                <td width="60%">
                                    <asp:TextBox ID="tbxNumeroLicenciamento" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Data de Abertura:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataAberturaLicenciamento" runat="server" CssClass="TextBox"
                                        Enabled="False" Width="100px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data de abertura de processo de licenciamento na ANM')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataAberturaLicenciamento_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataAberturaLicenciamento">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Data de Publicação:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxPublicacaoLicenciamento" runat="server" CssClass="TextBox" Width="100px"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data da publicação do licenciamento')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxPublicacaoLicenciamento_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxPublicacaoLicenciamento">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Possui PAE Aprovada:
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckbPossuiPAE" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Anexo:
                                </td>
                                <td>
                                    <asp:Button ID="btnUploadLicenciamento" runat="server" CssClass="ButtonUpload" Height="22px"
                                        OnClientClick="tooltip.hide();" Text="    Inserir / Visualizar Anexos" 
                                        Width="170px" OnClick="btnUploadLicenciamento_Click"
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Adiciona arquivos ao licenciamento')" OnInit="btnUploadLicenciamento_Init" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbObservacoesLicenciamento" runat="server" Font-Size="8pt" OnClick="lkbObservacoesLicenciamento_Click"
                                        OnInit="lkbObservacoes_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:ImageButton ID="btnSelecionarLicencasAssociadasLicenciamento" runat="server"
                                        Text="Selecionar Licença" OnClick="btnSelecionarLicencasAssociadasLicenciamento_Click"
                                        OnInit="btnSelecionarLicencasAssociadasLicenciamento_Init" ImageUrl="~/imagens/icone_adicionar.png"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Associa licenças ambientais ao licenciamento')" />
                                    <strong>Licenças Associadas:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvLicencasLicenciamento" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" OnRowDeleting="grvLicencasLicenciamento_RowDeleting" PageSize="1"
                                            Width="100%" OnInit="grvLicencasLicenciamento_Init" OnPageIndexChanging="grvLicencasLicenciamento_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                <asp:TemplateField HeaderText="Órgão">
                                                    <ItemTemplate>
                                                        <%#
    BindOrgao(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo">
                                                    <ItemTemplate>
                                                        <%# BindTipo(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DataRetirada" DataFormatString="{0:d}" HeaderText="Retirada" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarLicencasCadastroLicenciamento" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir3" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir3_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Desassocia do licenciamento as licenças ambientais selecionadas')" />
                                                        <input id="ckbSelecionar4" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarLicencasCadastroLicenciamento')" />
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
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:ImageButton ID="btnAdicionarExigenciasLicenciamento" runat="server" OnClick="btnAdicionarExigenciasLicenciamento_Click"
                                        OnInit="btnAdicionarExigenciasLicenciamento_Init" ImageUrl="~/imagens/icone_adicionar.png"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona exigências ao licenciamento')" /><strong>Exigências:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvNotificacaoLicenciamento" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                            ForeColor="#333333" GridLines="None" OnRowDeleting="grvNotificacaoLicenciamento_RowDeleting"
                                            PageSize="2" Width="100%" OnInit="grvNotificacaoLicenciamento_Init" OnPageIndexChanging="grvNotificacaoLicenciamento_PageIndexChanging"
                                            OnRowEditing="grvNotificacaoLicenciamento_RowEditing">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DataPublicacao" DataFormatString="{0:d}" HeaderText="Data de Publicação" />
                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                <asp:BoundField DataField="DiasPrazo" HeaderText="Dias
    de Prazo" />
                                                <asp:TemplateField HeaderText="Data de Vencimento">
                                                    <ItemTemplate>
                                                        <%# BindVencimentoRequerimentoPesquisa(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgArquivo2" runat="server" AlternateText="." CommandName="Edit"
                                                            ImageUrl="~/imagens/icone_editar.png" PostBackUrl="<%# BindAnexoRequerimentoPesquisa(Container.DataItem)%>"
                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exibe os dados da exigência para edição')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarExigenciasLicenciamentoCadastroLicenciamento" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir8" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir8_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as exigências selecionadas')" />
                                                        <input id="Checkbox4" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarExigenciasLicenciamentoCadastroLicenciamento')" />
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
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TabContainer ID="TabContainer5" runat="server" ActiveTabIndex="1" Width="100%"
                                        Height="110px" ScrollBars="Auto">
                                        <asp:TabPanel ID="TabPanel15" runat="server" HeaderText="Validade">
                                            <ContentTemplate>
                                                <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="right" width="40%">
                                                            Validade:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="tbxValidadeLicenciamento" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox><asp:CalendarExtender
                                                                ID="tbxValidadeLicenciamento_CalendarExtender" Format="dd/MM/yyyy" runat="server"
                                                                Enabled="True" TargetControlID="tbxValidadeLicenciamento">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Status:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlEstatusVencimentoLicenciamento" runat="server" CssClass="DropDownList"
                                                                DataTextField="Nome" DataValueField="Id" OnSelectedIndexChanged="ddlEstadoLicenca_SelectedIndexChanged1"
                                                                Width="170px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Situação do licenciamento')">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lkbVencimentoLicenciamento" runat="server" Font-Size="8pt" OnClick="lkbVencimentoLicenciamento_Click1"
                                                                OnClientClick="tooltip.hide();" OnInit="lkbVencimentoLicenciamento_Init" onmouseout="tooltip.hide();"
                                                                onmouseover="tooltip.show('Visualiza o histórico de todos os vencimentos do licenciamento')">Visualizar Vencimentos</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <asp:ImageButton ID="btnAdicionarNotificacaoValidadeLicenciamento" runat="server"
                                                                OnClick="btnAdicionarNotificacaoValidadeLicenciamento_Click" OnInit="btnAdicionarNotificacaoValidadeLicenciamento_Init"
                                                                OnClientClick="tooltip.hide();" ImageUrl="~/imagens/icone_adicionar.png" onmouseout="tooltip.hide();"
                                                                onmouseover="tooltip.show('Adiciona notificações por e-mail com aviso de vencimento do licenciamento')" /><strong>Notificações:</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <div class="container_grids">
                                                                <asp:GridView ID="grvNotificacaoValidadeLicenciamento" runat="server" AllowPaging="True"
                                                                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                                                    ForeColor="#333333" GridLines="None" OnInit="grvNotificacaoValidadeLicenciamento_Init"
                                                                    OnRowDeleting="grvNotificacaoValidadeLicenciamento_RowDeleting" PageSize="2"
                                                                    Width="100%" OnPageIndexChanging="grvNotificacaoValidadeLicenciamento_PageIndexChanging">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                        <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <HeaderTemplate>
                                                                                <asp:ImageButton ID="ibtnExcluir11" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                    OnPreRender="ibtnExcluir11_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" /><input
                                                                                        id="Checkbox5" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesValidadeLicenciamentoCadastro')" /></HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesValidadeLicenciamentoCadastro" /></ItemTemplate>
                                                                            <HeaderStyle Width="45px" />
                                                                            <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                                Font-Strikeout="False" Font-Underline="False" />
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
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanel16" runat="server" HeaderText="Entrega de Licença ou Protocolo">
                                            <ContentTemplate>
                                                <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="right">
                                                            Prazo (dias após a publicação):
                                                        </td>
                                                        <td width="60%">
                                                            <asp:DropDownList ID="ddlEntregaLicencaOuProtocolo" runat="server">
                                                                <asp:ListItem>60</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <asp:ImageButton ID="btnAdicionarNotificacaoEntregaLicenciamento" runat="server"
                                                                OnClick="btnAdicionarNotificacaoEntregaLicenciamento_Click" OnInit="btnAdicionarNotificacaoEntregaLicenciamento_Init"
                                                                ImageUrl="~/imagens/icone_adicionar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações por e-mail com aviso de término do prazo para entrega da licença ambiental ou protocolo de pedido de licença')" /><strong>Notificações:</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <div class="container_grids">
                                                                <asp:GridView ID="grvNotificacaoEntregaLicencieamento" runat="server" AllowPaging="True"
                                                                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                                                    ForeColor="#333333" GridLines="None" OnInit="grvNotificacaoEntregaLicencieamento_Init"
                                                                    OnRowDeleting="grvNotificacaoEntregaLicencieamento_RowDeleting1" PageSize="3"
                                                                    Width="100%" OnPageIndexChanging="grvNotificacaoEntregaLicencieamento_PageIndexChanging">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                        <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesEntregaLicenciamentoCadastro" /></ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <asp:ImageButton ID="ibtnExcluir12" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                    OnPreRender="ibtnExcluir12_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" /><input
                                                                                        id="Checkbox6" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesEntregaLicenciamentoCadastro')" /></HeaderTemplate>
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
                                <td align="right" colspan="2" height="35" valign="bottom">
                                    <asp:Button ID="btnSalvarLicenciamento" runat="server" CssClass="Button" Text="Salvar"
                                        Width="170px" OnClick="btnSalvarLicenciamento_Click" OnInit="btnSalvarLicenciamento_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpExtracao" style="width: 700px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div>
            <div id="fecharExtracao" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Extração</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopUpExtracao" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" width="40%">
                                    Número:
                                </td>
                                <td width="60%">
                                    <asp:TextBox ID="tbxNumeroExtracao" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    Data de Abertura:
                                </td>
                                <td width="60%">
                                    <asp:TextBox ID="tbxDataAberturaExtracao" runat="server" CssClass="TextBox" Enabled="False"
                                        Width="100px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data de abertura de processo de extração na ANM')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxDataAberturaExtracao_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataAberturaExtracao">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Data de Publicação:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxPublicacaoExtracao" runat="server" CssClass="TextBox" Width="100px"
                                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Data da publicação da extração')"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxPublicacaoExtracao_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxPublicacaoExtracao">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Número da Licença Ambiental:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxnumeroLicencaExtracao" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Validade da Licença Ambiental:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxValidadeLicencaExtracao" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxvalidadeLicencaExtracao_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxvalidadeLicencaExtracao">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Anexo:
                                </td>
                                <td>
                                    <asp:Button ID="btnUploadExtracao" runat="server" CssClass="ButtonUpload" Height="22px"
                                        OnClientClick="tooltip.hide();" Text="    Inserir / Visualizar Anexos" 
                                        Width="170px" OnClick="btnUploadExtracao_Click"
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Adiciona arquivos à extração')" OnInit="btnUploadExtracao_Init" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbObservacoesExtracao" runat="server" Font-Size="8pt" OnClick="lkbObservacoesExtracao_Click"
                                        OnInit="lkbObservacoes_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:ImageButton ID="btnAdicionarExigenciasExtracao" runat="server" OnClick="btnAdicionarExigenciasExtracao_Click"
                                        OnClientClick="tooltip.hide();" OnInit="btnAdicionarExigenciasExtracao_Init"
                                        ImageUrl="~/imagens/icone_adicionar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona exigências à Extração')" />
                                    <strong>Exigências:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids" style="margin-bottom: 5px;">
                                        <asp:GridView ID="grvExigenciaExtracao" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" OnRowDeleting="grvExigenciaExtracao_RowDeleting" PageSize="3"
                                            Width="100%" OnInit="grvExigenciaExtracao_Init" OnPageIndexChanging="grvExigenciaExtracao_PageIndexChanging"
                                            OnRowEditing="grvExigenciaExtracao_RowEditing">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DataPublicacao" DataFormatString="{0:d}" HeaderText="Data de Publicação" />
                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                <asp:BoundField DataField="DiasPrazo" HeaderText="Dias
    de Prazo" />
                                                <asp:TemplateField HeaderText="Data de Vencimento">
                                                    <ItemTemplate>
                                                        <%# BindVencimentoRequerimentoPesquisa(Container.DataItem)%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgArquivo" runat="server" AlternateText="." CommandName="Edit"
                                                            ImageUrl="~/imagens/icone_editar.png" PostBackUrl="<%# BindAnexoRequerimentoPesquisa(Container.DataItem)%>"
                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre os dados da exigência para edição')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarExigenciasCadastroExtracao" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir0" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            OnPreRender="ibtnExcluir0_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as exigências selecionadas')" />
                                                        <input id="Checkbox8" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarExigenciasCadastroExtracao')" />
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
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TabContainer ID="tcExtracao" runat="server" ActiveTabIndex="0" Width="100%"
                                        Height="120px" ScrollBars="Auto">
                                        <asp:TabPanel ID="tpExtracao1" runat="server" HeaderText="Validade">
                                            <ContentTemplate>
                                                <div style="width: 100%; height: 10px;">
                                                </div>
                                                <table cellpadding="2" style="width: 100%;">
                                                    <tr>
                                                        <td align="right">
                                                            Validade:
                                                        </td>
                                                        <td width="60%">
                                                            <asp:TextBox ID="tbxValidadeExtracao" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox><asp:CalendarExtender
                                                                ID="tbxValidadeExtracao_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                                Enabled="True" TargetControlID="tbxValidadeExtracao">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Status:
                                                        </td>
                                                        <td width="60%">
                                                            <asp:DropDownList ID="ddlEstatusVencimentoExtracao" runat="server" CssClass="DropDownList"
                                                                DataTextField="Nome" DataValueField="Id" OnSelectedIndexChanged="ddlEstadoLicenca_SelectedIndexChanged1"
                                                                Width="170px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Situação atual da extração')">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                        <td width="60%">
                                                            <asp:LinkButton ID="lkbVencimentoExtracao" runat="server" Font-Size="8pt" OnClick="lkbVencimentoExtracao_Click"
                                                                OnInit="lkbVencimentoExtracao_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico de todos os vencimentos da extração')">Visualizar Vencimentos</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <asp:ImageButton ID="btnAdicionarNotificacaoValidadeExtracao" runat="server" OnClick="btnAdicionarNotificacaoValidadeExtracao_Click"
                                                                OnInit="btnAdicionarNotificacaoValidadeExtracao_Init" ImageUrl="~/imagens/icone_adicionar.png"
                                                                onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações por e-mail com aviso de vencimento da extração')" /><strong>Notificações:</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <div class="container_grids">
                                                                <asp:GridView ID="grvNotificacaoValidadeExtracao" runat="server" AllowPaging="True"
                                                                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" EnableModelValidation="True"
                                                                    ForeColor="#333333" GridLines="None" OnInit="grvNotificacaoValidadeExtracao_Init"
                                                                    OnRowDeleting="grvNotificacaoValidadeExtracao_RowDeleting" PageSize="3" Width="100%"
                                                                    OnPageIndexChanging="grvNotificacaoValidadeExtracao_PageIndexChanging">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                                        <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <HeaderTemplate>
                                                                                <asp:ImageButton ID="ibtnExcluir2" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                                    OnPreRender="ibtnExcluir2_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" /><input
                                                                                        id="Checkbox9" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesValidadeCadastroExtracao')" /></HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesValidadeCadastroExtracao" /></ItemTemplate>
                                                                            <HeaderStyle Width="45px" />
                                                                            <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                                Font-Strikeout="False" Font-Underline="False" />
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
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    </asp:TabContainer>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSalvarExtracao" runat="server" CssClass="Button" Text="Salvar"
                                        Width="170px" OnClick="btnSalvarExtracao_Click" OnInit="btnSalvarExtracao_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpSelecionarLicenca" style="width: 596px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
        <div>
            <div id="fecharSelecionarLicenca" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Seleção de Licença</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upSelecionarLicenca" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" width="250">
                                    Empresa:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlEmpresaLicenca" runat="server" CssClass="DropDownList" Width="170px">
                                    </asp:DropDownList>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="250">
                                    Tipo Órgão Ambiental:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlTipoOrgaoLicenca" runat="server" CssClass="DropDownList"
                                        OnSelectedIndexChanged="ddlTipoOrgaoLicenca_SelectedIndexChanged" Width="170px"
                                        AutoPostBack="True">
                                        <asp:ListItem>Estadual</asp:ListItem>
                                        <asp:ListItem>Federal</asp:ListItem>
                                        <asp:ListItem>Municipal</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <div id="divEstadoProcesso" runat="server" visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 248px">
                                                    Estado:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlEstadoLicenca" runat="server" CssClass="DropDownList" DataTextField="Nome"
                                                        DataValueField="Id" Width="170px" AutoPostBack="True" OnSelectedIndexChanged="ddlEstadoLicenca_SelectedIndexChanged1">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divCidadeProcesso" runat="server" visible="False">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 248px">
                                                    Cidade:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlCidadeLicenca" runat="server" CssClass="DropDownList" DataTextField="Nome"
                                                        DataValueField="Id" Width="170px" AutoPostBack="True" OnSelectedIndexChanged="ddlCidadeLicenca_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="250">
                                    Orgãos Ambientais:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlOrgaoLicenca" runat="server" CssClass="DropDownList" OnSelectedIndexChanged="ddlOrgaoLicenca_SelectedIndexChanged"
                                        Width="170px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <div class="container_grids" style="overflow: auto; height: 240px; padding-left:200px;" 
                                        align="left">
                                        <asp:TreeView ID="tvwLicencas" runat="server" ShowCheckBoxes="Leaf">
                                        </asp:TreeView>
                                        <br />
                                        <asp:Label ID="lblResultLicencas" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:HiddenField ID="hfTipo" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnAdicionarLicenca" runat="server" CssClass="Button" Text="OK" Width="250px"
                                        OnClick="btnAdicionarLicenca_Click" OnInit="btnAdicionarLicenca_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpCadastroNotificacao" style="width: 805px; display: block; top: 0px;
        left: 0px;" class="pop_up_super_super">
        <div>
            <div id="fecharCadastroNotificacao" class="btn_cancelar_popup">
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
                                    Dias de Aviso:
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="cblDias" runat="server" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
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
                                    <asp:HiddenField ID="EhExigencia" runat="server" />
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
    <div id="divPopUpRenovacao" style="width: 596px; display: block; top: 0px; left: 0px;"
        class="pop_up">
        <div id="fecharRenovacao" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            RENOVAÇÃO</div>
        <asp:UpdatePanel ID="upRenovacao" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
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
                            <asp:Label ID="lblValidadeRenovacao" runat="server"></asp:Label>
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="tbxDiasValidadeRenovacao" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                            <asp:TextBox ID="tbxDataValidadeRenovacao" runat="server" CssClass="TextBox"></asp:TextBox>
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
                            <asp:HiddenField ID="hfIdExigenciaRenovacao" runat="server" />
                            <asp:HiddenField ID="hfTipoRenovacao" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                                        OnSelectedIndexChanged="ddlVencimentos_SelectedIndexChanged" onmouseout="tooltip.hide();"
                                        onmouseover="tooltip.show('Selecione a data de vencimento para visualizar as informações')">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="ibtnRemoverVencimento" runat="server" ImageUrl="~/imagens/excluir.gif"
                                        OnClick="ibtnRemoverVencimento_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o vencimento selecionado')" />
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
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>Notificações:</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <div class="container_grids">
                                        <asp:GridView ID="grvNotificacaoVencimentos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" PageSize="3" Width="100%" OnPageIndexChanging="grvNotificacaoVencimentos_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                                <asp:BoundField DataField="Emails" HeaderText="E-mails" />
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
                                    <asp:HiddenField ID="hfIDVencimento" runat="server" />
                                    <asp:HiddenField ID="hfTypeVencimento" runat="server" />
                                    <asp:Button ID="btnSalvarExigencias0" runat="server" CssClass="Button" OnClick="btnSalvarExigencias0_Click"
                                        Text="Salvar" Width="170px" OnInit="btnSalvarExigencias0_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpCadastroExigencia" style="width: 700px; display: block; top: 0px;
        left: 0px;" class="pop_up_super">
        <div>
            <div id="fecharCadastroExigencia" class="btn_cancelar_popup">
            </div>
            <asp:UpdatePanel ID="upExigencias" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="barra_titulo">
                        Cadastro de
                        <asp:Label ID="lblExigenciaCondicionante" runat="server" Text="Exigencias"></asp:Label>
                    </div>
                    <div>
                        <div>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" class="style1">
                                        Status:
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="ddlStatusExigencia" runat="server" CssClass="DropDownList"
                                            DataTextField="Nome" DataValueField="Id" Width="170px" AutoPostBack="True" 
                                            oninit="ddlStatusExigencia_Init" 
                                            onselectedindexchanged="ddlStatusExigencia_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="chkPeriodicaExigencia" runat="server" Text="Periodica" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style1">
                                        Data de Publicação:
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="tbxDataPublicacaoExigencias" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtenderdsf" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="tbxDataPublicacaoExigencias">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Descrição:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbxDescricaoExigencias" runat="server" CssClass="TextBox" Width="492px"
                                            MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Prazo para Atendimento (dias):
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbxDiasPrazo" runat="server" CssClass="TextBox" Width="70px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:LinkButton ID="lkbProrrogacao" runat="server" Font-Size="8pt" 
                                            OnClick="lkbProrrogacao_Click" OnClientClick="tooltip.hide();" 
                                            OnInit="lkbProrrogacao_Init" onmouseout="tooltip.hide();" 
                                            onmouseover="tooltip.show('Visualiza o vencimento atual e todas as suas renovações anteriores')">Abrir Prorrogações</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Data de Atendimento:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbxDataAtendimentoExigencia" runat="server" CssClass="TextBox" Width="100px"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quando o vencimento é atendido')"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxDataAtendimentoExigencia_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxDataAtendimentoExigencia">
                                        </asp:CalendarExtender>
                                        &nbsp;Protocolo de Atendimento:
                                        <asp:TextBox ID="tbxProtocoloExigencia" runat="server" CssClass="TextBox" Width="100px"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Número do Protocolo de atendimento')"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Anexo:
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUploadExigencia" runat="server" CssClass="ButtonUpload" Height="22px"
                                            Text="    Inserir / Visualizar Anexos" Width="170px" 
                                            onmouseout="tooltip.hide();" 
                                            onmouseover="tooltip.show('Anexa arquivos ao vencimento')" OnClick="btnUploadExigencia_Click" OnInit="btnUploadExigencia_Init" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lkbVencimentoExigencia" runat="server" Font-Size="8pt" OnClick="lkbVencimentoExigencia_Click"
                                            OnClientClick="tooltip.hide();" OnInit="lkbVencimentoExigencia_Init" onmouseout="tooltip.hide();"
                                            onmouseover="tooltip.show('Visualiza o vencimento atual e todas as suas renovações anteriores')">Visualizar Vencimentos</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="lkbObservacoesExigencia" runat="server" Font-Size="8pt" OnClick="lkbObservacoesExigencia_Click"
                                            OnInit="lkbObservacoes_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico de registros')">Histórico de Registros</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <strong>
                                            <asp:ImageButton ID="ibtnAddNotificacaoExigencia" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                OnClick="btnAdicionarNotificacao_Click" OnClientClick="tooltip.hide();" OnInit="btnAdicionarNotificacao_Init"
                                                onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona notificações de aviso de vencimento por e-mail')" />
                                            Notificações:</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <div class="container_grids">
                                            <asp:GridView ID="grvNotificacoesExigencias" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                                GridLines="None" OnInit="grvNotificacoesExigencias_Init1" OnRowDeleting="grvNotificacoesExigencias_RowDeleting"
                                                PageSize="2" Width="100%" OnPageIndexChanging="grvNotificacoesExigencias_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                                    <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                                    <asp:TemplateField HeaderText="Excluir">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoesCadastroExigencias" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:ImageButton ID="ibtnExcluir1" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                OnPreRender="ibtnExcluir1_PreRender" ToolTip="Excluir" />
                                                            <input id="ckbSelCadastroExigencias" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoesCadastroExigencias')" />
                                                        </HeaderTemplate>
                                                        <HeaderStyle Width="45px" />
                                                        <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                            Font-Strikeout="False" Font-Underline="False" />
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
                                    <td align="right" colspan="2">
                                        <asp:HiddenField ID="hfItemExigencia" runat="server" />
                                        <asp:HiddenField ID="hfIdExigencia" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnSalvarExigencias" runat="server" CssClass="Button" Text="Salvar"
                                            Width="170px" OnClick="btnSalvarExigencias_Click" OnInit="btnSalvarExigencias_Init"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva os dados da exigência')" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
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
                                                    <asp:TextBox ID="tbxPrazoAdicional" runat="server" CssClass="TextBox" 
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 30%">
                                                    Data de Protocolo:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="tbxDataProtocoloAdicional" runat="server" 
                                                        CssClass="TextBox" onmouseout="tooltip.hide();" 
                                                        onmouseover="tooltip.show('Data em que foi protocolado o pedido de prorrogação de prazo')" 
                                                        Width="100px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="tbxDataProtocoloAdicional_CalendarExtender" 
                                                        runat="server" Enabled="True" Format="dd/MM/yyyy" 
                                                        TargetControlID="tbxDataProtocoloAdicional">
                                                    </asp:CalendarExtender>
                                                    &nbsp;Protocolo de Prorrogação:
                                                    <asp:TextBox ID="tbxProtocoloAdicional" runat="server" 
                                                        CssClass="TextBox" onmouseout="tooltip.hide();" 
                                                        onmouseover="tooltip.show('Número do Protocolo de pedido de prorrogação de prazo')" 
                                                        Width="100px"></asp:TextBox>&nbsp;
                                                    <strong>
                                                        <asp:Button ID="btnAddProrrogacao" runat="server" Text="Salvar" 
                                                        CssClass="Button" onclick="btnAddProrrogacao_Click" 
                                                        oninit="btnAddProrrogacao_Init"/>
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
                                                PageSize="3" Width="100%" onrowdeleting="grvProrrogacoes_RowDeleting">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="DataProtocoloAdicional" 
                                                        HeaderText="Data do Protocolo" DataFormatString="{0:d}" />
                                                    <asp:BoundField DataField="PrazoAdicional" HeaderText="Dias de Prorrogação" />
                                                     <asp:TemplateField HeaderText="Nova Data de Vencimento">
                                                    <ItemTemplate>
                                                    <%# bindNovaData(Container.DataItem) %>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProtocoloAdicional" HeaderText="Protocolo" />
                                                    <asp:TemplateField HeaderText="excluir">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbExcluir" runat="server" 
                                                                CssClass="chkSelecionarProrrogacaoPrazo" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:ImageButton ID="ibtnExcluir6" runat="server" CommandName="Delete" 
                                                                ImageUrl="~/imagens/excluir.gif" oninit="ibtnExcluir6_Init" 
                                                                OnPreRender="ibtnExcluir6_PreRender1" ToolTip="Excluir" />
                                                            <input id="Checkbox15" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarProrrogacaoPrazo')" />
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
                                        <asp:Button ID="btnFechar" runat="server" CssClass="Button" Text="Fechar" 
                                            Width="220px" onclick="btnFechar_Click" />
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

    <div id="divPopUpObs" style="width: 866px; display: block; top: 0px; left: 0px;"
        class="pop_up_super_super">
        <div>
            <div id="fecharObs" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Registros em Histórico</div>
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
                                    <td align="right" colspan="2" valign="middle">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2" valign="middle">
                                        <asp:Button ID="btnEnviarHistoricoPorEmail" runat="server" CssClass="Button" 
                                            onclick="btnEnviarHistoricoPorEmail_Click" 
                                            oninit="btnEnviarHistoricoPorEmail_Init" Text="Enviar Histórico por e-mail" 
                                            Width="220px" />
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
    <div id="divPopSubstancia" style="width: 866px; display: block; top: 0px; left: 0px;"
        class="pop_up_super_super">
        <div>
            <div id="btn_fechar_popSubstancia" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Substâncias</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upSubstancias" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="ibtnSubstancias">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="right" width="20%">
                                        Nome da Substância:
                                    </td>
                                    <td width="60%">
                                        <asp:TextBox ID="tbxNomeSubstancia" runat="server" CssClass="TextBox" Width="70%"></asp:TextBox>&nbsp;
                                        </td>
                                </tr>
                                <tr>
                                    <td align="right" width="20%">
                                        Data
                                    </td>
                                    <td width="60%">
                                        <asp:TextBox ID="tbxDataSubstancia" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxDataSubstancia_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" TargetControlID="tbxDataSubstancia">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="20%">
                                        Protocolo:
                                    </td>
                                    <td width="60%">
                                        <asp:TextBox ID="tbxProtocoloSubstancia" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                                        <strong>
                                            <asp:ImageButton ID="ibtnSubstancias" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                                OnClick="ibtnSubstancias_Click" ValidationGroup="rfvAdicionarSubstancia" />
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
                                            <asp:GridView ID="gdvSubstancias" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                                PageSize="3" Width="100%" OnRowDeleting="gdvSubstancias_RowDeleting">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" />
                                                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                                    <asp:BoundField DataField="Protocolo" HeaderText="Protocolo" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibtnExcluirSubstancia" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                                onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="1px" />
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
                                    <td align="right" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="divPopUpNotificacoes" style="width: 866px; display: block; top: 0px; left: 0px;"
        class="pop_up_super_super">
        <div>
            <div id="fecharNotificacoes" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Notificações</div>
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
                                <div class="container_grids">
                                    <asp:GridView ID="grvNotificacoes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                                        PageSize="3" Width="100%">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                            <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                            <asp:BoundField DataField="Emails" HeaderText="E-mails" />
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
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>    
    <div id="divPopUpAlteracaoStatus" style="width: 805px; display: block; top: 0px;
        left: 0px;" class="pop_up_super_super_super">
        <div>
            <div id="fecharAlteracaoStatus" class="btn_cancelar_popup" style="display:none">
            </div>
            <div class="barra_titulo">
                Alteração de Histórico
            </div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upPopStatus" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td align="left">
                                    <strong>Você alterou o Histórico de um Vencimento - Caso deseja enviar um email 
                                    com esta alteração, preencha os campos abaixo e clique em Enviar/Salvar: </strong>
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
                                        runat="server" CssClass="TextBox" Height="56px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:CheckBox ID="chkEnviarEmail" runat="server" Checked="True" Text="Enviar e-mail com alteração de status" />
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

    <div id="divPopUpEnviarEmailHistorico" style="width: 866px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
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
                                    <strong>Os registros históricos serão enviados para o(s) e-mail(s) selecionados abaixo: </strong>
                                </td>
                            </tr>                           
                            <tr>
                                <td align="left" colspan="2">
                                    &nbsp;</td>
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
                                                <input id="Checkbox13" type="checkbox" class="chkMarcarEmailsEmpresa"
                                                    checked="checked" />&nbsp; <strong>Empresa:</strong>
                                            </td>
                                            <td align="left" width="33%">
                                                <input id="Checkbox14" type="checkbox" class="chkMarcarEmailsConsultora"
                                                    checked="checked" />&nbsp; <strong>Consultoria: </strong>
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
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                    <asp:Button ID="btnEnviarHistorico" runat="server" CssClass="Button" 
                                        Text="Enviar" Width="170px"
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Envia o histórico para os e-mails selecionados')" 
                                        onclick="btnEnviarHistorico_Click" />
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
                <asp:HiddenField ID="hfIdRalVencimentosPeriodicos" runat="server" />                
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
        
</asp:Content>
