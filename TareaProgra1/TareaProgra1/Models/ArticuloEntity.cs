using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TareaProgra1.Models
{
    public class ArticuloEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }
    }
}
