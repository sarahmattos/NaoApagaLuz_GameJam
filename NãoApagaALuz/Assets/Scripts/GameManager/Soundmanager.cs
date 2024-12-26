using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioSource music_AudioSource;
    [SerializeField] private AudioSource time_AudioSource;
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _clickoff;
    [SerializeField] private AudioClip[] _scary;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _door;
    [SerializeField] private AudioClip _popup;
    [SerializeField] private AudioClip _timeRemaing;
    public static Soundmanager Instance;
    int currentId = 0;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        Instance = this;
    }
    public void ClickLigthOn() => PlaySound(_click);
    public void TimeRemaingSound()
    {
        time_AudioSource.Play();
    }
    public void PopUp() => PlaySound(_popup);
    public void ClickLigthOff() => PlaySound(_clickoff);
    public void MusicPLay()
    {
        music_AudioSource.Play();
    }
    public void ScarySound(bool value=true)
    {
        if (value)
        {
            PlaySound(_scary[currentId]);
            currentId++;
            if (currentId >= _scary.Length) currentId = 0;
        }
        else
        {
           // m_AudioSource.Stop();
        }
       
    }
    public void HitSound() => PlaySound(_hit);
    public void DoorSound() => PlaySound(_door);
    public void PlaySound(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }

    
}
