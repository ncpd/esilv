/* ========== INSERTION DANS LA TABLE PARKING ========== */

INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P1', 'Rivoli', '2 Rue Boucher', '75001', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P2', 'Rivoli', '2 Rue Boucher', '75002', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P3', 'Beaubourg', '31 Rue Beaubourg', '75003', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P4', 'Lobau', '4 Rue Lobau', '75004', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P5', 'Soufflot', '22 Rue Soufflot', '75005', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P6', 'Jardin des Plantes', '25 Rue Geoffroy-Saint-Hilaire', '75006', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P7', 'Maubourg', '45 Quai d''Orsay', '75007', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P8', 'Champs-Elysées', '77 Avenue Marceau', '75008', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P9', 'Pigalle', '10 Rue Jean-Baptiste Pigalle', '75009', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P10', 'Lariboisière', '1 bis Rue Ambroise Paré', '75010', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P11', 'Oberkampf', '11 Rue Ternaux', '75011', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P12', 'Gare de Lyon', '6 Rue de Rambouillet', '75012', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P13', 'Italie', '25 Rue Stephen Pichon', '75013', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P14', 'Raspail', '120 Boulevard du Montparnasse', '75014', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P15', 'Beaugrenelle', '5 Quai Andre Citroen', '75015', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P16', 'Victor Hugo', '74 Avenue Victor Hugo', '75016', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P17', 'Ternes', '38 Avenue des Ternes', '75017', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P18', 'Stalingrad', '13 Rue d''Aubervilliers', '75018', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P19', 'Philharmonie', '185 Boulevard Sérurier', '75019', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P20', 'Rosa Parks', '157 Boulevard Macdonald', '75020', 'Paris');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P21', 'Orly', 'Orly Airport', '94310', 'Orly');
INSERT INTO `pica_nico`.`parking` (`id`, `nom`, `adresse`, `code_postal`, `ville`) VALUES ('P22', 'Roissy', 'Roissy Airport', '95700', 'Roissy en France');

/* ========== INSERTION DANS LA TABLE CONTROLEUR ========== */

INSERT INTO `pica_nico`.`controleur` (`id`, `nom`, `prenom`) VALUES ('CL1', 'Doiron', 'Rémy');
INSERT INTO `pica_nico`.`controleur` (`id`, `nom`, `prenom`) VALUES ('CL2', 'Simard', 'Benjamin');
INSERT INTO `pica_nico`.`controleur` (`id`, `nom`, `prenom`) VALUES ('CL3', 'Leroux', 'Thierry');

/* ========== INSERTION DANS LA TABLE CLIENT ========== */

INSERT INTO `pica_nico`.`client` (`codeC`, `nom`, `prenom`, `adresse`, `telephone`, `email`) VALUES ('C001', 'Lagueux', 'Isabelle', '87 Rue Hubert de Lisle 33310 LORMONT', '0524849895', 'isabellelagueux@wanadoo.fr');
INSERT INTO `pica_nico`.`client` (`codeC`, `nom`, `prenom`, `adresse`, `telephone`, `email`) VALUES ('C002', 'Saindon', 'Anton', '29 Avenue de l''Amandier 92270 BOIS-COLOMBES', '0112788512', 'antonsaindon@hotmail.fr');
INSERT INTO `pica_nico`.`client` (`codeC`, `nom`, `prenom`, `adresse`, `telephone`, `email`) VALUES ('C003', 'Martinez', 'Robert E.', '794 Willow Greene Drive Andalusia AL 36420', '334-222-0744', 'robertemartinez@gmail.com');
INSERT INTO `pica_nico`.`client` (`codeC`, `nom`, `prenom`, `adresse`, `telephone`, `email`) VALUES ('C004', 'Metzger', 'Lukas ', 'Borstelmannsweg 7 92239 Hirschau', '09608538975', 'lukasmetzger@yahoo.de');
INSERT INTO `pica_nico`.`client` (`codeC`, `nom`, `prenom`, `adresse`, `telephone`, `email`) VALUES ('C005', 'Whitehouse', 'Adam', '12 Harrogate Road RUSHWICK WR26TX', '07038185146', 'adamwhitehouse@gmail.com');
INSERT INTO `pica_nico`.`client` (`codeC`, `nom`, `prenom`, `adresse`, `telephone`, `email`) VALUES ('C006', 'Pavlicek', 'Lukas ', 'Alsova 1350 582 22 Pribyslav', '566253204', 'lukaspavlicek@gmail.com');

