using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace AWSDBConnection.Models
{
    public class ArticulosEntity
    {
        [Key] 
        public int ID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

    }
}
