using ControleResidencial.DTOs.Transacao.Request;
using ControleResidencial.DTOs.Transacao.Response;
using ControleResidencial.DTOs.Usuario.Response;
using ControleResidencial.Infra.Repository.Entity;
using ControleResidencial.Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleResidencial.Infra.Repository
{

    public class TransacaoRepository : BaseRepository<Transacao>, ITransacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public TransacaoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        // chama o método create do repositório base
        public void CreateAsync(Transacao transacao)
        {
            Create(transacao);
        }
        // chama o método delete do repositório base
        public void DeleteAsync(Transacao transacao)
        {
            Delete(transacao);
        }
        // chama o método update do repositório base
        public void UpdateAsync(Transacao transacao)
        {
            Update(transacao);
        }
        // busca uma transacao pelo id

        public async Task<Transacao?> GetById(string id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
        // listagem com filtro, ordenação e paginação
        public async Task<TransacaoFilterResponseDTO> ListTransacaoAsync(TransacaoFilterRequestDTO dto)
        {
            var query = _context.Transacoes.AsQueryable();
            // filtra por descrição se informado
            if (!string.IsNullOrWhiteSpace(dto.Pesquisa))
            {
                query = query.Where(x =>
                    x.Descricao.Contains(dto.Pesquisa));
            }

            var total = await query.CountAsync();
            if (dto.OrdenarColunaDirecao?.ToLower() == "desc") 
                query = query.OrderByDescending(x => x.DataTransacao);
            else                
                query = query.OrderBy(x => x.DataTransacao);
            var lista = await query
                .Skip((dto.Page - 1) * dto.Regs)
                .Take(dto.Regs)
                .Select(x => new TransacaoFilterDataDTO
                {
                    Id = x.Id.ToString(),
                    Descricao = x.Descricao,
                    Valor = x.Valor,
                    DataTransacao = x.DataTransacao,
                    Usuario = x.UsuarioId != null ? _context.Usuarios
                    .Where(u => u.Id == x.UsuarioId)
                    .Select(u => new UsuarioFilterDataDTO
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Idade = u.Idade,
                        IsAtivo = u.IsAtivo,
                        DataInclusao = u.DataInclusao,
                        DataAtualizacao = u.DataAtualizacao
                    })
                     .FirstOrDefault()
                     : null
                })
                .ToListAsync();

            return new TransacaoFilterResponseDTO
            {
                Total = total,
                Lista = lista
            };

        }
        // busca uma transacao pelo usuarioid
        public async Task<List<Transacao>> GetByUsuarioId(string usuarioId)
        {
            return await FindByCondition(x => x.UsuarioId == usuarioId)
                .ToListAsync();
        }


    }
}