/* ========== INSERTION DANS LA TABLE SEJOUR ========== */

INSERT INTO `pica_nico`.`sejour` (`id`, `theme`, `date`, `annee`) VALUES ('S001', '04', '21', 2018);
INSERT INTO `pica_nico`.`sejour` (`id`, `theme`, `date`, `annee`) VALUES ('S002', '11', '38', 2018);
INSERT INTO `pica_nico`.`sejour` (`id`, `theme`, `date`, `annee`) VALUES ('S003', '16', '46', 2016);
INSERT INTO `pica_nico`.`sejour` (`id`, `theme`, `date`, `annee`) VALUES ('S004', '18', '51', 2017);

/* ========== INSERTION DANS LA TABLE VOITURE ========== */
/* CABRIOLETS 2 PLACES */
INSERT INTO `pica_nico`.`voiture` VALUES ('405DFG75', 'Audi', 'TT', 'CL1', 'cabriolet', '2', true, 75, null, 'P1', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('222HUY75', 'BMW', 'Z4', 'CL1', 'cabriolet', '2', true, 82, null, 'P2', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('261MDU75', 'Chrysler', 'Crossfire', 'CL1', 'cabriolet', '2', true, 85, null, 'P3', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('420FDH75', 'Corvette', 'C6', 'CL1', 'cabriolet', '2', true, 90, null, 'P4', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('317KDU75', 'Ferrari', 'F430 Spider', 'CL1', 'cabriolet', '2', true, 100, null, 'P5', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('248QCH75', 'Fiat', 'Barchetta', 'CL1', 'cabriolet', '2', true, 86, null, 'P6', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('409MBU75', 'Ford', 'Streetka', 'CL1', 'cabriolet', '2', true, 79, null, 'P7', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('468NBV75', 'Honda', 'S2000', 'CL1', 'cabriolet', '2', true, 78, null, 'P8', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('494MQT75', 'Lotus', 'Elise', 'CL1', 'cabriolet', '2', true, 81, null, 'P9', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('114ACO75', 'Maserati', 'Spider', 'CL1', 'cabriolet', '2', true, 89, null, 'P10', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('223JDI75', 'Mazda', 'MX5', 'CL1', 'cabriolet', '2', true, 92, null, 'P11', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('484API75', 'MG', 'TF', 'CL1', 'cabriolet', '2', true, 74, null, 'P12', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('385ZER75', 'Nissan', '350Z', 'CL1', 'cabriolet', '2', true, 87, null, 'P13', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('425YTG75', 'Porsche', 'Boxster', 'CL1', 'cabriolet', '2', true, 79, null, 'P14', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('376OSU75', 'Smart', 'Fortwo Cabrio', 'CL2', 'cabriolet', '2', true, 65, null, 'P15', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('474SRC75', 'Fiat', '500C', 'CL2', 'cabriolet', '2', true, 86, null, 'P16', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('172DJI75', 'Peugeot', '207 CC', 'CL2', 'cabriolet', '2', true, 76, null, 'P21', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('256RYU75', 'Audi', 'A5', 'CL2', 'cabriolet', '2', true, 87, null, 'P21', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('304TZO75', 'Audi', 'A5', 'CL2', 'cabriolet', '2', true, 87, null, 'P22', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('188SIJ75', 'Mercedes', 'Classe E', 'CL2', 'cabriolet', '2', true, 95, null, 'P22', 'A1');

/* BERLINES 4 PLACES */

INSERT INTO `pica_nico`.`voiture`  VALUES ('336DJO75', 'Dacia', 'Logan', 'CL2', 'berline', '4', true, 74, null, 'P17', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('197MQS75', 'Dacia', 'Sandero', 'CL2', 'berline', '4', true, 76, null, 'P18', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('341GEJ75', 'Fiat', 'Tipo', 'CL2', 'berline', '4', true, 82, null, 'P19', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('343LSD75', 'Suzuki', 'Baleno', 'CL2', 'berline', '4', true, 73, null, 'P20', 'A0');
INSERT INTO `pica_nico`.`voiture`  VALUES ('396EIK75', 'Citroen', 'C4 Cactus', 'CL2', 'berline', '4', true, 71, null, 'P1', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('371EIL75', 'Kia', 'Cee''d', 'CL2', 'berline', '4', true, 66, null, 'P2', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('221EKO75', 'Ford', 'Focus', 'CL2', 'berline', '4', true, 68, null, 'P3', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('109PKZ75', 'Skoda', 'Octavia', 'CL3', 'berline', '4', true, 72, null, 'P4', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('294SIY75', 'Nissan', 'Pulsar', 'CL3', 'berline', '4', true, 76, null, 'P5', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('376DHU75', 'Volkswagen', 'Golf', 'CL3', 'berline', '4', true, 64, null, 'P6', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('398XNI75', 'Renault', 'Megane', 'CL3', 'berline', '4', true, 63, null, 'P7', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('379SID75', 'Toyota', 'Auris', 'CL3', 'berline', '4', true, 62, null, 'P8', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('457SIM75', 'Opel', 'Astra', 'CL3', 'berline', '4', true, 64, null, 'P10', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('183QUJ75', 'Seat', 'Leon', 'CL3', 'berline', '4', true, 66, null, 'P11', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('305QOK75', 'Hyundai', 'I30', 'CL3', 'berline', '4', true, 67, null, 'P12', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('385FPO75', 'Peugeot', '308', 'CL3', 'berline', '4', true, 70, null, 'P13', 'A1');
INSERT INTO `pica_nico`.`voiture`  VALUES ('349PLS75', 'Mini', 'Clubman', 'CL3', 'berline', '4', true, 75, null, 'P21', 'A2');
INSERT INTO `pica_nico`.`voiture`  VALUES ('440DSJ75', 'Volvo', 'V40', 'CL3', 'berline', '4', true, 69, null, 'P21', 'A3');
INSERT INTO `pica_nico`.`voiture`  VALUES ('132SOK75', 'Alfa Romeo', 'Giulietta', 'CL3', 'berline', '4', true, 72, null, 'P22', 'A2');
INSERT INTO `pica_nico`.`voiture`  VALUES ('388AEO75', 'Subaru', 'Impreza', 'CL3', 'berline', '4', true, 68, null, 'P22', 'A3');

/* ========== INSERTION DANS LA TABLE INTERVENTION ========== */
INSERT INTO `pica_nico`.`intervention` (`id`, `immat`, `id_controleur`, `type_intervention`, `date`) VALUES ('I001', '474SRC75', 'CL1', 'regonflage des pneus', '2018-03-22');
INSERT INTO `pica_nico`.`intervention` (`id`, `immat`, `id_controleur`, `type_intervention`, `date`) VALUES ('I002', '474SRC75', 'CL1', 'plein d''essence', '2018-03-22');
INSERT INTO `pica_nico`.`intervention` (`id`, `immat`, `id_controleur`, `type_intervention`, `date`) VALUES ('I003', '474SRC75', 'CL1', 'nettoyage intérieur', '2018-03-22');

/* ========== INSERTION DANS LA TABLE LOCATION ========== */
INSERT INTO `pica_nico`.`location` (`id`, `sejour`, `immat`, `codeC`, `appreciation`, `note`, `confirme`) VALUES ('L001', 'S001', '474SRC75', 'C001', 'Génial !', 5, 1);
INSERT INTO `pica_nico`.`location` (`id`, `sejour`, `immat`, `codeC`, `appreciation`, `note`, `confirme`) VALUES ('L002', 'S003', '474SRC75', 'C002', 'Bon service', 3, 1);
INSERT INTO `pica_nico`.`location` (`id`, `sejour`, `immat`, `codeC`, `appreciation`, `note`, `confirme`) VALUES ('L003', 'S004', '294SIY75', 'C003', 'Bien mais dommage qu''on ne puisse pas louer plus qu''un weekend', 4, 1);
INSERT INTO `pica_nico`.`location` (`id`, `sejour`, `immat`, `codeC`, `appreciation`, `note`, `confirme`) VALUES ('L004', 'S002', '294SIY75', 'C004', 'A revoir', 3, 1);
