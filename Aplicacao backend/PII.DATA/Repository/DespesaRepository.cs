using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PII.DATA.Dto;
using PII.DATA.Model;

namespace PII.DATA.Repository
{
    public class DespesaRepository
    {
        private readonly DataContext _dataContext;

        public DespesaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<DespesaDTO> GetAll()
        {
            List<DespesaDTO> despesaDTOs = new List<DespesaDTO>();
            foreach (var item in _dataContext.Despesas.ToList())
            {
                despesaDTOs.Add(ModelToDto(item));
            }
            return despesaDTOs;
        }

        public DespesaDTO GetById(int id)
        {
           return ModelToDto(ById(id));
        }

        public List<Despesa> GetByTitulo(string titulo)
        {
            return _dataContext.Despesas.Where(d => d.Titulo.Equals(titulo)).ToList();
        }

        public Object GetTotal()
        {
            return _dataContext.Despesas.ToList()
                .GroupBy(d => d.Titulo)
                .Select(d => new
                {
                    Titulo = d.Key,
                    Total = d.Sum(s => s.Preco)
                }).ToList();
        }

        public void Add(DespesaDTO despesa)
        {
            _dataContext.Despesas.Add(DtoToModel(despesa));
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            EntityEntry<Despesa> temp = _dataContext.Despesas.Remove(ById(id));
            _dataContext.SaveChanges();

            if (temp.Entity == null)
            {
                throw new Exception("Falha ao remover a despesa!");
            }
        }

        public DespesaDTO Edit(DespesaDTO despesaDto)
        {
            Despesa despesa = DtoToModel(despesaDto); 
            despesa.DateTime = DateTime.Now;
            _dataContext.Entry(despesa).State = EntityState.Modified;
            _dataContext.SaveChanges();

            return despesaDto;
        }

        private Despesa? ById(int id)
        {
            return _dataContext.Despesas.FirstOrDefault(d => d.DespesaId == id);
        }

        private DespesaDTO ModelToDto(Despesa despesa)
        {
            DespesaDTO despesaDTO = new DespesaDTO();
            despesaDTO.DespesaId = despesa.DespesaId;
            despesaDTO.Titulo = despesa.Titulo;
            despesaDTO.Preco = despesa.Preco;
            despesaDTO.DateTime = despesa.DateTime;

            return despesaDTO;
        }

        private Despesa DtoToModel(DespesaDTO despesaDTO)
        {
            Despesa despesa = new Despesa();
            despesa.DespesaId = despesaDTO.DespesaId;
            despesa.Titulo = despesaDTO.Titulo;
            despesa.Preco = despesaDTO.Preco;
            despesa.DateTime = despesaDTO.DateTime;

            return despesa;
        }
    }
}
