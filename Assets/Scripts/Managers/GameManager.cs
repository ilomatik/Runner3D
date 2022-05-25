using System;
using Player;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region variables

        public static GameManager Instance;

        [SerializeField] private Player.Player player;
        [SerializeField] private PlayerAnimationController playerAnimationController;

        private bool isPlaying;

        #endregion
        
        public static Action<int> OnHealthValueChange;

        #region Unity Functions

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GameStartFunctions();
        }

        #endregion

        #region Custom Functions

        private void GameStartFunctions()
        {
            UIManager.OnUIStateChange?.Invoke(UIState.Upgrade);
            LevelManager.Instance.SetLevel();
            CameraManager.Instance.CameraStartFunctions();
            ScoreManager.Instance.LoadGoldValue();
            UpgradeManager.Instance.LoadUpgradeLevels();
            PoolManager.Instance.CreatePools();
        }

        public void SetIsPlaying(bool value)
        {
            isPlaying = value;
        }

        public void SetPlayer(Player.Player _player)
        {
            player = _player;
        }

        public void SetPlayerAnimationController(PlayerAnimationController _playerAnimationController)
        {
            playerAnimationController = _playerAnimationController;
        }

        public bool GetIsPlaying()
        {
            return isPlaying;
        }
        
        public Player.Player GetPlayer()
        {
            return player;
        }

        public PlayerAnimationController GetPlayerAnimationController()
        {
            return playerAnimationController;
        }

        #endregion
    }
}