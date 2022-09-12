using CinemaClient.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;

namespace CinemaClient.Models
{
    public static class ExtensionMethods
    {
        public static TipoAnimacaoEnum ToTipoAnimacaoEnum(string item)
        {
            var tipoAnimacao = (TipoAnimacaoEnum)Enum.Parse(typeof(TipoAnimacaoEnum), item);
            return tipoAnimacao;
        }
        
        public static TipoAudioEnum ToTipoAudioEnum(string item)
        {
            var tipoAudio = (TipoAudioEnum)Enum.Parse(typeof(TipoAudioEnum), item);
            return tipoAudio;
        }

        public static string GetDescription(this Enum enumValue)
        {
            object[] attr = enumValue.GetType().GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attr.Length > 0
               ? ((DescriptionAttribute)attr[0]).Description
               : enumValue.ToString();
        }

        public static SelectList ToSelectList<TEnum>(this TEnum obj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return new SelectList(Enum.GetValues(typeof(TEnum))
                .OfType<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = GetDescription(x),
                    Value = Convert.ToInt32(x).ToString()
                }), "Value", "Text");
        }

        public static SelectList ToSelectList<TEnum>(this TEnum obj, object SelectValue)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return new SelectList(Enum.GetValues(typeof(TEnum))
                .OfType<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = GetDescription(x),
                    Value = Convert.ToInt32(x).ToString()
                }), "Value", "Text", SelectValue);
        }

    }
}
