using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Projectiles
{
    public class ProjectileSpawningService
    {
        public List<ProjectileController> ActiveObjects = new();
        public Stack<ProjectileController> InactiveObjects = new();
        private ProjectileController.Factory _factory;
        public bool TrySpawnProjectile(Vector2 Position, Vector2 Velocity, out GameObject createdObject, LayerMask IncludedMask = default, float despawningTime = 4, AudioClip appearClip = null, AudioClip destroyedClip = null, GameObject targetObj = null)
        {
            createdObject = null;
            if (_factory == null) return false;
            ProjectileController controller = GetNewObject();
            if (controller == null) return false;
            controller.transform.position = Position;
            controller.SetUp(this, Position, Velocity, despawningTime, IncludedMask,appearClip,destroyedClip,targetObj);
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
            if (!InactiveObjects.TryPop(out ProjectileController output))
            {
                output = _factory.Create();
            }
            return output;
        }
        [Inject]
        private void Construct(ProjectileController.Factory factory)
        {
            _factory = factory;
            for (int i = 0; i < 4; i++)
            {
                ProjectileController newI = _factory.Create();
                GameObject newO = newI.gameObject;
                newO.SetActive(false);
                InactiveObjects.Push(newO.GetComponent<ProjectileController>());
            }
        }
    }
}
