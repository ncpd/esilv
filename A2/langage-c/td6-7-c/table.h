#ifndef TABLE_H
#define TABLE_H

#include <stdbool.h>
#include "vecteur.h"
#include "fonctionnaire.h"

/*
 * Définition du type structuré table
 * Une taille (un nombre de vecteurs)
 * Un contenu composé de (n = taille) vecteurs
 */
typedef struct 
{
	int taille;
	vecteur * _table;
} table;

/*
 * Fonction qui alloue dynamiquement une table de vecteurs
 * Retour : pointeur sur la table
 */
table* allouer_table(int taille);

/*
 * Fonction qui calcule la puissance d'un nombre
 * Retour : nombre élevé à la puissance pow
 */
long long puissance(long long number, int pow);

/*
 * Fonction qui calcule le modulo d'un nombre
 * Retour : nombre modulo diviseur
 */
long long modulo(long long number, long long diviseur);

/*
 * Fonction qui formate le nom de la personne de la bonne manière (en majuscules)
 * Retour : chaîne de caractères modifiée
 */
char * formatNom(char * nom);

/*
 * Fonction qui formate le prenom de la personne de la bonne manière
 * (Majuscule à l'initiale et après un underscore, le reste en minuscules)
 * Retour : chaîne de caractères modifiée
 */
char * formatPrenom(char * prenom);

/*
 * Fonction qui concatène les 4 premières lettres du nom 
 * et les 2 premières lettres du prénom du fonctionnaire
 * Retour : Chaînes de caractères concaténée en une seule
 */
char * concat(const char * nom, const char * prenom);

/*
 * Fonction qui calcule l'index d'un fonctionnaire compte tenu de son nom, de son prénom,
 * de la base et de la taille de la table
 * Retour : index du fonctionnaire
 */
int indexFonctionnaire(char * nom, char * prenom, int base, int taille);

/*
 * Fonction qui ajoute un fonctionnaire à la table
 * Retour : booléen qui assure que l'ajout a bien été effectué
 */
bool addToTable(char * nom, char * prenom, int salaire, unsigned long base, table * tab, int taille);

/*
 * Fonction qui supprime un utilisateur de la table
 * Retour : booléen qui assure que la suppression a bien été effectuée
 */
bool deleteFromTable(char * nom, char * prenom, unsigned long base, table * tabl, int taille);

#endif