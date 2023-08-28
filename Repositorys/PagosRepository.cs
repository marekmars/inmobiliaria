using System;
using System.Collections.Generic;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; // Asegúrate de tener la referencia correcta a MySql.Data
using System.Text.RegularExpressions;


namespace Inmobiliaria.Models;
public class PagosRepository
{

    protected readonly string connectionString;
    public PagosRepository()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    //     public List<Contrato> GetAllContratos()
    //     {
    //         List<Contrato> contratos = new();
    //         using (MySqlConnection connection = new MySqlConnection(connectionString))
    //         {
    //             connection.Open();
    //             string query = "SELECT `id`, `idInquilino`, `idInmueble`,`montoMensual`, `fechaInicio`, `fechaFin`, `estado` FROM `contratos`";
    //             InquilinosRepository inquilinosRepo = new();
    //             InmueblesRepository inmueblesRepo = new();
    //             using (MySqlCommand command = new(query, connection))
    //             {
    //                 using (MySqlDataReader reader = command.ExecuteReader())
    //                 {
    //                     while (reader.Read())
    //                     {
    //                         Inquilino inquilino = inquilinosRepo.GetInquilinoById(reader.GetInt32("idInquilino"));
    //                         Inmueble inmueble = inmueblesRepo.GetInmuebleById(reader.GetInt32("idInmueble"));

    //                         Contrato contrato = new()
    //                         {
    //                             Id = reader.GetInt32("id"),
    //                             IdInquilino = inquilino.Id,
    //                             Inquilino = inquilino,
    //                             IdInmueble = inmueble.Id,
    //                             Inmueble = inmueble,
    //                             MontoMensual = reader.GetDouble("montoMensual"),
    //                             FechaInicio = reader.GetDateTime("fechaInicio"),
    //                             FechaFin = reader.GetDateTime("fechaFin"),
    //                             Estado = reader.GetBoolean("estado"),

    //                         };

    //                         contratos.Add(contrato);
    //                     }
    //                     connection.Close();
    //                 }
    //             }
    //         }

    //         return contratos;
    //     }
    public Pago GetPagoById(int id)
    {
        Pago pago = new();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"SELECT `id`, `idContrato`, `fechaPago`, `importe` FROM `pagos` WHERE `id` = @id";
            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pago = new()
                        {
                            Id = reader.GetInt32("id"),
                            IdContrato = reader.GetInt32("idContrato"),
                            FechaPago = reader.GetDateTime("fechaPago"),
                            Importe = reader.GetDouble("importe"),
                        };
                    }
                    connection.Close();
                }
            }
        }
        return pago;
    }
    public List<Pago> GetPagoByContratoId(int idContrato)
    {
        Console.WriteLine("Contrato: " + idContrato);
        Pago pago;
        List<Pago> pagos = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"SELECT `id`, `idContrato`, `fechaPago`, `importe` FROM `pagos` WHERE `idContrato` = @idContrato";
            ContratosRepository repo = new();

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@idContrato", idContrato);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contrato contrato = repo.GetContratoById(idContrato);

                        pago = new()
                        {
                            Id = reader.GetInt32("id"),
                            IdContrato = reader.GetInt32("idContrato"),
                            FechaPago = reader.GetDateTime("fechaPago"),
                            Importe = reader.GetDouble("importe"),
                            Contrato = contrato,

                        };
                        pagos.Add(pago);


                    }
                    connection.Close();
                }
            }
        }
        return pagos;
    }
    //     // public List<String> GetEnumsTipes(string atributo)
    //     // {

    //     //     {
    //     //         List<string> enumValues = new List<string>();

    //     //         using (MySqlConnection connection = new MySqlConnection(connectionString))
    //     //         {
    //     //             connection.Open();

    //     //             string query = $"SHOW COLUMNS FROM inmuebles LIKE '{atributo}'";

    //     //             using (MySqlCommand command = new MySqlCommand(query, connection))
    //     //             {
    //     //                 using (MySqlDataReader reader = command.ExecuteReader())
    //     //                 {
    //     //                     if (reader.Read())
    //     //                     {
    //     //                         string enumDefinition = reader["Type"].ToString();
    //     //                         enumDefinition = enumDefinition.Replace("enum(", "").Replace(")", "");
    //     //                         string[] enumOptions = enumDefinition.Split(',');

    //     //                         foreach (string option in enumOptions)
    //     //                         {
    //     //                             enumValues.Add(option.Trim('\''));
    //     //                         }
    //     //                     }
    //     //                 }
    //     //             }
    //     //         }

    //     //         return enumValues;
    //     //     }
    //     // }

    public int CreatePago(Pago pago)
    {
        var res = -1;





        try
        {
            if (pago.FechaPago != DateTime.MinValue && pago.Importe != 0 && pago.IdContrato != 0)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString)) ;
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        string query = @"INSERT INTO `pagos`(`id`, `idContrato`, `fechaPago`, `importe`) values (NULL, @idContrato, @fechaPago, @importe);
                         SELECT LAST_INSERT_ID()";

                        using (MySqlCommand command = new(query, connection))
                        {
                            connection.Open();
                            command.Parameters.AddWithValue("@idContrato", pago.IdContrato);
                            command.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
                            command.Parameters.AddWithValue("@importe", pago.Importe);
                            res = Convert.ToInt32(command.ExecuteScalar());
                            connection.Close();

                        }

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


    public int DeletePago(int id)
    {
        var res = -1;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"DELETE FROM `pagos`
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

    public int UpdatePago(Pago pago)
    {
        var res = -1;



        if (pago.FechaPago != DateTime.MinValue && pago.Importe != 0 && pago.IdContrato != 0)
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"UPDATE `pagos`
                                SET `idContrato` = @idContrato,
                                `fechaPago` = @fechaPago,
                                `importe` = @importe
                                WHERE `id` = @id";

                using (MySqlCommand command = new(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", pago.Id);
                    command.Parameters.AddWithValue("@idContrato", pago.IdContrato);
                    command.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
                    command.Parameters.AddWithValue("@importe", pago.Importe);            
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }

            }



        }
        else
        {
            res = -1;
        }

        return res;

    }


    //     public bool VerificarDisponibilidad(Contrato contrato)
    //     {
    //         {
    //             using (MySqlConnection connection = new MySqlConnection(connectionString))
    //             {
    //                 connection.Open();

    //                 string checkQuery = @"SELECT COUNT(*) FROM `contratos`
    //                               WHERE `idInmueble` = @idInmueble
    //                               AND `id` != @id
    //                               AND (@fechaInicio BETWEEN `fechaInicio` AND `fechaFin`
    //                                    OR @fechaFin BETWEEN `fechaInicio` AND `fechaFin`) AND `estado` = 1";

    //                 using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
    //                 {
    //                     checkCommand.Parameters.AddWithValue("@id", contrato.Id);
    //                     checkCommand.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
    //                     checkCommand.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
    //                     checkCommand.Parameters.AddWithValue("@fechaFin", contrato.FechaFin);

    //                     int count = Convert.ToInt32(checkCommand.ExecuteScalar());
    //                     Console.WriteLine(count);
    //                     return count > 0;
    //                 }
    //             }
    //         }
    //     }

}
