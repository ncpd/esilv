#ifndef ARBRE_H
#define ARBRE_H

/*
 * Type structur� d'un noeud
 */
struct _noeud
{
	matrice_donnees * donnees; // acc�s aux donn�es
	double speciesToPredict; // Y � pr�dire
	int * listeIndexIndividus; // Liste des index des individus dans l'�chantillon
	int tailleEchantillon;
	int xi;
	double medCorrected;
	char * critereDivision; // <=> '<=' ou '>'
	double precision; // Pr�cision par rapport � Y
	struct _noeud * parent; // acc�s au parent
	struct _noeud * fils_gauche; // acc�s au fils de gauche
	struct _noeud * fils_droite; // acc�s au fils de droite
};
typedef  struct _noeud  noeud;

/*
 * Fonction qui affiche un noeud : son Xi, son test d'in�galit�, sa m�diane corrig�e, sa taille et sa pr�cision
 */
void afficheNoeud(noeud * n);

/*
 * M�thode qui cr�e l'arbre de d�cision r�cursivement
 */
void arbreDecision(noeud * racine, int hauteurMax, int nbMinIndiv, double minPrec, double maxPrec, double valAPredire, matrice_donnees * data);

/*
 * Fonction qui donne la pr�cision qu'un iris a d'�tre Y selon ses valeurs Xi
 */
double predire(double x1, double x2, double x3, double x4, noeud * racine);

/*
 * Fonction qui affiche la pr�cision en % depuis une pr�diction
 */
void afficherPrediction(double x1, double x2, double x3, double x4, noeud * racine, double y);

/*
 * Fonction qui initialise la racine de l'arbre avec l'int�gralit� de l'�chantillon de d�part
 * Retour : pointeur sur racine
 */
noeud * initialiser_racine(double valeurAPredire, matrice_donnees * dat);

/*
 * Fonction qui cr�e un noeud g�n�rique
 * Retour : pointeur sur noeud cr�e
 */
noeud * creer_noeud(matrice_donnees * ech, double specToPredict, int tailleEch);
/*
 * Fonction qui ins�re une liste d'individus dans un noeud
 */
void insertListeIndividus(int * liste, int taille, noeud * node);

/*
 * Fonction qui calcule et ins�re la pr�cision dans un noeud
 */
void insertPrecision(int * sousEchantillon, int taille, noeud * n);

/*
 * M�thode qui teste si un noeud peut �tre divis�
 * Retour : bool�en qui indique si l'�chantillon a pu �tre divis�
 */
bool canBeDivided(noeud * a, int hauteurMaximale, int nbMinIndividus, double minPrecision, double maxPrecision);

/*
 * Fonction qui ins�re le Xi, la m�diane corrig�e et le test d'in�galit� dans un noeud
 */
void insertCritereDivision(int xi, double medianeCorrected, char * operation, noeud * n);

/*
 * Tri s�lection d'un tableau de r�els (pour le calcul de la m�diane les valeurs se doivent d'�tre tri�es)
 */
void selection(double * t, int taille);

/*
 * M�thode qui r�cup�re et trie un crit�re des donn�es (ex : longueur p�tales, etc.)
 * Retour : tableau de valeurs du crit�re Xi tri�
 */
double * triSelonX(int i, matrice_donnees * dat, noeud * n);

/*
 * Fonction qui r�cup�re les donn�es d'un crit�re Xi � partir d'un �chantillon
 * Retour : tableau des valeurs de Xi pour les individus de l'�chantillon
 */
double * getDataXi(int i, noeud * n);

/*
 * M�thode qui calcule la m�diane d'un tableau pr�c�mment tri�
 * Retour : m�diane du tableau
 */
double mediane(double * tab, int taille);

/*
 * Fonction qui v�rifie si toutes les valeurs d'un tableau sont les m�mes
 * Retour : bool�en qui indique que toutes les valeurs sont les m�mes (ou non)
 */
bool allEquals(double * tab, int taille);

/*
 * M�thode qui calcule la m�diane corrig�e d'un tableau pr�c�demment tri�
 * Retour : m�diane corrig�e d'un tableau
 */
double medianeCorrigee(double * tab, int taille, double mediane);

/*
 * Fonction qui trouve le maximum d'un tableau de double
 * Retour : �l�ment maximum
 */
