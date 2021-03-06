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

        public void SpawnCurrentLevel(Level level, int currentLevel)
        {
            if (spawnedLevel != null)
            {
                Destroy(spawnedLevel.gameObject);
            }

            spawnedLevel = Instantiate(level, Vector3.zero, Quaternion.identity);
            UIManager.Instance.SetLevelText(currentLevel);
            ScoreManager.Instance.ResetCurrentLevelGoldValue();
            ScoreManager.OnGoldValueChange?.Invoke(ScoreManager.Instance.GetGoldValue());
            LevelManager.Instance.SetPlayerTransform(spawnedLevel.GetPlayer().transform);
            GameManager.Instance.SetPlayer(spawnedLevel.GetPlayer());
            GameManager.Instance.SetPlayerAnimationController(spawnedLevel.GetPlayerAnimationController());
            GameManager.Instance.GetPlayer().ResetPlayerHealth();
            CameraManager.Instance.TransitionTo(CameraType.Game);
            CameraManager.Instance.SetCameraFollowTransform(GameManager.Instance.GetPlayer().transform);
        }

        #endregion
    }
}