using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HandyButtons
{
    public static class ButtonUtility
    {
        private const BindingFlags kBindings = BindingFlags.Public
                                             | BindingFlags.NonPublic
                                             | BindingFlags.Instance
                                             | BindingFlags.Static;

        public static IList<ButtonData> GetButtons(Editor editor)
        {
            var buttons = new List<ButtonData>();

            var methods = editor.target
                                .GetType()
                                .GetMethods(kBindings)
                                .Where(m => m.GetParameters().Length == 0
                                         && !m.ContainsGenericParameters);

            foreach (var method in methods)
            {
                var attribute = Attribute.GetCustomAttribute(method, typeof(ButtonAttribute)) as ButtonAttribute;
                if (attribute == null)
                    continue;

                var button = new ButtonData();
                button.method = method;
                button.title = attribute.title;
                button.mode = attribute.mode;

                if (string.IsNullOrEmpty(button.title))
                    button.title = ObjectNames.NicifyVariableName(method.Name);

                buttons.Add(button);
            }

            return buttons;
        }

        public static void DrawButtons(Editor editor, IList<ButtonData> buttons)
        {
            var isPlaying = Application.isPlaying;
            var targets = editor.targets;

            for (int i = 0, icount = buttons.Count; i < icount; ++i)
            {
                var button = buttons[i];
                var disabled = (button.mode == ExecutionMode.EditModeOnly && isPlaying)
                            || (button.mode == ExecutionMode.PlayModeOnly && !isPlaying);

                using (new EditorGUI.DisabledScope(disabled))
                {
                    if (GUILayout.Button(button.title))
                    {
                        for (int z = 0, zcount = targets.Length; z < zcount; ++z)
                        {
                            button.method.Invoke(targets[z], null);
                        }
                    }
                }
            }
        }
    }
}
