using UnityEngine;

namespace Core.Services.Sounds
{
    public class SoundService : IService
    {
        private GameObject prefab;
        public void PlaySound(Vector2 position,AudioClip clip)
        {
            if (clip == null || prefab == null ) return;
            GameObject newObject = Behaviour.Instantiate(prefab,position,Quaternion.identity);
            if (newObject.TryGetComponent(out AudioSource source))
            {
                source.PlayOneShot(clip);
                Behaviour.Destroy(newObject,clip.length + 0.1f);
            } else
            {
                Behaviour.Destroy(newObject);
            }
        }
        public SoundService(GameObject _prefab)
        {
            prefab = _prefab;
        }
    }
}