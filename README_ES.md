# Launcher de World of WarCraft

`Launcher` - Esta es una herramienta de hobby para proporcionar a los jugadores de tu servidor un acceso rápido a una variedad de características.

<p align="center">
  <img src="https://pp.vk.me/c631428/v631428303/500ff/wMh1l71dY5M.jpg" width="500" height="356"/>
</p>

---

## 📚 Documentación

Puedes encontrar la documentación oficial en el archivo README.docx en el repositorio, o usar esta guía traducida al español.

---

## 🎮 Características Principales

### Funcionalidades Principales

* **Actualización automática del programa** - El launcher se actualiza a sí mismo cuando hay nuevas versiones disponibles.
* **Limpieza de caché** - Elimina automáticamente la carpeta Cache del cliente.
* **Escritura del realmlist** - Actualiza automáticamente el archivo `realmlist.wtf` con la conexión del servidor.
* **Visualización de noticias** - Muestra noticias del servidor directamente en el launcher.
* **Descarga de nuevos parches** (sin necesidad de actualizar el launcher):
  * Compara el hash MD5 de los archivos (tus parches siempre estarán seguros).
  * Muestra el progreso de descarga de actualizaciones (tiempo restante, velocidad, porcentaje, tamaño).
  * Descarga resumible de actualizaciones (si se interrumpe, continúa desde donde se quedó).
* **Eliminación de parches del servidor**:
  * Si los jugadores desean abandonar el servidor (sin interferir con el launcher).
* **Eliminación de todos los parches no deseados**:
  * Útil para: revertir actualizaciones fallidas, eliminar parches extranjeros excepto los originales y los del servidor.
* **Prevención de ejecución del launcher**:
  * Evita ejecutar múltiples instancias del launcher.
  * Evita ejecutar el launcher mientras el cliente WoW está corriendo.

### Configuración de la Aplicación

La aplicación tiene sus propias configuraciones de usuario:

* **Autorización automática en el cliente del juego** usando datos de usuario guardados:
  * Los datos ingresados permanecen anónimos por defecto.
* **Indicación explícita e implícita de la ruta del cliente del juego**:
  * Instalando el programa en la raíz del cliente del juego o seleccionando la carpeta manualmente.
* **Límite de descarga personalizable** para archivos del juego:
  * Limitación del ancho de banda de la red.
* **Visualización de progreso de descarga personalizable**:
  * Progreso total de descarga.
  * Progreso de descarga actual.
  * Progreso de descarga mixto.

---

## 🛠️ Configuración del Servidor

### Requisitos Previos

Para usar este launcher con tu servidor privado de WoW, necesitas configurar los siguientes archivos en tu servidor web:

1. **patchlist.txt** - Lista de parches a descargar
   - Formato: `URL#NombreArchivo#HashMD5#TamañoEnBytes`
   - Ejemplo: `https://tuservidor.com/patch-1.mpq#patch-1.mpq#a1b2c3d4e5f6#1048576`

2. **patchtodel.txt** - Lista de parches a eliminar (opcional)
   - Formato: Un nombre de archivo por línea
   - Ejemplo: `old-patch.mpq`

3. **realmlist.txt** - Contenido del realmlist
   - Formato: `set realmlist tu.servidor.com`

4. **version.txt** - Versión actual del launcher
   - Formato: `1.0.0.0`

5. **news.txt** - Noticias para mostrar (HTML o texto plano)

6. **updates.txt** - Historial de actualizaciones del launcher

### Configuración en App.config

Edita el archivo `Launcher/App.config` y actualiza las URLs:

```xml
<setting name="PatchDownloadURL" serializeAs="String">
    <value>https://tuservidor.com/patchlist.txt</value>
</setting>
<setting name="PatchToDelete" serializeAs="String">
    <value>https://tuservidor.com/patchtodel.txt</value>
</setting>
<setting name="RealmlistURL" serializeAs="String">
    <value>https://tuservidor.com/realmlist.txt</value>
</setting>
<setting name="LauncherVersionUrl" serializeAs="String">
    <value>https://tuservidor.com/version.txt</value>
</setting>
<setting name="LauncherNewsFileUrl" serializeAs="String">
    <value>https://tuservidor.com/news.txt</value>
</setting>
<setting name="LauncherUpdates" serializeAs="String">
    <value>https://tuservidor.com/updates.txt</value>
</setting>
```

### Soporte de Versiones de WoW

El launcher soporta tres versiones principales de WoW:

