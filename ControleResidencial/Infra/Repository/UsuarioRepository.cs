using ControleResidencial.DTOs.Usuario.Request;
using ControleResidencial.DTOs.Usuario.Response;
using ControleResidencial.Infra.Repository.Entity;
using ControleResidencial.Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleResidencial.Infra.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        // chama o método create do repositório base
        public void CreateAsync(Usuario usuario)
        {
            Create(usuario);
        }
        // chama o método delete do repositório base

        public void DeleteAsync(Usuario usuario)
        {
            Delete(usuario);
        }
        // chama o método update do repositório base

        public void UpdateAsync(Usuario usuario)
        {
            Update(usuario);
        }
        // busca um usuario pelo id

        public async Task<Usuario?> GetById(string id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
        // listagem com filtro, ordenação e paginação
        public async Task<UsuarioFilterResponseDTO> ListUsuarioAsync(UsuarioFilterRequestDTO dto)
        {
            var query = _context.Usuarios.AsQueryable();
            if (!string.IsNullOrWhiteSpace(dto.Pesquisa))
            {
                query = query.Where(x =>
                    x.Nome.Contains(dto.Pesquisa));
            }

            var total = await query.CountAsync();
            if (dto.OrdenarColunaDirecao?.ToLower() == "desc")
                query = query.OrderByDescending(x => x.DataInclusao);
            else
                query = query.OrderBy(x => x.DataInclusao);

            var lista = await query
                .Skip((dto.Page - 1) * dto.Regs)
                .Take(dto.Regs)
                .Select(x => new UsuarioFilterDataDTO
                {
                    Id = x.Id.ToString(),
                    Nome = x.Nome,
                    Idade = x.Idade,
                    IsAtivo = x.IsAtivo,
                    DataInclusao = x.DataInclusao
                })
                .ToListAsync();

            return new UsuarioFilterResponseDTO
            {
                Total = total,
                Lista = lista
            };
        }

        
    }
}
