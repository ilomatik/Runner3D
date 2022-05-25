using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        private Vector3 moveFactor;

        [SerializeField] private float maxSwerveAmount;
        [SerializeField] private float playerXMovementBoundary;

        private void FixedUpdate()
        {
            // var position = transform.position;
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     moveFactor = new Vector3(
            //         Mathf.Clamp(transform.position.x, -playerXMovementBoundary, playerXMovementBoundary),
            //         position.y, -Time.deltaTime * 10f);
            // }
            // else if (Input.GetKeyDown(KeyCode.D))
            // {
            //     moveFactor =
            //         new Vector3(Mathf.Clamp(transform.position.x, -playerXMovementBoundary, playerXMovementBoundary),
            //             position.y, Time.deltaTime * 10f);
            // }
            // else
            // {
            //     moveFactor = position;
            // }
            //
            // transform.Translate(moveFactor);
        }
    }
}