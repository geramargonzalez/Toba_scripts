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

## 🔢 Versión de Unity

| Parámetro | Valor |
|-----------|-------|
| **Versión estimada** | Unity **2018.4 LTS – 2020.3 LTS** (estimado por APIs usadas) |
| **API Compatibility** | .NET 4.x (usa `System.Runtime.Serialization.Formatters.Binary`) |
| **Render Pipeline** | Built-in Render Pipeline (no URP/HDRP detectado) |
| **Plataformas target** | Windows, macOS, iOS, Android |
| **Tipo de proyecto** | Unity 2D |

> ⚠️ **Nota:** La versión exacta de Unity **no está incluida en este repositorio** (solo se alojan scripts, no el proyecto completo).  
> El rango **2018.4 – 2020.3 LTS** es una **aproximación basada en el análisis de APIs** usadas en el código (`WWW`, `BinaryFormatter`, `Physics2D`). Puede variar; confirmar abriendo `ProjectSettings/ProjectVersion.txt` en el proyecto Unity original.  
> Una vez confirmada la versión real, actualizar esta tabla.

---

## 📁 Estructura de Carpetas

```
Toba_scripts/                         (92 scripts C# en total)
│
├── CharacterController2D.cs          # Controlador de física 2D (raycasting, pendientes, plataformas)
├── PlayerControllerAdvanced.cs       # Controlador avanzado: doble salto, wall jump, planeo, stomping
│
├── Audio/                            # Sistema de audio (6 scripts)
│   ├── AudioCtrl.cs                  # Singleton maestro de audio
│   ├── PlayerAudio.cs                # Clips del jugador (muerte, monedas, checkpoints)
│   ├── EnemyAudio.cs                 # Efectos de enemigos
│   ├── AudioEffects.cs               # SFX generales (alarmas, monedas)
│   ├── MultiploAudio.cs              # Feedback de operaciones matemáticas
│   └── BGMusic.cs                    # Música de fondo
│
├── BD/                               # Base de datos SQLite (2 scripts)
│   ├── BD.cs                         # Wrapper: lectura/escritura de progreso por nivel
│   └── SQLite.cs                     # Driver SQLite para Unity (Mono.Data.Sqlite, ~3500 líneas)
│
├── Certificado/                      # Certificado de finalización (2 scripts)
│   ├── CertificateScript.cs          # Genera y comparte screenshot vía Android Intent
│   └── btnCertificate.cs             # Botón de certificado
│
├── Enemy/                            # Enemigos e IA (14 scripts)
│   ├── EnemyScript.cs                # Troll: presenta problema matemático, valida respuesta
│   ├── Enemy.cs                      # Enemigo básico: sigue al jugador en radio de visión
│   ├── EnemyPatrol.cs                # Patrulla izq/der con animación idle en giro
│   ├── EnemyHeadCtrl.cs              # Control de cabeza del enemigo
│   ├── HeadCtrl.cs                   # Control genérico de cabeza
│   ├── AnimalsCtrl.cs                # Animales transformables
│   ├── FishAI.cs                     # Pez saltarín
│   ├── FishSpawner.cs                # Spawner de peces
│   ├── BomberBeAI.cs                 # Abeja bombardera
│   ├── OrquitosEnemy.cs              # Mariposas pequeñas
│   ├── BeActivator.cs                # Activa abejas
│   └── checkPointAlcanzado.cs        # Checkpoint del jugador
│
├── Generales/                        # Lógica principal del juego y utilidades
│   ├── CaidaBarril.cs                # Barril/obstáculo que cae
│   ├── CheckPointBola.cs             # Checkpoint de bola/coleccionable
│   ├── GritoTroll.cs                 # Sonido de grito del troll
│   ├── InteraccionBola.cs            # Interacción con bola
│   ├── LanzarBola.cs                 # Mecánica de lanzar bola
│   ├── Loading.cs                    # Pantalla de carga
│   ├── Opciones.cs                   # Menú de opciones/configuración
│   ├── OcultarGraficosTuto.cs        # Oculta gráficos del tutorial
│   ├── PararEntrada.cs               # Bloquea input del jugador
│   ├── ScriAnimOp.cs                 # Animación de operación
│   ├── TimeToScale.cs                # Control de time scale
│   ├── TutoEnemigo.cs                # Enemigo de tutorial
│   └── Wiggle.cs                     # Animación wiggle
│
│   ├── Intermedia1/                  # Nivel intermedio específico (2 scripts)
│   │   ├── SisJuegoIntermedia.cs     # Sistema de juego intermedio
│   │   └── inputValue.cs             # Manejo de input de valor
│   │
│   └── Niveles/                      # Núcleo del juego (29 scripts)
│       ├── SistemaDejuego.cs         # ⭐ CONTROLADOR CENTRAL: 2.313 líneas, lógica de todo el juego
│       ├── DataCtrl.cs               # Persistencia: guarda/carga con BinaryFormatter + SQLite
│       ├── GameData.cs               # Modelo de datos del jugador (35 campos públicos)
│       ├── Nivel.cs                  # Estadísticas por nivel (estrellas, aciertos, fallos)
│       ├── GeneradorTablas.cs        # Generador de tablas matemáticas (archivo vacío – la lógica vive actualmente en SistemaDejuego.cs)
│       ├── UI.cs                     # Holder de 34 referencias a elementos UI
│       ├── LevelCompleteCtrl.cs      # Pantalla de nivel completado + animación estrellas
│       ├── MenuGameOver.cs           # Menú game over
│       ├── NivelCompletado.cs        # Handler de nivel completado
│       ├── EstadisticasCtrl.cs       # Pantalla de estadísticas
│       ├── CameraFollow.cs           # Cámara suave siguiendo al jugador
│       ├── MainCamera.cs             # Setup de cámara
│       ├── ActivarYCamera.cs         # Activa objetos y cámara
│       ├── CoinCollector.cs          # Recolector de monedas/huesos
│       ├── CoinCtrl.cs               # UI de monedas
│       ├── MonedasScript.cs          # Spawner de monedas
│       ├── GuardarPuntoInicial.cs    # Guarda posición de spawn
│       ├── HonguitoCtrl.cs           # Controlador de hongo (power-up)
│       ├── healthpickup.cs           # Ítem de vida
│       ├── PortalCambio.cs           # Portal de cambio de nivel
│       ├── MuertePorCaida.cs         # Muerte al caer al vacío
│       ├── DestroyWithDelay.cs       # Destruye objeto con retraso
│       ├── Numero.cs                 # Display de número
│       ├── Numero2.cs                # Display de número (variante)
│       ├── SFX.cs                    # Datos de efectos de sonido
│       ├── SFXCtrl.cs                # Controlador de SFX (Singleton)
│       ├── btnRespInter.cs           # Botón de respuesta nivel intermedio
│       ├── chainAndSowScript.cs      # Mecánica de cadena/enredadera
│       └── setearOperacion.cs        # Setea operación matemática activa
│
├── Menu/                             # Navegación y menú principal (4 scripts)
│   ├── MenuController.cs             # Cargador básico de escenas
│   ├── SceneChanger.cs               # Transiciones entre escenas con opción de reset
│   ├── BtnCtrl.cs                    # Botones de selección de nivel (estrellas, candados)
│   └── BtnSaveCtrl.cs                # Guarda datos desde el menú
│
├── MobileUI/                         # Controles táctiles móvil (7 scripts)
│   ├── MobileCtrlUI.cs               # Controlador central de UI móvil
│   ├── btnJump.cs                    # Botón salto
│   ├── btnFly.cs                     # Botón vuelo/planeo
│   ├── BtnMoveRight.cs               # Botón mover derecha
│   ├── BtnMoveLeft.cs                # Botón mover izquierda
│   ├── btnMoveCrouch.cs              # Botón agacharse
│   └── MovColliderEscena4.cs         # Collider móvil para escena 4
│
├── Plataformas/                      # Mecánicas de plataformas (8 scripts)
│   ├── Plataforma.cs                 # Clase base (vacía)
│   ├── MovingPlataform.cs            # Plataforma móvil: punto A → punto B, lleva al jugador
│   ├── droppingPlataform.cs          # Cae al pisarla
│   ├── PlataformaFalling.cs          # Cae con retraso, se resetea
│   ├── PlataformaFallingNivelIntermedio.cs  # Variante para nivel intermedio
│   ├── PlataformFallingSmall.cs      # Versión pequeña de plataforma que cae
│   ├── platformaMovil.cs             # Variante de plataforma móvil
│   └── Router.cs                     # Enrutador de plataformas
│
└── Player/                           # Mecánicas del jugador (4 scripts)
    ├── PlayerController.cs           # Controlador básico (Rigidbody2D + Animator)
    ├── FeetCtrl.cs                   # Partículas de pies al aterrizar
    ├── btnRespuestas.cs              # Detección de clics en botones de respuesta
    └── followPlayerRespuestas.cs     # UI de respuestas sigue al jugador
```

