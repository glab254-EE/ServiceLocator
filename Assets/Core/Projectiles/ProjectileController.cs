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
        private ProjectileSpawningService source;
        private Vector2 OriginPoint = Vector2.zero;
        private AudioSource Asource;
        private AudioClip destroyClip;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Asource = GetComponent<AudioSource>();
        }
        void FixedUpdate()
        {
            if (!gameObject.activeInHierarchy) return;
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
        public void SetUp(ProjectileSpawningService origin, Vector2 from, Vector2 _velocity, float timeUntilDespawn, LayerMask IncludedMask, AudioClip appearClip = null, AudioClip destroyedClip = null)
        {
            source = origin;
            deathTimer = timeUntilDespawn;
            velocity = _velocity;
            gameObject.SetActive(true);
            rb = GetComponent<Rigidbody2D>();
            rb.includeLayers = IncludedMask;
            OriginPoint = from;
            if (Asource != null)
            {
                if (appearClip != null) Asource.PlayOneShot(appearClip);
                if (destroyedClip != null) destroyClip = destroyedClip;
            }
        }
        public void Dissable()
        {
            source.ReturnObject(this);
        }
        public void TearDown()
        {
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
                if (Asource != null && destroyClip != null)
                {
                    Asource.PlayOneShot(destroyClip);
                }
                Dissable();
            }
        }
    }
}