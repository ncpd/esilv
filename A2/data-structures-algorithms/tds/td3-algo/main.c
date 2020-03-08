#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <stdbool.h>

#define TAILLE 5

//srand(time(NULL)); // initialisation du générateur aléatoire

int* genererTableauAleatoire(int taille, int valeurMax)
{
	int* tableau = NULL;
	if ( (taille > 0) && (valeurMax > 0) )
	{
		tableau = (int*) malloc( taille * sizeof(int) );  // allocation dynamique du tableau
		for(int i = 0 ; i < taille ; i++)                 // remplissage du tableau
		{
			int alea = rand() % (valeurMax+1); // nb aléatoire dans l'intervalle [0, valeur_max+1[
			*(tableau+i) = alea;   // ou bien :  tableau[i] = alea;
		}
	}
	return tableau;
}

void afficherTableau(int tableau[], int taille)
{
	if(tableau != NULL && taille > 0) {
		for(int i = 0; i < taille; i++) {
			printf("%d ", tableau[i]);
		}
		printf("\n");
	}
}

int recherche_dichotomique_iteratif(int tableau[], int taille, int valeur)
{
	int borne_inf = 0;
	int borne_sup = taille - 1;
	
	int position_milieu;
	while( borne_inf < borne_sup )
	{
		position_milieu = borne_inf + ((borne_sup - borne_inf) / 2);
		
		//--- Variante : arrêt si élément trouvé au niveau du "milieu"... 
		//               mais cela génère plus de tests !!!
		if( tableau[position_milieu] == valeur )
		{
			return position_milieu;
		}
		//--- Fin de la variante
		
		if( tableau[position_milieu] < valeur )
		{
			borne_inf = position_milieu + 1;
		}
		else // tableau[position_milieu] >= valeur
		{
			borne_sup = position_milieu;
		}
	}
	
	int position_resultat = -1;
	if( tableau[borne_inf] == valeur )
	{
		position_resultat = borne_inf;
	}
	return position_resultat;	
}


int recherche_dichotomique_recursif(int tableau[], int borne_inf, int borne_sup, int valeur)
{
	if( borne_inf == borne_sup )
	{
		int position_resultat = -1;
		if( tableau[borne_inf] == valeur )
		{
			position_resultat = borne_inf;
		}
		return position_resultat;
	}
	
	int position_milieu = (borne_inf + borne_sup) / 2;
	
	if( tableau[position_milieu] < valeur )
	{
		return recherche_dichotomique_recursif(tableau, position_milieu +1, borne_sup, valeur );
	}
	else // tableau[position_milieu] >= valeur
	{
		return recherche_dichotomique_recursif(tableau, borne_inf, position_milieu, valeur );
	}
}

void exo1()
{
	int tableau[TAILLE] = {58, 59, 60, 61, 62};
	int val = 58;
	afficherTableau(tableau, TAILLE);
	printf("Recherche dichotomique iterative - index de %d : %d\n", val, recherche_dichotomique_iteratif(tableau, TAILLE, val));
	printf("Recherche dichotomique recursive - index de %d : %d\n", val, recherche_dichotomique_recursif(tableau, 0, TAILLE - 1, val));
}

int partitionner(int *tableau, int p, int r) {
    int pivot = tableau[p], i = p-1, j = r+1;
    int temp;
    while (1) {
        do
            j--;
        while (tableau[j] > pivot);
        do
            i++;
        while (tableau[i] < pivot);
        if (i < j) {
            temp = tableau[i];
            tableau[i] = tableau[j];
            tableau[j] = temp;
        }
        else {
			return j;
		}
    }
}

bool quickSort(int *tableau, int p, int r) {
    int q;
    if (p < r) {
        q = partitionner(tableau, p, r);
        quickSort(tableau, p, q);
        quickSort(tableau, q+1, r);
    }
	return true;
}

void exo2()
{
	int tableau[TAILLE] = {45, 18, 38, 23, 42};
	printf("Tableau en desordre :\n");
	afficherTableau(tableau, TAILLE);
	if(quickSort(tableau, 0, TAILLE - 1)) {
		printf("Tableau trie :\n");
		afficherTableau(tableau, TAILLE);
		printf("\nComplexite O(nlog(n))\n");
	} else {
		printf("An error occured\n");
	}
}

int main(int argc, char **argv)
{
	//exo1();
	//exo2();
	return 0;
}
