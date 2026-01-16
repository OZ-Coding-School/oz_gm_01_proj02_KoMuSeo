using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    protected bool isOpen;
    public bool IsOpen
    {
        get {  return isOpen; }
        set
        {
            isOpen = value;
            gameObject.SetActive(isOpen);
        }
    }

    CanvasGroup cg;

    protected virtual void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public virtual void Open()
    {
        IsOpen = true;
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
        cg.interactable = true;
        OnOpened();
    }

    public virtual void Close()
    {
        IsOpen = false;
        OnClosed();
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }

    protected virtual void OnOpened() { }
    protected virtual void OnClosed() { }
}