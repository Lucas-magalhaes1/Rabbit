using System.Collections.Generic;

class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; }
    public List<string> Produtos { get; set; }
    public string EnderecoEntrega { get; set; }

    public Pedido(int id, string cliente, List<string> produtos, string enderecoEntrega)
    {
        Id = id;
        Cliente = cliente;
        Produtos = produtos;
        EnderecoEntrega = enderecoEntrega;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Cliente: {Cliente}, Produtos: {string.Join(", ", Produtos)}, Endereço de Entrega: {EnderecoEntrega}";
    }
}