---

## 🎬 Escenas Principales

> Las escenas no están incluidas en este repositorio (solo los scripts), pero se pueden reconstruir a partir del código:

| Escena | Descripción | Scripts Clave |
|--------|-------------|---------------|
| **Menú Principal** | Pantalla de inicio con selección de niveles | `MenuController`, `BtnCtrl`, `SceneChanger` |
| **Nivel 0 (Tutorial)** | Primer nivel guiado, presenta mecánicas básicas | `SistemaDejuego`, `TutoEnemigo`, `OcultarGraficosTuto` |
| **Niveles 1 – 7** | Niveles de plataformas con problemas de suma, resta, multiplicación, división | `SistemaDejuego`, `EnemyScript`, `PlayerControllerAdvanced`, `DataCtrl` |
| **Nivel Intermedio** | Variante de dificultad media con input directo | `SisJuegoIntermedia`, `inputValue`, `PlataformaFallingNivelIntermedio` |
| **Game Over** | Pantalla de fin de partida | `MenuGameOver` |
| **Nivel Completado** | Resultado con animación de estrellas | `LevelCompleteCtrl`, `NivelCompletado` |
| **Estadísticas** | Resumen de desempeño por operación | `EstadisticasCtrl` |
| **Certificado** | Genera y comparte el certificado de finalización | `CertificateScript`, `btnCertificate` |

