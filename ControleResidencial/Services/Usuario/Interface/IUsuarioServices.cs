using ControleResidencial.DTOs.Usuario.Request;
using ControleResidencial.DTOs.Usuario.Response;

namespace ControleResidencial.Services.Usuario.Interface
{
    public interface IUsuarioServices
    {
        Task<UsuarioResponseDTO> GetByUsuarioId(string usuarioId);
        Task<UsuarioResponseDTO> CreateAsync(CreateUsuarioRequestDTO dto);
        Task<UsuarioResponseDTO> UpdateAsync(UpdateUsuarioRequestDTO dto);
        Task<UsuarioResponseDTO> DeleteAsync(DeleteUsuarioRequestDTO dto);
        Task<UsuarioFilterResponseDTO> GetUsuarioFilterAsync(UsuarioFilterRequestDTO dto);
    }
}
