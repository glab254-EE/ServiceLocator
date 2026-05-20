using Core.Services.Sounds;
using UnityEngine;
using Zenject;

namespace Core.Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector2 velocity;
        private float deathTimer;
        private float rotationSpeed = 1f;
        private float speed;
        private ProjectileSpawningService source;
        private Vector2 OriginPoint = Vector2.zero;
        private AudioClip destroyClip;
        private Transform targetTransform = null;
        private Rigidbody2D targetRB = null;
        [Inject]private SoundService soundService;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void FixedUpdate()
        {
            if (!gameObject.activeInHierarchy) return;
            if (targetTransform != null)
            {
                Vector2 targetPos = (Vector2)targetTransform.position;
                if (targetRB != null)
                {
                    targetPos += targetRB.linearVelocity;
                }
                Vector2 directionNormal = (targetPos-(Vector2)transform.position).normalized;
                velocity = Vector2.Lerp(velocity,directionNormal*speed,Time.deltaTime*rotationSpeed);
            }
            if (rb != null)
            {
                rb.linearVelocity = velocity;
            }
            if (deathTimer > 0)
            {
                deathTimer -= Time.deltaTime;
            }
            else
            {
                Dissable();
            }
        }
        public void SetUp(ProjectileSpawningService origin, Vector2 from, Vector2 _velocity, float timeUntilDespawn, LayerMask IncludedMask, AudioClip appearClip = null, AudioClip destroyedClip = null, GameObject targetObj = null)
        {
            speed = _velocity.magnitude;
            source = origin;
            deathTimer = timeUntilDespawn;
            velocity = _velocity;
            gameObject.SetActive(true);
            rb = GetComponent<Rigidbody2D>();
            rb.includeLayers = IncludedMask;
            OriginPoint = from;
            if (soundService != null)
            {
                if (appearClip != null) soundService.PlaySound(transform.position,appearClip);
                if (destroyedClip != null) destroyClip = destroyedClip;
            }
            if (targetObj != null )
            {
                targetTransform = targetObj.transform;
                targetObj.TryGetComponent(out targetRB);
            }
        }
        public void Dissable()
        {
            targetTransform = null;
            targetRB = null;
            source.ReturnObject(this);
        }
        public void TearDown()
        {
            targetTransform = null;
            targetRB = null;
            gameObject.SetActive(false);
            velocity = Vector2.zero;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && collision.gameObject != null && collision.gameObject.TryGetComponent(out IHittable hittable))
            {
                Vector2 HitPos = collision.ClosestPoint(transform.position);
                Vector2 InputPosition = HitPos - (OriginPoint- HitPos).normalized * 0.01f;
                hittable.OnHit(InputPosition);
            }
            if (gameObject.activeInHierarchy)
            {
                if (soundService != null && destroyClip != null)
                {
                    soundService.PlaySound(transform.position,destroyClip);
                }
                Dissable();
            }
        }
        public class Factory : PlaceholderFactory<ProjectileController>
        {
            
        }
    }
}