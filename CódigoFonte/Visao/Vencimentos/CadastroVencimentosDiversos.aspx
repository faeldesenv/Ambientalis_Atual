<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="CadastroVencimentosDiversos.aspx.cs" Inherits="Vencimentos_CadastroVencimentosDiversos" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            font-size: small;
        }
        .style2
        {
            height: 15px;
        }
    </style>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    Cadastro de Vencimentos Diversos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblPopStatus" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="ModalStatusDiverso" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fechar_cadastro_status" PopupControlID="cadastro_status_diverso" TargetControlID="lblPopStatus">
    </asp:ModalPopupExtender>
    
    <asp:Label ID="lblPopNotificacoes" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="ModalExtenderNotificoes" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fecharCadastroNotificacao" PopupControlID="divPopUpCadastroNotificacao" 
        TargetControlID="lblPopNotificacoes"></asp:ModalPopupExtender>
    
    <asp:Label ID="lblPopupVencimentos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="ModalExtenderVisualizarVencimentos" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fecharVencimento" PopupControlID="divPopUpVencimentos" 
        TargetControlID="lblPopupVencimentos"></asp:ModalPopupExtender>
    
    <asp:Label ID="lblPopupAlteracaoVencimentos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblAlteracaoStatus_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fecharAlteracaoStatus" 
        PopupControlID="divPopUpAlteracaoStatus" TargetControlID="lblPopupAlteracaoVencimentos"></asp:ModalPopupExtender>
    
    <asp:Label ID="lblExtenderAddTipo" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="ModalTipoDiverso" runat="server" BackgroundCssClass="simplemodal" CancelControlID="div_fechar_cadastro_tipo" PopupControlID="cadastro_tipo" 
        TargetControlID="lblExtenderAddTipo"></asp:ModalPopupExtender>
    
    <asp:Label ID="lblExtenderVerHistoricos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="ModalExtenderHistoricos" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fecharHistoricos" PopupControlID="divPopHistoricos" 
        TargetControlID="lblExtenderVerHistoricos"></asp:ModalPopupExtender>
    
    <asp:Label ID="lblEnvioHistorico" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblEnvioHistorico_popupextender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fecharEnviarEmailHistorico" 
        PopupControlID="divPopUpEnviarEmailHistorico" TargetControlID="lblEnvioHistorico"></asp:ModalPopupExtender>
    
    <asp:Label ID="lblRenovacaoVencimentosPeriodicos" runat="server" Text=""></asp:Label>
    <asp:ModalPopupExtender ID="lblRenovacaoVencimentosPeriodicos_popupextender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fechar_vencimentos_periodicos" 
        PopupControlID="divPopUpVencimentosPeriodicos" TargetControlID="lblRenovacaoVencimentosPeriodicos"></asp:ModalPopupExtender>
    
    <asp:Label ID="lblRenovacaoVencimentosPeriodicosDatas" runat="server" Text=""></asp:Label>
    <asp:ModalPopupExtender ID="lblRenovacaoVencimentosPeriodicosDatas_popupextender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fecharRenovacaoPeriodicosDatas" 
        PopupControlID="divRenovacaoPeriodicosDatas" TargetControlID="lblRenovacaoVencimentosPeriodicosDatas"></asp:ModalPopupExtender>    
    
    <asp:Label ID="lblUploadArquivos" runat="server" Text="" />
    <asp:ModalPopupExtender ID="lblUploadArquivos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fechar_arquivos_teste" DynamicServicePath="" Enabled="True" 
        PopupControlID="arquivos_teste" TargetControlID="lblUploadArquivos"></asp:ModalPopupExtender>
    
    <table width="100%">
        <tr>
            <td align="right" width="35%">
                Grupo Econômico:
            </td>
            <td width="65%">
                <asp:DropDownList ID="ddlGrupoEconomico" runat="server" Width="358px" CssClass="DropDownList"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoEconomico_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" width="35%">
                Empresa:
            </td>
            <td>
                <asp:UpdatePanel ID="UPEmpresa" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="DropDownList" Width="358px">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomico" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right" width="35%">
                Tipo:
            </td>
            <td>
                <asp:UpdatePanel ID="UPTipoDiverso" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlTipoDiverso" runat="server" CssClass="DropDownList" Width="358px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDiverso_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:ImageButton ID="btnAdicionarTipo" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                            OnClick="btnAdicionarTipo_Click" OnInit="btnAdicionarTipo_Init" />
                        &nbsp;
                        <asp:ImageButton ID="ibtnExcluirTipo" runat="server" ImageUrl="~/imagens/excluir.gif"
                            OnClick="ibtnExcluirTipo_Click" OnPreRender="ibtnExcluirTipo_PreRender" Width="20px" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEmpresa" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ibtnExcluirTipo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right" width="35%">
                Status:
            </td>
            <td>
                <asp:UpdatePanel ID="UPStatus" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="358px" CssClass="DropDownList">
                        </asp:DropDownList>
                        <asp:ImageButton ID="btnAdicionarStatus" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                            OnClick="btnAdicionarStatus_Click" OnInit="btnAdicionarStatus_Init" Style="width: 20px" />&nbsp;
                        <asp:ImageButton ID="ibtnExcluirStatus" runat="server" ImageUrl="~/imagens/excluir.gif"
                            OnClick="ibtnExcluirStatus_Click" OnPreRender="ibtnExcluirStatus_PreRender" Width="20px" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoDiverso" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ibtnExcluirStatus" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right" width="35%">
                Descrição:
            </td>
            <td>
                <asp:TextBox ID="tbxDescricao" CssClass="TextBox" Width="350px" runat="server"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbxDescricao"
                    ErrorMessage="Campo Obrigatório!" ValidationGroup="rfvSalvarDiverso"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" width="35%">
                Detalhamento:</td>
            <td>
                <asp:TextBox ID="tbxDetalhamento" runat="server" CssClass="TextBox" 
                    Width="350px" Height="50px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" width="35%">
                Vencimento:
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td width="15%">
                            <asp:UpdatePanel ID="upDataVencimento" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                              <asp:TextBox ID="tbxVencimento" CssClass="TextBox" Width="120px" runat="server"></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxVencimento">
                              </asp:CalendarExtender>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UPVisualizacoes" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="visualizacoes" class="visualizacoes" runat="server" visible="false">
                                        <asp:LinkButton ID="lxbVisualizarVencimentos" runat="server" OnClick="lxbVisualizarVencimentos_Click"
                                            OnInit="lxbVisualizarVencimentos_Init"><img alt="" src="../imagens/visualizar20x20.png" style="border:0px"/> Visualizar Vencimentos</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="lkbHistoricos" runat="server" Font-Size="8pt" OnClick="lkbHistoricos_Click"
                                            OnInit="lkbHistoricos_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Visualiza o histórico dos registros')">Registros em Histórico</asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" width="35%">
            </td>
            <td>
                <asp:CheckBox ID="chkPeriodico" Text="Periódico" runat="server" />
                <br />                
            </td>
        </tr>
        <tr>
            <td align="right" width="35%">
                Anexos:
            </td>
            <td>
                <asp:UpdatePanel ID="UPanexosDiversos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnUploadDiverso" runat="server" CssClass="ButtonUpload" Height="22px"
                            OnClientClick="tooltip.hide();" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Adiciona arquivos ao Vencimento Diverso')"
                            Text="     Inserir / Visualizar Anexos" Width="170px" 
                            OnClick="btnUploadDiverso_Click" OnInit="btnUploadDiverso_Init" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <div id="content_conteudos" style="text-align: center !important;">
                    <asp:UpdatePanel ID="UPNotificacoes" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="text-align: left; margin-bottom: 8px;">
                                <strong><span class="style1">Notificações:</span></strong>
                                <asp:ImageButton ID="ibtnAddNotificacoes" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                    OnClick="ibtnAddNotificacoes_Click" OnInit="ibtnAddNotificacoes_Init" /></div>
                            <div class="container_grids" style="text-align: center !important;">
                                <asp:GridView ID="grvNotificacoes" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="4" DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333"
                                    GridLines="None" OnRowDeleting="grvNotificacoes_RowDeleting" PageSize="4" Width="100%"
                                    OnPageIndexChanging="grvNotificacoes_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="DiasAviso" HeaderText="Dias" />
                                        <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                        <asp:BoundField DataField="Emails" HeaderText="E-mails" />
                                        <asp:TemplateField HeaderText="Excluir">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarNotificacoes" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:ImageButton ID="ibtnExcluir5" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                    onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as notificações selecionadas')"
                                                    OnPreRender="ibtnExcluir5_PreRender" />
                                                <input id="ckbSelecionar1" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGridVerificandoPermissao(this, 'chkSelecionarNotificacoes')" />
                                            </HeaderTemplate>
                                            <HeaderStyle Width="45px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="False" ForeColor="White"
                                        HorizontalAlign="Left" />
                                    <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                                <asp:HiddenField ID="hfId" runat="server" Value="0" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right" width="35%">
            </td>
            <td>
                <asp:Button ID="Button1" CssClass="Button" runat="server" Text="Salvar" OnClick="Button1_Click"
                    ValidationGroup="rfvSalvarDiverso" OnInit="Button1_Init" />&nbsp;
                <asp:Button ID="btnNovo" runat="server" CssClass="Button" Text="Novo" OnClick="btnNovo_Click" />&nbsp;
                <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="ButtonExcluir"
                    OnClick="btnExcluir_Click" OnPreRender="btnExcluir_PreRender" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="popups" style="width: 100%">
        
        <div id="cadastro_tipo" class="pop_up" style="width: 500px">
            <div id="div_fechar_cadastro_tipo" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Tipos de Vencimentos Diversos</div>
            <div id="campos_cadastro_tipo">
                <table style="width: 100%" cellspacing="5">
                    <tr>
                        <td align="right" width="20%">
                            Nome*:
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="UPNomeTipo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="tbxNomeTipo" runat="server" Width="200px" TextMode="SingleLine"
                                        CssClass="TextBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbxNomeTipo"
                                        Display="Dynamic" ErrorMessage="Obrigatório!" ValidationGroup="rfvSalvarTipo"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Button ID="btnSalvarTipo" runat="server" CssClass="Button" Text="Salvar" ValidationGroup="rfvSalvarTipo"
                                OnInit="btnSalvarTipo_Init" OnClick="btnSalvarTipo_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <div id="cadastro_status_diverso" class="pop_up" style="width: 500px">
            <div id="fechar_cadastro_status" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Cadastro de Status de Vencimentos Diversos</div>
            <div id="Div3">
                <asp:UpdatePanel ID="UPTituloTipo" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%" cellspacing="5">
                            <tr>
                                <td align="right" width="20%">
                                    Tipo:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxTipoStatus" runat="server" CssClass="TextBox" ReadOnly="True"
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="20%">
                                    Nome*:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxNomestatus" runat="server" Width="200px" TextMode="SingleLine"
                                        CssClass="TextBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Obrigatório!"
                                        ValidationGroup="rfvSalvarStatus" ControlToValidate="tbxNomestatus" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnSalvarStatus" runat="server" CssClass="Button" Text="Salvar" ValidationGroup="rfvSalvarStatus"
                                        OnInit="btnSalvarStatus_Init" OnClick="btnSalvarStatus_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div id="divPopUpCadastroNotificacao" style="width: 805px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
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

        <div id="divPopUpVencimentos" style="width: 697px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
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
                                            Width="20px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o vencimento selecionado')"
                                            OnClick="ibtnRemoverVencimento_Click1" />
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
                                        <asp:Button ID="btnSalvarvencimentos" runat="server" CssClass="Button" OnClick="btnSalvarvencimentos_Click"
                                            Text="Salvar" Width="170px" OnInit="btnSalvarvencimentos_Init" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlVencimentos" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ibtnRemoverVencimento" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div id="divPopUpAlteracaoStatus" style="width: 805px; display: block; top: 0px; left: 0px;" class="pop_up_super_super_super">
            <div>
                <div id="fecharAlteracaoStatus" class="btn_cancelar_popup" style="display: none">
                </div>
                <div class="barra_titulo">
                    Alteração de Status
                </div>
            </div>
            <div>
                <div>
                    <asp:UpdatePanel ID="upPopStatus" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td align="left">
                                        <strong>Você alterou os seguintes Status de Vencimento - Caso deseja enviar um email
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
                                                        <asp:CheckBoxList ID="ckbEmpresasAlteracao" runat="server" CssClass="chkEmailsEmpresasCont">
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
                                        <strong>Registros em Histórico</strong> &nbsp;<asp:TextBox ID="tbxHistoricoAlteracao"
                                            runat="server" CssClass="TextBox" Height="56px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:CheckBox ID="chkEnviarEmail" runat="server" Checked="True" Text="Enviar e-mail com alteração de status" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnAlterarStatus" runat="server" CssClass="Button" OnClick="btnAlterarStatus_Click"
                                            OnInit="btnAlterarStatus_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva as notificações com os dias e os e-mails selecionados')"
                                            Text="Enviar/Salvar" Width="170px" />
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div id="divPopHistoricos" style="width: 866px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
            <div>
                <div id="fecharHistoricos" class="btn_cancelar_popup">
                </div>
                <div class="barra_titulo">
                    Registros em Históricos</div>
            </div>
            <div>
                <div>
                    <asp:UpdatePanel ID="UPHistoricos" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
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
                                                    <asp:TemplateField HeaderText="Data do Registro">
                                                        <ItemTemplate>
                                                            <%# bindDataRegistro(Container.DataItem) %></ItemTemplate>
                                                        <ItemStyle Width="105px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Alteracao" HeaderText="Título">
                                                        <ItemStyle Width="435px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Observacao" HeaderText="Descrição">
                                                        <ItemStyle Width="300px" />
                                                    </asp:BoundField>
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
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnEnviarHistorico" runat="server" CssClass="Button" OnClick="btnEnviarHistorico_Click"
                                            OnInit="btnEnviarHistorico_Init" Text="Enviar Histórico Por Email" Width="220px" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ibtnAddObs" EventName="Click" />
                        </Triggers>
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
                                    <td align="left" colspan="2" class="style2">
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
                                        <asp:Button ID="btnEnviar" runat="server" CssClass="Button" Text="Enviar" Width="170px"
                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Envia o histórico para os e-mails selecionados')"
                                            OnClick="btnEnviar_Click" />
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
                                            <td colspan="2">
                                                <strong><asp:Label ID="lblItemDaRenovacao" runat="server" Text="<%# BindNomeItemDaRenovacao(Container.DataItem) %>"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Este vencimento já atingiu sua data de expiração.<br /> Para renová-lo informe a quantidade de dias de renovação do vencimento.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="17%" align="right">
                                                Dias de Renovação:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbxDiasRenovacaoPeriodicos" runat="server" CssClass="TextBox" Text="<%# BindDiasRenovacao(Container.DataItem) %>"></asp:TextBox>&nbsp;&nbsp;
                                                <asp:ImageButton ID="ibtnAddDiasRenovacaoPeriodicos" runat="server" ImageUrl="~/imagens/icone_adicionar.png" CommandName="SelComand" 
                                                    CommandArgument="<%# BindIdItemRenovacao(Container.DataItem) + ';' + BindTipoItemRenovacao(Container.DataItem) %>" />
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
                Renovação de Vencimentos Periódicos
            </div>
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
                                        <td align="right" colspan="2">&nbsp;</td>
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
            <div class="barra_titulo">Arquivos</div>
            <div>
                <asp:UpdatePanel ID="UPFrameArquivos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <iframe runat="server" name="conteudo" width="545px" height="435px" marginwidth="0" marginheight="0" scrolling="no" frameborder="0" id="conteudo" ></iframe>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
</asp:Content>
