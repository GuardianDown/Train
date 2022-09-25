using System.Collections;
using System.Collections.Generic;
using Train.Bonuses;
using UnityEngine;

namespace Train.Results
{
    public class GameSaver : ISaver
    {
        private readonly IEnumerable<ISaveData> _saveData;

        public GameSaver(IEnumerable<ISaveData> saved) => _saveData = saved;

        public void Save()
        {
            foreach(ISaveData save in _saveData)
            {
                save.Save();
            }

            PlayerPrefs.Save();
        }
    }

    public interface ISaver
    {
        void Save();
    }
}
