using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CinemaClient.Enums
{
    public enum TipoAnimacaoEnum
    {

        [Description("Animação 2D")]
        Animacao2D = 1,
        [Description("Animação 3D")]
        Animacao3D = 2
    }
}
