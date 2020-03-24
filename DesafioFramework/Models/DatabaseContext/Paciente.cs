using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DatabaseContext
{
    public class Paciente : Entity
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage ="Favor seguir o padrão. Ex: 111.111.111-11")]
        public string CPF { get; set; }
        [Required]
        public int PaisId { get; set; }
        public string Pais { get; set; }
        [Required]
        public int EstadoId { get; set; }
        public string Estado { get; set; }
        [Required]
        public int CidadeId { get; set; }
        public string Cidade { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
