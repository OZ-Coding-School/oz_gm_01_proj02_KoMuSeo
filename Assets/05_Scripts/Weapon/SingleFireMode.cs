public class SingleFireMode : IFireModeStrategy
{
    public void Tick(Weapon weapon, WeaponContext ctx, FireInputContext input)
    {
        if (!input.wasPressedThisFrame) return;
        weapon.DoFire(ctx);
    }
}