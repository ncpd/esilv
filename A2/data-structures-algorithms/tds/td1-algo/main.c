#include <stdio.h>

// ------ SOMME -----

int somme_iteratif(int n)
{
	int somme = 0;
	if(n >= 0) {
	for(int i = 0; i <= n; i++) {
		somme = somme + i;
	}
	} else {
		somme = -1;
	}
	return somme;
}

int somme_recursif(int n)
{
	int somme;
	if(n == 0) {
		somme = 0;
	} else if(n < 0) {
		somme = -1;
	} else {
		somme = n + somme_recursif(n-1);
	}
	return somme;
}

int somme_recursif_terminale(int n, int sum)
{
	int somme;
	if(n == 0) {
		somme = sum;
	}
	else {
		somme = somme_recursif_terminale(n-1,sum+n);
	}
	return somme;
}

void exo1()
{
	int n;
	int resultat;
	printf("Entrez un entier n :\n");
	scanf("%d", &n);
	
	resultat = somme_iteratif(n); // O(n)
	printf("Somme jusqu\'a %d, version iterative : %d\n\n", n, resultat);
	
	resultat = somme_recursif(n); // O(n)
	printf("Somme jusqu\'a %d, version recursive : %d\n", n, resultat);
}

void exo3()
{
	int n;
	int resultat;
	printf("Entrez un entier n :\n");
	scanf("%d", &n);
	
	resultat = somme_recursif_terminale(n,0); // O(n)
	printf("Somme jusqu\'a %d, version recursive terminale : %d\n\n", n, resultat);
}

// ------ FIBONACCI ------

int fibonacci_iteratif(int n)
{
	int premier = 0;
	int second = 1;
	int tmp;
	while(n--) {
		tmp = premier + second;
		premier = second;
		second = tmp;
	}
	return premier;
}

int fibonacci_recursif(int n)
{
	int fibo;
	if(n < 2) {
		fibo = n;
	} else {
		fibo = fibonacci_recursif(n-1) + fibonacci_recursif(n-2);
	}
	return fibo;
}

int fibonacci_recursif_terminal(int n, int resultat0, int resultat1)
{
	int resultat;
	if(n == 0) {
		resultat = resultat1; // Cas où n = 0 : f(0) = 0
	} else {
		resultat = fibonacci_recursif_terminal(n-1, resultat1, resultat0+resultat1); // n-1, le second paramètre passe premier et la somme des deux deuxieme parametre
	}
	return resultat;
}

void exo4_5()
{
	int n;
	printf("Entrez un entier n :\n");
	scanf("%d", &n);
	printf("Fibonacci iteratif : %d\n\n", fibonacci_iteratif(n));
	printf("Fibonacci recursif : %d\n\n", fibonacci_recursif(n));
	printf("Fibonacci recursif terminal : %d\n\n", fibonacci_recursif_terminal(n,1,0));
}

// ------ FACTORIELLE ------

int factorielle_iteratif(int n)
{
	int facto = 1;
	if(n >= 1) {
		for(int i = 1; i <= n; i++) {
			facto = facto * i;
		}
	}
	return facto;
}

int factorielle_recursif(int n)
{
	int facto;
	if(n <= 1) {
		facto = 1;
	} else {
		facto = n * factorielle_recursif(n-1);
	}
	return facto;
}

int factorielle_recursif_terminal(int n, int a)
{
	int facto;
	if(n <= 0) {
		facto = a;
	} else {
		facto = factorielle_recursif_terminal(n - 1, n * a);
	}
	return facto;
}

void exo6()
{
	int n;
	printf("Entrez un entier n :\n");
	scanf("%d", &n);
	printf("Factorielle iteratif : %d\nFactorielle recursif : %d\nFactorielle recursif terminal : %d\n", factorielle_iteratif(n), factorielle_recursif(n), factorielle_recursif_terminal(n, 1));
}

int main(int argc, char **argv)
{
	//exo1();
	//exo3();
	//exo4_5();
	exo6();
	return 0;
}
