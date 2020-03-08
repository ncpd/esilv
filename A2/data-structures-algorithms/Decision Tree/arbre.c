#include <stdbool.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include "donnees.h"
#include "arbre.h"

void afficheNoeud(noeud * n)
{
    if(n != NULL) {
        printf("X%d %s %lg, taille : %d, precision : %lg ", n->xi, n->critereDivision, n->medCorrected, n->tailleEchantillon, n->precision);
    }
}

void arbreDecision(noeud * racine, int hauteurMax, int nbMinIndiv, double minPrec, double maxPrec, double valAPredire, matrice_donnees * data)
{
	if(canBeDivided(racine, hauteurMax, nbMinIndiv, minPrec, maxPrec)) {
		int critereDivision = bestDivision(racine, valAPredire); // Meilleur critère de division de l'échantillon
		double * tableauValeursXiTrie = triSelonX(critereDivision, racine->donnees, racine); // Tableau des valeurs de Xi trié (pour médianes uniquement)

		//---------- Médianes ----------
		double medi = mediane(tableauValeursXiTrie, racine->tailleEchantillon);
		double mediCorrigee = medianeCorrigee(tableauValeursXiTrie, racine->tailleEchantillon, medi);

		//---------- Sous-échantillons ----------
		int * sousEchantillonGauche = creationSsEchantillonGauche(racine->listeIndexIndividus, racine->tailleEchantillon, mediCorrigee, racine->donnees, critereDivision);
		int * sousEchantillonDroit = creationSsEchantillonDroite(racine->listeIndexIndividus, racine->tailleEchantillon, mediCorrigee, racine->donnees, critereDivision);
        int tailleSousEchantillonGauche = getTailleSsEchantillonG(racine->listeIndexIndividus, racine->tailleEchantillon, mediCorrigee, racine->donnees, critereDivision);
		int tailleSousEchantillonDroit = getTailleSsEchantillonD(racine->listeIndexIndividus, racine->tailleEchantillon, mediCorrigee, racine->donnees, critereDivision);

		//---------- Création des noeuds gauche et droit ----------
        noeud * fGauche = creer_noeud(racine->donnees, valAPredire, tailleSousEchantillonGauche);
        insertListeIndividus(sousEchantillonGauche, tailleSousEchantillonGauche, fGauche);
        insertCritereDivision(critereDivision, mediCorrigee, "<=", fGauche);
        insertPrecision(sousEchantillonGauche, tailleSousEchantillonGauche, fGauche);

        noeud * fDroit = creer_noeud(racine->donnees, valAPredire, tailleSousEchantillonDroit);
        insertListeIndividus(sousEchantillonDroit, tailleSousEchantillonDroit, fDroit);
        insertCritereDivision(critereDivision, mediCorrigee, ">", fDroit);
        insertPrecision(sousEchantillonDroit, tailleSousEchantillonDroit, fDroit);

		//---------- Association des fils ----------
		associer_fils_gauche(racine, fGauche);
		associer_fils_droite(racine, fDroit);
		associer_parent(racine, fGauche);
		associer_parent(racine, fDroit);
		arbreDecision(racine->fils_gauche, hauteurMax - 1, nbMinIndiv, minPrec, maxPrec, valAPredire, data);
		arbreDecision(racine->fils_droite, hauteurMax - 1, nbMinIndiv, minPrec, maxPrec, valAPredire, data);
	}
}

double predire(double x1, double x2, double x3, double x4, noeud * racine)
{
    if(racine->fils_gauche == NULL && racine->fils_droite == NULL) { // <=> est une feuille
        return racine->precision;
    } else {
        noeud * noeudSuivant = NULL;
        double med = racine->fils_gauche->medCorrected; // <=> même médiane que pour fils droit donc équivalent
        int xi = racine->fils_gauche->xi; // <=> même xi que pour fils droit donc équivalent
        int direction; // <=> 0 pour la gauche, 1 pour la droite
        switch(xi) {
            case 1:
                direction = (x1 <= med) ? 0 : 1;
                break;
            case 2:
                direction = (x2 <= med) ? 0 : 1;
                break;
            case 3:
                direction = (x3 <= med) ? 0 : 1;
                break;
            case 4:
                direction = (x4 <= med) ? 0 : 1;
                break;
            default:
                break;
        }
        if(direction == 0) { // Aller à gauche
            noeudSuivant = racine->fils_gauche;
        } else { // Aller à droite
            noeudSuivant = racine->fils_droite;
        }
        return predire(x1, x2, x3, x4, noeudSuivant);
    }
}

