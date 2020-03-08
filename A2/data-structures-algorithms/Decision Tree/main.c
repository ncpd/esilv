#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>
#include "donnees.h"
#include "arbre.h"

const char nom_fichier[] = "iris.txt";

void affichageMenu()
{
	printf("\n\n");
	printf("    ||||||||||||||||||||||||||||| Menu ||||||||||||||||||||||||||||||||\n");
	printf("    |||                                                             |||\n");
	printf("    |||     1/ Hauteur de l\'arbre\t4/ Feuilles de l\'arbre      |||\n");
	printf("    |||     2/ Largeur de l\'arbre\t5/ Predire                  |||\n");
	printf("    |||     3/ Arbre en arborescence                                |||\n");
	printf("    |||                                                             |||\n");
	printf("    |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n");
}

bool input(double y, double pMin, double pMax, int nbMinIndiv, int hMax)
{
    bool inp = false;
    int yValide = (y == 1.0 || y == 2.0 || y == 3.0) ? 1 : 0;
    int pMinValide = (pMin >= 0.0 && pMin <= 1.0) ? 1 : 0;
    int pMaxValide = (pMax >= pMin && pMax <= 1.0) ? 1 : 0;
    int nbMinIndividusValide = (nbMinIndiv > 0) ? 1 : 0;
    int hMaxValide = (hMax > 0) ? 1 : 0;
    if((yValide + pMinValide + pMaxValide + nbMinIndividusValide + hMaxValide) == 5) {
        inp = true;
    }
    return inp;
}

void menu()
{
	double valToPredict;
	double minPrecision; // [0-1]
	double maxPrecision; // [0-1]
	int nbMinIndividus;
	int tailleMaxArbre;

	double x1;
    double x2;
    double x3;
    double x4;

	do {
		printf("Bonjour !\nEntrez une valeur Y a predire (1, 2 ou 3):\t");
        scanf("%lg", &valToPredict);
        printf("\nEntrez maintenant un seuil de precision minimal:\t");
        scanf("%lg", &minPrecision);
        printf("\nUn seuil de precision maximal:\t");
        scanf("%lg", &maxPrecision);
        printf("\nUn nombre minimal d'individus par echantillon:\t");
        scanf("%d", &nbMinIndividus);
        printf("\nEt enfin la taille maximale de l'arbre:\t");
        scanf("%d", &tailleMaxArbre);
        system("cls");
	} while(!input(valToPredict, minPrecision, maxPrecision, nbMinIndividus, tailleMaxArbre));
    matrice_donnees * data = charger_donnees(nom_fichier);
    if(data != NULL) {
        noeud * racine = initialiser_racine(valToPredict, data);
        arbreDecision(racine, tailleMaxArbre, nbMinIndividus, minPrecision, maxPrecision, valToPredict, data);

        int choix = -1;
        char choice;
        do {
            do {
                affichageMenu();
                printf("\nChoisissez un item :\n");
                scanf("%d", &choix);
                system("cls");
                switch(choix) {
                    case 1:
                        printf("La hauteur de l'arbre est de %d.\n", hauteurArbre(racine));
                        do {
                            printf("\n\nSouhaitez vous faire autre chose ? (O/N)\t");
                            getchar();
                            choice = getchar();
						} while (choice != 'O' && choice != 'N');
                        system("cls");
                        break;
                    case 2:
                        printf("La largeur de l'arbre est de %d.\n", largeurArbre(racine));
                        do {
                            printf("\n\nSouhaitez vous faire autre chose ? (O/N)\t");
                            getchar();
                            choice = getchar();
						} while (choice != 'O' && choice != 'N');
                        system("cls");
                        break;
                    case 3:
                        affichage_arborescence(racine, 0);
                        do {
                            printf("\n\nSouhaitez vous faire autre chose ? (O/N)\t");
                            getchar();
                            choice = getchar();
						} while (choice != 'O' && choice != 'N');
                        system("cls");
                        break;
                    case 4:
                        printf("Afficher les feuilles :\n\n");
                        afficherFeuilles(racine);
                        do {
                            printf("\n\nSouhaitez vous faire autre chose ? (O/N)\t");
                            getchar();
                            choice = getchar();
						} while (choice != 'O' && choice != 'N');
                        system("cls");
                        break;
                    case 5:
                        do {
                            printf("Entrez X1 :\t");
                            scanf("%lf", &x1);
                            printf("Entrez X2 :\t");
                            scanf("%lf", &x2);
                            printf("Entrez X3 :\t");
                            scanf("%lf", &x3);
                            printf("Entrez X4 :\t");
                            scanf("%lf", &x4);
                        } while(x1 < 0.0 && x2 < 0.0 && x3 < 0.0 && x4 < 0.0);
                        afficherPrediction(x1, x2, x3, x4, racine, valToPredict);
                        do {
                            printf("\n\nSouhaitez vous faire autre chose ? (O/N)\t");
                            getchar();
                            choice = getchar();
						} while (choice != 'O' && choice != 'N');
                        system("cls");
                        break;
                    default:
                        break;
                }
            } while(choix < 1 || choix > 5);
        } while(choice == 'O');
        data = liberer_donnees(data);
        exit(EXIT_SUCCESS);
    } else {
        printf("Le fichier %s n'a pas pu être chargé - fichier introuvable\n", nom_fichier);
    }
}

void afficheData(matrice_donnees * dat)
{
	printf("Nb lignes : %u\nNb colonnes : %u\n", dat->nb_lignes, dat->nb_colonnes);
	for(int i = 0; i < dat->nb_lignes; i++) {
		for(int j = 0; j < dat->nb_colonnes; j++) {
			if(j == 0) {
				switch((int)dat->matrice[i][j]) {
					case 1:
						printf("Setosa ");
						break;
					case 2:
						printf("Versicolor ");
						break;
					case 3:
						printf("Virginica ");
						break;
					default:
						break;
				}
			} else {
				printf("%lg ", dat->matrice[i][j]);
			}
		}
		printf("\n");
	}
}

int main(int argc, char **argv)
{
    menu();
	return 0;
}
