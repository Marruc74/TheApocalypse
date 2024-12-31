using Assets.Scripts.Utils.Enums.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Behaviours.Managers.SceneSpecific.CampaignMain
{
    public class CampaignMainManager : MonoBehaviour
    {
        public GameObject tileMap;

        void Update()
        {
            Test();
        }

        void Awake()
        {
            var savedGameManager = GetComponent<SavedGamesManager>();

            if (savedGameManager.GamePropertiesExists())
            {
                savedGameManager.LoadGameProperties();
            }
            else
            {
                savedGameManager.SaveGameProperties();
            }

            var name = GameManagerInstance.Instance.GameProperties.CurrentCampaign;

            if (!string.IsNullOrEmpty(name))
            {
                var savedGame = savedGameManager.Load(name);

                GameManagerInstance.Instance.GameInfo = savedGame.GameInfo;
            }
        }

        public void Test()
        {
            //Vector3Int tilePos = tileMap.GetComponent<Tilemap>().WorldToCell(Input.mousePosition);

            //YourTileClass tileInstance = ScriptableObject.CreateInstance<YourTileClass>();

            //Texture2D tex = Resources.Load<Texture2D>("tileTexture") as Texture2D;

            //Sprite sprite = new Sprite();

            //sprite = Sprite.Create(tex, new Rect(0, 0, 400, 400), new Vector2(0.5f, 0.5f));

            //Tile tile = Resources.Load<Tile>("tileInstance") as Tile;

            //tileInstance.sprite = sprite;

            //yourTileMap.SetTile(tilePos, tile);
        }
    }
}
