using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float jumpPower;

    public float maxSpeed;
    public float basicSpeed; // 900
    public float minSpeed;
    public float tempSpeed;

    bool isGround = false;
    bool isUnBeatTime = false;
    public bool isDead = false;
    public bool canMove = false;

    public bool inputRun;
    public bool inputJump;

    Animator Moveanim;
    Rigidbody2D rigid;
    new SpriteRenderer renderer;
    AudioSource jump_effect;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        Moveanim = gameObject.GetComponentInChildren<Animator>();
        jump_effect = gameObject.GetComponent<AudioSource>();
    }

    // 땅에 닿았을 시에만 다시 점프할 수 있도록
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ground"))
            isGround = true;
    }
    
    //장애물 또는 좀비에 닿았을 시 활성화
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("obstacle") && !isUnBeatTime)
        {
            isUnBeatTime = true; // 무적 설정
            StartCoroutine(UnBeatTime());
        }

        if(col.gameObject.CompareTag("zombie"))
        {
            Time.timeScale = 0f;
            StartCoroutine(GameOver());
        }
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    // 장애물 부딫혔을 시 잠시 동안 무적 시간이 됨
    IEnumerator UnBeatTime()
    {
        tempSpeed = minSpeed; // 속도 감소
        int countTime = 0;

        // 캐릭터가 깜빡깜빡거리며 무적 시간임을 표현
        while(countTime < 10)
        {
            if (countTime % 2 == 0)
                renderer.color = new Color32(255, 255, 255, 90);
            else
                renderer.color = new Color32(255, 255, 255, 180);
            yield return new WaitForSeconds(0.1f);

            countTime++;
        }

        tempSpeed = basicSpeed; // 속도 복원
        renderer.color = new Color(255, 255, 255, 255); // 투명도 복원
        isUnBeatTime = false; // 무적 해제
        
        yield return null;
    }

    IEnumerator GameOver()
    {
        Moveanim.SetBool("isDeath", true);

        
        int wait = 0;
        isDead = true;
        canMove = false;

        while (wait < 100)
        {
            wait += 1;
            yield return null;
        }
        
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    void Move()
    {
        if (isUnBeatTime || !canMove)
            return;

        if (inputRun || Input.GetAxisRaw("Horizontal") > 0)
        {
            tempSpeed = maxSpeed;
        }
        else
        {
            if(!isUnBeatTime) tempSpeed = basicSpeed;
        }
    }

    void Jump()
    {
        if (!canMove)
            return;

        if (inputJump || Input.GetButton("Jump"))
        {
            if (isGround)
            {
                rigid.velocity = Vector2.zero;
                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                isGround = false;

                jump_effect.Play();
            }
        }
    }

    public void RBtnDown()
    {
        inputRun = true;

    }
    public void RBtnUp()
    {
        inputRun = false;
    }

    public void JBtnDown()
    {
        inputJump = true;
    }

    public void JBtnUp()
    {
        inputJump = false;
    }
}