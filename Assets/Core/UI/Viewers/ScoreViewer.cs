using Core.Services;
using Core.Services.Score;
using UnityEngine;
using Zenject;

namespace Core.UI.Score
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private string Suffix;
        private ScoreService scoreService;
        private TMPro.TMP_Text field;
        void Start () 
        {
            field = GetComponent<TMPro.TMP_Text>();
            if (scoreService != null)
            {
                scoreService.OnScoreUpdated += OnUpdate;
            }
        }
        void OnUpdate(double score)
        {
            field.text = Suffix+score.ToString();
        }
        [Inject]
        void Initialize(ScoreService _scores)
        {
            scoreService = _scores;
        }
    }

}
