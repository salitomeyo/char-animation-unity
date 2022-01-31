# Proyecto final introduccion a la creacion de videojuegos

Creado en la version 2021.1.12f1 usando el template > universal render pipeline

En este repositorio se guardaran y animaran los personajes que se usaran dentro del proyecto final de la materia introduccion a la creacion de videojuegos, cabe destacar que se esta intentando realizar las animaciones desde 0 usando el package del unity registry "Animation Rigging"

Actualmente la animacion del personaje 'Character_Chost' provoca que se salga del mapa, esto se puede evitar quitando el Target dentro del componente Multi-Aim constrain del inspector del gameObject Character_Chost > Rig 1 > HeadAim 

Actualmente atribuimos este comportamiento extraño a el hecho de que este personaje no contenia una animacion idle por defecto como los demas que se estan usando dentro de la escena 'Luigi-mansion' ya que por ejemplo el personaje 'Free Burrow' tiene la misma configuracion del componente Multi-Aim pero no se cae del mapa.

deshacer.