using System.Data.SqlClient;

class BancoDeDadosHelper
{
    private static string connectionString = "your_connection_string_here";

    public static void InserirPedido(Pedido pedido)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            
        }
    }
}
