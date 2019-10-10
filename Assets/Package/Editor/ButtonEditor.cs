using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HandyButtons
{
    public abstract class ButtonEditor : Editor
    {
        private IList<ButtonData> _buttons;

        protected virtual void OnEnable()
        {
            _buttons = ButtonUtility.GetButtons(this);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ButtonUtility.DrawButtons(this, _buttons);
        }
    }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    public class MonoBehaviourButtonEditor : ButtonEditor
    { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableObject), true, isFallback = true)]
    public class ScriptableObjectButtonEditor : ButtonEditor
    { }
}
