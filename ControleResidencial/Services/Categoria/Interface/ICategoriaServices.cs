using ControleResidencial.DTOs.Categoria.Request;
using ControleResidencial.DTOs.Categoria.Response;

namespace ControleResidencial.Services.Categoria.Interface
{
    public interface ICategoriaServices
    {
        Task<CategoriaResponseDTO> CreateAsync(CreateCategoriaRequestDTO dto);
        Task<CategoriaFilterResponseDTO> GetCategoriaFilterAsync(CategoriaFilterRequestDTO dto);
    }
}
