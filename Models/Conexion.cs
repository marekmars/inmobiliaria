using System;

public class Conexion
{
    private static readonly string Server = "localhost";
    private static readonly string Database = "inmobiliaria";
    private static readonly string User = "root";
    private static readonly string Password = "";
    private static readonly string ConnectionString = $"Server={Server};Database={Database};User={User};Password={Password};SslMode=none";

    public static string GetConnection
    {
        get
        {
            return ConnectionString;
        }
    }

}