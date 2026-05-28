namespace TiendaInventario
{
  /// <summary>
  /// Sistema de inventario para una tienda con múltiples sucursales.
  /// Demuestra el uso profesional de bucles for anidados en un contexto real.
  /// </summary>
  class Program
  {
    // ── Datos de la tienda ───────────────────────────────────────────────
    static readonly string[] Sucursales = { "Centro", "Norte", "Sur", "Occidente" };

    static readonly string[] Categorias = { "Electrónica", "Ropa", "Alimentos", "Hogar" };

    static readonly string[,] Productos = {
            { "Celular",     "Televisor",  "Audífonos",  "Cargador"  },
            { "Camiseta",    "Pantalón",   "Zapatos",    "Chaqueta"  },
            { "Arroz",       "Aceite",     "Pasta",      "Leche"     },
            { "Lámpara",     "Cojín",      "Sábanas",    "Tapete"    }
        };

    // Precio base por producto (categoría x producto)
    static readonly double[,] PreciosBase = {
            { 850000, 1200000, 180000,  45000  },
            { 35000,  80000,   120000,  200000 },
            { 4500,   12000,   3800,    5200   },
            { 48000,  25000,   65000,   38000  }
        };

    // Stock por sucursal, categoría y producto [sucursal, categoría, producto]
    static int[,,] Stock = new int[4, 4, 4];

    // ── Punto de entrada ─────────────────────────────────────────────────
    static void Main(string[] args)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;

      InicializarStock();

      Encabezado("🏪  SISTEMA DE INVENTARIO — TIENDA ÉXITO EXPRESS");

      MostrarInventarioCompleto();
      MostrarResumenPorSucursal();
      MostrarProductosMasVendibles();
      BuscarStockBajo(umbral: 8);

      Console.WriteLine("\n  Presiona cualquier tecla para salir...");
      Console.ReadKey();
    }

    // ────────────────────────────────────────────────────────────────────
    // PASO 1: Llenar stock con datos simulados (triple for anidado)
    // ────────────────────────────────────────────────────────────────────
    static void InicializarStock()
    {
      var rng = new Random(99);

      // 3 niveles: sucursal → categoría → producto
      for (int s = 0; s < Sucursales.Length; s++)
        for (int c = 0; c < Categorias.Length; c++)
          for (int p = 0; p < 4; p++)
            Stock[s, c, p] = rng.Next(2, 50);

      // Forzar algunos stocks bajos para la alerta
      Stock[0, 2, 1] = 3;   // Centro  - Alimentos - Aceite
      Stock[2, 0, 0] = 5;   // Sur     - Electrónica - Celular
      Stock[3, 1, 3] = 4;   // Occ.    - Ropa - Chaqueta
    }

    // ────────────────────────────────────────────────────────────────────
    // MÓDULO 1: Inventario completo (triple for anidado visual)
    // ────────────────────────────────────────────────────────────────────
    static void MostrarInventarioCompleto()
    {
      Seccion("MÓDULO 1 — Inventario Completo por Sucursal");

      // Nivel 1: sucursales
      for (int s = 0; s < Sucursales.Length; s++)
      {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n  📍 Sucursal: {Sucursales[s]}");
        Console.ResetColor();
        Console.WriteLine($"  {"Categoría",-14} {"Producto",-14} {"Stock",8}  {"Precio",12}  {"Valor en bodega",16}");
        Console.WriteLine("  " + new string('─', 68));

        // Nivel 2: categorías
        for (int c = 0; c < Categorias.Length; c++)
        {
          // Nivel 3: productos dentro de la categoría
          for (int p = 0; p < 4; p++)
          {
            int unidades = Stock[s, c, p];
            double precio = PreciosBase[c, p];
            double valorBodega = unidades * precio;

            // Resaltar en rojo si hay stock bajo
            if (unidades <= 7)
              Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"  {Categorias[c],-14} {Productos[c, p],-14} {unidades,8}  {precio,12:C0}  {valorBodega,16:C0}");
            Console.ResetColor();
          }
        }
      }
      Console.WriteLine();
    }

    // ────────────────────────────────────────────────────────────────────
    // MÓDULO 2: Resumen financiero por sucursal (doble for anidado)
    // ────────────────────────────────────────────────────────────────────
    static void MostrarResumenPorSucursal()
    {
      Seccion("MÓDULO 2 — Valor Total del Inventario por Sucursal");

      double granTotal = 0;

      Console.WriteLine($"\n  {"Sucursal",-14} {"Unidades Totales",18} {"Valor Total Inventario",24}");
      Console.WriteLine("  " + new string('─', 58));

      // Nivel 1: sucursales
      for (int s = 0; s < Sucursales.Length; s++)
      {
        int totalUnidades = 0;
        double totalValor = 0;

        // Nivel 2: todos los productos (categoría + producto)
        for (int c = 0; c < Categorias.Length; c++)
          for (int p = 0; p < 4; p++)
          {
            totalUnidades += Stock[s, c, p];
            totalValor += Stock[s, c, p] * PreciosBase[c, p];
          }

        granTotal += totalValor;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  {Sucursales[s],-14} {totalUnidades,18} {totalValor,24:C0}");
        Console.ResetColor();
      }

      Console.WriteLine("  " + new string('─', 58));
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"  {"GRAN TOTAL",-14} {"",18} {granTotal,24:C0}");
      Console.ResetColor();
      Console.WriteLine();
    }

    // ────────────────────────────────────────────────────────────────────
    // MÓDULO 3: Producto más vendible por categoría (doble for anidado)
    // ────────────────────────────────────────────────────────────────────
    static void MostrarProductosMasVendibles()
    {
      Seccion("MÓDULO 3 — Producto con Mayor Stock por Categoría (todas las sucursales)");

      Console.WriteLine();

      // Nivel 1: categorías
      for (int c = 0; c < Categorias.Length; c++)
      {
        int maxStock = 0;
        int indexMax = 0;

        // Nivel 2: acumular stock de todas las sucursales por producto
        for (int p = 0; p < 4; p++)
        {
          int stockTotal = 0;

          for (int s = 0; s < Sucursales.Length; s++)
            stockTotal += Stock[s, c, p];

          if (stockTotal > maxStock)
          {
            maxStock = stockTotal;
            indexMax = p;
          }
        }

        Console.Write($"  📦 {Categorias[c],-14} → ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write($"{Productos[c, indexMax],-14}");
        Console.ResetColor();
        Console.WriteLine($"  ({maxStock} unidades en red)");
      }
      Console.WriteLine();
    }

    // ────────────────────────────────────────────────────────────────────
    // MÓDULO 4: Alerta de stock bajo (triple for — búsqueda con umbral)
    // ────────────────────────────────────────────────────────────────────
    static void BuscarStockBajo(int umbral)
    {
      Seccion($"MÓDULO 4 — ⚠  Alertas de Stock Bajo (menos de {umbral} unidades)");

      bool hayAlertas = false;
      Console.WriteLine();

      // Triple for: recorre toda la estructura
      for (int s = 0; s < Sucursales.Length; s++)
      {
        for (int c = 0; c < Categorias.Length; c++)
        {
          for (int p = 0; p < 4; p++)
          {
            if (Stock[s, c, p] < umbral)
            {
              hayAlertas = true;
              Console.ForegroundColor = ConsoleColor.Red;
              Console.Write($"  ⚠  ALERTA");
              Console.ResetColor();
              Console.WriteLine(
                  $"  Sucursal: {Sucursales[s],-10} " +
                  $"Categoría: {Categorias[c],-12} " +
                  $"Producto: {Productos[c, p],-12} " +
                  $"Stock actual: {Stock[s, c, p]} uds."
              );
            }
          }
        }
      }

      if (!hayAlertas)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  ✔ Todo el inventario está en niveles óptimos.");
        Console.ResetColor();
      }
      Console.WriteLine();
    }

    // ── Helpers de consola ───────────────────────────────────────────────
    static void Encabezado(string titulo)
    {
      string linea = new string('═', titulo.Length + 4);
      Console.WriteLine($"\n  ╔{linea}╗");
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine($"  ║  {titulo}  ║");
      Console.ResetColor();
      Console.WriteLine($"  ╚{linea}╝\n");
    }

    static void Seccion(string titulo)
    {
      Console.WriteLine("\n  ┌─────────────────────────────────────────────────────────────┐");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine($"  │  {titulo,-61}│");
      Console.ResetColor();
      Console.WriteLine("  └─────────────────────────────────────────────────────────────┘");
    }
  }
}
