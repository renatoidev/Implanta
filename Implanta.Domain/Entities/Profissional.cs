namespace Implanta.Models.Entities;

public class Profissional
{
    public Profissional()
    {
    }

    public Profissional(string nomeCompleto, 
        string cpf, DateTime dataNascimento, char sexo, 
        bool ativo, int numeroRegistro, string cep, string cidade, 
        decimal valorRenda)
    {
        NomeCompleto = nomeCompleto;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        Ativo = ativo;
        NumeroRegistro = numeroRegistro;
        Cep = cep;
        Cidade = cidade;
        ValorRenda = valorRenda;
    }

    public Profissional(string nomeCompleto,
        string cpf, DateTime dataNascimento, char sexo,
        bool ativo, string cep, string cidade,
        decimal valorRenda)
    {
        NomeCompleto = nomeCompleto;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        Ativo = ativo;
        Cep = cep;
        Cidade = cidade;
        ValorRenda = valorRenda;
    }

   
    public int Id { get; private set; }
    public string NomeCompleto { get; private set; }
    public string Cpf { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public char Sexo { get; private set; }
    public bool Ativo { get; private set; }
    public int NumeroRegistro { get; private set; }
    public string Cep { get; private set; }
    public string Cidade { get; private set; }
    public decimal ValorRenda { get; private set; }
    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;

    public void Update(string nomeCompleto,
        string cpf, DateTime dataNascimento, char sexo,
        bool ativo, string cep, string cidade,
        decimal valorRenda)
    {
        NomeCompleto = nomeCompleto;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        Ativo = ativo;
        Cep = cep;
        Cidade = cidade;
        ValorRenda = valorRenda;
    }
}
