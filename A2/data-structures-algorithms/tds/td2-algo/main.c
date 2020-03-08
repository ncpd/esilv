#include <stdio.h>

int pgcd(int a, int b)
{
	while((a % b) != 0) {
		int reste = a % b;
		a = b;
		b = reste;
	}
	return b;
}

int pgcdRecursif(int a, int b)
{
	if((a % b) == 0) {
		return b;
	}
	return pgcdRecursif(b, a % b);
}

void exo1()
{
	int a = 710;
	int b = 310;
	int pgcdNb = pgcd(a, b);
	int pgcdRec = pgcdRecursif(a, b);
	printf("PGCD iteratif de %d par %d : %d\n", a, b, pgcdNb);
	printf("PGCD recursif de %d par %d : %d\n", a, b, pgcdRec);
}

void afficherIteratif(int tab[], int taille)
{
	for(int i = 0; i < taille; i++) {
		printf("%d ", tab[i]);
	}
	printf("\n");
}

void afficherRecursif(int tab[], int taille, int index)
{
	if(index < taille) {
		printf("%d ",tab[index]);
		afficherRecursif(tab, taille, index + 1);
	} else {
		printf("\n");
	}
}

void exo2()
{
	int tableau[5] = { 1, 2, 3, 4, 5 };
	int taille = 5;
	printf("Affichage iteratif :\n");
	afficherIteratif(tableau, taille);
	printf("Affichage recursif :\n");
	afficherRecursif(tableau, taille, 0);
}

int rechercheMaximumIteratif(int tab[], int taille)
{
	int positionMax = 0;
	for(int i = 1; i < taille; i++) {
	if(tab[i] > tab[positionMax]) {
			positionMax = i;
		}
	}
	return positionMax;
}

int rechercheMaximumRecursif(int tab[], int taille, int positionMax)
{
	if(taille == 0) {
		return positionMax;
	}
	if(tab[taille - 1] > tab[positionMax]) {
		positionMax = taille - 1;
	}
	return rechercheMaximumRecursif(tab, taille - 1, positionMax);
}

void exo3()
{
	int tableau[5] = { 82, 68, 1000, 56, 125 };
	int taille = 5;
	afficherIteratif(tableau, taille);
	printf("Index ou se situe le maximum du tableau : %d\n\n", rechercheMaximumIteratif(tableau, taille) + 1);
	printf("Index ou se situe le maximum du tableau (recursif) : %d", rechercheMaximumRecursif(tableau, taille, taille-1) + 1);
}

double somme_iteratif(double tableau[], int taille)
{
	double somme = 0.0;
	if(taille > 0) {
	for(int i = 0; i < taille; i++) {
		somme += tableau[i];
	}
	} else {
		somme = -1.0;
	}
	return somme;
}

double somme_recursif(double tableau[], int taille)
{
	double somme;
	if(taille == 0) {
		somme = 0.0;
	} else {
		somme = tableau[taille-1] + somme_recursif(tableau, taille-1);
	}
	return somme;
}

double sommeRecursifTerminal(double tableau[], int taille, double sum)
{
	if(taille == 0) {
		return sum;
	}
	return sommeRecursifTerminal(tableau, taille - 1, sum + tableau[taille-1]);
}

void exo4()
{
	double tab[] = { 1.2, 3.4, -5.0, 6.7, -8.9 };
	printf("Somme iteratif : %d\n", somme_iteratif(tab, 5));
	printf("Somme recursif : %d\n", somme_recursif(tab, 5));
	printf("Somme recursif terminal : %d\n", sommeRecursifTerminal(tab, 5, 0.0));
}

double moyenneRecursif(double tableau[], int taille)
{
	if(taille == 1) {
		return tableau[0];
	} else if(taille > 1) {
		return (tableau[taille - 1] + (moyenneRecursif(tableau, taille - 1) * (taille - 1))) / taille;
	}
}

void exo5()
{
	double tab[] = { 1.0, 2.0, 3.0, 4.0, 5.0 };
	printf("Moyenne du tableau : %lg", moyenneRecursif(tab, 5));
}
/*
int * recherchePtrMaximumIteratif(int tab[], int taille)
{
	int * ptrMax = (*int) malloc(taille * sizeof(int));
	if(ptrMax != NULL) {
		for(int i = 0; i < taille; i++) {
			*(ptrMax + i) = tab[i];
	}
	for(int i = 1; i < taille; i++) {
		if(tab[i] > *ptrMax) {
			ptrMax = (tab + i);
		}
	}
	return ptrMax;
}

void exo6()
{
	int tableau[5] = {9, 8, 6, 7, 5};
	afficherIteratif(tableau, 5);
	int * ptr = recherchePtrMaximumIteratif(tableau, 5);
	int val = *recherchePtrMaximumIteratif(tableau, 5);
	printf("Adresse pointeur : %p\nContenu du pointeur : %d", ptr, val);
}
*/
void triInsertion(int tab[], int taille)
{
	int j;
	for(int i = 1; i < taille; i++) {
		int elem = tab[i];
		for(j = i; j > 0 && tab[j - 1] > elem; j--) {
			tab[j] = tab[j - 1];
		}
		tab[j] = elem;
	}
}

void exo7()
{
	int tableau[5] = {9, 8, 6, 7, 5};
	afficherIteratif(tableau, 5);
	triInsertion(tableau, 5);
	afficherIteratif(tableau, 5);
}

int main(int argc, char **argv)
{
	//exo1();
	//exo2();
	//exo3();
	//exo4();
	//exo5();
	//exo6();
	exo7();
	return 0;
}
