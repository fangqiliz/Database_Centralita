using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class Llamada
{
    public string NumOrigen { get; set; }
    public string NumDestino { get; set; }
    public double Duracion { get; set; }

    public Llamada(string numOrigen, string numDestino, double duracion)
    {
        NumOrigen = numOrigen;
        NumDestino = numDestino;
        Duracion = duracion;
    }

    public virtual double CalcularPrecio()
    {
        return 0;
    }

    public override string ToString()
    {
        return $"Origen: {NumOrigen}, Destino: {NumDestino}, Duracion: {Duracion} segundos";
    }



}