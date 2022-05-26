using Managers;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int decreaseValue;

        #endregion

        #region CustomFunctions

        public int GetDecreaseValue()
        {
            return decreaseValue;
        }

        public void DestroyObstacle()
        {
            var particleObject = PoolManager.Instance.GetPoolObject(PoolType.ObstacleParticle);
            particleObject.transform.position = transform.position;
            particleObject.SetActive(true);
            Destroy(gameObject);
        }

        #endregion
    }
}