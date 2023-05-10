using UnityEditor;
using UnityEngine;

namespace UniTools.Build
{
    [CustomPropertyDrawer(typeof(PathProperty))]
    public sealed class PathPropertyDrawer : PropertyDrawer
    {
        private const int TypeMaxWidth = 80;
        private const int ContentPreferredWidth = 240;
        private const float PreferredWidth = TypeMaxWidth + ContentPreferredWidth; 

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var typeWidth = position.width >= PreferredWidth
                ? TypeMaxWidth
                : TypeMaxWidth / PreferredWidth * position.width;
            var contentWidth = position.width - typeWidth;
            // Calculate rects
            Rect firstRect = new Rect(position.x, position.y, contentWidth, position.height);
            Rect secondRect = new Rect(position.x + contentWidth, position.y, typeWidth, position.height);

            SerializedProperty typeProperty = property.FindPropertyRelative("m_type");

            PathTypes t1 = (PathTypes) typeProperty.enumValueIndex;

            if (t1 == PathTypes.String)
            {
                SerializedProperty p = property.FindPropertyRelative("m_path");
                Color c = GUI.color;
                if (string.IsNullOrEmpty(p.stringValue))
                {
                    GUI.color = Color.red;
                }

                EditorGUI.PropertyField(firstRect, p, GUIContent.none);
                GUI.color = c;
            }

            if (t1 == PathTypes.Scriptable)
            {
                SerializedProperty p = property.FindPropertyRelative("m_scriptablePath");

                Color c = GUI.color;
                if (p.objectReferenceValue == null)
                {
                    GUI.color = Color.red;
                }

                EditorGUI.PropertyField(firstRect, p, GUIContent.none);
                GUI.color = c;
            }

            EditorGUI.PropertyField(secondRect, typeProperty, GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}