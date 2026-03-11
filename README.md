# 🐾 Toba Scripts

Scripts de Unity para **Toba**, un videojuego educativo de plataformas 2D diseñado para enseñar operaciones matemáticas (suma, resta, multiplicación y división) a niños a través del juego interactivo.

---

## 🎮 Descripción del Proyecto

**Toba** es un juego de plataformas 2D donde el jugador controla a Toba (un perrito) que recorre niveles llenos de obstáculos y enemigos. Cada enemigo representa un problema matemático: responder correctamente derrota al enemigo, mientras que una respuesta incorrecta le quita vida al jugador. El juego registra el desempeño del jugador por tipo de operación y adapta la dificultad conforme el jugador avanza.

---

## ✨ Características Principales

- 🧮 **Operaciones matemáticas** – Suma, resta, multiplicación y división con dificultad progresiva.
- 🏆 **Sistema de estrellas** – Hasta 3 estrellas por nivel según el desempeño del jugador.
- 🔒 **Progresión de niveles** – Niveles bloqueados que se desbloquean conforme el jugador avanza.
- 📊 **Estadísticas detalladas** – Registro de aciertos/errores y tiempo promedio por operación.
- 💾 **Persistencia de datos** – Progreso guardado en base de datos SQLite.
- 📱 **Soporte móvil** – Controles táctiles para Android e iOS.
- 🎓 **Generación de certificado** – Al completar todos los niveles se genera un certificado compartible.
- 🎵 **Sistema de audio** – Música de fondo y efectos de sonido para cada acción del juego.
- 🔁 **Checkpoints** – Puntos de guardado dentro de cada nivel.

---

## 📁 Estructura del Repositorio

```
Toba_scripts/
│
├── CharacterController2D.cs          # Controlador de física 2D con raycasting para movimiento y colisiones
├── PlayerControllerAdvanced.cs       # Controlador avanzado del jugador (doble salto, wall jump, planeo, etc.)
│
├── Audio/                            # Sistema de audio
│   ├── AudioCtrl.cs                  # Singleton que gestiona toda la reproducción de audio
│   ├── PlayerAudio.cs                # Clips de audio del jugador
│   ├── EnemyAudio.cs                 # Efectos de sonido de enemigos
│   ├── AudioEffects.cs               # Efectos de sonido generales
│   ├── MultiploAudio.cs              # Audio para retroalimentación de operaciones
│   └── BGMusic.cs                    # Controlador de música de fondo
│
├── BD/                               # Base de datos y persistencia
│   ├── BD.cs                         # Gestor SQLite: lectura/escritura de progreso y estadísticas
│   └── SQLite.cs                     # Driver de SQLite para Unity
│
├── Certificado/                      # Generación de certificado final
│   ├── CertificateScript.cs          # Genera y comparte capturas de pantalla (Android Intent)
│   └── btnCertificate.cs            # Botón del certificado
│
├── Enemy/                            # Enemigos e inteligencia artificial
│   ├── EnemyScript.cs                # Enemigo "troll" que presenta problemas matemáticos
│   ├── Enemy.cs                      # Enemigo básico que sigue al jugador
│   ├── EnemyPatrol.cs                # Enemigo que patrulla entre dos puntos
│   ├── FishAI.cs                     # IA de pez saltarín
│   ├── FishSpawner.cs                # Generador de peces
│   ├── BomberBeAI.cs                 # IA de abeja bombardera
│   ├── OrquitosEnemy.cs              # Pequeñas mariposas enemigas
│   └── checkPointAlcanzado.cs        # Sistema de checkpoints
│
├── Generales/                        # Lógica principal del juego y utilidades
│   ├── NIVELES/                      # Sistema de niveles y datos del juego
│   │   ├── SistemaDejuego.cs         # Controlador central: genera problemas, invoca enemigos, gestiona dificultad
│   │   ├── GameData.cs               # Clase serializable con todos los datos del jugador
│   │   ├── DataCtrl.cs               # Gestor de datos persistentes (guarda/carga estado del juego)
│   │   ├── Nivel.cs                  # Datos de cada nivel (estrellas, estadísticas, bloqueado/desbloqueado)
│   │   ├── GeneradorTablas.cs        # Generador de problemas matemáticos aleatorios
│   │   ├── UI.cs                     # Gestión de la interfaz de usuario
│   │   ├── LevelCompleteCtrl.cs      # Pantalla de nivel completado con animación de estrellas
│   │   ├── MenuGameOver.cs           # Menú de game over
│   │   ├── CoinCollector.cs          # Recolección de monedas/huesos
│   │   ├── CameraFollow.cs           # Cámara que sigue al jugador suavemente
│   │   ├── EstadisticasCtrl.cs       # Seguimiento de estadísticas
│   │   └── ...                       # Otros scripts de nivel
│   └── ...                           # Scripts generales (carga, opciones, animaciones, etc.)
│
├── Menu/                             # Sistema de menú y navegación entre escenas
│   ├── MenuController.cs             # Cargador básico de escenas
│   ├── SceneChanger.cs               # Transiciones entre escenas
│   ├── BtnCtrl.cs                    # Botones de selección de nivel (estrellas, candados)
│   └── BtnSaveCtrl.cs                # Guardado de datos desde el menú
│
├── MobileUI/                         # Controles táctiles para móvil
│   ├── MobileCtrlUI.cs               # Controlador central de UI móvil
│   ├── btnJump.cs                    # Botón de salto
│   ├── btnFly.cs                     # Botón de vuelo/planeo
│   ├── BtnMoveRight.cs               # Botón de movimiento derecha
│   ├── BtnMoveLeft.cs                # Botón de movimiento izquierda
│   └── btnMoveCrouch.cs              # Botón de agacharse
│
├── Plataformas/                      # Mecánicas de plataformas
│   ├── MovingPlataform.cs            # Plataformas móviles (punto A a punto B)
│   ├── droppingPlataform.cs          # Plataformas que caen al pisarlas
│   ├── PlataformaFalling.cs          # Plataformas que caen con retraso y se resetean
│   └── ...                           # Variantes de plataformas
│
└── Player/                           # Mecánicas del jugador
    ├── PlayerController.cs           # Controlador básico del jugador
    ├── FeetCtrl.cs                   # Partículas de pies al aterrizar
    ├── btnRespuestas.cs              # Detección de clics en botones de respuesta
    └── followPlayerRespuestas.cs     # UI de respuestas sigue al jugador
```

