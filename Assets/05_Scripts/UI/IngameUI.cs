using TMPro;
using UnityEngine;

public class IngameUI : UIPanel
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI fireModeText;

    PlayerController Controller;
    PlayerContext ctx;
    Weapon currentWeapon;

    private void OnEnable()
    {
        Controller = StaticRegistry.Find<PlayerController>();
        ctx = Controller.playerCtx;
        ctx.OnHPChanged += UpdateHpUI;

        Controller.weaponManager.OnWeaponChanged += OnWeaponChanged;

        OnWeaponChanged(Controller.weaponManager.currentWeapon);
    }

    private void OnDisable()
    {
        ctx.OnHPChanged -= UpdateHpUI;
        Controller.weaponManager.OnWeaponChanged -= OnWeaponChanged;
        UnBindEvents();
    }

    void OnWeaponChanged(Weapon newWeapon)
    {
        UnBindEvents();

        currentWeapon = newWeapon;

        BindEvents(newWeapon);
    }

    void BindEvents(Weapon weapon)
    {
        weapon.OnAmmoChanged += UpdateAmmoFired;
        weapon.OnFireModeChanged += UpdateFiremodeUI;

        UpdateAmmoUI(weapon);
        UpdateFiremodeUI(weapon.CurrentMode.ToString());
    }

    void UnBindEvents()
    {
        if (currentWeapon == null) return;
        currentWeapon.OnAmmoChanged -= UpdateAmmoFired;
        currentWeapon.OnFireModeChanged -= UpdateFiremodeUI;
    }

    public void UpdateAmmoUI(Weapon weapon)
    {
        int maxAmmo = weapon.MaxMag;
        int curAmmo = weapon.CurrentMag;

        ammoText.text = $"{curAmmo} / {maxAmmo}";
    }

    public void UpdateFiremodeUI(string mode)
    {
        fireModeText.text = mode;
    }

    public void UpdateAmmoFired(int currentMag, int maxMag)
    {
        ammoText.text = $"{currentMag} / {maxMag}";
    }

    public void UpdateHpUI(float current, float max)
    {
        hpText.text = $"{current} / {max}";
    }
}
