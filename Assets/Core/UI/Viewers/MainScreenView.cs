using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.UI
{
    public class MainScreenView : MonoBehaviour, IUIView, IButtonConnectable
    {
        [SerializeField]
        private Button OpenButton;
        public void ConnectToButton(UnityAction action)
        {
            OpenButton.onClick.AddListener(action);
        }

        public void DissconnectFromButton(UnityAction action)
        {
            OpenButton.onClick.RemoveListener(action);
        }

        public void ToggleEnabled(bool enabled)
        {
            print(enabled);
            OpenButton.interactable = enabled;
            OpenButton.enabled = enabled;
        }
    }
}