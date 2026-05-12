using System.IO;
using UnityEngine;

namespace Core.Services.Data.JaSONy
{
    public class JSONDataLoadingService : IService
    {
        public bool TryGetData<T>(string fileName,out T result)
        {
            result = default(T);
            try
            {
                if (fileName == null || fileName == " ") return false;
                string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
                if (!File.Exists(path)) return false;
                string data = File.ReadAllText(path);
                result = JsonUtility.FromJson<T>(data);
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
            return false;
        }
    }
}
