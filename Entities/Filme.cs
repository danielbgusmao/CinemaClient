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
    public class Filme : BaseEntity
    {
        [Display(Name = "Título")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo título é obrigatório.")]
        public string Titulo { get; set; }

        [Display(Name = "Imagem")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo Imagem é obrigatório.")]
        public byte[] Imagem { get; set; }

        [Display(Name = "Descrição")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo descrição é obrigatório.")]
        public string Descricao { get; set; }

        [Display(Name = "Duração")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo duração é obrigatório.")]
        public string Duracao { get; set; }

        public List<Sessao>? Sessoes { get; set; }

    }
}
