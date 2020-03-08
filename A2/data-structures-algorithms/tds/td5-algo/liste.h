#ifndef LISTE_H
#define LISTE_H

struct _element
{
	double elt;
	struct _element * next;
};
typedef struct _element element;

element * inserer_en_tete(element * liste, double valeur);

element * inserer_en_queue(element * liste, double valeur);

element * supprimer_en_tete(element * liste);

element * supprimer_en_queue(element * liste);

element * acceder_element(element * liste, int position);

element * ajouter_element(element * liste, int position, double valeur);

element * supprimer_element(element * liste, int position);

element * effacer_liste(element * liste);

element * recherche(element * liste, double val);

element * ajout_bis(element * liste, double valeur);

element * supress_bis(element * liste);

#endif