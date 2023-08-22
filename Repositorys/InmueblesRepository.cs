using System;
using System.Collections.Generic;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; // Asegúrate de tener la referencia correcta a MySql.Data
using System.Text.RegularExpressions;


namespace Inmobiliaria.Models;
public class InmueblesRepository
{

    protected readonly string connectionString;
    public InmueblesRepository()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Inmueble> GetAllInmuebles()
    {
        List<Inmueble> inmuebles = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, idPropietario, direccion, uso, tipo, cantAmbientes, latitud, longitud, precio FROM inmuebles";
            PropietariosRepository propietariosRepo = new();
            using (MySqlCommand command = new(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Propietario propietario = propietariosRepo.GetPropietarioById(reader.GetInt32("idPropietario"));

                        Inmueble inmueble = new()
                        {
                            Id = reader.GetInt32("id"),
                            Propietario = propietario,
                            Direccion = reader.GetString("direccion"),
                            Uso = reader.GetString("uso"),
                            Tipo = reader.GetString("tipo"),
                            CantAmbientes = reader.GetInt32("cantAmbientes"),
                            Latitud = reader.GetDouble("latitud"),
                            Longitud = reader.GetDouble("longitud"),
                            Precio = reader.GetDouble("precio")
                        };

                        inmuebles.Add(inmueble);
                    }
                    connection.Close();
                }
            }
        }

        return inmuebles;
    }

    public Inmueble GetInmuebleById(int id)
    {
        Inmueble inmueble = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, idPropietario, direccion, uso, tipo, cantAmbientes, latitud, longitud, precio FROM inmuebles WHERE id=@id";
            PropietariosRepository propietariosRepo = new();
            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Propietario propietario = propietariosRepo.GetPropietarioById(reader.GetInt32("idPropietario"));

                        inmueble = new()
                        {
                            Id = reader.GetInt32("id"),
                            Propietario = propietario,
                            Direccion = reader.GetString("direccion"),
                            Uso = reader.GetString("uso"),
                            Tipo = reader.GetString("tipo"),
                            CantAmbientes = reader.GetInt32("cantAmbientes"),
                            Latitud = reader.GetDouble("latitud"),
                            Longitud = reader.GetDouble("longitud"),
                            Precio = reader.GetDouble("precio")
                        };


                    }
                    connection.Close();
                }
            }
        }
        return inmueble;
    }
    public List<String> GetEnumsTipes(string atributo)
    {

        {
            List<string> enumValues = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SHOW COLUMNS FROM inmuebles LIKE '{atributo}'";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string enumDefinition = reader["Type"].ToString();
                            enumDefinition = enumDefinition.Replace("enum(", "").Replace(")", "");
                            string[] enumOptions = enumDefinition.Split(',');

                            foreach (string option in enumOptions)
                            {
                                enumValues.Add(option.Trim('\''));
                            }
                        }
                    }
                }
            }

            return enumValues;
        }
    }
    public int CreateInmueble(Inmueble inmueble)
    {
        var res = -1;

        try
        {
            if (inmueble.Direccion != "" && inmueble.Uso != "" && inmueble.Tipo != "" && inmueble.CantAmbientes != 0 && inmueble.IdPropietario != 0
                && inmueble.Latitud != 0 && inmueble.Longitud != 0 && inmueble.Precio != 0 && inmueble.Uso != "")
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"INSERT INTO `inmuebles`(`idPropietario`, `direccion`, `uso`, `tipo`, `cantAmbientes`, `latitud`, `longitud`, `precio`)
                     VALUES (@IdPropietario, @Direccion, @Uso, @Tipo, @CantAmbientes, @Latitud, @Longitud, @Precio);
                     SELECT LAST_INSERT_ID()";

                    using (MySqlCommand command = new(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);
                        command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
                        command.Parameters.AddWithValue("@Uso", inmueble.Uso);
                        command.Parameters.AddWithValue("@Tipo", inmueble.Tipo);
                        command.Parameters.AddWithValue("@CantAmbientes", inmueble.CantAmbientes);
                        command.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
                        command.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
                        command.Parameters.AddWithValue("@Precio", inmueble.Precio);
                        res = Convert.ToInt32(command.ExecuteScalar());
                        connection.Close();

                    }

                }
            }
            else
            {
                res = -1;
            }
        }

        catch (System.Exception)
        {
            throw;
        }
        return res;

    }

    public int DeleteInmueble(int id)
    {
        var res = -1;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"DELETE FROM `inmuebles`
                            WHERE `id` = @id";

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

    public int UpdateInmueble(Inmueble inmueble)
    {
        var res = -1;
        Console.WriteLine($"inmueble.IdPropietario {inmueble.IdPropietario}");
        Console.WriteLine($"inmueble.Uso {inmueble.Uso}");
        Console.WriteLine($"inmueble.Tipo {inmueble.Tipo}");
        Console.WriteLine($"inmueble.CantAmbientes {inmueble.CantAmbientes}");
        Console.WriteLine($"inmueble.Latitud {inmueble.Latitud}");
        Console.WriteLine($"inmueble.Longitud {inmueble.Longitud}");
        Console.WriteLine($"inmueble.Precio {inmueble.Precio}");
        if (inmueble.Direccion != "" && inmueble.Uso != "" && inmueble.Tipo != "" && inmueble.CantAmbientes != 0 && inmueble.IdPropietario != 0
                        && inmueble.Latitud != 0 && inmueble.Longitud != 0 && inmueble.Precio != 0 && inmueble.Uso != "")
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE `inmuebles`SET `idPropietario` = @IdPropietario,`direccion` = @Direccion,`uso` = @Uso,`tipo` = @Tipo,
                `cantAmbientes` = @CantAmbientes, `latitud` = @Latitud,`longitud` = @Longitud,`precio` = @Precio WHERE `id` = @Id;";

                using (MySqlCommand command = new(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", inmueble.Id);
                    command.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);
                    command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue("@Uso", inmueble.Uso);
                    command.Parameters.AddWithValue("@Tipo", inmueble.Tipo);
                    command.Parameters.AddWithValue("@CantAmbientes", inmueble.CantAmbientes);
                    command.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
                    command.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
                    command.Parameters.AddWithValue("@Precio", inmueble.Precio);
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }

            }
        }

        return res;

    }




}
