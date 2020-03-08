#include <stdio.h>
#include <stdlib.h>
#include <math.h>

enum _booleen { FALSE = 0 , TRUE = 1 };
typedef enum _booleen booleen;

struct _point
{
	double x;
	double y;
};
typedef struct _point point;

struct _rectangle
{
	point bsgauche;
	point bsdroit;
	point bigauche;
	point bidroit;
	double aire;
};
typedef struct _rectangle rectangle;

void exo1()
{
	booleen b1 = TRUE;
	booleen b2 = TRUE;
	
	if(b1 && b2) {
		printf("Les deux booleens sont vrais\n");
	} else {
		printf("Au moins un des deux booleens est faux\n");
	}
	printf("b1 : %d, b2 : %d\n", b1, b2);
}

booleen ordonner(int * ptr1, int * ptr2)
{
	booleen result;
	if(*ptr2 < *ptr1) {
		int tmp = *ptr2;
		*ptr2 = *ptr1;
		*ptr1 = tmp;
		result = TRUE;
	} else {
		result = FALSE;
	}
	return result;
}

void exo2()
{
	int x = 3;
	int y = 2;
	int * ptrx = &x;
	int * ptry = &y;
	printf("x : %d, y : %d\n", x, y);
	ordonner(ptrx,ptry);
	printf("Après :\nx : %d, y : %d", x, y);
}

int exo3()
{
	printf("Exo 3\n");
	printf("taille mémoire (en octets) pour stocker un entier : %lu\n", sizeof(int));
	printf("\n");
	
	int * p_entier = (int*) malloc( sizeof(int) ); // alloue 4 octets pour stocker un pointeur
	if( p_entier != NULL )
	{
		*p_entier = 42;
		printf("contenu de l'élément pointé : %d\n", *p_entier); // 42
	}
	
	printf("\n");
	int * p_elements = (int*) malloc( sizeof(int) * 2 ); // alloue 8 octets pour stocker un pointeur
	if( p_elements != NULL )
	{
		*p_elements = 12;
		printf("contenu du premier élément pointé : %d\n", *p_elements); //12
		printf("contenu du premier élément pointé (version bis) : %d\n", p_elements[0]); //12
		
		*(p_elements+1) = 34;
		printf("contenu du second  élément pointé : %d\n", *(p_elements+1)); //octet suivant
		printf("contenu du second  élément pointé (version bis) : %d\n", p_elements[1]);
	}
	
	printf("Libération mémoire\n");
	free(p_entier);
	free(p_elements);
	return 0;
}

int* nouveau_tableau_inverse(int tableau[], int taille)
{
	int * ptrTableau = malloc(taille * sizeof(int));
	if(ptrTableau != NULL) {
		int i = taille - 1;
		int j = 0;
		while(i >= 0) {
			ptrTableau[j] = tableau[i];
			i--;
			j++;
		}
	}
	return ptrTableau;
}

void afficheTableau(int tableau[], int taille)
{
	for(int i = 0; i < taille; i++) {
		printf("%d ", tableau[i]);
	}
	printf("\n");
}

void exo4()
{
	int tab[5] = { 1, 2, 3, 4, 5 };
	int length = 5;
	afficheTableau(tab, length);
	int * ptrTableau = nouveau_tableau_inverse(tab,length);
	if(ptrTableau =! NULL) {
		afficheTableau(ptrTableau, length);
		free(ptrTableau);
		ptrTableau = NULL;
	} else {
		printf("NULL");
	}
}

void afficherPoint(point pt)
{
	printf("(%lg,%lg)\n", pt.x, pt.y);
}

double distance(point pt1, point pt2)
{
	return sqrt((pt2.x - pt1.x)*(pt2.x - pt1.x) + (pt2.y - pt1.y)*(pt2.y - pt1.y));
} 

void exo5()
{
	point p1 = { 1.0 , 2.0};
	point p2 = { -3.0 , 4.0};
	afficherPoint(p1);
	afficherPoint(p2);
	printf("Distance entre p1 et p2 : %lg",distance(p1,p2));
}

void afficherPointV2(point * pt)
{
	printf("(%lg,%lg)\n", pt->x, pt->y);
}

double distanceV2(point * pt1, point * pt2)
{
	return sqrt((pt2->x - pt1->x)*(pt2->x - pt1->x) + (pt2->y - pt1->y)*(pt2->y - pt1->y));
}

void exo6()
{
	point p1 = { 1.0 , 2.0};
	point p2 = { -3.0 , 4.0};
	point * ptrP1 = &p1;
	point * ptrP2 = &p2;
	afficherPointV2(ptrP1);
	afficherPointV2(ptrP2);
	printf("Distance entre p1 et p2 : %lg",distanceV2(ptrP1,ptrP2));
}

