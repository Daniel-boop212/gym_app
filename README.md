# 🏋️ GymApp

Aplicación móvil de entrenamiento personal desarrollada con **C# y .NET MAUI**.

El objetivo del proyecto es crear una aplicación que permita organizar rutinas, consultar ejercicios, registrar entrenamientos reales y posteriormente obtener información de progreso a partir de la actividad realizada.

---

## 🚀 Tecnologías

- **C#**
- **.NET 10**
- **.NET MAUI**
- **XAML**
- **MVVM**
- **SQLite** *(planeado para una etapa posterior)*

---

## 📱 Estado actual

| Módulo | Estado |
|---|---|
| Inicio / Dashboard | ✅ Funcional |
| Ejercicios | ✅ Funcional |
| Rutinas | ✅ Funcional |
| Perfil | ✅ Funcional |
| Entrenamiento | 🟡 En desarrollo |
| Progreso | ⏳ Pendiente |
| Persistencia con SQLite | ⏸️ Pendiente |
| Diseño visual final | ⏳ Pendiente |

---

## 🧩 Funcionalidades actuales

### 🏠 Inicio

El Dashboard funciona como un resumen de la aplicación y actualmente muestra:

- Saludo al usuario.
- Entrenamiento del día.
- Resumen de entrenamientos y tiempo.
- Actividad reciente.

Los accesos principales se mantienen en la navegación inferior para evitar duplicar funciones en el Dashboard.

### 🏋️ Ejercicios

La aplicación cuenta con un catálogo de ejercicios almacenado en `Data/exercises.json`.

Actualmente permite:

- Consultar ejercicios.
- Buscar por nombre.
- Filtrar por grupo muscular.
- Ver información detallada.
- Consultar equipo, dificultad, tipo, músculos secundarios, instrucciones y consejos.

### 📋 Rutinas

Actualmente se pueden:

- Crear rutinas.
- Ver las rutinas existentes.
- Entrar al detalle de una rutina.
- Agregar ejercicios.
- Configurar series.
- Configurar repeticiones.
- Configurar peso.
- Configurar tiempo de descanso.
- Editar ejercicios de una rutina.
- Eliminar ejercicios de una rutina.

La estructura utilizada es:

```text
Rutina
  ↓
RoutineExercise
  ↓
Exercise
```

Esto permite que un mismo ejercicio del catálogo tenga diferentes configuraciones dependiendo de la rutina.

### 👤 Perfil

El perfil permite registrar información como:

- Nombre.
- Edad.
- Altura.
- Peso.
- Objetivo de entrenamiento.
- Información de salud opcional.
- Lesiones o restricciones.
- Movimientos que se deben evitar.

La información de salud es opcional y está planteada únicamente como información para tener en cuenta dentro de la aplicación.

---

## 🏃 Entrenamiento real

Actualmente se está construyendo el sistema que permitirá registrar lo que realmente ocurre durante un entrenamiento.

La idea es diferenciar entre:

```text
Exercise = ejercicio disponible en el catálogo

Routine = planificación

WorkoutSession = entrenamiento realizado

WorkoutExercise = ejercicio realizado dentro del entrenamiento

WorkoutSet = serie realizada
```

### Registro de entrenamiento

El diseño actual permite:

- Iniciar un entrenamiento desde una rutina.
- Registrar una serie a la vez.
- Registrar repeticiones.
- Registrar peso.
- Marcar un ejercicio como realizado.
- Omitir un ejercicio.
- Pasar al siguiente ejercicio.
- Finalizar el entrenamiento.
- Medir el tiempo total del entrenamiento.

El sistema no obliga a que el usuario cumpla exactamente las series y repeticiones planeadas.

Por ejemplo:

```text
Rutina:
Press banca → 3 × 10 @ 20 kg

Entrenamiento real:
Serie 1 → 10 × 20 kg
Serie 2 → 9 × 20 kg
Serie 3 → 8 × 20 kg
```

El entrenamiento sigue siendo válido porque representa lo que realmente ocurrió.

Esto también permite que un ejercicio pueda ser omitido sin invalidar todo el entrenamiento.

---

## ⏱️ Tiempo de entrenamiento

