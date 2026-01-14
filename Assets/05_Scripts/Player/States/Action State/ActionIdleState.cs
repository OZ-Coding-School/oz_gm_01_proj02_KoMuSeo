using StateController;

public class ActionIdleState : BaseState
{
    public ActionIdleState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    public override void OnUpdateState()
    {
        if (Controller.fireInput.wasPressedThisFrame)
        {
            var weapon = Controller.weaponManager.GetCurrentWeapon();
            Controller.playerCtx.ActionSM.ChangeState(
                weapon != null && weapon.UseThrowState ?
                StateName.Throw :
                StateName.Fire);
        }

        if (Controller.isMelee)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.Melee);
        }

        if (Controller.isReload) 
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.Reload);
        }
    }
}