using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utils.Extensions
{
    public static class TransformExtensios
    {
        public static Transform ClearChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
            return transform;
        }
    }
}
