using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Gold Data", menuName = "Gold Data")]
    public class GoldData : ScriptableObject
    {
        #region Variables
        
        private int currentGoldValue;
        private const string GoldDataKeyword = "current_gold_value";

        #endregion

        #region Custom Functions
        
        public int GetCurrentGoldValue()
        {
            LoadGoldValue();
            return currentGoldValue;
        }

        public void SetGoldValue(int value)
        {
            currentGoldValue = value;
            SaveGoldValue();
        }

        private void SaveGoldValue()
        {
            PlayerPrefs.SetInt(GoldDataKeyword, currentGoldValue);
        }

        private void LoadGoldValue()
        {
            currentGoldValue = PlayerPrefs.HasKey(GoldDataKeyword) ? PlayerPrefs.GetInt(GoldDataKeyword) : 0;
        }

        #endregion
    }
}