#ifndef FONCTIONNAIRE_H
#define FONCTIONNAIRE_H

#include <stdbool.h>

/*
 * Définition du type structuré fonctionnaire
 */
typedef struct
{
	char * nom;
	char * prenom;
	int salaire;
} fonctionnaire;

/*
 * Fonction qui crée dynamiquement un fonctionnaire et lui affecte un nom, un prénom, ainsi qu'un salaire
 * Retour : pointeur sur le nouveau fonctionnaire
 */
fonctionnaire * allouer_fonctionnaire(char const * nom, char const * prenom, int salaire);

/*
 * Fonction qui vérifie si deux fonctionnaires sont égaux selon le nom et le prénom
 * Retour : booléen qui indique si les deux fonctionnaires sont égaux ou non
 */
bool equals(fonctionnaire * f1, fonctionnaire * f2);

#endif