void afficherPrediction(double x1, double x2, double x3, double x4, noeud * racine, double y)
{
    double p = predire(x1, x2, x3, x4, racine);
    p *= 100.0;
    if(y == 1.0) {
        printf("Il est probable a %lg%% que l'iris soit un Setosa\n", p);
    } else if(y == 2.0) {
        printf("Il est probable a %lg%% que l'iris soit un Versicolor\n", p);
    } else if(y == 3.0) {
        printf("Il est probable a %lg%% que l'iris soit un Virginica\n", p);
    }
}

noeud * initialiser_racine(double valeurAPredire, matrice_donnees * dat)
{
    int * initListe = malloc(dat->nb_lignes * sizeof(int));
	for(int i = 0; i < dat->nb_lignes; i++) {
		initListe[i] = i;
	}
	noeud * racine = creer_noeud(dat, valeurAPredire, dat->nb_lignes);
	insertListeIndividus(initListe, dat->nb_lignes, racine);
	racine->precision = calculPrecisionSsEchantillon(racine, valeurAPredire);
	racine->xi = 0;
	racine->critereDivision = "-Racine-";
	racine->medCorrected = 0.0;
	free(initListe);
	return racine;
}

noeud * creer_noeud(matrice_donnees * ech, double specToPredict, int tailleEch)
{
	noeud * node = malloc(sizeof(noeud));
	if(node != NULL && ech != NULL) {
		node->donnees = ech; // Accès aux données
		node->speciesToPredict = specToPredict; // Valeur de Y à prédire
		node->listeIndexIndividus = NULL;
		node->tailleEchantillon = tailleEch;
		node->xi = 0;
		node->critereDivision = "--";
		node->medCorrected = 0.0;
		node->parent = NULL;
		node->fils_droite = NULL;
		node->fils_gauche = NULL;
	}
	return node;
}

void insertListeIndividus(int * liste, int taille, noeud * node)
{
	if(node != NULL) {
		if(node->listeIndexIndividus == NULL && liste != NULL && taille > 0) {
			free(node->listeIndexIndividus);
			node->listeIndexIndividus = malloc(taille * sizeof(int));
			for(int i = 0; i < taille; i++) {
				node->listeIndexIndividus[i] = liste[i];
			}
		}
	}
}

void insertPrecision(int * sousEchantillon, int taille, noeud * n)
{
    if(n != NULL && sousEchantillon != NULL && taille > 0) {
        double prec = calculPrecisionFromListe(sousEchantillon, taille, n->speciesToPredict, n->donnees);
        n->precision = prec;
    }
}

bool canBeDivided(noeud * a, int hauteurMaximale, int nbMinIndividus, double minPrecision, double maxPrecision)
{
	bool divisible = false;
	if((hauteurArbre(a) < hauteurMaximale) && (a->tailleEchantillon >= nbMinIndividus) && (a->precision >= minPrecision && a->precision <= maxPrecision)) {
		// Conditions pour que l'échantillon soit divisible
		divisible = true;
	}
	return divisible;
}

void insertCritereDivision(int xi, double medianeCorrected, char * operation, noeud * n)
{
    int comparaisonG = strcmp(operation, "<=");
    int comparaisonD = strcmp(operation, ">");
    if((xi > 0 && xi < 5) && (comparaisonG == 0 || comparaisonD == 0) && n != NULL) {
        n->xi = xi;
        n->medCorrected = medianeCorrected;
        if(comparaisonG == 0) {
            n->critereDivision = "<=";
        } else if(comparaisonD == 0) {
            n->critereDivision = ">";
        }
    }
}

