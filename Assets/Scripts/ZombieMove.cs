using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    public float speed; // 좀비 속도
    float diff; // 좀비 속도 - 플레이어 속도
    float distance; // 플레이어.x - 좀비.x (거리)
    private float _maxDistance; // 플레이어와 좀비 간 최대 거리

    PlayerMove PlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = GameObject.FindWithTag("player").GetComponent<PlayerMove>();
        _maxDistance = 2 * Camera.main.orthographicSize * Camera.main.aspect * 1.5f;
    }
    
    void FixedUpdate()
    {
        diff = speed - PlayerScript.tempSpeed;
        distance = PlayerScript.transform.position.x - this.transform.position.x;

        Move();
    }

    void Move()
    {
        if (diff > 0) // 좀비가 더 빠름
            this.transform.position += new Vector3(diff, 0, 0) * Time.deltaTime;
        else // 플레이어가 더 빠름
        {
            if (distance >_maxDistance) // 최대 거리 벗어남
                this.transform.position = new Vector3(PlayerScript.transform.position.x - distance, this.transform.position.y, 0);
            else
                this.transform.position += new Vector3(diff, 0, 0) * Time.deltaTime;
        }
    }
}