double maxValue(double * tab, int taille);

/*
 * M�thode qui trouve la deuxi�me valeur maximale d'un tableau p�c�demment tri�
 * Retour : deuxi�me valeur maximale du tableau
 */
double secondMaxValue(double * tab, int taille);

/*
 * Fonction qui calcule la taille que doit avoir le sous �chantillon gauche (<= mediane corrigee)
 * Retour : taille du sous �chantillon gauche
 */
int getTailleSsEchantillonG(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi);

/*
 * Fonction qui calcule la taille que doit avoir le sous �chantillon droit (> mediane corrigee)
 * Retour : taille du sous �chantillon droit
 */
int getTailleSsEchantillonD(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi);

/*
 * Fonction qui cr�e le sous �chantillon gauche
 * Retour : liste des individus avec xi <= mediane corrig�e
 */
int * creationSsEchantillonGauche(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi);

/*
 * Fonction qui cr�e le sous �chantillon droit
 * Retour : liste des individus avec xi > mediane corrig�e
 */
int * creationSsEchantillonDroite(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi);

/*
 * Fonction qui calcule la pr�cision d'un �chantillon depuis le tableau des index d'individus
 * Retour : pr�cision de l'�chantillon
 */
double calculPrecisionFromListe(int * listeIndividus, int tailleListe, double valueToPredict, matrice_donnees * data);

/*
 * Fonction qui calcule la pr�cision d'un �chantillon depuis un noeud
 * Retour : pr�cision de l'�chantillon
 */
double calculPrecisionSsEchantillon(noeud * n, double valueToPredict);

/*
 * Fonction qui v�rifie si un noeud est une feuille
 * Retour : bool�en qui assure que le noeud est une feuille
 */
bool est_feuille(noeud const * element);

/*
 * Fonction qui renvoie le maximum des 2 param�tres
 * Retour : r�el maximum des 2 param�tres
 */
double maxDouble(double a, double b);

/*
 * Fonction qui renvoie le maximum des 2 param�tres
 * Retour : entier maximum des 2 param�tres
 */
int maxi(int a, int b);

/*
 * M�thode qui calcule la hauteur d'un arbre
 * Retour : entier correspondant � la hauteur de l'arbre
 */
int hauteurArbre(noeud const * arbre);

/*
 * M�thode qui calcule la largeur d'un arbre
 * Retour : entier correspondant � la largeur de l'arbre (ie. nombre de feuilles)
 */
int largeurArbre(noeud const * arbre);

/*
 * M�thode qui affiche les feuilles d'un arbre
 */
void afficherFeuilles(noeud * arbre);

/*
 * M�thode qui affiche le chemin d'un noeud
 */
void afficherChemin(noeud * feuille);

/*
 * M�thode qui affiche un offset (nombre d'espaces)
 */
void afficher_offset(int offset);

/*
 * M�thode qui affiche l'arbre sous forme arborescente
 */
void affichage_arborescence(noeud * arbre, int offset);

/*
 * M�thode qui trouve la plus grande pr�cision entre 2 �chantillons
 * Retour : <=> 0 si l'�chantillon a la plus haute pr�cision est le gauche, <=> 1 si l'�chantillon a la plus haute pr�cision est le droit
 */
int bestPrecisionInterEchantillons(int * ssEchantillonGauche, int tailleGauche, int * ssEchantillonDroit, int tailleDroit, double valueToPredict, matrice_donnees * data);

/*
 * M�thode qui trouve le meilleur crit�re de division parmi les Xi
 * Retour : 1, 2, 3 ou 4 -- Xi qui a la plus grande pr�cision
 */
int bestDivision(noeud * n, double valueToPredict);

/*
 * M�thode associe un noeud parent � un noeud enfant
 * Retour : bool�en qui indique que l'association s'est bien faite
 */
bool associer_parent(noeud * parent, noeud * enfant);

/*
 * M�thode associe un noeud comme fils gauche d'un noeud parent
 * Retour : bool�en qui indique que l'association s'est bien faite
 */
bool associer_fils_gauche(noeud * parent, noeud * enfant);

/*
 * M�thode associe un noeud comme fils droit d'un noeud parent
 * Retour : bool�en qui indique que l'association s'est bien faite
 */
bool associer_fils_droite(noeud * parent, noeud * enfant);

#endif
