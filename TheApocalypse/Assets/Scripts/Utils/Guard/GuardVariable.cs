using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.Utils.Guard
{
    /// <summary>
    /// Methods to guard methods from receiving invalid values in the parameters.
    /// </summary>
    public static class GuardVariable
    {
        /// <summary>
        /// Throws an error if the object is null.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="variable">The name of the variable.</param>
        public static void ArgumentIsNull(object value, string variable)
        {
            if (value == null)
            {
                var stackTrace = new StackTrace();

                throw new ArgumentNullException(variable, string.Format("Value can not be null in {0}", stackTrace.GetFrame(1).GetMethod().Name));
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
        /// Throws an error if the index is not in the list.
        /// </summary>
        /// <typeparam name="T">The type of items in the list</typeparam>
        /// <param name="list">The list to check</param>
        /// <param name="index">The index to check</param>
        /// <param name="variable">The name of the variable.</param>
        public static void ArgumentIndexNotInList<T>(List<T> list, int index, string variable)
        {
            if (index < 0 || index >= list.Count)
            {
                var stackTrace = new StackTrace();

                throw new ArgumentOutOfRangeException(variable, string.Format("List does not contain the index looked for in {0}", stackTrace.GetFrame(1).GetMethod().Name));
            }
        }

        /// <summary>
        /// Throws an error if the script is not on the game object.
        /// </summary>
        /// <typeparam name="T">The type of script</typeparam>
        /// <param name="gameObject">The game object to check.</param>
        public static void GameObjectNotHasScript<T>(GameObject gameObject) where T: Component
        {           
            if (gameObject.GetComponent<T>() == null)
            {
                var stackTrace = new StackTrace();

                throw new ArgumentException(gameObject.name, string.Format("GameObject does not have script in {0}", stackTrace.GetFrame(1).GetMethod().Name));
            }
        }

        /// <summary>
        /// Throws an error if the game object does not have a child with the specified name.
        /// </summary>
        /// <param name="gameObject">The game object to check.</param>
        /// <param name="child">The name of the child to look for.</param>
        public static void GameObjectNotHasChild(GameObject gameObject, string child)
        {
            if (gameObject.transform.Find(child) == null)
            {
                var stackTrace = new StackTrace();

                throw new ArgumentException(gameObject.name, string.Format("GameObject does not contain a child name {0} in {1}", child, stackTrace.GetFrame(1).GetMethod().Name));
            }
        }

        /// <summary>
        /// Throws an error if the value is zero or less.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="parameter">The name of the parameter.</param>
        public static void IntIsZeroOrLess(int value, string parameter)
        {
            if (value <= 0)
            {
                var stackTrace = new StackTrace();

                throw new ArgumentOutOfRangeException(parameter, string.Format("Value can not be zero or less in {0}", stackTrace.GetFrame(1).GetMethod().Name));
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
