# Battle-Rhythm

## Système de combat
Le système de combat a été entièrement refait. Le joueur doit accumuler des points en jouant à un jeu de rythme pour pouvoir effectuer des actions. Le joueur peut:
 - Changer de cible
 - Effectuer une attaque sur la cible
 - Sélectionner une compétence
Des attaques spéciales comme cacher une partie du jeu de rythme et des notes piégées ont été ajouté aux ennemis pour nuire au joueur.

### Ennemi normal
3 types d'ennemi différents avec leur propre sprite ont été ajouté. Les ennemis attaquent le joueur après un certain temps à intervalles réguliers.

Les ennemis sont organisés de cette manière:
    Chaque ennemi est un Entity, qui permet de les instancier dans le même parent.
    Chaque ennemi détient des attaques qu'il peut exécuter.
    Un ennemi peut exécuter des attaques toutes les quelques secondes, un temps variant selon l'ennemi.

### Boss
2 boss avec le même sprite, mais avec des attaques uniques ont été ajouté. Chacun des boss a un IA qui joue lui aussi au jeu de rythme en même temps que le joueur. Les boss doivent aussi accumuler des points avant de pouvoir faire des actions.

### Show Off
Il s'agit ici d'une mécanique unique au combat de boss. Celle-ci permet au joueur d'entrer dans une sous-section de jeu où il doit:
 - Accumuler un combo en appuyant sur les touches musicales de manière consécutive, sans erreur.
 - Avoir un combo plus élevé que le boss après 15 notes.

L'entité ayant le plus grand combo à la fin frappe l'adversaire avec beaucoup de dégâts. 

## Système d'inventaire & magasin
Une interface d'inventaire ainsi qu'un magasin a été ajouté. Avec de la monnaie qu'il peut récupérer en gagnant des combats, il est possible pour le joueur de se procurer 3 types d'objets: 
 - de l'équipement
 - des consommables
 - des objets clés

## PNJ
Des personnages non jouables ont été ajoutés. Il est possible pour le joueur de parler avec les PNJ lorsqu'il est assez près d'eux. Il est aussi possible de sélectionner des choix de dialogue.

## Cinématique
La possibilité d'avoir des cinématiques a été ajoutée. Une cinématique est un mélange de conversations avec des PNJ et de déplacements. Elle se déclenche lorsque le joueur entre dans sa zone de collision.

## À venir
 - Revoir le système de rythme pour:
  - Ajouter des types de notes
  - Revoir comment suivre le rythme de la chanson.
  - Permettre d'avoir des fichiers de notes pour chaque chanson et d'avoir plus de contrôles sur les notes.
 - Ajouter un tutoriel au mode de combat pour expliquer au joueur les différentes mécaniques.
