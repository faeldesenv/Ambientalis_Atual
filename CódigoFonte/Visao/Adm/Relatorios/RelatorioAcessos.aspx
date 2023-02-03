<%@ Page Title="" Language="C#" MasterPageFile="~/Adm/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioAcessos.aspx.cs" Inherits="Adm_Relatorios_RelatorioAcessos" %>

<%@ Register Src="~/Adm/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataAcessoDe.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataAcessoAte.ClientID %>"));
        });
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    acessos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" Runat="Server">
    <div style="padding-top: 10px; padding-bottom: 15px;">
        <div style="width: 150px; text-align: left; font-family: 'Open Sans'; color: black; font-size: 13px;">
            Exibir as Colunas:
        </div>
        <div style="border: 1px solid white; padding: 10px;">
            <div>
                <asp:CheckBoxList ID="ckbColunas" runat="server" CssClass="chekListRelatorios" RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="0" CellSpacing="0" Font-Names="Open Sans" Font-Size="13px" ForeColor="Black">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                        adicionarDatePicker($("#<%= tbxDataAcessoDe.ClientID %>"));
                        adicionarDatePicker($("#<%= tbxDataAcessoAte.ClientID %>"));
                    });
                        </script>
                <table width="100%">
                    <tr>
                        <td align="right" width="30%">
                            Sistema:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSistemaAcessos" runat="server" AutoPostBack="True" CssClass="DropDownList" ValidationGroup="rfv" Width="50%" OnSelectedIndexChanged="ddlSistemaAcessos_SelectedIndexChanged" >
                                <asp:ListItem Selected="True" Value="0">Sustentar</asp:ListItem>
                                <asp:ListItem Value="1">Ambientalis</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="administrador_acessos" runat="server" visible="false">
                                <table width="100%">
                                    <tr>
                                        <td align="right" width="30%">
                                            Administrador:
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlAdministradorAcessos" runat="server" AutoPostBack="True" CssClass="DropDownList" Width="50%" OnSelectedIndexChanged="ddlAdministradorAcessos_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="30%">
                            Grupo Econômico:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlGrupoAdministradorAcessos" runat="server" CssClass="DropDownList" Width="50%" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoAdministradorAcessos_SelectedIndexChanged1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="30%">
                            Usuário:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlUsuarioAcessos" runat="server" CssClass="DropDownList" Width="50%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Período de:
                        </td>
                        <td align="left" valign="bottom">
                            <asp:TextBox ID="tbxDataAcessoDe" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                            &nbsp;até&nbsp;
                            <asp:TextBox ID="tbxDataAcessoAte" runat="server" CssClass="TextBox" Width="22%"></asp:TextBox>
                        </td>
                    </tr>                    
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSistemaAcessos" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlAdministradorAcessos" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlGrupoAdministradorAcessos" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>        
    </div> 
    <table width="100%">
        <tr>
            <td align="right" width="30%">
            </td>
            <td align="center">
                <br />
                <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="Button" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" Width="150px" />
            </td>
        </tr>
    </table>   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField DataField="GetGrupoAdministrador" HeaderText="Grupo Econômico/Administrador" />
            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}"/>
            <asp:BoundField DataField="Ip" HeaderText="IP" />
            <asp:BoundField DataField="GetUsuario" HeaderText="Usuário" />                                            
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

