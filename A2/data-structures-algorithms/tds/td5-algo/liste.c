#include <liste.h>
#include <stdlib.h>

element * inserer_en_tete(element * liste, double valeur)
{
	element * maillon = (element*) malloc(sizeof(element));
	if(maillon != NULL) {
		maillon->elt = valeur;
		maillon->next = liste;
		liste = maillon;
	}
	return liste;
}

element * inserer_en_queue(element * liste, double valeur)
{
	element * maillon = (element*) malloc(sizeof(element));
	if(maillon != NULL) {
		maillon->elt = valeur;
		maillon->next = NULL;
		if (liste == NULL)
        {
			return maillon;
        } else {
			element * maillon_tmp = liste;
			while(maillon_tmp->next != NULL) {
				maillon_tmp = maillon_tmp->next;
			}
			maillon_tmp->next = maillon;
			return liste;
		}
	}
}

element * supprimer_en_tete(element * liste)
{
	if(liste != NULL) {
		element * debut = liste->next;
		free(liste);
		return debut;
	} else {
		return NULL;
	}
}

element * supprimer_en_queue(element * liste)
{
	if(liste == NULL) {
		return NULL;
	} else if(liste->next == NULL) {
		free(liste);
		return NULL;
	} else {
		element * maillon_tmp = liste;
		element * maillon_pre_tmp = liste;
		while(maillon_tmp->next != NULL) {
				maillon_pre_tmp = maillon_tmp;
				maillon_tmp = maillon_tmp->next;
			}
		maillon_pre_tmp->next = NULL;
		free(maillon_tmp);
		return liste;
	}
}

element * acceder_element(element * liste, int position)
{
	for(int k = 0; k < position && liste->next != NULL; k++) {
		liste = liste->next;
	}
	if(liste == NULL) {
		return NULL;
	} else {
		return liste;
	}
}

element * ajouter_element(element * liste, int position, double valeur)
{
	if(liste == NULL) {
		return NULL;
	} else if(liste->next == NULL) {
		return NULL;
	} else {
		element * maillon_tmp = liste;
		element * maillon_pre_tmp = liste;
		for(int k = 0; k < position && liste->next != NULL; k++) {
			maillon_pre_tmp = maillon_tmp;
			maillon_tmp = maillon_tmp->next;
		}
		element * new_maillon = (element*) malloc(sizeof(element));
		if(new_maillon != NULL) {
			new_maillon->elt = valeur;
			new_maillon->next = maillon_tmp;
			maillon_pre_tmp->next = new_maillon;
		}
		return liste;
	}
}

element * supprimer_element(element * liste, int position)
{
	if(liste == NULL) {
		return NULL;
	} else if(liste->next == NULL) {
		return NULL;
	} else {
		element * maillon_tmp = liste;
		element * maillon_pre_tmp = liste;
		for(int k = 0; k < position && liste->next != NULL; k++) {
			maillon_pre_tmp = maillon_tmp;
			maillon_tmp = maillon_tmp->next;
		}
		maillon_pre_tmp->next = maillon_tmp->next;
		free(maillon_tmp);
		return liste;
	}
}

element * effacer_liste(element * liste)
{
	element * maillon_tmp = liste;
	element * maillon_tmp_next;
	while(maillon_tmp->next != NULL) {
		maillon_tmp_next = maillon_tmp->next;
		free(maillon_tmp);
		maillon_tmp = maillon_tmp_next;
	}
	return NULL;
}

element * recherche(element * liste, double val)
{
	element * tmp = liste;
	while(tmp->next != NULL) {
		if(tmp->elt == val) {
			return tmp;
		}
		tmp = tmp->next;
	}
	return NULL;
}

element * ajout_bis(element * liste, double valeur)
{
	if(liste == NULL) {
		return NULL;
	} else if(liste->next == NULL) {
		return NULL;
	} else {
		element * maillon_post = liste->next;
		element * new_maillon = (element*) malloc(sizeof(element));
		if(new_maillon != NULL) {
			new_maillon->elt = valeur;
			new_maillon->next = maillon_post;
			liste->next = new_maillon;
		}
		return liste;
	}
}

element * supress_bis(element * liste)
{
	if(liste == NULL) {
		return NULL;
	} else if(liste->next == NULL) {
		free(liste);
		return NULL;
	} else {
		element * maillon_post = (liste->next)->next;
		liste->next = maillon_post;
		free(liste->next);
		return liste;
	}
}