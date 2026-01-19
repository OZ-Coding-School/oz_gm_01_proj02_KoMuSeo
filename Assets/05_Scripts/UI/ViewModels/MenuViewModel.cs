using System;

public enum MenuMode
{
    Start,
    Pause
}

public class MenuViewModel
{
    UIManager ui;
    GameManager gm;

    public MenuMode Mode { get; set; }

    public string TitleText { get; set; } = "";
    public string PlayButtonText { get; set; } = "";
    public bool ShowExitButton { get; set; } = true;

    public event Action OnChanged;

    public MenuViewModel(UIManager ui, GameManager gm)
    {
        this.ui = ui;
        this.gm = gm;
    }

    public void SetMode(MenuMode mode)
    {
        Mode = mode;

        if(Mode == MenuMode.Pause)
        {
            TitleText = "PAUSE";
            PlayButtonText = "RESUME";
            ShowExitButton = true;
        }
        else
        {
            TitleText = "START";
            PlayButtonText = "PLAY";
            ShowExitButton = true;
        }

        OnChanged?.Invoke();
    }

    public void OnOpened()
    {
        gm.Pause();
        gm.TimeScale = 0f;
    }

    public void OnClosed()
    {
        gm.Resume();
        gm.TimeScale = 1f;
    }

    public void ClickPlayOrResume()
    {
        ui.Show(UIKey.Menu, false);
        ui.Show(UIKey.HUD, true);
    }

    public void ClickSetting()
    {
        ui.Show(UIKey.Setting, true);
    }

    public void ClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        UnityEngine.Application.Quit();
    }
}