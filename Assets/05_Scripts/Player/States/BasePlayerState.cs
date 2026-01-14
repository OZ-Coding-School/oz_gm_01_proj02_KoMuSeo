using StateController;
using UnityEngine;
public abstract class BasePlayerState : BaseState
{
    protected PlayerContext playerCtx;
    protected float yVelocity;
    protected const float gravity = -20f;

    protected Vector3 newMovementVelocity;
    protected Vector3 newMovementVelocityRef;

    const float SMOOTH_TIME = 0.2f;

    public BasePlayerState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        playerCtx = Controller.playerCtx;

        Vector3 moveDir = Controller.transform.right * Controller.inputDir.x +
                          Controller.transform.forward * Controller.inputDir.y;

        newMovementVelocity = moveDir.normalized * playerCtx.MoveSpeed;
        newMovementVelocityRef = Vector3.zero;
    }

    public override void OnUpdateState()
    {
        CommonMovement();
    }

    public void ApplyGravity()
    {
        if (playerCtx.CharacterController.isGrounded && yVelocity < 0f)
            yVelocity = -1f;

        yVelocity += gravity * Time.deltaTime;
    }

    public void CommonMovement()
    {
        ApplyGravity();
        Vector3 moveDir = Controller.transform.right * Controller.inputDir.x + 
            Controller.transform.forward * Controller.inputDir.z;
        moveDir.Normalize();

        Vector3 targetVelocity = moveDir * playerCtx.MoveSpeed;

        if(moveDir.sqrMagnitude < 0.01f)
        {
            newMovementVelocity = Vector3.zero;
            newMovementVelocityRef = Vector3.zero;
        }
        else
        {
            newMovementVelocity = Vector3.SmoothDamp(
                            newMovementVelocity,
                            targetVelocity,
                            ref newMovementVelocityRef,
                            SMOOTH_TIME);
        }

        Vector3 velocity = newMovementVelocity;
        velocity.y = yVelocity;


        playerCtx.CharacterController.Move(velocity * Time.deltaTime);
    }
}