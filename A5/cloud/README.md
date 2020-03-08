# Développement d'application Cloud

## Git
[Repository sur GitHub](https://github.com/nickxla/cloud)  
[Jeux de données](https://drive.google.com/drive/folders/1uzmHZzNd2DmUUwD7o2EGm4PhC0bjdjcE?usp=sharing)

## Cas d'usage

Les différents cas d'usage sont disponibles dans `/schemas`.
Le jeu de données utilisé est `Employee`, disponible [ici](https://relational.fit.cvut.cz/dataset/Employee).
Le rapport fourni est disponible dans `/reports`.

## Dénormalisation

Les modèles de dénormalisation sont également disponibles dans `/schemas`.
Les schémas Entité/Association y sont également.
Le rapport fourni est disponible dans `/reports`.

## Scalability

Le programme réalisé en python pour l'importation des données se situe dans `/scalability`.
Deux modules ont été réalisés afin de disposer d'opérations plus simples pour la mise à l'échelle (cf. `/scalability/utils`).
Le fichier `ids.txt` est une liste des numéros d'employés, afin de procéder à leur dump respectif.
Afin de lancer le script, il est nécessaire d'installer les dépendances avec la commande suivante:
```bash
python3 -m pip install -r requirements.txt
```
La traduction des requêtes est disponible dans `/scalability/requests`.
Les Unit Files correspondant aux services systemd sont disponibles dans `/systemd`.
Le rapport correspondant est également dans `/reports`.

## Application pour le cloud

L'application est décomposée en deux parties:
- un API afin d'interagir avec le routeur mongos
- un client React.js sous un serveur Nginx qui communique avec cet API

Le code de l'application est disponible dans `/application`. Un fichier expliquant l'installation y est présent.  
La démonstration vidéo de l'application est dans `/media`, on y montre les différentes fonctionnalités de l'application.  
Le jeu de données original est dans `/scalability/dumps`, tandis que le nouveau est `/scalability/employee.employees.json`.