void affichageRectangle(rectangle rec)
{
	point origine;
	origine.x = 0, origine.y = 0;
		while(origine.y < rec.bsgauche.y) {
		printf("\n");
		(origine.y)++;
	}
	while(origine.x < rec.bsgauche.x) {
		printf(" ");
		(origine.x)++;
	}
	printf("*");
	while(origine.x < rec.bsdroit.x) {
		printf(" ");
		(origine.x)++;
	}
	printf("*");
	while(origine.y < rec.bigauche.y) {
		printf("\n");
		(origine.y)++;
	}
	origine.x=0;
	while(origine.x < rec.bigauche.x) {
		printf(" ");
		(origine.x)++;
	}
	printf("*");
	while(origine.x < rec.bidroit.x) {
		printf(" ");
		(origine.x)++;
	}
	printf("*");
}

double aireRectangle(rectangle rec)
{
	double largeurRectangle = distance(rec.bsgauche, rec.bsdroit);
	double hauteurRectangle = distance(rec.bsgauche, rec.bigauche);
	return largeurRectangle*hauteurRectangle;
}

booleen isInside(rectangle rec, point p)
{
	booleen b = FALSE;
	if((p.x >= rec.bsgauche.x && p.x <= rec.bsdroit.x) && (p.y >= rec.bsgauche.y && p.y <= rec.bigauche.y)) {
		b = TRUE;
	}
	return b;
}

void exo7()
{
	point p1;
	p1.x = 3, p1.y = 4;
	point p2;
	p2.x = 22, p2.y = 4;
	point p3;
	p3.x = 3, p3.y = 10;
	point p4;
	p4.x = 22, p4.y = 10;
	rectangle rec;
	rec.bsgauche = p1, rec.bsdroit = p2, rec.bigauche = p3, rec.bidroit = p4;
	affichageRectangle(rec);
	printf("\n\n\nL\'aire du rectangle est %lg\n\n",aireRectangle(rec));
	point p;
	p.x = 8, p.y = 6;
	if(isInside(rec, p)) {
		printf("Le point (%lg,%lg) est a l\'interieur !\n", p.x, p.y);
	} else {
		printf("Le point est en dehors !\n");
	}
}

int findMinAire(rectangle tab[], int taille)
{
	int resultat = -1;
	double min = tab[0].aire;
	for(int i = 1; i < taille - 1; i++) {
		if(min > tab[i].aire) {
			resultat = i;
			min = tab[i].aire;
		}
	}
	return resultat;
}

void remplirTableauRectangles(rectangle tableau[], int taille)
{
	for(int i = 0; i < taille; i++) {
		point p1;
		p1.x = randomisateur(0,10), p1.y = randomisateur(0,10);
		point p2;
		p2.x = randomisateur(0,10000), p2.y = p1.y;
		point p3;
		p3.x = p1.x, p3.y = randomisateur(0,1000);
		point p4;
		p4.x = p2.x, p4.y = p3.y;
		rectangle rec;
		rec.bsgauche = p1, rec.bsdroit = p2, rec.bigauche = p3, rec.bidroit = p4;
		rec.aire = aireRectangle(rec);
		tableau[i] = rec;
	}
}

int randomisateur(int a, int b)
{
	return rand()%(b - a) + a;
}

void exo8()
{
	rectangle tableau[5];
	int taille = 5;
	remplirTableauRectangles(tableau, taille);
	printf("Aire du rectangle 1 : %lg\nAire du rectangle 2 : %lg\nAire du rectangle 3 : %lg\nAire du rectangle 4 : %lg\nAire du rectangle 5 : %lg\n", tableau[0].aire, tableau[1].aire, tableau[2].aire, tableau[3].aire, tableau[4].aire);
	printf("Le rectangle a la plus petite aire se situe a l\'index %d", findMinAire(tableau, taille) + 1);
}

void train()
{
	point origine;
	origine.x = 25;
	origine.y = 25;
	int r = 22;
	for(int y = -r; y < r; y++) {
		for(int x = -r; x < r; x++) {
			if((x * x) + (y * y) <= (r * r))  {
				printf("*");
			} else {
				printf(" ");
			}
		}
		printf("\n");
	}
}

int main(int argc, char **argv)
{
	//exo1();
	//exo2();
	//exo3();
	//exo4();
	//exo5();
	//exo6();
	//exo7();
	//exo8();
	train();
	return 0;
}
