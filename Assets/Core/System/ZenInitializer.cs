using Core.Projectiles;
using Core.Services.Data.JaSONy;
using Core.Services.Data.PlayerProfile;
using Core.Services.Fade;
using Core.Services.Score;
using Core.Services.Sounds;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Core
{
    public class ZenInitializer : MonoInstaller
    {
        [Header("Services Settings")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip openClip;
        [SerializeField] private AudioClip closeClip;
        [SerializeField] private ScoreService scoreServiceReference;
        [Header("Projectiles Set-up")]
        [SerializeField] private GameObject projectilesPrefab;
        [SerializeField] private float projectileDeathTime;
        public override void InstallBindings()
        {
            Container.Bind<FadeService>().AsSingle();
            if (audioSource != null)
            {
                TwoStateSoundPlayer player = new(audioSource, openClip, closeClip);
                Container.Bind<TwoStateSoundPlayer>().FromInstance(player).AsSingle();
                Container.Bind<JSONDataSavingService>().AsSingle();
                Container.Bind<JSONDataLoadingService>().AsSingle();
            }

            Container.Bind<PlayerProfileSavingService>().AsSingle();
            Container.Bind<PlayerProfileLoadingService>().AsSingle();
            if (scoreServiceReference != null) Container.BindInstance(scoreServiceReference).AsSingle().NonLazy();

            if (projectilesPrefab != null)
            {
                ProjectileSpawningService service = new ProjectileSpawningService(projectilesPrefab);
                Container.Bind<ProjectileSpawningService>().FromInstance(service).AsTransient().NonLazy();
            }
        }
    }
}