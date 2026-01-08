public class HeadShotModifier : IDamageModifier
{
    public void Modify(ref DamageContext context, ref DamageResult result)
    {
        if(context.hitZone == HitZone.Head)
        {
            result.finalDamage *= 2f;
            result.isCritical = true;
        }
    }
}