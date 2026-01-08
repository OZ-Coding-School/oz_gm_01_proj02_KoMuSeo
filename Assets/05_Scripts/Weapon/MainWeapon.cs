using UnityEngine;

public class MainWeapon : MonoBehaviour
{
    [Header("References")]
    public Transform muzzle;

    [Header("Hit-scan")]
    public float hitscanRange = 30f;
    public int hitDamage = 1;

    [Header("Ballistic")]
    public float bulletSpeed = 800f;
    public float fireRate = 0.1f;
    float lastFireTime;

    [Header("Spread")]
    public float spreadAngle = 0.6f;

    DamageSystem dms;

    private void Awake()
    {
        dms = StaticRegistry.Find<DamageSystem>();
    }

    public void Fire() 
    {
        if (Time.time < lastFireTime + fireRate) return;
        lastFireTime = Time.time;

        Debug.Log("Fire");


        Vector3 dir = GetSpreadDirection();

        if (Physics.Raycast(muzzle.position, dir, out RaycastHit hit, hitscanRange))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out var dmg))
            {
                DamageContext context = new()
                {
                    attacker = gameObject,
                    target = hit.collider.gameObject,
                    hitPoint = hit.point,
                    hitNormal = hit.normal,
                    damage = hitDamage,
                    distance = hit.distance,
                    damageType = DamageType.Bullet,
                    hitZone = dms.ResolveHitZone(hit.collider)
                };
                
                DamageResult res = dms.Pipeline.Calculate(context);
                dmg.ApplyDamage(res);
            }

            SpawnTracer(hit.point);
            return;
        }

        var bm = StaticRegistry.Find<BulletManger>();
        bm.SpawnBullet(muzzle.position, dir, bulletSpeed);

    }

    Vector3 GetSpreadDirection()
    {
        Vector3 dir = muzzle.forward;
        dir = Quaternion.Euler(
            Random.Range(-spreadAngle, spreadAngle),
            Random.Range(-spreadAngle, spreadAngle),
            0f
        ) * dir;
        return dir.normalized;
    }


    void SpawnTracer(Vector3 endPoint)
    {
        // LineRenderer
    }
}
