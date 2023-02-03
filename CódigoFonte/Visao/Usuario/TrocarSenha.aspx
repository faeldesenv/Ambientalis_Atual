<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="TrocarSenha.aspx.cs" Inherits="Usuario_TrocarSenha" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">Alterar Senha
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="conteudo_trocar_senha">
      <table width="100%">
          <tr>
              <td align="right" width="30%">
                  &nbsp;</td>
              <td align="left">
                    <asp:Label ID="lblInformativoSenha" runat="server" 
                        Text="A senha deve ter no mínimo 6 dígitos, com no mínimo 2 números e 2 letras" 
                        style="font-weight: 700; font-size: small"></asp:Label>
              </td>
          </tr>
          <tr>
              <td align="right" width="30%">
                  &nbsp;</td>
              <td align="left">
                  &nbsp;</td>
          </tr>
          <tr>
              <td align="right" width="30%">
                  Senha atual*:</td>
              <td align="left">
                  <asp:TextBox ID="tbxSenhaAtual" runat="server" CssClass="TextBox" Width="200px" 
                      TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                      ErrorMessage="Obrigatório!" ValidationGroup="rfvAlterar" 
                      ControlToValidate="tbxSenhaAtual"></asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr>
              <td align="right">
                  Nova Senha*:</td>
              <td align="left">
                  <asp:TextBox ID="tbxNovaSenha" runat="server" CssClass="TextBox" Width="200px" 
                      TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                      ErrorMessage="Obrigatório!" ValidationGroup="rfvAlterar" 
                      ControlToValidate="tbxNovaSenha"></asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr>
              <td align="right">
                  Confirmação da Nova Senha*:</td>
              <td align="left">
                  <asp:TextBox ID="tbxConfirmaNovaSenha" runat="server" CssClass="TextBox" 
                      Width="200px" TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                      ErrorMessage="Obrigatório!" ValidationGroup="rfvAlterar" 
                      ControlToValidate="tbxConfirmaNovaSenha"></asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr>
              <td align="right">
                  &nbsp;</td>
              <td align="left">
                  <asp:Button ID="btnAlterarSenha" runat="server" CssClass="Button" 
                      Text="Alterar Senha" ValidationGroup="rfvAlterar" 
                      onclick="btnAlterarSenha_Click" />
                  <asp:HiddenField ID="hfId" runat="server" Value="0" />
              </td>
          </tr>
      </table>
  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
</asp:Content>

