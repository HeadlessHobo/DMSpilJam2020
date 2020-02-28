using Plugins.NaughtyAttributes.Scripts.Core.DrawerAttributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Attributes;
using Plugins.NaughtyAttributes.Scripts.Editor.Utility;
using UnityEditor;

namespace Plugins.NaughtyAttributes.Scripts.Editor.PropertyDrawers
{
    [PropertyDrawer(typeof(SliderAttribute))]
    public class SliderPropertyDrawer : PropertyDrawer
    {
        public override void DrawProperty(SerializedProperty property)
        {
            EditorDrawUtility.DrawHeader(property);

            SliderAttribute sliderAttribute = PropertyUtility.GetAttribute<SliderAttribute>(property);

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUILayout.IntSlider(property, (int)sliderAttribute.MinValue, (int)sliderAttribute.MaxValue);
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                EditorGUILayout.Slider(property, sliderAttribute.MinValue, sliderAttribute.MaxValue);
            }
            else
            {
                string warning = sliderAttribute.GetType().Name + " can be used only on int or float fields";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, context: PropertyUtility.GetTargetObject(property));

                EditorDrawUtility.DrawPropertyField(property);
            }
        }
    }
}
