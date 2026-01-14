using TMPro;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI ammoText;
    PlayerController Controller;
    public Weapon currentWeapon;

    private void Start()
    {
        Controller = StaticRegistry.Find<PlayerController>();
        Controller.weaponManager.OnWeaponChanged += OnWeaponChanged;

        OnWeaponChanged(Controller.weaponManager.currentWeapon);
    }

    void OnWeaponChanged(Weapon newWeapon)
    {
        if (currentWeapon)
            currentWeapon.OnAmmoChanged -= UpdateAmmoFired;

        currentWeapon = newWeapon;

        if (currentWeapon)
            currentWeapon.OnAmmoChanged += UpdateAmmoFired;

        UpdateAmmoUI(newWeapon);
    }

    public void UpdateAmmoUI(Weapon weapon)
    {
        int maxAmmo = weapon.MaxMag;
        int curAmmo = weapon.CurrentMag;

        ammoText.text = $"{curAmmo} / {maxAmmo}";
    }

    public void UpdateAmmoFired(int currentMag, int maxMag)
    {
        ammoText.text = $"{currentMag} / {maxMag}";
    }
}
