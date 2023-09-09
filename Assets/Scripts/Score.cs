using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text myScore;
    PlayerMove player;

    float timeCount = 0;
    public float m_Score;

    // Start is called before the first frame update
    void Start()
    {
        myScore = GameObject.FindWithTag("score").GetComponent<Text>();
        player = GameObject.FindWithTag("player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        SetCountText();

        // 하이스코어 기록
        if(player.isDead && m_Score > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", m_Score);
        }

        // 속도 증가
        if(m_Score % 500 == 0 && player.basicSpeed <= 1350)
        {
            player.basicSpeed += 50f;
            player.maxSpeed = player.basicSpeed * 1.5f;
        }
    }

    // 스코어 표시
    void SetCountText()
    {
        m_Score = (Mathf.Round(timeCount * 50) * 1);
        myScore.text = "Score : " + m_Score.ToString();
    }
}