void selection(double * t, int taille)
{
	int i;
	int mini;
	int j;
	double x;
	for (i = 0; i < taille - 1; i++) {
		mini = i;
		for (j = i + 1; j < taille; j++) {
			if (t[j] < t[mini]) {
				mini = j;
			}
		}
		x = t[i];
		t[i] = t[mini];
		t[mini] = x;
     }
}

double * triSelonX(int i, matrice_donnees * dat, noeud * n)
{
	double * tableauValXi = NULL;
	if(i > 0 && i < 5 && dat != NULL && n != NULL) {
		// création tableau des valeurs de Xi à trier
		if(n->listeIndexIndividus != NULL) {
			tableauValXi = malloc(n->tailleEchantillon*sizeof(double));
			int j = 0;
			for(int lig = 0; lig < n->tailleEchantillon; lig++) {
				int indexEch = n->listeIndexIndividus[j];
				tableauValXi[lig] = dat->matrice[indexEch][i];
				j++;
			}
			//tri
			selection(tableauValXi, n->tailleEchantillon);
		}
	}
	return tableauValXi;
}

double * getDataXi(int i, noeud * n)
{
	double * tableauValXi = NULL;
	if(i > 0 && i < 5 && n != NULL) {
		if(n->listeIndexIndividus != NULL && n->donnees != NULL) {
			tableauValXi = malloc(n->tailleEchantillon*sizeof(double));
			int j = 0;
			while(j < n->tailleEchantillon) {
                int indexLigne = n->listeIndexIndividus[j]; // récupérer à l'index dans la liste
                tableauValXi[j] = n->donnees->matrice[indexLigne][i];
                j++;
			}
		}
	}
	return tableauValXi;
}

double mediane(double * tab, int taille)
{
	double med;
	int p;
	if(taille % 2 == 0) {
		p = taille / 2;
		med = (tab[p - 1] + tab[p]) / 2;
	} else {
		p = (taille - 1) / 2;
		med = tab[p];
	}
	return med;
}

bool allEquals(double * tab, int taille)
{
	bool allEqual = false;
	int count = 0;
	if(tab != NULL && taille > 0) {
		int i = taille;
		while(i > 0) {
			if(tab[i] == tab[i - 1]) {
				count++;
			}
			i--;
		}
	}
	if(count == taille - 1) { // nb d'égalités
		allEqual = true;
	}
	return allEqual;
}

double medianeCorrigee(double * tab, int taille, double mediane)
{
	double medianeCorrected = mediane;
	bool sameValues = allEquals(tab, taille);
	if(taille > 2 && !sameValues) {
		if(mediane == maxValue(tab, taille)) {
			medianeCorrected = secondMaxValue(tab, taille);
		}
	}
	return medianeCorrected;
}

double maxValue(double * tab, int taille)
{
	double max = tab[0];
	for(int i = 1; i < taille; i++) {
		if(tab[i] > max) {
			max = tab[i];
		}
	}
	return max;
}

double secondMaxValue(double * tab, int taille)
{
	// Le tableau est censé être déjà trié
	double max = tab[taille - 1];
	int i = taille - 1;
	while(tab[i] == max) {
		i--;
	}
	max = tab[i];
	return max;
}

int getTailleSsEchantillonG(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi)
{
	int j = 0;
	for(int i = 0; i < taille; i++) {
            int indexLigne = tab[i];
            if(data->matrice[indexLigne][xi] <= medianeCorrected) {
                j++;
            }
    }
	return j;
}

int getTailleSsEchantillonD(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi)
{
	int j = 0;
	for(int i = 0; i < taille; i++) {
            int indexLigne = tab[i];
            if(data->matrice[indexLigne][xi] > medianeCorrected) {
                j++;
            }
    }
	return j;
}

int * creationSsEchantillonGauche(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi) // tableau des individus, taille initiale = 120
{
    int tailleSSEchantillon = getTailleSsEchantillonG(tab, taille, medianeCorrected, data, xi);
	int * ssEchGauche = malloc(tailleSSEchantillon * sizeof(int));
	int j = 0;
	for(int i = 0; i < taille; i++) {
        int indexLigne = tab[i];
		if(data->matrice[indexLigne][xi] <= medianeCorrected) {
			ssEchGauche[j] = indexLigne;
			j++;
		}
	}
	return ssEchGauche;
}

