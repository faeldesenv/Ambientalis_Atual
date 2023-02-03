<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="Emails.aspx.cs" Inherits="Notificacao_Emails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        function marcarDesmarcarEmailsTelaVerificandoPermissao(chk) {

            var checkar = $(chk).attr('checked') == "checked" ? true : false;
            for (var i = 0; i < document.getElementsByClassName('chkSelecionarCliente').length; i++) {
                if (document.getElementsByClassName('chkSelecionarCliente')[i].children[0].disabled == false) {
                    document.getElementsByClassName('chkSelecionarCliente')[i].children[0].checked = checkar;
                }
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            font-size: medium;
        }
        .style2
        {
            font-size: x-small;
        }
        .style3
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
    <p>
        Gerenciamento de e-mails em notificações</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="conteudo_content">

 <div id="campos_filtro_email" style="width:62%; float:left">
  <div id="filtros">
      <table class="contador_itens_grid">
          <tr>
              <td align="left" colspan="4" style="font-size:10pt">
                 <strong>Pesquisa de Notificações</strong></td>
          </tr>
          <tr>
              <td align="left" colspan="4" style="font-size:10pt">
                  &nbsp;</td>
          </tr>
          <tr>
              <td align="right" width="15%">
                  Grupo Econômico:</td>
              <td>
                  <asp:DropDownList ID="ddlGrupoEconomico" runat="server" Width="100%" 
                      CssClass="DropDownList" AutoPostBack="True" 
                      onselectedindexchanged="ddlGrupoEconomico_SelectedIndexChanged">
                  </asp:DropDownList>
              </td>
              <td align="right" width="10%">
                  Tipo:</td>
              <td align="left" width="40%">
                  <asp:DropDownList ID="ddlTipo" runat="server" Width="95%" CssClass="DropDownList">
                  </asp:DropDownList>
              </td>
          </tr>
          <tr>
              <td align="right">
                  Empresa:</td>
              <td>
                  <asp:UpdatePanel ID="UPEmpresa" runat="server" UpdateMode="Conditional">
                      <ContentTemplate>
                          <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="DropDownList" 
                              Width="100%">
                          </asp:DropDownList>
                      </ContentTemplate>
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="ddlGrupoEconomico" 
                              EventName="SelectedIndexChanged" />
                      </Triggers>
                  </asp:UpdatePanel>
              </td>
              <td align="right">
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              
          </tr>
          <tr>
              <td align="right">
                  Que possuam o e-mail:</td>
              <td>
                  <asp:TextBox ID="tbxEmail" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
              <td align="right">
                  <asp:UpdatePanel ID="UPPesquisa" runat="server" UpdateMode="Conditional">
                   <ContentTemplate>
                     <asp:HiddenField ID="hfId" runat="server" />
                   </ContentTemplate>
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                      </Triggers>
                  </asp:UpdatePanel>
                  <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="Button" 
                      onclick="btnPesquisar_Click" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Pesquisa as notificações conforme os filtros escolhidos.')"/>
                  
              </td>
          </tr>
          </table>
     </div>
     <div class="contador_itens_grid">
            <table width="100%">
                <tr>
                    <td width="90%" align="right">
                        <asp:Label ID="lblExtenderPopUpEmpresas" runat="server"></asp:Label>
                        <asp:ModalPopupExtender ID="lblExtenderPopUpEmpresas_ModalPopupExtender" runat="server" 
                            BackgroundCssClass="simplemodal" CancelControlID="fecharEmailsEmpresa" 
                            PopupControlID="divPopEmailsEmpresa" TargetControlID="lblExtenderPopUpEmpresas">
                        </asp:ModalPopupExtender>
                        Quantidade de itens por pagina:</td>
                    <td>
                        <asp:DropDownList ID="ddlQuantidaItensGrid" runat="server" CssClass="DropDownList" Width="100%" AutoPostBack="True" onselectedindexchanged="ddlQuantidaItensGrid_SelectedIndexChanged" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Quantidade de notificações que serão exibidas em cada página do resultado da pesquisa')">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
  <div id="notificacoes" style="margin-top:10px">
      <asp:UpdatePanel ID="UPNotificacoes" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
              <asp:DataGrid ID="dgr" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4" DataKeyField="Id" ForeColor="#333333" GridLines="None" OnPageIndexChanged="dgr_PageIndexChanged" 
                  Width="100%" AllowPaging="True">
                  <PagerStyle BackColor="#CCCCCC" CssClass="GridPager" Font-Size="Small" 
                      ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" 
                      NextPageText="" />
                  <AlternatingItemStyle BackColor="White" />
                  <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                  <Columns>
                      <asp:BoundColumn DataField="Id" Visible="False"></asp:BoundColumn>
                      <asp:TemplateColumn HeaderText="Sel">
                          <ItemTemplate>
                              <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" Visible="<%#BindingVisivel(Container.DataItem) %>" Enabled="<%#BindingVisivel(Container.DataItem) %>"/>
                          </ItemTemplate>
                          <HeaderTemplate>
                              Sel.
                              <input id="ckbSelecionar" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarEmailsTelaVerificandoPermissao(this)" />
                          </HeaderTemplate>
                          <HeaderStyle Width="50px" />
                          <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                              Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                      </asp:TemplateColumn>
                      <asp:BoundColumn DataField="Data" DataFormatString="{0:d}" HeaderText="Data">
                          <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                              Font-Strikeout="False" Font-Underline="False" Width="80px" />
                          <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                              Font-Strikeout="False" Font-Underline="False" />
                      </asp:BoundColumn>
                      <asp:TemplateColumn HeaderText="Tipo">
                        <ItemTemplate>
                                    <asp:Label ID="lblTipoNotificacao" runat="server" 
                                        Text="<%# bindingTipoNotificacao(Container.DataItem) %>"></asp:Label>
                                </ItemTemplate>
                          <HeaderStyle Width="140px" />
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Empresa / Grupo">
                          <ItemTemplate>
                              <asp:Label ID="lblEmpresaGrupoEconomico" runat="server" 
                                  Text="<%# bindingEmpresaGrupoEconomico(Container.DataItem) %>"></asp:Label>
                          </ItemTemplate>
                          <HeaderStyle Width="240px" />
                      </asp:TemplateColumn>
                      <asp:BoundColumn DataField="Emails" HeaderText="E-mails">
                          <HeaderStyle Width="340px" />
                      </asp:BoundColumn>
                  </Columns>
                  <EditItemStyle BackColor="#7C6F57" />
                  <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                  <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="True" 
                      ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                  <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
              </asp:DataGrid>
          </ContentTemplate>
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
              <asp:AsyncPostBackTrigger ControlID="ddlQuantidaItensGrid" 
                  EventName="SelectedIndexChanged" />
              <asp:AsyncPostBackTrigger ControlID="ibtnIncluir" EventName="Click" />
              <asp:AsyncPostBackTrigger ControlID="ibtnAlterar" EventName="Click" />
              <asp:AsyncPostBackTrigger ControlID="ibtnExcluir" EventName="Click" />
          </Triggers>
      </asp:UpdatePanel>
     </div>
     
     </div>
      <div id="campos_alteracao_email" style="width:37%; float:right">
   <table width="100%" cellspacing="5">
         <tr>
             <td colspan="2" class="style1">
                 <strong>OPÇÕES: Inclusão - Alteração - Exclusão<br />
                 </strong><span class="style2">Selecione as notificações na lista ao lado que 
                 deseja alterar. </span></td>
         </tr>
         <tr>
             <td colspan="2" class="barra_titulo" style="background-color:Silver">
                 <strong>Inclusão</strong></td>
         </tr>
         <tr>
             <td align="right" width="20%">
                 Incluír:</td>
             <td align="left" >
                 <asp:TextBox ID="tbxIncluirEmail" runat="server" CssClass="TextBox" Width="70%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex: (Paulo)paulo@sustentar.inf.br<br/>Obs: Não é possível incluir mais de 1(um) e-mail por vez.')"></asp:TextBox>
             </td>
         </tr>         
         <tr>
             <td>
                 &nbsp;</td>
             <td align="left">
                 <asp:CheckBox ID="ckbIncluirTambem" runat="server" 
                     Text="Desejo também incluir este e-mail nas empresas cadastradas" />
             </td>
         </tr>
         <tr>
             <td>
                 &nbsp;</td>
             <td align="right">
               <asp:ImageButton ID="ibtnIncluir" runat="server" ImageUrl="~/imagens/icone_adicionar.png"
                        OnClick="ibtnVisualizarContrato_Click" oninit="ibtnIncluir_Init" 
                     style="height: 20px" />&nbsp;
                    <asp:Label ID="lblIncluir" runat="server" Text="Incluir nas notificações selecionadas" Style="font-weight: 700;
                        font-family: Arial; font-size: small;" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Incluir o e-mail informado nas notificações selecionadas')"></asp:Label>
             </td>
         </tr>
         <tr>
             <td>
                 &nbsp;</td>
             <td align="right">
                 &nbsp;</td>
         </tr>
         <tr>
             <td colspan="2" class="barra_titulo" style="background-color:Silver">
                 <strong>Alteração</strong>
                 </td>
         </tr>
         <tr>
             <td align="right">
                 de:</td>
             <td align="left">
                 <asp:TextBox ID="tbxEmailRetiradoAlteracao" runat="server" CssClass="TextBox" Width="70%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex: (Paulo)paulo@sustentar.inf.br<br/>Obs: Não é possível retirar mais de 1(um) e-mail por vez.')"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td align="right">
                 para:</td>
             <td align="left">
                 <asp:TextBox ID="tbxEmailIncluidoAlteracao" runat="server" CssClass="TextBox" Width="70%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex: (Paulo)paulo@sustentar.inf.br<br/>Obs: Não é possível incluir mais de 1(um) e-mail por vez.')"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td align="right">
                 &nbsp;</td>
             <td align="left">
                 <asp:CheckBox ID="ckbAlterarTambem" runat="server" 
                     Text="Desejo também realizar esta alteração nas empresas cadastradas" />
             </td>
         </tr>
         <tr>
             <td align="right">
                 &nbsp;</td>
             <td align="right">
               <asp:ImageButton ID="ibtnAlterar" runat="server" 
                     ImageUrl="~/imagens/icone_adicionar.png" onclick="ibtnAlterar_Click" 
                     oninit="ibtnAlterar_Init" style="height: 20px"/>&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Alterar nas notificações selecionadas" Style="font-weight: 700;
                        font-family: Arial; font-size: small;" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Altera o e-mail informado nas notificações selecionadas')"></asp:Label>
             </td>
         </tr>
         <tr>
             <td align="right">
                 &nbsp;</td>
             <td align="right">
                 &nbsp;</td>
         </tr>
         <tr>
             <td align="left" colspan="2" class="barra_titulo" style="background-color:Silver">
                 <strong>Exclusão</strong></td>
         </tr>
         <tr>
             <td align="right">
                 Retirar:</td>
             <td align="left">
                 <asp:TextBox ID="tbxExluirEmail" runat="server" CssClass="TextBox" Width="70%" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Para inserir nome nos emails, adicione-os entre parênteses.<br/>Ex: (Paulo)paulo@sustentar.inf.br<br/>Obs: Não é possível excluir mais de 1(um) e-mail por vez.')"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td align="right">
                 &nbsp;</td>
             <td align="left">
                 <asp:CheckBox ID="ckbExcluirTambem" runat="server" 
                     Text="Desejo também excluir este e-mail nas empresas cadastradas" />
             </td>
         </tr>
         <tr>
             <td align="right">
                 &nbsp;</td>
             <td align="right">
               <asp:ImageButton ID="ibtnExcluir" runat="server" 
                     ImageUrl="~/imagens/icone_adicionar.png" onclick="ibtnExcluir_Click" 
                     oninit="ibtnExcluir_Init"/>&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Excluir nas notificações selecionadas" Style="font-weight: 700;
                        font-family: Arial; font-size: small;" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o e-mail informado nas notificações selecionadas')"></asp:Label>
             </td>
         </tr>
         <tr>
             <td align="right" colspan="2">
                 <br />
                 <br />
                 <br />
                 <br />
                 <br />
                 <br />
                 <br />
                 <br />
                 <br />
                 <br />
                 <br />
                 <br />
             </td>
         </tr>
         <tr>
             <td align="right" colspan="2" class="style3">
                 <strong>* As notificações já enviadas também serão alteradas</strong></td>
         </tr>
     </table>
 </div>
     <div style="clear:both"></div>
 </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
    <div id="divPopEmailsEmpresa" style="width: 850px; display: block; top: 0px;
        left: 0px;" class="pop_up">
        <div id="fecharEmailsEmpresa" class="btn_cancelar_popup">
        </div>
        <div class="barra_titulo">
            Gerência de e-mails em Empresas, Consultoras e Grupos Econômicos</div>
        <div>
            <div>
                <asp:UpdatePanel ID="upEmailsEmpresas" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="False">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left">
                                <strong><asp:Label ID="lblMensagemEdicao" runat="server"></asp:Label></strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;</td>
                            </tr>                            
                            <tr>
                                <td align="left">
                                    <div class="container_grids" style="height:256px; overflow:auto">
                                        <asp:GridView ID="grvEmailsEmpresas" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                                            DataKeyNames="Id" EnableModelValidation="True" ForeColor="#333333" 
                                            GridLines="None" 
                                            OnPageIndexChanging="grvEmailsEmpresas_PageIndexChanging" PageSize="4" 
                                            Width="100%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>                                              
                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                                                <asp:TemplateField HeaderText="Excluir">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarEmailsEmpresas" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        Sel.<input id="ckbSelecionar1" type="checkbox" class="labelFiltros" onclick="marcarDesmarcarGrid(this, 'chkSelecionarEmailsEmpresas')" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle Width="45px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipo" runat="server" Text='<%# BindTipo(Container.DataItem)%>'></asp:Label>  
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="115px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nome" HeaderText="Nome" >
                                                <HeaderStyle Width="260px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="E-mails">
                                                 <ItemTemplate>
                                                   <%# BindEmails(Container.DataItem) %>
                                                 </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" 
                                                HorizontalAlign="Left" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:HiddenField ID="hfTipoAcao" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="bottom">
                                    <asp:Button ID="btnAlterarEmailsEmpresas" runat="server" CssClass="Button" 
                                        OnClick="btnAlterarEmailsEmpresas_Click" Text="Salvar" Width="170px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

