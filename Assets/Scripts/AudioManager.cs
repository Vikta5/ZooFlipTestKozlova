using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get
        {
            return Instance;
        }
    }

    public static AudioManager instance = null;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
    }

    public enum Sound
    {
        Up,
        Down,
        Start,
        Finish,
        Coin
    }

    [SerializeField] AudioClip up, down, start, finish, coin;
    AudioClip clip;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Stop();
        Play(Sound.Start);
    }
    public void Play(Sound sound)
    {

        switch (sound)
        {
            case Sound.Down:
                {
                    clip = down;
                    break;
                }
            case Sound.Finish:
                {
                    clip = finish;
                    break;
                }
            case Sound.Start:
                {
                    clip = start;
                    break;
                }
            case Sound.Up:
                {
                    clip = up;
                    break;
                }
            case Sound.Coin:
                {
                    clip = coin;
                    break;
                }
        }

        audio.PlayOneShot(clip);
    }
}
