using Core.Services.Data.JaSONy;
using Core.Services.Data.PlayerProfile;
using System;
using System.Collections;
using UnityEngine;
namespace Core.Services.Score
{
    public class ScoreService: MonoBehaviour, IService
    {
        public event Action<double> OnScoreUpdated;
        public double CurrentScore { get; private set; } = 0;
        private double counterScore = 0;
        private Coroutine coroutine;
        void OnApplicationQuit()
        {
            StopCoroutine(coroutine);
            SaveScore();
        }
        public void OnCounterButtonPress()
        {
            CurrentScore += counterScore;
            counterScore = 0;
            OnScoreUpdated?.Invoke(CurrentScore);
        }
        public void SaveScore()
        {
            if (ServiceLocator.TryGetService(out PlayerProfileSavingService firstService))
            {
                firstService.TrySave((float)CurrentScore, "score");
            }

            if (ServiceLocator.TryGetService(out JSONDataSavingService jsonService))
            {
                jsonService.TrySave(CurrentScore, "SaveData");
            }
        }
        public void Initialize()
        {

            CurrentScore = 0;
            counterScore = 0;
            LoadScore();
            OnScoreUpdated?.Invoke(CurrentScore);
            coroutine = StartCoroutine(CounterEnumerator());
        }
        private void LoadScore()
        {
            bool Loaded = false;
            if (ServiceLocator.TryGetService(out PlayerProfileLoadingService firstService))
            {
                if (firstService.TryGetData("score",out double res))
                {
                    CurrentScore = res;
                    Loaded = true;
                }
            }

            if (!Loaded && ServiceLocator.TryGetService(out JSONDataLoadingService jsonService))
            {
                if (jsonService.TryGetData("SaveData",out double res))
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
