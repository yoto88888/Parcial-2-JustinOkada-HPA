# Laboratorio: Patrón MVP y Seguridad en C# (.NET)

Este proyecto es una aplicación de Windows Forms desarrollada en C# que implementa la arquitectura **MVP (Modelo-Vista-Presentador)** y el diseño en múltiples capas. El objetivo principal es demostrar la separación de responsabilidades, el acceso a datos mediante MySQL y la aplicación de buenas prácticas de seguridad (Hashing y Salting de contraseñas) a través de contratos (Interfaces).

**Curso:** Herramientas de la Programación Aplicada III (.Net)

**Docente:** Ing. Irina Fong

## 🚀 Características y Requisitos Cumplidos

* **Arquitectura MVP:** Separación estricta entre la interfaz de usuario (Vista), la lógica de control (Presentador) y el acceso a datos (Modelo/Datos).

* **Seguridad (Capa de Negocio):** Implementación del principio de responsabilidad única utilizando dos contratos independientes:

  * `IHashService`: Encargado de generar el Hash (SHA-256) y el Salt dinámico para nuevos registros.

  * `IVerificarHashService`: Encargado de validar contraseñas en texto plano contra los hashes almacenados.

* **Base de Datos MySQL:** Operaciones de lectura y escritura (Login y Registro) usando la librería `MySql.Data`.

* **Interfaz Gráfica (UI):**

  * Menú lateral (Panel Dock).

  * Reloj en tiempo real utilizando el control `Timer`.

  * Control de sesión: Visualización dinámica del usuario actualmente logeado en la parte inferior del menú.

## 📁 Estructura del Proyecto

El código está organizado en las siguientes carpetas físicas y espacios de nombres para asegurar el desacoplamiento:

* 📂 **Entidades:** Clases puras (DTOs) como `Usuario` que transportan la información entre capas.

* 📂 **Datos:** Clases como `UsuarioRepository` encargadas de la conexión y consultas a MySQL.

* 📂 **Negocio:** El núcleo de las reglas del programa. Contiene las interfaces de seguridad y su implementación (`PasswordSeguridadService`).

* 📂 **Presentadores:** Contiene `LoginPresenter`, que actúa como mediador, orquestando las interacciones entre la Vista, el Negocio y los Datos.

* 📂 **Vistas:** Contiene la interfaz gráfica (`Form1`). Es completamente pasiva y solo interactúa con el Presentador.

## 🛠️ Requisitos Previos (Prerequisites)

Para ejecutar este proyecto, necesitarás tener instalado:

* Visual Studio (con la carga de trabajo de desarrollo de escritorio de .NET).

* Servidor MySQL (MySQL Workbench).

* El paquete NuGet `MySql.Data` instalado en el proyecto.

## ⚙️ Configuración de la Base de Datos

Antes de ejecutar la aplicación, debes crear la base de datos y la tabla correspondiente en tu servidor MySQL local. Ejecuta el siguiente script en MySQL Workbench:

```sql
CREATE DATABASE LaboratorioMVP;
USE LaboratorioMVP;

CREATE TABLE Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Salt VARCHAR(255) NOT NULL
);
```

**Nota sobre la conexión:**

Asegúrate de revisar el archivo `UsuarioRepository.cs` en la carpeta **Datos** y modificar la variable `_cadenaConexion` para que coincida con tu contraseña del usuario `root` de MySQL:

```csharp
private string _cadenaConexion = "Server=localhost;Database=LaboratorioMVP;Uid=root;Pwd=TuContraseñaReal;";
```

## 💻 Instrucciones de Uso

1. Clona este repositorio o descarga el código fuente.
2. Abre el archivo `.sln` con Visual Studio.
3. Asegúrate de que la base de datos esté creada y en ejecución.
4. Compila la solución (`Ctrl + Shift + B`) para restaurar los paquetes NuGet.
5. Inicia el programa (Botón "Iniciar" o `F5`).
6. Utiliza el botón **Registrar** para crear un usuario de prueba en la base de datos (con encriptación real).
7. Utiliza el botón **Ingresar** para iniciar sesión y observar cómo la interfaz actualiza el estado de la sesión.

---

## 📸 Demostración del Funcionamiento

<img width="837" height="551" alt="Screenshot 2026-11-08 231150" src="https://github.com/user-attachments/assets/64b65d67-f695-4a02-a7dc-0643da7668dc" />

