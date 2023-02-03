<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Adm.master" AutoEventWireup="true" CodeFile="CadastroClientesSite.aspx.cs" Inherits="Adm_CadastroClientesSite" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

        .gridEsp td 
        {
            border-right:1px solid #cfd1d1;
            border-left:1px solid #cfd1d1;     
        }

        
        .gridEspMaster
        {
            border-bottom:1px solid #cfd1d1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
    Cadastro de Grupo Econômico no Site
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSalvar">
        <div>
            <table width="100%" cellpadding="0" cellspacing="5">
                <tr>
                    <td width="40%" align="right">
                        Logomarca:
                    </td>
                    <td>
                            
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Image ID="imgLogomarca" runat="server" ImageUrl="~/imagens/FotoIndisponivelAlbum.png" style="max-width:200px; max-height:200px;" />
                        </ContentTemplate>
                        </asp:UpdatePanel>
                            <br />

                        
                        <div>
                            <asp:FileUpload ID="fuiGrupoEconomico" runat="server" CssClass="TextBox" />
                            <asp:Button CssClass="Button"
                                ID="btnUploadImagem" runat="server" Text="Upload" 
                                style="margin-top:5px;" onclick="btnUploadImagem_Click" />
                        </div>
                    </td>
                    
                </tr>
                <tr>
                    <td width="40%" align="right">
                        Nome:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <ContentTemplate>
                            <asp:TextBox ID="tbxNome" runat="server" Width="383px" CssClass="TextBox">
                        </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="Campo Obrigatório!" ControlToValidate="tbxNome" ValidationGroup="qwert"></asp:RequiredFieldValidator>
                           </ContentTemplate> 
                        </asp:UpdatePanel>
                        
                    </td>
                </tr>
                <tr>
                    <td width="40%" align="right">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSalvar" runat="server" CssClass="Button" 
                            Text="Salvar Cliente" onclick="btnSalvar_Click" ValidationGroup="qwert" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div>
    
    <div class="barra_titulos" id="barraClientes" runat="server" style="height:auto;   text-align:center;margin:0 auto; margin-top:20px; font-size:13px; font-weight:bold;">
    Lista de Clientes
    </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border-bottom:1px solid #cfd1d1;">
        <asp:GridView ID="grvClientes" Width="100%" runat="server" 
            AutoGenerateColumns="False" EnableModelValidation="True" BorderWidth="0px" 
            GridLines="None" CssClass="gridEspMaster">
            <AlternatingRowStyle BackColor="White" CssClass="gridEsp" />
            <Columns>
                <asp:TemplateField HeaderText="Logomarca">
                    <ItemTemplate>
                        <img alt="" src='<%# Eval("LinkImagem") %>' style="max-height:50px; max-width:50px;" />
                    </ItemTemplate>
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Nome" HeaderText="Nome" >
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Ações">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        
                        <asp:ImageButton OnPreRender="prerenderExcluirClientes" ID="ImageButton1" runat="server" ImageUrl="~/imagens/excluir.gif" CommandArgument='<%# Eval("Id") %>' OnClick="btnDelete_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BorderWidth="0px" />
            <HeaderStyle 
                CssClass="GridHeader" />
            <RowStyle BackColor="#E3EAEB" CssClass="gridEsp" Height="52px" 
                VerticalAlign="Middle" />
        </asp:GridView>
    </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
</asp:Content>

