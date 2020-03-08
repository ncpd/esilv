#ifndef LABYRINTHE_H
#define LABYRINTHE_H

#include <stdbool.h>

typedef struct
{
	unsigned int nbLignes;
	unsigned int nbColonnes;
	char ** matrice;
} labyrinthe;

labyrinthe * chargerLaby(const char * nom_fichier);

void afficherLaby(labyrinthe const * laby);

bool estUnMur(char * c);
bool estArrivee(char * c);

bool isSurroundedBy(labyrinthe const * laby);

#endif