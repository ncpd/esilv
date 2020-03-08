/* Exercice 1 */
select * from voiture, proprietaire;
select * from voiture natural join proprietaire;

/* Exercice 2 */
select pseudo from proprietaire natural join voiture where immat = '56AA46';
/* Jules */
select count(*) from location natural join voiture where marque = 'Peugeot' and modele = '205';
/* 3 */
select marque, modele from voiture natural join location where mois = '4' and annee = '2015';
select immat from voiture natural join proprietaire where pseudo = 'Marcel';
select immat from voiture inner join proprietaire on voiture.codeP = proprietaire.codeP where pseudo = 'Marcel';
/* 3eme manière */
select immat from voiture natural join proprietaire where email = 'bozo@gmail.com';
select immat from voiture natural join proprietaire where email in('bozo@gmail.com');
/* 42RS75, 42RL75 */
select nom from client inner join location on location.codeC = client.codeC inner join voiture on location.immat = voiture.immat inner join proprietaire on proprietaire.codeP = voiture.codeP where proprietaire.pseudo = 'Marcel';
/* Delon Auteuil */
select * from voiture v inner join location l on l.immat = v.immat inner join client c on c.codeC = l.codeC where c.nom = 'Juniot' and v.categorie = 'cabriolet';
/* 75AZ92 - Peugeot 205 */
select nom, prenom from client c inner join location l on l.codeC = c.codeC where l.villeA <> l.villeD;
/* Boon, Richard Pierre */
select nom, prenom from client c inner join location l on l.codeC = c.codeC inner join voiture v on v.immat = l.immat inner join proprietaire p on p.codeP = v.codeP where l.villeA = p.ville;
select * from proprietaire p inner join client c on c.ville = p.ville and c.nom = 'Delon';
/* Germain, Emile, Marcel */
select count(*) from location l inner join client c on c.codeC = l.codeC where c.nom = 'Delon';
/* 2 locs */
select count(*) from location l inner join voiture v on v.immat = l.immat inner join proprietaire p on v.codeP = p.codeP where p.pseudo = 'Jules';

/* EXERCICE 3 */
