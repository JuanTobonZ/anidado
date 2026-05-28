# 🏪 TiendaInventario — Sistema de Inventario en C#

> Demostración profesional del uso de **bucles `for` anidados** en C# aplicado a un sistema de inventario multi-sucursal con 3 niveles de profundidad.

---

## 📋 Descripción

**TiendaInventario** simula el sistema de gestión de inventario de una cadena de tiendas con 4 sucursales, 4 categorías de productos y 4 artículos por categoría. El proyecto demuestra cómo los bucles `for` anidados resuelven problemas reales del mundo empresarial: recorrer estructuras de datos tridimensionales, calcular totales, generar reportes y emitir alertas automáticas.

---

## 🗂️ Estructura del Proyecto

```
TiendaInventario/
├── TiendaInventario.cs     # Código fuente principal
└── README.md               # Este archivo
```

---

## ⚙️ Requisitos

| Requisito        | Versión mínima |
|------------------|----------------|
| .NET SDK         | 6.0 o superior |
| Sistema operativo| Windows / Linux / macOS |

---

## ▶️ Instalación y Ejecución

### Opción 1 — Proyecto `dotnet` (recomendado)

```bash
# 1. Crear proyecto
dotnet new console -n TiendaInventario
cd TiendaInventario

# 2. Reemplazar Program.cs con el archivo descargado
cp TiendaInventario.cs Program.cs

# 3. Ejecutar
dotnet run
```

### Opción 2 — Compilación directa con Mono

```bash
csc TiendaInventario.cs
mono TiendaInventario.exe
```

---

## 🧩 Módulos del Sistema

### Módulo 1 — Inventario Completo
Recorre **sucursal → categoría → producto** (triple `for` anidado) y muestra una tabla con:
- Unidades en stock
- Precio unitario
- Valor total en bodega
- Resaltado en 🔴 rojo si el stock es crítico

### Módulo 2 — Resumen Financiero por Sucursal
Doble `for` anidado que acumula unidades y valor económico por cada sucursal, mostrando el **gran total** de la red.

### Módulo 3 — Producto Estrella por Categoría
Triple `for` que suma el stock de todas las sucursales por producto e identifica cuál tiene **mayor disponibilidad** en la red.

### Módulo 4 — Alertas de Stock Bajo ⚠️
Triple `for` de búsqueda que detecta productos con **menos de 8 unidades** e imprime alertas indicando sucursal, categoría y producto exactos.

---

## 🏗️ Estructura de Datos

```
Stock[sucursal, categoría, producto]
  ├── 4 Sucursales:   Centro · Norte · Sur · Occidente
  ├── 4 Categorías:   Electrónica · Ropa · Alimentos · Hogar
  └── 4 Productos por categoría (ej: Celular, Televisor, Audífonos, Cargador)
```

El arreglo tridimensional `int[,,]` almacena el stock de cada combinación posible, dando un total de **64 registros** gestionados con bucles anidados.

---

## 🔑 Conceptos de C# Aplicados

| Concepto                    | Dónde se usa                            |
|-----------------------------|-----------------------------------------|
| `for` anidado (2 niveles)   | Módulo 2 — resumen financiero           |
| `for` anidado (3 niveles)   | Módulos 1, 3 y 4 — inventario completo  |
| Arreglos multidimensionales | `int[,,] Stock`, `string[,] Productos`  |
| `Console.ForegroundColor`   | Resaltado de alertas y totales          |
| Constantes y semilla aleatoria | `Random(99)` para datos reproducibles |
| Métodos estáticos           | Separación por responsabilidad          |
| Formato de moneda           | `{valor:C0}` — pesos colombianos        |

---

## 📸 Ejemplo de Salida

```
  ╔══════════════════════════════════════════════════════════╗
  ║  🏪  SISTEMA DE INVENTARIO — TIENDA ÉXITO EXPRESS        ║
  ╚══════════════════════════════════════════════════════════╝

  📍 Sucursal: Centro
  Categoría      Producto        Stock       Precio    Valor en bodega
  ────────────────────────────────────────────────────────────────────
  Electrónica    Celular            24    $850.000       $20.400.000
  Electrónica    Televisor          11  $1.200.000       $13.200.000
  ...
  ⚠  ALERTA  Sucursal: Centro    Categoría: Alimentos  Producto: Aceite  Stock: 3 uds.
```

---

## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Puedes:
- Agregar más sucursales o categorías en los arreglos estáticos
- Implementar entrada de usuario para actualizar stock en tiempo real
- Exportar el reporte a un archivo `.csv` o `.txt`

---

## 📄 Licencia

Este proyecto es de uso libre para fines educativos y de aprendizaje.
