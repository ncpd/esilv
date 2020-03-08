#include <stdio.h>
#include <stdlib.h>
#include "arbre.h"

int main(int argc, char **argv)
{
	noeud * n12 = creer_noeud(12);
	noeud * n1 = creer_noeud(1);
	noeud * n91 = creer_noeud(91);
	noeud * n67 = creer_noeud(67);
	noeud * n7 = creer_noeud(7);
	noeud * n82 = creer_noeud(82);
	noeud * n61 = creer_noeud(61);
	associer_fils_gauche(n12, n1);
	associer_fils_droite(n12, n7);
	associer_fils_gauche(n1, n91);
	associer_fils_droite(n1, n67);
	associer_fils_droite(n7, n82);
	associer_fils_gauche(n82, n61);
	/*
	printf("Affichage prefixe :\n");
	affichage_prefixe(n12);
	printf("\nAffichage infixe :\n");
	affichage_infixe(n12);
	printf("\nAffichage postfixe :\n");
	affichage_postfixe(n12);
	*/
	printf("\nAffichage en arborescence :\n");
	affichage_arborescence(n12, 0);
	int h = hauteurArbre(n12);
	printf("\nH : %d\n", h);
	//printf("\nNombre d'enfants de n61 : %d\n", nombre_enfants(n61));
	return 0;
}
