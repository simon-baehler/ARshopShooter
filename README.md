# ARshopShooter
Ce projet a été réalisé dans le catre du travail final de bachelor filière infomratique du 28 fevrier au 28 juillet 2017.

Candidat : Baehler Simon

Responsable Ehrensberger Jurgen

Date de dernière modification : 11 juillet 2017


# Introduction

L'apparition de nouvelles technologies comme les casques de réalité augmentée / réalité virtuelle, a attiré les technophiles mais également les plus néophytes voyant déjà dans ces "gadgets" l'apparition d'un Saint Graal, ou le premiers pas vers l'armure d'Iron Man ou à d'autres films (la liste est longue) de science-fiction où l'humain est entièrement connecté à la machine.

Mais où en sommes-nous aujourd'hui ? Actuellement quelles sont les possibilités d'une telle technologie dans le cadre de formation ? Nous aurons certes un parti pris axé sur la formation très pratique, celle de la formation de policiers dans le cadre de scénario de crise, mais rien ne nous empêche de nous pose la question de manière plus large, comme l'utilisation de la réalité augmenté / réalité virtuelle dans le cadre de formation plus théorique tel que l'anatomie ou l'astronomie.

La réponse à ces questions s'appuie essentiellement sur l'analyse des expériences personnelles qui ont eu lieux tout au long du projet, et qui devrait donc permettre d'évaluer l'utilité du \Hololens ou de manière plus générale, l'utilité de la réalité virtuelle. L'exploitation de ces expériences devait permettre de répondre à une série d'interrogations inhérentes au sujet tel que : L'immersion dans la simulation est-elle satisfaisante ? Est-il possible de réaliser une simulation proche de la réalité ?

Hypothétiquement parlant nous pouvons répondre à la question seulement grâce à son oxymore qui en fait partie intégrante : réalité augmenté / réalité virtuelle. D'un autre bord les caques réalité augmenté / réalité virtuelle permettraient l’immersion quasi-totale dans un nouvel environnement, inconnu, et donc toucher des doigts des scénarios rares mais possibles.


# Résumé


Ce travail de Bachelor s’intéresse à la formation de policiers dans des scénarios de crise. Ce travail de Bachelor possède donc deux objectifs bien distincts. Le premier objectif est la réalisation d'un outils/application de formation pour la gestion de scénario de crise pour la police. Le second objectif, est un travail de recherche visant à explorer le Hololens en exploitant ces capacités techniques et à évaluer si oui ou non le Hololens est un outils viable à la formation de policer pour des scénario de crise, mais également dans un sens plus large, celui de l'évaluation de la viabilité de la réalité augmentée/ réalité virtuelle dans le cadre de la formation de policer.

Cette application est présentée sous la forme d'une simulation en vue à la première personne dans un espace virtuel en trois dimensions. Le but de cette simulation est d'avoir une immersion et un rapprochement de la réalité maximal. La simulation prend place dans un supermarché, où plusieurs civil se déplacent en marchant et prennent peur à la vue du forcené/shooter. Le travail du policer n'est pas uniquement d'arrêter le forcené/shooter mais également de gérer l'évacuation des civils. L'interaction entre le policer et les intelligences artificielles peut s'effectue de manière gestuelles et/ou vocales.

Au cours de ce projet l'outil utilisé a été Unity, un moteur de jeux, ainsi qu'un plug-in du nom de RainAI pour la gestion de intelligences artificielles. Pour la question du langage, seul le C# a été utilisé.

# Manuel d'installation
## Installation des logiciels
Le déploiement de l'application se fait en deux temps, dans un premier temps sous Unity et dans un second sous Visual Studio. Les indispensables pour un déploiement de A à Z sont les
suivants :

* [Unity](https://store.unity.com/download?ref=personal)
  * .NET Scripting Backend
  * IL2CPP Scripting Backend
* [Visual Studio](https://www.visualstudio.com/fr/downloads/)
  *  Windows 10 SDK
  *  Unity ToolKit
* [Hololens Emulator (Optionnel)](http://go.microsoft.com/fwlink/?LinkID=823018)


Dans un premier lieux, lisez et acceptez les conditions générales d'utilisation. Ensuite, installez Unity  AVEC .NET Scripting Backend et IL2CPP Scripting Backend. Il est impératif que ces composants soit installé, si non, lors du build avec Unity des erreurs seront levées.

Passez ensuite à l'installation de Visual Studio. Lisez attentivement à nouveau les conditions générales d'utilisation et acceptez les, puis lancez l'installation Il est impératif d'avoir le SDK de Windows 10 installé ainsi que le ToolKit de développement pour Unity

\begin{figure}[h]
   \caption{\label{étiquette} Installation Visual Studio}
\centerline{\includegraphics[scale=0.4]{images/installation.PNG}}
\end{figure}

## Installation de l'emulateur
Le Hololens coûtant relativement chère (chose qui pourrait contraindre les développeur à laisser tomber le Hololens), l'utilisation un emulateur rend donc possible le développement accessible à tous.

Notez une chose avant de tenter de déployer l'application sur l'emulateur : Il faut un nombre conséquent de RAM disponible. Si vous n'avez pas Windows 10 Pro, l'émulateur HoloLens ne fonctionnera pas car il a besoin qu'Hyper-V soit activé.

Suivez l'installation en laissant les paramètres par défaut, assurez-vous juste que lors de la dernière étape que Microsoft Hololens Emulator et Microsoft Hololens App Templates soient bien cochés.


## Déploiement de l'application

Le déploiement de l'application commence sur Unity. Un fois le projet ouvert sur Unity allez dans :

* File -> Build Settings
* Assurez vous que Windows Store soit bien selectionné (Logo Unity en regard)
* Définissez le SDK à Universal 10
* UWP Build type} doit etre à D3D
* UWP SDK} doit etre à Last installed
* Unity C# Project doit etre coché


Avant de lancer le buid, allez dans \textit{Player Setting} et assurez vous que :

* Virtual Reality Supported soit bien coché
* Virtual Reality SDK soit bien Windows Holographic
* Scripting backend} soit bien IL2CPP
* API compatibility level soit bien .Net 2.0


Pour terminer cliquez sur build et créez un dossier du nom de App dans le quel le Build va se faire.

Une fois le build terminé, allez dans le dossier App et cliquez sur .sln, normal visual studio se lance automatiquement.

Pour un déploiement sur Hololens, branchez le Hololens en USB. Pour la configuration de déploiement choisissez Release, x86 et Device
