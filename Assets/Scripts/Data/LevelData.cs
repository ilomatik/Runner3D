using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Level Data")]
    public class LevelData : ScriptableObject
    {
        #region Variables
        
        private int currentLevel;
        private const string LevelDataKeyword = "current_level_value";

        #endregion

        #region Custom Functions
        
        public int GetCurrentLevel()
        {
            LoadLevelValue();
            return currentLevel;
        }

        public void IncreaseLevel()
        {
            currentLevel++;
            SaveLevelValue();
        }

        private void SaveLevelValue()
        {
            PlayerPrefs.SetInt(LevelDataKeyword, currentLevel);
        }

        private void LoadLevelValue()
        {
            currentLevel = PlayerPrefs.HasKey(LevelDataKeyword) ? PlayerPrefs.GetInt(LevelDataKeyword) : 1;
        }

        #endregion
    }
}