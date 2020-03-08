/* ---------------------------------------------
 * | AUTEURS : Nicolas PICARD / Thomas PIBERNE |
 * | ESILV A3 - TD N                           |
 * ------------------------------------------- |*/
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include "table.h"
#include "vecteur.h"

const char nom_fichier[] = "Chicago.txt";

void affichageIndex(int taille, int base)
{
	char nom[20];
	char prenom[20];
	printf("Entrez un nom : ");
	scanf("%s", nom);
	formatNom(nom);
	printf("Entrez un prenom : ");
	scanf("%s", prenom);
	formatPrenom(prenom);
	if(nom != NULL && prenom != NULL) {
		int ind = indexFonctionnaire(nom, prenom, base, taille);
		printf("Index de %s %s : %d\n", nom, prenom, ind);
	}
}

void ajouter(table * tabl, int taille, int base)
{
	char nom[20];
	char prenom[20];
	int salaire = -1;
	
	printf("Entrez un nom : ");
	scanf("%s", nom);
	formatNom(nom);
	printf("Entrez un prenom : ");
	scanf("%s", prenom);
	formatPrenom(prenom);
	printf("Entrez un salaire : ");
	scanf("%d", &salaire);
	if(nom != NULL && prenom != NULL && base > 0 && salaire > 0) {
		bool b = addToTable(nom, prenom, salaire, base, tabl, taille);
		if(b == true) {
			printf("Le fonctionnaire a bien ete ajoute a la table.\n");
		} else {
			printf("Une erreur s'est produite, le fonctionnaire n'a pas ete ajoute a la table.\n");
		}
	}
}

void charger(table * tabl, int taille, int base)
{
	int nbFonctionnairesACharger = 0;
	printf("Entrez un nombre de fonctionnaires a charger : ");
	scanf("%d", &nbFonctionnairesACharger);
	FILE* fichier = fopen(nom_fichier,"r");
	if(fichier != NULL) {
		int nbFonctionnairesMax;
		fscanf(fichier, "%d", &nbFonctionnairesMax);
		if(nbFonctionnairesACharger <= nbFonctionnairesMax) {
			char nom[20];
			char prenom[20];
			int salaire;
			int i = nbFonctionnairesACharger - 1;
			while(i >= 0) {
				fscanf(fichier, "%s %s %d\n", nom, prenom, &salaire);
				addToTable(nom, prenom, salaire, base, tabl, taille); // ajoute pour chaque ligne un fonctionnaire propre aux informations du fichier (nom, prenom, salaire)
				i--;
			}
			printf("Les fonctionnaires du fichier %s ont bien ete charges.\n", nom_fichier);
		} else {
			printf("Echec du chargement - nombre de fonctionnaires à charger trop important.\n");
		}
	} else {
		printf("Echec du chargement - fichier introuvable.\n");
	}
}

void afficheSalaire(table * tab, int taille, int base)
{
	char nom[20];
	char prenom[20];
	int salaire = -1;
	printf("Entrez un nom : ");
	scanf("%s", nom);
	formatNom(nom);
	printf("Entrez un prenom : ");
	scanf("%s", prenom);
	formatPrenom(prenom);
	int indexFonc = indexFonctionnaire(nom, prenom, base, taille);
	vecteur * v = &(tab->_table[indexFonc]);
	fonctionnaire * f = allouer_fonctionnaire(nom, prenom, salaire); // utile pour la comparaison des noms et prénoms uniquement
	if(fonctionnaireExists(v, f)) {
		for(int i = 0; i < v->tailleLogique; i++) {
			if(equals(&(v->vec_fonctionnaires[i]), f)) {
				printf("Salaire de %s %s : %d USD\n", v->vec_fonctionnaires[i].nom, v->vec_fonctionnaires[i].prenom, v->vec_fonctionnaires[i].salaire);
			}
		}
	} else {
		printf("Salaire de %s %s : %d USD\n", nom, prenom, salaire); // fonctionnaire introuvable -> -1 USD
	}
}

void afficheEntre(table * tab, int taille, int base)
{
	int startIndex = -1;
	int endIndex = -1;
	printf("Entrez un index de debut : ");
	scanf("%d", &startIndex);
	printf("Entrez un index de fin : ");
	scanf("%d", &endIndex);
	if(startIndex >= 0 && endIndex < taille && startIndex <= endIndex) {
		for(int i = startIndex; i <= endIndex; i++) {
			vecteur * v = &(tab->_table[i]);
			if(!isNullOrEmpty(v)) {
				printf("Index %d :\n", i);
				for(int j = 0; j < v->tailleLogique; j++) {
					printf("%s %s : %d USD\n", v->vec_fonctionnaires[j].nom, v->vec_fonctionnaires[j].prenom, v->vec_fonctionnaires[j].salaire);
				}
				printf("\n");
			} else {
				printf("Index %d :\nPas de fonctionnaire\n", i);
			}
		}
	} else {
		printf("Un probleme est survenu - vos index sont errones\n");
	}
}

void nbConflits(table * tab, int taille, int base)
{
	int nbConflicts = 0;
	for(int i = 0; i < taille; i++) { // parcours de la table et verification de l'existence de conflit
		vecteur * v = &(tab->_table[i]);
		if(haveConflict(v)) {
			nbConflicts++;
		}
	}
	printf("Nombre de conflits dans la table : %d\n", nbConflicts);
}

void tailleMoyenneConflits(table * tab, int taille, int base)
{
	int sommeNbElements = 0;
	int nbConflicts = 0;
	double moyenne = 0;
	for(int i = 0; i < taille; i++) {
		vecteur * v = &(tab->_table[i]);
		if(haveConflict(v)) {
			nbConflicts++;
			sommeNbElements += v->tailleLogique;
		}
	}
	if(nbConflicts > 0) { // Évite la division par 0
		moyenne = (double)sommeNbElements / (double)nbConflicts;
	}
	printf("Taille moyenne des conflits : %lg\n", moyenne);
}

