/* Exercice 1 */
select * from voiture where modele = 'T550';
/* La T550 est une Ferrari */
select * from proprietaire where ville = 'Paris';
/* Jules, Fred, Christian, Marcel */
select * from voiture where categorie = 'luxe';
/* Renault */
select * from voiture where categorie = 'cabriolet' && places = 4;
/* 32RS75 */
select categorie, count(*) from voiture group by categorie;
/* berline 3, cabriolet 5, citadine 2, familiale 5, luxe 3, premium 3, utilitaire 1 */
select count(distinct categorie) from voiture;
/* berline, cabriolet, citadine, familiale, luxe, premium, utilitaire */
select modele from voiture inner join location on location.immat = voiture.immat where marque = 'Peugeot';
/* 205 */
select pseudo from proprietaire where email like '%gmail%';
/* Germain, Emile, Lucien, Marcel, Marcel */
select compteur from voiture where immat = '75AZ92';
/* 17560 */
select * from client where ville = 'Nantes';

/* Exercice 2 */
select * from  voiture order by achatA;
select marque, modele from voiture order by marque, modele;
select marque, modele, immat, places from voiture where places >= 4 order by places desc;
select distinct ville from proprietaire order by ville asc;
select location.immat from location inner join voiture on location.immat = voiture.immat order by compteur desc;

/* Exercice 3 */
select count(*) from voiture where categorie = 'cabriolet' and (couleur = 'bleu' or couleur = 'rouge');
/* 1 */
select categorie, count(*) from voiture group by categorie;
select count(*) from voiture where ((categorie = 'familiale' and compteur <= 50000) or (categorie = 'utilitaire')) and couleur = 'noir' or couleur = 'blanc';
/* 10 */
select marque from voiture group by marque;
select immat, marque, modele from voiture where marque = 'Peugeot' or marque = 'Citroen';
select * from client where ville = 'Paris' and age >= 50 order by nom, prenom asc;
select immat from voiture where couleur = 'blanc' not in (select immat from voiture where categorie = 'cabriolet');
select count(*) from voiture where (categorie = 'luxe' or categorie = 'premium') and achatA <= 2012;
/* 6 */
select numLoc, villeD, villeA, annee, mois from location l where l.villeD = l.villeA order by annee, mois;

/* Exercice 4 */
select * from voiture where achatA between 2010 and 2012;
select * from voiture where marque in ('Peugeot', 'Renault','Citroen');
select count(*) from proprietaire where ville <> 'Paris' and ville <> 'Lyon' and ville <> 'Nantes';
select * from client where permis is null;
select client.codeC from client inner join location on location.codeC = client.codeC group by client.codeC;
select codeC from client where permis is not null;
select client.codeC from client inner join location on location.codeC = client.codeC where client.permis is null;
select * from client inner join proprietaire on proprietaire.ville = client.ville group by codeC;
select voiture.immat from voiture inner join location on voiture.immat = location.immat group by immat;

select voiture.immat from voiture inner join location on voiture.immat <> location.immat group by immat;
/* 2eme ecriture */

/* Exercice 5 */
select numLoc, immat, max(duree) from location;
select avg(age) from client;
select codeC, age, avg(age) as moy from client where age <= 'moy';