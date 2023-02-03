<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload_Upload" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>UPLOAD</title>
    <style type="text/css">
        .style1
        {
            font-size: x-small;
        }
        
        a img
        {
            border: none;
            }
    <%--        input[type=file] {
   display:block;
   height:0;
   width:0;
}--%>
    </style>
    <script src="../Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
        function AppendFileUploadClickEvent(id) {
            var o = document.getElementById(id);
            if (o == null || o.tagName != "INPUT" || o.type != "file")
                return;
            alert('opa');
            var mask = document.createElement("DIV");
            mask.style.width = o.clientWidth + "px";
            mask.style.height = o.clientHeight + "px";
            mask.style.position = "absolute";
            mask.style.left = o.offsetLeft + "px";
            mask.style.top = o.offsetTop + "px";
            mask.style.zIndex = 9999;
            mask.style.backgroundColor = "white";
            mask.style.filter = "alpha(opacity=0)";
            mask.style.opacity = "0";
            mask.style.cssText += "-moz-opacity:0;";
            mask.onclick = function () { o.click(); };
            document.body.appendChild(mask);
        }

        $(document).ready(function () {
            $('#<%=Button1.ClientID %>').click(function () {
                $('#bolado').trigger('click');
            });
            $('input[type=file]').change(function () {
                var vals = $(this).val(),
       val = vals.length ? vals.split('\\').pop() : '';

                $('input[type=text]').val(val);
            });
        });
</script>--%>

</head>
<body>
    <form id="form1" runat="server">     
        <div class="box">
            <div class="titulo">
                UPLOAD DE ARQUIVOS</div>
               
                 <div class="upload">
                     <asp:Button ID="btnUpload" runat="server" Text="Enviar" CssClass="Button" 
                         onclick="btnUpload_Click" Width="115px" />
            </div>
            <div class="botao_content">
                
                     <asp:FileUpload ID="fluUpload" runat="server" Width="329px" class="teste" />                     
                     <br />
                     <span class="style1">Descrição</span><br />
                     <asp:TextBox ID="tbxDescricao" runat="server" CssClass="TextBox" Width="100%"></asp:TextBox>
                    
            </div>
            <div class="content">                    
                    <asp:DataGrid ID="dgrArquivos" runat="server" AutoGenerateColumns="False"
                        CellPadding="2" DataKeyField="Id" Font-Bold="False" 
                    Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" Height="45px"
                        PageSize="8" Width="98%" ForeColor="#333333">
                        <PagerStyle BackColor="#CCCCCC" Font-Size="Small" ForeColor="White" HorizontalAlign="Center"
                            NextPageText="" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Identificador" HeaderText="Identificador">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Descricao" HeaderText="Descrição"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Visualizar">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlLink" runat="server" ImageUrl="~/imagens/icone_anexo.png" 
                                        NavigateUrl="<%# BindUrlArquivo(Container.DataItem) %>" Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Width="1px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Del.">
                                <HeaderTemplate>
                                    <asp:ImageButton ID="ibtnExcluir" runat="server" CommandName="Delete" 
                                        ImageUrl="~/imagens/excluir.gif" ToolTip="Excluir" 
                                        onclick="ibtnExcluir_Click" />
                                    &nbsp;
                                </HeaderTemplate>
                                                                
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbExcluir" runat="server" CssClass="chkSelecionarCliente" />
                                </ItemTemplate>
                                <HeaderStyle Width="45px" Font-Bold="False" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                            VerticalAlign="Top" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    
            </div>
        </div>
    
    </form>
</body>
</html>
