using UnityEngine;

namespace BaltaRed.Editor.AttributeExtensions
{
    public enum EnumSlider
    {
        INT2,
        FLOAT2,
        STRING2,
        STRINGFLOAT,
        FLOATSTRING,
        INTSTRING,
        STRINGINT,
    }

    public class MinMaxSliderAttribute : PropertyAttribute{

        public float minFloat;
        public float maxFloat;

        public int minInt;
        public int maxInt;

        public string minString;
        public string maxString;

        public EnumSlider enumSlider = EnumSlider.FLOAT2;

        public MinMaxSliderAttribute(float min, float max)
        {
            enumSlider = EnumSlider.FLOAT2;

            minFloat = min;
            maxFloat = max;
        }

        public MinMaxSliderAttribute(int min, int max)
        {
            enumSlider = EnumSlider.INT2;

            minInt = min;
            maxInt = max;
        }

        public MinMaxSliderAttribute(string min, string max)
        {
            enumSlider = EnumSlider.STRING2;

            minString = min;
            maxString = max;
        }

        public MinMaxSliderAttribute(string min, float max)
        {
            enumSlider = EnumSlider.STRINGFLOAT;

            minString = min;
            maxFloat = max;
        }

        public MinMaxSliderAttribute(float min, string max)
        {
            enumSlider = EnumSlider.FLOATSTRING;

            minFloat = min;
            maxString = max;
        }

        public MinMaxSliderAttribute(int min, string max)
        {
            enumSlider = EnumSlider.INTSTRING;

            minInt = min;
            maxString = max;
        }

        public MinMaxSliderAttribute(string min, int max)
        {
            enumSlider = EnumSlider.STRINGINT;

            minString = min;
            maxInt = max;
        }
    }  
}