using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Modelo
{
    [Serializable]
    [JoinedSubclass(Table = "grupos_economicos", Extends = "Modelo.Pessoa, Modelo", Name = "Modelo.GrupoEconomico, Modelo")]
    [Key(Column = "id")]
    public partial class GrupoEconomico : Pessoa
    {
        #region ___________ Atributos ___________

        private IList<Empresa> empresas;
        private int limiteEmpresas;
        private int limiteUsuariosEdicao;
        private int limiteUsuariosVisualizacao;
        private Administrador administrador;
        private IList<Menu> menus;
        private DateTime dataCadastro;
        private bool ativoLogus;
        private bool ativoAmbientalis;
        private bool termoAceito;
        private IList<Usuario> usuarios;
        private string representanteLegal;
        private string gestorEconomico;
        private bool cancelado;
        private DateTime dataCancelamento;
        private IList<Contrato> contratos;
        private bool primeiroAcesso;
        private int limiteProcessos;
        private IList<ModuloPermissao> modulosPermissao;
        private bool grupoTeste;
        private DateTime inicioTeste;
        private DateTime fimTeste;
        private bool gestaoCompartilhada;        

        #endregion

        public GrupoEconomico(int id) { this.Id = id; }
        public GrupoEconomico(Object id) { this.Id = Convert.ToInt32("0" + id.ToString()); }
        public GrupoEconomico() { }

        [ManyToOne(Name = "Administrador", Column = "id_administrador", Class = "Modelo.Administrador, Modelo")]
        public virtual Administrador Administrador
        {
            get { return administrador; }
            set { administrador = value; }
        }

        [Property(Column = "limite_empresas")]
        public virtual int LimiteEmpresas
        {
            get { return limiteEmpresas; }
            set { limiteEmpresas = value; }
        }

        [Property(Column = "limite_usuarios_edicao")]
        public virtual int LimiteUsuariosEdicao
        {
            get { return limiteUsuariosEdicao; }
            set { limiteUsuariosEdicao = value; }
        }

        [Property(Column = "limite_usuarios_visualizacao")]
        public virtual int LimiteUsuariosVisualizacao
        {
            get { return limiteUsuariosVisualizacao; }
            set { limiteUsuariosVisualizacao = value; }
        }

        [Bag(Name = "Empresas", Table = "empresas", Cascade = "delete")]
        [Key(2, Column = "id_grupo_economico")]
        [OneToMany(3, Class = "Modelo.Empresa, Modelo")]
        public virtual IList<Empresa> Empresas
        {
            get { return empresas; }
            set { empresas = value; }
        }

        [Bag(Table = "menus_clientes")]
        [Key(2, Column = "id_grupo_economico")]
        [ManyToMany(3, Class = "Modelo.Menu, Modelo", Column = "id_menu")]
        public virtual IList<Menu> Menus
        {
            get { return menus; }
            set { menus = value; }
        }

        [Property(Column = "data_cadastro")]
        public virtual DateTime DataCadastro
        {
            get
            {
                if (dataCadastro <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataCadastro;
            }
            set { dataCadastro = value; }
        }

        [Property(Column = "ativo_logus", Type = "TrueFalse")]
        public virtual bool AtivoLogus
        {
            get { return ativoLogus; }
            set { ativoLogus = value; }
        }

        [Property(Column = "ativo_ambientalis", Type = "TrueFalse")]
        public virtual bool AtivoAmbientalis
        {
            get { return ativoAmbientalis; }
            set { ativoAmbientalis = value; }
        }

        [Property(Column = "termo_aceito", Type = "TrueFalse")]
        public virtual bool TermoAceito
        {
            get { return termoAceito; }
            set { termoAceito = value; }
        }

        [Bag(Name = "Usuarios", Table = "usuarios", Cascade = "delete")]
        [Key(2, Column = "id_grupo_economico")]
        [OneToMany(3, Class = "Modelo.Usuario, Modelo")]
        public virtual IList<Usuario> Usuarios
        {
            get { return usuarios; }
            set { usuarios = value; }
        }

        [Property(Column = "representante_legal")]
        public virtual string RepresentanteLegal
        {
            get { return representanteLegal; }
            set { representanteLegal = value; }
        }

        [Property(Column = "gestor_economico")]
        public virtual string GestorEconomico
        {
            get { return gestorEconomico; }
            set { gestorEconomico = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Cancelado
        {
            get { return cancelado; }
            set { cancelado = value; }
        }

        [Property(Column = "data_cancelamento")]
        public virtual DateTime DataCancelamento
        {
            get
            {
                if (dataCancelamento <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return dataCancelamento;
            }

            set { dataCancelamento = value; }
        }

        [Bag(Name = "Contratos", Table = "contratos", Cascade = "delete", Lazy = CollectionLazy.False)]
        [Key(2, Column = "id_grupo_economico")]
        [OneToMany(3, Class = "Modelo.Contrato, Modelo")]
        public virtual IList<Contrato> Contratos
        {
            get { return contratos; }
            set { contratos = value; }
        }

        [Property(Type = "TrueFalse", Column = "primeiro_acesso")]
        public virtual bool PrimeiroAcesso
        {
            get { return primeiroAcesso; }
            set { primeiroAcesso = value; }
        }

        [Property(Column = "limite_processos")]
        public virtual int LimiteProcessos
        {
            get { return limiteProcessos; }
            set { limiteProcessos = value; }
        }

        [Bag(Table = "grupos_modulos_permissao")]
        [Key(2, Column = "id_grupo")]
        [ManyToMany(3, Class = "Modelo.ModuloPermissao, Modelo", Column = "id_modulo_permissao")]
        public virtual IList<ModuloPermissao> ModulosPermissao
        {
            get { return modulosPermissao; }
            set { modulosPermissao = value; }
        }

        [Property(Type = "TrueFalse", Column = "grupo_teste")]
        public virtual bool GrupoTeste
        {
            get { return grupoTeste; }
            set { grupoTeste = value; }
        }

        [Property(Column = "inicio_teste")]
        public virtual DateTime InicioTeste
        {
            get
            {
                if (inicioTeste <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return inicioTeste;
            }

            set { inicioTeste = value; }            
        }

        [Property(Column = "fim_teste")]
        public virtual DateTime FimTeste
        {
            get
            {
                if (fimTeste <= SqlDate.MinValue)
                    return SqlDate.MinValue;
                else
                    return fimTeste;
            }

            set { fimTeste = value; }  
        }

        [Property(Type = "TrueFalse", Column = "gestao_compartilhada")]
        public virtual bool GestaoCompartilhada
        {
            get { return gestaoCompartilhada; }
            set { gestaoCompartilhada = value; }
        }
    }
}
