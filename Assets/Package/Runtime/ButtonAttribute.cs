using System;

namespace HandyButtons
{
    public enum ExecutionMode
    {
        Always,
        PlayModeOnly,
        EditModeOnly,
    }

    /// <summary>
    /// Attribute used to draw a button in the Unity Inspector.
    /// Put the [Button] attribute above a method in your MonoBehaviour/ScriptableObject class.
    /// You can optionally specify a button name shown in the inspector and 
    /// when a button is clickable (play mode, edit mode or both).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ButtonAttribute : Attribute
    {
        public readonly string title;
        public readonly ExecutionMode mode;

        public ButtonAttribute() : this(string.Empty, ExecutionMode.Always)
        { }

        public ButtonAttribute(string title) : this(title, ExecutionMode.Always)
        { }

        public ButtonAttribute(ExecutionMode mode) : this(string.Empty, mode)
        { }

        public ButtonAttribute(string title, ExecutionMode mode)
        {
            this.title = title;
            this.mode = mode;
        }
    }
}
