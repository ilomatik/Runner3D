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
            PoolManager.Instance.GetPoolObject(PoolType.ObstacleParticle);
            Destroy(gameObject);
        }

        #endregion
    }
}