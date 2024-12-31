using Assets.Scripts.Behaviours.Instances.Settlement;
using Assets.Scripts.Models.Settlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers
{
    public class SettlementManager : MonoBehaviour
    {
        public void Start()
        {
            var settlement = GetComponent<AssetManager>().GetSettlementTypes(GameManagerInstance.Instance.GameInfo.PlayerFaction.Settlements[0].SettlementTypeId);
            GameManagerInstance.Instance.GameInfo.PlayerFaction.Settlements[0].Grid = new Square[settlement.xSize, settlement.zSize];

            GameObject grid = new GameObject
            {
                name = "Grid"
            };

            for (int z = 0; z < settlement.zSize; z++)
            {
                for (int x = 0; x < settlement.xSize; x++)
                {
                    GameManagerInstance.Instance.GameInfo.PlayerFaction.Settlements[0].Grid[x, z] = new Square();
                    GameObject floorInstance = Instantiate(settlement.floor, new Vector3(x * 5, 0 , z* 5), Quaternion.identity) as GameObject;
                    floorInstance.AddComponent<GridSquare>();
                    var script = floorInstance.GetComponent<GridSquare>();
                    script.xPosition = x;
                    script.zPosition = z;
                    floorInstance.name = String.Format("Grid{0}-{1}", x, z);
                    floorInstance.transform.parent = grid.transform;
                }
            }
        }
    }
}
