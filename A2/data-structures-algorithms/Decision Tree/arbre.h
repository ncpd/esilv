#ifndef ARBRE_H
#define ARBRE_H

/*
 * Type structuré d'un noeud
 */
struct _noeud
{
	matrice_donnees * donnees; // accès aux données
	double speciesToPredict; // Y à prédire
	int * listeIndexIndividus; // Liste des index des individus dans l'échantillon
	int tailleEchantillon;
	int xi;
	double medCorrected;
	char * critereDivision; // <=> '<=' ou '>'
	double precision; // Précision par rapport à Y
	struct _noeud * parent; // accès au parent
	struct _noeud * fils_gauche; // accès au fils de gauche
	struct _noeud * fils_droite; // accès au fils de droite
};
typedef  struct _noeud  noeud;

/*
 * Fonction qui affiche un noeud : son Xi, son test d'inégalité, sa médiane corrigée, sa taille et sa précision
 */
void afficheNoeud(noeud * n);

/*
 * Méthode qui crée l'arbre de décision récursivement
 */
void arbreDecision(noeud * racine, int hauteurMax, int nbMinIndiv, double minPrec, double maxPrec, double valAPredire, matrice_donnees * data);

/*
 * Fonction qui donne la précision qu'un iris a d'être Y selon ses valeurs Xi
 */
double predire(double x1, double x2, double x3, double x4, noeud * racine);

/*
 * Fonction qui affiche la précision en % depuis une prédiction
 */
void afficherPrediction(double x1, double x2, double x3, double x4, noeud * racine, double y);

/*
 * Fonction qui initialise la racine de l'arbre avec l'intégralité de l'échantillon de départ
 * Retour : pointeur sur racine
 */
noeud * initialiser_racine(double valeurAPredire, matrice_donnees * dat);

/*
 * Fonction qui crée un noeud générique
 * Retour : pointeur sur noeud crée
 */
noeud * creer_noeud(matrice_donnees * ech, double specToPredict, int tailleEch);
/*
 * Fonction qui insère une liste d'individus dans un noeud
 */
void insertListeIndividus(int * liste, int taille, noeud * node);

/*
 * Fonction qui calcule et insère la précision dans un noeud
 */
void insertPrecision(int * sousEchantillon, int taille, noeud * n);

/*
 * Méthode qui teste si un noeud peut être divisé
 * Retour : booléen qui indique si l'échantillon a pu être divisé
 */
bool canBeDivided(noeud * a, int hauteurMaximale, int nbMinIndividus, double minPrecision, double maxPrecision);

/*
 * Fonction qui insère le Xi, la médiane corrigée et le test d'inégalité dans un noeud
 */
void insertCritereDivision(int xi, double medianeCorrected, char * operation, noeud * n);

/*
 * Tri sélection d'un tableau de réels (pour le calcul de la médiane les valeurs se doivent d'être triées)
 */
void selection(double * t, int taille);

/*
 * Méthode qui récupère et trie un critère des données (ex : longueur pétales, etc.)
 * Retour : tableau de valeurs du critère Xi trié
 */
double * triSelonX(int i, matrice_donnees * dat, noeud * n);

/*
 * Fonction qui récupère les données d'un critère Xi à partir d'un échantillon
 * Retour : tableau des valeurs de Xi pour les individus de l'échantillon
 */
double * getDataXi(int i, noeud * n);

/*
 * Méthode qui calcule la médiane d'un tableau précémment trié
 * Retour : médiane du tableau
 */
double mediane(double * tab, int taille);

/*
 * Fonction qui vérifie si toutes les valeurs d'un tableau sont les mêmes
 * Retour : booléen qui indique que toutes les valeurs sont les mêmes (ou non)
 */
bool allEquals(double * tab, int taille);

/*
 * Méthode qui calcule la médiane corrigée d'un tableau précédemment trié
 * Retour : médiane corrigée d'un tableau
 */
double medianeCorrigee(double * tab, int taille, double mediane);

/*
 * Fonction qui trouve le maximum d'un tableau de double
 * Retour : élément maximum
 */
double maxValue(double * tab, int taille);

