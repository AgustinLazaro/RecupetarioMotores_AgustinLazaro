
https://agustinlazaro.itch.io/

https://github.com/AgustinLazaro


https://www.mixamo.com/#/?page=1&query=bot&type=Character
=========================================================
ENTREGA RECUPERATORIO: SHOOTER DE COBERTURA
Alumno/a: [Agustin lazaro Blanco Romero]


A continuación se detalla el estado de los requerimientos "Mínimos para aprobar".

Mínimo para aprobar:
○ Primera Persona. SI
○ Movimiento con WASD. SI
○ Salto con Espacio. SI
○ Agacharse con Ctrl. SI
○ Correr con Shift. SI
○ Control de cámara con el mouse. SI
○ Disparo con click.
○ Cursor Bloqueado y Lockeado. SI


 Mínimo para aprobar:
○ Una columna donde el jugador empiece y que el enemigo no lo vea. SI
○ Una caja (Cover) ubicada entre el jugador y el enemigo. SI
○ El enemigo detecta al jugador solo si ésta lo vé. SI
○ El enemigo debe tener una barra de vida. SI

MInimo para aprobar:
○ Al detectar al jugador, el enemigo entra en un estado de "apuntado" durante X segundos. SI
○ Al cumplirse el tiempo de apuntado, dispara y le quita vida al jugador. SI
○ Si el jugador vuelve a taparse durante el apuntado, el enemigo debería cancelar el disparo (no
disparar "a ciegas"). SI
○ Cooldown de 1 segundo entre disparos, para que no sea una ametralladora. SI
El enemigo muere al llegar a 0 de vida: SI

MInimo para aprobar:
 Hit markers en pantalla al acertar un disparo. SI
Indicador de vida del jugador en pantalla (barra). SI
Componente “Health” reutilizable para Player y Enemy. SI
Barra de vida flotante sobre el enemigo. SI
○ Modelos 3D reales para Player y Enemy (nada de cápsulas/cubos para ellos). SI
○ Animator Controller con, al menos: Idle, Caminar, Agacharse, Saltar, Disparar y Morir. SI (menos la de morir )

 Implementaciones que suman:
○ Blend Tree para el movimiento del jugador WASD + Shift. SI
○ Capas de Animator (Layers) para poder disparar mientras camina, sin cortar la animación de
piernas. (creo)
Implementaciones que suman:
 Scripts modulares y reutilizables: “Health”, “Shooter”, “PlayerController”, “EnemyAI”. SI
○ Respeto de la regla de “RequireComponent” en todo “GetComponent” a self. SI
○ Cero singletons salvo un eventual GameManager. SI
○ Configuración de arma/enemigo por “ScriptableObject” (daño, cadencia, tiempo de apuntado) 
en vez de todo hardcodeado en el Inspector. SI
○ UI manejada por Observer. SI
○ Instancias manejadas por Pool. SI

● Mínimo para aprobar:
○ Piso, una caja de cobertura y algún límite del área (paredes o simplemente un espacio
acotado). Acá sí está permitido usar primitivas. SI
● Implementaciones que suman:
○ Más de un punto de cobertura, para probar que la detección funcione en distintas posiciones. SE PODRIA DECIR QUE SI 
