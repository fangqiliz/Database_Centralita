using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class LlamadaProvincial : Llamada
{
    private const double precioFranja1 = 0.20;
    private const double precioFranja2 = 0.25;
    private const double precioFranja3 = 0.30;
    private int Franja { get; set; }

    public LlamadaProvincial(string numOrigen, string numDestino, double duracion, int franja) : base(numOrigen, numDestino, duracion)
    {
        Franja = franja;
    }

    public override double CalcularPrecio()
    {
        switch (Franja)
        {
            case 1:
                return Duracion * precioFranja1;
            case 2:
                return Duracion * precioFranja2;
            case 3:
                return Duracion * precioFranja3;
            default:
                return 0;
        }
    }

    public override string ToString()
    {
        return base.ToString() + $", Franja: {Franja}, Precio: {CalcularPrecio():C2}";
    }

    public int GetFranja()
    {
        return Franja;
    }

}