<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="ManterEmpresa.aspx.cs" Inherits="Site_Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
        .style2
        {
            height: 25px;
        }
        .style3
        {
            width: 100%;
        }
        
        img
        {
            border:0;
            }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <table width="100%" id="meuForm" cellpadding="0" cellspacing="5">
            <tr>
                <td width="40%" align="right">
                    Grupo Econômico:
                </td>
                <td>
                    <asp:DropDownList ID="ddlGrupoEconomico" runat="server" Width="305px" CssClass="DropDownList" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Seleciona dentro de qual grupo econômico será criada a empresa')">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" Checked="True" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Somente poderão ser acessados, editados e excluídos, dados de empresas ativas')"/>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Nome:
                </td>
                <td>
                    <asp:TextBox ID="tbxNome" runat="server" Width="250px" CssClass="TextBox"></asp:TextBox>
                    <asp:Button
                        ID="btnImportarDadosCadastrais" runat="server" Text="Importar Dados Cadastrais" 
                        CssClass="ButtonImportar" onclick="btnImportarDadosCadastrais_Click" 
                        Width="160px" onprerender="PremissaoUsuario_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Importa os dados básicos do grupo econômico selecionado para a empresa')"  />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Campo obrigatório!" ControlToValidate="tbxNome" 
                        ValidationGroup="rvg"></asp:RequiredFieldValidator>
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
                        <table class="style3">
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
                                <td width="40%" align="right" id="divDescCPF0">
                                    CPF:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxCPF" runat="server" Width="305px" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%" align="right" id="divDescDataNascimento0" class="style2">
                                    Data de Nascimento:
                                </td>
                                <td class="style2" align="left">
                                    <asp:TextBox ID="tbxDataNascimento" runat="server" Width="305px" CssClass="TextBox"></asp:TextBox>
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
                                <td width="40%" align="right" id="divDescRG0">
                                    RG:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxRG" runat="server" Width="305px" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="tabelaPessoaJuridica">
                        <table class="style3">
                            <tr>
                                <td width="40%" align="right">
                                    CNPJ:
                                </td>
                                <td class="cnpj" align="left">
                                    <asp:TextBox ID="tbxCNPJ" runat="server" Width="305px" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    Razão Social:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxRazaoSocial" runat="server" Width="305px" CssClass="TextBox"></asp:TextBox>
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
                    Responsável:
                </td>
                <td>
                    <asp:TextBox ID="tbxResponsavel" runat="server" Width="305px" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Representante Legal</td>
                <td>
                    <asp:TextBox ID="tbxRepresentanteLegal" runat="server" CssClass="TextBox" 
                        Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Gestor Econômico:</td>
                <td>
                    <asp:TextBox ID="tbxGestorEconomico" runat="server" CssClass="TextBox" 
                        Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Site:
                </td>
                <td>
                    <asp:TextBox ID="tbxSite" runat="server" Width="305px" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" valign="top">
                    E-mails (separar por ponto-e-virgula):
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <asp:TextBox ID="tbxEmail" runat="server" Width="305px" CssClass="TextBox" 
                            Height="120px" TextMode="MultiLine" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para adicionar mais de um email, separe-os por ponto e vírgula. Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex:<br/>(Paulo)paulo@sustentar.inf.br;<br/>pedro@sustentar.inf.br;<br/>(José)jose@sustentar.inf.br')"></asp:TextBox>
                    <asp:Button
                        ID="btnImportarEmail" runat="server" Text="Importar E-mails" 
                        CssClass="ButtonImportar" onclick="btnImportarEmail_Click" 
                            onprerender="PremissaoUsuario_PreRender"  onmouseout="tooltip.hide();" onmouseover="tooltip.show('Importa os emails do grupo econômico selecionado para a empresa')"/>
                            &nbsp;<asp:HyperLink ID="hplOrientacoesSpam" runat="server" ImageUrl="~/imagens/visualizar20x20.png" Target="_blank" NavigateUrl="http://sustentar.inf.br/Sistema/Repositorio/Orientacoes%20Antispam.pdf"></asp:HyperLink>
                    <asp:Label ID="lblVisualizarCont0" runat="server" Text="Orientações Anti-spam" 
                            Style="font-family: Arial; font-size: small; color: #666666;" onmouseout="tooltip.hide();" 
                        
                            onmouseover="tooltip.show('Orientações para evitar que os e-mails do sustentar cheguem como spam')"></asp:Label>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnImportarEmail" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" class="style1">
                    Telefone:
                </td>
                <td class="style1">
                    <asp:TextBox ID="tbxTelefone" runat="server" Width="123px" CssClass="TextBox"></asp:TextBox>&nbsp;Celular:<asp:TextBox
                        ID="tbxCelular" runat="server" Width="123px" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right" class="style1">
                    Fax:
                </td>
                <td class="style1">
                    <asp:TextBox ID="tbxFax" runat="server" Width="123px" CssClass="TextBox"></asp:TextBox>
                    &nbsp;Ramal:<asp:TextBox ID="tbxRamal" runat="server" Width="62px" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Nacionalidade:
                </td>
                <td>
                    <asp:TextBox ID="tbxNacionalidade" runat="server" Width="305px" CssClass="TextBox"></asp:TextBox>
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
                    <asp:Button
                        ID="btnImportarEndereco" runat="server" Text="Importar Endereço" 
                        CssClass="ButtonImportar" Width="120" 
                        onclick="btnImportarEndereco_Click" 
                        onprerender="PremissaoUsuario_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Importa os dados do endereço do grupo econômico selecionado para a empresa')"  />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">                    
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="right" width="40%">
                                        Logradouro:</td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxLogradouro" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Número:</td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxNumero" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Complemento:</td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxComplemento" runat="server" CssClass="TextBox" 
                                            Width="305px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Bairro:</td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxBairro" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        CEP:</td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxCEP" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Ponto de Referência:</td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxPontoReferencia" runat="server" CssClass="TextBox" 
                                            Width="305px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Estado:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" 
                                            CssClass="DropDownList" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" 
                                            Width="305px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Cidade:</td>
                                    <td align="left">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlCidade" runat="server" CssClass="DropDownList" 
                                                    Width="305px">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlEstado" 
                                                    EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnImportarEndereco" EventName="Click" />
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
                    &nbsp;</td>
                <td>
                    <asp:CheckBox ID="chkIsentoICMS" runat="server" Text="Isento de ICMS:" />
                </td>
            </tr>
            <tr>
                <td width="40%" align="right">
                    Inscrição Estadual:</td>
                <td>
                    <asp:TextBox ID="tbxInscricaoEstadual" runat="server" CssClass="TextBox" 
                        Width="170px"></asp:TextBox>
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
                    <asp:TextBox ID="tbxObservacoes" runat="server" CssClass="TextBox" 
                        TextMode="MultiLine" Width="305px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfIdGrupoEconomico" runat="server" />
                    <asp:HiddenField ID="hfCnpjCpf" runat="server" Visible="False" />
                </td>
                <td align="left">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="Button" Text="Salvar" 
                        OnClick="btnSalvar_Click" ValidationGroup="rvg" 
                        onprerender="PremissaoUsuario_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva as alterações numa empresa existente ou cria uma nova empresa')" />
                    <asp:Button ID="btnNovo" runat="server" CssClass="Button" Text="Novo" 
                        OnClick="btnNovo_Click" onprerender="PremissaoUsuario_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre os campos para cadastro de uma nova empresa')" />
                    <asp:Button ID="btnExcluir" runat="server" CssClass="ButtonExcluir" Text="Excluir"
                        OnClick="btnExcluir_Click" onprerender="PremissaoUsuario_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui a empresa carregada')" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Barra">
    <p>
        Cadastro de Empresa</p>
</asp:Content>
