<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true"
    CodeFile="ManterTipoLicencaAdm.aspx.cs" Inherits="Licenca_ManterTipoLicencaAdm" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/mask.js" type="text/javascript"></script>    
     <script type="text/javascript">
         $(document).ready(function () { CriarEventos(); });

         function CriarEventos() {
             $('#<%=tbxValidadePadrao.ClientID%>').maskMoney({ thousands: '', decimal: '' });
             $('#<%=tbxValidadePadraoCondicionante.ClientID%>').maskMoney({ thousands: '', decimal: '' });
         }

         function marcarDesmarcarLicencas(chk) {
             var checkar = $(chk).attr('checked') == "checked" ? true : false;
             for (var i = 0; i < document.getElementsByClassName('chkSelecionarLicenca').length; i++) {
                 document.getElementsByClassName('chkSelecionarLicenca')[i].children[0].checked = checkar;
             }
         }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    <p>
        cadastro de tipo de licença</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table cellpadding="0" cellspacing="5" width="100%">
            <tr>
                <td align="right">
                    Nome:
                </td>
                <td>
                    <asp:TextBox ID="tbxNome" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Sigla:
                </td>
                <td>
                    <asp:TextBox ID="tbxSigla" runat="server" CssClass="TextBox" Width="200px"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    Dias de Validade Padrão:
                </td>
                <td>
                    <asp:TextBox ID="tbxValidadePadrao" runat="server" CssClass="TextBox" Width="200px" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Os dias de validade padrão são sugeridos quando for criada uma nova licença deste tipo')"></asp:TextBox>
                    &nbsp;
                    <asp:button id="btnPopUp1" runat="server" text="Button" 
                style="visibility: hidden" />
                <asp:ModalPopupExtender ID="escolherCliente_ModalPopupExtender" 
                runat="server" CancelControlID="fecharEscolherCliente"
                    Drag="True" DynamicServicePath="" Enabled="True" PopupControlID="div_popupEscolherCliente"
                    TargetControlID="btnPopUp1" BackgroundCssClass="simplemodal">
                </asp:ModalPopupExtender>          
                </td>
            </tr>
            </table>
            <asp:UpdatePanel ID="upTabela" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
        <div>
            <strong>Condicionantes Padrões:
                    <asp:ImageButton ID="ibtnAdicionarCondicionante" ImageUrl="~/imagens/icone_adicionar.png"
                    runat="server" OnPreRender="ibtnAdicionarCondicionante_PreRender" 
                onclick="ibtnAdicionarCondicionante_Click" onmouseout="tooltip.hide();" 
                onmouseover="tooltip.show('A adição de condicionantes padrão permite que estas sejam importadas na criação de uma licença')" 
                oninit="ibtnAdicionarCondicionante_Init"/>
            </strong>
        </div>
        <div id="grid">
                    <asp:DataGrid ID="dgrCondicionante" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="4" DataKeyField="Id" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" Height="45px"
                        OnDeleteCommand="dgrCondicionante_DeleteCommand" OnEditCommand="dgrCondicionante_EditCommand"
                        OnItemDataBound="dgrCondicionante_ItemDataBound" OnPageIndexChanged="dgr_PageIndexChanged"
                        PageSize="8" Width="100%" OnInit="dgrCondicionante_Init">
                       <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" 
                        HorizontalAlign="Center" Mode="NumericPages" NextPageText="" 
                            CssClass="GridPager" />
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <ItemStyle BackColor="#F7F6F3" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Descricao" HeaderText="Descrição"></asp:BoundColumn>
                            <asp:BoundColumn DataField="diasValidade" HeaderText="Prazo padrão">
                                <HeaderStyle Width="200px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Periódica">
                             <ItemTemplate>
                               <%# bindPeriodica(Container.DataItem) %>
                             </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                    Width="170px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Edit.">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAbrir" runat="server" AlternateText="." CommandName="Edit"
                                        ImageUrl="../imagens/icone_editar.png" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre a condicioante padrão para edição')" />
                                </ItemTemplate>
                                <HeaderStyle Width="22px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Del.">
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnExcluir" runat="server" CommandName="Delete" ImageUrl="~/imagens/excluir.gif"
                                        OnPreRender="ibtnExcluir_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui as condicionantes padrão adicionadas ao tipo de licença')" />
                                    <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarLicencas(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarLicenca" />
                                </ItemTemplate>
                                <HeaderStyle Width="45px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" 
                        HorizontalAlign="Left" VerticalAlign="Top" CssClass="GridHeader" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <asp:HiddenField ID="hfId" runat="server" />
        </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        <div>
            <table cellpadding="0" cellspacing="5" width='100%'>
                <tr>
                    <td width="40%">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="Button" 
                            onclick="Button2_Click" onmouseout="tooltip.hide();" 
                            onmouseover="tooltip.show('Salva as alterações em um tipo de licença existente ou cria um novo tipo de licença')"/>
                        <asp:Button ID="btnNovo" runat="server" CssClass="Button" Text="Novo" 
                            OnClick="btnNovo_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre os campos para cadastro de um novo tipo de licença')" />
                        <asp:Button ID="btnExcluir" runat="server" CssClass="ButtonExcluir" Text="Excluir"
                            OnClick="btnExcluir_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o tipo de licença selecionado')"/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="div_popupEscolherCliente" class="pop_up" style="width: 850px; display: block;
        top: 0px; left: 0px;">
        <div id="fecharEscolherCliente" class="btn_cancelar_popup">
        </div>
        <asp:UpdatePanel ID="upCondicionantes" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="barra_titulo">
                    Condicionantes Padrão</div>
                <div id="camposEscolherCliente" style="min-height: 100px;">
                    <div align="left" style="margin-top: 10px; margin-bottom: 10px; overflow: auto; background-color: White;
                        border-radius: 9px;">
                        <div style="text-align: right">
                            <table cellpadding="0" cellspacing="5" width="100%">
                                <tr>
                                    <td align="right" width="25%">
                                        Descrição:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxDescricao" runat="server" CssClass="TextBox" Width="90%" Height="60px"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Prazo de Cumprimento Padrão:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbxValidadePadraoCondicionante" runat="server" CssClass="TextBox"
                                            Width="200px"></asp:TextBox>
                                        &nbsp;
                                        </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;</td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkCondicionantePeriódica" runat="server" Text="Periodica" />
                                        <asp:HiddenField ID="hfIdCondicionante" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="text-align: right">
                        <asp:Button ID="btnSalvarCondicionanteP" runat="server" CssClass="Button" Text="Salvar"
                            OnClick="btnSalvarCondicionanteP_Click" OnInit="btnSalvarCondicionanteP_Init" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva as alterações em uma condicionante padrão existente ou cria uma nova condicionante padrão')" />
                        <asp:Button ID="btnNovoCondicionanteP" runat="server" CssClass="Button" Text="Novo"
                            OnClick="btnNovoCondicionanteP_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre os campos para cadastro de uma nova condicionante padrão')"/>
                        <asp:Button ID="btnExcluirCondicionanteP" runat="server" CssClass="ButtonExcluir"
                            Text="Excluír" OnClick="btnExcluirCondicionanteP_Click" 
                            OnInit="btnExcluirCondicionanteP_Init" 
                            onprerender="btnExcluirCondicionanteP_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui a condicionante padrão carregada')"/>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNovoCondicionanteP" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnExcluirCondicionanteP" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
