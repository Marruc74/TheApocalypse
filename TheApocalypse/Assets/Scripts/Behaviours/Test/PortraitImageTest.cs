using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Behaviours.Test
{
    public class PortraitImageTest : MonoBehaviour
    {
        public Image Image;

        public void Start()
        {
            var parent = transform.parent.gameObject;
            var npc = parent.GetComponent<PortraitTest>().Character;
            Image.GetComponent<Image>().sprite = npc.Portrait.GetFullImage(npc.WearableItems);
        }
    }
}
