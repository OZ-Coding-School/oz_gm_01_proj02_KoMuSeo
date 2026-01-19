using System;
using UnityEngine;

public class HUDViewModel
{
    public string AmmoText { get; set; } = "- / -";
    public string HpText { get; set; } = "- / -";
    public string FireModeText { get; set; } = "-";

    public event Action OnChanged;

    PlayerContext playerCtx;
    WeaponManager weaponManager;
    Weapon currentWeapon;

    public HUDViewModel(PlayerContext contex, WeaponManager weaponManager)
    {
        playerCtx = contex;
        this.weaponManager = weaponManager;
    }

    public void Activate()
    {
        if (playerCtx)
        {
            playerCtx.OnHPChanged += OnHChanged;
            HpText = $"{playerCtx.CurrentHP} / {playerCtx.MaxHP}";
        }

        if (weaponManager)
        {
            weaponManager.OnWeaponChanged += OnWeaponChanged;
            OnWeaponChanged(weaponManager.currentWeapon);
        }

        OnChanged?.Invoke();
    }

    public void Deactivate()
    {
        playerCtx.OnHPChanged -= OnHChanged;
        weaponManager.OnWeaponChanged -= OnWeaponChanged;

        UnbindWeaponEvents();
    }

    private void OnWeaponChanged(Weapon newWeapon)
    {
        UnbindWeaponEvents();
        currentWeapon = newWeapon;
        BindWeaponEvents();

        AmmoText = $"{currentWeapon.CurrentMag} / {currentWeapon.MaxMag}";
        FireModeText = currentWeapon.CurrentMode.ToString();

        OnChanged?.Invoke();
    }

    private void OnHChanged(float current, float max)
    {
        HpText = $"{current} / {max}";
        OnChanged?.Invoke();

    }

    void BindWeaponEvents()
    {
        currentWeapon.OnAmmoChanged += OnAmmoChanged;
        currentWeapon.OnFireModeChanged += OnFireModeChanged;
    }

    void UnbindWeaponEvents()
    {
        if (!currentWeapon) return;
        currentWeapon.OnAmmoChanged -= OnAmmoChanged;
        currentWeapon.OnFireModeChanged -= OnFireModeChanged;
    }

    private void OnFireModeChanged(string obj)
    {
        FireModeText = obj;
        OnChanged?.Invoke();
    }

    private void OnAmmoChanged(int arg1, int arg2)
    {
        AmmoText = $"{arg1} / {arg2}";
        OnChanged?.Invoke();
    }
}