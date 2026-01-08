public interface IDamageModifier
{
    void Modify(ref DamageContext context, ref DamageResult result);
}