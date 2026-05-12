using UnityEngine;

namespace Core.Services.Data.PlayerProfile
{
    public class PlayerProfileLoadingService : IService
    {
        public bool TryGetData(string key, out double result)
        {
            result = 0;
            try
            {
                if (key == null) return false;
                result = PlayerPrefs.GetFloat(key);
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
