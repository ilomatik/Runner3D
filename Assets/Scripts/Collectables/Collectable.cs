using Managers;
using UnityEngine;

namespace Collectables
{
    public enum CollectableType
    {
        GoldItem,
        HealthItem
    }
    
    public class Collectable : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private CollectableType collectableType;
        [SerializeField] private int increaseValue;

        #endregion

        #region Unity Functions

        private void Start()
        {
            UpgradeManager.OnEarningGoldUpgradeLevelChange += SetIncreaseValue;
        }

        private void OnDestroy()
        {
            UpgradeManager.OnEarningGoldUpgradeLevelChange -= SetIncreaseValue;
        }

        #endregion

        #region Custom Functions

        public CollectableType GetCollectableType()
        {
            return collectableType;
        }

        private void SetIncreaseValue(int value = 0)
        {
            increaseValue += value;
        }

        public int GetIncreaseValue()
        {
            return increaseValue;
        }

        public void DestroyCollectable()
        {
            var particleObject = PoolManager.Instance.GetPoolObject(PoolType.CollectableParticle);
            particleObject.transform.position = transform.position;
            particleObject.SetActive(true);
            Destroy(gameObject);
        }

        #endregion
    }
}