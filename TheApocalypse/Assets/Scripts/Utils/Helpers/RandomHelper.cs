using Assets.Scripts.Utils.Guard;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils.Helpers
{
    public class RandomHelper
    {
        /// <summary>
        /// Get a random item from a list. Each item in the list must contain an int called chance and a ScriptableObject called data.
        /// A random number between 1 and 100 will be used to loop through the list. If the random number is lower than the chance of the 
        /// current item plus the chance of all before, the item will be selected. If no item is selected, the default will be returned.
        /// </summary>
        /// <typeparam name="TData">A scriptable object connected to the data property of the items in the list.</typeparam>
        /// <typeparam name="TChance">The class the objects in the list are instantiated from.</typeparam>
        /// <param name="list">A list of items to search trough</param>
        /// <param name="defaultData">The default item to be returned if no other is selected.</param>
        /// <returns></returns>
        public TData GetRandomDataByChance<TData, TChance>(List<TChance> list, TData defaultData)
        {
            GuardParameter.ArgumentIsEmptyList(list, "list");
            GuardParameter.ArgumentIsNull(defaultData, "defaultData");

            var randomValue = Random.Range(1, 101);
            int currentValue = 0;
            int count = 1;

            foreach (var obj in list)
            {
                if (obj.GetType().GetField("chance") == null)
                {
                    Debug.Log("error");
                }
                var chance = System.Convert.ToInt32(obj.GetType().GetField("chance").GetValue(obj));
               
                currentValue = currentValue + chance;
                if (randomValue <= currentValue)
                {
                    return (TData)(obj.GetType().GetField("data").GetValue(obj));
                }
                count++;
            }

            return defaultData;
        }

        /// <summary>
        /// Get a random item from a list.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The list of items.</param>
        /// <param name="nullChance">The chance for no item to picked at all.</param>
        /// <returns>The random item selected, or default depeding on type is no item should be picked.</returns>
        public T GetRandomFromList<T>(List<T> list, int nullChance = 0)
        {
            if (list == null || list.Count() == 0 || Random.Range(0, 101) < nullChance)
            {
                return default(T);
            }

            var index = Random.Range(0, list.Count);

            return list[index];
        }

        /// <summary>
        /// Get a random value from an enum
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>The random value from the enum.</returns>
        public T GetRandomFromEnum<T>()
        {
            var array = System.Enum.GetValues(typeof(T));
            T value = (T)array.GetValue(Random.Range(0, array.Length));
            return value;
        }
    }
}
