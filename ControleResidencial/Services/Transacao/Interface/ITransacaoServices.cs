using ControleResidencial.DTOs.Transacao.Request;
using ControleResidencial.DTOs.Transacao.Response;
using ControleResidencial.DTOs.Usuario.Request;
using ControleResidencial.DTOs.Usuario.Response;

namespace ControleResidencial.Services.Transacao.Interface
{
    public interface ITransacaoServices
    {
        Task<TransacaoResponseDTO> GetByTransacaoId(string transacaoId);
        Task<TransacaoResponseDTO> GetByTransacaoPorUsuarioId(string usuarioId);
        Task<TransacaoResponseDTO> CreateAsync(CreateTransacaoRequestDTO dto);
        Task<TransacaoResponseDTO> UpdateAsync(UpdateTransacaoRequestDTO dto);
        Task<TransacaoResponseDTO> DeleteAsync(DeleteTransacaoRequestDTO dto);
        Task<TransacaoFilterResponseDTO> GetTransacaoFilterAsync(TransacaoFilterRequestDTO dto);
    }
}