---

## ⭐ Scripts Clave y su Responsabilidad

| Script | Tipo | Responsabilidad Principal | Líneas | Singletons |
|--------|------|--------------------------|--------|-----------|
| `SistemaDejuego.cs` | MonoBehaviour | **Orquestador central**: genera problemas matemáticos, gestiona enemigos, dificultad, progresión y UI | 2.313 | ✅ |
| `DataCtrl.cs` | MonoBehaviour | Guarda y carga el estado del juego (BinaryFormatter + SQLite) | 696 | ✅ |
| `CharacterController2D.cs` | MonoBehaviour | Motor de física 2D con raycasting: colisiones, pendientes, plataformas | 572 | ❌ |
| `PlayerControllerAdvanced.cs` | MonoBehaviour | Movimiento avanzado del jugador: doble salto, wall jump, planeo, stomping | 628 | ❌ |
| `EnemyScript.cs` | MonoBehaviour | IA del troll: presenta operación matemática, valida respuesta, aplica daño | ~250 | ❌ |
| `BD.cs` | Plain Class | Interfaz con base de datos SQLite: lectura/escritura de niveles y estadísticas | ~200 | ❌ |
| `GameData.cs` | Plain Class `[Serializable]` | Modelo de datos del jugador: puntos, vidas, nivel, estadísticas | 74 | ❌ |
| `Nivel.cs` | Plain Class `[Serializable]` | Estadísticas por nivel: estrellas, aciertos/fallos por operación | 72 | ❌ |
| `UI.cs` | Plain Class `[Serializable]` | Holder de 34 referencias a elementos de UI (textos, paneles, imágenes) | 78 | ❌ |
| `AudioCtrl.cs` | MonoBehaviour | Singleton de audio: reproduce SFX de todas las acciones del juego | ~150 | ✅ |
| `LevelCompleteCtrl.cs` | MonoBehaviour | Pantalla de nivel completado con animación de estrellas (DoTween) | ~100 | ❌ |
| `CertificateScript.cs` | MonoBehaviour | Captura pantalla y la comparte vía Android Intent | ~80 | ❌ |
| `MobileCtrlUI.cs` | MonoBehaviour | Gestiona visibilidad y estado de los botones táctiles en móvil | ~60 | ❌ |

---

## 🔌 Assets y Plugins Utilizados

| Plugin / Asset | Uso en el Proyecto | Estado |
|---------------|-------------------|--------|
| **DoTween (DG.Tweening)** | Animaciones de UI: estrellas en nivel completado, transiciones | ✅ Activo (3 archivos) |
| **SQLite for Unity** (Mono.Data.Sqlite) | Base de datos local para progreso de niveles | ✅ Activo |
| **Prime31 CharacterController2D** | Motor de física 2D del personaje (raycasting) | ✅ Activo (adaptado) |
| **Android Intent (vía AndroidJavaObject)** | Compartir capturas de pantalla del certificado | ✅ Solo Android |
| **BinaryFormatter** (.NET) | Serialización del archivo de guardado (`.dat`) | ⚠️ Obsoleto |
| **WWW API** (Unity legacy) | Carga de la base de datos en Android | ⚠️ Deprecado |

