/* Liste des clients (par numéro de client) */
SELECT * FROM client ORDER BY codeC;

/* Saisie d'un nouveau client */
INSERT INTO `pfr`.`client` (`codeC`, `nom`, `prenom`, `adresse`, `telephone`, `email`) VALUES ('C7', 'Lanvin', 'Gérard', '3 rue Jean Baptiste Drapier 95300 PONTOISE', '0139198413', 'gerard.lanvin@gmail.com');

/* Liste des voitures, de leur position et de leur disponibilité */
SELECT immat, id_parking, place_parking, disponibilite FROM voiture ORDER BY id_parking, place_parking;

/* Sélection d'une voiture disponible dans un arrondissement */
SELECT immat, p.nom, p.adresse, p.ville, v.place_parking FROM voiture v INNER JOIN parking p ON v.id_parking = p.id WHERE p.code_postal LIKE '%015';

/* Requête de mise à jour de la place de parking d'un véhicule identifié par son immatriculation */
UPDATE voiture SET id_parking = 'P1', place_parking = 'A9' WHERE immat = '474SRC75';

/* Combien d'opérations de maintenance sur une voiture identifiée par son immatriculation */
SELECT immat, COUNT(*) FROM intervention WHERE immat = '474SRC75';

/* Enregistrement du retour d'une voiture */
UPDATE voiture SET disponibilite = true, motif_indisponibilite = NULL WHERE immat = '474SRC75';

/* Nombre de voitures contrôlées par chacun des contrôleurs */
SELECT id_controleur, COUNT(*) as nb_voitures_controlees FROM voiture GROUP BY id_controleur;

/* Liste des voitures indisponibles et du motif correspondant */
SELECT immat, motif_indisponibilite FROM voiture WHERE disponibilite = false;

/* Enregistrement d'une opération de maintenance par un des contrôleurs sur une voiture identifiée par son immatriculation */
INSERT INTO `pfr`.`intervention` (`id`, `immat`, `id_controleur`, `type_intervention`, `date`) VALUES ('I4', '221EKO75', 'CL2', 'contrôle technique', '2018-03-23');
