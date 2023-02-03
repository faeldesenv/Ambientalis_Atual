<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Comercial.master" AutoEventWireup="true"
    CodeFile="ContratoPersonalizado.aspx.cs" Inherits="Comercial_ContratoPersonalizado" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<script type="text/javascript">

    function verificarCarencia() {
        if ($("#<%= rbtnCarenciaSim.ClientID %>").is(":checked") == true) {
            $("#mesesCarenciaContent").show();
        } else {
            $("#mesesCarenciaContent").hide();
        }
    }
    $(document).ready(function () {
        verificarCarencia();
        $("#<%= rbtnCarenciaSim.ClientID %>").change(function () {
            if ($(this).is(":checked") == true) {
                $("#mesesCarenciaContent").show();
            } else {
                $("#mesesCarenciaContent").hide();
            }

        });

        $("#<%= rbtnCarenciaNao.ClientID %>").change(function () {
            if ($(this).is(":checked") == true) {
                $("#mesesCarenciaContent").hide();
            } else {
                $("#mesesCarenciaContent").show();
            }

        });
    });
    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    Contrato Personalizado
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnEnviarContrato">
        <div>
            <table width="100%" cellpadding="0" cellspacing="5">
                <tr>
                    <td width="40%" align="right">
                        E-mail:
                    </td>
                    <td>
                        <asp:TextBox ID="tbxEmail" runat="server" Width="305px" CssClass="TextBox">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Obrigatório!"
                            ControlToValidate="tbxEmail" ValidationGroup="qwert"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="40%" align="right">
                        Modalidade:</td>
                    <td>
                        <asp:RadioButtonList ID="rblModalidade" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="rblModalidade_SelectedIndexChanged">
                            <asp:ListItem Value="E">Quantidade de Empresas</asp:ListItem>
                            <asp:ListItem Value="P">Quantidade de Processos</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="40%">
                        Quantidade de Processos:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="tbxQuantidadeProcessos" runat="server" CssClass="TextBox" 
                                    Enabled="False" Width="305px"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblModalidade" 
                                    EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="40%">
                        Quantidade de Empresas e/ou Filiais:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="tbxQuantidadeEmpresas" runat="server" CssClass="TextBox" 
                                    Enabled="False" Width="305px"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblModalidade" 
                                    EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td width="40%" align="right">
                        Quantidade de Úsuarios:
                    </td>
                    <td>
                        <asp:TextBox ID="tbxQuantidadeUsuarios" runat="server" Width="305px" CssClass="TextBox">
                        </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ErrorMessage="Campo Obrigatório!" ControlToValidate="tbxQuantidadeUsuarios" ValidationGroup="qwert"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="40%">
                        <b>Módulo(s):</b></td>
                    <td>
                        <asp:CheckBox ID="chkDnpm" runat="server" Text="ANM" Checked="True" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkMeioAmbiente" runat="server" Text="Meio Ambiente" 
                            Checked="True" />                        
                    </td>
                </tr>
                <tr>
                    <td width="40%" align="right">
                        <b>VALOR TOTAL DA MENSALIDADE: R$</b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxValorMensalidade" runat="server" Width="305px" CssClass="TextBox"
                            Style="font-weight: bold;">
                        </asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ErrorMessage="Campo Obrigatório!" ControlToValidate="tbxValorMensalidade" ValidationGroup="qwert"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="40%" align="right">
                        <b>Carência na Mensalidade:</b>
                    </td>
                    <td>
                        <asp:RadioButton ID="rbtnCarenciaSim" runat="server" GroupName="Carencia" Text="Sim" />&nbsp;&nbsp;<asp:RadioButton
                            ID="rbtnCarenciaNao" runat="server" GroupName="Carencia" Text="Não" Checked="True" />
                    </td>
                </tr>
                <tr id="mesesCarenciaContent" style="display:none;">
                    <td width="40%" align="right">
                        <b>Quantidade de Meses de Carência:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="tbxQuantidadeMesesCarencia" runat="server" Width="305px" CssClass="TextBox" Style="font-weight: bold;">
                        </asp:TextBox>
                    </td>
                </tr>                
                <tr>
                    <td width="40%" align="right">
                    </td>
                    <td>
                        <asp:Button ID="btnEnviarContrato" CssClass="Button" runat="server" Text="Enviar Solicitação de Cadastro"
                            OnClick="btnEnviarContrato_Click" ValidationGroup="qwert" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
