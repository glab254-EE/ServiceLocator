using Core.Services;
using Core.Services.Fade;
using Core.Services.Sounds;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

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
        [Inject] FadeService FadeService;
        [Inject] TwoStateSoundPlayer SoundService;
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
            if (PrimaryFrameCanvas != null && FadeService != null)
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
                FadeService.FadeCanvas(PrimaryFrameCanvas, targetA, targetTime, () =>
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
            if (SoundService == null) return;
            if (state == true)
            {
                SoundService.PlayOpenSound();
            }
            else
            {
                SoundService.PlayCloseSound();
            }
        }
    }
}