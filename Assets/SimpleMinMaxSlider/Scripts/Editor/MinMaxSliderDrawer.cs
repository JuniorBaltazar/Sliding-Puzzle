using UnityEngine;
using UnityEditor;

namespace BaltaRed.Editor.AttributeExtensions.Drawer
{
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            MinMaxSliderAttribute minMaxAttribute = (MinMaxSliderAttribute)attribute;
            SerializedPropertyType propertyType = property.propertyType;

            label.tooltip = minMaxAttribute.minFloat.ToString("F2") + " to " + minMaxAttribute.maxFloat.ToString("F2");

            //PrefixLabel returns the rect of the right part of the control.
            Rect controlRect = EditorGUI.PrefixLabel(rect, label);

            Rect[] splittedRect = SplitRect(controlRect, 3);

            if (propertyType == SerializedPropertyType.Vector2)
            {
                OnGuiSetVector2(property, minMaxAttribute, splittedRect);
            }
            else if (propertyType == SerializedPropertyType.Vector2Int)
            {
                OnGuiSetVector2Int(property, minMaxAttribute, splittedRect);
            }
            else if (propertyType == SerializedPropertyType.Float)
            {
                float minValue;
                float maxValue;

                string minString;
                string maxString;

                if (minMaxAttribute.enumSlider == EnumSlider.FLOAT2)
                {
                    minValue = minMaxAttribute.minFloat;
                    maxValue = minMaxAttribute.maxFloat;
                }
                else if (minMaxAttribute.enumSlider == EnumSlider.STRING2)
                {
                    minString = minMaxAttribute.minString;
                    maxString = minMaxAttribute.maxString;

                    SerializedObject serializedObject = property.serializedObject;
                    SerializedProperty minValueProperty = serializedObject.FindProperty(minString);
                    SerializedProperty maxValueProperty = serializedObject.FindProperty(maxString);

                    minValue = minValueProperty.floatValue;
                    maxValue = maxValueProperty.floatValue;
                }
                else if (minMaxAttribute.enumSlider == EnumSlider.STRINGFLOAT)
                {
                    minString = minMaxAttribute.minString;
                    maxValue = minMaxAttribute.maxFloat;

                    SerializedObject serializedObject = property.serializedObject;
                    SerializedProperty minValueProperty = serializedObject.FindProperty(minString);

                    minValue = minValueProperty.floatValue;
                }
                else
                {
                    minValue = minMaxAttribute.minFloat;
                    maxString = minMaxAttribute.maxString;

                    SerializedObject serializedObject = property.serializedObject;
                    SerializedProperty maxValueProperty = serializedObject.FindProperty(maxString);

                    maxValue = maxValueProperty.floatValue;
                }

                EditorGUI.BeginChangeCheck();

                EditorGUI.Slider(rect, property, minValue, maxValue, label);

                if (EditorGUI.EndChangeCheck())
                {
                    property.floatValue = Mathf.Clamp(property.floatValue, minValue, maxValue);
                }
            }
            else if (propertyType == SerializedPropertyType.Integer)
            {
                int minValue;
                int maxValue;

                string minString;
                string maxString;
                
                
                if (minMaxAttribute.enumSlider == EnumSlider.INT2)
                {
                    minValue = minMaxAttribute.minInt;
                    maxValue = minMaxAttribute.maxInt;
                }
                else if (minMaxAttribute.enumSlider == EnumSlider.STRING2)
                {
                    minString = minMaxAttribute.minString;
                    maxString = minMaxAttribute.maxString;

                    SerializedObject serializedObject = property.serializedObject;
                    SerializedProperty minValueProperty = serializedObject.FindProperty(minString);
                    SerializedProperty maxValueProperty = serializedObject.FindProperty(maxString);

                    minValue = minValueProperty.intValue;
                    maxValue = maxValueProperty.intValue;
                }
                else if (minMaxAttribute.enumSlider == EnumSlider.INTSTRING)
                {
                    minValue = minMaxAttribute.minInt;
                    maxString = minMaxAttribute.maxString;

                    SerializedObject serializedObject = property.serializedObject;
                    SerializedProperty maxValueProperty = serializedObject.FindProperty(maxString);

                    maxValue = maxValueProperty.intValue;
                }
                else
                {
                    minString = minMaxAttribute.minString;
                    maxValue = minMaxAttribute.maxInt;

                    SerializedObject serializedObject = property.serializedObject;
                    SerializedProperty minValueProperty = serializedObject.FindProperty(minString);

                    minValue = minValueProperty.intValue;
                }

                EditorGUI.BeginChangeCheck();

                EditorGUI.IntSlider(rect, property, minValue, maxValue, label);

                if (EditorGUI.EndChangeCheck())
                {
                    property.intValue = Mathf.Clamp(property.intValue, minValue, maxValue);
                }
            }