---

## 📊 Estado Actual del Proyecto

### ✅ Qué Funciona

| Sistema | Estado | Notas |
|---------|--------|-------|
| Movimiento del jugador | ✅ Funcional | Incluye doble salto, wall jump, planeo, slope sliding |
| Mecánica de preguntas matemáticas | ✅ Funcional | Suma, resta, multiplicación, división |
| Sistema de vidas y daño | ✅ Funcional | Animaciones, feedback de audio |
| Progresión de niveles (0 → 7) | ✅ Funcional | Niveles bloqueados/desbloqueados |
| Sistema de estrellas (1-3 por nivel) | ✅ Funcional | Animación DoTween en pantalla de resultados |
| Estadísticas por operación | ✅ Funcional | Aciertos, fallos, promedio de tiempo |
| Persistencia básica (BinaryFormatter) | ✅ Funcional | En builds actuales de Unity |
| Base de datos SQLite | ✅ Funcional | Para estadísticas de largo plazo |
| Controles móviles táctiles | ✅ Funcional | Android e iOS |
| Certificado compartible | ✅ Funcional | Solo Android (vía Intent) |
| Sistema de audio | ✅ Funcional | BGM + SFX completos |
| Plataformas dinámicas | ✅ Funcional | Móviles, que caen, que se resetean |
| Checkpoints | ✅ Funcional | Guardan posición mid-nivel |

### ⚠️ Qué Está Roto o en Riesgo

| Problema | Severidad | Detalle |
|---------|-----------|---------|
| `BinaryFormatter` | 🔴 Crítico | Marcado como obsoleto en .NET 5+; Unity 2022+ emite advertencias; puede romper guardado en builds nuevas |
| `WWW` API | 🔴 Alto | API deprecada desde Unity 2017; reemplazar por `UnityWebRequest` |
| `SistemaDejuego.cs` (2.313 líneas) | 🟠 Alto | God Class con +40 métodos públicos; muy difícil de mantener o extender |
| `GeneradorTablas.cs` vacío | 🟠 Alto | El archivo existe pero no tiene implementación; la lógica está dentro de `SistemaDejuego` |
| `GameObject.Find()` en runtime | 🟡 Medio | Usado en +11 lugares; rompe si se renombra cualquier objeto de escena |
| Hard-coding de nombres de escena | 🟡 Medio | Nombres como `"DogAdvanced"` dispersos en el código |
| Sin namespaces (89 de 92 scripts) | 🟡 Medio | Polución del namespace global; conflictos potenciales al integrar assets |
| Carga bloqueante de SQLite en Android | 🟡 Medio | Bucle `while(!loadDB.isDone)` congela el hilo principal durante la carga |
| `Debug.Log` de desarrollo | 🟢 Bajo | Llamadas de debug no removidas (`"LO toca !!!!???????"`) |
| Sin inyección de dependencias | 🟢 Bajo | Acoplamiento fuerte entre sistemas via Singleton y `GetComponent` |

---

## 🎯 Objetivo Ideal del Juego Hoy

**Toba** tiene una base sólida para convertirse en una herramienta educativa de calidad. El objetivo ideal en 2024-2025:

1. **Producto educativo multiplataforma** disponible en Google Play y App Store, con seguimiento de progreso por alumno.
2. **Panel docente** donde el maestro/a puede ver estadísticas de cada estudiante.
3. **Contenido ampliable**: fracciones, geometría básica, tablas de multiplicar avanzadas.
4. **Accesibilidad**: soporte de idiomas, tamaños de fuente configurables, narración de audio para niños con dislexia.
5. **Monetización o distribución escolar**: licencias por institución o descarga gratuita con contenido premium.
6. **Analytics básico**: saber qué operación le cuesta más a la mayoría de los jugadores para mejorar el diseño de dificultad.

---

## 🗺️ Diagrama del Flujo del Juego

