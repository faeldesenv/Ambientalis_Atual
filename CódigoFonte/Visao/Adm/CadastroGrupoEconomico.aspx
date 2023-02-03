<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true"
    CodeFile="CadastroGrupoEconomico.aspx.cs" Inherits="Site_Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Funcoes.js" type="text/javascript"></script>
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () { CriarEventos(); });

        function CriarEventos() {
            $('#<%=rbtnPessoaFisica.ClientID %>').change(function () { rbtnTipoPessoaClick(this) });
            $('#<%=rbtnPessoaJuridica.ClientID %>').change(function () { rbtnTipoPessoaClick(this) });
            $('#<%=chkIsentoICMS.ClientID %>').click(function () { chkIsento(this); });
            $('#<%=tbxEmail.ClientID %>').focusout(function () { verificarEMail(this); });
            $('#<%=ddlSistema.ClientID %>').change(function () { habilitarVisualizacao(this); });

            ///Máscaras
            $("#<%=tbxCNPJ.ClientID %>").mask("99.999.999/9999-99");
            //$('#<%=tbxCNPJ.ClientID %>').keypress(function () { MascaraCNPJ(this); });
            $('#<%=tbxCPF.ClientID %>').mask("999.999.999-99");
            //$('#<%=tbxCPF.ClientID %>').keypress(function () { MascaraCPF(this); });
            $('#<%=tbxCEP.ClientID %>').mask("99.999-999");
            //$('#<%=tbxCEP.ClientID %>').keypress(function () { MascaraCep(this); });           
            $('#<%=tbxRamal.ClientID%>').maskMoney({ thousands: '', decimal: '' });
            $('#<%=tbxLimiteEmpresas.ClientID%>').maskMoney({ thousands: '', decimal: '' });
            $('#<%=tbxLimiteUsuarioEdicao.ClientID%>').maskMoney({ thousands: '', decimal: '' });
            $('#<%=tbxLimiteProcessos.ClientID%>').maskMoney({ thousands: '', decimal: '' });

            //IniciarCampos
            habilitarPessoaFisica('none');
            habilitarPessoaJuridica('block');
            $('#<%=lblEmailInvalido.ClientID %>').hide();

            if ($('#<%=rbtnPessoaFisica.ClientID %>').attr('checked')) {
                habilitarPessoaFisica('block');
                habilitarPessoaJuridica('none');
            }
        }

        function verificarEMail(campoEmail) {
            if (!ValidaEMail($(campoEmail)))
                $('#<%=lblEmailInvalido.ClientID %>').show();
            else
                $('#<%=lblEmailInvalido.ClientID %>').hide();
        }

        function chkIsento(chk) {
            if ($('#<%=chkIsentoICMS.ClientID %>').attr('checked'))
                $('#<%=tbxInscricaoEstadual.ClientID %>').attr('disabled', true);
            else
                $('#<%=tbxInscricaoEstadual.ClientID %>').attr('disabled', false);
        }

        function rbtnTipoPessoaClick(radioBtn) {
            if ($(radioBtn).attr('id') == $('#<%=rbtnPessoaFisica.ClientID%>').attr('id')) {
                $('#<%=rbtnPessoaJuridica.ClientID%>').attr('checked', false);
                habilitarPessoaFisica('block');
                habilitarPessoaJuridica('none');
            } else {
                $('#<%=rbtnPessoaFisica.ClientID%>').attr('checked', false);
                habilitarPessoaFisica('none');
                habilitarPessoaJuridica('block');
            }
        }

        function habilitarPessoaFisica(habilitar) {
            $('#tabelaPessoaFisica').css('display', habilitar);
        }

        function habilitarPessoaJuridica(habilitar) {
            $('#tabelaPessoaJuridica').css('display', habilitar);
        }

        function habilitarVisualizacao(ddl) {

            if (eval($(ddl).val()) == 1) {
                $('.visualizacao_conts').css('display', 'none');
            } else {
                $('.visualizacao_conts').css('display', 'block');
            }
        }

        function ValidateFile(source, args) {
            var fileAndPath = document.getElementById(source.controltovalidate).value;
            var lastPathDelimiter = fileAndPath.lastIndexOf("\\");
            var fileNameOnly = fileAndPath.substring(lastPathDelimiter + 1);
            var file_extDelimiter = fileNameOnly.lastIndexOf(".");
            var file_ext = fileNameOnly.substring(file_extDelimiter + 1).toLowerCase();
            if (file_ext != "jpg") {
                args.IsValid = false;
                if (file_ext != "gif")
                    args.IsValid = false;
                if (file_ext != "png") {
                    args.IsValid = false;
                    return;
                }
            }
            args.IsValid = true;
        }
    </script>
    <style type="text/css">
        .paragrafo
        {
            text-indent: 10mm;
            font-family: Arial;
            font-size: 12px;
            margin: 4px 15px 0 55px;
        }
        
        .titulo
        {
            text-align: center;
            margin: 12px auto;
        }
        .clausula
        {
            margin-top: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <table width="100%" cellpadding="0" cellspacing="5">
            <tr>
                <td width="40%" align="right">
                    Sistema*:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSistema" runat="server" CssClass="DropDownList" Width="200px"
                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Escolhe se será criado um cliente do Sistema Sustentar ou um cliente da Ambientalis')">
                        <asp:ListItem Value="0">Sustentar</asp:ListItem>
                        <asp:ListItem Value="1">Ambientalis</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Nome*:
                </td>
                <td>
                    <asp:TextBox ID="tbxNome" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNome"
                        ErrorMessage="Campo Obrigatório" ValidationGroup="rfvSalvar">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                    <asp:CheckBox ID="chkCancelado" runat="server" Text="Cancelado" onmouseout="tooltip.hide();"
                        onmouseover="tooltip.show('Um cliente cancelado não será excluído, mais perderá o acesso ao sistema')" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="visualizacao_conts" class="visualizacao_conts" runat="server" style="margin-top: 3px">
                        <table width="100%" cellspacing="5">
                            <tr>
                                <td align="right" width="40%">
                                    &nbsp;
                                </td>
                                <td align="left" valign="top">
                                    <asp:ImageButton ID="ibtnVisualizarContrato" runat="server" ImageUrl="~/imagens/visualizar20x20.png"
                                        OnClick="ibtnVisualizarContrato_Click" OnInit="ibtnVisualizarContrato_Init" />&nbsp;
                                    <asp:Label ID="lblVisualizarCont" runat="server" Text="Visualizar Contrato" Style="font-weight: 700;
                                        font-family: Arial; font-size: small;" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Listagem de empresas cadastradas neste grupo econômico')"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Controlar por:</td>
                <td>
                    <asp:DropDownList ID="ddlTipoControle" runat="server" CssClass="DropDownList" 
                        Width="305px" AutoPostBack="True" 
                        onselectedindexchanged="ddlTipoControle_SelectedIndexChanged">
                     <asp:ListItem Selected="True" Value="0">-- Selecione --</asp:ListItem>
                     <asp:ListItem Value="1">Limite de Empresas</asp:ListItem>
                     <asp:ListItem Value="2">Limite de Processos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblVisualizarContratoAdm" runat="server"></asp:Label>
                    <asp:ModalPopupExtender ID="modalContratos_extender" runat="server" BackgroundCssClass="simplemodal"
                        CancelControlID="fecharVisualizarContratos" PopupControlID="divPopUpVisualizarContratos"
                        TargetControlID="lblVisualizarContratoAdm">
                    </asp:ModalPopupExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                 <div>
                     <asp:UpdatePanel ID="UPEMpresasProcessos" runat="server" 
                         UpdateMode="Conditional">
                      <ContentTemplate>
                        <table width="100%">
                        <tr>
                         <td width="40%" align="right">
                          Limite de Empresas:
                         </td>
                         <td>
                          <asp:TextBox ID="tbxLimiteEmpresas" runat="server" Width="305px" CssClass="TextBox" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade máxima de empresas que será possível criar dentro do grupo econômico. A quantidade 0(zero) significa ilimitado.')"></asp:TextBox>
                         </td>
                        </tr>
                        <tr>
                         <td width="40%" align="right">
                          Limite de Processos:</td>
                         <td>
                          <asp:TextBox ID="tbxLimiteProcessos" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                         </td>
                        </tr>
                       </table>
                      </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddlTipoControle" 
                                 EventName="SelectedIndexChanged" />
                         </Triggers>
                     </asp:UpdatePanel>
                    </div>                    
                </td>
            </tr>
            
            <tr>
                <td width="40%" align="right">
                    Limite de Usuários:
                </td>
                <td>
                    <asp:TextBox ID="tbxLimiteUsuarioEdicao" runat="server" Width="305px" CssClass="TextBox"
                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade máxima de usuários com permissão de edição que será possível criar dentro do grupo. A quantidade 0(zero) não deixará criar nenhum usuário de edição.')">
                    </asp:TextBox>
                </td>
            </tr>            
            <tr>
                <td width="40%" align="right">
                    Permissões por:</td>
                <td>
                    <asp:RadioButtonList ID="rblTipoPermissaoPor" runat="server" RepeatDirection="Horizontal" CellSpacing="2" Enabled="False">
                        <asp:ListItem Value="N">Gestão com edição coletiva</asp:ListItem>
                        <asp:ListItem Value="C">Gestão com edição nomeada</asp:ListItem>
                    </asp:RadioButtonList>                   
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    <strong>Tipo de Pessoa:</strong>
                </td>
                <td>
                    <asp:RadioButton ID="rbtnPessoaFisica" runat="server" Text="Física" />
                    <asp:RadioButton ID="rbtnPessoaJuridica" runat="server" Checked="True" Text="Jurídica" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <div id="tabelaPessoaFisica">
                        <table width="100%">
                            <tr>
                                <td width="40%" align="right">
                                    Sexo:
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="rblSexo" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Selected="True" Value="M">Masculino</asp:ListItem>
                                        <asp:ListItem Value="F">Feminino</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" align="right" id="Td1">
                                    CPF:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxCPF" runat="server" Width="305px" CssClass="TextBox">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" align="right" id="Td2">
                                    Data de Nascimento:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxDataNascimento" runat="server" Width="305px" CssClass="TextBox">
                                    </asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataNascimento">
                                    </asp:CalendarExtender>
                                    <asp:CalendarExtender ID="tbxDataNascimento_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="tbxDataNascimento">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" align="right">
                                    Estado Civil:
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="rblEstadoCivil" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Selected="True" Value="s">Solteiro(a) </asp:ListItem>
                                        <asp:ListItem Value="c">Casado(a) </asp:ListItem>
                                        <asp:ListItem Value="p">Separado(a) </asp:ListItem>
                                        <asp:ListItem Value="d">Divorciado(a) </asp:ListItem>
                                        <asp:ListItem Value="v">Viúvo(a) </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" align="right" id="Td3">
                                    RG:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxRG" runat="server" Width="305px" CssClass="TextBox">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <div id="tabelaPessoaJuridica">
                        <table width="100%">
                            <tr>
                                <td width="40%" align="right">
                                    CNPJ:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxCNPJ" runat="server" Width="305px" CssClass="TextBox">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    Razão Social:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxRazaoSocial" runat="server" Width="305px" CssClass="TextBox">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                    <asp:Label ID="Label182" runat="server" Text="Campos Bloqueados" Font-Bold="True"
                        onmouseout="tooltip.hide();" onmouseover="tooltip.show('Esses campos poderão ser editados pelos clientes, quando os mesmos acessarem suas bases.')"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Responsável:
                </td>
                <td>
                    <asp:TextBox ID="tbxResponsavel" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Representante Legal:
                </td>
                <td>
                    <asp:TextBox ID="tbxRepresentanteLegal" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Gestor Econômico:
                </td>
                <td>
                    <asp:TextBox ID="tbxGestorEconomico" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Site:
                </td>
                <td>
                    <asp:TextBox ID="tbxSite" runat="server" CssClass="TextBox" Width="305px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" valign="top">
                    E-mails (separar por ponto-e-virgula):
                </td>
                <td>
                    <asp:TextBox ID="tbxEmail" runat="server" Width="305px" CssClass="TextBox" Height="123px"
                        TextMode="MultiLine" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                    <asp:Label ID="lblEmailInvalido" runat="server" ForeColor="Red" Text="E-mail Inválido!">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" class="style1">
                    Telefone:
                </td>
                <td class="style1">
                    <asp:TextBox ID="tbxTelefone" runat="server" Width="123px" CssClass="TextBox">
                    </asp:TextBox>&nbsp;Celular:<asp:TextBox ID="tbxCelular" runat="server" Width="123px"
                        CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" class="style1">
                    Fax:
                </td>
                <td class="style1">
                    <asp:TextBox ID="tbxFax" runat="server" Width="123px" CssClass="TextBox">
                    </asp:TextBox>
                    &nbsp;Ramal:<asp:TextBox ID="tbxRamal" runat="server" Width="62px" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Nacionalidade:
                </td>
                <td>
                    <asp:TextBox ID="tbxNacionalidade" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    <strong>Endereço</strong>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Logradouro:
                </td>
                <td>
                    <asp:TextBox ID="tbxLogradouro" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Número:
                </td>
                <td>
                    <asp:TextBox ID="tbxNumero" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Complemento:
                </td>
                <td>
                    <asp:TextBox ID="tbxComplemento" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Bairro:
                </td>
                <td>
                    <asp:TextBox ID="tbxBairro" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    CEP:
                </td>
                <td>
                    <asp:TextBox ID="tbxCEP" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Ponto de Referência:
                </td>
                <td>
                    <asp:TextBox ID="tbxPontoReferencia" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged"
                        Width="305px" CssClass="DropDownList">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Cidade:
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCidade" runat="server" Width="305px" CssClass="DropDownList">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="chkIsentoICMS" runat="server" Text="Isento de ICMS:" />
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Inscrição Estadual:
                </td>
                <td>
                    <asp:TextBox ID="tbxInscricaoEstadual" runat="server" Width="170px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Detalhamento:
                </td>
                <td>
                    <asp:TextBox ID="tbxObservacoes" runat="server" Width="305px" CssClass="TextBox"
                        TextMode="MultiLine">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <b>Ativação do Grupo Econômico</b>
                    <div id="ativacao_grupoEconomico" style="border: 1px solid Black">
                        <table style="width: 100%">
                            <tr>
                                <td align="right" style="width: 40%">
                                    Ativado pela Logus:
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:Label ID="lblAtivadoLogus" runat="server" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Informa se o grupo econômico foi habilitado pela Logus. A ativação é fundamental para que possam ser inseridos dados neste grupo.')"></asp:Label>
                                </td>
                                <td align="left" class="style2">
                                    <asp:Button ID="btnAtivarLogus" runat="server" CssClass="Button" Text="Ativar (Logus)"
                                        Width="200px" OnClick="btnAtivarLogus_Click" OnInit="btnAtivarLogus_Init" OnPreRender="btnAtivarLogus_PreRender" />
                                    <asp:Label ID="lblAux" runat="server"></asp:Label>
                                    <asp:ModalPopupExtender ID="modal_ativacao" runat="server" BackgroundCssClass="simplemodal"
                                        CancelControlID="div_fechar_popup_ativacao" DynamicServicePath="" Enabled="True"
                                        PopupControlID="pop_up_ativacao" TargetControlID="lblAux">
                                    </asp:ModalPopupExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Ativado pela Ambientalis:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblAtivoAmbientalis" runat="server" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Informa se o grupo econômico foi habilitado pela Ambientalis. A ativação é fundamental para que possam ser inseridos dados neste grupo.')"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnAtivarAmbientalis" runat="server" CssClass="Button" Text="Ativar (Ambientalis)"
                                        Width="200px" OnClick="btnAtivarAmbientalis_Click" OnInit="btnAtivarLogus_Init"
                                        OnPreRender="btnAtivarLogus_PreRender" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DataGrid ID="dgr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    BorderColor="#666666" CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None"
                                    OnDeleteCommand="dgr_DeleteCommand" OnEditCommand="dgr_EditCommand" OnItemDataBound="dgr_ItemDataBound"
                                    OnPageIndexChanged="dgr_PageIndexChanged" Width="100%">
                                    <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                                        Mode="NumericPages" NextPageText="" />
                                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                    <ItemStyle BackColor="#F7F6F3" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Id" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Nome" HeaderText="Nome"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Responsavel" HeaderText="Responsável"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Telefone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTelefone" runat="server" CssClass="Label" Text="<%# bindTelefone(Container.DataItem) %>"></asp:Label>
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
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:ImageButton ID="ibtnExcluir0" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                    OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                                <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientes(this)" />
                                            </HeaderTemplate>
                                            <HeaderStyle Width="45px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                        VerticalAlign="Top" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfBD" runat="server" />
                    <asp:HiddenField ID="hfCnpjCpf" runat="server" Visible="False" />
                </td>
                <td align="left">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="Button" OnClick="btnSalvar_Click"
                        Text="Salvar" ValidationGroup="rfvSalvar" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva novo grupo ou salva as alterações feitas no grupo.')" />
                    <asp:Button ID="btnNovo" runat="server" CssClass="Button" OnClick="btnNovo_Click"
                        Text="Novo" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre os campos para cadastro de novo grupo econômico.')" />
                        
                        <asp:Button ID="btnExcluir" runat="server" CssClass="ButtonExcluir" 
                            onclick="btnExcluir_Click" Text="Excluir" onmouseout="tooltip.hide();" 
                            onmouseover="tooltip.show('Exclui o orgão ambiental carregado')" />
                        
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Barra">
    <p>
        Cadastro de grupo econômico</p>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="popups">
    <div id="pop_ups_grupo_economico">
        <div id="pop_up_ativacao" class="pop_up" style="width: 500px">
            <div id="div_fechar_popup_ativacao" class="btn_cancelar_popup">
            </div>
            <asp:UpdatePanel ID="upAtivacao" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td align="right" style="width: 25%">
                                Senha de Ativação*:
                            </td>
                            <td>
                                <asp:TextBox ID="tbxSenhaAtivacao" runat="server" TextMode="Password" Width="60%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Obrigatório!"
                                    ControlToValidate="tbxSenhaAtivacao" ValidationGroup="rfvAtivacao" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hfIdAdministrador" runat="server" Value="0" />
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
        </div>
    </div>
    <div id="divPopUpVisualizarContratos" style="width: 700px; border-radius: 9px" class="conteudo_interno">
        <div>
            <div id="fecharVisualizarContratos" style="position: relative; width: 25px; height: 29px;
                background-image: url('../imagens/x.png'); background-repeat: no-repeat; top: -5px;
                right: -12px; cursor: pointer; float: right;">
            </div>
            <div class="barra_titulo" style="margin: 10px">
                Visualização de Contratos/Aditivos</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="UpdatePanelVisualizarContratos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="660px">
                            <tr>
                                <td align="right" width="30%" style="font-family: Arial, Helvetica, sans-serif; font-size: 12px;">
                                    Contrato/Aditivo:
                                </td>
                                <td width="70%" align="left">
                                    &nbsp;
                                    <asp:DropDownList ID="ddlContrato" runat="server" CssClass="DropDownList" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlContrato_SelectedIndexChanged" Width="305px" DataTextField="NumeroContrato"
                                        DataValueField="Id">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="View7" runat="server">
                                            <table cellpadding="0" cellspacing="5" style="font-family: Arial, Helvetica, sans-serif;
                                                font-size: 12px; margin-left: 20px" width="660px">
                                                <tr>
                                                    <td>
                                                        <div id="contrato_original" align="left" style="padding: 5px; background-color: #FFFFFF;
                                                            padding: 10px; height: 372px; overflow: auto;">
                                                            <div id="imprimir_contrato_original">
                                                                <style type="text/css">
                                                                    .paragrafo
                                                                    {
                                                                        text-indent: 10mm;
                                                                        font-family: Arial;
                                                                        font-size: 12px;
                                                                        margin: 4px 15px 0 55px;
                                                                    }
                                                                    
                                                                    .titulo
                                                                    {
                                                                        text-align: center;
                                                                        margin: 12px auto;
                                                                        background-color: Silver;
                                                                        font-weight: bold;
                                                                    }
                                                                    .clausula
                                                                    {
                                                                        margin-top: 15px;
                                                                    }
                                                                    
                                                                    .logomarca
                                                                    {
                                                                        text-align: center;
                                                                        width: 100%;
                                                                        height: 105px;
                                                                        margin-bottom: 15px;
                                                                    }
                                                                </style>
                                                                <div class="logomarca">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="../imagens/logo_contrato.png" />
                                                                </div>
                                                                <div class="paragrafo titulo" style="font-size: 11pt;">
                                                                    CONTRATO DE ASSINATURA DE PRESTAÇÃO DE SERVIÇO WEB
                                                                    <div>
                                                                        Nº
                                                                        <asp:Label ID="lblContratoOriginalNumero" runat="server"></asp:Label></div>
                                                                </div>
                                                                <div class="paragrafo">
                                                                    O presente contrato, regido pelas condições e cláusulas descritas abaixo, constitui
                                                                    total entendimento entre
                                                                    <asp:Label ID="lblContratoOriginalContratante" runat="server"></asp:Label>
                                                                    , doravante denominado <strong>USUÁRIO</strong> e do outro lado, <strong>AMBIENTALIS
                                                                        ASSESSORIA E SERVIÇOS LTDA</strong>, localizada à Rod. Fued Nemer, s/n – km
                                                                    02, Santa Bárbara, Castelo/ES - CEP.: 29360-000, devidamente registrada no CNPJ/MF
                                                                    sob nº 11.259.526/0001-07, Inscrição Estadual n° [ISENTA] e; <strong>LOGUS SISTEMAS
                                                                        LTDA - EPP</strong>, localizada à Rua Hyercem Machado, nº 26, Bairro Gilberto
                                                                    Machado, Cachoeiro de Itapemirim/ES - CEP.:29.303-312, devidamente registrado no
                                                                    CNPJ/MF sob nº 36.420.818/0001-00, Inscrição Estadual nº [ISENTA], doravante denominadas
                                                                    <strong>CONTRATADAS</strong>.</div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA PRIMEIRA - DO OBJETO</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>1.</strong> &nbsp;O objeto deste presente instrumento é a assinatura do
                                                                    <strong>SERVIÇO WEB - SISTEMA SUSTENTAR</strong> para o <strong>USUÁRIO</strong>
                                                                    monitorar/acompanhar seus processos administrativos ambientais e/ou minerários,
                                                                    de forma intransferível e não exclusiva, obrigando-se os usuários a o utilizarem
                                                                    unicamente para as atividades a que se destina - Lei 9.609, de fevereiro de 1.998,
                                                                    da seguinte forma:
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="center" colspan="2" style="background-color: Silver; font-size: 11pt;">
                                                                                <strong>PLANO DE ASSINATURA</strong>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" width="5%">
                                                                                01
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblContratoOriginalEmpresas" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                02
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblContratoOriginalUsuarios" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <table width="100%" style="background-color: Silver;">
                                                                                    <tr>
                                                                                        <td align="center" width="50%">
                                                                                            <strong>VALOR MENSAL</strong>
                                                                                        </td>
                                                                                        <td align="center">
                                                                                            <strong>
                                                                                                <asp:Label ID="lblContratoOriginalTotal" runat="server"></asp:Label></strong>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <asp:Label ID="lblModulosContratoOriginal" runat="server"></asp:Label><br />
                                                                <div class="paragrafo">
                                                                    <strong>
                                                                        <asp:Label ID="lblMesesCarencia" runat="server" Text="Label"></asp:Label></strong>
                                                                </div>
                                                                <div class="paragrafo clausula">
                                                                    <strong>1.1.</strong> &nbsp;Este documento apresenta os termos e condições da prestação
                                                                    do serviço do sistema <strong>SUSTENTAR</strong> para acompanhamento de Informações
                                                                    sobre Processos Administrativos Ambientais e Minerários lançados pelo próprio <strong>
                                                                        USUÁRIO</strong>, devidamente registrado no cartório geral de imóveis, títulos
                                                                    e documentos de pessoas jurídicas da comarca de Castelo, estado do Espírito Santo,
                                                                    sob nº. 5889, Livro B – 5, protocolado sob nº. 2.365, na data de 31 de julho de
                                                                    2012, cujo teor o cliente declara conhecer e concordar na sua totalidade.</div>
                                                                <div class="paragrafo">
                                                                    <strong>1.2.</strong> &nbsp;O serviço contratado tem como finalidade viabilizar
                                                                    o acompanhamento por meio eletrônico, site: www.sustentar.inf.br dos processos administrativos
                                                                    ambientais e minerários realizados unicamente pelo <strong>USUÁRIO</strong>.</div>
                                                                <div class="paragrafo">
                                                                    <strong>1.3.</strong> &nbsp;Constitui infração, e é expressamente proibida a comercialização,
                                                                    reprodução, modificação ou distribuição, de parte ou totalidade das informações
                                                                    e todo o conteúdo contido em ambos os serviços, sem prévia e expressa autorização.</div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA SEGUNDA - DO PREÇO E DA FORMA DE PAGAMENTO</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>2.</strong> &nbsp;Para a utilização dos serviços ora avençados, será cobrada
                                                                    uma taxa mensal no valor de <strong>
                                                                        <asp:Label ID="lblPrecoMensalidade" runat="server"></asp:Label></strong>, com
                                                                    primeiro vencimento imediato após “aceite” do contrato, e demais pagamentos todo
                                                                    dia 15 (quinze) dos meses subsequentes.</div>
                                                                <div class="paragrafo">
                                                                    <strong>2.1.</strong> &nbsp;Na hipótese de não pagamento, conforme cláusula anterior,
                                                                    o <strong>USUÁRIO</strong>, além de ter o seu serviço suspenso, passados 10 (dez)
                                                                    dias do vencimento da mensalidade, igualmente incorrerá na aplicação de multa contratual
                                                                    de 2% (dois por cento) e juros de mora de 1% (um por cento) ao mês, sobre o valor
                                                                    total do débito, calculado da data do vencimento até a data do efetivo pagamento.</div>
                                                                <div class="paragrafo">
                                                                    <strong>2.2.</strong> &nbsp;A mensalidade será enviada automaticamente, no ato da
                                                                    assinatura, através de Nota Fiscal juntamente com o Boleto bancário, no qual, conterá
                                                                    a instrução de protesto expressa. Contudo, o valor descrito seguirá as opções de
                                                                    serviços escolhidas pelo próprio <strong>USUÁRIO</strong>.</div>
                                                                <div class="paragrafo">
                                                                    <strong>2.3.</strong> &nbsp;O valor da mensalidade será reajustado anualmente pelo
                                                                    IGPM (Índice Geral de Preços de Mercado) ou, na ausência deste, será adotado o índice
                                                                    oficial que o substituir, ou outro que esteja de acordo com a lei.</div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA TERCEIRA - DA PRESTAÇÃO DE SERVIÇOS</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>3.</strong> &nbsp;Os serviços contratados consistem em manter a atualização
                                                                    das funções existentes nos módulos do sistema <strong>SUSTENTAR</strong>, com relação
                                                                    às variáveis normalmente alteradas em virtude de legislação federal. Destarte, não
                                                                    é de responsabilidade das <strong>CONTRATADAS</strong> a realização de nenhuma outra
                                                                    atribuição e/ou tarefa.</div>
                                                                <div class="paragrafo">
                                                                    <strong>3.1.</strong> &nbsp;O <strong>USUÁRIO</strong> terá direito aos seguintes
                                                                    serviços disponibilizados pelo sistema <strong>SUSTENTAR</strong>:
                                                                    <div class="paragrafo">
                                                                        • Acesso à área restrita do serviço do sistema <strong>SUSTENTAR</strong>, quando
                                                                        autorizados expressamente pelos proprietários do sistema, por meio do site www.sustentar.inf.br.</div>
                                                                    <div class="paragrafo">
                                                                        • Suporte telefônico ou pelo atendimento on-line fornecido pelo site www.sustentar.inf.br
                                                                        disponibilizado pelas <strong>CONTRATADAS</strong>, em horário comercial.</div>
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>3.2.</strong> &nbsp;O <strong>USUÁRIO</strong> se compromete a não interferir
                                                                    de qualquer forma com a prestação dos serviços ora expostos, nem tentar acessá-los
                                                                    por meio diferente da interface disponibilizada.</div>
                                                                <div class="paragrafo">
                                                                    <strong>3.3.</strong> &nbsp;Deve o USUÁRIO usar os serviços em conformidade com
                                                                    a legislação vigente e conforme as instruções fornecidas pelas <strong>CONTRATADAS</strong>.</div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA QUARTA - DAS RESPONSABILIDADES DAS CONTRADAS</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>4.</strong> &nbsp;Liberar de imediato, após a confirmação da assinatura
                                                                    do sistema <strong>SUSTENTAR</strong>, a permissão de utilização do <strong>USUÁRIO</strong>
                                                                    ao site <a href="http://www.sustentar.inf.br" target="_blank">www.sustentar.inf.br</a>.</div>
                                                                <div class="paragrafo">
                                                                    <strong>4.1.</strong> &nbsp;Fornecer ao <strong>USUÁRIO</strong> o “login” e “senha
                                                                    de administrador”, que lhe dará acesso ao sistema <strong>SUSTENTAR</strong>.</div>
                                                                <div class="paragrafo">
                                                                    <strong>4.2.</strong> &nbsp;Enviar ao <strong>USUÁRIO</strong>, após a liberação
                                                                    prevista na cláusula 4, a cobrança da primeira parcela da taxa mensal de prestação
                                                                    de serviço do sistema <strong>SUSTENTAR</strong>.</div>
                                                                <div class="paragrafo">
                                                                    <strong>4.3.</strong> &nbsp;Sem prejuízo as <strong>CONTRATADAS</strong>, lhe são
                                                                    facultadas suspender o acesso ao sistema de forma total ou parcial na ocorrência
                                                                    de inadimplência pelo <strong>USUÁRIO</strong>, porém, reativar-se-á após a purgação
                                                                    da mora, conforme art. 390 do Código Civil.</div>
                                                                <div class="paragrafo">
                                                                    <strong>4.4.</strong> &nbsp;As
                                                                    <stron>CONTRATADAS</stron>
                                                                    reservam-se o direito de alterar qualquer um dos serviços ou produtos oferecidos
                                                                    ou incluir novos, não implicando em qualquer infração a este contrato, independente
                                                                    de comunicação ao <strong>USUÁRIO</strong>.</div>
                                                                <div class="paragrafo">
                                                                    <strong>4.5.</strong> &nbsp;É facultado às <strong>CONTRATADAS</strong> remover
                                                                    conteúdos que violem políticas legais e morais.</div>
                                                                <div class="paragrafo">
                                                                    <strong>4.6.</strong> &nbsp;Divulgar, durante a vigência desse contrato, sem necessidade
                                                                    de qualquer tipo de remuneração, em “home-pages” e quaisquer outros meios, que o
                                                                    USUÁRIO possui conta e é efetivo utilizador do Sistema <strong>SUSTENTAR</strong>
                                                                    e seus produtos.</div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA QUINTA - DAS OBRIGAÇÕES DOS USUÁRIOS</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>5.</strong> &nbsp;O sistema <strong>SUSTENTAR</strong> somente poderá ser
                                                                    utilizado por <strong>USUÁRIOS</strong> registrados no site, bem como, que se encontrem
                                                                    adimplentes com as mensalidades, de acordo com a cláusula 2.</div>
                                                                <div class="paragrafo">
                                                                    <strong>5.1.</strong> &nbsp;O <strong>USUÁRIO</strong> através de sua assinatura
                                                                    declara ter conhecimento de que o sistema <strong>SUSTENTAR</strong> é independente,
                                                                    portanto, não vincula-se a nenhum órgão minerário e/ou ambiental seccional, setorial,
                                                                    local ou quaisquer outros órgãos componentes do SISNAMA – SISTEMA NACIONAL DE MEIO
                                                                    AMBIENTE e/ou ANM - Agência Nacional de Mineração.</div>
                                                                <div class="paragrafo">
                                                                    <strong>5.2.</strong> &nbsp;Aquisição de equipamentos e recursos tecnológicos necessários
                                                                    para a utilização do sistema <strong>SUSTENTAR</strong>, indicados, quando necessário,
                                                                    pelas <strong>CONTRATADAS</strong>.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>5.3.</strong> &nbsp;Ter acesso à rede mundial de computadores – internet.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>5.4.</strong> &nbsp;Cabe ao <strong>USUÁRIO</strong> manter sigilo absoluto
                                                                    sobre a senha de acesso, comprometendo-se desde já a não repassá-la a terceiros.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>5.4.1.</strong> &nbsp;Quaisquer danos decorrentes do uso indevido, por parte
                                                                    de terceiros, das senhas dos <strong>USUÁRIOS</strong> cadastrados no sistema é
                                                                    de inteira responsabilidade do <strong>USUÁRIO</strong>.</div>
                                                                <div class="paragrafo">
                                                                    <strong>5.5.</strong> &nbsp;O <strong>USUÁRIO</strong> assume total responsabilidade
                                                                    pelas informações pessoais fornecidas ao sistema.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>5.6.</strong> &nbsp;Na contratação de mais de 01 (um) <strong>USUÁRIO</strong>,
                                                                    fica sob responsabilidade do <strong>USUÁRIO ADMINISTRADOR</strong> a criação do
                                                                    (s) outro(s) <strong>USUÁRIO(S)</strong> e caberá à ele dar ciência da totalidade
                                                                    do conteúdo desta avença a todos os demais USUÁRIOS, informando-lhes, notadamente
                                                                    suas respectivas obrigações.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>5.7.</strong> &nbsp;É defeso ao <strong>USUÁRIO</strong> suprimir ou remover
                                                                    quaisquer avisos legais e/ou do sistema exibidos junto ao serviço do sistema <strong>
                                                                        SUSTENTAR</strong>.
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA SEXTA - DAS LIMITAÇÕES DOS SERVIÇOS</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>6.</strong> &nbsp;O uso do serviço não confere ao <strong>USUÁRIO</strong>
                                                                    a propriedade intelectual do mesmo, não fornecendo o direito a utilização de quaisquer
                                                                    marcas ou logotipos utilizados pelas <strong>CONTRATADAS</strong>.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>6.1.</strong> &nbsp;As <strong>CONTRATADAS</strong> não se responsabilizam
                                                                    pelas informações inseridas pelos <strong>USUÁRIOS</strong>, sendo de inteira responsabilidade
                                                                    destes a inserção correta dos dados que alimentam o sistema <strong>SUSTENTAR</strong>.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>6.2.</strong> &nbsp;O <strong>USUÁRIO</strong> reconhece que as <strong>CONTRATADAS</strong>
                                                                    não serão responsáveis por prejuízos ou danos de qualquer natureza, que sejam resultantes
                                                                    da utilização do Sistema <strong>SUSTENTAR</strong>, se estes foram advindos de
                                                                    informações equivocadas ou inverídicas inseridas pelo <strong>USUÁRIO</strong> ou
                                                                    de questões de impossibilidade técnica que inviabilizem a prestação do serviço.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>6.3.</strong> &nbsp;As <strong>CONTRATADAS</strong> apenas viabilizam tecnicamente
                                                                    este serviço, não sendo responsáveis por prejuízos ocasionados em virtude do acesso
                                                                    não autorizado ao sistema.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>6.4.</strong> &nbsp;Falhas técnicas ou defeitos em outros programas de computador,
                                                                    equipamentos, nas instalações elétricas e de rede, assim como as consequências oriundas
                                                                    destes.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>6.5.</strong> &nbsp;As <strong>CONTRATADAS</strong> se eximem integralmente
                                                                    da responsabilidade pelos danos de qualquer natureza que, venham ocorrer nos equipamentos
                                                                    do USUÁRIO decorrentes de mau uso de softwares, hardware ou conexões inapropriadas.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>6.6.</strong> &nbsp;Fica expressamente proibido ao <strong>USUÁRIO</strong>
                                                                    ceder a terceiros seus direitos decorrentes do presente contrato, sem a prévia autorização
                                                                    das <strong>CONTRATADAS</strong> por escrito.
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA SÉTIMA - DA VIGÊNCIA</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>7.</strong> &nbsp;O prazo de vigência do presente contrato é indeterminado,
                                                                    iniciando-se automaticamente na data de sua assinatura.
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA OITAVA - DA RESCISÃO</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>8.</strong> &nbsp;O presente contrato poderá ser rescindido por qualquer
                                                                    das partes contratantes, a qualquer momento, independentemente de multas, indenizações
                                                                    ou qualquer cláusula penal, desde que se faça mediante prévia comunicação por escrito
                                                                    a outra parte, com prazo mínimo de <strong>30 (trinta) dias de antecedência</strong>,
                                                                    exceto na ocorrência de caso fortuito ou força maior, nos termos do artigo 393 do
                                                                    Código Civil Brasileiro.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>8.1.</strong> &nbsp;Assiste às <strong>CONTRATADAS</strong> a possibilidade
                                                                    de rescindir unilateralmente esta avença, caso o <strong>USUÁRIO</strong> viole
                                                                    alguma das disposições ora descritas.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>8.1.1.</strong> &nbsp;Em caso de cancelamento do contrato ora firmado, as
                                                                    <strong>CONTRATADAS</strong> se comprometem em manter os dados do <strong>USUÁRIO</strong>
                                                                    por até 06 (seis) meses, contados a partir do efetivo cancelamento.
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA NONA - DISPOSIÇÕES GERAIS</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>9.</strong> &nbsp;As <strong>CONTRATADAS</strong> podem, sem aviso preliminar,
                                                                    cancelar, suspender por tempo indeterminado ou negar ao <strong>USUÁRIO</strong>
                                                                    que, não esteja em pleno cumprimento de suas obrigações contratuais e/ou que de
                                                                    qualquer forma ameaçar a estabilidade deste contrato, o direito de utilizar o serviço
                                                                    do sistema <strong>SUSTENTAR</strong>. Neste caso, ficam isentas as <strong>CONTRATADAS</strong>
                                                                    da obrigação de prestar manutenção das informações do <strong>USUÁRIO</strong> no
                                                                    sistema.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>9.1.</strong> &nbsp;Considerando a independência deste serviço e o fato
                                                                    de que as <strong>CONTRATADAS</strong> apenas supervisioná-lo, sem acesso aos dados
                                                                    dos <strong>USUÁRIOS</strong>, o <strong>USUÁRIO</strong> declara reconhecer que
                                                                    as <strong>CONTRATADAS</strong> não são responsáveis por eventuais informações e/ou
                                                                    erros oriundos de órgãos ambientais componentes do SISNAMA – Sistema Nacional de
                                                                    Meio Ambiente e/ou ANM - Agência Nacional de Mineração.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>9.2.</strong> &nbsp;As informações depositadas no sistema <strong>SUSTENTAR</strong>,
                                                                    bem como sua página inicial e demais páginas de serviço ou frações destas não podem
                                                                    ser objeto de exibição pública ou em processos judiciais sem autorização expressa
                                                                    das <strong>CONTRATADAS</strong>.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>9.3.</strong> &nbsp;O <strong>USUÁRIO</strong> está ciente de que será inteiramente
                                                                    responsável por todas as atividades consideradas ilícitas, danosas ou não apropriadas,
                                                                    tais como: insulto ou agressão a outro <strong>USUÁRIO</strong>; transmissão de
                                                                    qualquer material considerado, ilegal, abusivo, caluniador, vulgar ou racista; transmissão
                                                                    de qualquer informação publicitária ou promocional que não tenha sido requerida
                                                                    ou previamente autorizada.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>9.4.</strong> &nbsp;O <strong>USUÁRIO</strong> declara que: possui plena
                                                                    capacidade jurídica para celebrar o presente contrato e fazer uso destes serviços
                                                                    e produtos; ser pessoa responsável e capaz de arcar financeiramente com todas as
                                                                    despesas e custos decorrentes deste instrumento; reconhece que o presente contrato,
                                                                    torna-se válido e, desta forma, sujeito aos termos e condições anteriormente descritos,
                                                                    após o USUÁRIO clicar no espaço intitulado "CONCORDO", presente na tela do sistema.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>9.5.</strong> &nbsp;Os serviços oferecidos pelo sistema <strong>SUSTENTAR</strong>,
                                                                    bem como suas marcas, funcionalidades, conteúdo e direitos são de propriedade intelectual
                                                                    e posse das <strong>CONTRATADAS</strong>.
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA DÉCIMA - DO FORO</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>10.</strong> &nbsp;As condições gerais descritas se regem pela Lei brasileira
                                                                    e quaisquer controvérsias relativas a elas, serão dirimidas no Foro da Comarca de
                                                                    Cachoeiro de Itapemirim/ES, à exclusão de qualquer outro, sendo este eleito pelas
                                                                    partes deste contrato, para solucionar quaisquer controvérsias provindas do presente
                                                                    contrato, que venham a ocorrer a qualquer tempo.
                                                                </div>
                                                                <div class="paragrafo">
                                                                    <strong>10.1.</strong> &nbsp;O <strong>USUÁRIO</strong> declara estar de acordo
                                                                    com todos os termos e condições descritos acima e assume a responsabilidade de cumprir
                                                                    todas as cláusulas presentes neste contrato.
                                                                </div>
                                                                <br />
                                                                <br />
                                                                <div class="paragrafo titulo">
                                                                    Cachoeiro de Itapemirim/ES.
                                                                    <asp:Label ID="lblCOntratoOriginalData" runat="server" Text="Label"></asp:Label></div>
                                                            </div>
                                                        </div>
                                                        </div>
                                                        <div style="text-align: center; margin-top: 10px; margin-bottom: 10px">
                                                            <a href="#" onclick="printDiv('imprimir_contrato_original','janela')">
                                                                <img style="border: 0px; margin-top: 5px;" alt="imprimir" src="../imagens/ico_imprimir.gif" /><br />
                                                                Imprimir Contrato</a></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="View8" runat="server">
                                            <table cellpadding="0" cellspacing="5" style="font-family: Arial, Helvetica, sans-serif;
                                                font-size: 12px; margin-left: 20px" width="660px">
                                                <tr>
                                                    <td align="center" style="margin-bottom: 10px">
                                                        <div id="texto_aditivo" align="left" style="padding: 5px; background-color: #FFFFFF;
                                                            padding: 10px; height: 372px; overflow: auto;">
                                                            <div id="imprimir_aditivo">
                                                                <style type="text/css">
                                                                    .paragrafo
                                                                    {
                                                                        text-indent: 10mm;
                                                                        font-family: Arial;
                                                                        font-size: 12px;
                                                                        margin: 4px 15px 0 55px;
                                                                    }
                                                                    
                                                                    .titulo
                                                                    {
                                                                        text-align: center;
                                                                        margin: 12px auto;
                                                                        background-color: Silver;
                                                                        font-weight: bold;
                                                                    }
                                                                    .clausula
                                                                    {
                                                                        margin-top: 15px;
                                                                    }
                                                                    
                                                                    .logomarca
                                                                    {
                                                                        text-align: center;
                                                                        width: 100%;
                                                                        height: 105px;
                                                                        margin-bottom: 15px;
                                                                    }
                                                                </style>
                                                                <div class="logomarca">
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="../imagens/logo_contrato.png" />
                                                                </div>
                                                                <div class="paragrafo titulo" style="font-size: 11pt;">
                                                                    ADITIVO AO CONTRATO DE ASSINATURA DE PRESTAÇÃO DE SERVIÇO WEB
                                                                    <div>
                                                                        Nº
                                                                        <asp:Label ID="lblContOriginalDoAditivoVisualizacao" runat="server"></asp:Label></div>
                                                                </div>
                                                                <div class="paragrafo">
                                                                    O presente aditivo, regido pelas condições e cláusulas descritas abaixo, constitui
                                                                    total entendimento entre
                                                                    <asp:Label ID="lblContratanteDoAditivoVisualizacao" runat="server"></asp:Label>
                                                                    &nbsp; doravante denominado <strong>USUÁRIO</strong> e do outro lado, <strong>AMBIENTALIS
                                                                        ASSESSORIA E SERVIÇOS LTDA</strong>, localizada à Rod. Fued Nemer, s/n – km
                                                                    02, Santa Bárbara, Castelo/ES - CEP.: 29360-000, devidamente registrada no CNPJ/MF
                                                                    sob nº 11.259.526/0001-07, Inscrição Estadual n° [ISENTA] e; <strong>LOGUS SISTEMAS
                                                                        LTDA - EPP</strong>, localizada à Rua Hyercem Machado, nº 26, Bairro Gilberto
                                                                    Machado, Cachoeiro de Itapemirim/ES - CEP.:29.303-312, devidamente registrado no
                                                                    CNPJ/MF sob nº 36.420.818/0001-00, Inscrição Estadual nº [ISENTA], doravante denominadas
                                                                    <strong>CONTRATADAS</strong>.</div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA PRIMEIRA - DO OBJETO</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>1.4.</strong> &nbsp;O presente aditivo ao <strong>CONTRATO DE ASSINATURA DE PRESTAÇÃO
                                                                        DE SERVIÇO WEB
                                                                        <asp:Label ID="lblContOriginalDoAditivo2Visualizacao" runat="server"></asp:Label></strong>,
                                                                    firmado entre as partes acima qualificadas na data de
                                                                    <asp:Label ID="lblContOriginalAditivoDataVisualizacao" runat="server"></asp:Label>,
                                                                    vem alterar o plano de assinatura conforme descrito abaixo:
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="center" colspan="2" style="background-color: Silver; font-size: 11pt;">
                                                                                <strong>PLANO DE ASSINATURA</strong>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" width="5%">
                                                                                01
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblAditivoEmpresasVisualizacao" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                02
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblAditivoUsuariosVisualizacao" runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <table width="100%" style="background-color: Silver;">
                                                                                    <tr>
                                                                                        <td align="center" width="50%">
                                                                                            <strong>VALOR MENSAL</strong>
                                                                                        </td>
                                                                                        <td align="center">
                                                                                            <strong>
                                                                                                <asp:Label ID="lblAditivoTotalVisualizacao" runat="server"></asp:Label></strong>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>CLÁUSULA NONA - DISPOSIÇÕES GERAIS</strong><br />
                                                                </div>
                                                                <br />
                                                                <div class="paragrafo">
                                                                    <strong>9.6.</strong> &nbsp;Nenhuma outra cláusula no contrato original, devidamente
                                                                    registrado no cartório geral de imóveis, títulos e documentos de pessoas jurídicas
                                                                    da comarca de Castelo, estado do Espírito Santo, sob nº. 5889, Livro B – 5, protocolado
                                                                    sob nº. 2.365, na data de 31 de julho de 2012, cujo teor o cliente declara conhecer
                                                                    e concordar na sua totalidade, sofre qualquer alteração por conta deste aditivo.
                                                                </div>
                                                                <br />
                                                                <br />
                                                                <div class="paragrafo titulo">
                                                                    Cachoeiro de Itapemirim - ES
                                                                    <asp:Label ID="lblAditivoDataVisualizacao" runat="server" Text="Label"></asp:Label></div>
                                                            </div>
                                                        </div>
                                                        <div style="text-align: center; margin-top: 10px; margin-bottom: 10px">
                                                            <a href="#" onclick="printDiv('imprimir_aditivo','janela')">
                                                                <img style="border: 0px; margin-top: 5px;" alt="imprimir" src="../imagens/ico_imprimir.gif" /><br />
                                                                Imprimir Contrato</a></div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                    </asp:MultiView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlContrato" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