int * creationSsEchantillonDroite(int * tab, int taille, double medianeCorrected, matrice_donnees * data, int xi)
{
    int tailleSSEchantillon = getTailleSsEchantillonD(tab, taille, medianeCorrected, data, xi);
	int * ssEchDroite = malloc(tailleSSEchantillon * sizeof(int));
	int j = 0;
	for(int i = 0; i < taille; i++) {
        int indexLigne = tab[i];
		if(data->matrice[indexLigne][xi] > medianeCorrected) {
			ssEchDroite[j] = indexLigne;
			j++;
		}
	}
	return ssEchDroite;
}

double calculPrecisionFromListe(int * listeIndividus, int tailleListe, double valueToPredict, matrice_donnees * data)
{
	int nbValeursCorrectes = 0;
	for(int i = 0; i < tailleListe; i++) {
		//regarder à l'index de la liste
		int indexSSEch = (listeIndividus[i]);
		if(data->matrice[indexSSEch][0] == valueToPredict) {
			nbValeursCorrectes++;
		}
	}
	double precision = ((double)(nbValeursCorrectes) / (double)(tailleListe));
	return precision;
}

double calculPrecisionSsEchantillon(noeud * n, double valueToPredict)
{
	int nbValeursCorrectes = 0;
	for(int i = 0; i < (n->tailleEchantillon); i++) {
		//regarder à l'index de la liste
		int indexSSEch = (n->listeIndexIndividus[i]);
		if(n->donnees->matrice[indexSSEch][0] == valueToPredict) {
			nbValeursCorrectes++;
		}
	}
	double precision = ((double)nbValeursCorrectes / (double)(n->tailleEchantillon));
	return precision;
}

bool est_feuille(noeud const * element)
{
	bool feuille = false;

	if( element != NULL )
	{
		if( (element->fils_gauche == NULL) && (element->fils_droite == NULL) )
		{
			feuille = true;
		}
	}

	return feuille;
}

double maxDouble(double a, double b)
{
	return (a > b) ? a : b;
}

int max(int a, int b)
{
	return (a > b) ? a : b;
}

int hauteurArbre(noeud const * arbre)
{
	int fg = (arbre->fils_gauche == NULL) ? 0 : hauteurArbre(arbre->fils_gauche);
	int fd = (arbre->fils_droite == NULL) ? 0 : hauteurArbre(arbre->fils_droite);
	return 1 + max(fg, fd);
}

int largeurArbre(noeud const * arbre)
{
    if (arbre == NULL) {
        return 0;
    } else {
        if(arbre->fils_gauche == NULL && arbre->fils_droite == NULL) {
            return 1;
        } else {
            return largeurArbre(arbre->fils_gauche) + largeurArbre(arbre->fils_droite);
        }
    }
 }

void afficherFeuilles(noeud * arbre)
{
   if(arbre != NULL) {
       if(arbre->fils_gauche == NULL && arbre->fils_droite == NULL) {
           afficheNoeud(arbre);
           printf(" ---- ");
           afficherChemin(arbre);
           printf("\n");
       } else {
           afficherFeuilles(arbre->fils_gauche);
           afficherFeuilles(arbre->fils_droite);
       }
   }
}

void afficherChemin(noeud * feuille)
{
    if(feuille->parent == NULL) {
       printf("Chemin : Racine; ");
    } else {
        afficherChemin(feuille->parent);
        printf("X%d %s %lg; ", feuille->xi, feuille->critereDivision, feuille->medCorrected);
    }
}

void afficher_offset(int offset)
{
	for(int i = 0 ; i < offset ; i++)
	{
		printf("  "); // 2 espaces
	}
}

