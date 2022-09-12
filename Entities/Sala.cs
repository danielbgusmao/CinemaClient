using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CinemaClient.Entities
{
    public class Sala : BaseEntity
    {
        [Display(Name = "Nome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo nome é obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Quantidade de assentos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo quantiodade de assentos é obrigatório.")]
        public int QuantidadeAssentos { get; set; }

    }
}
