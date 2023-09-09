using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Text _highScore;
    public float getScore;

    // Start is called before the first frame update
    void Start()
    {
        _highScore = GameObject.FindWithTag("highscore").GetComponent<Text>();
        getScore = PlayerPrefs.GetFloat("HighScore", 0);
        _highScore.text = "HighScore : " + getScore.ToString();
    }
}
