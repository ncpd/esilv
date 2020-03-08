#include <stdio.h>
#include <math.h>

void exo1()
{
	printf("Exo 1\n");
	
	int entier = 12;
	printf("Le contenu de l'entier est %d\n", entier);
	
	int entier2 = 34;
	printf("Les contenus des deux entiers sont respectivement %d et %d", entier, entier2);
}

void exo2a()
{
	printf("Entrez deux entiers, separes par un espace :\n");
	int entier1;
	int entier2;
	scanf("%d %d", &entier1, &entier2);
	
	int somme = entier1 + entier2;
	int difference = entier1 - entier2;
	int produit = entier1 * entier2;
	int division = entier1 / entier2;
	int reste = entier1 % entier2;
	
	printf("Somme : %d\n", somme);
	printf("Difference : %d\n", difference);
	printf("Produit : %d\n", produit);
	printf("Division : %d\n", division);
	printf("Reste : %d\n", reste);
}

void exo2b()
{
	printf("Entrez deux entiers, separes par un espace :\n");
	int entier1;
	int entier2;
	scanf("%d %d", &entier1, &entier2);
	
	printf("Somme : %d\n", entier1 + entier2);
	printf("Difference : %d\n", entier1 - entier2);
	printf("Produit : %d\n", entier1 * entier2);
	printf("Division : %d\n", entier1 / entier2);
	printf("Reste : %d\n", entier1 % entier2);
}

void exo3a()
{
	const float PI = 3.1415;
	
	printf("Exo 3a\n");
	printf("cos(Pi/6)=%f\ncos(Pi/4)=%f\ncos(Pi/3)=%f\n", cos(PI/6), cos(PI/4), cos(PI/3));
	printf("sin(Pi/3)=%f\nsin(Pi/4)=%f\nsin(Pi/6)=%f\n", sin(PI/3), sin(PI/4), sin(PI/6));
}

void exo3b()
{
	const double PI = 2 * asin(1);
	
	printf("Exo 3b\n");
	printf("cos(Pi/6)=%lf\ncos(Pi/4)=%lf\ncos(Pi/3)=%lf\n", cos(PI/6), cos(PI/4), cos(PI/3));
	printf("sin(Pi/3)=%lf\nsin(Pi/4)=%lf\nsin(Pi/6)=%lf\n", sin(PI/3), sin(PI/4), sin(PI/6));
}

int exo4a(int valeur)
{
	int pair;
	if(valeur % 2 == 0) {
		pair = 1;
	} else {
		pair = 0;
	}
	return pair;
}

int exo4b(int valeur)
{
	int pair = (valeur % 2 == 0) ? 1 : 0;
	return pair;
}

void exo5()
{
	int entier = 42;
	int * pointeur = NULL;
	
	printf("Exo 5 :\n");
	printf("Contenu de l\'entier : %d\n", entier); //42
	printf("Contenu du pointeur : %p\n", pointeur); //0000000 = NULL
	
	printf("Modification du pointeur\n");
	pointeur = &entier;
	printf("Contenu de l\'entier : %d - Adresse de l\'entier : %p\n", entier, &entier); //42 et adresse entier
	printf("Contenu du pointeur : %p - Contenu de l\'element pointé : %d\n", pointeur, *pointeur); //adresse entier et 42
	printf("Adresse du pointeur = %p\n", &pointeur); //adresse pointeur
	
	printf("Modification de l\'élément pointé\n");
	*pointeur = 33;
	printf("Contenu de l\'entier : %d - Adresse de l\'entier : %p\n", entier, &entier); //33 et adresse entier
	printf("Contenu du pointeur : %p - Contenu de l\'élément pointé : %d\n", pointeur, *pointeur); //adresse entier et 33
	printf("Adresse du pointeur = %p\n", &pointeur); //adresse pointeur
	
	printf("Modification du pointeur et de l\'élément pointé\n");
	int nombre = 123;
	pointeur = &nombre;
	*pointeur = 12345;
	printf("Contenu de l\'entier : %d - Adresse de l\'entier : %p\n", entier, &entier); //33 et adresse entier
	printf("Contenu du nombre : %d - Adresse du nombre : %p\n", nombre, &nombre); //12345 et adresse nombre
	printf("Contenu du pointeur : %p - Contenu de l\'élément pointé : %d\n", pointeur, *pointeur); //adresse nombre et 12345
	printf("Adresse du pointeur = %p\n", &pointeur); //adresse pointeur
}

