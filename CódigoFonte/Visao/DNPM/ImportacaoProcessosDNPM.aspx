<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="ImportacaoProcessosDNPM.aspx.cs" Inherits="DNPM_ImportacaoProcessosDNPM" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () { CriarEventos(); });

        function CriarEventos() {

            $('.checkEvento').change(function () { PegarValoresEventosMarcados(); });
            MarcarDesmarcarTodosRegimes();
        }

        function MarcarDesmarcarTodosRegimes() {
            $('.chkTodosRegimes').change(function () {
                var estacochk = new Boolean();
                estadochk = $(this).attr('checked');

                $('.checkEvento').each(function (index) {
                    if (estadochk) {
                        $(this).attr('checked', 'checked');
                        $("#<%= hfCodigosEventosMarcados.ClientID %>").val($("#<%= hfCodigosEventosMarcados.ClientID %>").val() + '#' + $(this).val());
                    } else {
                        $(this).removeAttr('checked');
                        $("#<%= hfCodigosEventosMarcados.ClientID %>").val('');
                    }
                });

                console.log($("#<%= hfCodigosEventosMarcados.ClientID %>").val());
            });
        }

        function PegarValoresEventosMarcados() {

            $("#<%= hfCodigosEventosMarcados.ClientID %>").val('');

            $('.checkEvento').each(function (index) {

                if ($(this).is(':checked')) {
                    $("#<%= hfCodigosEventosMarcados.ClientID %>").val($("#<%= hfCodigosEventosMarcados.ClientID %>").val() + '#' + $(this).val());
                } 
            });

            console.log($("#<%= hfCodigosEventosMarcados.ClientID %>").val());
        }

        function setarDados() {
            
            $("#<%= hfDataAbertura.ClientID %>").val($('#ctl00_conteudo_lblDataProtocolo').text());
            $("#<%= hfTipoFase.ClientID %>").val($('#ctl00_conteudo_lblTipoFase').text());
            $("#<%= hfDetalhamento.ClientID %>").val($('#ctl00_conteudo_lblTipoRequerimento').text());

            //percorrendo os itens da tabela de substancias
            $('#ctl00_conteudo_gridSubstancias').children("tbody").children("tr").each(function (index) {                
                if (index != 0) {
                    $("#<%= hfSubstancias.ClientID %>").val($("#<%= hfSubstancias.ClientID %>").val() + $(this).children("td").first().text().trim() + "#");
                }
            });

            //pegando a cidade e o estado
            $('#ctl00_conteudo_gridMunicipios').children("tbody").children("tr").each(function (index) {
                if (index != 0) {
                    var cidadeEstado = $(this).children("td").first().text().trim(); 
                    var arr = cidadeEstado.split('/');
                    $("#<%= hfCidade.ClientID %>").val(arr[0]);
                    $("#<%= hfEstado.ClientID %>").val(arr[1]); 
                }
            });

            //pegando os eventos
            $("#<%= hfCodigosEventosMarcados.ClientID %>").val('');
            $('#ctl00_conteudo_gridEventos').children("tbody").children("tr").each(function (index) {
                if (index != 0) {                    

                    var descEvento = $(this).children("td").first().text().trim();
                    var codDesEvento = descEvento.split('-');

                    $('#checkBoxes').append($(document.createElement('input')).attr({ id: 'myCheckbox', name: 'myCheckbox', value: codDesEvento[0].trim() + '&' + codDesEvento[1].trim() + '-' + $(this).children("td").last().text().trim(), type: 'checkbox', class: 'checkEvento', checked: true }));
                    $('#checkBoxes').append(codDesEvento[1].trim() + ' - ' + $(this).children("td").last().text().trim() + '<br />');

                    $("#<%= hfCodigosEventosMarcados.ClientID %>").val($("#<%= hfCodigosEventosMarcados.ClientID %>").val() + '#' + codDesEvento[0].trim() + '&' + codDesEvento[1].trim() + '-' + $(this).children("td").last().text().trim());
                   
                }
            });            
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Importação de processos dnpm
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblImportacaoProcesso" runat="server" Text=""></asp:Label>
    <asp:ModalPopupExtender ID="lblImportacaoProcesso_popupextender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fecharImportacaoProcessoDnpm"
        PopupControlID="divImportacaoProcessoDnpm" TargetControlID="lblImportacaoProcesso">
    </asp:ModalPopupExtender>
    <div style="text-align: center">
        <div id="conteudo_sistema" style="width: 100%;">
            <table width="100%">
                <tr>
                    <td>Informe o número dos processos que deseja importar do DNPM
                        <br />
                        (Separe os processos com ';')
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="tbxProcessos" runat="server" TextMode="MultiLine" Height="152px" Width="98%"></asp:TextBox>                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <!-- -->
                        <asp:Button ID="btnIniciarImportacao" runat="server" CssClass="Button" Text="Iniciar Importação" Width="170px" OnClick="btnIniciarImportacao_Click" OnInit="btnIniciarImportacao_Init" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="divImportacaoProcessoDnpm" style="width: 700px; display: block; top: 0px; left: 0px;" class="pop_up_super_super">
        <div id="fecharImportacaoProcessoDnpm" class="btn_cancelar_popup" style="display:none;">
        </div>
        <div>
            <asp:UpdatePanel ID="upRegimes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <script type="text/javascript">
                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { setarDados(); CriarEventos(); });
                    </script>
                    <div class="barra_titulo">
                        Processo:&nbsp;
            <asp:Label ID="lblNumeroProcessoTitulo" runat="server"></asp:Label>
                    </div>
                    <div>                        
                        <div id="mensagem_problema_regimes" runat="server">                            
                            <asp:Label ID="lblMensagemProblema" runat="server"></asp:Label>
                        </div>
                        <div id="regimes" runat="server">                            
                            <div>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left" colspan="2">Para o processo&nbsp;
                                        <asp:Label ID="lblNumeroProcesso" runat="server"></asp:Label>&nbsp;
                                        deverão ser criados os regimes e eventos listados abaixo.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">Selecione os regimes e eventos que serão criados para o processo:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">&nbsp; &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <div style="max-height: 300px; overflow: auto; border-radius: 4px; background-color: White; border: 1px solid #a6a5a5;">
                                                <input id="marcarTodosRegimes" type="checkbox" class="chkTodosRegimes" checked="checked" />&nbsp;
                                            <strong>Todos:</strong>
                                                <div id="checkBoxes"></div>                                                
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">&nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                        </div>                        
                        <asp:HiddenField ID="hfDataAbertura" runat="server" />
                        <asp:HiddenField ID="hfIdEmpresa" runat="server" />
                        <asp:HiddenField ID="hfTipoFase" runat="server" />
                        <asp:HiddenField ID="hfDetalhamento" runat="server" />
                        <asp:HiddenField ID="hfSubstancias" runat="server" />
                        <asp:HiddenField ID="hfCidade" runat="server" />
                        <asp:HiddenField ID="hfEstado" runat="server" />
                        <asp:HiddenField ID="hfCodigosEventosMarcados" runat="server" />
                        <div style="text-align: right; margin-top:15px;">
                            <asp:Button ID="btnContinuar" runat="server" CssClass="Button" Text="Continuar" Width="170px" OnClick="btnContinuar_Click" />&nbsp;
                        <asp:Button ID="btnImportarProcesso" runat="server" CssClass="Button" Text="Importar Processo" Width="170px" OnClick="btnImportarProcesso_Click" />&nbsp;
                        <asp:Button ID="btnCancelarImportacao" runat="server" CssClass="ButtonExcluir" Text="Cancelar Importação" onmouseout="tooltip.hide();"
                            onmouseover="tooltip.show('Cancela a Importação de Processos')" OnClick="btnCancelarImportacao_Click" OnPreRender="btnCancelarImportacao_PreRender" />
                        </div>
                        <div style="display: block; height: 0px; overflow: hidden;">
                            <asp:Label ID="lblConteudoProcesso" runat="server"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

