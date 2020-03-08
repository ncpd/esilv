#include "labyrinthe.h"
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

labyrinthe * chargerLaby(const char * nom_fichier)
{
	FILE* fichier = fopen(nom_fichier,"r");
	if(fichier != NULL) {
		unsigned int nblig;
		unsigned int nbcol;
		fscanf(fichier, "%u %u\n", &nblig, &nbcol);
		char** mat = (char**) malloc(nblig * sizeof(char*));
		for(int ligne = 0 ; ligne < nblig ; ligne++)
		{
			// allocation des colonnes de la matrice (pour chaque ligne)
			mat[ligne] = (char*) malloc(nbcol * sizeof(char));
			for(int col = 0 ; col < nbcol ; col++)
			{
				mat[ligne][col] = fgetc(fichier);
			}
			fgetc(fichier); // lecture du retour chariot ('\n'). Valeur ignorée => passage à la ligne suivante
		}
		labyrinthe * laby = (labyrinthe*) malloc(sizeof(labyrinthe));
		laby->nbColonnes = nbcol;
		laby->nbLignes = nblig;
		laby->matrice = mat;
		
		fclose(fichier);
		return laby;
	} else {
	printf("Echec du chargement - fichier introuvable.\n");
	}
}

void afficherLaby(labyrinthe const * laby)
{
	if(laby != NULL) {
		for(int lig = 0; lig < laby->nbLignes; lig++) {
			for(int col = 0; col < laby->nbColonnes; col++) {
				if(estUnMur(&(laby->matrice[lig][col]))) {
					printf("#");
				} else if(estArrivee(&(laby->matrice[lig][col]))) {
					printf(".");
				} else {
					printf(" ");
				}
			}
			printf("\n");
		}
	}
}

bool estUnMur(char * c)
{
	bool isAWall = false;
	if(*c == '*') {
		isAWall = true;
	}
	return isAWall;
}

bool estArrivee(char * c)
{
	bool isFinish = false;
	if(*c == 'A') {
		isFinish = true;
	}
	return isFinish;
}

bool isSurroundedByWalls(labyrinthe const * laby)
{
	if(laby != NULL) {
		for(int lig = 0; lig < laby->nbLignes; lig++) {
			for(int col = 0; col < laby->nbColonnes; col++) {
				if(estUnMur(&(laby->matrice[lig][col]))) {
					
				}
			}
		}
	}
}