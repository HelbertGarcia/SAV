# Proyecto: Pipeline ETL para Análisis de Ventas

Este proyecto implementa un pipeline de **Extraer, Transformar y Cargar (ETL)** en C# (.NET) utilizando principios de **Arquitectura Limpia**. El sistema lee datos de ventas desde múltiples archivos CSV, los procesa y los carga en una base de datos SQL Server normalizada para su posterior análisis.

---
## Flujo de Trabajo (Workflow)

El pipeline sigue un proceso secuencial de tres fases para asegurar la integridad y calidad de los datos:

### 1. Extracción
El proceso inicia leyendo los datos desde cuatro archivos CSV (`customers.csv`, `products.csv`, etc.). La librería **CsvHelper** se utiliza para parsear eficientemente cada archivo y convertir sus filas en una lista de Objetos de Transferencia de Datos (DTOs) en memoria.

### 2. Transformación
Una vez extraídos, los datos pasan por una fase de transformación. El `ImportSalesUseCase` orquesta este paso, mapeando los DTOs a las Entidades del Dominio (`Customer`, `Product`, etc.). Durante este mapeo, se aplican reglas de limpieza, como la eliminación de duplicados y la validación de valores como el "Status" de las órdenes.

### 3. Carga
En la fase final, los datos ya limpios y transformados son persistidos en la base de datos `SalesAnalysisDB`. Se utiliza **Entity Framework Core** para interactuar con SQL Server. La carga se realiza de manera transaccional y en un orden específico para respetar las restricciones de claves foráneas.

---
## Tecnologías Utilizadas
* **Lenguaje:** C# 12
* **Framework:** .NET 8 (Worker Service)
* **Arquitectura:** Arquitectura Limpia (Clean Architecture)
* **Base de Datos:** SQL Server
* **ORM:** Entity Framework Core 8
* **Lectura de CSV:** CsvHelper

---
## Cómo Ejecutar el Proyecto

Sigue estos pasos para poner en marcha el pipeline:

1.  **Clonar el Repositorio:**
    ```bash
    git clone https://github.com/tu-usuario/tu-repositorio.git
    ```
2.  **Crear la Base de Datos:** Ejecuta el script SQL proporcionado en SQL Server Management Studio para crear la base de datos `SalesAnalysisDB` y sus tablas.

3.  **Configurar la Conexión:** Abre el archivo `Worker/appsettings.json` y modifica la `DefaultConnection` para que apunte a tu instancia de SQL Server.

4.  **Añadir los Archivos CSV:** Coloca los cuatro archivos CSV (`customers.csv`, `products.csv`, etc.) dentro de la carpeta `Worker/CSVs`.

5.  **Ejecutar la Aplicación:** Abre la solución en Visual Studio y ejecuta el proyecto `Worker`. La consola mostrará el progreso del proceso ETL.
