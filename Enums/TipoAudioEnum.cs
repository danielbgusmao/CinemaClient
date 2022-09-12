using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CinemaClient.Enums
{
    public enum TipoAudioEnum
    {
        [Display(Name = "Original")]
        Original = 1,
        [Display(Name = "Dublado")]
        Dublado = 2
    }
}
