<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true" CodeFile="Avisos.aspx.cs" Inherits="Avisos_Avisos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
   <script type="text/javascript">
       $(document).ready(function () { CriarEventos(); });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" Runat="Server">
  <p>
        avisos</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="campos_cadastro_usuarios">
        <table class="contador_itens_grid">
            <tr>
                <td style="width: 25%; text-align: right">
                    Exibição de:
                </td>
                <td style="width: 65%">
                    <asp:TextBox ID="tbxDataInicio" runat="server" class="TextBox"></asp:TextBox>
                    <asp:CalendarExtender ID="tbxDataInicio_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="tbxDataInicio" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 25%; text-align: right">
                    até:
                </td>
                <td style="width: 25%">
                    <asp:TextBox ID="tbxDataFim" runat="server" CssClass="TextBox"></asp:TextBox>
                    <asp:CalendarExtender ID="tbxDataFim_CalendarExtender" runat="server" Enabled="True"
                        TargetControlID="tbxDataFim" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 25%; text-align: right">
                    Exibir em:</td>
                <td style="width: 25%">
                    <asp:DropDownList ID="ddlExibirEm" runat="server" CssClass="DropDownList" Width="180px">
                     <asp:ListItem Value="0" Selected="True">-- Selecione --</asp:ListItem>
                     <asp:ListItem Value="1">Sistema Sustentar</asp:ListItem>
                     <asp:ListItem Value="2">Sustentar Comercial</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    Aviso:
                    <cc1:Editor ID="Editor1" runat="server" Height="400px" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSalvar" runat="server" CssClass="botao_login" Text="Criar aviso" 
                        onclick="btnSalvar_Click" Width="200px" />
                    &nbsp;
                    &nbsp;
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">
</asp:Content>

