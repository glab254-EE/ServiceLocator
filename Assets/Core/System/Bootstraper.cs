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
            if (mainScreenView == null || secondaryScreenView == null) return;
            Dictionary<Type, AUIWindowState> dictionary = new();
            stateMachine = new();
            MainUIWindowState mainState = new(stateMachine, mainScreenView.GetComponent<IUIView>(), mainScreenView.GetComponent<IButtonConnectable>(), typeof(SecondaryScreenView));
            SecondaryUIWindowState secondState = new(stateMachine, mainScreenView.GetComponent<IUIView>(), mainScreenView.GetComponent<IButtonConnectable>(), typeof(MainScreenView));
            dictionary.Add(mainState.GetType(), mainState);
            dictionary.Add(secondState.GetType(), secondState);
            stateMachine.States = dictionary;
            stateMachine.SwitchState(mainState.GetType());
        }
    }
}
