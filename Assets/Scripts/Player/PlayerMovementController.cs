using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float playerXMovementBoundary;
        [SerializeField] private float horizontalMovement;
        private float moveHorizontal;

        private void FixedUpdate()
        {
            if (!GameManager.Instance.GetIsPlaying()) return;

            if (Input.GetKey(KeyCode.A))
            {
                moveHorizontal -= horizontalMovement * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveHorizontal += horizontalMovement * Time.deltaTime;
            }
            else
            {
                moveHorizontal += 0;
            }
            
            transform.position = new Vector3(
                Mathf.Clamp(moveHorizontal, -playerXMovementBoundary, playerXMovementBoundary),
                0, transform.position.z + Time.deltaTime * 5f);
        }
    }
}