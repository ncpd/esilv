#include "arbreRecherche.h"
#include <stdlib.h>

noeud * creer_noeud(int valeur)
{
	noeud * node = malloc(sizeof(noeud));
	if(node != NULL) {
		node->valeur = valeur;
		node->fils_droite = NULL;
		node->fils_gauche = NULL;
		node->parent = NULL;
	}
	return node;
}

void insertion(noeud ** p_arbre, int valeur)
{
	_insertion(p_arbre, valeur, NULL);
}

void _insertion(noeud ** p_arbre, int valeur, noeud * parent)
{
	if(*p_arbre == NULL) {
		(*p_arbre) = (noeud*) malloc(sizeof(noeud));
		(*p_arbre)->valeur = valeur;
		(*p_arbre)->fils_gauche = NULL;
		(*p_arbre)->fils_droite = NULL;
		(*p_arbre)->parent = parent;
	} else {
		if((*p_arbre)->valeur > valeur) {
			_insertion(&(*p_arbre)->fils_droite, valeur, *p_arbre);
		}
		if((*p_arbre)->valeur < valeur) {
			_insertion(&(*p_arbre)->fils_gauche, valeur, *p_arbre);
		}
	}
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

 noeud * recherche(noeud * arbre, int val)
 {
	 if(arbre == NULL) {
		 return NULL;
	 } else {
		 if(arbre->valeur == val) {
			 return arbre;
		 } else if(arbre->valeur > val) {
			 recherche(arbre->fils_gauche, val);
		 } else if(arbre->valeur < val) {
			 recherche(arbre->fils_droite, val);
		 }
	 }
 }
 
 noeud * suivant(noeud const * arbre)
 {
	 
 }