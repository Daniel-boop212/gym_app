# GymApp

Aplicación móvil de gimnasio desarrollada con **.NET MAUI**, **C#** y **XAML**.

Actualmente, la aplicación carga una colección de ejercicios desde un archivo JSON y los muestra en una lista desplazable.

## Tecnologías

- C#
- .NET 10
- .NET MAUI
- XAML
- Android SDK
- JSON
- MVVM (estructura inicial)

## Estructura del proyecto

```text
GymApp/
├── Data/
│   └── exercises.json
├── Models/
│   └── Exercise.cs
├── Services/
│   └── ExerciseService.cs
├── ViewModels/
│   └── ExercisesViewModel.cs
├── MainPage.xaml
├── MainPage.xaml.cs
├── App.xaml
├── App.xaml.cs
├── AppShell.xaml
├── AppShell.xaml.cs
├── MauiProgram.cs
└── GymApp.csproj
```

### Función de cada parte

- **Data/exercises.json**: contiene los datos de los ejercicios.
- **Models/Exercise.cs**: define la estructura de un ejercicio.
- **Services/ExerciseService.cs**: lee el archivo JSON y convierte los datos en objetos `Exercise`.
- **ViewModels/ExercisesViewModel.cs**: obtiene los ejercicios del servicio y los prepara para la interfaz.
- **MainPage.xaml**: define la interfaz visual.
- **MainPage.xaml.cs**: conecta la página con el ViewModel.
- **GymApp.csproj**: contiene la configuración del proyecto y los recursos MAUI.

## Requisitos

Para trabajar con el proyecto se necesita:

1. Windows.
2. .NET 10 SDK.
3. .NET MAUI.
4. Android Studio.
5. Android SDK.
6. Un dispositivo Android físico o un emulador.

### Comprobar .NET

Abrir PowerShell y ejecutar:

```powershell
dotnet --version
```

El proyecto fue desarrollado con .NET 10.

### Comprobar MAUI

```powershell
dotnet workload list
```

Debe aparecer el workload:

```text
maui
```

## Configuración de Android

Android Studio debe tener instalado el Android SDK.

La ubicación puede variar dependiendo del usuario. Una ubicación habitual en Windows es:

```text
C:\Users\TU_USUARIO\AppData\Local\Android\Sdk
```

Para este proyecto se utilizó Android API 36.

Si el proyecto indica que falta `android.jar` para API 36, se pueden instalar las dependencias de Android con:

```powershell
dotnet build -t:InstallAndroidDependencies -f net10.0-android -p:AndroidSdkDirectory="C:\Users\TU_USUARIO\AppData\Local\Android\Sdk" -p:AcceptAndroidSDKLicenses=True
```

Reemplazar `TU_USUARIO` por el usuario de Windows correspondiente.

## Clonar el proyecto

Desde PowerShell:

```powershell
git clone URL_DEL_REPOSITORIO
```

Entrar en la carpeta:

```powershell
cd GymApp
```

También se puede abrir directamente la carpeta clonada desde VS Code.

## Restaurar el proyecto

Después de clonar el repositorio:

```powershell
dotnet restore
```

Esto descarga las dependencias necesarias del proyecto.

## Compilar

Para comprobar que el proyecto compila para Android:

```powershell
dotnet build -f net10.0-android
```

Si la compilación termina correctamente, el proyecto está listo para ejecutarse.

## Ejecutar en un celular Android

### 1. Preparar el celular

En el teléfono:

- Activar las **Opciones de desarrollador**.
- Activar **Depuración USB**.
- Conectar el celular al computador mediante USB.
- Aceptar la autorización de depuración USB cuando aparezca.

### 2. Comprobar el dispositivo

Ejecutar:

```powershell
adb devices
```

Debe aparecer algo parecido a:

```text
List of devices attached
XXXXXXXX    device
```

Si `adb` no se reconoce, agregar `platform-tools` al PATH de Windows. La ruta normalmente es:

```text
C:\Users\TU_USUARIO\AppData\Local\Android\Sdk\platform-tools
```

También se puede comprobar ADB directamente:

```powershell
& "C:\Users\TU_USUARIO\AppData\Local\Android\Sdk\platform-tools\adb.exe" devices
```

### 3. Ejecutar la aplicación

Con el celular detectado:

```powershell
dotnet build -t:Run -f net10.0-android
```

Esto compila, instala y ejecuta la aplicación en el dispositivo Android.

## Si aparece `XA0010: No hay ningún dispositivo disponible`

Significa que .NET no está detectando un dispositivo Android.

Comprobar:

```powershell
adb devices
```

El dispositivo debe aparecer como:

```text
device
```

Si aparece:

```text
unauthorized
```

desbloquear el teléfono y aceptar la solicitud de **Depuración USB**.

## Si aparece `XA5207: No se ha podido encontrar android.jar`

Significa que falta la plataforma Android correspondiente.

Para este proyecto se utilizó API 36. Ejecutar:

```powershell
dotnet build -t:InstallAndroidDependencies -f net10.0-android -p:AndroidSdkDirectory="C:\Users\TU_USUARIO\AppData\Local\Android\Sdk" -p:AcceptAndroidSDKLicenses=True
```

Después volver a intentar:

```powershell
dotnet build -f net10.0-android
```

## Si solo se quiere comprobar que el código compila

No es necesario conectar un celular:

```powershell
dotnet build -f net10.0-android
```

## Datos de los ejercicios

Los ejercicios están almacenados en:

```text
Data/exercises.json
```

Actualmente contiene **50 ejercicios**.

Cada ejercicio utiliza información como:

- ID
- Nombre
- Grupo muscular principal
- Músculos secundarios
- Equipamiento
- Dificultad
- Tipo
- Descripción
- Instrucciones
- Consejos

## Flujo actual de datos

La aplicación utiliza este flujo:

```text
exercises.json
      ↓
ExerciseService
      ↓
ExercisesViewModel
      ↓
MainPage.xaml
      ↓
CollectionView
```

El JSON no se conecta directamente con la interfaz. El `ExerciseService` se encarga de leer los datos y el ViewModel los prepara para la vista.

## Importante para GitHub

No es necesario subir archivos generados por la compilación.

No se deben subir normalmente carpetas como:

```text
bin/
obj/
.vs/
```

El repositorio debe conservar principalmente el código fuente, los archivos JSON, el `.csproj` y la configuración del proyecto.

## Estado actual

- [x] Proyecto .NET MAUI creado.
- [x] Proyecto configurado para Android.
- [x] Android SDK configurado.
- [x] Modelo `Exercise` creado.
- [x] Base inicial de datos en JSON.
- [x] `ExerciseService` creado.
- [x] `ExercisesViewModel` creado.
- [x] Lista de ejercicios mostrada en XAML.
- [x] Scroll de ejercicios funcionando.
- [x] Ejecución comprobada en un dispositivo Android.

## Próximas funcionalidades

Algunas funcionalidades que se pueden agregar posteriormente:

- Búsqueda de ejercicios.
- Filtros por grupo muscular.
- Filtros por equipamiento.
- Filtros por dificultad.
- Pantalla de detalle de cada ejercicio.
- Favoritos.
- Rutinas personalizadas.
- Registro de entrenamientos.
- Base de datos SQLite.
- Sistema de usuarios.
