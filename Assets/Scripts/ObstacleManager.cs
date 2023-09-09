using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Obstacles
{
    public Transform pattern; // 크기 1 : 다룰 오브젝트 하나 배정
    public Transform lastCorn; // 패턴마다 가장 오른쪽에 있는 장애물
    public bool isOperating; // 작동하고 있는 장애물이 있는가
    public bool pick; // 가장 최근에 고른 장애물
}

public class ObstacleManager : MonoBehaviour
{
    public Obstacles[] m_Obstacles; // 장애물 패턴 갯수만큼 Size 선언 (현재 6개)

    private int _startTime = 5; // 초반에 바로 장애물 안나오도록 시간 조절
    bool canSpawn; // 다음 장애물 작동 가능한가
    Vector3 pos;

    PlayerMove PlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = GameObject.FindWithTag("player").GetComponent<PlayerMove>();

        canSpawn = true;

        StartCoroutine(CheckObstacles());
        StartCoroutine(SpawnObstacle());
    }

    // 캐릭터 속도로 장애물 이동
    void Update()
    {
        for (int i = 0; i < m_Obstacles.Length; i++)
        {
            if (m_Obstacles[i].isOperating)
                m_Obstacles[i].pattern.position -= new Vector3(PlayerScript.tempSpeed, 0, 0) * Time.deltaTime;
        }
    }

    // 장애물 작동 가능한지 체크
    IEnumerator CheckObstacles()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f); // 코루틴 딜레이

        while (true)
        {
            for (int i = 0; i < m_Obstacles.Length; i++)
            {
                // lastcorn의 카메라에 대한 좌표
                pos = Camera.main.WorldToViewportPoint(m_Obstacles[i].lastCorn.transform.position);

                if (m_Obstacles[i].pick) // 가장 최근에 고른 장애물이 
                {
                    if (pos.x > 1.0f) // 생성 구역(카메라 우측 끝)을 못 벗어났다면
                    {
                        canSpawn = false;
                    }
                    else // 생성 구역을 벗어났다면
                    {
                        m_Obstacles[i].pick = false;
                        canSpawn = true;
                    }
                }

                if (pos.x < -0.2f) // 카메라 좌측 끝 넘어가면 초기 위치로 복원
                {
                    pos.x = 1.2f;
                    m_Obstacles[i].isOperating = false;
                    m_Obstacles[i].pattern.position = Camera.main.ViewportToWorldPoint(pos);
                }
            }

            yield return delay;
        }
    }

    // 장애물 작동
    IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(_startTime); // 초반에 바로 장애물 안나오도록 시간 조절

        WaitForSeconds wait = new WaitForSeconds(1.0f); // 코루틴 딜레이

        while (true)
        {
            if (canSpawn)
                Pick();

            yield return wait; // 반복
        }
    }

    // 랜덤으로 작동시킬 장애물 선택
    void Pick()
    {
        int random = Random.Range(0, 101); // 0~100

        //장애물 갯수(6)에 따라 확률 조정 : 0~10%는 꽝
        if (random <= 10)
            return;
        else if (random <= 25) // 15%
        {
            m_Obstacles[0].pick = true;
            m_Obstacles[0].isOperating = true;
        }
        else if (random <= 40) // 15%
        {
            m_Obstacles[1].pick = true;
            m_Obstacles[1].isOperating = true;
        }
        else if (random <= 55) // 15%
        {
            m_Obstacles[2].pick = true;
            m_Obstacles[2].isOperating = true;
        }
        else if (random <= 70) // 15%
        {
            m_Obstacles[3].pick = true;
            m_Obstacles[3].isOperating = true;
        }
        else if (random <= 85) // 15%
        {
            m_Obstacles[4].pick = true;
            m_Obstacles[4].isOperating = true;
        }
        else if (random <= 100) // 15%
        {
            m_Obstacles[5].pick = true;
            m_Obstacles[5].isOperating = true;
        }
    }
}
