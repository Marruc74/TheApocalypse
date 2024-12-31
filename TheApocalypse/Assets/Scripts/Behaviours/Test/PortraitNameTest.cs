using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Behaviours.Test
{
    public class PortraitNameTest : MonoBehaviour
    {
        public Text Text;

        public void Start()
        {
            var parent = transform.parent.gameObject;
            var npc = parent.GetComponent<PortraitTest>().Character;
            Text.GetComponent<Text>().text = npc.Name.Nickname;
        }
    }
}
