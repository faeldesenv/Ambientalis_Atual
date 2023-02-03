SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

DROP SCHEMA IF EXISTS `ambientalteste` ;
CREATE SCHEMA IF NOT EXISTS `ambientalteste` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci ;
USE `ambientalteste` ;

-- -----------------------------------------------------
-- Table `ambientalteste`.`estados`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`estados` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`estados` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(64) NULL ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`cidades`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`cidades` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`cidades` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(64) NULL ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  `id_estado` INT(11) NOT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_cidades_estados` (`id_estado` ASC) ,
  CONSTRAINT `fk_cidades_estados`
    FOREIGN KEY (`id_estado` )
    REFERENCES `ambientalteste`.`estados` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`dados_pessoas`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`dados_pessoas` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`dados_pessoas` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `inscricao_estadual` VARCHAR(24) NULL ,
  `isentoICMS` CHAR(1) NULL ,
  `sexo` CHAR(1) NULL ,
  `cpf` VARCHAR(11) NULL ,
  `rg` VARCHAR(10) NULL ,
  `data_nascimento` DATETIME NULL ,
  `estado_civil` CHAR(1) NULL ,
  `razao_social` VARCHAR(64) NULL ,
  `cnpj` VARCHAR(16) NULL ,
  `tipo` VARCHAR(12) NULL ,
  `naturalidade` VARCHAR(64) NULL ,
  `nome_pai` VARCHAR(64) NULL ,
  `nome_mae` VARCHAR(64) NULL ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`pessoas`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`pessoas` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`pessoas` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(64) NULL ,
  `site` VARCHAR(64) NULL ,
  `responsavel` VARCHAR(64) NULL ,
  `nacionalidade` VARCHAR(64) NULL ,
  `ativo` CHAR(1) NULL ,
  `rua` VARCHAR(128) NULL ,
  `numero` VARCHAR(8) NULL ,
  `bairro` VARCHAR(64) NULL ,
  `complemento` VARCHAR(64) NULL ,
  `referencia` VARCHAR(256) NULL ,
  `cep` VARCHAR(8) NULL ,
  `telefone` VARCHAR(10) NULL ,
  `ramal` INT(10) NULL ,
  `celular` VARCHAR(10) NULL ,
  `fax` VARCHAR(10) NULL ,
  `email` VARCHAR(64) NULL ,
  `observacoes` TEXT NULL ,
  `receber_noticias` CHAR(1) NULL ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  `id_cidade` INT(11) NULL ,
  `id_dados_pessoa` INT(11) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_pessoas_cidades1` (`id_cidade` ASC) ,
  INDEX `fk_pessoas_dados_pessoas1` (`id_dados_pessoa` ASC) ,
  CONSTRAINT `fk_pessoas_cidades1`
    FOREIGN KEY (`id_cidade` )
    REFERENCES `ambientalteste`.`cidades` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_pessoas_dados_pessoas1`
    FOREIGN KEY (`id_dados_pessoa` )
    REFERENCES `ambientalteste`.`dados_pessoas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`consultoras`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`consultoras` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`consultoras` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_consultoras_pessoas1` (`id` ASC) ,
  CONSTRAINT `fk_consultoras_pessoas1`
    FOREIGN KEY (`id` )
    REFERENCES `ambientalteste`.`pessoas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`administradores`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`administradores` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`administradores` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_administradores_pessoas1` (`id` ASC) ,
  CONSTRAINT `fk_administradores_pessoas1`
    FOREIGN KEY (`id` )
    REFERENCES `ambientalteste`.`pessoas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`clientes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`clientes` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`clientes` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `limite_empresas` INT NULL ,
  `id_administrador` INT(11) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_clientes_pessoas1` (`id` ASC) ,
  INDEX `fk_clientes_administradores1` (`id_administrador` ASC) ,
  CONSTRAINT `fk_clientes_pessoas1`
    FOREIGN KEY (`id` )
    REFERENCES `ambientalteste`.`pessoas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_clientes_administradores1`
    FOREIGN KEY (`id_administrador` )
    REFERENCES `ambientalteste`.`administradores` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`processos_ibamas`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`processos_ibamas` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`processos_ibamas` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`empresas`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`empresas` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`empresas` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `id_cliente` INT(11) NULL ,
  `id_processo_ibama` INT(11) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_empresas_pessoas1` (`id` ASC) ,
  INDEX `fk_empresas_clientes1` (`id_cliente` ASC) ,
  INDEX `fk_empresas_ibamas1` (`id_processo_ibama` ASC) ,
  CONSTRAINT `fk_empresas_pessoas1`
    FOREIGN KEY (`id` )
    REFERENCES `ambientalteste`.`pessoas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_empresas_clientes1`
    FOREIGN KEY (`id_cliente` )
    REFERENCES `ambientalteste`.`clientes` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_empresas_ibamas1`
    FOREIGN KEY (`id_processo_ibama` )
    REFERENCES `ambientalteste`.`processos_ibamas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`orgaos_ambientais`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`orgaos_ambientais` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`orgaos_ambientais` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(64) NULL ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`processos`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`processos` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`processos` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `data_abertura` DATETIME NULL ,
  `observacoes` TEXT NULL ,
  `numero` VARCHAR(20) NULL ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  `id_empresa` INT(11) NULL ,
  `id_orgao_ambiental` INT(11) NULL ,
  `id_consultora` INT(11) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_processos_empresas1` (`id_empresa` ASC) ,
  INDEX `fk_processos_orgaos_ambientais1` (`id_orgao_ambiental` ASC) ,
  INDEX `fk_processos_consultoras1` (`id_consultora` ASC) ,
  CONSTRAINT `fk_processos_empresas1`
    FOREIGN KEY (`id_empresa` )
    REFERENCES `ambientalteste`.`empresas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_processos_orgaos_ambientais1`
    FOREIGN KEY (`id_orgao_ambiental` )
    REFERENCES `ambientalteste`.`orgaos_ambientais` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_processos_consultoras1`
    FOREIGN KEY (`id_consultora` )
    REFERENCES `ambientalteste`.`consultoras` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`tipos_licencas`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`tipos_licencas` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`tipos_licencas` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(64) NULL ,
  `sigla` VARCHAR(10) NULL ,
  `dias_validade_padrao` INT NULL ,
  `dias_aviso_padrao` INT NULL ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`licencas`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`licencas` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`licencas` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `descricao` VARCHAR(64) NULL ,
  `numero` VARCHAR(20) NULL ,
  `dias_validade` INT(11) NULL ,
  `dias_aviso` INT(11) NULL ,
  `caminho_arquivo` VARCHAR(256) NULL ,
  `data_retirada` DATETIME NULL ,
  `emails_para_enviar` VARCHAR(512) NULL ,
  `data_aviso` DATETIME NULL ,
  `data_vencimento` DATETIME NULL ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  `id_processo` INT(11) NULL ,
  `id_tipo_licenca` INT(11) NULL ,
  `id_processo_ibama` INT(11) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_licencas_processos1` (`id_processo` ASC) ,
  INDEX `fk_licencas_tipo_licenca1` (`id_tipo_licenca` ASC) ,
  INDEX `fk_licencas_processos_ibamas1` (`id_processo_ibama` ASC) ,
  CONSTRAINT `fk_licencas_processos1`
    FOREIGN KEY (`id_processo` )
    REFERENCES `ambientalteste`.`processos` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_licencas_tipo_licenca1`
    FOREIGN KEY (`id_tipo_licenca` )
    REFERENCES `ambientalteste`.`tipos_licencas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_licencas_processos_ibamas1`
    FOREIGN KEY (`id_processo_ibama` )
    REFERENCES `ambientalteste`.`processos_ibamas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`status`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`status` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`status` (
  `id` INT NOT NULL AUTO_INCREMENT ,
  `nome` VARCHAR(45) NULL ,
  `emp` INT NULL ,
  `version` INT NULL ,
  PRIMARY KEY (`id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`condicionais`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`condicionais` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`condicionais` (
  `id` INT NOT NULL AUTO_INCREMENT ,
  `numero` VARCHAR(45) NULL ,
  `descricao` VARCHAR(512) NULL ,
  `dias_prazo` INT NULL ,
  `dias_aviso` INT NULL ,
  `data_vencimento` DATETIME NULL ,
  `data_aviso` DATETIME NULL ,
  `prazo_adicional` INT NULL ,
  `periodica` CHAR(1) NULL ,
  `emails_para_enviar` VARCHAR(512) NULL ,
  `observacoes` TEXT NULL ,
  `protocolo` VARCHAR(45) NULL ,
  `protocoloPedidoPrazo` VARCHAR(45) NULL ,
  `emp` INT NULL ,
  `version` INT NULL ,
  `id_status` INT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_Condicional_status1` (`id_status` ASC) ,
  CONSTRAINT `fk_Condicional_status1`
    FOREIGN KEY (`id_status` )
    REFERENCES `ambientalteste`.`status` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`renovacoes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`renovacoes` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`renovacoes` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `data` DATETIME NULL ,
  `proximo_vencimento` DATETIME NULL ,
  `emp` INT(11) NULL ,
  `version` INT(11) NULL ,
  `id_condicional` INT NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_renovacoes_Condicional1` (`id_condicional` ASC) ,
  CONSTRAINT `fk_renovacoes_Condicional1`
    FOREIGN KEY (`id_condicional` )
    REFERENCES `ambientalteste`.`condicionais` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`condicionantes_padroes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`condicionantes_padroes` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`condicionantes_padroes` (
  `id` INT NOT NULL AUTO_INCREMENT ,
  `descricao` VARCHAR(512) NULL ,
  `dias_validade` INT NULL ,
  `dias_aviso` INT NULL ,
  `emp` INT NULL ,
  `version` INT NULL ,
  `id_tipo_licenca` INT(11) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_condicionantes_padroes_tipos_licencas1` (`id_tipo_licenca` ASC) ,
  CONSTRAINT `fk_condicionantes_padroes_tipos_licencas1`
    FOREIGN KEY (`id_tipo_licenca` )
    REFERENCES `ambientalteste`.`tipos_licencas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`condicionantes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`condicionantes` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`condicionantes` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `id_licenca` INT(11) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_condicionantes_condicionais1` (`id` ASC) ,
  INDEX `fk_condicionantes_licencas1` (`id_licenca` ASC) ,
  CONSTRAINT `fk_condicionantes_condicionais1`
    FOREIGN KEY (`id` )
    REFERENCES `ambientalteste`.`condicionais` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_condicionantes_licencas1`
    FOREIGN KEY (`id_licenca` )
    REFERENCES `ambientalteste`.`licencas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`outros_empresas`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`outros_empresas` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`outros_empresas` (
  `id` INT(11) NOT NULL AUTO_INCREMENT ,
  `id_orgao_ambiental` INT(11) NULL ,
  `id_empresa` INT(11) NULL ,
  `id_consultora` INT(11) NULL ,
  `id_processo_ibama` INT(11) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_table2_condicionais1` (`id` ASC) ,
  INDEX `fk_outros_empresas_orgaos_ambientais1` (`id_orgao_ambiental` ASC) ,
  INDEX `fk_outros_empresas_empresas1` (`id_empresa` ASC) ,
  INDEX `fk_outros_empresas_consultoras1` (`id_consultora` ASC) ,
  INDEX `fk_outros_empresas_processos_ibamas1` (`id_processo_ibama` ASC) ,
  CONSTRAINT `fk_table2_condicionais1`
    FOREIGN KEY (`id` )
    REFERENCES `ambientalteste`.`condicionais` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_outros_empresas_orgaos_ambientais1`
    FOREIGN KEY (`id_orgao_ambiental` )
    REFERENCES `ambientalteste`.`orgaos_ambientais` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_outros_empresas_empresas1`
    FOREIGN KEY (`id_empresa` )
    REFERENCES `ambientalteste`.`empresas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_outros_empresas_consultoras1`
    FOREIGN KEY (`id_consultora` )
    REFERENCES `ambientalteste`.`consultoras` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_outros_empresas_processos_ibamas1`
    FOREIGN KEY (`id_processo_ibama` )
    REFERENCES `ambientalteste`.`processos_ibamas` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ambientalteste`.`outros_processos`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ambientalteste`.`outros_processos` ;

CREATE  TABLE IF NOT EXISTS `ambientalteste`.`outros_processos` (
  `id` INT NOT NULL AUTO_INCREMENT ,
  `id_processo` INT(11) NULL ,
  PRIMARY KEY (`id`) ,
  INDEX `fk_table3_condicionais1` (`id` ASC) ,
  INDEX `fk_outros_processos_processos1` (`id_processo` ASC) ,
  CONSTRAINT `fk_table3_condicionais1`
    FOREIGN KEY (`id` )
    REFERENCES `ambientalteste`.`condicionais` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_outros_processos_processos1`
    FOREIGN KEY (`id_processo` )
    REFERENCES `ambientalteste`.`processos` (`id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
