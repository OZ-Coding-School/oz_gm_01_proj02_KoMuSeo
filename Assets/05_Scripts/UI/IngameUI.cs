using TMPro;
using UnityEngine;

public class IngameUI : UIPanel, IBindable<HUDViewModel>
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI fireModeText;

    HUDViewModel vm;

    public void Bind(HUDViewModel vm)
    {
        Unbind();

        this.vm = vm;
        if (this.vm == null) return;

        this.vm.OnChanged += Refresh;
        Refresh();
    }

    public void Unbind()
    {
        if (vm == null) return;
        vm.OnChanged -= Refresh;
    }

    private void Refresh()
    {
        ammoText.text = vm.AmmoText;
        hpText.text = vm.HpText;
        fireModeText.text = vm.FireModeText;
    }

    protected override void OnClosed()
    {
        Unbind();
    }
}
