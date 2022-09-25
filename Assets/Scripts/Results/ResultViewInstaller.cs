using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Train.Bonuses;
using Train.GameOver;

namespace Train.Results
{
    public class ResultViewInstaller : IDisposable
    {
        private readonly IGameOver _gameOver;
        private readonly ResultView _resultViewPrefab;
        private readonly BonusesData _bonusesData;
        private readonly Canvas _canvas;

        private ResultView _resultView;

        public ResultViewInstaller(IGameOver gameOver, ResultView resultViewPrefab, BonusesData bonusesData, Canvas canvas)
        {
            _gameOver = gameOver;
            _resultViewPrefab = resultViewPrefab;
            _bonusesData = bonusesData;
            _canvas = canvas;

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe() => _gameOver.onGameOver += InstantiateResultView;

        private void Unsubscribe() => _gameOver.onGameOver -= InstantiateResultView;

        private void InstantiateResultView()
        {
            _resultView = Object.Instantiate(_resultViewPrefab);
            _resultView.AmountOfBonusesView.Construct(_bonusesData);
            _resultView.AmountOfBonusesView.UpdateView(_bonusesData.AmountOfBonuses);
            _canvas.gameObject.SetActive(false);
        }
    }
}
