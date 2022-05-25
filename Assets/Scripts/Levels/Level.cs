using Player;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Player.Player player;
        [SerializeField] private PlayerAnimationController playerAnimationController;

        public Player.Player GetPlayer()
        {
            return player;
        }

        public PlayerAnimationController GetPlayerAnimationController()
        {
            return playerAnimationController;
        }
    }
}