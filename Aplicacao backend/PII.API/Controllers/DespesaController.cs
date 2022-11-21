using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PII.DATA;
using PII.DATA.Dto;
using PII.DATA.Model;
using PII.DATA.Repository;

namespace PII.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly DespesaRepository _despesaRepository;

        public DespesaController(DataContext dataContext)
        {
            _despesaRepository = new DespesaRepository(dataContext);
        }

        [HttpGet("inicio")]
        public ActionResult Get()
        {
            return Ok(_despesaRepository.GetAll());
        }

        [HttpGet("byId/{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_despesaRepository.GetById(id));
        }

        [HttpGet("{titulo}")]
        public ActionResult GetByTitulo(string titulo)
        {
            return Ok(_despesaRepository.GetByTitulo(titulo));
        }

        [HttpGet("total")]
        public ActionResult GetTotal()
        {
            return Ok(_despesaRepository.GetTotal());
        }

        [HttpPost]
        public ActionResult Post([FromBody]DespesaDTO despesa)
        {
            if (!Validacao(despesa)) return Ok("Campos Obrigatorio!");
            
            despesa.DateTime = DateTime.Now;
            despesa.Titulo = despesa.Titulo.ToUpper();

            _despesaRepository.Add(despesa);

            return Ok("Despesa adicionada com sucesso!");
        }
      
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _despesaRepository.Delete(id);
                return Ok("Despesa removida com sucesso!");
            }
            catch (ArgumentException)
            {
                return Ok("Despesa não encontrada!");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Edit([FromBody] DespesaDTO despesaToEdit)
        {
            return Ok(_despesaRepository.Edit(despesaToEdit));
        }
        private bool Validacao(DespesaDTO despesa)
        {
            bool valido = true;

            if (string.IsNullOrEmpty(despesa.Titulo))
                valido = false;

            if (despesa.Preco <= 0)
                valido = false;

            return valido;
        }
    }
}
