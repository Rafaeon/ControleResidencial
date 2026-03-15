using ControleResidencial.DTOs.Categoria.Request;
using ControleResidencial.DTOs.Categoria.Response;
using ControleResidencial.DTOs.Transacao.Request;
using ControleResidencial.DTOs.Transacao.Response;
using ControleResidencial.Infra.Repository.Entity;

namespace ControleResidencial.Infra.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        void CreateAsync(Categoria categoria);
        Task<Categoria?> GetById(string id);
        Task<CategoriaFilterResponseDTO> ListCategoriaAsync(CategoriaFilterRequestDTO dto);

    }
}
