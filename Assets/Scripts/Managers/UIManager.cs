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
        
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI levelText;
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
            if (levelText == null) return;

            levelText.text = "Level " + value;
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
            wonGoldText.text = "You Won " + wonGold + " Gold";
        }

        public void SetLevelEndTotalGoldText(int totalGoldValue)
        {
            levelEndTotalGoldText.text = "You Have Total " + totalGoldValue + " Gold";
        }
        
        public void SetGoldText(string text)
        {
            if (goldText == null) return;
            
            goldText.text = text;
        }

        public void PlayButton()
        {
            OnUIStateChange?.Invoke(UIState.Game);
            GameManager.Instance.GetPlayerAnimationController().SetPlayerRun();
        }

        public void RetryButton()
        {
            LevelManager.Instance.SetLevel();
        }

        public void NextButton()
        {
            LevelManager.Instance.IncreaseLevel();
            LevelManager.Instance.SetLevel();
        }

        #endregion
    }
}