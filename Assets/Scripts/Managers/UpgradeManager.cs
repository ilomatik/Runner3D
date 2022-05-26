using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class UpgradeManager : MonoBehaviour
    {
        #region Variables

        public static UpgradeManager Instance;

        [SerializeField] private List<Upgrade> upgrades;

        private int earningGoldUpgradeLevel;
        private int playerHealthUpgradeLevel;
        private const string EarningGoldUpgradeLevelKeyword = "earning_gold_upgrade_level";
        private const string PlayerHealthUpgradeLevelKeyword = "player_health_upgrade_level";

        #endregion

        public static Action<int> OnEarningGoldUpgradeLevelChange;

        #region Unity Functions

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Custom Functions

        public void IncreaseUpgradeLevel(UpgradeType upgradeType)
        {
            ScoreManager.Instance.SetGoldValue(-GetCurrentUpgradeLevelPrice(upgradeType));
            ScoreManager.Instance.SaveGoldValue();
            
            switch (upgradeType)
            {
                case UpgradeType.EarningGold:
                    earningGoldUpgradeLevel++;
                    OnEarningGoldUpgradeLevelChange?.Invoke(1);
                    break;
                case UpgradeType.PlayerHealth:
                    playerHealthUpgradeLevel++;
                    GameManager.Instance.GetPlayer().SetPlayerHealthValue(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null);
            }
            
            SaveUpgradeLevels();
        }

        public int GetCurrentUpgradeLevel(UpgradeType upgradeType)
        {
            var upgradeLevel = upgradeType switch
            {
                UpgradeType.EarningGold => earningGoldUpgradeLevel,
                UpgradeType.PlayerHealth => playerHealthUpgradeLevel,
                _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
            };

            return upgradeLevel;
        }

        public int GetCurrentUpgradeLevelPrice(UpgradeType upgradeType)
        {
            var upgradeLevelPrice = upgradeType switch
            {
                UpgradeType.EarningGold => earningGoldUpgradeLevel < GetCurrentUpgrade(upgradeType).price.Count
                    ? GetCurrentUpgrade(upgradeType).price[earningGoldUpgradeLevel - 1]
                    : GetCurrentUpgrade(upgradeType).price[GetCurrentUpgrade(upgradeType).price.Count - 1],
                UpgradeType.PlayerHealth => playerHealthUpgradeLevel < GetCurrentUpgrade(upgradeType).price.Count
                    ? GetCurrentUpgrade(upgradeType).price[playerHealthUpgradeLevel - 1]
                    : GetCurrentUpgrade(upgradeType).price[GetCurrentUpgrade(upgradeType).price.Count - 1],
                _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
            };

            return upgradeLevelPrice;
        }

        public bool GetUpgradeability(UpgradeType upgradeType, int goldValue)
        {
            var temp = upgradeType switch
            {
                UpgradeType.EarningGold => earningGoldUpgradeLevel < GetCurrentUpgrade(upgradeType).price.Count
                    ? earningGoldUpgradeLevel
                    : GetCurrentUpgrade(upgradeType).price.Count,
                UpgradeType.PlayerHealth => playerHealthUpgradeLevel < GetCurrentUpgrade(upgradeType).price.Count
                    ? playerHealthUpgradeLevel
                    : GetCurrentUpgrade(upgradeType).price.Count,
                _ => throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null)
            };

            var value = GetCurrentUpgrade(upgradeType).price[temp - 1] <= goldValue;
            
            Debug.Log($"GetUpgradeability is working goldValue : {goldValue}");
            Debug.Log($"GetUpgradeability is working temp : {temp}");
            Debug.Log($"GetUpgradeability is working value : {value}");

            return value;
        }

        public void SaveUpgradeLevels()
        {
            PlayerPrefs.SetInt(EarningGoldUpgradeLevelKeyword, earningGoldUpgradeLevel);
            PlayerPrefs.SetInt(PlayerHealthUpgradeLevelKeyword, playerHealthUpgradeLevel);
        }

        internal void LoadUpgradeLevels()
        {
            earningGoldUpgradeLevel = PlayerPrefs.HasKey(EarningGoldUpgradeLevelKeyword)
                ? PlayerPrefs.GetInt(EarningGoldUpgradeLevelKeyword)
                : 1;

            playerHealthUpgradeLevel = PlayerPrefs.HasKey(PlayerHealthUpgradeLevelKeyword)
                ? PlayerPrefs.GetInt(PlayerHealthUpgradeLevelKeyword)
                : 1;
            
            GameManager.Instance.GetPlayer().SetPlayerStartHealthValue(playerHealthUpgradeLevel - 1);
        }

        public Upgrade GetCurrentUpgrade(UpgradeType upgradeType)
        {
            var currentUpgrade = new Upgrade();

            foreach (var upgrade in upgrades.Where(upgrade => upgrade.upgradeType == upgradeType))
            {
                currentUpgrade = upgrade;
            }

            return currentUpgrade;
        }

        #endregion
    }

    [Serializable]
    public class Upgrade
    {
        public UpgradeType upgradeType;
        public List<int> price;
    }

    public enum UpgradeType
    {
        EarningGold,
        PlayerHealth
    }
}