using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float playerXMovementBoundary;
        [SerializeField] private float horizontalMovement;
        [SerializeField] private float turnDuration;

        private float moveHorizontal;
        private Quaternion playerLeftMoveRotation;
        private Quaternion playerRightMoveRotation;
        private Quaternion playerForwardMoveRotation;

        #endregion

        #region Unity Functions

        private void Start()
        {
            playerLeftMoveRotation = Quaternion.Euler(0f, -45f, 0f);
            playerRightMoveRotation = Quaternion.Euler(0f, 45f, 0f);
            playerForwardMoveRotation = Quaternion.Euler(0f, 0, 0f);
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.GetIsPlaying()) return;

            if (Input.GetKey(KeyCode.A))
            {
                moveHorizontal -= horizontalMovement * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, playerLeftMoveRotation, turnDuration);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveHorizontal += horizontalMovement * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, playerRightMoveRotation, turnDuration);
            }
            else
            {
                moveHorizontal += 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, playerForwardMoveRotation, turnDuration);
            }

            transform.position = new Vector3(
                Mathf.Clamp(moveHorizontal, -playerXMovementBoundary, playerXMovementBoundary),
                0, transform.position.z + Time.deltaTime * 5f);
        }

        #endregion
    }
}