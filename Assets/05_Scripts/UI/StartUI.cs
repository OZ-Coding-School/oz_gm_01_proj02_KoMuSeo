using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : UIPanel
{
    [SerializeField] Button playBtn;
    [SerializeField] Button settingBtn;
    [SerializeField] Button exitBtn;
    UIManager um;
    GameManager gm;

    private void Start()
    {
        um = StaticRegistry.Find<UIManager>();
        gm = StaticRegistry.Find<GameManager>();

        playBtn.onClick.AddListener(OnPlayButtonCkicked);
        //settingBtn.onClick.AddListener();
        exitBtn.onClick.AddListener(OnExitPressed);
    }

    void OnPlayButtonCkicked()
    {
        um.Show(UIKey.Start, false);
        um.Show(UIKey.HUD, true);
    }

    void OnExitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    
    protected override void OnOpened()
    {
        gm.Pause();
        gm.TimeScale = 0f;
    }

    protected override void OnClosed()
    {
        gm.Resume();
        gm.TimeScale = 1f;
    }
}
