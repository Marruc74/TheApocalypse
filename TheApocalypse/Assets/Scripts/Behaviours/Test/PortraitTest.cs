using Assets.Scripts.Behaviours.Managers;
using Assets.Scripts.Managers;
using Assets.Scripts.Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Test
{
    public class PortraitTest : MonoBehaviour
    {
        public GameObject managers;
        public Character Character { get; set; }

        void Awake()
        {
            Character = new CharacterManager().CreateCharacter(managers.GetComponent<AssetManager>().GetRegion(1), managers.GetComponent<AssetManager>(), true, 1);
        }
    }
}
