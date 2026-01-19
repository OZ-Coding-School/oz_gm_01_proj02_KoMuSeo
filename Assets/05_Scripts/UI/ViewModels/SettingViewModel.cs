using System;

public class SettingViewModel
{
    UIManager ui;

    public float Master { get; set; } = 1f;
    public float Sfx { get; set; } = 1f;
    public float Sensitivity { get; set; } = 1f;

    public event Action OnChanged;

    public SettingViewModel(UIManager ui)
    {
        this.ui = ui;
    }

    public void SetMaster(float v)
    {
        this.Master = v;

        OnChanged?.Invoke();
    }

    public void SetSfx(float v)
    {
        this.Sfx = v;

        OnChanged?.Invoke();
    }

    public void SetSensitivity(float v)
    {
        Sensitivity = v;

        OnChanged?.Invoke();
    }

    public void ClickBack()
    {
        ui.Show(UIKey.Setting, false);
    }
}