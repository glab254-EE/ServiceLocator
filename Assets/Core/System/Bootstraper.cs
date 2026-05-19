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
        public WindowStateMachine stateMachine { get; private set; }
        void Awake()
        {
            SetUpUI();
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