            // Local Methods
            static void OnGuiSetVector2(SerializedProperty property, MinMaxSliderAttribute minMaxAttribute, Rect[] splittedRect)
            {
                EditorGUI.BeginChangeCheck();

                Vector2 vector2 = property.vector2Value;
                float minValue = vector2.x;
                float maxValue = vector2.y;

                //F2 limits the float to two decimal places (0.00).
                minValue = EditorGUI.FloatField(splittedRect[0], float.Parse(minValue.ToString("F2")));
                maxValue = EditorGUI.FloatField(splittedRect[2], float.Parse(maxValue.ToString("F2")));

                EditorGUI.MinMaxSlider(splittedRect[1], ref minValue, ref maxValue,
                minMaxAttribute.minFloat, minMaxAttribute.maxFloat);

                if (minValue < minMaxAttribute.minFloat)
                {
                    minValue = minMaxAttribute.minFloat;
                }

                if (maxValue > minMaxAttribute.maxFloat)
                {
                    maxValue = minMaxAttribute.maxFloat;
                }

                vector2 = new Vector2(minValue > maxValue ? maxValue : minValue, maxValue);

                if (EditorGUI.EndChangeCheck())
                {
                    property.vector2Value = vector2;
                }
            }

            static void OnGuiSetVector2Int(SerializedProperty property, MinMaxSliderAttribute minMaxAttribute, Rect[] splittedRect)
            {
                EditorGUI.BeginChangeCheck();

                Vector2Int vector2Int = property.vector2IntValue;
                float minValue = vector2Int.x;
                float maxValue = vector2Int.y;

                minValue = EditorGUI.FloatField(splittedRect[0], minValue);
                maxValue = EditorGUI.FloatField(splittedRect[2], maxValue);

                EditorGUI.MinMaxSlider(splittedRect[1], ref minValue, ref maxValue,
                minMaxAttribute.minFloat, minMaxAttribute.maxFloat);

                if (minValue < minMaxAttribute.minFloat)
                {
                    maxValue = minMaxAttribute.minFloat;
                }

                if (minValue > minMaxAttribute.maxFloat)
                {
                    maxValue = minMaxAttribute.maxFloat;
                }

                vector2Int = new Vector2Int(Mathf.FloorToInt(minValue > maxValue ? maxValue : minValue), Mathf.FloorToInt(maxValue));

                if (EditorGUI.EndChangeCheck())
                {
                    property.vector2IntValue = vector2Int;
                }
            }
        }

        private Rect[] SplitRect(Rect rectToSplit, int amountSplitedRects)
        {
            Rect[] rects = new Rect[amountSplitedRects];

            for (int i = 0; i < amountSplitedRects; i++)
            {
                rects[i] = new Rect(rectToSplit.position.x + (i * rectToSplit.width / amountSplitedRects), rectToSplit.position.y, rectToSplit.width / amountSplitedRects, rectToSplit.height);
            }

            int padding = (int)rects[0].width - 40;
            int space = 5;

            rects[0].width -= padding + space;
            rects[2].width -= padding + space;

            rects[1].x -= padding;
            rects[1].width += padding * 2;

            rects[2].x += padding + space;

            return rects;
        }
    }
}