```
┌─────────────────────────────────────────────────────────────────────┐
│                        TOBA – FLUJO DE JUEGO                        │
└─────────────────────────────────────────────────────────────────────┘

  ┌──────────┐      ┌────────────────┐      ┌──────────────────────┐
  │  INICIO  │─────▶│ MENÚ PRINCIPAL │─────▶│ SELECCIÓN DE NIVEL   │
  └──────────┘      │ (MenuController│      │ (BtnCtrl - estrellas/│
                    │  SceneChanger) │      │  candados)           │
                    └────────────────┘      └──────────┬───────────┘
                                                       │
                    ┌──────────────────────────────────▼──────────────┐
                    │                  NIVEL DE JUEGO                  │
                    │  ┌─────────────┐   ┌──────────────────────┐    │
                    │  │  Toba (Dog) │   │  SistemaDejuego       │    │
                    │  │  Navega el  │   │  - Genera problema    │    │
                    │  │  nivel con  │   │    matemático         │    │
                    │  │  plataformas│   │  - Administra enemigos│    │
                    │  └──────┬──────┘   │  - Controla timer     │    │
                    │         │          │  - Gestiona UI        │    │
                    │         │          └──────────┬────────────┘    │
                    │         │                     │                 │
                    │         ▼                     ▼                 │
                    │  ┌────────────────────────────────┐            │
                    │  │       ENCUENTRO CON ENEMIGO     │            │
                    │  │  (EnemyScript)                  │            │
                    │  │  Troll muestra:  A [op] B = ?   │            │
                    │  │  Opciones: [R1]  [R2]  [R3]     │            │
                    │  └────────────┬───────────────────┘            │
                    │               │                                 │
                    │        ┌──────┴──────┐                         │
                    │        │             │                         │
                    │   ✅ Correcto    ❌ Incorrecto                 │
                    │        │             │                         │
                    │        ▼             ▼                         │
                    │  Enemigo muere  Toba pierde vida               │
                    │  +Monedas       AudioCtrl → SFX                │
                    │  +Estadísticas  Si vidas=0 → GAME OVER        │
                    │        │                                        │
                    └────────┼────────────────────────────────────────┘
                             │
               ┌─────────────┴──────────────────┐
               │                                │
       Todos los enemigos            Sin vidas / caída al vacío
       derrotados                    (MuertePorCaida.cs)
               │                                │
               ▼                                ▼
  ┌─────────────────────┐           ┌──────────────────┐
  │   NIVEL COMPLETADO  │           │    GAME OVER      │
  │  (LevelCompleteCtrl)│           │  (MenuGameOver)   │
  │  ★ Animación stars  │           │  Reintentar       │
  │  Guarda progreso    │           │  Volver al menú   │
  │  DataCtrl.Save()    │           └──────────────────┘
  │  SQLite.Update()    │
  └──────────┬──────────┘
             │
    ┌────────┴──────────┐
    │                   │
Último nivel        Más niveles
completado          disponibles
    │                   │
    ▼                   ▼
┌──────────────┐   ┌────────────────┐
│ ESTADÍSTICAS │   │ SIGUIENTE NIVEL│
│ + CERTIFICADO│   │ (desbloqueado) │
│(CertificateS)│   └────────────────┘
└──────────────┘
        │
        ▼
  Compartir en
  Android / iOS
```

---

## 🏗️ Arquitectura Actual

### Diagrama de Dependencias entre Sistemas

