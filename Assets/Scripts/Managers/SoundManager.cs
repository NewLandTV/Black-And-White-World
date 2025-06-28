using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField]
    private Sound[] sfx;
    [SerializeField]
    private Sound[] bgm;

    [SerializeField]
    private AudioSource bgmPlayer;
    [SerializeField]
    private AudioSource[] sfxPlayers;

    [Serializable]
    public struct Sound
    {
        [SerializeField]
        private string name;
        public string Name => name;

        [SerializeField]
        private AudioClip clip;
        public AudioClip Clip => clip;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(string p_bgmName, bool loop = true)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (!p_bgmName.Equals(bgm[i].Name))
            {
                continue;
            }

            bgmPlayer.clip = bgm[i].Clip;
            bgmPlayer.loop = loop;

            bgmPlayer.Play();
        }
    }

    public void PauseBGM()
    {
        bgmPlayer.Pause();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (!p_sfxName.Equals(sfx[i].Name))
            {
                continue;
            }

            for (int j = 0; j < sfxPlayers.Length; j++)
            {
                if (sfxPlayers[j].isPlaying)
                {
                    continue;
                }

                sfxPlayers[j].clip = sfx[i].Clip;

                sfxPlayers[j].Play();
            }
        }
    }

    public void StopSFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (!p_sfxName.Equals(sfx[i].Name))
            {
                continue;
            }

            for (int j = 0; j < sfxPlayers.Length; j++)
            {
                if (!sfxPlayers[j].isPlaying)
                {
                    continue;
                }

                sfxPlayers[j].Stop();
            }
        }
    }

    public void StopAllSFX()
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            sfxPlayers[i].Stop();
        }
    }
}