void swap(int * p1, int * p2) // Pointeurs en arguments
{
	if((p1 != NULL) && (p2 != NULL)) { 
		int tmp = *p1; // tmp prend la valeur val1
		*p1 = *p2; // val2 prend val1
		*p2 = tmp; // val2 prend tmp
	}
}

void exo6(int val1, int val2)
{
	int *p1 = &val1;
	int *p2 = &val2;
	printf("Valeur 1 : %d - Valeur 2 : %d\n\n", val1, val2);
	swap(p1, p2);
	printf("Valeur 1 : %d - Valeur 2 : %d", val1, val2);
}

void afficher_tab(double tableau[], int taille)
{
	for(int i = 0; i < taille; i++) {
		printf("Element %d : %lg\n", i+1, tableau[i]);
	}
}

double somme_tableau(double tableau[], int taille)
{
	double somme = 0;
	for(int i = 0; i < taille; i++) {
		somme = somme + tableau[i];
	}
	return somme;
}

double celsius_to_farhenheit(double temp_celsius)
{
	return (1.8 * temp_celsius) + 32;
}

double farhenheit_to_celsius(double temp_farhenheit)
{
	return (temp_farhenheit - 32) / 1.8;
}

void exo8()
{
	int choix;
		printf("Que souhaitez vous convertir :\n1) Degres Celsius en Farhenheit\n2) Farhenheit en degres Celsius\n\n");
		scanf("%d", &choix);
		if(choix == 1) {
			printf("Entrez une temperature en degres Celsius :\n\n");
			double temp_celsius;
			scanf("%lf", &temp_celsius);
			printf("%Temperature en Farhenheit : %lf\n\n", celsius_to_farhenheit(temp_celsius));
		} else if(choix == 2) {
			printf("Entrez une temperature en Farhenheit :\n\n");
			double temp_farhenheit;
			scanf("%lf", &temp_farhenheit);
			printf("Temperature en Celsius : %lf\n\n", farhenheit_to_celsius(temp_farhenheit));
		} else {
			printf("Erreur de choix");
		}
}

void exobonus()
{
	int nbvaleurs;
	printf("Combien de valeurs souhaitez vous entrer ?\n");
	scanf("%d", &nbvaleurs);
	printf("A present, entrez vos valeurs espacées d'un espace :\n");
	int tab[nbvaleurs];
	int compteur_pair = 0;
	int compteur_impair = 0;
	for(int i = 0; i < nbvaleurs; i++) {
		scanf("%d ", &tab[i]);
		if(exo4b(tab[i])) {
			compteur_pair++;
		} else {
			compteur_impair++;
		}
	}
	int tab_pair[compteur_pair];
	int tab_impair[compteur_impair];
	printf("%d   ", compteur_pair);
	printf("%d", compteur_impair);
	for(int i = 0; i < nbvaleurs; i++) {
		
	}
}

void exogit()
{
	printf("salut !");
}

int main(int argc, char **argv)
{
	//exo1();
	//exo2a();
	//exo2b();
	//exo3a();
	//exo3b();
	//printf("%d", exo4a(1));
	//printf("%d", exo4b(3));
	//exo5();
	//exo6(6, 12);
	//double tableau[] = { 2.3, 3.5, 3.6, 6.4, 7.5 };
	//afficher_tab(tableau, 5);
	//printf("Somme des elements du tableau : %lg", somme_tableau(tableau, 5));
	//exo8();
	//int tab[8];
	//printf("%d", sizeof(tab));
	exobonus();
	return 0;
}
