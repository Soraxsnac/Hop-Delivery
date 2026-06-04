# 🍺 Hop Delivery

Hop Delivery es una solución de software de gestión de catálogos de cervezas y marcas, compuesta por una aplicación móvil multiplataforma y una API RESTful backend. El sistema incluye autenticación segura de usuarios, gestión de inventario y almacenamiento en la nube.

## 🏗️ Arquitectura y Tecnologías

El proyecto está dividido en dos capas principales:

### 1. Frontend (Aplicación Móvil)
* **Framework:** .NET MAUI
* **Lenguajes:** C#, XAML
* **Patrón de Arquitectura:** Integración de Vistas y Modelos con Inyección de Dependencias.
* **Características:**
  * Consumo de API RESTful mediante `HttpClient` (`ApiService`).
  * Almacenamiento seguro de credenciales con `SecureStorage`.
  * Navegación controlada (Login/Registro -> AppShell/Catálogo).

### 2. Backend (Web API)
* **Framework:** ASP.NET Core Web API
* **Seguridad:** ASP.NET Core Identity + JSON Web Tokens (JWT) para autenticación y autorización.
* **Base de Datos:** Azure Database for MySQL Flexible Server (versión 8.0).
* **ORM:** Entity Framework Core (`IdentityDbContext`).
* **Características:**
  * Endpoints protegidos para registro e inicio de sesión de usuarios.
  * Operaciones CRUD para Cervezas y Marcas (Catálogos).
  * Reglas de contraseñas personalizadas para agilizar el entorno de desarrollo.

---

## 🚀 Requisitos Previos

Para ejecutar y compilar este proyecto de forma local, necesitarás:
* [Visual Studio 2022](https://visualstudio.microsoft.com/) (en Windows) o Visual Studio Code / Rider (en Mac).
* Carga de trabajo de **.NET MAUI** instalada.
* SDK de **.NET 8** (o la versión correspondiente al proyecto).
* Acceso a la base de datos MySQL hospedada en Azure (requiere la IP permitida en el Firewall de Azure).

---

## ⚙️ Configuración y Despliegue

Sigue estos pasos para arrancar el proyecto desde cero:

### Paso 1: Configurar el Backend (`HopDelivery.API`)
1. Abre el proyecto backend en Visual Studio.
2. Localiza el archivo `appsettings.json` y asegúrate de configurar tu conexión a la base de datos de Azure y la llave JWT:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=TU_SERVIDOR.mysql.database.azure.com; Port=3306; Database=HopDeliveryDB; Uid=TU_USUARIO; Pwd=TU_PASSWORD; SslMode=Required;"
     },
     "jwtkey": "TuLlaveSuperSecretaYMuyLargaParaTokensDe64Caracteres"
   }