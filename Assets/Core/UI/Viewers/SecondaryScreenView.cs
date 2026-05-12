using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.UI
{
    public class SecondaryScreenView : MonoBehaviour, IUIView, IButtonConnectable
    {
        [SerializeField]
        private Button CloseButton;
        [SerializeField]
        private GameObject PrimaryFrame;
        public void ConnectToButton(UnityAction action)
        {
            CloseButton.onClick.AddListener(action);
        }

        public void DissconnectFromButton(UnityAction action)
        {
            CloseButton.onClick.RemoveListener(action);
        }
        public void ToggleEnabled(bool enabled)
        {
            print(enabled);
            CloseButton.interactable = enabled;
            CloseButton.enabled = enabled;
            PrimaryFrame.SetActive(enabled);
        }
    }
}