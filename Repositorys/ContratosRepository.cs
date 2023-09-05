using System;
using System.Collections.Generic;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; // Asegúrate de tener la referencia correcta a MySql.Data
using System.Text.RegularExpressions;


namespace Inmobiliaria.Models;
public class ContratosRepository
{

    protected readonly string connectionString;
    public ContratosRepository()
    {
        connectionString = Conexion.GetConnection;
    }

    public List<Contrato> GetAllContratos(bool vig)
    {
        Console.WriteLine("vigentes: " + vig);
        List<Contrato> contratos = new();
        string query;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            if (vig)
            {
                query = "SELECT `id`, `idInquilino`, `idInmueble`,`montoMensual`, `fechaInicio`, `fechaFin`, `estado` FROM `contratos` WHERE `estado` = 1";
            }
            else
            {
                query = "SELECT `id`, `idInquilino`, `idInmueble`,`montoMensual`, `fechaInicio`, `fechaFin`, `estado` FROM `contratos` ";
            }

            InquilinosRepository inquilinosRepo = new();
            InmueblesRepository inmueblesRepo = new();
            using (MySqlCommand command = new(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inquilino inquilino = inquilinosRepo.GetInquilinoById(reader.GetInt32("idInquilino"));
                        Inmueble inmueble = inmueblesRepo.GetInmuebleById(reader.GetInt32("idInmueble"));

                        Contrato contrato = new()
                        {
                            Id = reader.GetInt32("id"),
                            IdInquilino = inquilino.Id,
                            Inquilino = inquilino,
                            IdInmueble = inmueble.Id,
                            Inmueble = inmueble,
                            MontoMensual = reader.GetDouble("montoMensual"),
                            FechaInicio = reader.GetDateTime("fechaInicio"),
                            FechaFin = reader.GetDateTime("fechaFin"),
                            Estado = reader.GetBoolean("estado"),

                        };

                        contratos.Add(contrato);
                    }
                    connection.Close();
                }
            }
        }

        return contratos;
    }
    public List<Contrato> GetAllContratosFecha(DateTime desde, DateTime hasta)
    {

        List<Contrato> contratos = new();
        string query = @"SELECT `id`, `idInquilino`, `idInmueble`, `montoMensual`, `fechaInicio`, `fechaFin`, `estado` 
        FROM `contratos` 
        WHERE `fechaInicio` <=@Hasta  AND `fechaFin` >= @Desde and estado = 1";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();


            InquilinosRepository inquilinosRepo = new();
            InmueblesRepository inmueblesRepo = new();
            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@Desde", desde);
                command.Parameters.AddWithValue("@Hasta", hasta);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inquilino inquilino = inquilinosRepo.GetInquilinoById(reader.GetInt32("idInquilino"));
                        Inmueble inmueble = inmueblesRepo.GetInmuebleById(reader.GetInt32("idInmueble"));

                        Contrato contrato = new()
                        {
                            Id = reader.GetInt32("id"),
                            IdInquilino = inquilino.Id,
                            Inquilino = inquilino,
                            IdInmueble = inmueble.Id,
                            Inmueble = inmueble,
                            MontoMensual = reader.GetDouble("montoMensual"),
                            FechaInicio = reader.GetDateTime("fechaInicio"),
                            FechaFin = reader.GetDateTime("fechaFin"),
                            Estado = reader.GetBoolean("estado"),

                        };

                        contratos.Add(contrato);
                    }
                    connection.Close();
                }
            }
        }

        return contratos;
    }
    public List<Contrato> GetAllContratosInmueble(int idInmueble)
    {

        List<Contrato> contratos = new();
        string query = @"SELECT `id`, `idInquilino`, `idInmueble`, `montoMensual`, `fechaInicio`, `fechaFin`, `estado` 
        FROM `contratos` 
        WHERE `idInmueble` =@idInmueble  and estado = 1";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();


            InquilinosRepository inquilinosRepo = new();
            InmueblesRepository inmueblesRepo = new();
            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@idInmueble", idInmueble);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inquilino inquilino = inquilinosRepo.GetInquilinoById(reader.GetInt32("idInquilino"));
                        Inmueble inmueble = inmueblesRepo.GetInmuebleById(reader.GetInt32("idInmueble"));

                        Contrato contrato = new()
                        {
                            Id = reader.GetInt32("id"),
                            IdInquilino = inquilino.Id,
                            Inquilino = inquilino,
                            IdInmueble = inmueble.Id,
                            Inmueble = inmueble,
                            MontoMensual = reader.GetDouble("montoMensual"),
                            FechaInicio = reader.GetDateTime("fechaInicio"),
                            FechaFin = reader.GetDateTime("fechaFin"),
                            Estado = reader.GetBoolean("estado"),

                        };

                        contratos.Add(contrato);
                    }
                    connection.Close();
                }
            }
        }

        return contratos;
    }

    public Contrato GetContratoById(int id)
    {
        Contrato contrato = new();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"SELECT `id`, `idInquilino`, `idInmueble`, `fechaInicio`, `fechaFin`,`montoMensual`, `estado` FROM `contratos` WHERE id=@id";
            InquilinosRepository inquilinosRepo = new();
            InmueblesRepository inmueblesRepo = new();

            using (MySqlCommand command = new(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inquilino inquilino = inquilinosRepo.GetInquilinoById(reader.GetInt32("idInquilino"));
                        Inmueble inmueble = inmueblesRepo.GetInmuebleById(reader.GetInt32("idInmueble"));

                        contrato = new()
                        {
                            Id = reader.GetInt32("id"),
                            IdInquilino = inquilino.Id,
                            Inquilino = inquilino,
                            IdInmueble = inmueble.Id,
                            Inmueble = inmueble,
                            MontoMensual = reader.GetDouble("montoMensual"),
                            FechaInicio = reader.GetDateTime("fechaInicio"),
                            FechaFin = reader.GetDateTime("fechaFin"),
                            Estado = reader.GetBoolean("estado"),

                        };


                    }
                    connection.Close();
                }
            }
        }
        return contrato;
    }

    public int CreateContrato(Contrato contrato)
    {
        var res = -1;


        bool disponibilidad = VerificarDisponibilidad(contrato);
        Console.WriteLine("ENTRO CONTRATOS");
        try
        {

            if (contrato.IdInquilino != 0 && contrato.IdInmueble != 0 && contrato.FechaFin != DateTime.MinValue &&
            contrato.FechaInicio != DateTime.MinValue)
            {
                if (!disponibilidad)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        string query = @"INSERT INTO `contratos`(`idInquilino`, `idInmueble`,`montoMensual`, `fechaInicio`, `fechaFin`, `estado`)
                     VALUES (@idInquilino, @idInmueble,@montoMensual, @fechaInicio, @fechaFin, @estado);
                     SELECT LAST_INSERT_ID()";

                        using (MySqlCommand command = new(query, connection))
                        {
                            connection.Open();
                            command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                            command.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                            command.Parameters.AddWithValue("@montoMensual", contrato.MontoMensual);
                            command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                            command.Parameters.AddWithValue("@fechaFin", contrato.FechaFin);
                            command.Parameters.AddWithValue("@estado", true);
                            res = Convert.ToInt32(command.ExecuteScalar());
                            connection.Close();

                        }

                    }
                }
                else
                {
                    res = -3;
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

    public int DeleteContrato(int id)
    {
        var res = -1;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"UPDATE `contratos` SET `estado` = 0 WHERE `id` = @Id;";


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

    public int UpdateContrato(Contrato contrato)
    {
        var res = -1;

        bool disponibilidad = VerificarDisponibilidad(contrato);


        if (contrato.IdInquilino != 0 && contrato.IdInmueble != 0 && contrato.MontoMensual != 0 && contrato.FechaFin != DateTime.MinValue &&
            contrato.FechaInicio != DateTime.MinValue)
        {
            if (!disponibilidad)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"UPDATE `contratos` SET `fechaInicio` = @fechaInicio,`fechaFin` = @fechaFin,`montoMensual`= @montoMensual,
                   `idInquilino` = @idInquilino , `idInmueble` = @idInmueble WHERE `id`= @Id";

                    using (MySqlCommand command = new(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id", contrato.Id);
                        command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                        command.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                        command.Parameters.AddWithValue("@montoMensual", contrato.MontoMensual);
                        command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                        command.Parameters.AddWithValue("@fechaFin", contrato.FechaFin);
                        res = command.ExecuteNonQuery();
                        connection.Close();
                    }

                }
            }
            else
            {
                res = -3;
            }
        }

        return res;

    }


    public bool VerificarDisponibilidad(Contrato contrato)
    {
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string checkQuery = @"SELECT COUNT(*) FROM `contratos`
                              WHERE `idInmueble` = @idInmueble
                              AND `id` != @id
                              AND (@fechaInicio BETWEEN `fechaInicio` AND `fechaFin`
                                   OR @fechaFin BETWEEN `fechaInicio` AND `fechaFin`) AND `estado` = 1";

                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@id", contrato.Id);
                    checkCommand.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                    checkCommand.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                    checkCommand.Parameters.AddWithValue("@fechaFin", contrato.FechaFin);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    Console.WriteLine(count);
                    return count > 0;
                }
            }
        }
    }

}