```
┌─────────────────────────────────────────────────────────────────────┐
│                     ARQUITECTURA DE TOBA SCRIPTS                    │
└─────────────────────────────────────────────────────────────────────┘

  ┌─────────────────────────────────────────┐
  │         CAPA DE PRESENTACIÓN (UI)        │
  │  UI.cs · EstadisticasCtrl · CoinCtrl    │
  │  LevelCompleteCtrl · MenuGameOver       │
  │  BtnCtrl · MobileCtrlUI                 │
  └──────────────────┬──────────────────────┘
                     │ referencia directa
  ┌──────────────────▼──────────────────────┐
  │       CONTROLADOR CENTRAL (GOD CLASS)   │
  │            SistemaDejuego.cs            │  ← Singleton
  │  - Generación de problemas              │
  │  - Gestión de enemigos                  │
  │  - Lógica de nivel                      │
  │  - Control de UI                        │
  │  - Gestión de audio                     │
  │  - Tracking de estadísticas             │
  │  - Progresión y dificultad              │
  └────┬─────────────┬────────────┬─────────┘
       │             │            │
  ┌────▼────┐  ┌─────▼────┐  ┌───▼─────────────┐
  │DataCtrl │  │AudioCtrl │  │ EnemyScript      │
  │(Persist)│  │(Audio)   │  │ (Troll AI)       │
  │Singleton│  │Singleton │  │ - Muestra prob.  │
  └────┬────┘  └──────────┘  │ - Valida respues.│
       │                      └──────────────────┘
  ┌────▼──────────────────┐
  │      CAPA DE DATOS    │
  │  GameData.cs          │  ← [Serializable] BinaryFormatter
  │  Nivel.cs             │  ← [Serializable] por nivel
  │  BD.cs                │  ← SQLite wrapper
  │  SQLite.cs            │  ← Driver SQLite (~3500 líneas)
  └───────────────────────┘

  ┌─────────────────────────────────────────┐
  │          CAPA DE MOVIMIENTO             │
  │  CharacterController2D.cs  ←──────────┐ │
  │  PlayerControllerAdvanced.cs ──────────┘ │
  │  PlayerController.cs (básico)            │
  └─────────────────────────────────────────┘

  ┌─────────────────────────────────────────┐
  │          ENTORNO / PLATAFORMAS          │
  │  MovingPlataform · PlataformaFalling    │
  │  droppingPlataform · Router             │
  └─────────────────────────────────────────┘
```

### Patrones Utilizados

| Patrón | Implementación | Archivos |
|--------|---------------|---------|
| **Singleton** | `static instance` manual | `DataCtrl`, `SistemaDejuego`, `AudioCtrl`, `SFXCtrl` |
| **Serializable** | `[Serializable]` + BinaryFormatter | `GameData`, `Nivel` |
| **Observer** | `Action` delegates y callbacks | `CharacterController2D` eventos de colisión |
| **State Machine** | Flags booleanos en Update() | IA de enemigos (patrulla/ataque) |
| **Coroutines** | 69 usos de `IEnumerator` | Cooldowns, delays, transiciones |
| **Component Caching** | `GetComponent` en `Start()` | Mayoría de los MonoBehaviours |

---

## 🟢 Qué Rescatar

Estos elementos están bien implementados y deben conservarse como base:

| Qué | Por qué rescatarlo |
|-----|-------------------|
| **`CharacterController2D.cs`** (Prime31) | Física profesional con raycasting; manejo correcto de pendientes y plataformas; documentado. Puede reutilizarse directamente. |
| **`PlayerControllerAdvanced.cs`** | Implementa muchas habilidades (double jump, wall jump, glide) con un sistema de flags configurable. Buena base para refactorizar. |
| **Modelo de datos `GameData` / `Nivel`** | Las clases `[Serializable]` son el contrato correcto; solo hay que cambiar el serializador (de BinaryFormatter a JsonUtility o Newtonsoft). |
| **Sistema de audio** | `AudioCtrl` es un Singleton limpio y bien encapsulado; los 5 scripts de audio están bien separados por responsabilidad. |
| **`EnemyScript.cs` (lógica matemática)** | La idea de que el enemigo genere y valide operaciones es correcta; solo refactorizar para extraerla a una clase independiente (`MathProblemGenerator`). |
| **`LevelCompleteCtrl.cs`** | Usa DoTween correctamente; la pantalla de resultados con animación de estrellas es una feature valiosa. |
| **`BD.cs` (estructura SQLite)** | El esquema de base de datos es sólido para tracking educativo; solo cambiar la API de carga en Android. |
| **`MobileCtrlUI.cs` + botones táctiles** | El sistema de controles móviles funciona bien; es una feature diferenciadora. |
| **Plataformas dinámicas** | `MovingPlataform` y `PlataformaFalling` tienen comportamientos correctos y variados. |
| **Checkpoints** | El sistema de puntos de guardado mid-nivel es correcto para un juego de plataformas educativo. |

---

## 🔧 Qué Refactorizar Primero

Prioridad decreciente (qué atacar antes por impacto/riesgo):

### 🔴 Prioridad 1 – Crítico (Hacer YA)

**1. Reemplazar `BinaryFormatter` por `JsonUtility` o `Newtonsoft.Json`**

```csharp
// ❌ ACTUAL - Obsoleto en .NET 5+
BinaryFormatter bf = new BinaryFormatter();
bf.Serialize(file, gameData);

// ✅ NUEVO - JsonUtility (built-in en Unity)
string json = JsonUtility.ToJson(gameData);
File.WriteAllText(path, json);
```

**2. Reemplazar `WWW` por `UnityWebRequest`**

