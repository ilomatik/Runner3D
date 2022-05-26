using System;
using Data;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Variables

        public static ScoreManager Instance;

        [SerializeField] private GoldData goldData;
        private int currentLevelGoldValue;
        private int goldValue;
        private readonly int maxGoldValue = Int32.MaxValue;

        private int GoldValue
        {
            get => goldValue;
            set
            {
                goldValue = value;
                OnGoldValueChange?.Invoke(goldValue);

                if (goldValue > maxGoldValue)
                {
                    goldValue = maxGoldValue;
                }
            }
        }

        #endregion

        public static Action<int> OnGoldValueChange; 

        #region Unity Functions

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Custom Functions

        public void SetGoldValue(int value)
        {
            GoldValue += value;
            OnGoldValueChange?.Invoke(GoldValue);
        }

        public int GetGoldValue()
        {
            return goldData.GetCurrentGoldValue();
        }

        public void SetCurrentLevelGoldValue(int value)
        {
            currentLevelGoldValue += value;
            OnGoldValueChange?.Invoke(GoldValue + currentLevelGoldValue);
        }

        public int GetCurrentLevelGoldValue()
        {
            return currentLevelGoldValue;
        }

        public void ResetCurrentLevelGoldValue()
        {
            currentLevelGoldValue = 0;
        }

        public void SaveGoldValue()
        {
            goldData.SetGoldValue(GoldValue);
        }

        internal void LoadGoldValue()
        {
            GoldValue = goldData.GetCurrentGoldValue();
        }

        // For test
        [ContextMenu("Add 1000 Gold")]
        public void AddGold()
        {
            GoldValue += 1000;
        }

        #endregion
    }
}