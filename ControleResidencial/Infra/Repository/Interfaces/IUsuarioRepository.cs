using ControleResidencial.DTOs.Usuario.Request;
using ControleResidencial.DTOs.Usuario.Response;
using ControleResidencial.Infra.Repository.Entity;

namespace ControleResidencial.Infra.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetById(string id);
        void UpdateAsync(Usuario usuario);
        void CreateAsync(Usuario usuario);
        void DeleteAsync(Usuario usuario);
        Task<UsuarioFilterResponseDTO> ListUsuarioAsync(UsuarioFilterRequestDTO dto);
        Task<UsuarioTotaisResponseDTO> GetTotaisPorUsuarioAsync();
    }
}
