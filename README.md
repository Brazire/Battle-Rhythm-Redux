# Battle-Rhythm

## Système de combat
Le système de combat a été entièrement refait. Le joueur doit accumulé des points en jouant à un jeu de rhytme pour pouvoir effectuer des actions. Le joueur peut:
 - Changer de cible
 - Effectuer un attaque sur la cible
 - Sélectionner une compétence
Des attaques spécials comme cacher une partie du jeu de rhytme et des notes piégés ont été ajouté aux ennemis pour nuire au joueur.

### Ennemi normal
3 types d'ennemi différent avec leur propre sprite ont été ajouté. Les ennemis attaques le joueur après un certain temps à intervale régulier.

Les enemies sont organiser de cette manière:
    Chaque enemies est un Entity, qui permet des les instantier dans le même parent.
    Chaque enemies détiennent des attaques qu'ils peuvent executer.
    Un enemies peut executer des attaques à chaque quelque seconds, un temp variant selon l'enemie.

### Boss
2 boss avec le même sprite, mais avec des attaques unique ont été ajouté. Chacun des boss a un IA qui joue lui aussi au jeu de rhytme en même temps que le joueur. Les boss doivent aussi accumulé des point avant de pouvoir faire des actions.

### Show Off
Il s'agit ici d'une méchanique unique au combat de boss. Celle-ci permet au joueur d'entrer dans une sous-section de jeu ou il doit:

    - Accumuler un combo en appuyant les touche musical de manière consécutif, sans erreur.
    - Avoir un combo plus elever que le boss après 15 note.

L'entité ayant le plus grand combo à la fin frappe l'adversaire avec beaucoup de dégat. 

## Système d'inventaire & magasin
Un interface d'inventaire ainsi qu'un magasin a été ajouté. Avec de la monnaie qu'il peut récupérer en gagnant des combats, il est possible pour le joueur de se procurer 3 types d'objets: 
 - de l'équipement
 - des consomables
 - des objets clés

## PNJ
Des personnages non-jouable ont été ajouté. Il est possible pour le joueur de parler avec les PNJ lorsqu'il est asser près d'eux. Il est aussi possible de sélectionner des choix de dialogue.

## Cinématique
La possibilité d'avoir des cinématiques a été ajouté. Une cinématique est un mélange de conversations avec des PNJ et de déplacements. Elle se déclanche lorsque le joueur entre dans sa zone de collision.

## A venir

    - Revoir le système de rhythm pour:
        - Ajouter des type de note
        - Revoir comment suivre la rhythme de la chanson.
        - Permettre d'avoir des fichiers de note pour chaque chanson et avoir plus de contrôle sur les notes.

    - Ajouter un tutoriel au mode de combat pour expliquer au joueur les différentes méchaniques.




