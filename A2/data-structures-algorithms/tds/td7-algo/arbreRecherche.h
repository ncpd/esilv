#ifndef ARBRE_RECHERCHE
#define ARBRE_RECHERCHE

struct _noeud
{
	int valeur;  // valeur contenu dans le nœud
	struct _noeud * parent;
	struct _noeud * fils_gauche; // accès au fils à gauche
	struct _noeud * fils_droite; // accès au fils à droite
};

typedef  struct _noeud  noeud;

noeud * creer_noeud(int valeur);

/* -----------------------------------------------------------------
 * ------              Exercice 1 : Insertion                 ------
 * -----------------------------------------------------------------
 */
void insertion(noeud ** p_arbre, int valeur);
void _insertion(noeud ** p_arbre, int valeur, noeud * parent);

void affichage_prefixe(noeud const * arbre);
void affichage_infixe(noeud const * arbre);
void affichage_postfixe(noeud const * arbre);

/* -----------------------------------------------------------------
 * ------              Exercice 2 : Recherche                 ------
 * -----------------------------------------------------------------
 */
 noeud * recherche(noeud * arbre, int val);
 
/* -----------------------------------------------------------------
 * ------          Exercice 3 : Minimum / Maximum             ------
 * -----------------------------------------------------------------
 */
noeud * minimum(noeud const * arbre);
noeud * maximum(noeud const * arbre);

/* -----------------------------------------------------------------
 * ------         Exercice 4 : Suivant / Précédent            ------
 * -----------------------------------------------------------------
 */
noeud * suivant(noeud const * element);
noeud * precedent(noeud const * element);
#endif