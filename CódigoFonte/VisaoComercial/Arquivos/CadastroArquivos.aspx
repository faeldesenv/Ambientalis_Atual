<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true"
    CodeFile="CadastroArquivos.aspx.cs" Inherits="Arquivos_CadastroArquivos" %>
<%@ Register src="../MBOX/MBOX.ascx" tagname="MBOX" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .item_arquivo_container
        {
            height: auto;
            width: 250px;
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
            background-image: url('../imagens/icone_arquivos_item.png');
            background-repeat: no-repeat;
            background-position: 10px center;
            position: relative;
            float: left;
            margin: 5px;
        }
        
        .item_arquivo_titulo
        {
            margin-left: 60px;
            margin-top: 8px;            
            overflow: hidden;
            color:#918d8d;
        }
        .item_arquivo_acoes
        {
            right: 5px;
            bottom: 3px;
            position: absolute;
        }
        
        .item_arquivo_descricao
        {
            margin-left: 60px;
            margin-top: 3px;            
            overflow: hidden;
            font-size:11px;
            color:#a8a5a5;
           
        }
            
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //DesabilitaBotoes();
        });
        function DesabilitaBotoes() {
            $('.ibtnExcluirArquivo_class').hide();
            $('.ibtnExcluirArquivo_class').prop("disabled", true);
            $('#<%= btnPopUpUpload.ClientID %>').prop("disabled", true);
            $('#<%= btnPopUpUpload.ClientID %>').hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Barra" runat="Server">
    Arquivos &nbsp;<span>
        <asp:Button ID="btnPopUpUpload" CssClass="Button" runat="server" Text="Upload" />
    <asp:ModalPopupExtender ID="btnPopUpUpload_ModalPopupExtender" runat="server" 
        BackgroundCssClass="simplemodal" DynamicServicePath="" Enabled="True" 
        PopupControlID="divPopUp" TargetControlID="btnPopUpUpload" 
        CancelControlID="cancelPopUp">
    </asp:ModalPopupExtender>
    </span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 10px;">
    </div>
    <div style="min-height: 400px;">
        <asp:UpdatePanel ID="udpArquivos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Repeater ID="rptArquivos" runat="server">
                    <ItemTemplate>
                        <div class="item_arquivo_container" >
                            <div class="item_arquivo_titulo">
                                <%# Eval("Identificador") %></div>
                            <div class="item_arquivo_descricao">
                                <%# Eval("Descricao") %></div>
                            <div class="item_arquivo_descricao">
                                <%# Eval("DataPublicacao", "{0:d}") %></div>                            
                            <div class="item_arquivo_acoes">
                                <%--<asp:ImageButton ID="ibtnVisualizarArquivo" runat="server" ImageUrl="~/imagens/visualizar20x20.png" />--%>
                                <a href="../../<%# Eval("Caminho") %>" target="_blank">
                                    <img alt="" src="../imagens/visualizar20x20.png" /> </a>
                                &nbsp;&nbsp;&nbsp;<asp:ImageButton
                                    ID="ibtnExcluirArquivo" CssClass="ibtnExcluirArquivo_class" runat="server" ImageUrl="~/imagens/excluir.gif" OnClick="ibtnExcluirArquivo_Click"
                                     CommandName="Delete" commandargument='<%# Eval("Id") %>' OnPreRender="ibtnExcluirArquivo_PreRender" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div style="width: 100%; height: 1px; clear: both">
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="divPopUp" style="width: 292px; display: block; top: 0px; left: 0px;" class="pop_up">
        <div class="barra_titulo" style="margin-top: 10px;">
            Upload</div>
        <div>
            <asp:FileUpload ID="fulArquivo" runat="server" />

            <br />
            <br />
            <span><strong><em>Descrição:</em></strong></span><br />
            <asp:TextBox ID="tbxDescricao" runat="server" CssClass="TextBox" Width="95%" 
                Height="50px" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div style="text-align: right; margin-top: 5px;">
            <asp:Button ID="btnUpLoadArquivo" runat="server" CssClass="Button" Text="Upload"
                OnClick="btnUpLoadArquivo_Click" />&nbsp;&nbsp;
            <a id="cancelPopUp" class="Button" href="#">Cancelar</a></div>
    </div>    
</asp:Content>
