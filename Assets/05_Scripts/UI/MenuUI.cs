using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : UIPanel, IBindable<MenuViewModel>
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Button playBtn;
    [SerializeField] TextMeshProUGUI playBtnText;
    [SerializeField] Button settingBtn;
    [SerializeField] Button exitBtn;

    MenuViewModel vm;

    public void Bind(MenuViewModel vm)
    {
        Unbind();
        this.vm = vm;

        this.vm.OnChanged += Refresh;

        playBtn.onClick.AddListener(this.vm.ClickPlayOrResume);
        settingBtn.onClick.AddListener(this.vm.ClickSetting);
        exitBtn.onClick.AddListener(this.vm.ClickExit);

        Refresh();
    }

    private void Refresh()
    {
        if (titleText) titleText.text = vm.TitleText;
        if (playBtnText) playBtnText.text = vm.PlayButtonText;
        if (exitBtn) exitBtn.gameObject.SetActive(vm.ShowExitButton);
    }

    public void Unbind()
    {
        if (vm != null)
        {
            vm.OnChanged -= Refresh;

            playBtn.onClick.RemoveListener(vm.ClickPlayOrResume);
            settingBtn.onClick.RemoveListener(vm.ClickSetting);
            exitBtn.onClick.RemoveListener(vm.ClickExit);
        }
    }

    protected override void OnOpened()
    {
        vm?.OnOpened();
    }

    protected override void OnClosed()
    {
        vm?.OnClosed();
    }
}
