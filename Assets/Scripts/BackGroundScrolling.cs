using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    [SerializeField] Transform[] m_Backgrounds = null;

    float leftPosX = 0f;
    float rightPosX = 0f;

    PlayerMove PlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = GameObject.FindWithTag("player").GetComponent<PlayerMove>();

        // length = 맵 길이, leftPosX = 좌측 한계선, rightPosX = 우측 한계선
        float length = m_Backgrounds[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x * this.transform.localScale.x;
        leftPosX = -length;
        rightPosX = length * m_Backgrounds.Length;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_Backgrounds.Length; i++)
        {
            m_Backgrounds[i].position -= new Vector3(PlayerScript.tempSpeed, 0, 0) * Time.deltaTime;
            if (m_Backgrounds[i].position.x < leftPosX)
            {
                // 맵이 leftPosX 넘어가면 rightPosX로 넘겨서 무한맵 구현
                Vector3 selfPos = m_Backgrounds[i].position;
                selfPos.Set(selfPos.x + rightPosX, selfPos.y, selfPos.z);
                m_Backgrounds[i].position = selfPos;
            }
        }
    }
}
