using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, IRegistryAdder
{
    [SerializeField] private List<AudioClip> preloadClips = new();
    Dictionary<string, AudioClip> clips = new();
    AudioSource currentAudio;

    public float MasterVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }
    public float SFXVolume { get; set; } = 1.0f;
    public float BGMVolume { get; set; } = 1.0f;

    public AudioSource CurrentAudio => currentAudio;

    private void Awake()
    {
        AddRegistry();
        foreach(var c in preloadClips)
        {
            clips[c.name] = c;
        }
    }

    public AudioClip GetClip(string name)
    {
        return clips.TryGetValue(name, out var clip) ? clip : null;
    }


    public void PlaySound(AudioClip clip, Vector3 pos, Quaternion rot)
    {
        var speaker = ObjectPoolManager.Instance.Spawn(PoolId.SoundPlayer, pos, rot);
        var audioSource = speaker.GetComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = SFXVolume;
        audioSource.spatialBlend = 1.0f; // 3D sound
        audioSource.Play();

        StartCoroutine(Co_DespawnSoundPlayer(audioSource));
    }

    public void PlayBGM(AudioClip clip, Vector3 pos, Quaternion rot)
    {
        if (currentAudio != null)
            currentAudio.Stop();
        var bgmPlayer = ObjectPoolManager.Instance.Spawn(PoolId.SoundPlayer, pos, rot);
        var audioSource = bgmPlayer.GetComponent<AudioSource>();
        currentAudio = audioSource;

        audioSource.volume = BGMVolume;
        audioSource.clip = clip;
        audioSource.spatialBlend = 0.0f; // 2D sound
        audioSource.loop = true;
        audioSource.Play();
    }

    IEnumerator Co_DespawnSoundPlayer(AudioSource aus)
    {
        while(aus.isPlaying)
            yield return null;

        ObjectPoolManager.Instance.Despawn(aus.gameObject);
    }

    public void AddRegistry()
    {
        StaticRegistry.Add<SoundManager>(this);
    }
}