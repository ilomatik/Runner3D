using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField] private TextMeshProUGUI upgradePriceText;
        [SerializeField] private TextMeshProUGUI upgradeLevelText;
        [SerializeField] private Animator buttonAnimation;
        private Button button;

        #region Unity Functions

        private void Start()
        {
            Invoke(nameof(StartFunctions), 0.01f);
            ScoreManager.OnGoldValueChange += SetButtonInteractable;
        }

        private void OnDestroy()
        {
            ScoreManager.OnGoldValueChange -= SetButtonInteractable;
        }

        #endregion

        #region Functions

        private void StartFunctions()
        {
            button = GetComponent<Button>();
            SetUpgradeLevelText();
            SetUpgradeLevelPriceText();
            SetButtonInteractable(ScoreManager.Instance.GetGoldValue());
        }

        public void UpgradeButtonTrigger()
        {
            UpgradeManager.Instance.IncreaseUpgradeLevel(upgradeType);
            SetUpgradeLevelText();
            SetUpgradeLevelPriceText();
        }

        private void SetButtonInteractable(int value)
        {
            var upgradeability = UpgradeManager.Instance.GetUpgradeability(upgradeType, value);
            button.interactable = upgradeability;
            
            if (upgradeability)
            {
                buttonAnimation.enabled = true;
                buttonAnimation.Play("UpgradeButton");
            }
            else
            {
                buttonAnimation.enabled = false;
            }
        }

        private void SetUpgradeLevelText()
        {
            if (upgradeLevelText == null) return;

            upgradeLevelText.text = "Level " + UpgradeManager.Instance.GetCurrentUpgradeLevel(upgradeType);
        }

        private void SetUpgradeLevelPriceText()
        {
            if (upgradePriceText == null) return;

            upgradePriceText.text = "$" + UpgradeManager.Instance.GetCurrentUpgradeLevelPrice(upgradeType);
        }

        #endregion
    }
}