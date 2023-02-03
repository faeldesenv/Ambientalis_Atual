<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true"
    CodeFile="ManterProspecto.aspx.cs" Inherits="Prospecto_ManterProspecto" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Funcoes.js" type="text/javascript"></script>
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () { CriarEventos(); });

        function CriarEventos() {
            $('#<%=rbtnPessoaFisica.ClientID %>').change(function () { rbtnTipoPessoaClick(this) });
            $('#<%=rbtnPessoaJuridica.ClientID %>').change(function () { rbtnTipoPessoaClick(this) });            

            ///Máscaras
            //$('#<%=tbxCNPJ.ClientID %>').keypress(function () { MascaraCNPJ(this); });
            $("#<%=tbxCNPJ.ClientID %>").mask("99.999.999/9999-99");
            //$('#<%=tbxCPF.ClientID %>').keypress(function () { MascaraCPF(this); });
            $('#<%=tbxCPF.ClientID %>').mask("999.999.999-99"); 
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
        cadastro de Indicação de Cliente</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <strong>
            <asp:Label ID="btnPopUpinteracao" runat="server" Text="" />
            <asp:ModalPopupExtender ID="btnPopUpinteracao_popupextender" runat="server" BackgroundCssClass="simplemodal"
                CancelControlID="fecharInteracao" DynamicServicePath="" Enabled="True" PopupControlID="divPopUpInteracao"
                TargetControlID="btnPopUpinteracao">
            </asp:ModalPopupExtender>
        </strong>
        <asp:TabContainer ID="TabContainer4" runat="server" ActiveTabIndex="0" Height="750px"
            ScrollBars="Auto" Width="100%" AutoPostBack="True" 
            onactivetabchanged="TabContainer4_ActiveTabChanged">
            <asp:TabPanel ID="TabPanel10" runat="server" HeaderText="Validade">
                <HeaderTemplate>
                    DADOS DA INDICAÇÃO DO CLIENTE
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="width: 100%; height: 10px;">
                        <table cellpadding="0" cellspacing="5" width="100%">
                            <tr>
                                <td align="right" width="40%">
                                    Revenda</td>
                                <td>
                                    <asp:DropDownList ID="ddlRevenda" runat="server" CssClass="DropDownList" 
                                        DataTextField="Nome" DataValueField="Id" Width="313px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="40%">
                                    &nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="chkAtivo" runat="server" Checked="True" 
                                        onmouseout="tooltip.hide();" 
                                        onmouseover="tooltip.show('Somente poderão ser acessados, editados e excluídos, dados de Indicações de clientes ativos')" 
                                        Text="Ativo" />
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
                                              <td align="right" width="40%">Nome*:</td>
                                              <td align="left">
                                                <asp:TextBox ID="tbxNome" runat="server" CssClass="TextBox" ValidationGroup="rfvSalvar" Width="305px"></asp:TextBox>
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
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <div id="tabelaPessoaJuridica">
                                        <table width="100%">
                                            <tr>
                                              <td align="right" width="40%">Nome Fantasia*:</td>
                                              <td align="left">
                                                <asp:TextBox ID="tbxNomeFantasia" runat="server" CssClass="TextBox" ValidationGroup="rfvSalvar" Width="305px"></asp:TextBox>
                                              </td>
                                            </tr>
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
                                    Responsável:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbxResponsavel" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
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
                                        OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" Width="313px" 
                                        DataTextField="Nome" DataValueField="Id">
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
                                            <asp:DropDownList ID="ddlCidade" runat="server" CssClass="DropDownList" 
                                                Width="313px" DataTextField="Nome" DataValueField="Id">
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
                                    Data de Cadastro:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDataCadastro" runat="server" CssClass="TextBox" 
                                        Width="123px" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="40%">
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnNovo" runat="server" CssClass="Button" OnClick="btnNovo_Click"
                                                Text="Novo" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnSalvar" runat="server" CssClass="Button" OnClick="btnSalvar_Click"
                                                Text="Salvar" ValidationGroup="rfvSalvar" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnExcluir" runat="server" CssClass="ButtonExcluir" OnClick="btnExcluir_Click"
                                                Text="Excluir" />
                                            <asp:HiddenField ID="hfId" runat="server" />
                                            <asp:HiddenField ID="hfCnpjCpf" runat="server" Visible="False" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel11" runat="server" HeaderText="Comunicar Início da Pesquisa">
                <HeaderTemplate>
                    REGISTRO DE INTERAÇÕES
                </HeaderTemplate>
                <ContentTemplate>
                    <table cellpadding="2" style="width: 100%;">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UPTituloProspecto" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <strong>
                                        <asp:Label ID="lblNomeProspecto" runat="server"></asp:Label>
                                        </strong>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="TabContainer4" 
                                            EventName="ActiveTabChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>
                                    <asp:ImageButton ID="ibtnAddInteracao" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                                        OnClick="ibtnAddInteracao_Click" OnInit="ibtnAddInteracao_Init" 
                                    style="width: 20px" />
                                    &nbsp;INTERAÇÃO:</strong>
                                <asp:UpdatePanel ID="upInteracao" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DataGrid ID="dgrinteracoes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            DataKeyField="Id" ForeColor="#333333" GridLines="None" Width="100%" OnEditCommand="dgrinteracoes_EditCommand"
                                            OnInit="dgrinteracoes_Init" OnDeleteCommand="dgrinteracoes_DeleteCommand">
                                            <AlternatingItemStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Data" DataFormatString="{0:d}" HeaderText="Data"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Tipo" HeaderText="Tipo"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Pessoa (Cargo)">
                                                  <ItemTemplate>
                                                    <%# bindingPessoaCargo(Container.DataItem) %>
                                                  </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="Descricao" HeaderText="Descrição"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Status" HeaderText="Status"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Edit.">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgAbrir0" runat="server" AlternateText="." CommandName="Edit"
                                                            ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre a empresa para edição dos dados')"
                                                            ToolTip="Abrir" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="22px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="ibtnExcluir0" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                                            onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui a(s) empresa(s) selecionada(s)')"
                                                            OnPreRender="ibtnExcluir_PreRender" ToolTip="Excluir" />
                                                        <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarClientes(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="45px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <EditItemStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle CssClass="GridHeader" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                VerticalAlign="Top" />
                                            <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" Font-Size="Small" ForeColor="White"
                                                HorizontalAlign="Center" Mode="NumericPages" NextPageText="" />
                                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:DataGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="popups">

    <div id="divPopUpInteracao" style="width: 697px; display: block; top: 0px; left: 0px;"
        class="pop_up_super_super">
        <div>
            <div id="fecharInteracao" class="btn_cancelar_popup">
            </div>
            <div class="barra_titulo">
                Interação</div>
        </div>
        <div>
            <div>
                <asp:UpdatePanel ID="upInteracoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="right" style="width: 30%">
                                    Clientes:&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxProspectoInteracao" runat="server" CssClass="TextBox" 
                                        Width="300px" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Data:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxData" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="tbxData_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbxData">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Tipo:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="DropDownList" Width="170px">
                                        <asp:ListItem>Pessoal</asp:ListItem>
                                        <asp:ListItem>Telefone</asp:ListItem>
                                        <asp:ListItem>Email</asp:ListItem>
                                        <asp:ListItem>Chat</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Status:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownList" Width="170px">
                                        <asp:ListItem>Realizada</asp:ListItem>
                                        <asp:ListItem>Agendada</asp:ListItem>
                                        <asp:ListItem>Adiada</asp:ListItem>
                                        <asp:ListItem>Cancelada</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Nome da Pessoa:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxNomePessoa" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Cargo da Pessoa:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxCargoPessoa" runat="server" CssClass="TextBox" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Descrição:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxDescricao" runat="server" CssClass="TextBox" Height="77px" TextMode="MultiLine"
                                        Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:HiddenField ID="hfIdInteracao" runat="server" />
                                    <asp:Button ID="btnSalvarInteracao" runat="server" CssClass="Button" Text="Salvar"
                                        Width="170px" OnClick="btnSalvarInteracao_Click" OnInit="btnSalvarInteracao_Init" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarInteracao" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
