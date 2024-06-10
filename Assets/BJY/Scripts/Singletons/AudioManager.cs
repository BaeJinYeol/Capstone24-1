using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource bgSound;
    public AudioClip[] bglist;
    public float bgVolume;
    public float sfxVolume;
    public float voiceVolume;

    public override void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i <bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
                PlayBgSound(bglist[i]);
        }
    }

    private void Start()
    {
        SetBgVolume(bgVolume * 0.1f);
        SetSfxVolume(sfxVolume * 0.09f);
    }

    public void SetBgVolume(float volume)
    {
        bgSound.volume = bgVolume = volume * 0.1f;
    }
    public void SetSfxVolume(float volume)
    {
        voiceVolume = volume;
        sfxVolume = volume * 0.09f;
    }

    public GameObject PlaySFXSound(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Audio");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.volume = sfxVolume;
        audiosource.Play();

        Destroy(go, clip.length);
        return go;
    }

    public GameObject PlayeVoice(string voiceName, AudioClip clip)
    {
        GameObject go = new GameObject(voiceName + "Audio");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.volume = voiceVolume;
        audiosource.Play();

        Destroy(go, clip.length);
        return go;
    }

    public void PlayBgSound(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = bgVolume;
        bgSound.Play();
    }
}
