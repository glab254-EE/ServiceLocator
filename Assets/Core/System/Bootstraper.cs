using Core.Services;
using Core.Services.Data.JaSONy;
using Core.Services.Data.PlayerProfile;
using Core.Services.Fade;
using Core.Services.Score;
using Core.Services.Sounds;
using Core.UI;
using Core.UI.States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField] private MainScreenView mainScreenView;
        [SerializeField] private SecondaryScreenView secondaryScreenView;
        [Header("Services Settings")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip openClip;
        [SerializeField] private AudioClip closeClip;
        [SerializeField] private ScoreService scoreServiceReference;
        public WindowStateMachine stateMachine { get; private set; }
        void Awake()
        {
            SetUpServices();
            SetUpUI();
        }
        void SetUpServices()
        {
            #region VISUAL services
            FadeService fadeService = new FadeService();
            ServiceLocator.TryAddService(fadeService);

            TwoStateSoundPlayer player = new(audioSource, openClip, closeClip);
            ServiceLocator.TryAddService(player);
            #endregion
            #region DATA services
            ServiceLocator.TryAddService(scoreServiceReference);

            JSONDataSavingService jSONDataSavingService = new JSONDataSavingService();
            JSONDataLoadingService jSONDataLoadingService = new JSONDataLoadingService();

            ServiceLocator.TryAddService(jSONDataSavingService);
            ServiceLocator.TryAddService(jSONDataLoadingService);

            PlayerProfileSavingService playerProfileSavingService = new PlayerProfileSavingService();
            PlayerProfileLoadingService playerProfileLoadingService = new PlayerProfileLoadingService();

            ServiceLocator.TryAddService(playerProfileSavingService);
            ServiceLocator.TryAddService(playerProfileLoadingService);

            scoreServiceReference.Initialize();
            #endregion
        }
        void SetUpUI()
        {
            if (mainScreenView == null || secondaryScreenView == null) return;
            Dictionary<Type, AUIWindowState> dictionary = new();
            stateMachine = new();
            MainUIWindowState mainState = new MainUIWindowState(stateMachine, mainScreenView.GetComponent<IUIView>(), mainScreenView.GetComponent<IButtonConnectable>(), typeof(SecondaryScreenView));
            SecondaryUIWindowState secondState = new SecondaryUIWindowState(stateMachine, secondaryScreenView.GetComponent<IUIView>(), secondaryScreenView.GetComponent<IButtonConnectable>(), typeof(MainUIWindowState));
            dictionary.Add(typeof(MainUIWindowState), mainState);
            dictionary.Add(typeof(SecondaryScreenView), secondState);
            stateMachine.States = dictionary;
            stateMachine.SwitchState(typeof(MainUIWindowState));
        }
    }
}
