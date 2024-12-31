using Assets.Scripts.Behaviours.Managers;
using Assets.Scripts.Managers;
using Assets.Scripts.Models.Character;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour {

    public GameObject managers;

	// Use this for initialization
	void Start () {
        var list = new List<Character>();

        for (int i = 0; i < 20; i++)
        {
            list.Add(new CharacterManager().CreateCharacter(managers.GetComponent<AssetManager>().GetRegion(1), managers.GetComponent<AssetManager>(), true, i + 1));
        }

        list = new CharacterManager().SetupRelations(list);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