void affichage_arborescence(noeud * arbre, int offset)
{
	if(arbre != NULL)
	{
		// Etape 1 - afficher la valeur
		printf("\n");
		afficher_offset(offset);
		if( offset != 0 ) // tous les éléments sauf la racine
		{
			printf("|-");
		}
		afficheNoeud(arbre);
		//printf("X%d %s %lg, taille de l'echantillon : %d, precision : %lg", arbre->xi, arbre->critereDivision, arbre->medCorrected, arbre->tailleEchantillon, arbre->precision);

		// Etape 2 - appel récursif avec sous-arbre gauche
			// Si à gauche (et uniquement à gauche) c'est NULL on affiche "|-x"
			if( !est_feuille(arbre) && (arbre->fils_gauche == NULL) )
			{
				printf("\n");
				afficher_offset(offset+1);
				printf("|-x");
			}
		affichage_arborescence(arbre->fils_gauche, offset+1);


		// Etape 2 - appel récursif avec sous-arbre de droite

			// Si à droite (et uniquement à droite) c'est NULL on affiche "|-x"
			if( !est_feuille(arbre) && (arbre->fils_droite == NULL) )
			{
				printf("\n");
				afficher_offset(offset+1);
				printf("|-x");
			}
		affichage_arborescence(arbre->fils_droite, offset+1);
	}
	//else <=> arrêt de la récursivité
}

int bestPrecisionInterEchantillons(int * ssEchantillonGauche, int tailleGauche, int * ssEchantillonDroit, int tailleDroit, double valueToPredict, matrice_donnees * data)
{
    // <=> 0 pour gauche, 1 pour droit
    double precisionGauche = calculPrecisionFromListe(ssEchantillonGauche, tailleGauche, valueToPredict, data);
    double precisionDroite = calculPrecisionFromListe(ssEchantillonDroit, tailleDroit, valueToPredict, data);
    return (precisionGauche >= precisionDroite) ? 0 : 1;
}

int bestDivision(noeud * n, double valueToPredict)
{
	int x = 1; // Xi qui sera le critère de décision
	double precisionTempG = 0.0;
	double precisionTempD = 0.0;
	for(int i = 1; i < 5; i++) {
		// De X1 à X4
		double * listeValeursTriees = triSelonX(i, n->donnees, n);
		//double * listeValeurs = getDataXi(i, n);

		double med = mediane(listeValeursTriees, n->tailleEchantillon);
        double medCorrigee = medianeCorrigee(listeValeursTriees, n->tailleEchantillon, med);

        // Création du sous échantillon gauche
		int * ssEchGauche = creationSsEchantillonGauche(n->listeIndexIndividus, n->tailleEchantillon, medCorrigee, n->donnees, i);

		// Création du sous échantillon droit
		int * ssEchDroit = creationSsEchantillonDroite(n->listeIndexIndividus, n->tailleEchantillon, medCorrigee, n->donnees, i);

		int g = getTailleSsEchantillonG(n->listeIndexIndividus, n->tailleEchantillon, medCorrigee, n->donnees, i);
		int d = getTailleSsEchantillonD(n->listeIndexIndividus, n->tailleEchantillon, medCorrigee, n->donnees, i);

		// Calcul de précision des échantillons
		double precisionGauche = calculPrecisionFromListe(ssEchGauche, g, valueToPredict, n->donnees);
		double precisionDroite = calculPrecisionFromListe(ssEchDroit, d, valueToPredict, n->donnees);

		// Quand la précision est maximale pour un des deux sous-échantillons, on copie ses valeurs et on garde le Xi actuel
		int echantillonWithMaxPrecision = bestPrecisionInterEchantillons(ssEchGauche, g, ssEchDroit, d, valueToPredict, n->donnees);
		double precisionMaximumActuelle = maxDouble(precisionTempG, precisionTempD);
		if(echantillonWithMaxPrecision == 0) { // <=> l'échantillon de gauche a la précision la plus haute
            if(precisionGauche > precisionMaximumActuelle) {
                    precisionTempG = precisionGauche;
                    precisionTempD = precisionDroite;
                    x = i;
                }
		} else { // <=> l'échantillon de droite a la précision la plus haute
            if(precisionDroite > precisionMaximumActuelle) {
                    precisionTempG = precisionGauche;
                    precisionTempD = precisionDroite;
                    x = i;
                }
		}
	}
	return x;
}

bool associer_parent(noeud * parent, noeud * enfant)
{
	bool b = false;
	if(parent != NULL && enfant != NULL) {
		if(enfant->parent == NULL) {
            enfant->parent = parent;
            b = true;
		}
	}
	return b;
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
