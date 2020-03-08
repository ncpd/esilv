#ifndef VECTEUR_H
#define VECTEUR_H

#include <stdbool.h>
#include "fonctionnaire.h"

/*
 * Définition du type structuré vecteur
 * Une taille (le nombre de fonctionnaires)
 * Un contenu composé de fonctionnaires
 */
typedef struct
{
	int tailleLogique;
	fonctionnaire * vec_fonctionnaires;
} vecteur;

/*
 * Fonction qui crée dynamiquement un vecteur dont le contenu est vide
 * Retour : pointeur sur vecteur
 */
vecteur * allouer_vecteur();

/*
 * Fonction qui vérifie si le vecteur est NULL (ou sans éléments)
 * Retour : booléen qui indique si le vecteur est NULL/vide
 */
bool isNullOrEmpty(vecteur const * v);

/*
 * Fonction qui vérifie si le vecteur a un conflit (ie. 2 ou plus fonctionnaires)
 * Retour : booléen qui indique si le vecteur possède un conflit
 */
bool haveConflict(vecteur const * v);

/*
 * Fonction qu trie alphabétiquement selon le nom un vecteur de fonctionnaires
 */
void triAlpha(fonctionnaire * vecFonctionnaires, int nbFonc);

/*
 * Fonction qui ajoute un fonctionnaire au vecteur
 * Retour : booléen qui assure que l'ajout s'est bien passé
 */
bool ajouterFonctionnaire(vecteur * v, char * nom, char * prenom, int salaire);

/*
 * Fonction qui supprime un fonctionnaire du vecteur
 * Retour : booléen qui assure que la suppression s'est bien passée
 */
bool supprimerFonctionnaire(vecteur * v, char * nom, char * prenom);

/*
 * Fonction qui vérifie si le fonctionnaire existe dans le vecteur
 * Retour : booléen qui indique si le fonctionnaire existe ou non
 */
bool fonctionnaireExists(vecteur * vec, fonctionnaire * fonc);

#endif