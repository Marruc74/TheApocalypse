using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utils.Helpers
{
    /// <summary>
    /// Various helper methods for use on the UI.
    /// </summary>
    public static class UiHelper
    {
        /// <summary>
        /// Set the text on the text component of a specific game object.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <param name="text">The text to set.</param>
        public static void SetText(GameObject gameObject, string text)
        {
            var textComponent = gameObject.GetComponent<Text>();

            if (textComponent != null)
            {
                textComponent.text = text;
            }
        }

        public static void SetTextInChild(GameObject gameObject, string text)
        {
            var textComponent = gameObject.GetComponentInChildren<Text>();

            if (textComponent != null)
            {
                textComponent.text = text;
            }
        }

        /// <summary>
        /// Gets the text on the text component of a specific game object.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <returns></returns>
        public static string GetText(GameObject gameObject)
        {
            var textComponent = gameObject.GetComponent<Text>();

            if (textComponent != null)
            {
                return textComponent.text;
            }

            return string.Empty;
        }

        public static void SetKeyValue(GameObject gameObject, string text, string value)
        {
            var textChild = gameObject.transform.Find("Name");
            var valueChild = gameObject.transform.Find("Value");

            if (textChild != null)
            {
                SetText(textChild.gameObject, text);
            }

            if (valueChild != null)
            {
                SetText(valueChild.gameObject, value);
            }
        }

        public static void SetImage(GameObject gameObject, Sprite image)
        {
            var component = gameObject.GetComponent<UnityEngine.UI.Image>();

            if (component != null)
            {
                component.sprite = image;
            }
        }
    }
}
