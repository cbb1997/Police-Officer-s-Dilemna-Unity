#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EditorInvokeButton
{
    [CustomEditor(typeof(Object), true)]
    public class InvokeButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var instance     = target;
            var instanceType = instance.GetType();
            var methods      = instanceType.GetMethods(
                                   BindingFlags.Instance |
                                   BindingFlags.Public   |
                                   BindingFlags.NonPublic);

            foreach (var m in methods)
            {
                var attr = m.GetCustomAttribute<InvokeButtonAttribute>();
                if (attr == null) continue;
                if (m.GetParameters().Length != 0) continue; 

                string label = string.IsNullOrEmpty(attr.Label)
                               ? ObjectNames.NicifyVariableName(m.Name)
                               : attr.Label;

                if (GUILayout.Button(label))
                {
                    Undo.RecordObject(instance, $"Invoke {m.Name}");
                    m.Invoke(instance, null);
                    EditorUtility.SetDirty(instance);
                }
            }
        }
    }
}
#endif
