<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="CadastroClientes.aspx.cs" Inherits="Clientes_CadastroClientes" %>

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
            //$('#<%=tbxCEP.ClientID %>').keypress(function () { MascaraCep(this); });
            $('#<%=tbxCEP.ClientID %>').mask("99.999-999");
            $('#<%=tbxRamal.ClientID%>').maskMoney({ thousands: '', decimal: '' });            

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        Cadastro de Cliente</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%; height: auto;">
        <table cellpadding="0" cellspacing="5" width="100%">
            <tr>
                <td align="right" width="40%">
                    Nome*:
                </td>
                <td>
                    <asp:TextBox ID="tbxNome" runat="server" CssClass="TextBox" ValidationGroup="rfvSalvar"
                        Width="305px"></asp:TextBox>&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNome"
                        ErrorMessage="Campo Obrigatório" ValidationGroup="rfvSalvar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    &nbsp;</td>
                <td>
                    <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" Checked="True" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Somente poderão ser acessados, editados e excluídos, dados de clientes ativos')"/>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Atividade:
                </td>
                <td>
                        <asp:UpdatePanel ID="UPAtividade" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlAtividade" runat="server" Width="313px" 
                                    CssClass="DropDownList" DataTextField="Nome" DataValueField="Id">
                                </asp:DropDownList>
                                <asp:ImageButton ID="btnAdicionarAtividade" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                    OnClick="btnAdicionarAtividade_Click" Style="width: 20px" ToolTip="Novo" />&nbsp;
                                <asp:ImageButton ID="btnEditarAtividade" runat="server" ImageUrl="~/imagens/icone_editar.png"
                                    OnClick="btnEditarCentroCusto_Click" Style="width: 20px" 
                                    ToolTip="Editar" />
                                &nbsp;
                                <asp:ImageButton ID="ibtnExcluirAtividade" runat="server" ImageUrl="~/imagens/excluir.gif"
                                    OnClick="ibtnExcluirCentroCusto_Click" OnPreRender="ibtnExcluirAtividade_PreRender"
                                    Width="20px" ToolTip="Excluír" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
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
                                <td align="right" width="40%">
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
                                <td id="Td1" align="right" width="40%">
                                    CPF:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxCPF" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td id="Td2" align="right" width="40%">
                                    Data de Nascimento:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxDataNascimento" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataNascimento">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
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
                                <td id="Td3" align="right" width="40%">
                                    RG:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxRG" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
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
                                <td align="right" width="40%">
                                    CNPJ:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxCNPJ" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    Razão Social:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxRazaoSocial" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    &nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="chkIsentoICMS" runat="server" Text="Isento de ICMS:" />
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Inscrição Estadual:
                </td>
                <td>
                    <asp:TextBox ID="tbxInscricaoEstadual" runat="server" CssClass="TextBox" Width="170px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Responsável Técnico:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxResponsavelTecnico" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Site:
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxSite" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" width="40%">
                    E-mails (separar por ponto-e-vírgula):
                </td>
                <td align="left">
                    <asp:TextBox ID="tbxEmails" runat="server" CssClass="TextBox" Height="40px" TextMode="MultiLine"
                        Width="305px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1" width="40%">
                    Telefone:
                </td>
                <td class="style1" valign="middle">
                    <asp:TextBox ID="tbxTelefone" runat="server" CssClass="TextBox" Width="123px"></asp:TextBox>
                    &nbsp;Celular:<asp:TextBox ID="tbxCelular" runat="server" CssClass="TextBox" Width="131px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1" valign="middle" width="40%">
                    Fax:
                </td>
                <td class="style1" valign="middle">
                    <asp:TextBox ID="tbxFax" runat="server" CssClass="TextBox" Width="123px"></asp:TextBox>
                    &nbsp;Ramal:<asp:TextBox ID="tbxRamal" runat="server" CssClass="TextBox" Width="132px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    <strong>Endereço</strong>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Logradouro:
                </td>
                <td>
                    <asp:TextBox ID="tbxLogradouro" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1" width="40%">
                    Número:
                </td>
                <td class="style1" valign="middle">
                    <asp:TextBox ID="tbxNumero" runat="server" CssClass="TextBox" Width="70px"></asp:TextBox>
                    &nbsp;Complemento:<asp:TextBox ID="tbxComplemento" runat="server" CssClass="TextBox"
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Bairro:
                </td>
                <td>
                    <asp:TextBox ID="tbxBairro" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    CEP:
                </td>
                <td>
                    <asp:TextBox ID="tbxCEP" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" CssClass="DropDownList"
                        OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" Width="313px" DataTextField="Nome"
                        DataValueField="Id">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" width="40%">
                    Cidade:
                </td>
                <td>
                    <asp:UpdatePanel ID="UPCidade" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCidade" runat="server" CssClass="DropDownList" Width="313px"
                                DataTextField="Nome" DataValueField="Id">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" width="40%">
                    Detalhamento:
                </td>
                <td>
                    <asp:TextBox ID="tbxObservacoes" runat="server" CssClass="TextBox" Height="60px"
                        TextMode="MultiLine" Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" width="40%">
                    <asp:Label ID="lblPopAtividade" runat="server"></asp:Label>
                    <asp:ModalPopupExtender ID="ModalPopAtividade" runat="server" BackgroundCssClass="simplemodal"
                        CancelControlID="fechar_cadastro_status" PopupControlID="cadastro_status_diverso"
                        TargetControlID="lblPopAtividade">
                    </asp:ModalPopupExtender>
                </td>
                <td>
                    <asp:UpdatePanel ID="UPBotoes" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnNovo" runat="server" CssClass="Button" OnClick="btnNovo_Click"
                                Text="Novo" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnSalvar" runat="server" CssClass="Button" OnClick="btnSalvar_Click"
                                Text="Salvar" ValidationGroup="rfvSalvar" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnExcluir" runat="server" CssClass="ButtonExcluir" OnClick="btnExcluir_Click"
                                Text="Excluir" OnPreRender="btnExcluir_PreRender" />
                            <asp:HiddenField ID="hfId" runat="server" />
                            <asp:HiddenField ID="hfCnpjCpf" runat="server" Visible="False" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnNovo" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="cadastro_status_diverso" class="pop_up" style="width: 500px">
        <div id="fechar_cadastro_status" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Cadastro de Atividade</div>
        <div id="Div3">
            <asp:Panel ID="pnlPopCusto" runat="server" DefaultButton="btnSavarAtividade">
                <asp:UpdatePanel ID="UPTituloTipo" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%" cellspacing="5">
                            <tr>
                                <td align="right" width="20%">
                                    Nome*:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxAtividade" runat="server" Width="200px" TextMode="SingleLine"
                                        CssClass="TextBox"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Obrigatório!"
                                        ValidationGroup="rfvSalvarStatus" ControlToValidate="tbxAtividade" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                    <asp:Button ID="btnSavarAtividade" runat="server" CssClass="Button" OnClick="btnSavarAtividade_Click"
                                        Text="Salvar" ValidationGroup="rfvSalvarStatus" 
                                        oninit="btnSavarAtividade_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