#### **Vanilla (1.12.x)**
- Archivo realmlist: `realmlist.wtf` en la raíz del juego
- Descomentar líneas 269-275 en `MainWindow.xaml.cs`

#### **Wrath of the Lich King (3.3.5)**
- Archivo realmlist: `Data\{idioma}\realmlist.wtf`
- Ya está habilitado por defecto (líneas 278-282)

#### **Mists of Pandaria (5.x)**
- Archivo realmlist: `WTF\config.wtf` con "set realmlist"
- Descomentar líneas 285-291 en `MainWindow.xaml.cs`

**Importante:** Comenta/descomenta los bloques apropiados según la versión de tu servidor.

---

## 🔧 Configuración Paso a Paso

### 1. Configuración de Propiedades del Proyecto

**Ubicación:** Explorador de Soluciones → Propiedades (doble clic)

#### a) Configuración Básica
1. **Nombre de compilación**: Cambia el nombre por el de tu launcher
2. Haz clic en **"Detalles de compilación"**
3. Cambia los campos:
   - **Nombre**: Nombre de tu launcher
   - **Producto**: Nombre de tu servidor/launcher
4. Haz clic en **"Aceptar"**

#### b) Pestaña "Opciones" - Variables Globales

Estas son las URLs que debes configurar en tu servidor web:

| Variable | Descripción | Ejemplo |
|----------|-------------|---------|
| `PatchDownloadURL` | Enlace a la lista de actualizaciones para el cliente | `https://tuservidor.com/patchlist.txt?dl=1` |
| `PatchToDelete` | Lista de archivos a eliminar (actualizaciones incorrectas) | `https://tuservidor.com/patchtodel.txt?dl=1` |
| `LauncherVersionUrl` | Archivo con la versión actual del programa | `https://tuservidor.com/version.txt?dl=1` |
| `LauncherNewsFileUrl` | Archivo de noticias del programa | `https://tuservidor.com/news.txt?dl=1` |
| `LauncherUpdates` | Historial de actualizaciones del programa | `https://tuservidor.com/updates.txt?dl=1` |
| `RealmlistURL` | Información de la lista de reinos | `https://tuservidor.com/realmlist.txt?dl=1` |
| `ClientExeName` | Nombre del ejecutable del juego | `Wow.exe` (o ejecutable parcheado) |

⚠️ **IMPORTANTE**: Los enlaces deben ser directos (agregar `?dl=1` al final si usas Dropbox).

### 2. Configuración de MainWindow.xaml.cs

**Ubicación:** Explorador de Soluciones → MainWindow.xaml → MainWindow.xaml.cs (doble clic)

#### Buscar tareas TODO (Ctrl+F)

Encontrarás 3 tareas importantes:

**a) TODO: CAMBIAR NOMBRE DEL SERVIDOR Y VERSIÓN DEL CLIENTE**
- Ubicación: Líneas 231-249
- Cambia el mensaje de error con el nombre de tu servidor
- Actualiza la versión del cliente (por defecto: 3.3.5.12340)

```csharp
// Ejemplo:
var result = MessageBox.Show("¡Para jugar en el servidor MI SERVIDOR WOW se requiere el cliente versión 3.3.5.12340!...", ...);
```

**b) TODO: SISTEMA DE REALMLIST**
- Ubicación: Líneas 269-291
- **Para clientes 3.3.5a e inferiores**: Usa el bloque "lich king realmlist changer" (YA HABILITADO)
- **Para clientes anteriores a 3.3.5**: Comenta el bloque lich king y descomenta "vanilla realmlist changer"
- **Para Pandaria+**: Descomenta el bloque "pandaria realmlist changer"

**c) Enlaces Rápidos**
- Ubicación: Líneas 830-850
- Cambia los enlaces "fictícios" por los de tu servidor:

```csharp
private void link_main_Click(object sender, RoutedEventArgs e)
{
    Process.Start("http://tu-servidor.com");
}

private void link_cabinet_Click(object sender, RoutedEventArgs e)
{
    Process.Start("http://tu-servidor.com/panel");
}

private void link_registration_Click(object sender, RoutedEventArgs e)
{
    Process.Start("http://tu-servidor.com/registro");
}

private void link_social_Click(object sender, RoutedEventArgs e)
{
    Process.Start("http://tu-servidor.com/discord");
}
```

### 3. Configuración de LNewVer.xaml

**Ubicación:** Explorador de Soluciones → LNewVer.xaml (doble clic)

