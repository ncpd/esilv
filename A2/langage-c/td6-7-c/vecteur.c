#include "table.h"
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

vecteur * allouer_vecteur()
{
	vecteur * v = (vecteur*) malloc(sizeof(vecteur));
	if(v != NULL) {
		v->tailleLogique = 0;
		v->vec_fonctionnaires = NULL;
	}
	return v;
}

bool isNullOrEmpty(vecteur const * v)
{
	bool b = true;
	if(v != NULL) {
		if(v->tailleLogique > 0 && v->vec_fonctionnaires != NULL) {
			b = false;
		}
	}
	return b;
}

bool haveConflict(vecteur const * v)
{
	bool b = false;
	if(v != NULL) {
		if(v->tailleLogique > 1 && v->vec_fonctionnaires != NULL) {
			b = true;
		}
	}
	return b;
}

bool fonctionnaireExists(vecteur * vec, fonctionnaire * fonc)
{
	bool existence = false;
	if(fonc != NULL) {
		if(vec->vec_fonctionnaires != NULL) {
			if(vec->tailleLogique > 0) {
				for(int i = 0; i < vec->tailleLogique; i++) {
					fonctionnaire * f = &(vec->vec_fonctionnaires[i]);
					if(equals(f, fonc)) {
						existence = true;
					}
				}
			}
		}
	}
	return existence;
}

void triAlpha(fonctionnaire * vecFonctionnaires, int nbFonc)
{
	fonctionnaire temp; 
	for (int i = 0; i < nbFonc; i++) {
		for (int j = 0; j < nbFonc; j++) {
			if (strcmp(vecFonctionnaires[i].nom, vecFonctionnaires[j].nom) < 0) {
					temp = vecFonctionnaires[i];
					vecFonctionnaires[i] = vecFonctionnaires[j];
					vecFonctionnaires[j] = temp;
			}
		}
	}
}

bool ajouterFonctionnaire(vecteur * v, char * nom, char * prenom, int salaire)
{
	bool ajout = false;
	fonctionnaire * newFonctionnaire = allouer_fonctionnaire(nom, prenom, salaire);
	if(v != NULL) {
		if(!fonctionnaireExists(v, newFonctionnaire)) {
			fonctionnaire * new_contenu = (fonctionnaire*) malloc((v->tailleLogique + 1) * sizeof(fonctionnaire)); // nouveau contenu de taille n + 1
			if(new_contenu != NULL) {
				unsigned int i;
				for(i = 0 ; i < v->tailleLogique ; i++) // copie des anciens éléments
				{
					fonctionnaire t1 = v->vec_fonctionnaires[i];
					new_contenu[i] = t1;
				}
				new_contenu[i] = *newFonctionnaire; // ajout du nouvel élément
				v->tailleLogique++;
				free(v->vec_fonctionnaires); // libération mémoire de l'ancien contenu
				v->vec_fonctionnaires = new_contenu;
				ajout = true;
				triAlpha(v->vec_fonctionnaires, v->tailleLogique);
			}
		}
	}
	return ajout;
}

bool supprimerFonctionnaire(vecteur * v, char * nom, char * prenom)
{
	bool suppr = false;
	fonctionnaire * newFonctionnaire = allouer_fonctionnaire(nom, prenom, -1); // pour la verification du nom et prenom uniquement (comparaison de types * fonctionnaires)
	if(v != NULL) {
		if(fonctionnaireExists(v, newFonctionnaire)) {
			if(v->tailleLogique > 1) {
				if(v->vec_fonctionnaires != NULL) {
					unsigned int i;
					for(i = 0 ; i < v->tailleLogique ; i++) {
						fonctionnaire t1 = v->vec_fonctionnaires[i];
						if(equals(newFonctionnaire, &t1)) {
							for(int j = i; j < v->tailleLogique - 1; j++) { // decalage des valeurs après celle à supprimer
								if(j < v->tailleLogique - 1) {
									v->vec_fonctionnaires[j] = v->vec_fonctionnaires[j+1];
								} else {
									free(&(v->vec_fonctionnaires[j])); // si l'élément à supprimer est le dernier élément du tableau
								}
							}
						}
					}
					v->tailleLogique--;
					suppr = true;
					triAlpha(v->vec_fonctionnaires, v->tailleLogique);
				}
			} else {                                          // Cas particulier : 1 seul élément dans le vecteur
				free(&(v->vec_fonctionnaires[0]));
				free(v->vec_fonctionnaires);
				v->vec_fonctionnaires = NULL;
				v->tailleLogique = 0;
				suppr = true;
			}
		}
	}
	return suppr;
}