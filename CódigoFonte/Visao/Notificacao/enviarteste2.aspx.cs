using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;

public partial class Notificacao_enviarteste2 : System.Web.UI.Page
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = Utilitarios.Criptografia.Criptografia.Decrypt("d+pzDYIrnIfQUCfZ7dWh8Q==", true) + " ---  " + Utilitarios.Criptografia.Criptografia.Decrypt("mH5lrpBMqs6zATTJvGPq3ifAqkOwn2Ea", true);

        //
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            Alert.Show(ex.Message + " - " + ex.InnerException.ToString());
        }
        finally
        {

        }
    }
}