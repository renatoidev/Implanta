using Implanta.ViewModels;

namespace Implanta.Service.ViewModels;

public class ProfissionalResponseViewModel
{
    public string NomeCompleto { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public char Sexo { get; set; }
    public bool Ativo { get; set; }
    public string Cep { get; set; }
    public string Cidade { get; set; }
    public decimal ValorRenda { get; set; }
    public List<string> Errors { get; set; }

    public ProfissionalResponseViewModel()
    {
        Errors = new List<string>();
    }
}
