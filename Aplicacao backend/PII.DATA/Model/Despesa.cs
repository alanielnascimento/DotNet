using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PII.DATA.Model
{
    public class Despesa
    {
        public int DespesaId { get; set; }

        [StringLength(100)]
        public string Titulo { get; set; }

        [Precision(18, 2)]
        public decimal Preco { get; set; }
        public DateTime DateTime { get; set; }
    }
}
