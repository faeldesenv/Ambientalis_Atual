using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;

namespace Utilitarios
{
    public static class RenovarPeriodicos
    {        

        public static void RenovarVencimentosPeriodicos(IList<DateTime> datas, string tipoItemRenovacao, int idItemRenovacao) 
        {
            Msg msg = new Msg();

            if (tipoItemRenovacao.ToUpper() == "CONDICIONANTE")
            {
                Condicionante c = Condicionante.ConsultarPorId(idItemRenovacao);
                Vencimento v = c.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }

                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        c.Vencimentos.Add(novo);                        
                    }
                    c = c.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "OUTROSEMPRESA") 
            {
                OutrosEmpresa o = OutrosEmpresa.ConsultarPorId(idItemRenovacao);
                Vencimento v = o.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        o.Vencimentos.Add(novo);                        
                    }
                    o = o.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "OUTROSPROCESSO")
            {
                OutrosProcesso op = OutrosProcesso.ConsultarPorId(idItemRenovacao);
                Vencimento v = op.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        op.Vencimentos.Add(novo);
                        
                    }
                    op = op.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "RELATORIOCTF") 
            {
                CadastroTecnicoFederal cadRel = CadastroTecnicoFederal.ConsultarPorId(idItemRenovacao);
                Vencimento v = cadRel.GetUltimoRelatorio;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        cadRel.EntregaRelatorioAnual.Add(novo);

                    }
                    cadRel = cadRel.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "TAXACTF") 
            {
                CadastroTecnicoFederal cadTaxa = CadastroTecnicoFederal.ConsultarPorId(idItemRenovacao);
                Vencimento v = cadTaxa.GetUltimoPagamento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        cadTaxa.TaxaTrimestral.Add(novo);

                    }
                    cadTaxa = cadTaxa.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "CERTIFICADOCTF") 
            {
                CadastroTecnicoFederal cadCert = CadastroTecnicoFederal.ConsultarPorId(idItemRenovacao);
                Vencimento v = cadCert.GetUltimoCertificado;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        cadCert.CertificadoRegularidade.Add(novo);

                    }
                    cadCert = cadCert.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "EXIGENCIA")
            {
                Exigencia exig = Exigencia.ConsultarPorId(idItemRenovacao);
                Vencimento v = exig.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        exig.Vencimentos.Add(novo);

                    }
                    exig = exig.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "GUIAUTILIZACAO")
            {
                GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(idItemRenovacao);
                Vencimento v = guia.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        guia.Vencimentos.Add(novo);

                    }
                    guia = guia.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "TAXAANUALALVARA")
            {
                AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(idItemRenovacao);
                Vencimento v = alvara.GetUltimaTaxaAnualHectare;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        alvara.TaxaAnualPorHectare.Add(novo);

                    }
                    alvara = alvara.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "DIPEM")
            {
                AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(idItemRenovacao);
                Vencimento v = alvara.GetUltimoDIPEM;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        alvara.DIPEM.Add(novo);

                    }
                    alvara = alvara.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "VALIDADELICENCIAMENTO")
            {
                Licenciamento licenciamento = Licenciamento.ConsultarPorId(idItemRenovacao);
                Vencimento v = licenciamento.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        licenciamento.Vencimentos.Add(novo);

                    }
                    licenciamento = licenciamento.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "EXTRACAO")
            {
                Extracao extracao = Extracao.ConsultarPorId(idItemRenovacao);
                Vencimento v = extracao.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        extracao.Vencimentos.Add(novo);

                    }
                    extracao = extracao.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "RAL")
            {
                RAL ral = RAL.ConsultarPorId(idItemRenovacao);
                Vencimento v = ral.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        Vencimento novo = new Vencimento();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.Status = v.Status;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = novo.Salvar();
                        ral.Vencimentos.Add(novo);

                    }
                    ral = ral.Salvar();
                }
            }
            else if (tipoItemRenovacao.ToUpper() == "VENCIMENTODIVERSO")
            {
                Diverso diverso = Diverso.ConsultarPorId(idItemRenovacao);
                VencimentoDiverso v = diverso.GetUltimoVencimento;
                if (v.Data <= SqlDate.MinValue)
                {
                    msg.CriarMensagem("Insira um vencimento ao item antes de fazer uma renovação", "Alera", MsgIcons.Alerta);
                    return;
                }
                if (datas != null && datas.Count > 0)
                {
                    foreach (DateTime data in datas)
                    {
                        VencimentoDiverso novo = new VencimentoDiverso();
                        novo.Data = data;
                        novo.Periodico = v.Periodico;
                        novo.StatusDiverso = v.StatusDiverso;
                        if (data == datas[datas.Count - 1])
                        {
                            if (novo.Notificacoes == null)
                                novo.Notificacoes = new List<Notificacao>();

                            foreach (Notificacao n in v.Notificacoes)
                            {
                                Notificacao nn = new Notificacao();
                                nn.Template = n.Template;
                                nn.DiasAviso = n.DiasAviso;
                                nn.Emails = n.Emails;
                                novo = (VencimentoDiverso)novo.Salvar();
                                nn.Vencimento = novo;
                                nn = nn.Salvar();
                                novo.Notificacoes.Add(nn);
                                
                            }

                            //concluindo as notificações que foram copiadas como base se não envia 2 vezes 777
                            foreach (Notificacao n in v.Notificacoes)
                            {
                                n.Enviado = 1;
                                n.DataUltimoEnvio = DateTime.Now;
                                n.Salvar();
                            }
                        }

                        novo = (VencimentoDiverso)novo.Salvar();
                        diverso.VencimentosDiversos.Add(novo);

                    }
                    diverso = diverso.Salvar();
                }
            }
            
        }

        public static string NomeItemFormatado(string nomeItemRenovacao, int idItemRenovacao)
        {
            if (nomeItemRenovacao.ToUpper() == "CONDICIONANTE")
            {
                Condicionante c = Condicionante.ConsultarPorId(idItemRenovacao);
                return c != null ? "Condicionante " + c.Numero.Trim() : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "OUTROSEMPRESA")
            {
                OutrosEmpresa o = OutrosEmpresa.ConsultarPorId(idItemRenovacao);
                return o != null ? "Outro de Empresa" : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "OUTROSPROCESSO")
            {
                OutrosProcesso op = OutrosProcesso.ConsultarPorId(idItemRenovacao);
                return op != null ? "Outro de Processo" : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "RELATORIOCTF")
            {
                CadastroTecnicoFederal cadRel = CadastroTecnicoFederal.ConsultarPorId(idItemRenovacao);
                return cadRel != null ? "Entrega do Relatório Anual do Cadastro Técnico Federal" : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "TAXACTF")
            {
                CadastroTecnicoFederal cadTaxa = CadastroTecnicoFederal.ConsultarPorId(idItemRenovacao);
                return cadTaxa != null ? "Pagamento da Taxa Trimestral do Cadastro Técnico Federal" : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "CERTIFICADOCTF")
            {
                CadastroTecnicoFederal cadCert = CadastroTecnicoFederal.ConsultarPorId(idItemRenovacao);
                return cadCert != null ? "Certificado de Regularidade do Cadastro Técnico Federal" : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "EXIGENCIA")
            {
                Exigencia exig = Exigencia.ConsultarPorId(idItemRenovacao);
                return exig != null ? "Exigência" : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "GUIAUTILIZACAO")
            {
                GuiaUtilizacao guia = GuiaUtilizacao.ConsultarPorId(idItemRenovacao);
                return guia != null ? "Guia de Utilização " + guia.Numero.Trim() : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "TAXAANUALALVARA")
            {
                AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(idItemRenovacao);
                return alvara != null ? "Taxa Anual por Hectare do Alvará de Pesquisa " + alvara.Numero.Trim() : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "DIPEM")
            {
                AlvaraPesquisa alvara = AlvaraPesquisa.ConsultarPorId(idItemRenovacao);
                return alvara != null ? "DIPEM do Alvará de Pesquisa " + alvara.Numero.Trim() : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "VALIDADELICENCIAMENTO")
            {
                Licenciamento licenciamento = Licenciamento.ConsultarPorId(idItemRenovacao);
                return licenciamento != null ? "Validade do Licenciamento " + licenciamento.Numero.Trim() : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "EXTRACAO")
            {
                Extracao extracao = Extracao.ConsultarPorId(idItemRenovacao);
                return extracao != null ? "Extração " + extracao.NumeroExtracao.Trim() : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "RAL")
            {
                RAL ral = RAL.ConsultarPorId(idItemRenovacao);
                return ral != null ? "Relatório Anual de Lavra - RAL " : "";
            }
            else if (nomeItemRenovacao.ToUpper() == "VENCIMENTODIVERSO")
            {
                Diverso diverso = Diverso.ConsultarPorId(idItemRenovacao);
                return diverso != null ? "Vencimento Diverso " : "";
            }

            return "";

        }
    }
}
