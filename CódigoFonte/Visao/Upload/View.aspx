<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Upload_Upload" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet2.css" rel="stylesheet" type="text/css" />
    <title>Arquivos</title>
    </head>
<body>
    <form id="form1" runat="server">
    
        <div class="box">
            <div class="titulo">
                ARQUIVOS ANEXOS</div>
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
                            <asp:BoundColumn DataField="Identificador" HeaderText="Identificador">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Descricao" HeaderText="Descrição"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Visualizar">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlLink" runat="server" ImageUrl="~/imagens/icone_anexo.png" 
                                        NavigateUrl="<%# BindUrlArquivo(Container.DataItem) %>" Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Width="1px" />
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
