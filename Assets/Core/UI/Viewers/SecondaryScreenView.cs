using Core.Services;
using Core.Services.Fade;
using Core.Services.Sounds;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.UI
{
    public class SecondaryScreenView : MonoBehaviour, IUIView, IButtonConnectable
    {
        [SerializeField]
        private float FadeDurationAppear;
        [SerializeField]
        private float FadeDurationDissappear;
        [SerializeField]
        private Button CloseButton;
        [SerializeField]
        private GameObject PrimaryFrame;
        [SerializeField]
        private CanvasGroup PrimaryFrameCanvas;
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
            PlaySound(enabled);
            if (PrimaryFrameCanvas != null && ServiceLocator.TryGetService(out FadeService service))
            {
                float targetA = enabled ? 1 : 0;
                float targetTime = enabled ? FadeDurationAppear : FadeDurationDissappear;
                if (enabled == true)
                {
                    PrimaryFrameCanvas.alpha = 0;
                    PrimaryFrame.SetActive(true);
                } else
                {
                    CloseButton.interactable = false;
                }
                service.FadeCanvas(PrimaryFrameCanvas, targetA, targetTime, () =>
                {
                    PrimaryFrame.SetActive(enabled);
                    CloseButton.interactable = enabled;
                });
            } else
            {
                CloseButton.interactable = enabled;
                PrimaryFrame.SetActive(enabled);
            }
        }
        private void PlaySound(bool state)
        {
            if (!ServiceLocator.TryGetService(out TwoStateSoundPlayer player)) return;
            if (state == true)
            {
                player.PlayOpenSound();
            }
            else
            {
                player.PlayCloseSound();
            }
        }
    }
}