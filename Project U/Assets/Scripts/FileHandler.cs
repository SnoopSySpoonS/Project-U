using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Project_U.Assets.Scripts
{
    public class FileHandler
    {
        public static void Save<T>(T obj, string name) where T: Saveable
        {
            string objJson = JsonUtility.ToJson(obj);
            string filePath = obj.GetSaveLocation();
            FileInfo fi = new FileInfo(filePath);
            if (fi.Directory.Exists)
            {
                File.WriteAllText (filePath, objJson);
            }
            else 
            {
                throw new DirectoryNotFoundException();
            }
        }
        
        public static T Load<T>(string filePath)
        {
            string objJson = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(objJson);
        }
    }

    public interface Saveable
    {
        string GetSaveLocation();
    }
}
