using Levels;
using UnityEngine;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        #region Variables

        public static SpawnManager Instance;

        private Level spawnedLevel;

        #endregion

        #region Unity Functions

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Custom Functions

        public void SpawnCurrentLevel(Level level)
        {
            if (spawnedLevel != null)
            {
                Destroy(spawnedLevel.gameObject);
            }

            spawnedLevel = Instantiate(level, Vector3.zero, Quaternion.identity);
            LevelManager.Instance.SetPlayerTransform(spawnedLevel.GetPlayer().transform);
            GameManager.Instance.SetPlayer(spawnedLevel.GetPlayer());
            GameManager.Instance.SetPlayerAnimationController(spawnedLevel.GetPlayerAnimationController());
            CameraManager.Instance.TransitionTo(CameraType.Game);
            CameraManager.Instance.SetCameraFollowTransform(GameManager.Instance.GetPlayer().transform);
        }

        #endregion
    }
}