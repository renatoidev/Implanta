using Implanta.Models.Entities;

namespace Implanta.Domain.Contracts;

public interface IProfissionalRepository
{
    bool ExisteProfissional(string nomeCompleto);
    int NumeroRegistro();
    bool ExisteNumeroRegistro(int numeroRegistro);
    bool MaiorDeIdade(DateTime dataNascimento);
    Task<List<Profissional>> GetProfissionais();
    Task<Profissional> GetProfissionalById(int id);
    Task<List<Profissional>> GetProfissionais(int de, int ate);
    Task<List<Profissional>> GetProfissionaisAtivos(bool ativo);
    Task<bool> AddProfissional(Profissional profissional);
    Task<bool> EditProfissional(Profissional profissional);
    Task<bool> DeleteProfissional(Profissional profissional);
    Task<int> SaveChangesAsync();
}
