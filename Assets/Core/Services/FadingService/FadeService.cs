using DG.Tweening;
using UnityEngine;

namespace Core.Services.Fade
{
    public class FadeService : IService
    {
        public void FadeCanvas(CanvasGroup canvas, float targetA, float duration)
        {
            canvas.DOFade(targetA, duration);
        }
        public void FadeCanvas(CanvasGroup canvas, float targetA, float duration,System.Action OnCompleteAction)
        {
            canvas.DOFade(targetA, duration).onComplete += new TweenCallback(OnCompleteAction);
        }
    }
}