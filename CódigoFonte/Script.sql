/*
 -------------------------------------------------------------------------------------
 QUEM: 
 QUANDO: 
 DESCRIÇÃO: 
 -------------------------------------------------------------------------------------
*/
/*felipe
hoje
inserir esses scripts no banco:*/

alter table avisos add aviso_comercial char(1) null;

alter table mensalidades add pago_revenda char(1) null, pago_supervisor char(1) null;

UPDATE menus_comerciais SET Nome = 'Visualizar' WHERE id_menu = 15;

alter table contratos_comerciais add desativado char(1) null;



/* Verificar as permissoes nos menus */

/*--------------------- Menus de Politica ---------------------*/
/* Cria Menu */
INSERT INTO menus_comerciais VALUES(33, 
2, 1, 'TOD', 'Política',
 'http://sustentar.inf.br/Comercial/Repositorio/Arquivos/d045b9f2-2.pdf' ,
  '1', 
  0, 23 );
/* Atualiza os ja existentes */
UPDATE menus_comerciais SET url = NULL WHERE id = 23;
UPDATE menus_comerciais SET id_menu = 23  WHERE id = 32;


/*--------------------- Menus de Materia de Apoio ---------------------*/

INSERT INTO menus_comerciais VALUES(34, 
2, 1, 'TOD', 'Material de Apoio',
NULL ,  '../imagens/menu/arquivo.png',   0, NULL );

UPDATE menus_comerciais SET id_menu = 34, url_icone= NULL  WHERE id = 24;
INSERT INTO menus_comerciais VALUES(35, 
2, 1, 'TOD', 'Videos',
NULL ,  '',   0, 34 );


/*--------------------- Menus de Base Demostracao ---------------------*/
/* Cria Menu */
INSERT INTO menus_comerciais VALUES(36, 
2, 1, 'TOD', 'Demostração',
 'http://sustentar.inf.br/sistema/acesso/LoginDemostracao.aspx?v=0$xyzfsdahfiuh2132dsfa1=fasd21f3as2d1f3a2sdfs45fd6a5sd4f6as' ,
  '../imagens/icone_TipodeLicença.png', 
  0, NULL );





















