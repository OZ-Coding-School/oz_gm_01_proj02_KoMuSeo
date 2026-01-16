using UnityEngine;
using System.Collections;
using System;

public enum PlayState
{
    None,
    Playing,
    Pause
}

public class GameManager : MonoBehaviour, IRegistryAdder
{
    [Header("Variable")]
    [SerializeField]private PlayState currentPlayState;

    public event Action<PlayState> OnPlayStateChanged;
    public event Action OnTimeScaleChanged;
    public float TimeScale
    {
        get
        {
            return Time.timeScale;
        }

        set
        {
            Time.timeScale = value;

            bool flow = Time.timeScale >= 0.5f;
            Cursor.lockState = flow ? CursorLockMode.Locked : CursorLockMode.Confined;
            Cursor.visible = !flow;
            OnTimeScaleChanged?.Invoke();
        }
    }

    public bool IsPlaying => currentPlayState == PlayState.Playing;

    private void Awake()
    {
        AddRegistry();
        StartCoroutine(Co_PlayLoop());
    }

    private IEnumerator Co_PlayLoop()
    {
        SetCurrentState(PlayState.Pause);

        while (true)
        {
            if (currentPlayState == PlayState.None) break;

            while(currentPlayState == PlayState.Pause)
            {
                yield return null;
            }

            yield return null;
        }
    }
    public void AddRegistry()
    {
        StaticRegistry.Add(this);
    }

    public PlayState GetCurrentState()
    {
        return currentPlayState;
    }

    private void SetCurrentState(PlayState change)
    {
        currentPlayState = change;
        OnPlayStateChanged?.Invoke(currentPlayState);
    }

    public void Pause()
    {
        TimeScale = 0f;
        SetCurrentState(PlayState.Pause);
    }
    
    public void Resume()
    {
        TimeScale = 1f;
        SetCurrentState(PlayState.Playing);
    }
}
