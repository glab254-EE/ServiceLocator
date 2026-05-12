using Core.Services;
using Core.Services.Score;
using UnityEngine;

namespace Core.UI.Score
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private string Suffix;
        private TMPro.TMP_Text field;
        void Start () 
        {
            field = GetComponent<TMPro.TMP_Text>();
            if (ServiceLocator.TryGetService(out ScoreService scoreService))
            {
                scoreService.OnScoreUpdated += OnUpdate;
            }
        }
        void OnUpdate(double score)
        {
            field.text = Suffix+score.ToString();
        }
    }

}
