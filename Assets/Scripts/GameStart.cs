using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameStart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Transform m_Actors = null;
    Vector3 pos;

    public GameObject highScore;

    public GameObject titleBgm;
    public GameObject ingameBgm;

    public void StartGame()
    {/*
        AudioSource title = titlebgm.GetComponent<AudioSource>();
        AudioSource ingame = ingamebgm.GetComponent<AudioSource>();
        title.Stop();
        ingame.Play();
        */
        highScore.SetActive(false);

        StartCoroutine(ActorDown());
    }

    // 버튼이 아래로 내려가는 애니메이션 구현
    IEnumerator ActorDown()
    {
        WaitForSeconds wait = new WaitForSeconds(0.016f);

        while (true)
        {
            pos = Camera.main.WorldToViewportPoint(m_Actors.transform.position);
            m_Actors.position -= new Vector3(0, 600f, 0) * Time.deltaTime;

            if (pos.y < -0.3f)
            {
                Invoke("LoadScene",0.5f);
                yield break;
            }

            yield return wait;
        }
    }

    // 씬 불러오기
    void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    // 버튼 눌르고 있는 듯한 효과 구현
    public void OnPointerDown(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
    }

    // 크기 복원
    public void OnPointerUp(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}