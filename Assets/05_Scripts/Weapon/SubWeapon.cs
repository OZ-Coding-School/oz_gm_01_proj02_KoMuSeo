using UnityEngine;

public class SubWeapon : Weapon
{
    [Header("References"), Tooltip("탄환과 이펙트가 나오는 위치")]
    public Transform muzzle;
    public Transform muzzleVFX;

    [Header("Hit-scan"), Tooltip("히트 스캔 방식에 필요한 변수")]
    public float hitscanRange = 15f;
    public int hitDamage = 15;

    [Header("Ballistic"), Tooltip("탄도학 계산에 필요한 변수")]
    public float bulletSpeed = 500f;
    public float fireRate = 0.25f;
    public float maxRange = 75f;

    [Header("Spread"), Tooltip("탄환 퍼짐 정도")]
    public float spreadAngle = 0.8f;

    [Header("Mag"), Tooltip("장탄 수 제한")]
    public int maxMag;
    public int currentMag;

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

        currentMag = maxMag;

        fireModes.Add(FireMode.Single, new SingleFireMode());
        SetFireMode(FireMode.Single);
    }
}