using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assets.Scripts.Utils.Guard
{
    /// <summary>
    /// Methods to make sure valid properties are added to a mono behaviour.
    /// </summary>
    public static class GuardProperty
    {
        /// <summary>
        /// Throws an error if the object is null.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="parameter">The name of the parameter.</param>
        public static void ArgumentIsNull(object value, string parameter)
        {
            if (value.ToString() == "null" || value == null)
            {
                var stackTrace = new StackTrace();

                throw new ArgumentNullException(parameter, string.Format("Value can not be null in {0}", stackTrace.GetFrame(1).GetMethod().Name));
            }
        }

        /// <summary>
        /// Throws an error if the string is empty.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <param name="parameter">The name of the parameter.</param>
        public static void TextIsEmpty(string text, string parameter)
        {
            if (IsNullOrWhiteSpace(text))
            {
                var stackTrace = new StackTrace();

                throw new ArgumentOutOfRangeException(parameter, string.Format("Text can not be empty in {0}", stackTrace.GetFrame(1).GetMethod().Name));
            }
        }

        /// <summary>
        /// Throws an error if the list is empty.
        /// </summary>
        /// <typeparam name="T">The type of items in the list</typeparam>
        /// <param name="list">The list to check</param>
        /// <param name="variable">The name of the variable.</param>
        public static void ArgumentIsEmptyList<T>(List<T> list, string variable)
        {
            if (list.Count == 0)
            {
                var stackTrace = new StackTrace();

                throw new ArgumentException(variable, string.Format("List can not be empty in {0}", stackTrace.GetFrame(1).GetMethod().Name));
            }
        }

        /// <summary>
        /// Check if a string is null, empty or only contains whitespaces.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True if the string is null, empty or only contains whitespaces, otherwise false.</returns>
        private static bool IsNullOrWhiteSpace(string text)
        {
            if (string.IsNullOrEmpty(text) || ConsistsOfWhiteSpace(text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if a string only contains whitespaces.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True if the string only contains whitespaces, otherwise false.</returns>
        private static bool ConsistsOfWhiteSpace(string text)
        {
            foreach (char character in text)
            {
                if (character != ' ') return false;
            }
            return true;
        }
    }
}