void supprimer(table * tabl, int taille, int base)
{
	char nom[20];
	char prenom[20];
	printf("Entrez un nom : ");
	scanf("%s", nom);
	formatNom(nom);
	printf("Entrez un prenom : ");
	scanf("%s", prenom);
	formatPrenom(prenom);
	if(nom != NULL && prenom != NULL && base > 0) {
		bool b = deleteFromTable(nom, prenom, base, tabl, taille);
		if(b == true) {
			printf("Le fonctionnaire a bien ete efface.\n");
		} else {
			printf("Une erreur s'est produite, le fonctionnaire n'a pas ete efface de la table.\n");
		}
	}
}

void supprimerEntre(table * tab, int taille, int base)
{
	int startIndex = -1;
	int endIndex = -1;
	printf("Entrez un index de debut : ");
	scanf("%d", &startIndex);
	printf("Entrez un index de fin : ");
	scanf("%d", &endIndex);
	if(startIndex >= 0 && endIndex < taille && startIndex <= endIndex) {
		for(int i = startIndex; i <= endIndex; i++) {
			vecteur * v = &(tab->_table[i]);
			if(!isNullOrEmpty(v)) { // si le vecteur n'est pas vide uniquement
				free(v->vec_fonctionnaires);
				v->vec_fonctionnaires = NULL;
				v->tailleLogique = 0;
			}
		}
		printf("Les fonctionnaires des index %d a %d ont bien ete effaces.\n", startIndex, endIndex);
	} else {
		printf("Un probleme est survenu - vos index sont errones.\n");
	}
}

void affichageMenu()
{
	printf("\n\n");
	printf("    ||||||||||||||||||||||||||||| Menu ||||||||||||||||||||||||||||||||\n");
	printf("    |||                                                             |||\n");
	printf("    |||         1/ Index\t\t6/ Nb de conflits           |||\n");
	printf("    |||         2/ Ajouter\t\t7/ Taille moy. des conflits |||\n");
	printf("    |||         3/ Charger\t\t8/ Supprimer\t\t    |||\n");
	printf("    |||         4/ Afficher Salaire\t9/ Supprimer entre\t    |||\n");
	printf("    |||         5/ Afficher entre\t10/ Quitter\t\t    |||\n");
	printf("    |||                                                             |||\n");
	printf("    |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n");
}

void menu()
{
	int tailleTable;
	int base;
	do {
		printf("\n\n\tBonjour !\n\tQuelle taille souhaitez vous pour votre table ?\t");
		scanf("%d", &tailleTable);
		printf("\tEntrez maintenant la base souhaitee :\t");
		scanf("%d", &base);
	} while(tailleTable < 0 && base < 0);
	table * tab = allouer_table(tailleTable);
	system("cls"); // clear console
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
					affichageIndex(tailleTable, base);
					do {
						printf("Souhaitez vous refaire une operation ? (O/N)\t");
						getchar();
						choice = getchar();
						} while (choice != 'O' && choice != 'N');
						system("cls");
					break;
				case 2:
					ajouter(tab, tailleTable, base);
					do {
						printf("Souhaitez vous refaire une operation ? (O/N)\t");
						getchar();
						choice = getchar();
						} while (choice != 'O' && choice != 'N');
						system("cls");
					break;
				case 3:
					charger(tab, tailleTable, base);
					do {
						printf("Souhaitez vous refaire une operation ? (O/N)\t");
						getchar();
						choice = getchar();
						} while (choice != 'O' && choice != 'N');
						system("cls");
					break;
				case 4:
					afficheSalaire(tab, tailleTable, base);
					do {
						printf("Souhaitez vous refaire une operation ? (O/N)\t");
						getchar();
						choice = getchar();
						} while (choice != 'O' && choice != 'N');
						system("cls");
					break;
				case 5:
					afficheEntre(tab, tailleTable, base);
					do {
						printf("Souhaitez vous refaire une operation ? (O/N)\t");
						getchar();
						choice = getchar();
						} while (choice != 'O' && choice != 'N');
						system("cls");
					break;
				case 6:
					nbConflits(tab, tailleTable, base);
					do {
						printf("Souhaitez vous refaire une operation ? (O/N)\t");
						getchar();
						choice = getchar();
						} while (choice != 'O' && choice != 'N');
						system("cls");
					break;
				case 7:
					tailleMoyenneConflits(tab, tailleTable, base);
					do {
						printf("Souhaitez vous refaire une operation ? (O/N)\t");
						getchar();
						choice = getchar();
						} while (choice != 'O' && choice != 'N');
						system("cls");
					break;
				case 8:
					supprimer(tab, tailleTable, base);
					do {
						printf("Souhaitez vous refaire une operation ? (O/N)\t");
						getchar();
						choice = getchar();
						} while (choice != 'O' && choice != 'N');
						system("cls");
					break;
				case 9:
					supprimerEntre(tab, tailleTable, base);
					do {
						printf("Souhaitez vous refaire une operation ? (O/N)\t");
						getchar();
						choice = getchar();
						} while (choice != 'O' && choice != 'N');
						system("cls");
					break;
				case 10:
					exit(EXIT_SUCCESS);
					break;
				default:
					break;
			}
		} while(choix < 1 || choix > 10);
	} while(choice == 'O');
	exit(EXIT_SUCCESS);
}


int main(int argc, char **argv)
{
	menu();
	return 0;
}
