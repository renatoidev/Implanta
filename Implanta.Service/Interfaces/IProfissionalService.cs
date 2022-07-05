﻿using Implanta.Models.Entities;
using Implanta.Service.ViewModels;
using Implanta.ViewModels;

namespace Implanta.Service.Interfaces;

public interface IProfissionalService 
{
    bool ValidaProfissional(dynamic model);
    Task<List<GetProfissionalViewModel>> GetProfissionais();
    Task<List<GetProfissionalViewModel>> GetProfissionais(int de, int ate);
    Task<List<GetProfissionalViewModel>> GetProfissionaisAtivos(bool ativo);
    Task<Profissional> AddProfissional(AddProfissionalViewModel addProfissionalViewModel);
    Task<Profissional> EditProfissional(int id, EditProfissionalViewModel editProfissionalViewModel);
    Task<bool> DeleteProfissional(int id);
}
