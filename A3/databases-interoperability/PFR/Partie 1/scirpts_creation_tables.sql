/*DROP DATABASE IF EXISTS `pica_nico`;
CREATE DATABASE `pica_nico`;*/

USE `pica_nico`;

DROP TABLE IF EXISTS `pica_nico`.`client`;
CREATE TABLE `pica_nico`.`client` (
  `codeC` VARCHAR(4) NOT NULL,
  `nom` VARCHAR(20) NOT NULL,
  `prenom` VARCHAR(20) NULL,
  `adresse` VARCHAR(50) NULL,
  `telephone` VARCHAR(20) NULL,
  `email` VARCHAR(50) NULL,
  PRIMARY KEY (`codeC`) );

DROP TABLE IF EXISTS `pica_nico`.`sejour`;
CREATE TABLE `pica_nico`.`sejour` (
  `id` VARCHAR(4) NOT NULL,
  `theme` VARCHAR(2) NOT NULL,
  `date` INT(2) NOT NULL,
  `annee` INT(4) NOT NULL,
    PRIMARY KEY (`id`) );

DROP TABLE IF EXISTS `pica_nico`.`controleur`;
CREATE TABLE `pica_nico`.`controleur` (
  `id` VARCHAR(4) NOT NULL,
  `nom` VARCHAR(20) NOT NULL,
  `prenom` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`id`) );

DROP TABLE IF EXISTS `pica_nico`.`parking`;
CREATE TABLE `pica_nico`.`parking` (
  `id` VARCHAR(4) NOT NULL,
  `nom` VARCHAR(20) NOT NULL,
  `adresse` VARCHAR(50) NOT NULL,
  `code_postal` VARCHAR(5) NOT NULL,
  `ville` VARCHAR(40) NOT NULL,
  PRIMARY KEY (`id`) );

DROP TABLE IF EXISTS `pica_nico`.`voiture`;
  CREATE TABLE `pica_nico`.`voiture` (
    `immat` VARCHAR(10) NOT NULL,
    `marque` VARCHAR(20) NOT NULL,
    `modele` VARCHAR(20) NOT NULL,
    `id_controleur` VARCHAR(4) NOT NULL,
    `categorie` VARCHAR(20) NOT NULL,
    `places` INT NOT NULL CHECK (places <= 4),
    `disponibilite` BOOLEAN NOT NULL,
    `prixJ` INT NOT NULL,
    `motif_indisponibilite` VARCHAR(120) NULL,
    `id_parking` VARCHAR(4) NOT NULL,
    `place_parking` ENUM('A0','A1','A2','A3','A4','A5','A6','A7','A8','A9') NOT NULL,
    PRIMARY KEY (`immat`),
     CONSTRAINT `id_controleur_voiture` FOREIGN KEY (`id_controleur`)
  		REFERENCES `pica_nico`.`controleur` (`id`)
  		ON DELETE NO ACTION
  		ON UPDATE NO ACTION );

DROP TABLE IF EXISTS `pica_nico`.`intervention`;
CREATE TABLE `pica_nico`.`intervention` (
  `id` VARCHAR(4) NOT NULL,
  `immat` VARCHAR(10) NOT NULL,
  `id_controleur` VARCHAR(4) NOT NULL,
  `type_intervention` VARCHAR(20) NOT NULL,
  `date` DATE NOT NULL,
  PRIMARY KEY (`id`),
   CONSTRAINT `immat_voiture_intervention` FOREIGN KEY (`immat`)
    REFERENCES `pica_nico`.`voiture` (`immat`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
   CONSTRAINT `id_controleur_intervention` FOREIGN KEY (`id_controleur`)
 	  REFERENCES `pica_nico`.`controleur` (`id`)
 		 ON DELETE NO ACTION
 		 ON UPDATE NO ACTION );

DROP TABLE IF EXISTS `pica_nico`.`location`;
CREATE TABLE `pica_nico`.`location` (
  `id` VARCHAR(4) NOT NULL,
  `sejour` VARCHAR(4) NOT NULL,
  `immat` VARCHAR(10) NOT NULL,
  `codeC` VARCHAR(4) NOT NULL,
  `appreciation` VARCHAR(120) NULL,
  `note` INT NULL CHECK (note <= 5),
  `confirme` BOOLEAN NOT NULL,
  PRIMARY KEY (`id`, `sejour`, `immat`, `codeC`),
   INDEX `F_loc1_idx` (`id` ASC),
   INDEX `F_loc2_idx` (`sejour` ASC),
   INDEX `F_loc3_idx` (`immat` ASC),
   INDEX `F_loc4_idx` (`codeC` ASC),
   CONSTRAINT `sejour_location` FOREIGN KEY (`sejour`)
		REFERENCES `pica_nico`.`sejour` (`id`)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
  CONSTRAINT `immat_location` FOREIGN KEY (`immat`)
		REFERENCES `pica_nico`.`voiture` (`immat`)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
  CONSTRAINT `codeC_location` FOREIGN KEY (`codeC`)
    REFERENCES `pica_nico`.`client` (`codeC`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION  );
