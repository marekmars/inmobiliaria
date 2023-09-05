using System;
using System.Collections.Generic;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; // Asegúrate de tener la referencia correcta a MySql.Data
using System.Text.RegularExpressions;


namespace Inmobiliaria.Models;
public class PropietariosRepository
{

    protected readonly string connectionString;
    public PropietariosRepository()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }
    public List<Propietario> GetAllPropietarios()
    {
        List<Propietario> Propietarios = new List<Propietario>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, dni, apellido, nombre, telefono, correo FROM propietarios WHERE estado=1 ORDER BY apellido ";

            using (MySqlCommand command = new(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Propietario Propietario = new()
                        {
                            Id = reader.GetInt32("id"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                        };

                        Propietarios.Add(Propietario);
                    }
                    connection.Close();
                }
            }
        }
        Console.WriteLine("PropietarioS" + Propietarios);
        return Propietarios;
    }

    public Propietario GetPropietarioById(int id)
    {
        Propietario propietario = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, dni, apellido, nombre, telefono, correo FROM propietarios WHERE id=@id  ORDER BY apellido";

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propietario = new()
                        {
                            Id = reader.GetInt32("id"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                        };

                    }
                    connection.Close();
                }
            }
        }
        return propietario;
    }
    public Propietario GetPropietarioByDni(string dni)
    {
        Propietario propietario = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, dni, apellido, nombre, telefono, correo FROM propietarios WHERE  dni=@dni ORDER BY apellido";

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@dni", dni);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propietario = new()
                        {
                            Id = reader.GetInt32("id"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                        };

                    }
                    connection.Close();
                }
            }
        }
        return propietario;
    }
    public int CreatePropietario(Propietario propietario)
    {
        var res = -1;



        // Verificar si el correo electrónico es válido
        if (!EsCorreoElectronicoValido(propietario.Correo))
        {
            res = -3;
            return res;
        }

        // Verificar si el DNI es válido
        if (!EsDniValido(propietario.Dni))
        {
            res = -4;
            return res;
        }

        Propietario propietarioAux = GetPropietarioByDni(propietario.Dni);

        try
        {
            if (propietarioAux.Nombre == "")
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"INSERT INTO `propietarios`( `dni`, `apellido`, `nombre`, `telefono`, `correo`,estado) 
            VALUES (@Dni, @Apellido, @Nombre, @Telefono, @Correo ,@Estado);
            SELECT LAST_INSERT_ID()";

                    using (MySqlCommand command = new(query, connection))
                    {
                        command.Parameters.AddWithValue("@Dni", propietario.Dni);
                        command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                        command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                        command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                        command.Parameters.AddWithValue("@Correo", propietario.Correo);
                        command.Parameters.AddWithValue("@Estado", true);
                        connection.Open();
                        res = Convert.ToInt32(command.ExecuteScalar());
                        propietario.Id = res;
                        connection.Close();

                    }

                }
            }
            else
            {
                res = -2;
            }
        }

        catch (System.Exception)
        {
            throw;
        }
        return res;

    }

    public int DeletePropietario(int id)
    {
        var res = -1;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"UPDATE propietarios p
                             INNER JOIN inmuebles i ON p.id = i.idPropietario
                             SET p.estado = 0, i.estado = 0
                             WHERE p.id = @Id;";


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

    public int UpdatePropietario(Propietario propietario)
    {
        var res = -1;



        // Verificar si el correo electrónico es válido
        if (!EsCorreoElectronicoValido(propietario.Correo))
        {
            res = -3;
            return res;
        }

        // Verificar si el DNI es válido
        if (!EsDniValido(propietario.Dni))
        {
            res = -4;
            return res;
        }

        Propietario propietarioAux = GetPropietarioByDni(propietario.Dni);

        if (propietarioAux.Nombre == "" || propietarioAux.Id == propietario.Id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE `propietarios` SET 
                        `dni` = @Dni,
                        `apellido` = @Apellido,
                        `nombre` = @Nombre,
                        `telefono` = @Telefono,
                        `correo` = @Correo
                        WHERE `id` = @Id";

                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", propietario.Id);
                    command.Parameters.AddWithValue("@Dni", propietario.Dni);
                    command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                    command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                    command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                    command.Parameters.AddWithValue("@Correo", propietario.Correo);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }

            }
        }
        else
        {
            return -2;
        }

        return res;

    }
    public List<Inmueble> GetAllInmueblesPropietario(int id)
    {
        List<Inmueble> inmuebles = new();
        var propietariosRepo = new PropietariosRepository();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT id, idPropietario, direccion, uso, tipo, cantAmbientes, latitud, longitud, precio,estado FROM inmuebles WHERE idPropietario=@id AND estado=1";

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Propietario propietario = propietariosRepo.GetPropietarioById(id);

                        Inmueble inmueble = new()
                        {
                            Id = reader.GetInt32("id"),
                            Propietario = propietario,
                            IdPropietario = reader.GetInt32("idPropietario"),
                            Direccion = reader.GetString("direccion"),
                            // Uso = reader.GetString("uso"),
                            // Tipo = reader.GetString("tipo"),
                            CantAmbientes = reader.GetInt32("cantAmbientes"),
                            Latitud = reader.GetDouble("latitud"),
                            Longitud = reader.GetDouble("longitud"),
                            Precio = reader.GetDouble("precio"),
                            Estado = reader.GetBoolean("estado")
                        };

                        inmuebles.Add(inmueble);
                    }
                    connection.Close();
                }
            }
        }
        return inmuebles;
    }

    private bool EsCorreoElectronicoValido(string correo)
    {
        // Patrón de expresión regular para validar una dirección de correo electrónico
        string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(correo, patron);

    }

    private bool EsDniValido(string dni)
    {
        // Patrón de expresión regular para validar un DNI argentino (formato: XX.XXX.XXX)
        string patron = @"^\d{2}\.\d{3}\.\d{3}$";
        return Regex.IsMatch(dni, patron);
    }
}
