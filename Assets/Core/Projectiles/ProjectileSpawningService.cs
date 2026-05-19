using System.Collections.Generic;
using UnityEngine;

namespace Core.Projectiles
{
    public class ProjectileSpawningService
    {
        public List<ProjectileController> ActiveObjects = new();
        public Stack<ProjectileController> InactiveObjects = new();
        private GameObject prefab;
        public bool TrySpawnProjectile(Vector2 Position, Vector2 Velocity, out GameObject createdObject, LayerMask IncludedMask = default, float despawningTime = 4, AudioClip appearClip = null, AudioClip destroyedClip = null)
        {
            createdObject = null;
            if (prefab == null) return false;
            ProjectileController controller = GetNewObject();
            if (controller == null) return false;
            controller.transform.position = Position;
            controller.SetUp(this, Position, Velocity, despawningTime, IncludedMask,appearClip,destroyedClip);
            createdObject = controller.gameObject;
            ActiveObjects.Add(controller);
            return true;
        }
        public void ReturnObject(ProjectileController objec)
        {
            if (ActiveObjects.Contains(objec))
            {
                ActiveObjects.Remove(objec);
                objec.TearDown();
                InactiveObjects.Push(objec);
            }
        }
        private ProjectileController GetNewObject()
        {
            ProjectileController output = null;
            if (!InactiveObjects.TryPop(out output))
            {
                output = Behaviour.Instantiate(prefab).GetComponent<ProjectileController>();
            }
            return output;
        }
        public ProjectileSpawningService(GameObject _prefab, int startingCount = 4)
        {
            prefab = _prefab;

            for (int i = 0; i < startingCount; i++)
            {
                GameObject newO = Behaviour.Instantiate(prefab);
                newO.SetActive(false);
                InactiveObjects.Push(newO.GetComponent<ProjectileController>());
            }
        }
    }
}