Cada `WorkoutSession` tiene:

- `StartTime`
- `EndTime`

El tiempo transcurrido se calcula a partir de estos valores.

Actualmente existe un temporizador visual durante el entrenamiento.

---

## 📊 Progreso

El módulo de progreso todavía no está implementado.

La idea es que **Progreso no sea editable manualmente**, sino que se genere a partir de los entrenamientos realmente completados.

Conceptualmente:

```text
WorkoutSession
      ↓
Entrenamientos completados
      ↓
Datos históricos
      ↓
Progreso
```

Por esta razón, primero se está construyendo correctamente el sistema de entrenamiento antes de desarrollar las estadísticas.

---

## 💾 Persistencia de datos

Actualmente la aplicación utiliza datos en memoria para las rutinas y los entrenamientos.

SQLite se implementará posteriormente.

La decisión de retrasar SQLite permite terminar primero:

1. Modelos.
2. Relaciones entre entidades.
3. Flujo de rutinas.
4. Flujo de entrenamiento.
5. Progreso.

Después se podrá definir correctamente la base de datos y su persistencia.

SQLite será una base de datos local integrada en la aplicación; no requiere un servidor externo.

---

## 🗂️ Estructura del proyecto

```text
GymApp/
│
├── Data/
│   └── exercises.json
│
├── Models/
│   ├── Exercise.cs
│   ├── Routine.cs
│   ├── RoutineExercise.cs
│   ├── WorkoutSession.cs
│   ├── WorkoutExercise.cs
│   └── WorkoutSet.cs
│
├── Services/
│   ├── ExerciseService.cs
│   ├── RoutineService.cs
│   └── WorkoutService.cs
│
├── ViewModels/
│   ├── ExercisesViewModel.cs
│   ├── RoutinesViewModel.cs
│   └── WorkoutViewModel.cs
│
├── Views/
│   ├── Home/
│   ├── Exercises/
│   ├── Routines/
│   ├── Progress/
│   ├── Profile/
│   └── Workout/
│
├── App.xaml
├── App.xaml.cs
├── AppShell.xaml
├── AppShell.xaml.cs
├── MainPage.xaml
├── MainPage.xaml.cs
├── MauiProgram.cs
└── GymApp.csproj
```

---

## 🧠 Arquitectura

El proyecto utiliza una organización basada en **MVVM**:

```text
Model
  ↓
ViewModel
  ↓
View
```

### Models

Representan los datos de la aplicación.

Ejemplos:

- `Exercise`
- `Routine`
- `RoutineExercise`
- `WorkoutSession`
- `WorkoutExercise`
- `WorkoutSet`

### Views

Contienen la interfaz XAML y las páginas de la aplicación.

### ViewModels

Contienen el estado y la lógica relacionada con la interfaz.

### Services

Se encargan de operaciones relacionadas con los datos y la lógica de servicios.

---

## ▶️ Ejecutar el proyecto

### Requisitos

- .NET 10 SDK
- .NET MAUI
- Android SDK
- Un dispositivo Android o emulador

Desde la carpeta del proyecto:

```powershell
cd D:\GymApp
```

Para compilar:

```powershell
dotnet build -f net10.0-android
```

Para ejecutar en Android:

```powershell
dotnet build -t:Run -f net10.0-android
```

---

## 🛠️ Próximos pasos

El desarrollo seguirá aproximadamente este orden:

1. Terminar y probar el flujo de entrenamiento.
2. Revisar y ajustar los modelos y relaciones.
3. Implementar el módulo de Progreso.
4. Revisar el Dashboard utilizando datos reales.
5. Integrar SQLite.
6. Implementar persistencia completa.
7. Mejorar el diseño visual.
8. Revisar la experiencia general de usuario.

---

## 📌 Nota

El proyecto se encuentra en desarrollo. Actualmente la prioridad es construir correctamente la estructura y las funcionalidades antes de realizar el diseño visual definitivo.

La aplicación está pensada para registrar **entrenamientos reales**, no solamente planes ideales. Por eso el usuario puede modificar lo realizado durante una sesión y registrar las condiciones reales de cada entrenamiento.