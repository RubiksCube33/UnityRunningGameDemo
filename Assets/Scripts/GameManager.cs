using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool gamePaused;
    
    // 프레임 조절
    void Awake(){
         Application.targetFrameRate = 240;
    }

    // 계속하기
    public void Resume()
    {
        pausePanel.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1f;
    }

    // 일시정지
    public void Pause()
    {
        if (gamePaused)
            Resume();
        else
        {
            pausePanel.SetActive(true);
            gamePaused = true;
            Time.timeScale = 0f;
        }
    }

    // 다시하기
    public void Retry()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    // 홈으로 돌아가기
    public void GoMainScene()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    // 게임 종료
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}