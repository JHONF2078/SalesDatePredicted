# SalesDatePrediction


Esta aplicacion permite visualizar clientes con información de órdenes, predecir la fecha de la proxima orden  y graficar datos con D3.js.

---

##  Build y ejecución del proyecto

###  Requisitos

- .NET 8 SDK
- Node.js 20+
- Angular CLI 19+
- SQL Server

###  Configuración

1. Ejecutar el script de base de datos (`DBSetup.sql`) en SQL Server.
   - Recuerda cambiar la cadena de conexión a la bases de datos en el archivo appsettings.json
2. Abrir la solución en Visual Studio.
3. Configurar los proyectos de inicio:
   - `SalesDatePrediction.Server`
   - `salesdateprediction.client`
4. Ejecutar la solución (`F5` o botón "Start").


> **Alternativamente**, se puede configurar el proyecto SalesDatePrediction.Server como proyecto de inicio y ejecutar  la parte de Angular desde consola con:
>
> ```bash
> cd salesdateprediction.client
> npm install
> ng serve
> ```

---

##  Pruebas

Las pruebas se realizaron con **xUnit**, ubicadas en la carpeta de pruebas dentro de la solución. Para ejecutarlas:

1. Abrir la solución en Visual Studio.
2. Dirigirse al menú **Test** y seleccionar **Run All Tests** o usar la combinación de teclas `Ctrl + R, A`.

> También es posible ejecutarlas desde la línea de comandos con `dotnet test`.

---

##  Tecnologías y patrones utilizados

- **Backend**: .NET 8, ASP.NET Core
- **Frontend**: Angular 19, Angular Material, componentes standalone, D3.js
- **Patrones**: CQRS, Unit of Work
- **Pruebas**: xUnit
- **Base de datos**: SQL Server

---

##  Arquitectura por capas

1. **SalesDatePrediction.Application**: Lógica de negocio y casos de uso (CQRS, DTOs, Handlers).
2. **SalesDatePrediction.Domain**: Entidades del dominio y contratos.
3. **SalesDatePrediction.Infrastructure**: Acceso a datos, repositorios, implementación de Unit of Work.
4. **SalesDatePrediction.Server**: API de ASP.NET Core.
5. **SalesDatePrediction.Client**: Aplicación Angular 19.
6. **SalesDatePrediction.Tests**: Pruebas unitarias con xUnit.

---


