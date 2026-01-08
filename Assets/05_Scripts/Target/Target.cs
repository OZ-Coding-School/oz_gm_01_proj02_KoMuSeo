using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public int hp = 100;
    public void ApplyDamage(DamageResult res)
    {
        hp -= (int)res.finalDamage;
    }
}