```csharp
// ❌ ACTUAL - Deprecado
WWW loadDB = new WWW("jar:file://" + dbPath);
while (!loadDB.isDone) { }

// ✅ NUEVO - Coroutine con UnityWebRequest
using UnityEngine.Networking;
IEnumerator LoadDatabase(string path) {
    UnityWebRequest req = UnityWebRequest.Get(path);
    yield return req.SendWebRequest();
    // usar req.downloadHandler.data
}
```

### 🟠 Prioridad 2 – Alto (Sprint 1)

**3. Descomponer `SistemaDejuego.cs` en clases especializadas**

```
SistemaDejuego.cs (2.313 líneas) → dividir en:
  ├── GameManager.cs          (flujo de nivel, vidas, game over)
  ├── MathProblemGenerator.cs (generación y validación de operaciones)
  ├── EnemyManager.cs         (spawn y estado de enemigos)
  ├── DifficultyManager.cs    (escalado de dificultad por nivel)
  └── LevelProgressManager.cs (progresión, estrellas, desbloqueo)
```

**4. Eliminar todos los `GameObject.Find()` de runtime**

Reemplazar por referencias asignadas desde el Inspector de Unity o inyectadas en `Awake()`.

### 🟡 Prioridad 3 – Medio (Sprint 2)

**5. Agregar namespaces a todos los scripts**

```csharp
// Estructura sugerida
namespace Toba.Core { }         // GameManager, DataCtrl
namespace Toba.Player { }       // PlayerControllerAdvanced
namespace Toba.Enemies { }      // EnemyScript, EnemyPatrol
namespace Toba.Math { }         // MathProblemGenerator
namespace Toba.Audio { }        // AudioCtrl, BGMusic
namespace Toba.UI { }           // LevelCompleteCtrl, MenuGameOver
namespace Toba.Data { }         // GameData, Nivel, BD
```

**6. Encapsular campos públicos en `GameData` y `Nivel`**

```csharp
// ❌ ACTUAL
public int puntos;
public int vidas;

// ✅ NUEVO
public int Puntos { get; private set; }
public int Vidas { get; private set; }
public void AgregarPuntos(int cantidad) { Puntos += cantidad; }
```

**7. Extraer magic numbers a `ScriptableObject` de configuración**

```csharp
// ❌ ACTUAL - dispersos en SistemaDejuego.cs
if (nivel == 0) cantidadEnemigos = 3;
if (nivel == 1) cantidadEnemigos = 5;

// ✅ NUEVO - LevelConfig.asset (ScriptableObject)
[CreateAssetMenu]
public class LevelConfig : ScriptableObject {
    public int enemyCount;
    public float timeLimit;
    public MathOperation[] allowedOperations;
}
```

### 🟢 Prioridad 4 – Mejoras (Sprint 3+)

- Convertir los 4 Singletons manuales a un sistema de inyección de dependencias ligero (o usar el `ServiceLocator` de Unity `Services.Get<T>()`).
- Implementar `GeneradorTablas.cs` (actualmente vacío) como clase independiente.
- Agregar tests de unidad para `MathProblemGenerator` con Unity Test Framework.
- Migrar la cámara a **Cinemachine** (evitar código manual de `CameraFollow`).
- Extraer la carga de SQLite en Android a una coroutine no bloqueante.

---

## 🚀 Cómo Modernizarlo en Unity

### Actualizaciones de Tecnología

| Tecnología Actual | Tecnología Moderna | Beneficio |
|-------------------|--------------------|----------|
| `BinaryFormatter` | `JsonUtility` / `Newtonsoft.Json` | Seguro, legible, compatible con .NET 5+ |
| `WWW` | `UnityWebRequest` + Coroutines | API oficial, no bloqueante |
| `CameraFollow.cs` manual | **Cinemachine** (free en URP) | Cámara profesional en 5 minutos |
| `Physics2D` + `CharacterController2D` | **Unity Input System (nuevo)** + CC2D | Input multiplataforma unificado |
| `UnityEngine.UI` (UGUI) | **UI Toolkit** (Unity 2021+) | Más eficiente para pantallas complejas |
| `AudioCtrl` manual | **Audio Mixer** + `AudioCtrl` | Control de volumen por capas (BGM/SFX/Voice) |
| Singleton manual (`static instance`) | **ScriptableObject Events** o **ServiceLocator** | Desacoplamiento real entre sistemas |
| `GameObject.Find()` | **Dependency Injection** (serialized references) | Performance + mantenibilidad |
| Datos en `StreamingAssets` | **Addressables** | Carga asíncrona, patches sin rebuild |

