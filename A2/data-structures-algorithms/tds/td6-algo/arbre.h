#ifndef ARBRE_H
#define ARBRE_H

#include <stdbool.h>

//----------------------------------------------------------
// Définition d'un (nœud d'un) arbre binaire
//----------------------------------------------------------

// Un élément de l'arbre (on parle de « nœud »)
struct _noeud
{
	int valeur;  // valeur contenu dans le nœud
	struct _noeud * fils_gauche; // accès au fils à gauche
	struct _noeud * fils_droite; // accès au fils à droite
};

// Alias
typedef  struct _noeud  noeud;


/*
// ou bien :
typedef struct _noeud
{
	int valeur;
	struct _noeud * fils_gauche;
	struct _noeud * fils_droite;
} noeud;
*/


//------------------------------------------------------------------------------
//-- prototypes des fonctions pour la manipulation d'arbres binaires
//------------------------------------------------------------------------------

// Exo 1 - Mise en place d'un arbre

noeud* creer_noeud(int valeur);
bool associer_fils_gauche(noeud * parent, noeud * enfant);
bool associer_fils_droite(noeud * parent, noeud * enfant);
bool est_feuille(noeud const * element);


// À DÉCOMMENTER AU FUR ET À MESURE DE L'AVANCÉE DU TD (et à implémenter dans "arbre.c")

// Exo 2 - Les parcours en profondeur + affichage

void affichage_prefixe(noeud const * arbre);
void affichage_infixe(noeud const * arbre);
void affichage_postfixe(noeud const * arbre);


// Exo 3 - Afficher en arborescence un arbre binaire

void afficherOffset(int offset);
void affichage_arborescence(noeud const * arbre, int offset);


// Exo 4 - Nombre de fils, de descendants et de feuilles

int nombre_enfants(noeud const * parent);
int nombre_descendants(noeud const * parent);
int nombre_feuilles(noeud const * parent);


// Exo 5 - Hauteur

int hauteur(noeud const * arbre);
int max(int a, int b);
int hauteurArbre(noeud const * arbre);

// Exo 6 - Suppression 

void supprimer(noeud ** p_arbre);

#endif