Busca el siguiente código y cambia el atributo `NavigateUri`:

```xml
<Hyperlink NavigateUri="http://tu-servidor.com/descargas/launcher-ultima-version.exe">
```

Este enlace debe ser **estático** (por ejemplo: almacenado en FTP o servidor web, solo necesitas resubir el archivo).

### 4. Cambiar el Nombre del Launcher

Edita `Launcher/Launcher.csproj` línea 12:
```xml
<AssemblyName>TuNombreAqui</AssemblyName>
```

### 5. Personalización Visual

#### Cambiar Imágenes

Reemplaza las siguientes imágenes en la carpeta `Launcher/img/`:
- `main.jpg` - Imagen de fondo principal (1920x800px recomendado)
- `news_bg.jpg` - Fondo de noticias
- `101.ico` - Icono del launcher (32x32px o 64x64px)
- `play-thumb.png` - Icono del botón de la barra de tareas

#### Cambiar Cursores

Los cursores están en `Launcher/img/cursors/`:
- `wow.cur` - Cursor normal
- `WOW-ESCRIVIR.cur` - Cursor de lectura (cuando está sobre texto)
- `WOW-ENLACE-CURSOR.cur` - Cursor de enlace (cuando está sobre hipervínculos)

#### Personalizar Estilos y Plantillas

**Archivo principal de estilos:** `Launcher/Style/LauncherStyle.xaml`

Este archivo contiene todos los estilos visuales del launcher:
- Colores de botones
- Estilos de barras de progreso
- Efectos visuales
- Plantillas de controles

