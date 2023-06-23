using UnityEngine;

namespace BaltaRed.SlidingPuzzle.Customization.Editor
{
    public sealed class CustomRangeAttribute : PropertyAttribute
    {
        public readonly float minValue;
        public readonly float maxValue;

        public string minValueVariable;
        public string maxValueVariable;

        public CustomRangeAttribute(string minValueVariable, string maxValueVariable)
        {
            this.minValueVariable = minValueVariable;
            this.maxValueVariable = maxValueVariable;
        }

        public CustomRangeAttribute(float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public CustomRangeAttribute(float minValue, string maxValueVariable)
        {
            this.minValue = minValue;
            this.maxValueVariable = maxValueVariable;
        }
    }
}