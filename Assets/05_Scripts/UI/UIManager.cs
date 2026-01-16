using System;
using System.Collections.Generic;
using UnityEngine;

public enum UIKey
{
    HUD,
    Start,
    Pause,
    Setting,
    GameOver,
    FadeOut
}

public class UIManager : MonoBehaviour, IRegistryAdder
{
    [Header("UI Panel")]
    [SerializeField] UIPanel hudUI;
    [SerializeField] UIPanel startUI;
    [SerializeField] UIPanel pauseUI;
    [SerializeField] UIPanel settingUI;
    [SerializeField] UIPanel gameoverUI;
    [SerializeField] UIPanel fadeoutUI;

    List<UIPanel> rewindList = new();
    Dictionary<UIKey, UIPanel> panels = new();
    public event Action OnUIChanged;

    void Awake()
    {
        AddRegistry();
        AddtoDictionary();
    }

    public void AddRegistry()
    {
        StaticRegistry.Add(this);
    }

    void AddtoDictionary()
    {
        panels.Clear();
        Add(UIKey.HUD, hudUI);
        Add(UIKey.Start, startUI);
        Add(UIKey.Pause, pauseUI);
        Add(UIKey.Setting, settingUI);
        Add(UIKey.GameOver, gameoverUI);
        Add(UIKey.FadeOut, fadeoutUI);
    }

    void Add(UIKey key, UIPanel value)
    {
        if (value == null) return;
        panels[key] = value;
    }

    public void Show(UIKey key, bool open)
    {
        if(!panels.TryGetValue(key, out var p) || !p) return;

        if (open && !p.IsOpen)
        {
            p.Open();
            rewindList.Add(p);
        }
        else
            p.Close();

        OnUIChanged?.Invoke();
    }
}