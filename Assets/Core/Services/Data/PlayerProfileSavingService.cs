using UnityEngine;

namespace Core.Services.Data.PlayerProfile
{
    public class PlayerProfileSavingService : IService
    {
        public bool TrySave(float data, string key)
        {
            try
            {
                if (key == null)
                {
                    return false;
                }
                PlayerPrefs.SetFloat(key, data);
                PlayerPrefs.Save();
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
            return false;
        }
        public bool TrySave(object data, string key)
        {
            try
            {
                if (key == null || data == null)
                {
                    return false;
                }
                string jsonData = JsonUtility.ToJson(data);

                PlayerPrefs.SetString(key, jsonData);
                PlayerPrefs.Save();
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
