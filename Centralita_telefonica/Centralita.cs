using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class Centralita
{
    private List<Llamada> llamadas;
    private int cont;
    private double acum;
    private string connectionString = "Server=tcp:programacion-server.database.windows.net,1433;Initial Catalog=centralita-database;Persist Security Info=False;User ID=sqladmin;Password=Flz192006;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public Centralita()
    {
        llamadas = new List<Llamada>();
        cont = 0;
        acum = 0;
    }

    public void RegistrarLlamada(Llamada llamada)
    {
        llamadas.Add(llamada);
        cont++;
        double precio = llamada.CalcularPrecio();
        acum += precio;
        GuardarEnBaseDeDatos(llamada, precio);
        Console.WriteLine("Llamada registrada: " + llamada);
    }

    private void GuardarEnBaseDeDatos(Llamada llamada, double precio)
    {
        string tipo = llamada is LlamadaLocal ? "Local" : "Provincial";
        int? franja = (llamada is LlamadaProvincial provincial) ? provincial.GetFranja() : (int?)null;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO Llamadas (TipoLlamada, NumOrigen, NumDestino, Duracion, Franja, Precio) VALUES (@tipo, @origen, @destino, @duracion, @franja, @precio)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@origen", llamada.NumOrigen);
                cmd.Parameters.AddWithValue("@destino", llamada.NumDestino);
                cmd.Parameters.AddWithValue("@duracion", llamada.Duracion);
                cmd.Parameters.AddWithValue("@franja", (object?)franja ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void LeerLlamadasDesdeBaseDeDatos()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Llamadas";  // Asegúrate de que el nombre de la tabla sea correcto
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string tipoLlamada = reader["TipoLlamada"].ToString();
                    string numOrigen = reader["NumOrigen"].ToString();
                    string numDestino = reader["NumDestino"].ToString();
                    double duracion = Convert.ToDouble(reader["Duracion"]);
                    int? franja = reader["Franja"] != DBNull.Value ? (int?)reader["Franja"] : null;
                    double precio = Convert.ToDouble(reader["Precio"]);

                    Llamada llamada = null;

                    if (tipoLlamada == "Local")
                    {
                        llamada = new LlamadaLocal(numOrigen, numDestino, duracion);
                    }
                    else if (tipoLlamada == "Provincial" && franja.HasValue)
                    {
                        llamada = new LlamadaProvincial(numOrigen, numDestino, duracion, franja.Value);
                    }

                    if (llamada != null)
                    {
                        Console.WriteLine(llamada.ToString());
                    }
                }
            }
        }
    }

    public int GetTotalLlamada()
    {
        return cont;
    }

    public double GetTotalFacturado()
    {
        return acum;
    }
}
