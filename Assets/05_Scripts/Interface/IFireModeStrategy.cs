public interface IFireModeStrategy
{
    void Tick(Weapon weapon, WeaponContext context, FireInputContext input);
}