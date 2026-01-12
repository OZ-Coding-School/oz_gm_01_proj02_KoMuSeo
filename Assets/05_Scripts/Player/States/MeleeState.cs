using StateController;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Playables;
using Unity.VisualScripting;

public class MeleeState : BaseState
{
    float timer;
    bool attacked;

    const float MELEE_DURATION = 0.5f;
    const float HIT_TIME = 0.1f;

    public MeleeState(PlayerController controller) : base(controller) { }

    public override void OnEnterState()
    {
        base.OnEnterState();

        timer = 0f;
        attacked = false;

        //Controller.playerCtx.Anim.Play("Melee");
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;

        if (!attacked && timer >= HIT_TIME)
        {
            MeleeHit();
            attacked = true;
        }

        if (timer >= MELEE_DURATION)
        {
            Controller.playerCtx.ActionSM.ChangeState(StateName.ActionIdle);
        }

    }

    void MeleeHit()
    {
        float angle = 60;
        float distance = 5f;
        int rayCount = 10;
        LayerMask hitLayer = LayerMask.GetMask("Enemy");

        float half = angle * 0.5f;

        for (int i = 0; i < rayCount; ++i)
        {
            float currentAngle = -half + (angle / rayCount) * i;
            var cam = Camera.main.transform.forward;
            var dir = Quaternion.AngleAxis(currentAngle, Vector3.up) * cam;

            if (Physics.Raycast(Controller.transform.position, dir, out var hit, distance))
            {
                Debug.DrawLine(Controller.transform.position, hit.point, Color.green);
            }
            else
            {
                Debug.DrawRay(Controller.transform.position, dir * distance, Color.red);
            }
        }
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }
}