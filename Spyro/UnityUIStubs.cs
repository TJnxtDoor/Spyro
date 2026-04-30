using System;
using UnityEngine;

namespace UnityEngine.UI
{
    public class Text : MonoBehaviour
    {
        public string text;
    }

    public class Slider : MonoBehaviour
    {
        public float value;
    }

    public class Image : MonoBehaviour
    {
        public Color color;
    }

    public class Button : MonoBehaviour
    {
        public ButtonClickedEvent onClick = new ButtonClickedEvent();
    }

    public class ButtonClickedEvent
    {
        public void AddListener(Action callback)
        {
        }
    }
}
