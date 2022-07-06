using Flunt.Notifications;
using Implanta.Domain.Contracts;
using Implanta.Models.Entities;
using Implanta.Service.Interfaces;
using Implanta.Service.ViewModels;
using Implanta.ViewModels;

namespace Implanta.Service.Services;


public class ProfissionalServices : Notifiable <Notification>, IProfissionalService
{
    private readonly IProfissionalRepository _profissionalRepository;

    public ProfissionalServices(IProfissionalRepository profissionalRepository)
    {
        _profissionalRepository = profissionalRepository;
    }


    public static bool ValidaCpf(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf;
        string digito;
        int soma;
        int resto;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        
        if (cpf.Length != 11)
            return false;
        
        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++) 
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        
        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
        {
            resto = 11 - resto;
        }

        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
        {
            resto = 11 - resto;
        }
        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }

    public async Task<List<GetProfissionalViewModel>> GetProfissionais()
    {
        var profissionaisReporistory = await _profissionalRepository.GetProfissionais();
        var profissionais = new List<GetProfissionalViewModel>();

        foreach (var result in profissionaisReporistory)
        {
            var prof = new GetProfissionalViewModel()
            {
                Id = result.Id,
                NumeroRegistro = result.NumeroRegistro,
                NomeCompleto = result.NomeCompleto,
                Cpf = result.Cpf,
                Ativo = result.Ativo,
                DataCriacao = result.DataCriacao
            };
            profissionais.Add(prof);
        }
        return profissionais;
    }

    public async Task<List<GetProfissionalViewModel>> GetProfissionais(int de, int ate)
    {
        var profissionaisReporistory = await _profissionalRepository.GetProfissionais(de, ate);
        var profissionais = new List<GetProfissionalViewModel>();

        foreach (var result in profissionaisReporistory)
        {
            var prof = new GetProfissionalViewModel()
            {
                Id = result.Id,
                NumeroRegistro = result.NumeroRegistro,
                NomeCompleto = result.NomeCompleto,
                Cpf = result.Cpf,
                Ativo = result.Ativo,
                DataCriacao = result.DataCriacao
            };
            profissionais.Add(prof);
        }
        return profissionais;
    }

    public async Task<List<GetProfissionalViewModel>> GetProfissionaisAtivos(bool ativo)
    {
        var profissionaisReporistory = await _profissionalRepository.GetProfissionaisAtivos(ativo);
        var profissionais = new List<GetProfissionalViewModel>();

        foreach (var result in profissionaisReporistory)
        {
            var prof = new GetProfissionalViewModel()
            {
                Id = result.Id,
                NumeroRegistro = result.NumeroRegistro,
                NomeCompleto = result.NomeCompleto,
                Cpf = result.Cpf,
                Ativo = result.Ativo,
                DataCriacao = result.DataCriacao
            };
            profissionais.Add(prof);
        }
        return profissionais;
    }

    public async Task<ProfissionalResponseViewModel> AddProfissional(AddProfissionalViewModel model)
    {
        var numeroRegistro = _profissionalRepository.NumeroRegistro();

        var profissional = new Profissional(model.NomeCompleto, model.Cpf, model.DataNascimento,
            model.Sexo, model.Ativo, numeroRegistro, model.Cep, model.Cidade, model.ValorRenda);
        if (_profissionalRepository.ExisteProfissional(model.NomeCompleto))
        {
            AddNotification("NomeCompleto", "Profissional já cadastrado");
        }

        if (!ValidaProfissional(model))
            AddNotification("False", "Não foi possível cadastrar profissional");

        if (_profissionalRepository.ExisteNumeroRegistro(profissional.NumeroRegistro))
            AddNotification("NumeroRegistro","Número de Registro Inválido");

        var response = new ProfissionalResponseViewModel();
        
        if (!IsValid)
        {
            foreach (var not in Notifications)
                response.Errors.Add(not.Message);

            return response;
        }
        
        await _profissionalRepository.AddProfissional(profissional);
        await _profissionalRepository.SaveChangesAsync();
        
        response.NomeCompleto = model.NomeCompleto;
        response.Cep = model.Cep;
        response.DataNascimento = model.DataNascimento;
        response.Sexo = model.Sexo;
        response.Ativo = model.Ativo;
        response.Cep = model.Cep;
        response.Cidade = model.Cidade;
        response.ValorRenda = model.ValorRenda;
        
        return response;
    }

    public async Task<ProfissionalResponseViewModel> EditProfissional(int id, EditProfissionalViewModel model)
    {
        var response = new ProfissionalResponseViewModel();
        try
        {
            var profissional = await _profissionalRepository.GetProfissionalById(id);

            if (profissional is null)
            {
                AddNotification("Id", "Id não encontrado");
            }
            else
            {
                profissional.Update(model.NomeCompleto, model.Cpf, model.DataNascimento, model.Sexo,
                    model.Ativo, model.Cep, model.Cidade, model.ValorRenda);
            }

            if (!ValidaProfissional(profissional))
            {
                AddNotification("False", "Não foi possível editar profissional");
            }


            if (!IsValid)
            {
                foreach (var not in Notifications)
                    response.Errors.Add(not.Message);

                return response;
            }

            await _profissionalRepository.EditProfissional(profissional);
            await _profissionalRepository.SaveChangesAsync();
            
            response.NomeCompleto = model.NomeCompleto;
            response.Cep = model.Cep;
            response.DataNascimento = model.DataNascimento;
            response.Sexo = model.Sexo;
            response.Ativo = model.Ativo;
            response.Cep = model.Cep;
            response.Cidade = model.Cidade;
            response.ValorRenda = model.ValorRenda;

            return response;
        }
        catch (Exception)
        {
            AddNotification("False", "Erro interno no servidor");
        }
        
        return response;
    }

    public bool ValidaProfissional(dynamic model)
    {
        if (!ValidaCpf(model.Cpf))
        {
            AddNotification("Cpf", "CPF inválido");
            return false;
        }

        if (!_profissionalRepository.MaiorDeIdade(model.DataNascimento))
        {
            AddNotification("DataNascimento", "Menor de Idade");
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteProfissional(int id)
    {
        try
        {                
            var profissional = await _profissionalRepository.GetProfissionalById(id);
            await _profissionalRepository.DeleteProfissional(profissional);
            await _profissionalRepository.SaveChangesAsync();
        }
        catch (Exception)
        {
            AddNotification("Id","Id não localizado");
        }
        return true;
    }
}
