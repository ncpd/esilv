#ifndef MATRICE_H
#define MATRICE_H

//******************************************************************************
// Le type 'matrice' contenant des réels (de type double) :
//******************************************************************************

struct _matrice
{
	int nb_lignes;
	int nb_colonnes;
	double* contenu;
}; // ne pas oublier le ; ici

typedef  struct _matrice  matrice;

/* 
	ICI la matrice est représentée dans un tableau, où les lignes sont concaténées les unes à la suite des autres.
	Ainsi, d'un point de vu logiue, la matrice 
		a b 
		c d
		e f
	sera représentée physiquement sous sa "forme linéaire" par le tableau : a b c d e f. 
	
	---> d'où l'intérêt pour l'utilisateur d'avoir des fonctions comme recuperer_element 
	     pour « raisonner » sous forme matricielle et donc « oublier » cette "vision tableau".
*/


//******************************************************************************
// La déclaration des fonctions applicables à une matrice
// (on parle aussi des prototypes ou des signatures de ces fonctions)
//******************************************************************************

/*
 * Fonction qui créer en mémoire une matrice
 * Paramètre nb_lig : nombre strictement positif de lignes de la matrice
 * Paramètre nb_col : nombre strictement positif de colonnes de la matrice
 * Retour : accès (à la zone mémoire) à la matrice créée
 */
matrice* allouer_matrice(int nb_lig, int nb_col);

/*
 * Fonction qui d'un point de vue logique accède en écriture à un élément (une case) dans une matrice
 * Paramètre valeur : élément à positionner dans la matrice
 * Paramètre ligne : index de ligne où positionner l'élément
 * Paramètre colonne : index de colonne où positionner l'élément
 */
void positionner_element(double valeur, matrice* p_matrice, int ligne, int colonne);

/*
 * Fonction qui d'un point de vue logique accède en lecture à un élément (une case) dans une matrice
 * Paramètre ligne : index de ligne où se situe l'élément
 * Paramètre colonne : index de colonne où se situe l'élément
 * Retour : l'élément en position (ligne,colonne) dans la matrice
 */
double recuperer_element(matrice* p_matrice, int ligne, int colonne);



//------------------------------------------------------------------------------
// Prototypes à décommenter pour la suite du TD (lorsque cela vous sera demandé)
//------------------------------------------------------------------------------

/*
 * Fonction qui remplit la matrice de gauche à droite et de haut en bas
 *          à partir des valeurs stockées dans le tableau passé en paramètre. 
 * Paramètre p_matrice : accès à la matrice à "remplir"
 * Paramètre tab : accès au tableau contenant les données, 
 *                 de taille = nb_lignes * nb_colonnes dans p_matrice
 */
void affecter_matrice(matrice* p_matrice, double tab[]);


/*
 * Fonction qui désalloue tout ce qui a été alloué pour la matrice.
 * Paramètre p_matrice : accès à la matrice à désalouer
 */
void detruire_matrice(matrice* p_matr);



/*
 * Fonction qui calcule le produit matriciel de deux matrices
 * Paramètre p_matA : accès à la matrice "de gauche"
 * Paramètre p_matB : accès à la matrice "de droite"
 * Retour : accès à la matrice correspondant au produit matriciel ;
 *          cette matrice résultat a été alouée dynamiquement (pensez à libérer la mémoire)
 */
//matrice* produit_matriciel(matrice* p_matA, matrice* p_matB);


#endif