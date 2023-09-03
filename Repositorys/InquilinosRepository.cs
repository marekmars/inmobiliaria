using System;
using System.Collections.Generic;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Inmobiliaria.Models;
public class InquilinosRepository
{

    protected readonly string connectionString;
    public InquilinosRepository()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }
    public List<Inquilino> GetAllInquilinos()
    {
        List<Inquilino> inquilinos = new List<Inquilino>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, dni, apellido, nombre, telefono, correo FROM inquilinos WHERE estado=1 ORDER BY apellido ";

            using (MySqlCommand command = new(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inquilino inquilino = new()
                        {
                            Id = reader.GetInt32("id"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                        };

                        inquilinos.Add(inquilino);
                    }
                    connection.Close();
                }
            }
        }
        Console.WriteLine("INQUILINOS" + inquilinos);
        return inquilinos;
    }
    public Inquilino GetInquilinoById(int id)
    {
        Inquilino inquilino = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, dni, apellido, nombre, telefono, correo,estado FROM inquilinos WHERE id=@id AND estado=1";

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inquilino = new()
                        {
                            Id = reader.GetInt32("id"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                            Estado = reader.GetBoolean("estado")
                        };

                    }
                    connection.Close();
                }
            }
        }
        return inquilino;
    }
    public Inquilino GetInquilinoByDni(string dni)
    {
        Inquilino inquilino = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, dni, apellido, nombre, telefono, estado, correo FROM inquilinos WHERE dni=@dni ";

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@dni", dni);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inquilino = new()
                        {
                            Id = reader.GetInt32("id"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                            Estado = reader.GetBoolean("estado")
                        };

                    }
                    connection.Close();
                }
            }
        }
        return inquilino;
    }

    public int CreateInquilino(Inquilino inquilino)
    {
        var res = -1;
        if (!EsNumeroTelefonoValido(inquilino.Telefono))
        {
            res = -2;
            return res;
        }

        // Verificar si el correo electrónico es válido
        if (!EsCorreoElectronicoValido(inquilino.Correo))
        {
            res = -3;
            return res;
        }

        // Verificar si el DNI es válido
        if (!EsDniValido(inquilino.Dni))
        {
            res = -4;
            return res;
        }
        
    
        
        Inquilino inquilinoAux = GetInquilinoByDni(inquilino.Dni);
  

        try
        {
            if (inquilinoAux.Nombre == ""||!inquilinoAux.Estado)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"INSERT INTO `inquilinos`( `dni`, `apellido`, `nombre`, `telefono`, `correo`,`estado`) 
            VALUES (@Dni, @Apellido, @Nombre, @Telefono, @Correo,@Estado);
            SELECT LAST_INSERT_ID()";

                    using (MySqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@Dni", inquilino.Dni);
                        command.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                        command.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                        command.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                        command.Parameters.AddWithValue("@Correo", inquilino.Correo);
                        command.Parameters.AddWithValue("@Estado", true);
                        connection.Open();
                        res = Convert.ToInt32(command.ExecuteScalar());
                        inquilino.Id = res;
                        connection.Close();

                    }
                }
            }
            else
            {
                res = -5;
            }

        }

        catch (System.Exception)
        {
            throw;
        }



        return res;

    }

    public int DeleteInquilino(int id)
    {
        var res = -1;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"UPDATE `inquilinos` SET `estado` = 0 WHERE `id` = @Id;";

            using (MySqlCommand command = new(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }


        }
        return res;

    }

    public int UpdateInquilino(Inquilino inquilino)
    {
        var res = -1;

        if (!EsNumeroTelefonoValido(inquilino.Telefono))
        {
            res = -2;
            return res;
        }

        // Verificar si el correo electrónico es válido
        if (!EsCorreoElectronicoValido(inquilino.Correo))
        {
            res = -3;
            return res;
        }

        // Verificar si el DNI es válido
        if (!EsDniValido(inquilino.Dni))
        {
            res = -4;
            return res;
        }

        Inquilino inquilinoAux = GetInquilinoByDni(inquilino.Dni);

        if (inquilinoAux.Nombre == "" || inquilinoAux.Id == inquilino.Id||!inquilinoAux.Estado)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE `inquilinos` SET 
                        `dni` = @Dni,
                        `apellido` = @Apellido,
                        `nombre` = @Nombre,
                        `telefono` = @Telefono,
                        `correo` = @Correo
                        WHERE `id` = @Id";

                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", inquilino.Id);
                    command.Parameters.AddWithValue("@Dni", inquilino.Dni);
                    command.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                    command.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                    command.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                    command.Parameters.AddWithValue("@Correo", inquilino.Correo);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }

            }
        }
        else
        {
            return -5;
        }
        return res;

    }
    private bool EsNumeroTelefonoValido(string telefono)
    {

        string patron = @"^\+\d{2} \d{2,3}-\d{6,7}$";
        return Regex.IsMatch(telefono, patron);
    }

    private bool EsCorreoElectronicoValido(string correo)
    {

        string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(correo, patron);
    }

    private bool EsDniValido(string dni)
    {

        string patron = @"^\d{2}\.\d{3}\.\d{3}$";
        return Regex.IsMatch(dni, patron);
    }
}
