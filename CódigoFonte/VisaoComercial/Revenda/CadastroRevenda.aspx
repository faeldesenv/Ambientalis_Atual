<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true"
    CodeFile="CadastroRevenda.aspx.cs" Inherits="Revenda_Cadastro" %>

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

            ///Máscaras

            //$('#<%=tbxCNPJ.ClientID %>').keypress(function () { MascaraCNPJ(this); });
            $("#<%=tbxCNPJ.ClientID %>").mask("99.999.999/9999-99");
            //$('#<%=tbxCPF.ClientID %>').keypress(function () { MascaraCPF(this); });
            $('#<%=tbxCPF.ClientID %>').mask("999.999.999-99");
            $('#<%=tbxCEP.ClientID %>').mask("99.999-999");
            $('#<%=tbxRamal.ClientID%>').maskMoney({ thousands: '', decimal: '' });
            $('#<%=tbxCPFRepresentanteLegal.ClientID %>').mask("999.999.999-99");
            $('#<%=tbxComissaoContrato.ClientID%>').maskMoney({ thousands: '', decimal: '' });

            //IniciarCampos
            habilitarPessoaFisica('none');
            habilitarPessoaJuridica('block');

            if ($('#<%=rbtnPessoaFisica.ClientID %>').attr('checked')) {
                habilitarPessoaFisica('block');
                habilitarPessoaJuridica('none');
            }
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

        function chkIsento(chk) {
            if ($('#<%=chkIsentoICMS.ClientID %>').attr('checked'))
                $('#<%=tbxInscricaoEstadual.ClientID %>').attr('disabled', true);
            else
                $('#<%=tbxInscricaoEstadual.ClientID %>').attr('disabled', false);
        }

        function habilitarPessoaFisica(habilitar) {
            $('#tabelaPessoaFisica').css('display', habilitar);
        }

        function habilitarPessoaJuridica(habilitar) {
            $('#tabelaPessoaJuridica').css('display', habilitar);
        }

        function AddMascara() {
            $('#<%=tbxComissaoContrato.ClientID%>').maskMoney({ thousands: '', decimal: '' });            
//            $('#<%=tbxComissaoContrato.ClientID%>').bind('maskMoney', handler);
//            $('#<%=tbxComissaoContrato.ClientID%>').unbind('maskMoney', handler);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    Cadastro de Revenda
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="5">
            <tr>
                <td width="40%" align="right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Tipo de Parceria:</td>
                <td>
                    <asp:DropDownList ID="ddlTipoParceiro" runat="server" CssClass="DropDownList" Width="313px">
                                     <asp:ListItem Selected="True" Value="0">-- Selecione --</asp:ListItem>
                                     <asp:ListItem Value="1">Revenda / Agente de Negócios</asp:ListItem>
                                     <asp:ListItem Value="2">Consultoria Ambiental / Minerária</asp:ListItem>
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
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
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
                            <tr>
                <td align="right" width="40%">
                    Representante Legal da Empresa:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxRepresentanteEmpresa" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
             </tr>
             <tr>
                <td align="right" width="40%">
                    CPF do Representante Legal:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxCPFRepresentanteLegal" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
             </tr>
             <tr>
                <td align="right" width="40%">
                    Nacionalidade do Representante Legal:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxNacionalidadePresentanteLegal" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
             </tr>
              <tr>
                <td align="right" width="40%">
                    Site:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxSite" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:CheckBox ID="chkIsentoICMS" runat="server" Text="Isento de ICMS:" />
                </td>
             </tr>
             <tr>
                <td width="40%" align="right">
                    Inscrição Estadual:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxInscricaoEstadual" runat="server" Width="170px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
              </tr>
            </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Responsável Técnico:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxResponsavelTecnico" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Gestor Econômico:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxGestorEconomico" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%" valign="top">
                    E-mails (separar por ponto-e-vírgula):
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxEmails" runat="server" Width="305px" Height="40px" CssClass="TextBox"
                        TextMode="MultiLine" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" class="style1">
                    Telefone:
                </td>
                <td class="style1" valign="middle">
                    <asp:TextBox ID="tbxTelefone" runat="server" Width="123px" CssClass="TextBox">
                    </asp:TextBox>&nbsp;Celular:<asp:TextBox ID="tbxCelular" runat="server" Width="131px"
                        CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" class="style1" valign="middle">
                    Fax:
                </td>
                <td class="style1" valign="middle">
                    <asp:TextBox ID="tbxFax" runat="server" Width="123px" CssClass="TextBox">
                    </asp:TextBox>
                    &nbsp;Ramal:<asp:TextBox ID="tbxRamal" runat="server" Width="132px" CssClass="TextBox"></asp:TextBox>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Campo Obrigatório" ValidationGroup="rfvSalvar" ControlToValidate="tbxLogradouro"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" class="style1">
                    Número:
                </td>
                <td class="style1" valign="middle">
                    <asp:TextBox ID="tbxNumero" runat="server" Width="70px" CssClass="TextBox">
                    </asp:TextBox>
                    &nbsp;Complemento:<asp:TextBox ID="tbxComplemento" runat="server" Width="150px" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Bairro:
                </td>
                <td>
                    <asp:TextBox ID="tbxBairro" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="Campo Obrigatório" ValidationGroup="rfvSalvar" ControlToValidate="tbxBairro"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    CEP:
                </td>
                <td>
                    <asp:TextBox ID="tbxCEP" runat="server" Width="305px" CssClass="TextBox">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ErrorMessage="Campo Obrigatório" ValidationGroup="rfvSalvar" ControlToValidate="tbxCEP"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" class="style1">
                    Estado:
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlEstado" CssClass="DropDownList" Width="313px" 
                        runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlEstado_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Cidade:
                </td>
                <td>
                    <asp:UpdatePanel ID="UPCidade" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCidade" runat="server" CssClass="DropDownList" 
                                Width="313px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEstado" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" valign="top">
                    Detalhamento:
                </td>
                <td>
                    <asp:TextBox ID="tbxObservacoes" runat="server"  Width="305px" Height="60px" CssClass="TextBox" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" valign="top">
               
                    <asp:UpdatePanel ID="UPID" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hfId" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
               
                </td>
                <td>
                    <asp:Button ID="btnNovo" runat="server" Text="Novo" CssClass="Button" 
                        onclick="btnNovo_Click" />&nbsp;&nbsp;
                        <asp:Button CssClass="Button" ID="btnSalvar" runat="server" Text="Salvar" 
                        onclick="btnSalvar_Click" ValidationGroup="rfvSalvar" />&nbsp;&nbsp;
                        
                        <asp:Button ID="btnExcluir" CssClass="ButtonExcluir" runat="server" 
                        Text="Excluir" onclick="btnExcluir_Click" oninit="btnExcluir_Init" />
                    <asp:HiddenField ID="hfCnpjCpf" runat="server" />
                    <asp:Label ID="lblPopupContratoComissao" runat="server"></asp:Label>
                    <asp:ModalPopupExtender ID="lblPopupContratoComissao_popupextender" 
                        runat="server" BackgroundCssClass="simplemodal" 
                        CancelControlID="fecharContratoComissao" 
                        PopupControlID="divPopUpContratoComissao" 
                        TargetControlID="lblPopupContratoComissao">
                    </asp:ModalPopupExtender>
                    <asp:Label ID="lblPopupEnviarContrato" runat="server"></asp:Label>
                    <asp:ModalPopupExtender ID="lblPopupEnviarContrato_popupextender" 
                        runat="server" BackgroundCssClass="simplemodal" 
                        CancelControlID="fecharContrato" PopupControlID="divPopUpContrato" 
                        TargetControlID="lblPopupEnviarContrato">
                    </asp:ModalPopupExtender>
                </td>
            </tr>
        </table>
        <div class="barra_titulo" style="margin-top:10px;">Formalização da Parceria</div>
        <div>
            <asp:UpdatePanel ID="UPFormalizacaoParceria" runat="server" 
                UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="5">
                        <tr>
                            <td align="right" width="20%">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" width="20%">
                                Situação da Parceria:
                            </td>
                            <td>
                                <asp:Label ID="lblSituaçãoRevenda" runat="server" style="font-weight: 700" 
                                    Text="INATIVA"></asp:Label>
                            </td>
                        </tr>
            <tr>
                <td width="20%" align="right">
                    Contrato:</td>
                <td>
                    <table width="100%">
                        <tr>
                            <td width="10%">
                                <asp:Label ID="lblContrato" runat="server" Text="NÂO CRIADO" style="font-weight:700"></asp:Label>
                            </td>
                            <td align="left">
                              <div style="width:100%">
                                  <asp:UpdatePanel ID="UPContratos" runat="server" UpdateMode="Conditional">
                                      <ContentTemplate>
                                          <div id="contrato_naocriado" style="vertical-align:top; width:20%; float:left;" runat="server" visible="true">
                                            <asp:Button ID="btnCriarContrato" CssClass="Button" runat="server" 
                                                  Text="Criar Contrato" onclick="btnCriarContrato_Click" 
                                                  oninit="btnCriarContrato_Init" />
                                            </div>
                                          <div id="visualizar_contrato_criado" style="vertical-align:top; width:105px; float:left; margin:0;" runat="server" visible="false">
                                            <asp:Button ID="btnVisualizarContrato" CssClass="Button" runat="server"  Width="100px"
                                                  Text="Visualizar" onclick="btnVisualizarContrato_Click" 
                                                  oninit="btnVisualizarContrato_Init" />
                                                  </div>
                                                  <div id="criar_novo_contrato" runat="server" style="vertical-align:top; width:12%; float:left; margin:0; text-align:left;" runat="server" visible="false">
                                            <asp:Button ID="btnCriarNovoContrato" CssClass="Button" runat="server" 
                                                  Text="Criar Novo" onclick="btnCriarNovoContrato_Click" 
                                                          oninit="btnCriarNovoContrato_Init" />
                                                  </div>
                                            
                                      </ContentTemplate>
                                      <Triggers>
                                          <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                                      </Triggers>
                                  </asp:UpdatePanel>                                
                              </div> 
                            </td>
                        </tr>                        
                    </table>
                    </td>
            </tr>
                        <tr>
                            <td align="right" width="20%">
                                &nbsp;</td>
                            <td>
                                <asp:CheckBox ID="chkContratoAtivo" runat="server" 
                                    Text="Contrato Ativo (Revenda aceitou o contrado)" />
                            </td>
                        </tr>
            </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

         
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
<div id="divPopUpContratoComissao" style="width: 250px; display: block; top: 0px; left: 0px; background-color:#e2e3e3;" class="pop_up">
    <div id="fecharContratoComissao" class="btn_cancelar_popup"></div>
    <div class="barra_titulo" style="margin-top:10px;">Contrato</div>
    <div>
    <asp:UpdatePanel ID="UPComissao" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        Comissão (%):
            <asp:TextBox ID="tbxComissaoContrato" runat="server" CssClass="TextBox" 
                Width="150px"></asp:TextBox>
        </ContentTemplate>
        </asp:UpdatePanel>    
</div>
        <div style="text-align:right; margin-top:5px;"> 
            <asp:Button ID="btnSalvarContrato" 
                runat="server" CssClass="Button" Text="Criar" onclick="btnSalvarContrato_Click" 
                oninit="btnSalvarContrato_Init" /></div>
</div>

<div id="divPopUpContrato" style="width: 700px; display: block; top: 0px; left: 0px; background-color:#e2e3e3;" class="pop_up">
<div id="fecharContrato" class="btn_cancelar_popup"></div>       
  <div class="barra_titulo" style="margin-top:10px;">Contrato</div>
    <div>
        <asp:UpdatePanel ID="UPTextoContrato" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <div id='contrato_original' align='left' style='padding: 5px; background-color: #FFFFFF; padding: 10px; height: 372px; overflow: auto;'>
                <style type='text/css'>
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
                                                                <div class='logomarca'><asp:Image ID="imgLogoTopo" runat="server" ImageUrl="~/imagens/logo_contrato.png" /></div>              
                 <asp:Label ID="lblTextoContrato" runat="server"></asp:Label>
                 </div>                
                    <div id="enviar_contrato" style="text-align:right; margin-top:10px;" runat="server">
                        <asp:Button ID="btnEnviarContrato" runat="server" CssClass="Button" 
                            Text="Enviar Contrato" onclick="btnEnviarContrato_Click" 
                            oninit="btnEnviarContrato_Init" /></div>
                        </ContentTemplate>
                 </asp:UpdatePanel>
                </div> 
</div>
</asp:Content>
