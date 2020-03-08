#include "matrice.h"
#include <stdlib.h>

matrice* allouer_matrice(int nbLig, int nbCol)
{
	matrice * mat = (matrice*) malloc(sizeof(matrice));
	if(mat != NULL) {
		mat->nb_lignes = nbLig;
		mat->nb_colonnes = nbCol;
		mat->contenu = (double*) malloc((nbLig*nbCol)*sizeof(double));
	}
	return mat;
}

void positionner_element(double valeur, matrice* p_matrice, int ligne, int colonne)
{
	if(ligne <= p_matrice->nb_lignes && colonne <= p_matrice->nb_colonnes) {
		int i = colonne + (ligne * p_matrice->nb_colonnes);
		p_matrice->contenu[i] = valeur;
	}
}

double recuperer_element(matrice* p_matrice, int ligne, int colonne)
{
	double elem = -123456.0;
	if(ligne <= p_matrice->nb_lignes && colonne <= p_matrice->nb_colonnes) {
		int i = colonne + (ligne * p_matrice->nb_colonnes);
		elem = p_matrice->contenu[i];
	}
	return elem;
}

void affecter_matrice(matrice* p_matrice, double tab[])
{
	if(p_matrice != NULL && tab != NULL) {
		int i = 0;
		while(i < p_matrice->nb_lignes * p_matrice->nb_colonnes) {
			p_matrice->contenu[i] = tab[i];
			i++;
		}
	}
}

void detruire_matrice(matrice* mat)
{
	if(mat != NULL) {
		free(mat->contenu);
		mat->contenu = NULL;
		free(mat);
		mat = NULL;
	}
}