using CinemaClient.Entities;
using CinemaClient.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using Cinema.Application.Models;

namespace CinemaClient.Models
{
    public class SessaoViewModel : EntityViewModel
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
        public string ValorIngresso { get; set; }

        [Display(Name = "Tipo de Animação")]
        [EnumDataType(typeof(TipoAnimacaoEnum))]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo tipo de animação é obrigatório.")]
        public string TipoAnimacao { get; set; }

        [Display(Name = "Tipo de Áudio")]
        [EnumDataType(typeof(TipoAudioEnum))]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo tipo de áudio é obrigatório.")]
        public string TipoAudio { get; set; }

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
