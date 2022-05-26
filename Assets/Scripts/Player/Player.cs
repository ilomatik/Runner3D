using System;
using Collectables;
using Managers;
using Obstacles;
using UnityEngine;
using CameraType = Managers.CameraType;

namespace Player
{
    public class Player : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private int playerBaseHealthValue = 3;
        private int healthValue;

        private int HealthValue
        {
            get => healthValue;
            set
            {
                healthValue = value;
                GameManager.OnHealthValueChange?.Invoke(healthValue);
                
                if (healthValue != 0) return;
                
                GetComponent<PlayerAnimationController>().SetGameFinish();
                GameManager.Instance.SetIsPlaying(false);
                UIManager.Instance.SetLostGoldText(ScoreManager.Instance.GetCurrentLevelGoldValue());
                UIManager.OnUIStateChange?.Invoke(UIState.Lost);
            }
        }

        #endregion

        #region Unity Functions

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Collectable"))
            {
                var collectable = collision.transform.GetComponent<Collectable>();

                switch (collectable.GetCollectableType())
                {
                    case CollectableType.GoldItem:
                        ScoreManager.Instance.SetCurrentLevelGoldValue(collectable.GetIncreaseValue());
                        break;
                    case CollectableType.HealthItem:
                        IncreasePlayerHealthValue(collectable.GetIncreaseValue());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                collectable.DestroyCollectable();
            }
            else if (collision.transform.CompareTag("Obstacle")) 
            {
                DecreasePlayerHealthValue(collision.transform.GetComponent<Obstacle>().GetDecreaseValue());
                collision.transform.GetComponent<Obstacle>().DestroyObstacle();
            }
            else if (collision.transform.CompareTag("FinishLine")) 
            {
                Destroy(collision.gameObject);
                GetComponent<PlayerAnimationController>().SetGameFinish();
                CameraManager.Instance.TransitionTo(CameraType.LevelEnd);
                GameManager.Instance.SetIsPlaying(false);
                ScoreManager.Instance.SetGoldValue(ScoreManager.Instance.GetCurrentLevelGoldValue());
                ScoreManager.Instance.SaveGoldValue();
                UIManager.Instance.SetWonGoldText(ScoreManager.Instance.GetCurrentLevelGoldValue());
                UIManager.Instance.SetLevelEndTotalGoldText(ScoreManager.Instance.GetGoldValue());
                UIManager.OnUIStateChange?.Invoke(UIState.Won);
            }
        }

        #endregion

        #region Custom Functions

        public void SetPlayerHealthValue(int playerHealthValue = 0)
        {
            HealthValue += playerHealthValue;
        }

        public void SetPlayerStartHealthValue(int value = 0)
        {
            HealthValue = playerBaseHealthValue + value;
        }

        private void IncreasePlayerHealthValue(int healthIncreaseValue)
        {
            HealthValue += healthIncreaseValue;
        }

        private void DecreasePlayerHealthValue(int healthDecreaseValue)
        {
            HealthValue -= healthDecreaseValue;
        }

        public void ResetPlayerHealth()
        {
            SetPlayerStartHealthValue(UpgradeManager.Instance.GetCurrentUpgradeLevel(UpgradeType.PlayerHealth - 1));
        }

        #endregion
    }
}