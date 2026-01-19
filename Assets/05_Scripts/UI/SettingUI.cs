using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UIPanel, IBindable<SettingViewModel>
{
    [Header("Elements")]
    [SerializeField] private Slider masterVol;
    [SerializeField] private Slider sfxVol;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Button backBtn;

    SettingViewModel vm;

    public void Bind(SettingViewModel vm)
    {
        Unbind();
        this.vm = vm;

        this.vm.OnChanged += Refresh;

        masterVol.onValueChanged.AddListener(this.vm.SetMaster);
        sfxVol.onValueChanged.AddListener(this.vm.SetSfx);
        sensitivitySlider.onValueChanged.AddListener(this.vm.SetSensitivity);

        backBtn.onClick.AddListener(this.vm.ClickBack);

        Refresh();
    }

    private void Refresh()
    {
        masterVol.SetValueWithoutNotify(vm.Master);
        sfxVol.SetValueWithoutNotify(vm.Sfx);
        sensitivitySlider.SetValueWithoutNotify(vm.Sensitivity);
    }

    public void Unbind()
    {
        if(vm != null)
        {
            vm.OnChanged -= Refresh;

            masterVol.onValueChanged.RemoveListener(vm.SetMaster);
            sfxVol.onValueChanged.RemoveListener(vm.SetSfx);
            sensitivitySlider.onValueChanged.RemoveListener(vm.SetSensitivity);

            backBtn.onClick.RemoveListener(vm.ClickBack);
        }
    }
}