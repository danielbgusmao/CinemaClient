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
using System.ComponentModel;
using CinemaClient.Enums;
using System.Globalization;
using System.Reflection;
using CinemaClient.Models;
using NuGet.Packaging.Core;

namespace CinemaClient.Entities
{
    public class Sessao : BaseEntity
    {
        
        [Display(Name = "Data Início")]
        [DataType(DataType.DateTime)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo data de início é obrigatório.")]
        public DateTime? DataInicio { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Data de Término")]
        [DataType(DataType.DateTime)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo data de término é obrigatório.")]
        public DateTime? DataFim { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo valor do ingresso é obrigatório.")]
        public float? ValorIngresso { get; set; }

        [Display(Name = "Tipo de Animação")]
        [EnumDataType(typeof(TipoAnimacaoEnum))]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo tipo de animação é obrigatório.")]
        public string? TipoAnimacao { get; set; }

        [Display(Name = "Tipo de Áudio")]
        [EnumDataType(typeof(TipoAudioEnum))]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo tipo de áudio é obrigatório.")]
        public string? TipoAudio { get; set; }

        [Display(Name = "Filme")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo filme é obrigatório.")]
        public Guid FilmeId { get; set; }

        [Display(Name = "Sala")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo sala é obrigatório.")]
        public Guid SalaId { get; set; }

        public Filme? Filme { get; set; }

        public Sala? Sala { get; set; }

    }
}
