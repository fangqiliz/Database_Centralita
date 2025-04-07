using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class LlamadaLocal : Llamada
{
    private const double precioPorSegundo = 0.15;

    public LlamadaLocal(string numOrigen, string numDestino, double duracion) : base(numOrigen, numDestino, duracion) { }

    public override double CalcularPrecio()
    {
        return Duracion * precioPorSegundo;
    }

    public override string ToString()
    {
        return base.ToString() + $", precio: {CalcularPrecio():C2}";
    }

}
