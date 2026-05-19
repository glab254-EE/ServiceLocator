using Core.Services.Data.JaSONy;
using Core.Services.Data.PlayerProfile;
using System;
using System.Collections;
using UnityEngine;
using Zenject;
namespace Core.Services.Score
{
    public class ScoreService: MonoBehaviour, IService
    {
        private PlayerProfileSavingService playerProfileSavingService;
         private JSONDataSavingService jsonDataSavingService;

        private PlayerProfileLoadingService playerProfileLoadingService;
        private JSONDataLoadingService jSONDataLoadingService;
        public event Action<double> OnScoreUpdated;
        public double CurrentScore { get; private set; } = 0;
        private double counterScore = 0;
        private Coroutine coroutine;
        void OnApplicationQuit()
        {
            if (coroutine != null) StopCoroutine(coroutine);
            SaveScore();
        }
        [Inject]
        void Initialize(PlayerProfileSavingService ps, JSONDataSavingService js, PlayerProfileLoadingService pl, JSONDataLoadingService jl)
        {
            playerProfileSavingService = ps;
            playerProfileLoadingService = pl;

            jSONDataLoadingService = jl;
            jsonDataSavingService = js;

            CurrentScore = 0;
            counterScore = 0;
            LoadScore();
            OnScoreUpdated?.Invoke(CurrentScore);
            coroutine = StartCoroutine(CounterEnumerator());
            print("Init");
        }
        public void OnCounterButtonPress()
        {
            CurrentScore += counterScore;
            counterScore = 0;
            OnScoreUpdated?.Invoke(CurrentScore);
        }
        public void SaveScore()
        {
            if (playerProfileSavingService != null)
            {
                playerProfileSavingService.TrySave((float)CurrentScore, "score");
            }

            if (jsonDataSavingService != null)
            {
                jsonDataSavingService.TrySave(CurrentScore, "SaveData");
            }
        }
        private void LoadScore()
        {
            bool Loaded = false;
            if (playerProfileLoadingService != null)
            {
                if (playerProfileLoadingService.TryGetData("score", out double res))
                {
                    CurrentScore = res;
                    Loaded = true;
                }
            }

            if (!Loaded && jSONDataLoadingService != null)
            {
                if (jSONDataLoadingService.TryGetData("SaveData", out double res))
                {
                    CurrentScore = res;
                    Loaded = true;
                }
            }
        }
        private IEnumerator CounterEnumerator()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                counterScore+=1;
            }
        }
    }
}
