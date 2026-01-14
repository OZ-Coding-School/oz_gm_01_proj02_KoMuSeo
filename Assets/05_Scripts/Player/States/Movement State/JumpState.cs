using UnityEngine;

public class JumpState : BasePlayerState
{
    public JumpState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        base.OnEnterState();
        yVelocity = playerCtx.JumpForce;
    }

    public override void OnUpdateState()
    {
        if (playerCtx.CharacterController.isGrounded && yVelocity <= 0f)
        {
            playerCtx.MovementSM.ChangeState(Controller.PrevMovementState);
        }

        base.OnUpdateState();
    }

    public override void OnExitState()
    {
        Controller.isJump = false;
    }
}