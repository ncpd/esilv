#include <stdbool.h>
#include <stdlib.h>
#include "arbre.h"

noeud * creer_noeud(int valeur)
{
	noeud * node = malloc(sizeof(noeud));
	if(node != NULL) {
		node->valeur = valeur;
		node->fils_droite = NULL;
		node->fils_gauche = NULL;
	}
	return node;
}

int max(int a, int b)
{
	return (a > b) ? a : b;
}

int hauteurArbre(noeud const * arbre)
{
	int hg = (arbre->fils_gauche == NULL) ? 0 : hauteurArbre(arbre->fils_gauche);
	int hd = (arbre->fils_droite == NULL) ? 0 : hauteurArbre(arbre->fils_droite);
	int h = 1 + max(hg,hd);
	return h;
}

bool associer_fils_gauche(noeud * parent, noeud * enfant)
{
	bool b = false;
	if(parent != NULL && enfant != NULL) {
		if(parent->fils_gauche == NULL) {
			parent->fils_gauche = enfant;
			b = true;
		}
	}
	return b;
}

bool associer_fils_droite(noeud * parent, noeud * enfant)
{
	bool b = false;
	if(parent != NULL && enfant != NULL) {
		if(parent->fils_droite == NULL) {
			parent->fils_droite = enfant;
			b = true;
		}
	}
	return b;
}

bool est_feuille(noeud const * element)
{
	bool b = false;
	if(element != NULL) {
		if(element->fils_gauche == NULL && element->fils_droite == NULL) {
			b = true;
		}
	}
	return b;
}

void affichage_prefixe(noeud const * arbre)
{
	if(arbre != NULL) {
		printf("| %d ", arbre->valeur);
		if(arbre->fils_gauche != NULL) {
			affichage_prefixe(arbre->fils_gauche);
		}
		if(arbre->fils_droite != NULL) {
			affichage_prefixe(arbre->fils_droite);
		}
	}
}

void affichage_infixe(noeud const * arbre)
{
	if(arbre != NULL) {
		if(arbre->fils_gauche != NULL) {
			affichage_infixe(arbre->fils_gauche);
		}
		printf("| %d ", arbre->valeur);
		if(arbre->fils_droite != NULL) {
			affichage_infixe(arbre->fils_droite);
		}
	}
}

void affichage_postfixe(noeud const * arbre)
{
	if(arbre != NULL) {
		if(arbre->fils_gauche != NULL) {
			affichage_postfixe(arbre->fils_gauche);
		}
		if(arbre->fils_droite != NULL) {
			affichage_postfixe(arbre->fils_droite);
		}
		printf("| %d ", arbre->valeur);
	}
}

void afficherOffset(int offset)
{
	while(offset > 0) {
		printf(" ");
		offset--;
	}
	printf("|-");
}

void affichage_arborescence(noeud const * arbre, int offset)
{
	if(arbre != NULL) {
		printf("%d\n  ", arbre->valeur);
		if(arbre->fils_gauche != NULL) {
			afficherOffset(offset);
			affichage_arborescence(arbre->fils_gauche, offset + 2);
		}
		if(arbre->fils_droite != NULL) {
			afficherOffset(offset);
			affichage_arborescence(arbre->fils_droite, offset + 2);
		}
	}
}

int nombre_enfants(noeud const * parent)
{
	int fg = (parent->fils_gauche != NULL) ? 1 : 0;
	int fd = (parent->fils_droite != NULL) ? 1 : 0;
	return fg + fd;
}

int nombre_descendants(noeud const * parent)
{
	if(parent != NULL) {
		if(nombre_enfants(parent) )
		nombre_enfants(parent->fils_gauche);
	}
}