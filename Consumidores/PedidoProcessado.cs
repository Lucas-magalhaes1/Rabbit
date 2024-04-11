using System;

class PedidoProcessado
{
    public int Id { get; set; }
    public string Status { get; set; }
    public DateTime DataProcessamento { get; set; }

    public PedidoProcessado(int id, string status, DateTime dataProcessamento)
    {
        Id = id;
        Status = status;
        DataProcessamento = dataProcessamento;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Status: {Status}, Data de Processamento: {DataProcessamento}";
    }
}
