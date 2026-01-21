using UnityEngine;

public class SubWeapon : Weapon
{
    protected override void Awake()
    {
        base.Awake();

        context.muzzle = muzzle;
        context.fireRate = fireRate;
        context.damage = hitDamage;
        context.hitscanRange = hitscanRange;
        context.bulletSpeed = bulletSpeed;
        context.maxRange = maxRange;
        context.spreadAngle = spreadAngle;

        CurrentMag = MaxMag;

        fireModes.Add(FireMode.Single, new SingleFireMode());
        SetFireMode(FireMode.Single);
    }

    protected override void Start()
    {
        base.Start();
        fireSound = soundManager.GetClip("Hand Gun 1");
    }
}