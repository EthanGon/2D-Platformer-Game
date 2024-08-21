using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMusic : MonoBehaviour
{
    public Slider music;
    public AudioSource bgm;

    private void Awake()
    {
        bgm = GetComponent<AudioSource>();
    }

    private void Start()
    {
        music.value = bgm.volume;
    }

    public void setVol()
    {
        bgm.volume = music.value;
    }


}
