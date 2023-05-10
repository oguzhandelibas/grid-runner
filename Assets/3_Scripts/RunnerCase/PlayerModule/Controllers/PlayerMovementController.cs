using UnityEngine;

namespace GridRunner.Runner.Commands
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private bool _readyToMove = false;
        private float _otherSpeed = 0f;
        private float _direction;
        private const float _xSpeed = 35f;
        private const float _maxYVelocity = 0.25f;
        private const float _minYVelocity = -9.81f;
        private const float _rotationSpeed = 35f;

        #endregion

        #endregion
        public void StopPlayerMovement()
        {
            _readyToMove = false;
        }

        public void StartPlayerMovement()
        {
            _readyToMove = true;
        }

        public void PlayerMovement(Rigidbody rigidbody, float speed, Transform target)
        {
            if (_readyToMove)
            {
                if (!_readyToMove) return;

                var posX = target.position.x - rigidbody.position.x;
                _direction = posX < 0 ? -1f : 1f;
                if (Mathf.Abs(posX) >= 0.05f)
                    _otherSpeed = Mathf.Lerp(_otherSpeed, _xSpeed, 0.7f);
                else
                    _otherSpeed = 0f;

                var velocity = rigidbody.velocity;
                velocity = new Vector3(Time.fixedDeltaTime * _otherSpeed * _direction, Mathf.Clamp(velocity.y, _minYVelocity, _maxYVelocity), Time.fixedDeltaTime * speed);
                rigidbody.velocity = velocity;

                RotatePlayer(target, rigidbody);
            }
        }
        private void RotatePlayer(Transform target, Rigidbody rb)
        {
            if (!target) return;

            Vector3 movementDirection = new Vector3(target.position.x, 0f, target.position.z);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up * 3f);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
