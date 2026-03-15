using ControleResidencial.DTOs.Categoria.Request;
using ControleResidencial.DTOs.Usuario.Request;
using ControleResidencial.Services.Categoria.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ControleResidencial.Controllers
{
    // as controllers seguem o padrão que utilizamos no meu trabalho atual:
    // herdam o basecontroller que centraliza o customresponse,
    // validam o modelstate antes de chamar a service,
    // e delegam toda a regra de negócio para a camada de service

    //no meu ponto de vista, organizar o projeto em camadas facilita na hora de debugar
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaServices _categoriaService;

        public CategoriaController(ICategoriaServices categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoriaRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _categoriaService.CreateAsync(dto));
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetCategoriaFilterAsync([FromQuery] CategoriaFilterRequestDTO dto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _categoriaService.GetCategoriaFilterAsync(dto));
        }
    }
}
