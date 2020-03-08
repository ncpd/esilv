#include <stdio.h>
#include "matrice.h"

void afficherMatrice(matrice* mat)
{
	if(mat != NULL) {
		for(int i = 0; i < mat->nb_lignes; i++) {
			for(int j = 0; j < mat->nb_colonnes; j++) {
				printf("%lg ", recuperer_element(mat, i, j));
			}
			printf("\n");
		}
	}
}

void exo1()
{
	matrice * mat = allouer_matrice(2, 3);
	printf("Lignes : %d\n", mat->nb_lignes);
	printf("Colonnes : %d\n", mat->nb_colonnes);
	positionner_element(1.0, mat, 0, 0);
	positionner_element(2.0, mat, 0, 1);
	positionner_element(3.0, mat, 0, 2);
	positionner_element(4.0, mat, 1, 0);
	positionner_element(5.0, mat, 1, 1);
	positionner_element(6.0, mat, 1, 2);
	printf("Contenu :\n");
	afficherMatrice(mat);
	double tab[6] = {1.5, 2.5, 3.5, 4.5, 5.5, 6.5};
	affecter_matrice(mat, tab);
	printf("Contenu :\n");
	afficherMatrice(mat);
	printf("%p\n",mat);
	detruire_matrice(mat);
}

int main(int argc, char **argv)
{
	exo1();
	return 0;
}
