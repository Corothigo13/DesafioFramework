using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DatabaseContext
{
    public class Estado : Entity
    {
        [Required]
        public string Descricao { get; set; }
        public string Pais { get; set; }
        [Required]
        public int PaisId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