---

## 🛠️ Tecnologías Utilizadas

| Tecnología | Uso |
|-----------|-----|
| **Unity (2D)** | Motor del juego |
| **C#** | Lenguaje de programación |
| **SQLite** | Persistencia de datos entre sesiones |
| **Unity UI** | Sistema de interfaz (Canvas) |
| **DoTween** | Animaciones de tweening |
| **AudioSource / AudioClip** | Sistema de audio |
| **Android Intent** | Compartir capturas del certificado |

---

## 🎯 Mecánicas de Juego

### Bucle Principal
1. El jugador navega niveles de plataformas 2D como Toba.
2. Encuentra enemigos que presentan problemas matemáticos.
3. Responder correctamente derrota al enemigo; responder mal quita vida.
4. Completar el nivel desbloquea el siguiente y otorga estrellas.

### Habilidades del Jugador
- Movimiento lateral
- Salto y doble salto
- Wall jump y wall run
- Planeo / vuelo
- Deslizamiento en pendientes
- Agacharse
- Salto de poder y stomping
- Controles táctiles (móvil)

### Sistema de Dificultad Adaptativa
- Los enemigos aparecen de forma progresiva (más trolls, movimientos más rápidos).
- Los límites de tiempo disminuyen por nivel.
- La complejidad matemática aumenta (rango de números, tipo de operación).

---

## 📐 Patrones de Diseño

- **Singleton** – `AudioCtrl`, `SistemaDejuego`, `DataCtrl`
- **Observer** – Callbacks de eventos en botones de UI
- **State Machine** – IA de enemigos (patrulla, ataque, inactivo)
- **MVC-like** – Separación entre `GameData` / `DataCtrl` / `UI`
- **Object Pooling** – Sistema de spawn de enemigos/objetos

---

## 🚀 Cómo Usar

1. Abre el proyecto en **Unity** (versión 2019.x o superior recomendada).
2. Importa o verifica las dependencias: **DoTween**, **SQLite for Unity**.
3. Abre la escena principal desde `Assets/Scenes/`.
4. Asigna los scripts a los GameObjects correspondientes según la estructura de prefabs.
5. Presiona **Play** para probar el juego en el editor.

> **Para compilar en Android:** configura el entorno de Build Settings para Android y asegúrate de que los permisos de almacenamiento estén habilitados (para la función de certificado).

---

## 📄 Licencia

Este repositorio contiene scripts de Unity para el proyecto educativo **Toba**. Todos los derechos reservados.
