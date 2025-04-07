using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;


class Program
{
    static void Main()
    {
        Centralita centralita = new Centralita();

        // Registrar algunas llamadas
        centralita.RegistrarLlamada(new LlamadaLocal("809 475 9831", "829 438 8243", 100));
        centralita.RegistrarLlamada(new LlamadaProvincial("432 727 3272", "632 748 7843", 120, 1));
        centralita.RegistrarLlamada(new LlamadaProvincial("737 372 9859", "236 736 6477", 200, 3));

        // Leer todas las llamadas desde la base de datos
        Console.WriteLine("\nLlamadas registradas en la base de datos:");
        centralita.LeerLlamadasDesdeBaseDeDatos();

        // Mostrar el total de llamadas y facturación
        Console.WriteLine($"Total de llamadas: {centralita.GetTotalLlamada()}");
        Console.WriteLine($"Total Facturado: {centralita.GetTotalFacturado():C2}");
    }
}
