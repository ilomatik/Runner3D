using System.Collections.Generic;
using Data;
using Levels;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Variables

        public static LevelManager Instance;

        [SerializeField] private LevelData levelData;
        [SerializeField] private List<Level> levels;
        private Transform playerTransform;

        #endregion

        #region Unity Functions

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Custom Functions

        public void SetLevel()
        {
            var currentLevel = levelData.GetCurrentLevel() <= levels.Count - 1
                ? levels[levelData.GetCurrentLevel() - 1]
                : levels[levels.Count - 1];

            SpawnManager.Instance.SpawnCurrentLevel(currentLevel, levelData.GetCurrentLevel());
        }

        public void IncreaseLevel()
        {
            levelData.IncreaseLevel();
        }

        public void SetPlayerTransform(Transform _playerTransform)
        {
            playerTransform = _playerTransform;
        }

        public int GetCurrentLevel()
        {
            return levelData.GetCurrentLevel();
        }

        public Transform GetPlayerTransform()
        {
            return playerTransform;
        }

        #endregion
    }
}