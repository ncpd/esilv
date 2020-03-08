#include <stdio.h>
#include "vecteur.h"

vector* create_vector()
{
	vector * vec = malloc(sizeof(vector));
	if(vec != NULL) {
		vec->nbElements = 0;
		vec->contenu = NULL;
	}
	return vec;
}

bool is_null_or_empty(vector const * p_vec)
{
	bool b = false;
	if(p_vec == NULL || p_vec->contenu == NULL) {
		b = true;
	}
	return b;
}

bool add(double element, vector* p_vec)
{
	bool b = true;
	double * content = malloc(sizeof(p_vec->contenu) + sizeof(double));
	for(int i = 0; i < p_vec->nbElements; i++) {
		*(content + i) = *(p_vec->contenu + i); //copie si éléments existants
	}
	(p_vec->nbElements)++;
	*(content + p_vec->nbElements - 1) = element;
	p_vec->contenu = content;
	if(p_vec->contenu == NULL) {
		b = false;
	}
	return b;
}

unsigned int size(vector const * p_vec)
{
	if(p_vec != NULL && p_vec->contenu != NULL) {
		return p_vec->nbElements;
	} else {
		return 123456;
	}
}

double* element_at(unsigned int index, vector const * p_vec)
{
	if(p_vec != NULL && p_vec->contenu != NULL && index < p_vec->nbElements) {
		return &(p_vec->contenu[index]);
	}
}

void delete_vector(vector * p_vec)
{
	if(p_vec != NULL) {
		free(p_vec->contenu);
		free(p_vec);
	}
}

void afficher_vecteur(vector* vec)
{
	if(!is_null_or_empty(vec)) {
		int taille = size(vec);
		for(int i = 0; i < taille; i++) {
			double elt = *element_at(i, vec);
			printf("%lg\n", elt);
		}
	}
	printf("\n");
}

bool remove_at(unsigned int index, vector* p_vec)
{
	bool b = false;
	if(p_vec != NULL) {
		for(int i = index; i < p_vec->nbElements - 1; i++) {
			p_vec->contenu[i] = p_vec->contenu[i + 1];
		}
		p_vec->contenu[p_vec->nbElements] = -1;
		p_vec->nbElements -= 1;
		b = true;
	}
	return b;
}

void exo2()
{
	vector* vecteur = create_vector();
	
	add(1.0, vecteur);
	add(2.0, vecteur);
	add(3.0, vecteur);
	add(4.0, vecteur);
	add(5.0, vecteur);
	afficher_vecteur(vecteur);
	
	remove_at(5, vecteur);
	//remove_at(2, vecteur);
	afficher_vecteur(vecteur);
	
	add(6.0, vecteur);
	afficher_vecteur(vecteur);
	
	//delete_vector(vecteur);
	vecteur = NULL;
}


int main(int argc, char **argv)
{
	exo2();
	return 0;
}
