using Implanta.Data.Context;
using Implanta.Domain.Contracts;
using Implanta.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Implanta.Infra.Repository;

public class ProfissionalRepository : IProfissionalRepository
{
    private readonly ImplantaDataContext _context;
    protected readonly DbSet<Profissional> DbSet;

    public ProfissionalRepository(ImplantaDataContext context)
    {
        DbSet = context.Set<Profissional>();
        _context = context;
    }

    public async Task<List<Profissional>> GetProfissionais()
    {
        return await _context
            .Profissionais
            .AsNoTracking()
            .OrderBy(x => x.NomeCompleto)
            .ToListAsync();
    }

    public async Task<Profissional> GetProfissionalById(int id)
    {
        var result = await _context
            .Profissionais
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if(result == null)
            return null;

        return result;
    }

    public async Task<List<Profissional>> GetProfissionais(int de, int ate)
    {
        return await _context
            .Profissionais
            .AsNoTracking()
            .OrderBy(x => x.NomeCompleto)
            .Where(x => x.NumeroRegistro >= de && x.NumeroRegistro <= ate)
            .ToListAsync();
    }

    public async Task<List<Profissional>> GetProfissionaisAtivos(bool ativo)
    {
        return await _context
            .Profissionais
            .AsNoTracking()
            .OrderBy(x => x.NomeCompleto)
            .Where(x => x.Ativo == true)
            .ToListAsync();
    }

    public async Task<bool> AddProfissional(Profissional profissional)
    {
        await DbSet.AddAsync(profissional);
        return true;
    }

    public async Task<bool> EditProfissional(Profissional profissional)
    {
        _context.Profissionais.Update(profissional);
        return true;

    }

    public async Task<bool> DeleteProfissional(Profissional profissional)
    {
        _context.Profissionais.Remove(profissional);
        return true;
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public bool ExisteProfissional(string nomeCompleto)
    {
        if (_context.Profissionais.Any(x => x.NomeCompleto == nomeCompleto))
            return true;
        return false;
    }

    public int NumeroRegistro() => DbSet.Max(x => x.NumeroRegistro) + 1;

    public bool ExisteNumeroRegistro(int numeroRegistro)
    {
        if (_context.Profissionais.Any(x => x.NumeroRegistro == numeroRegistro))
            return true;
        return false;
    }

    public bool MaiorDeIdade(DateTime dataNascimento)
    {
        int AnoBase = DateTime.Today.Year - 18;

        if (dataNascimento.Year < AnoBase)
            return true;

        if (AnoBase == dataNascimento.Year)
        {
            if (dataNascimento.Month < DateTime.Now.Month)
                return true;

            if (dataNascimento.Month == DateTime.Now.Month)
            {
                if (dataNascimento.Day <= DateTime.Now.Day)
                    return true;
            }
        }
        return false;
    }
}