**Recursos útiles para personalización:**
- [MSDN: Estilos y Plantillas](https://msdn.microsoft.com/ru-ru/library/vstudio/bb613570(v=vs.100).aspx)
- [WPF Tutorial](http://professorweb.ru/my/WPF/base_WPF/level1/info_WPF.php) - Conceptos básicos
- [WPF Tutorial.net](http://wpftutorial.net/Home.html) - Ejemplos listos para usar
- [WPF-Tutorial.com](http://www.wpf-tutorial.com) - Lecciones completas

---

## 🏗️ Compilación

### Requisitos

- Visual Studio 2015 o superior (recomendado: Visual Studio 2019+)
- .NET Framework 4.8

### Pasos de Compilación

1. **Abre el proyecto**
   - Abre `Launcher/Launcher.sln` en Visual Studio

2. **Configura URLs y propiedades**
   - Sigue los pasos de la sección "Configuración Paso a Paso" arriba
   - Configura las URLs en las Propiedades del proyecto
   - Actualiza MainWindow.xaml.cs con tus datos
   - Actualiza LNewVer.xaml con tu enlace de descarga

3. **Personaliza el diseño**
   - Cambia las imágenes (ver sección "Personalización" abajo)
   - Actualiza enlaces rápidos
   - Modifica estilos si lo deseas

4. **Compila en modo Release**
   - En Visual Studio: Menú → Compilar → Compilar solución
   - O presiona `Ctrl+Shift+B`
   - Selecciona configuración "Release"

5. **Encuentra tu ejecutable**
   - El launcher compilado estará en: `Launcher/bin/Release/`
   - Distribuye el archivo `.exe` a tus jugadores

### 🔐 Protección de Código (Opcional pero Recomendado)

La plataforma .NET es vulnerable a la ingeniería inversa. Para proteger tu código:

**Herramienta Recomendada:** {Smart Assembly} de RedGate

- Ofusca y protege tu código compilado
- Evita la descompilación y uso no autorizado del código fuente
- Busca tutoriales sobre cómo usar Smart Assembly con aplicaciones WPF

**Pasos:**
1. Compila tu launcher en modo Release
2. Ejecuta Smart Assembly
3. Aplica ofuscación y protección
4. Distribuye el ejecutable protegido

---

## 📦 Utilidad Distribute Updates

Esta herramienta está incluida para generar los archivos `patchlist.txt` y `news.txt`.

### Vista General

Ejecuta `Distribute Updates/Updates.exe` o compila el proyecto desde `Distribute Updates.sln`.

### 📝 Pestaña: Lista de Actualizaciones

**Progreso para crear un nuevo registro:**

1. **Archivo de parche** (`patchlist.txt`)
   - Selecciona un archivo existente o crea uno nuevo

2. **Archivo de actualización**
   - Selecciona archivos `.mpq` o `Wow.exe` parcheado desde tu disco

3. **Nueva URL del archivo**
   - Enlace completo directo para descargar el archivo desde internet
   - Ejemplo: `https://tuservidor.com/parches/patch-enUS-1.mpq`

4. **Hash MD5 y Bytes totales**
   - Se calculan automáticamente al seleccionar el archivo

5. **Agregar y Guardar**
   - Haz clic en **"Agregar a la lista de parches"**
   - Los campos 2-4 se resetearán automáticamente
   - Repite los pasos 2-4 para agregar más archivos
   - Haz clic en **"Guardar en documento de texto"** cuando termines

#### Funciones Adicionales:
- **"Eliminar parche de la lista"**: Elimina el registro seleccionado
- **"Restablecer progreso actual"**: Limpia los campos 2-4
- **"Borrar lista de parches"**: Elimina todos los registros

#### ⚠️ Errores Comunes:

1. **Hash MD5 no coincide**: El hash calculado del archivo no coincide con el archivo descargado
   - Resultado: Descarga constante del archivo cada vez
   - Solución: Verifica que el archivo no se haya corrompido al subirlo

2. **Archivo generado manualmente**: El formato está incorrecto
   - Resultado: El analizador falla, cargador no funciona, aplicación se bloquea
   - Solución: SIEMPRE usa la utilidad Distribute Updates

**Formato correcto del patchlist.txt:**
```
https://servidor.com/patch-enUS-1.mpq#patch-enUS-1.mpq#A1B2C3D4E5F6789#1048576
https://servidor.com/patch-2.mpq#patch-2.mpq#F6E5D4C3B2A1098#2097152
```

### 📰 Pestaña: Generador de Noticias

**Pasos para crear una noticia:**

1. **Archivo de noticias** (`news.txt`)
   - Selecciona un archivo existente o crea uno nuevo

2. **Titular de la noticia**
   - Título de la noticia (aparece en grande)

3. **Resumen de noticias**
   - Texto breve de la noticia (aparece debajo del título)

4. **URL de la imagen a la noticia**
   - URL completa de una imagen de internet
   - Será el fondo de la noticia
   - Ejemplo: `https://tuservidor.com/imagenes/noticia1.jpg`

5. **URL de noticias en el sitio**
   - Al hacer clic en la noticia, abre este enlace en el navegador
   - Para lectura detallada en el sitio web
   - Ejemplo: `https://tuservidor.com/noticias/actualizacion-1`

6. **Agregar y Guardar**
   - Similar a la lista de parches
   - Agrega múltiples noticias antes de guardar

---

## 🌍 Soporte Multi-idioma

El launcher soporta 12 idiomas de WoW:
- enUS (Inglés US)
- esMX (Español México)
- ptBR (Portugués Brasil)
- deDE (Alemán)
- enGB (Inglés GB)
- esES (Español España)
- frFR (Francés)
- itIT (Italiano)
- ruRU (Ruso)
- koKR (Coreano)
- zhTW (Chino Tradicional)
- zhCN (Chino Simplificado)

Los parches con `-{idioma}-` en el nombre se instalan automáticamente en `Data\{idioma}\`.

---

## 🔒 Seguridad

### Verificación MD5

Todos los parches se verifican con hash MD5 antes de instalarse:
- Si el hash no coincide, el archivo se descarga nuevamente
- Los archivos descargados se almacenan con extensión `.upd` hasta que se verifiquen
- Una vez verificados, se mueven a su ubicación final

### Auto-login

Si usas la función de auto-login, ten en cuenta:
- Las credenciales se guardan en `%AppData%\Launcher\user.config`
- **NO están encriptadas** por defecto
- Se envían al proceso del juego mediante inyección de teclas de Windows API

---

## ❓ Solución de Problemas

### El launcher no inicia
- Verifica que .NET Framework 4.8 esté instalado
- Revisa que el launcher no esté ya ejecutándose

### No encuentra Wow.exe
- Coloca el launcher en la carpeta raíz del juego, o
- Usa el botón de configuración para seleccionar la carpeta del juego manualmente

### Los parches no se descargan
- Verifica que las URLs en `App.config` sean correctas y accesibles
- Revisa que el formato de `patchlist.txt` sea correcto
- Comprueba tu conexión a internet

### Error de versión del cliente
- Edita `MainWindow.xaml.cs` líneas 231-249
- Cambia el número de build del cliente (por defecto: 12340 para 3.3.5a)
- Recompila el launcher

### Realmlist no se actualiza
- Verifica que los archivos realmlist no estén marcados como "Solo lectura"
- Asegúrate de tener habilitado el bloque correcto para tu versión de WoW
- Revisa que las carpetas de idioma existan

---

## 📞 Contacto

### Desarrollo

* Гагауз Сергей (Gagauz Sergey) / Jumper

### Información de Contacto

- **Telegram**: [@Jumper92](https://t.me/Jumper92)
- **E-Mail**: gaga-ya@hotmail.com

---

## 🙏 Retroalimentación

Estaré feliz de recibir comentarios sobre deficiencias, sugerencias y tus desarrollos. Crea issues en Github, pull requests, o escribe un mensaje privado (usando los datos de contacto proporcionados).

---

## 📄 Licencia

WoW Launcher se distribuye bajo la licencia GPLv3. Para más información, consulta el archivo [LICENSE](https://github.com/Gagauz2010/WOWLauncher/blob/master/LICENSE).

---

## ⚠️ ADVERTENCIAS IMPORTANTES

### Licencia y Uso

- ❌ **PROHIBIDA LA VENTA**: Este código fuente está prohibido para venta y reventa
- ✅ **USO PERSONAL**: El código fuente está destinado únicamente para uso personal
- 🎯 **PROPÓSITO**: Crear launchers para servidores privados de World of Warcraft

### Configuración de Archivos del Servidor

Todos los archivos que configures en las URLs deben estar en tu servidor web:

**Archivos requeridos:**
1. `patchlist.txt` - Generado con Distribute Updates
2. `realmlist.txt` - Contenido: `set realmlist tu.servidor.com`
3. `version.txt` - Contenido: `1.0.0.0` (versión actual del launcher)
4. `news.txt` - Generado con Distribute Updates (opcional)
5. `updates.txt` - Historial de cambios (opcional)
6. `patchtodel.txt` - Lista de archivos a eliminar (opcional)

### Consejos de Configuración

1. ✅ **Usa enlaces HTTPS directos** - Más seguro y confiable
2. ✅ **Verifica hashes MD5** - Usa siempre Distribute Updates para generar listas
3. ✅ **Prueba antes de distribuir** - Compila y prueba completamente antes de dar a jugadores
4. ✅ **Mantén copias de seguridad** - Guarda tus archivos de configuración
5. ✅ **Documenta tus cambios** - Mantén un registro de modificaciones al código

---

## 🌟 Características Técnicas

### Arquitectura

- **Tecnología**: WPF con .NET Framework 4.8
- **Patrón**: MVVM parcial con code-behind
- **Threading**: BackgroundWorker para operaciones de descarga
- **Serialización**: DataContractJsonSerializer para persistencia

### Sistema de Descargas

- **Protocolo**: HTTP/HTTPS con WebClient
- **Resumible**: Sí, mediante archivos temporales con hash
- **Verificación**: MD5 hash de cada archivo
- **Limitación de velocidad**: Configurable por el usuario
- **Cola**: Sistema de cola FIFO para múltiples parches

### Detección de Cliente

El launcher detecta automáticamente la versión del cliente leyendo:
- `FileVersionInfo` de `Wow.exe`
- Versión de build del ejecutable

### Gestión de Procesos

- **Singleton**: Usa Mutex para prevenir múltiples instancias
- **Inyección de teclas**: Windows API SendMessage para auto-login
- **Notificaciones**: NotifyIcon para modo background

---

## 🚀 Casos de Uso

### Para Administradores de Servidor

1. Configura tu servidor web con los archivos necesarios
2. Personaliza el launcher con tu branding
3. Compila y distribuye a tus jugadores
4. Actualiza `patchlist.txt` cuando agregues nuevos parches

### Para Jugadores

1. Descarga el launcher de tu servidor
2. Ejecuta por primera vez y selecciona la carpeta del juego
3. El launcher descargará los parches necesarios
4. Opcional: Configura auto-login
5. Haz clic en "JUGAR" para iniciar el juego

---

## 🔄 Flujo de Actualización

```
┌─────────────────┐
│  Inicio Launcher│
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Detectar Cliente│
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Actualizar      │
│ Realmlist       │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Eliminar Parches│
│ Antiguos        │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Descargar       │
│ Patchlist       │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Verificar MD5   │
│ de Existentes   │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Descargar       │
│ Parches Nuevos  │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Verificar MD5   │
│ y Mover         │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Listo para      │
│ Jugar           │
└─────────────────┘
```

---

**¡Disfruta de tu servidor de WoW con este launcher personalizado!** 🎮✨
