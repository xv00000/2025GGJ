using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<AudioClip> waitingClips = new List<AudioClip>();
    private bool canAddClip;
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    public AudioSource bgmSource;
    public AudioSource effectSource;
    private void Start()
    {
        StartCoroutine(ContinueToChangeCanAddClips());
    }
    public void Initialize()
    {
        effectSource = gameObject.AddComponent<AudioSource>();
        effectSource.volume = 0.5f;

    }
    public void PlayBGM(string name, bool is_loop = true)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);
        bgmSource.clip = clip;
        bgmSource.loop = is_loop;
        bgmSource.Play();
    }
    public void StopBGM()
    {
        bgmSource.Stop();
    }
    public void PlayEffect(string name, bool isIgnore = true)
    {
        if (isIgnore)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Effects/" + name);
            if (canAddClip)
            {
                canAddClip = false;
                waitingClips.Add(clip);
            }

            if (waitingClips.Count != 0 && (effectSource.clip == null || name != effectSource.clip.name))
            {
                effectSource.PlayOneShot(waitingClips[0]);
                waitingClips.Remove(waitingClips[0]);
            }
        }
        else
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Effects/" + name);
            effectSource.PlayOneShot(clip);
        }
    }
    private IEnumerator ContinueToChangeCanAddClips()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            canAddClip = true;
        }
    }
}


