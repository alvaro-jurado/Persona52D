<h1>Persona52D</h1>

Dentro de este documento se detalla el proyecto que incluye la aventura point and click basada en Persona 5. Se describirá el sistema de carpetas de este repositorio y como acceder a las distintas escenas.

<h1>1. Sistema de carpetas de Assets</h1>

1. **Prefabs:** Se encuentran los Prefabs utilizados para el item del inventario y para el controlador de escenas.
2. **Resources:** Se encuentran todos los archivos externos que aportan diseño al juego (Sprites, Animations, Audio Sources, Cutscenes, etc.).
3. **Scenes:** Carpeta donde se almacenan las escenas del juego.
4. **Scripts:** Se encuentran todos los archivos de scripts c# que dan funcionalidad a los elementos del juego.
5. **Settings:** Sirve para almacenar y organizar archivos de configuración específicos del proyecto.
6. **TextMesh Pro:** Esta carpeta contiene archivos y recursos necesarios para que TextMesh Pro funcione correctamente y permite un mejor control sobre la apariencia y el rendimiento del texto.

<h1>2. Orden del juego (Scene Progression)</h1>

<h2>1. Leblanc</h2>
Es una escena donde simplemente tendremos unos dialogos de introducción y una cinematica que se puede saltar.

<h2>2. SaePalace</h2>
Es una escena de introducción al minijuego de la siguiente.

<h2>3. RouletteScene</h2>
En esta escena tenemos que jugar a la ruleta apostando 100 fichas en cada tirada, el objetivo es llegar a 800. Empezamos con 500 y si llegamos a 0 perdemos e iremos a la escena GameOverScene.

<h2>4. SecretArea</h2>
Es una escena donde debemos encontrar un objeto que tendrá un código para abrir la puerta con el terminal para acceder a la siguiente escena.

<h2>5. CombatScene</h2>
Es una escena en la que tendremos unos dialogos al empezar y al clicar en el enemigo (Sae) empezaremos un combate por turnos. En esta escena si ganamos iremos a la escena WinScene, si perdemos iremos a la escena GameOverScene.

<h2>6. WinScene</h2>
Es simplemente una escena con el diseño del final de Persona 5

<h2>7. GameOverScene</h2>
Si perdemos tanto en la ruleta como en el combate llegaremos a la Velvet Room y tendremos un boton para volver a intentar la escena anterior. Esto se hace con un script que funciona como tracker de escena.

<h2>8. EasterEgg</h2>
Esta es una escena que es un final alternativo, este final es un final malo que se consigue tocando 10 veces al jugador (Joker) en la escena de SecretArea.
