# Guía para Contribuir 🛠️

¡Gracias por interesarte en contribuir a Restaurant Orders System! Este documento explica cómo participar efectivamente.

## Tabla de Contenidos
1. [Reportar Errores](#-reportar-errores)
2. [Sugerir Mejoras](#-sugerir-mejoras)
3. [Entorno de Desarrollo](#-entorno-de-desarrollo)
4. [Estándares de Código](#-estándares-de-código)
5. [Enviar Pull Requests](#-enviar-pull-requests)
6. [Revisión de Código](#-revisión-de-código)
7. [Licencia](#-licencia)

---

## 🐛 Reportar Errores
- **Verifica primero** si el error ya está reportado en [Issues](https://github.com/manuelZapata90/restaurant-orders-system/issues).
- Usa la plantilla `Bug Report` y provee:
  - Pasos para reproducir el error.
  - Comportamiento esperado vs. actual.
  - Capturas de pantalla (si aplica).
  - Ambiente (SO, versión de .NET, dispositivo).

_Ejemplo de título:_  

`[BUG] La app crashea al guardar pedido sin conexión`

---

## 🚀 Sugerir Mejoras
- **Busca primero** si la idea ya fue propuesta.
- Usa la plantilla `Feature Request` y describe:
  - ¿Qué problema resuelve?
  - Propuesta de solución (opcional).
  - Casos de uso clave.

_Ejemplo de título:_  
`[FEATURE] Integrar scanner de QR para menú digital`

---

## 💻 Entorno de Desarrollo

### Requisitos
- .NET 8 SDK
- PostgreSQL 14+
- IDE (Visual Studio 2022, VS Code o Rider)

### Configuración Inicial
1. Clona el repositorio:
    ```bash
    git clone https://github.com/manuelZapata90/restaurant-orders-system.git

2. Base de datos:
    ```bash
    CREATE DATABASE RestaurantOrders;
    -- Ejecuta scripts en /src/Backend/Data/Migrations
  
3. Configura variables de entorno (Backend/appsettings.Development.json):

    ```bash
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=RestaurantOrders;User Id=postgres;Password=tu_contraseña;"
    }

4. Ejecuta el backend:

    ```bash
    cd src/Backend
    dotnet run

---

# 📜 Estándares de Código


## General


### Patrones de diseño

- Frontend: MVVM (.NET MAUI).
- Backend: Clean Architecture (Controllers → Services → Repositories).

### Nombrado

- Variables/métodos: PascalCase (C# estándar).
- Interfaces: IOrdenService (Prefijo "I").


### Comentarios

Documenta clases/métodos complejos con XML:

  ``` C#
   /// <summary>
   /// Envía un pedido a la cocina
   /// </summary>
   public void EnviarPedido() { ... }
   ```

---

# 🔀 Enviar Pull Requests

1. Crea una rama desde dev:
   
   ``` bash
   git checkout -b feature/nombre-de-la-funcionalidad

2. Haz commits atómicos con mensajes descriptivos:

   ``` bash
   feat: Añadir sincronización offline para pedidos
   fix: Corregir cálculo de total en pedidos
   docs: Actualizar guía de instalación

3. Sube los cambios

  ``` bash
  git push origin feature/nombre-de-la-funcionalidad
  ```
 
4. Abre un Pull Request:

- Usa la plantilla de PR.
- Vincula el Issue relacionado (ej: Closes #12).