using GridRunner.Runner.Enums;
using UnityEngine;

namespace GridRunner.Runner.Commands
{
    public class ChangePlayerAnimationCommand
    {
        #region Self Variables

        #region Private Variables

        private Animator _animator;
        private PlayerAnimationType _playerAnimationType;

        #endregion

        #endregion
        public ChangePlayerAnimationCommand(Animator animator)
        {
            _animator = animator;
        }
        public void ChangePlayerAnimation(PlayerAnimationType playerAnimationType)
        {
            _playerAnimationType = playerAnimationType;
            _animator.SetTrigger(_playerAnimationType.ToString());
        }
    }
}