### Upgrade Recomendado de Versión Unity

```
ACTUAL (estimado): Unity 2018.x – 2020.x
     ↓
PASO INTERMEDIO:   Unity 2021.3 LTS
  • Estable, buen soporte Android/iOS
  • Incluye nuevo Input System
  • Cinemachine integrado

     ↓
OBJETIVO:          Unity 2022.3 LTS
  • UI Toolkit maduro
  • Mejor rendimiento 2D (Sprite Atlas v2)
  • Android API Level 33+ compatible
  • .NET Standard 2.1 (sin BinaryFormatter)
```

---

## 🏁 Cómo Llevarlo a un MVP Más Actual

Un MVP moderno de Toba debería tener:

### Fase 1 – Estabilización (2-3 semanas)
- [ ] Migrar `BinaryFormatter` → `JsonUtility`
- [ ] Migrar `WWW` → `UnityWebRequest`
- [ ] Eliminar `Debug.Log` de producción con `#if UNITY_EDITOR` guards
- [ ] Documentar la versión exacta de Unity en `ProjectVersion.txt`
- [ ] Implementar `GeneradorTablas.cs` (actualmente vacío)

### Fase 2 – Refactorización Core (3-4 semanas)
- [ ] Descomponer `SistemaDejuego.cs` en 5 clases
- [ ] Eliminar `GameObject.Find()` del runtime
- [ ] Agregar namespaces a todos los scripts
- [ ] Convertir magic numbers a `ScriptableObjects` configurables
- [ ] Migrar al **nuevo Input System** de Unity (unifica desktop + mobile)

### Fase 3 – Features MVP Moderno (4-6 semanas)
- [ ] **Sistema de perfiles de jugador** – múltiples alumnos en el mismo dispositivo
- [ ] **Modo sin conexión confiable** – SQLite + JSON locales, sin dependencias externas
- [ ] **Panel de estadísticas mejorado** – gráfico de barras por operación
- [ ] **Notificaciones** – recordatorio diario de práctica (Android/iOS)
- [ ] **Pantalla de carga animada** – `Addressables` para carga asíncrona
- [ ] **Accesibilidad básica** – tamaño de texto configurable, narración de audio
- [ ] **Cinemachine** – reemplazar `CameraFollow.cs` manual

### Fase 4 – Publicación (2-3 semanas)
- [ ] Build Pipeline CI/CD (GitHub Actions → Unity Cloud Build)
- [ ] Firma de APK y configuración de Google Play
- [ ] Tests de regresión con **Unity Test Framework** para la lógica matemática
- [ ] **Analytics** – Unity Analytics o Firebase para métricas educativas

---

## 🛠️ Cómo Usar

1. Abre el proyecto en la **versión de Unity confirmada** (ver sección [Versión de Unity](#-versión-de-unity); estimado 2018.4–2020.3 LTS).  
   > ⚠️ **No abrir directamente en Unity 2022.3 LTS** sin completar primero la modernización (ver [Cómo Modernizarlo](#-cómo-modernizarlo-en-unity)), ya que las APIs deprecadas (`BinaryFormatter`, `WWW`) generarán errores de compilación en versiones recientes.
2. Importa o verifica las dependencias en el Package Manager:
   - **DoTween** (`com.demigiant.dotween`) – animaciones
   - **SQLite for Unity** (Mono.Data.Sqlite) – base de datos local
3. Abre la escena principal desde `Assets/Scenes/`.
4. Verifica que el `SistemaDejuego` GameObject en la escena tenga todas las referencias de UI asignadas.
5. Presiona **Play** para probar el juego en el editor.

> **Para compilar en Android:** en Build Settings seleccioná Android, asegurate de que `Internet Access: Required` y `Write Permission: External (SDCard)` estén habilitados (para certificado y SQLite).

> **Nota SQLite en Android:** La base de datos `tobaBd.db` debe estar en `Assets/StreamingAssets/`. En el primer lanzamiento en Android, `BD.cs` la copia al `persistentDataPath` usando `WWW` (deprecado) o `UnityWebRequest` (versión modernizada).

---

## 📄 Licencia

Este repositorio contiene scripts de Unity para el proyecto educativo **Toba**. Todos los derechos reservados.
