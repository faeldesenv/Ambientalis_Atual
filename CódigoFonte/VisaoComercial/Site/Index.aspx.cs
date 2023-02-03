using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;

public partial class Site_Index : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Transacao transacao = new Transacao();
    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.VerificarAvisos();
        }
        base.OnLoad(e);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void VerificarAvisos()
    {
        Aviso a = Aviso.ConsultarUltimoAvisoSistema(true);
        if (a != null)
        {
            if (DateTime.Now > a.DataInicio && DateTime.Now < a.DataFim)
                lblAviso.Text = a.Descricao;
        }
        divPopAviso.Visible = lblAviso.Text.IsNotNullOrEmpty();
    }
}