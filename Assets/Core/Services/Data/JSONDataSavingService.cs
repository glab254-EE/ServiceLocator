using System.IO;
using UnityEngine;

namespace Core.Services.Data.JaSONy
{
    public class JSONDataSavingService : IService
    {
        public bool TrySave(object data, string FileName, string additionalPath = null)
        {
            try
            {
                if (data == null || FileName == null || FileName == "" || FileName == " ")
                {
                    return false;
                }
                string path = Path.Combine(Application.persistentDataPath, FileName + ".json");
                if (additionalPath != null)
                {
                    path = Path.Combine(Application.persistentDataPath, additionalPath, FileName + ".json");
                }

                string jsonData = JsonUtility.ToJson(data);

                File.WriteAllText(path, jsonData);
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
