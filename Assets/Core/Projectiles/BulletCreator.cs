using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Projectiles
{
    public class BulletCreator : MonoBehaviour
    {
        [SerializeField]
        private Vector2 velocity = Vector2.right;
        [SerializeField]
        private LayerMask layerMask;
        [SerializeField]
        private float despawnTime = 4;
        [SerializeField]
        private AudioClip appearClip;
        [SerializeField]
        private AudioClip destroyedClip;
        [SerializeField]
        private GameObject target;
        [Inject]
        private ProjectileSpawningService service;
        private InputSystem_Actions ia;
        private Camera ca;
        void Start()
        {
            ca = Camera.main;
            ia ??= new();
            ia.Player.Attack.performed += OnClick;
            ia.Enable();
        }
        private void OnDestroy()
        {
            ia.Player.Attack.performed -= OnClick;
            ia.Disable();
        }
        void OnClick(InputAction.CallbackContext _)
        {
            Vector2 pos = ca.ScreenToWorldPoint(Mouse.current.position.value);
            if (pos != null && service != null)
            {
                service.TrySpawnProjectile(pos, velocity, out GameObject _, layerMask, despawnTime, appearClip, destroyedClip, target);
            }
        }
    }
}