/*
 * Méthode qui trouve la deuxième valeur maximale d'un tableau pécédemment trié
 * Retour : deuxième valeur maximale du tableau
 */
double secondMaxValue(double * tab, int taille);

/*
 * Fonction qui calcule la taille que doit avoir le sous échantillon gauche (<= mediane corrigee)
 * Retour : taille du sous échantillon gauche
 */
int getTailleSsEchantillonG(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi);

/*
 * Fonction qui calcule la taille que doit avoir le sous échantillon droit (> mediane corrigee)
 * Retour : taille du sous échantillon droit
 */
int getTailleSsEchantillonD(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi);

/*
 * Fonction qui crée le sous échantillon gauche
 * Retour : liste des individus avec xi <= mediane corrigée
 */
int * creationSsEchantillonGauche(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi);

/*
 * Fonction qui crée le sous échantillon droit
 * Retour : liste des individus avec xi > mediane corrigée
 */
int * creationSsEchantillonDroite(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi);

/*
 * Fonction qui calcule la précision d'un échantillon depuis le tableau des index d'individus
 * Retour : précision de l'échantillon
 */
double calculPrecisionFromListe(int * listeIndividus, int tailleListe, double valueToPredict, matrice_donnees * data);

/*
 * Fonction qui calcule la précision d'un échantillon depuis un noeud
 * Retour : précision de l'échantillon
 */
double calculPrecisionSsEchantillon(noeud * n, double valueToPredict);

/*
 * Fonction qui vérifie si un noeud est une feuille
 * Retour : booléen qui assure que le noeud est une feuille
 */
bool est_feuille(noeud const * element);

/*
 * Fonction qui renvoie le maximum des 2 paramètres
 * Retour : réel maximum des 2 paramètres
 */
double maxDouble(double a, double b);

/*
 * Fonction qui renvoie le maximum des 2 paramètres
 * Retour : entier maximum des 2 paramètres
 */
int maxi(int a, int b);

/*
 * Méthode qui calcule la hauteur d'un arbre
 * Retour : entier correspondant à la hauteur de l'arbre
 */
int hauteurArbre(noeud const * arbre);

/*
 * Méthode qui calcule la largeur d'un arbre
 * Retour : entier correspondant à la largeur de l'arbre (ie. nombre de feuilles)
 */
int largeurArbre(noeud const * arbre);

/*
 * Méthode qui affiche les feuilles d'un arbre
 */
void afficherFeuilles(noeud * arbre);

/*
 * Méthode qui affiche le chemin d'un noeud
 */
void afficherChemin(noeud * feuille);

/*
 * Méthode qui affiche un offset (nombre d'espaces)
 */
void afficher_offset(int offset);

/*
 * Méthode qui affiche l'arbre sous forme arborescente
 */
void affichage_arborescence(noeud * arbre, int offset);

/*
 * Méthode qui trouve la plus grande précision entre 2 échantillons
 * Retour : <=> 0 si l'échantillon a la plus haute précision est le gauche, <=> 1 si l'échantillon a la plus haute précision est le droit
 */
int bestPrecisionInterEchantillons(int * ssEchantillonGauche, int tailleGauche, int * ssEchantillonDroit, int tailleDroit, double valueToPredict, matrice_donnees * data);

/*
 * Méthode qui trouve le meilleur critère de division parmi les Xi
 * Retour : 1, 2, 3 ou 4 -- Xi qui a la plus grande précision
 */
int bestDivision(noeud * n, double valueToPredict);

/*
 * Méthode associe un noeud parent à un noeud enfant
 * Retour : booléen qui indique que l'association s'est bien faite
 */
bool associer_parent(noeud * parent, noeud * enfant);

/*
 * Méthode associe un noeud comme fils gauche d'un noeud parent
 * Retour : booléen qui indique que l'association s'est bien faite
 */
bool associer_fils_gauche(noeud * parent, noeud * enfant);

/*
 * Méthode associe un noeud comme fils droit d'un noeud parent
 * Retour : booléen qui indique que l'association s'est bien faite
 */
bool associer_fils_droite(noeud * parent, noeud * enfant);

#endif
