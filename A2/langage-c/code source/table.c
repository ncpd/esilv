#include "table.h"
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

table * allouer_table(int taille)
{
	table * tabl = (table*) malloc(sizeof(table));
	if(tabl != NULL) {
		tabl->taille = taille;
		tabl->_table = (vecteur*) malloc(taille*sizeof(vecteur));
		for(int i = 0; i < taille; i++) {
			(tabl->_table[i]) = *allouer_vecteur();
		}
	}
	return tabl;
}

long long puissance(long long number, int pow) // long long en cas de base de grande taille
{
	long long result = 0;
	if(pow == 0) {
		result = 1;
	} else {
		result = number * puissance(number, pow - 1);
	}
	return result;
}

long long modulo(long long number, long long diviseur) // long long pour gérer la somme (très grande)
{
	long long num = number - diviseur * (number / diviseur);
	return num;
}

char * formatNom(char * nom)
{
	size_t len = strlen(nom);
	size_t i;
	for(i = 0; i < len; i++) {
		if(nom[i] >= 'a' && nom[i] <= 'z') { // Caractère bien compris entre 'a' et 'z'
			nom[i] = nom[i] - 'a' + 'A'; // -32 en decimal
		}
	}
	return nom;
}

char * formatPrenom(char * prenom)
{
	size_t len = strlen(prenom);
	size_t i;
	if(prenom[0] >= 'a' && prenom[0] <= 'z') {
		prenom[0] = prenom[0] - 'a' + 'A'; // Initiale en majuscule
	}
	for(i = 1; i < len; i++) {
		if(prenom[i] >= 'A' && prenom[i] <= 'Z') { // Caractère bien compris entre 'A' et 'Z'
			prenom[i] = prenom[i] + 'a' - 'A'; // +32 en decimal
		}
		if(prenom[i - 1] == '_' && prenom[i] >= 'a' && prenom[i] <= 'z') { // majuscule après '_'
			prenom[i] = prenom[i] - 'a' + 'A';
		}
	}
	return prenom;
}

char * concat(const char * nom, const char * prenom)
{
	char * name = (char*) malloc((4 + 1) * sizeof(char)); // 4 premières lettres du nom
	char * fname = (char*) malloc((2 + 1) * sizeof(char)); // 2 premières lettres du prénom
	char * str = (char*) malloc((6 + 1) * sizeof(char)); // concaténation
	
	strncpy(name, nom, 4);
	*(name + 4) = '\0';
	strncpy(fname, prenom, 2);
	*(fname + 2) = '\0';
	strcpy(str, name);
	strcat(str, fname);
	
	*(str + 6) = '\0';
	return str;
}

int indexFonctionnaire(char * nom, char * prenom, int base, int taille)
{
	long index = -1;
	if(taille > 0 && nom != NULL && prenom != NULL && base > 0) {
		char * str = concat(nom, prenom);
		long long sum = 0;
		for(int i = 0; str[i] != '\0'; i++) { // calcul de la somme
			sum = sum + ((long long)str[i] * puissance((long long)base, i));
		}
		index = (int) modulo(sum, (unsigned long)taille);
	}
	return index;
}

bool addToTable(char * nom, char * prenom, int salary, unsigned long base, table * tabl, int taille)
{
	bool b = false;
	if(nom != NULL && prenom != NULL && base > 0 && salary > 0) {
		int ind = indexFonctionnaire(nom, prenom, base, taille);
		vecteur * vec = &(tabl->_table[ind]); // vecteur i où ajouter le nouveau fonctionnaire
		b = ajouterFonctionnaire(vec, nom, prenom, salary);
	}
	return b;
}

bool deleteFromTable(char * nom, char * prenom, unsigned long base, table * tabl, int taille)
{
	bool b = false;
	if(nom != NULL && prenom != NULL && base > 0) {
		int ind = indexFonctionnaire(nom, prenom, base, taille);
		vecteur * vec = &(tabl->_table[ind]); // vecteur i depuis lequel il faut supprimer le fonctionnaire
		b = supprimerFonctionnaire(vec, nom, prenom);
	}
	return b;
}