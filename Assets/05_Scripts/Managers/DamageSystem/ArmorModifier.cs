using UnityEngine;

public class ArmorModifier : IDamageModifier
{
    public void Modify(ref DamageContext context, ref DamageResult result)
    {
        //if(context.target.TryGetComponent<Armor>(out var armor))
        //{
        //    result.finalDamage  = Mathf.Max(0, result.finalDamage-armor.value);
        //}
    }
}
