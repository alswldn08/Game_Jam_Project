using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    #region 상태 함수
    public float Speed;
    public float JumpPower;
    public bool canJump;
    public float dashPowre;
    public float DashDelayTime;
    public bool canDash;
    public bool isDash;
    public int PlayerDamage;
    public float PlayerHpMinusTime;
    public int PlayerHp;
    public float cooldownTime = 2f;
    private float nextfireTime = 0f;
    public static int noOfClicks = 0;
    float lastclickedTime = 0;
    float maxComboDelay = 1;
    public bool canslash;
    public bool isActtack;
    public int Soul;
    #endregion

    public GameObject slashEffect;

    public GameObject afterimagePrefab1;
    public GameObject afterimagePrefab2;

    public GameObject HitCollider;

    #region 컴포넌트
    Rigidbody2D rd;
    SpriteRenderer sr;
    Animator animator;
    #endregion

    void Start()
    {
        PlayerHp = 0;
        Speed = 20;
        JumpPower = 10;
        dashPowre = 50;
        DashDelayTime = 0.7f;
        PlayerHpMinusTime = 0.05f;
        canJump = true;
        canDash = true;
        isDash = false;
        canslash = true;
        isActtack = false;
        animator = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(PlayerHpSet());
    }


    void Update()
    {
        PlayerKeyInput();
    }

    public void PlayerKeyInput()
    {
        animator.SetFloat("x", Mathf.Abs(rd.velocity.x));
        animator.SetFloat("y", rd.velocity.y);

        #region 움직임
        if (Input.GetKey(KeyCode.RightArrow) && !isDash)
        {
            sr.flipX = false;
            rd.velocity = new Vector2(Speed, rd.velocity.y);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow) && !isDash)
        {
            sr.flipX = true;
            rd.velocity = new Vector2(-Speed, rd.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rd.velocity = new Vector2(0, rd.velocity.y);
        }
        #endregion
        #region 액션
        if (Input.GetKey(KeyCode.Z) && canJump)
        {
            //점프
            canJump = false;
            rd.velocity = new Vector2(rd.velocity.x, JumpPower);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack1"))
        {
            animator.SetBool("hit1", false);
            isActtack = false;
            HitCollider.SetActive(false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack2"))
        {
            animator.SetBool("hit2", false);
            isActtack = false;
            HitCollider.SetActive(false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack3"))
        {
            animator.SetBool("hit3", false);
            isActtack = false;
            HitCollider.SetActive(false);
            noOfClicks = 0;
        }
        if (Time.time - lastclickedTime > maxComboDelay)
        {
            noOfClicks = 0;
            isActtack = false;
            HitCollider.SetActive(false);
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack2") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack3"))
        {
            animator.SetBool("hit1", false);
            animator.SetBool("hit2", false);
            animator.SetBool("hit3", false);
            noOfClicks = 0;
            isActtack = false;
            HitCollider.SetActive(false);
        }
        if (Time.time > nextfireTime)
        {
            if (Input.GetKey(KeyCode.X))
            {
                //공격
                Acttack();
            }
        }
        if (Input.GetKey(KeyCode.C) && canDash)
        {
            StartCoroutine(DashDelay());
        }
        #endregion
    }

    void Acttack()
    {
        lastclickedTime = Time.time;
        noOfClicks++;
        if(noOfClicks == 1)
        {
            animator.SetBool("hit1", true);
            isActtack = true;
            if (sr.flipX)
                HitCollider.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                HitCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
            HitCollider.SetActive(true);
            if (canslash)
                StartCoroutine(slashEffectInstans(180, 0, 0));
        }

        noOfClicks = Mathf.Clamp(noOfClicks, 0, 4);

        if(noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack1"))
        {
            animator.SetBool("hit1", false);
            animator.SetBool("hit2", true);

            if (sr.flipX)
                HitCollider.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                HitCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
            HitCollider.SetActive(true);
            isActtack = true;
            if (canslash  && animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack1"))
                StartCoroutine(slashEffectInstans(-120, 0, 0));

        }
        if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack2"))
        {
            animator.SetBool("hit2", false);
            animator.SetBool("hit3", true);
            if (sr.flipX)
                HitCollider.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                HitCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
            HitCollider.SetActive(true);
            isActtack = true;
            if (canslash && animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack2"))
                StartCoroutine(slashEffectInstans(0, 0, 0));
        }
        if (noOfClicks >= 4 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack3"))
        {
            animator.SetBool("hit3", false);
            animator.SetBool("hit1", true);
            if (sr.flipX)
                HitCollider.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                HitCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
            HitCollider.SetActive(true);
            isActtack = true;
            if (canslash && animator.GetCurrentAnimatorStateInfo(0).IsName("Acttack3"))
                StartCoroutine(slashEffectInstans(180, 0, 0));
            noOfClicks = 1;
        }
    }

    IEnumerator slashEffectInstans(int x,int y,int z)
    {
        canslash = false;
        var effect = Instantiate(slashEffect, new Vector3(transform.position.x, transform.position.y + 0.14f, -5.5f), Quaternion.identity);
        if (sr.flipX)
            effect.transform.rotation = Quaternion.Euler(x, y + 180, z);
        else
            effect.transform.rotation = Quaternion.Euler(x, y, z);

        effect.transform.GetChild(0).GetComponent<VisualEffect>().playRate = 0.9f;
        Destroy(effect, 0.5f);
        yield return new WaitForSeconds(0.35f);
        canslash = true;
    }

    IEnumerator PlayerHpSet()
    {
        yield return new WaitForSeconds(PlayerHpMinusTime);
        if (PlayerHp > -100)
            PlayerHp--;

        if (PlayerHp < -100)
            PlayerHp = -100;

        Speed = -0.1f * Mathf.Abs(PlayerHp) + 20;
        if (PlayerHp > 0)
            PlayerHpManager.Instans.SetUpHpValue(Mathf.Abs(PlayerHp / 100f));
        else if(PlayerHp < 0)
            PlayerHpManager.Instans.SetDownHpValue(Mathf.Abs(PlayerHp / 100f));
        else
        {
            PlayerHpManager.Instans.SetUpHpValue(0);
            PlayerHpManager.Instans.SetDownHpValue(0);
        }

        if (PlayerHp > 100)
        {
            PlayerHp = 100;
        }

        if (PlayerHp == -100 || PlayerHp == 100)
        {
            GameOver();
        }

        UiTextManager.Instans.SetSoulText(Soul.ToString());
        UiTextManager.Instans.SetHpText(PlayerHp.ToString());

        //데미지보류
        PlayerDamage = 100 - Mathf.Abs(PlayerHp);
        StartCoroutine(PlayerHpSet());
    }

    IEnumerator DashDelay()
    {
        animator.SetBool("Dash",true);
        SoundManager.Instance.PlayEffect(0);
        canDash = false;
        if (sr.flipX)
            rd.velocity = new Vector2(transform.localScale.x * -dashPowre, rd.velocity.y);
        else
            rd.velocity = new Vector2(transform.localScale.x * dashPowre, rd.velocity.y);
        StartCoroutine(afterimage());
        isDash = true;
        yield return new WaitForSeconds(0.1f);
        isDash = false;
        animator.SetBool("Dash", false);
        rd.velocity = new Vector2(0, rd.velocity.y);
        yield return new WaitForSeconds(DashDelayTime);
        canDash = true;
        
    }

    IEnumerator afterimage()
    {
        var obj = Instantiate(afterimagePrefab1, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        if (sr.flipX == true)
            obj.GetComponent<SpriteRenderer>().flipX = true;
        Destroy(obj, 0.1f);
        yield return null;
        var obj2 = Instantiate(afterimagePrefab2, new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
        if (sr.flipX == true)
            obj2.GetComponent<SpriteRenderer>().flipX = true;
        Destroy(obj2, 0.1f);
        yield return null;
        if (isDash)
            StartCoroutine(afterimage());

    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }

        if(collision.gameObject.tag == "Soul")
        {
            Soul++;
            UiTextManager.Instans.SetSoulText(Soul.ToString());
        }
    }
}
