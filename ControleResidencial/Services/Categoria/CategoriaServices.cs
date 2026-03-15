using AutoMapper;
using ControleResidencial.DTOs.Categoria.Request;
using ControleResidencial.DTOs.Categoria.Response;
using ControleResidencial.DTOs.Transacao.Request;
using ControleResidencial.DTOs.Transacao.Response;
using ControleResidencial.Infra.Repository;
using ControleResidencial.Infra.Repository.Interfaces;
using ControleResidencial.Services.Categoria.Interface;
using Microsoft.AspNetCore.Identity;

namespace ControleResidencial.Services.Categoria
{
    public class CategoriaServices : ICategoriaServices
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IBaseRepository<Infra.Repository.Entity.Categoria> _baseRepository;
        private readonly IMapper _mapper;

        public CategoriaServices(ICategoriaRepository categoriaRepository, IBaseRepository<Infra.Repository.Entity.Categoria> baseRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        // cria uma nova categoria com os dados do dto
        public async Task<CategoriaResponseDTO> CreateAsync(CreateCategoriaRequestDTO dto)
        {
            var response = new CategoriaResponseDTO();

            try
            {
                var data = new Infra.Repository.Entity.Categoria();

                data.Id = Guid.NewGuid().ToString();
                data.Descricao = dto.Descricao;
                data.Finalidade = dto.Finalidade;

                _categoriaRepository.CreateAsync(data);

                await _baseRepository.SaveChangesAsync();

                response.IdentityResult = IdentityResult.Success;
                response.Data = _mapper.Map<CategoriaResponseDataDTO>(data);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the categoria.", ex);
            }

            return response;
        }
        // retorna a listagem filtrada das categorias
        public async Task<CategoriaFilterResponseDTO> GetCategoriaFilterAsync(CategoriaFilterRequestDTO dto)
        {
            return await _categoriaRepository.ListCategoriaAsync(dto);
        }
    }
}
