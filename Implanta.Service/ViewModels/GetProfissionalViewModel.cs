namespace Implanta.Service.ViewModels;

public class GetProfissionalViewModel
{
    public int Id { get; set; }
    public int NumeroRegistro { get; set; }
    public string NomeCompleto { get; set; }
    public string Cpf { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
}
