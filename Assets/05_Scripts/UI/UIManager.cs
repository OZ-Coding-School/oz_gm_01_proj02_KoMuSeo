using System;
using System.Collections.Generic;
using UnityEngine;

public enum UIKey
{
    HUD,
    Menu,
    Setting,
    GameOver,
    FadeOut
}

public class UIManager : MonoBehaviour, IRegistryAdder
{
    [Header("UI Panel")]
    [SerializeField] UIPanel hudUI;
    [SerializeField] UIPanel menuUI;
    [SerializeField] UIPanel settingUI;
    [SerializeField] UIPanel gameoverUI;
    [SerializeField] UIPanel fadeoutUI;

    HUDViewModel hudVM;
    MenuViewModel menuVM;
    SettingViewModel settingVM;

    List<UIPanel> rewindList = new();
    Dictionary<UIKey, UIPanel> panels = new();
    //public event Action OnUIChanged;

    void Awake()
    {
        AddRegistry();
        AddtoDictionary();
    }

    void Start()
    {
        var player = StaticRegistry.Find<PlayerController>();
        var gm = StaticRegistry.Find<GameManager>();

        hudVM = new HUDViewModel(player.playerCtx, player.weaponManager);
        menuVM = new MenuViewModel(this, gm);
        settingVM = new SettingViewModel(this);

        Show(UIKey.HUD, false);
        ShowMenu(MenuMode.Start, true);
    }

    public void AddRegistry()
    {
        StaticRegistry.Add(this);
    }

    void AddtoDictionary()
    {
        panels.Clear();
        panels[UIKey.HUD] = hudUI;
        panels[UIKey.Menu] = menuUI;
        panels[UIKey.Setting] = settingUI;
        panels[UIKey.GameOver] = gameoverUI;
        panels[UIKey.FadeOut] = fadeoutUI;
    }

    public void Show(UIKey key, bool open)
    {
        if (!panels.TryGetValue(key, out var p) || !p) return;

        if (open && !p.IsOpen)
        {
            BindVM(key, p);
            p.Open();
            rewindList.Add(p);
        }
        else
        {
            UnbindVM(key, p);
            p.Close();

            if (rewindList.Count > 0)
                rewindList.RemoveAt(rewindList.Count - 1);
        }
    }

    public void ShowMenu(MenuMode mode, bool open = true)
    {
        menuVM?.SetMode(mode);
        Show(UIKey.Menu, open);
    }

    void BindVM(UIKey key, UIPanel panel)
    {
        switch (key)
        {
            case UIKey.HUD:
                if(panel is IBindable<HUDViewModel> hud)
                {
                    hud.Bind(hudVM);
                    hudVM.Activate();
                }
                break;

            case UIKey.Menu:
                if(panel is IBindable<MenuViewModel> menu)
                {
                    menu.Bind(menuVM);
                }
                break;

            case UIKey.Setting:
                if(panel is IBindable<SettingViewModel> setting)
                {
                    setting.Bind(settingVM);
                }
                break;
        }
    }

    void UnbindVM(UIKey key, UIPanel panel)
    {
        switch (key)
        {
            case UIKey.HUD:
                if (panel is IBindable<HUDViewModel> hud)
                {
                    hudVM?.Deactivate();
                    hud.Unbind();
                }
                break;

            case UIKey.Menu:
                if (panel is IBindable<MenuViewModel> menu)
                {
                    menu.Unbind();
                }
                break;

            case UIKey.Setting:
                if (panel is IBindable<SettingViewModel> set)
                {
                    set.Unbind();
                }
                break;
        }
    }
}