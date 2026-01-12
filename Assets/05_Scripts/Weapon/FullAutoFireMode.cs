public class FullAutoFireMode : IFireModeStrategy
{
    public void Tick(Weapon weapon, WeaponContext ctx, FireInputContext input)
    {
        if (!input.isPressed) return;
        weapon.DoFire(ctx);
    }
}