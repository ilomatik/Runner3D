using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Managers
{
    public enum UIState
    {
        Upgrade,
        Game,
        Won,
        Lost
    }

    [Serializable]
    public class UIObject
    {
        public UIState uiState;
        public GameObject uiObject;

        public void SetActiveUiObject(bool value)
        {
            uiObject.SetActive(value);
        }
    }
    
    public class UIManager : MonoBehaviour
    {
        #region Variables
        
        public static UIManager Instance;
        
        [SerializeField] private TextMeshProUGUI goldTextUpgrade;
        [SerializeField] private TextMeshProUGUI goldTextGame;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI levelTextUpgrade;
        [SerializeField] private TextMeshProUGUI levelTextGame;
        [SerializeField] private TextMeshProUGUI lostGoldText;
        [SerializeField] private TextMeshProUGUI wonGoldText;
        [SerializeField] private TextMeshProUGUI levelEndTotalGoldText;
        [SerializeField] private List<UIObject> uiObjects;

        #endregion

        public static Action<UIState> OnUIStateChange;

        #region Unity Functions

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            OnUIStateChange += SetUIState;
            GameManager.OnHealthValueChange += SetHealthText;
        }

        private void OnDestroy()
        {
            OnUIStateChange -= SetUIState;
            GameManager.OnHealthValueChange -= SetHealthText;
        }

        #endregion

        #region Custom Functions

        private void SetUIState(UIState uiState)
        {
            foreach (var uiObject in uiObjects)
            {
                uiObject.SetActiveUiObject(uiObject.uiState == uiState);
            }
        }

        public void SetLevelText(int value)
        {
            if (levelTextUpgrade != null)
            {
                levelTextUpgrade.text = "Level " + value;
            }

            if (levelTextGame != null)
            {
                levelTextGame.text = "Level " + value;
            }
        }

        private void SetHealthText(int value)
        {
            if (healthText == null) return;
                
            healthText.text = value.ToString();
        }

        public void SetLostGoldText(int lostGold)
        {
            lostGoldText.text = "You Lost " + lostGold + " Gold";
        }

        public void SetWonGoldText(int wonGold)
        {
            wonGoldText.text = wonGold.ToString();
        }

        public void SetLevelEndTotalGoldText(int totalGoldValue)
        {
            levelEndTotalGoldText.text = "Total Gold " + totalGoldValue;
        }
        
        public void SetGoldText(string text)
        {
            if (goldTextUpgrade != null) goldTextUpgrade.text = text;
            if (goldTextGame != null) goldTextGame.text = text;
        }

        public void PlayButton()
        {
            OnUIStateChange?.Invoke(UIState.Game);
            GameManager.Instance.SetIsPlaying(true);
            GameManager.Instance.GetPlayerAnimationController().SetPlayerRun();
        }

        public void RetryButton()
        {
            OnUIStateChange?.Invoke(UIState.Upgrade);
            LevelManager.Instance.SetLevel();
        }

        public void NextButton()
        {
            OnUIStateChange?.Invoke(UIState.Upgrade);
            LevelManager.Instance.IncreaseLevel();
            LevelManager.Instance.SetLevel();
        }

        #endregion
    }
}