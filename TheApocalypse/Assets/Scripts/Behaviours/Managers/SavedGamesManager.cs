using Assets.Scripts.Models.Save;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Managers
{
    public class SavedGamesManager : MonoBehaviour
    {
        public void Save(SavedGame savedGame, string name)
        {
            var bf = new BinaryFormatter();
            var file = File.Create(string.Format("{0}/{1}.sav", Application.persistentDataPath, name));
            bf.Serialize(file, savedGame);
            file.Close();
        }

        public SavedGame Load(string name)
        {
            string fileName = string.Format("{0}/{1}.sav", Application.persistentDataPath, name);
            if (File.Exists(fileName))
            {
                var bf = new BinaryFormatter();
                var file = File.Open(fileName, FileMode.Open);
                var savedGame = (SavedGame)bf.Deserialize(file);
                file.Close();
                return savedGame;
            }
            return null;
        }

        public void SaveGameProperties()
        {
            var properties = GameManagerInstance.Instance.GameProperties;
            var bf = new BinaryFormatter();
            var file = File.Create(string.Format("{0}/properties.gam", Application.persistentDataPath));
            bf.Serialize(file, properties);
            file.Close();
        }

        public void LoadGameProperties()
        {
            string fileName = string.Format("{0}/properties.gam", Application.persistentDataPath);
            if (File.Exists(fileName))
            {
                var bf = new BinaryFormatter();
                var file = File.Open(fileName, FileMode.Open);
                var properties = (GameProperties)bf.Deserialize(file);
                file.Close();
                GameManagerInstance.Instance.GameProperties = properties;
            }
        }

        public bool FileExists(string name)
        {
            var fileName = string.Format("{0}/{1}.sav", Application.persistentDataPath, name);

            return File.Exists(fileName);
        }

        public bool GamePropertiesExists()
        {
            var fileName = string.Format("{0}/properties.gam", Application.persistentDataPath);

            return File.Exists(fileName);
        }
    }
}
