using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;
using System.Web;

namespace Seguranca
{
    /// <summary>
    /// Responsável por manipular as permissões do Sistema
    /// </summary>
    public class Permissoes
    {
        public static bool UsuarioPossuiAcessoUrl(Usuario user, string url)
        {
            if (user.Administrador != null)
                return true;
            else
                foreach (Menu menu in user.Cliente.Menus)
                    if ((!string.IsNullOrEmpty(menu.UrlCadastro) && url.Contains(menu.UrlCadastro)) ||
                        (!string.IsNullOrEmpty(menu.UrlPesquisa) && url.Contains(menu.UrlPesquisa)))
                        return true;
            return false;
        }

        public static void ValidarControle(ImageButton globalSystemWebUIWebControlsImageButton)
        {
            throw new NotImplementedException();
        }
    }
}
