using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public GameObject SoundBtn;
    public Sprite SoundOn;
    public Sprite SoundOff;
    public AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Audio") == 0)
        {
            SoundBtn.GetComponent<Image>().sprite = SoundOff;
            BGM.Stop();
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = SoundOn;
            BGM.Play();
        }
    }

    public void ChangeImage()
    {
        if (SoundBtn.GetComponent<Image>().sprite == SoundOn)
        {
            PlayerPrefs.SetInt("Audio", 0);
            SoundBtn.GetComponent<Image>().sprite = SoundOff;
            BGM.Stop();
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 1);
            SoundBtn.GetComponent<Image>().sprite = SoundOn;
            BGM.Play();
        }
    }
}
