<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Acesso_Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Sustentar</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../imagens/favicon.png" type="image/x-icon" />
    <link rel="shortcut icon" href="../imagens/favicon.png" type="image/x-icon" />
    <link rel="apple-touch-icon" href="../imagens/favicon.png" type="image/x-icon" />
    <script src="../Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <%--<script src="../Utilitarios/ToolTip/script.js" type="text/javascript"></script>--%>
    <%--<script src="../Scripts/Funcoes.js" type="text/javascript"></script>--%>
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function mascaras() {            
            $("#<%=tbxCNPJ.ClientID %>").unbind();
            $("#<%=tbxCNPJ.ClientID %>").mask("99.999.999/9999-99");
            $("#<%=tbxCPF.ClientID %>").unbind();
            $("#<%=tbxCPF.ClientID %>").mask("999.999.999-99");
        }
    </script>

    <style type="text/css">
        .pop_up_super_super {
            padding: 20px;
            border-radius: 10px;
            position: relative;
            background-color: #fafafa;
            border: 1px inset #eae9e9;
            margin: 10px;
            font-size: 12px;
            color: #444141;
            z-index: 23000 !important;
        }

        .btn_cancelar_popup {
            position: absolute;
            width: 25px;
            height: 29px;
            background-image: url('../imagens/x.png');
            background-repeat: no-repeat;
            top: -5px;
            right: -12px;
            cursor: pointer;
        }

        .simplemodal {
            background-color: #000;
            cursor: wait;
            opacity: 0.5;
            z-index: 40;
        }

        .pop_up {
            padding: 20px;
            border-radius: 10px;
            position: relative;
            background-color: #fafafa;
            border: 1px inset #eae9e9;
            margin: 10px;
            font-size: 12px;
            color: #444141;
            z-index: 50;
        }

        .barra_titulo {
            -moz-border-radius: 9px;
            -webkit-border-radius: 9px;
            border-radius: 9px;
            padding: 5px;
            font-size: larger;
            font-weight: bold;
            margin-bottom: 10px;
            background-color: #565656;
            color: White;
        }

        .TextBox {
            border: 1px solid #e8e9e9;
            border-radius: 4px;
            text-align: left;
            padding: 3px;
            font-size: 12px;
            margin: 2px;
            -moz-box-shadow: 0 1px 1px #dddddd;
            -ms-box-shadow: 0 1px 1px #dddddd;
            -webkit-box-shadow: 0 1px 1px #dddddd;
            box-shadow: 0 1px 1px #dddddd;
        }

            .TextBox:focus {
                background-color: #eefaee;
            }
    </style>

</head>
<body>  
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScriptsHandlerUrl="~/Utilitarios/CombineScriptsHandler.ashx" EnableScriptGlobalization="true" EnableScriptLocalization="true">
        </asp:ToolkitScriptManager>        
       
        <div id="container">                    
            <uc1:MBOX ID="MBOX1" runat="server" /> <div id="logo">
            </div>
            <div style="width: 100%; height: 10px;">
            </div>
            <div id="header_login">
                Login
            </div>
            <div id="box_login">
                <p>
                    <label class="req">
                        Usuário:
                    </label>
                    <br />
                    <asp:TextBox ID="tbxLogin" runat="server" CssClass="tbxAcesso" ValidationGroup="rfv"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxLogin"
                        ErrorMessage="*" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <label class="req">
                        Senha:
                    </label>
                    <br />
                    <asp:TextBox ID="tbxSenha" runat="server" CssClass="tbxAcesso" TextMode="Password"
                        ValidationGroup="rfv"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxSenha"
                        ErrorMessage="*" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                </p>
                <p style="text-align: right; margin-left: 40px;">
                    <asp:Button ID="btnLogin" runat="server" Text="Acessar" CssClass="botao_login" OnClick="btnLogin_Click"
                        ValidationGroup="rfv" OnInit="btnLogin_Init" />
                </p>
            </div>
            <div style="margin-top: 3px; font-size: xx-small; text-align: center; color: #999999;">
                Sustentar Sistema © - Todos os direitos reservados
            </div>
            <asp:Label ID="lblDadosGrupo" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="modalDadosGrupo_extender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="div_fechar_cadastro_dados_grupo" 
                PopupControlID="cadastro_dados_grupo_pop_up" TargetControlID="lblDadosGrupo">
            </asp:ModalPopupExtender>
        </div>
        <div style="font-family: Arial; font-size: 12px; display:block;">
            <div id="cadastro_dados_grupo_pop_up" class="pop_up_super_super" style="width: 500px; background-color: white;">
                <div id="div_fechar_cadastro_dados_grupo" class="btn_cancelar_popup">
                </div>
                <div class="barra_titulo">
                    Dados do Grupo Econômico de Teste
                </div>
                <div id="campos_dados_grupo">
                    <asp:UpdatePanel ID="upDadosGrupo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <script type="text/javascript">
                                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { mascaras(); });
                         </script>
                            <table style="width: 100%">
                                <tr>
                                    <td align="left" colspan="2" class="auto-style1">
                                        <asp:Label ID="Label1" runat="server" Text="Este grupo de teste não possui CNPJ ou CPF cadastrados. Informe estes dados para prosseguir."
                                            Style="font-weight: 700; font-size: 12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>Tipo de Pessoa:</strong>
                                    </td>
                                    <td>                                        
                                        <asp:RadioButton ID="rbtnPessoaFisica" runat="server" Text="Física" OnCheckedChanged="rbtnPessoaFisica_CheckedChanged" AutoPostBack="True" />
                                        <asp:RadioButton ID="rbtnPessoaJuridica" runat="server" Checked="True" Text="Jurídica" OnCheckedChanged="rbtnPessoaJuridica_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                </tr>
                                </table>
                            <div>                               
                                    <table style="width: 100%">
                                    <tr>
                                    <td align="right">
                                        <div id="tabelaPessoaFisica" runat="server" visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td width="35%" align="right" id="Td1">CPF:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbxCPF" runat="server" Width="305px" CssClass="TextBox">
                                                        </asp:TextBox>                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <div id="tabelaPessoaJuridica" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td width="30%" align="right">CNPJ:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbxCNPJ" runat="server" Width="305px" CssClass="TextBox">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="40%">Razão Social:
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
                                </table>                                                               
                            </div>
                            <div id="div_obrigatorio" runat="server" visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblObrigatório" runat="server" style="color: #FF0000; font-weight: 700"></asp:Label>
                                                    </td>
                                                </tr>                                                
                                            </table>
                                        </div>
                            <table width="100%">                            
                                <tr>
                                    <td align="right">&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnSalvar" runat="server" CssClass="botao_login" Text="Avançar"
                                            OnClick="btnSalvar_Click" />
                                        <asp:HiddenField ID="hfIdGrupo" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rbtnPessoaFisica" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="rbtnPessoaJuridica" EventName="CheckedChanged" />
                        </Triggers>                        
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </form>
    
</body>
</html>
