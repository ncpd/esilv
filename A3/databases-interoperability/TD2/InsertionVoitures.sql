INSERT INTO `loueur`.`voiture` (`immat`, `modele`, `marque`, `categorie`, `couleur`, `places`, `achatA`, `compteur`,`prixJ`, `codeP`) VALUES ('10AK37', 'Peugeot', '205', 'cabriolet', 'violet', 2, '1980', 65000, 40, 'P99');
INSERT INTO `loueur`.`voiture` (`immat`, `modele`, `marque`, `categorie`, `couleur`, `places`, `achatA`, `compteur`,`prixJ`, `codeP`) VALUES ('11BF37', 'Peugeot', '206', 'cabriolet', 'violet', 2, '1980', 21000, 32, 'P99');
INSERT INTO `loueur`.`voiture` (`immat`, `modele`, `marque`, `categorie`, `couleur`, `places`, `achatA`, `compteur`,`prixJ`, `codeP`) VALUES ('15AK37', 'Peugeot', '205', 'cabriolet', 'violet', 2, '1985', 27000, 30, 'P99');

select * from voiture;
select count(*) from voiture where couleur = 'violet';
update voiture set couleur = 'blanc' where immat = '11BF37';

select * from voiture where couleur = 'violet';
delete from voiture where couleur = 'violet';

desc voiture;
INSERT INTO `loueur`.`voiture` (`immat`, `modele`, `marque`, `categorie`, `couleur`, `places`, `achatA`, `compteur`,`prixJ`, `codeP`) VALUES ('63GH94', 'Renault', 'Megane', 'berline', 'rouge', 4, '2012', 750, 40, 'P99');
INSERT INTO `loueur`.`voiture` (`immat`, `modele`, `marque`, `categorie`, `couleur`, `places`, `achatA`, `compteur`,`prixJ`, `codeP`) VALUES ('87AZ92', 'Renault', 'Clio', 'citadine', 'vert', 4, '1999', 61200, 40, 'P99');

ALTER TABLE location ADD COLUMN (note INT NULL CHECK (note >= 0 AND note <= 5), appreciation VARCHAR(120) NULL);
desc location;

create table `loueur`.`maintenance` (
	`dateIntervention` date NOT NULL,
    `vehiculeConcerne` VARCHAR(10) NOT NULL,
    `descriptionIntervention` VARCHAR(120) NOT NULL,
    PRIMARY KEY(`vehiculeConcerne`),
    CONSTRAINT `vehiculeConcerne` FOREIGN KEY (`vehiculeConcerne`)
		REFERENCES `loueur`.`voiture` (`immat`)
		ON DELETE CASCADE
		ON UPDATE NO ACTION);
        
select * from client where nom = 'Delon' and prenom = 'Alain';
