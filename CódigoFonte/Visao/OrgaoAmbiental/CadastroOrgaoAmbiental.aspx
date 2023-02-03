<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroOrgaoAmbiental.aspx.cs" Inherits="OrgaoAmbiental_CadastroOrgaoAmbiental" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server"> 
    <%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
    <p>
        Cadastro de orgão ambiental</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UPOrgaoAmbiental" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="right" width="40%">
                        Tipo de Orgão Ambiental*:</td>
                    <td>
                        <asp:DropDownList ID="ddlTipoOrgao" runat="server" AutoPostBack="True" 
                            CssClass="DropDownList" 
                            onselectedindexchanged="ddlTipoOrgao_SelectedIndexChanged" Width="305px">
                            <asp:ListItem Value="0">-- Selecione --</asp:ListItem>
                            <asp:ListItem Value="1">Federal</asp:ListItem>
                            <asp:ListItem Value="2">Estadual</asp:ListItem>
                            <asp:ListItem Value="3">Municipal</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="Campo Obrigatório" InitialValue="0" 
                            ValidationGroup="rfvSalvar" ControlToValidate="ddlTipoOrgao"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                     <div id="estado_licenca" runat="server" visible="false">
                        <table width="100%">
                            <tr>
                                <td align="right" width="40%">
                                    Estado:</td>
                                <td>
                                    <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" 
                                        CssClass="DropDownList" 
                                        onselectedindexchanged="ddlEstado_SelectedIndexChanged" Width="305px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                     <div id="cidade_licenca" runat="server" visible="false">
                        <table width="100%">
                            <tr>
                                <td align="right" width="40%">
                                    Cidade:</td>
                                <td>
                                    <asp:DropDownList ID="ddlCidade" runat="server" CssClass="DropDownList" 
                                        Width="305px">
                                    </asp:DropDownList>
                                    
                                </td>
                            </tr>
                        </table>
                       </div>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="40%">
                        Nome*:</td>
                    <td>
                        <asp:TextBox ID="tbxNomeOrgao" runat="server" CssClass="TextBox" Width="305px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="tbxNomeOrgao" ErrorMessage="Campo Obrigatório" 
                            ValidationGroup="rfvSalvar"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="40%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" width="40%">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnSalvar" runat="server" CssClass="Button" Text="Salvar" 
                            ValidationGroup="rfvSalvar" onclick="btnSalvar_Click" 
                            onprerender="PermissaoUsuario_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Salva as alterações em um orgão ambiental existente ou cria um novo orgão ambiental')"/>
                        <asp:Button ID="btnNovo" runat="server" CssClass="Button" Text="Novo" 
                            onclick="btnNovo_Click" onprerender="PermissaoUsuario_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Abre os campos para cadastro de um novo orgão ambiental')" />
                        <asp:Button ID="btnExcluir" runat="server" CssClass="ButtonExcluir" 
                            Text="Excluir" onclick="btnExcluir_Click" 
                            onprerender="PermissaoUsuario_PreRender" onmouseout="tooltip.hide();" onmouseover="tooltip.show('Exclui o orgão ambiental carregado')"/>
                        <asp:HiddenField ID="hfIdOrgao" runat="server" Visible="False" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTipoOrgao" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
</asp:Content>

