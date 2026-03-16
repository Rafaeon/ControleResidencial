using AutoMapper;
using ControleResidencial.DTOs.Usuario.Request;
using ControleResidencial.DTOs.Usuario.Response;
using ControleResidencial.Infra.Repository;
using ControleResidencial.Infra.Repository.Entity;
using ControleResidencial.Infra.Repository.Interfaces;
using ControleResidencial.Services.Usuario.Interface;
using Microsoft.AspNetCore.Identity;

namespace ControleResidencial.Services.Usuario
{
    // arquitetura de services que seguimos no meu trabalho atual,
    // separa a regra de negócio do repositório e do controller
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IBaseRepository<Infra.Repository.Entity.Usuario> _baseRepository;
        public UsuarioServices(IBaseRepository<Infra.Repository.Entity.Usuario> baseRepository, IUsuarioRepository usuarioRepository, ITransacaoRepository transacaoRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _usuarioRepository = usuarioRepository;
            _transacaoRepository = transacaoRepository;
            _mapper = mapper;
        }
        // retorna a listagem filtrada de usuarios
        public async Task<UsuarioFilterResponseDTO> GetUsuarioFilterAsync(UsuarioFilterRequestDTO dto)
        {
            return await _usuarioRepository.ListUsuarioAsync(dto);
        }
        // cria um novo usuario com os dados do dto
        public async Task<UsuarioResponseDTO> CreateAsync(CreateUsuarioRequestDTO dto)
        {
            var response = new UsuarioResponseDTO();
            try
            {
                var data = new Infra.Repository.Entity.Usuario();
                data.Id = Guid.NewGuid().ToString();
                data.Nome = dto.Nome;
                data.IsAtivo = true;
                data.Idade = dto.Idade;

                data.DataInclusao = DateTime.UtcNow;

                _usuarioRepository.CreateAsync(data);

                await _baseRepository.SaveChangesAsync();
                response.IdentityResult = IdentityResult.Success;
                response.Data = _mapper.Map<UsuarioResponseDataDTO>(data);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the usuario.", ex);
            }
            return response;
        }
        // remove um usuario pelo id, retorna erro se não encontrar
        public async Task<UsuarioResponseDTO> DeleteAsync(DeleteUsuarioRequestDTO dto)
        {
            var response = new UsuarioResponseDTO();

            try
            {
                var data = await _usuarioRepository.GetById(dto.Id);

                if (data == null)
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError()
                        {
                            Code = "001",
                            Description = "Registro não localizado"
                        });

                    return response;
                }

                // busca e deleta todas as transações do usuário antes de deletar o usuário
                var transacoes = await _transacaoRepository.GetByUsuarioId(dto.Id);
                if (transacoes != null && transacoes.Any())
                {
                    foreach (var transacao in transacoes)
                    {
                        _transacaoRepository.DeleteAsync(transacao);
                    }
                }

                _usuarioRepository.DeleteAsync(data);

                await _baseRepository.SaveChangesAsync();

                response.IdentityResult = IdentityResult.Success;
                response.Data = _mapper.Map<UsuarioResponseDataDTO>(data);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the usuario.", ex);
            }
        }
        // atualiza nome, idade e data de atualização do usuario
        public async Task<UsuarioResponseDTO> UpdateAsync(UpdateUsuarioRequestDTO dto)
        {
            var response = new UsuarioResponseDTO();
            try
            {
                var data = await _usuarioRepository.GetById(dto.Id);
                if (data == null)
                {
                    response.IdentityResult = IdentityResult.Failed(new IdentityError() { Code = "001", Description = "Registro não localizado" });
                    return response;
                }
                data.Nome = dto.Nome;
                data.IsAtivo = true;
                data.Idade = dto.Idade;
                data.DataAtualizacao = DateTime.UtcNow;

                _usuarioRepository.UpdateAsync(data);

                await _baseRepository.SaveChangesAsync();
                response.IdentityResult = IdentityResult.Success;
                response.Data = _mapper.Map<UsuarioResponseDataDTO>(data);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the usuario.", ex);
            }
            return response;
        }
        // busca um usuario pelo id, retorna erro se não encontrar
        public async Task<UsuarioResponseDTO> GetByUsuarioId(string usuarioId)
        {
            var response = new UsuarioResponseDTO();

            try
            {
                var data = await _usuarioRepository.GetById(usuarioId);

                if (data == null)
                {
                    response.IdentityResult = IdentityResult.Failed(
                        new IdentityError()
                        {
                            Code = "001",
                            Description = "Registro não localizado"
                        });

                    return response;
                }

                response.IdentityResult = IdentityResult.Success;
                response.Data = _mapper.Map<UsuarioResponseDataDTO>(data);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the usuario.", ex);
            }
        }
    }
}
