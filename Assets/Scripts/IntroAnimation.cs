using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    PlayerMove player;
    ZombieMove zombie;

    Animator playerAnim;

    public GameObject obstacleManager;
    public GameObject movement;
    public GameObject score;

    Coroutine anim;
    Coroutine anim2;

    float playerSpeed;
    float zombieSpeed;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player").GetComponent<PlayerMove>();
        zombie = GameObject.FindWithTag("zombie").GetComponent<ZombieMove>();
        playerAnim = player.gameObject.GetComponentInChildren<Animator>();
        
        // 저장
        playerSpeed = player.basicSpeed;
        zombieSpeed = zombie.speed;

        // 잠시 멈춰놓기
        playerAnim.SetFloat("isMove", 0f);
        player.basicSpeed = 0;
        zombie.speed = 0;
        
        // 코루틴 시작
        anim = StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        WaitForSeconds delay = new WaitForSeconds(0.016f); // 코루틴 딜레이

        while(true)
        {
            pos = Camera.main.WorldToViewportPoint(zombie.transform.position);
            
            if (pos.x <= 0.1f)
                Camera.main.transform.position -= new Vector3(350, 0, 0) * Time.deltaTime;
            else
            {
                yield return new WaitForSeconds(1f);
                anim2 = StartCoroutine(Anim2());
                StopCoroutine(anim);
            }

            yield return delay;
        }
    }

    IEnumerator Anim2()
    {
        WaitForSeconds delay = new WaitForSeconds(0.016f);

        while(true)
        {
            Camera.main.transform.position += new Vector3(350, 0, 0) * Time.deltaTime;

            if (Camera.main.transform.position.x >= 0.0f)
            {
                Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);

                yield return new WaitForSeconds(1f);

                score.SetActive(true);
                obstacleManager.SetActive(true);
                movement.SetActive(true);

                playerAnim.SetFloat("isMove", 1f);
                player.canMove = true;
                player.basicSpeed = playerSpeed;
                zombie.speed = zombieSpeed;

                StopCoroutine(anim2);
            }

            yield return delay;
        }
    }
}
