using ControleResidencial.DTOs.Transacao.Request;
using ControleResidencial.DTOs.Transacao.Response;
using ControleResidencial.DTOs.Usuario.Request;
using ControleResidencial.DTOs.Usuario.Response;
using ControleResidencial.Infra.Repository.Entity;

namespace ControleResidencial.Infra.Repository.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<Transacao?> GetById(string id);
        void UpdateAsync(Transacao transacao);
        void CreateAsync(Transacao transacao);
        void DeleteAsync(Transacao transacao);
        Task<TransacaoFilterResponseDTO> ListTransacaoAsync(TransacaoFilterRequestDTO dto);
        Task<List<Transacao>> GetByUsuarioId(string usuarioId);
    }
}
