#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include "fonctionnaire.h"

fonctionnaire * allouer_fonctionnaire(char const * nom, char const * prenom, int salaire)
{
	fonctionnaire * f = (fonctionnaire*) malloc(sizeof(fonctionnaire));
	if(f != NULL) {
		char * _nom = (char*) malloc(sizeof(nom));
		char * _prenom = (char*) malloc(sizeof(prenom));
		strcpy(_nom, nom);
		strcpy(_prenom, prenom);
		f->nom = _nom;
		f->prenom = _prenom;
		f->salaire = salaire;
	}
	return f;
}

bool equals(fonctionnaire * f1, fonctionnaire * f2)
{
	int verif = 0;
	bool b = false;
	verif = (strcmp(f1->nom, f2->nom) == 0) ? 1 : 0;
	verif += (strcmp(f1->prenom, f2->prenom) == 0) ? 1 : 0;
	//verif += (f1->salaire == f2->salaire) ? 1 : 0;
	if(verif == 2)//3) {
	{
		b = true;
	}
	return b;
}