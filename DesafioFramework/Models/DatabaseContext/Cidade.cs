using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DatabaseContext
{
    public class Cidade : Entity
    {
        [Required]
        public string Descricao { get; set; }
        public string Estado { get; set; }
        [Required]
        public int EstadoId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
