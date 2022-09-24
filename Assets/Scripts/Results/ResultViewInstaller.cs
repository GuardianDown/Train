using System;
using UnityEngine;
using Train.UI;
using Object = UnityEngine.Object;
using Train.Bonuses;

namespace Train.Results
{
    public class ResultViewInstaller : IDisposable
    {
        private readonly IGameOver _gameOver;
        private readonly ResultView _resultViewPrefab;
        private readonly BonusesData _bonusesData;

        private ResultView _resultView;

        public ResultViewInstaller(IGameOver gameOver, ResultView resultViewPrefab, BonusesData bonusesData)
        {
            _gameOver = gameOver;
            _resultViewPrefab = resultViewPrefab;
            _bonusesData = bonusesData;

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe() => _gameOver.onGameOver += InstantiateResultView;

        private void Unsubscribe() => _gameOver.onGameOver -= InstantiateResultView;

        private void InstantiateResultView()
        {
            _resultView = Object.Instantiate(_resultViewPrefab);
            _resultView.AmountOfBonusesView.Construct(_bonusesData);
            _resultView.AmountOfBonusesView.UpdateView(_bonusesData._AmountOfBonuses);
        }
    }
}
