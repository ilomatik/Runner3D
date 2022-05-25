using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Variables

        private Animator playerAnimator;
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsGameFinish = Animator.StringToHash("IsGameFinish");

        #endregion

        #region Unity Functions

        private void Awake()
        {
            playerAnimator = GetComponent<Animator>();
        }

        #endregion

        #region Custom Functions

        [ContextMenu("SetPlayerRun")]
        public void SetPlayerRun()
        {
            playerAnimator.SetBool(IsRunning, true);
            playerAnimator.SetBool(IsGameFinish, false);
        }
        
        [ContextMenu("SetGameFinish")]
        public void SetGameFinish()
        {
            playerAnimator.SetBool(IsRunning, false);
            playerAnimator.SetBool(IsGameFinish, true);
        }

        #endregion
    }
}