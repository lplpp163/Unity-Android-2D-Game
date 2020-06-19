using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip step, jump, ground, attk;
    public GameObject joy;
    VariableJoystick variableJoystick;


    public float speed = 5f;
    public float jumpStrngth = 5f;
    Animator m_animator;
    bool isGround;
    Rigidbody2D rb;
    public LayerMask groundLayer, enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Vector2 pos;
        pos.x = PlayerPrefs.GetFloat("PosX", -28f);
        pos.y = PlayerPrefs.GetFloat("PosY", -20f);
        transform.position = GameManager.gameMode == 0 ? new Vector2(-28f,-20f) : pos;

        variableJoystick = joy.GetComponent<VariableJoystick>();

    }

    public void Update()
    {
        JumpAndFall();
        Move();
        GroundCheck();

        m_animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        m_animator.SetFloat("VelocityY", rb.velocity.y);
    }

    void Move()
    {
        if (variableJoystick.Horizontal != 0)
        {
            rb.velocity = new Vector2(speed * (variableJoystick.Horizontal), rb.velocity.y);
            //transform.Translate(Vector2.right * Mathf.Abs(variableJoystick.Horizontal) * Time.deltaTime * speed);
            transform.eulerAngles = Vector3.up * (variableJoystick.Horizontal < 0 ? 180 : 0);
        }
    }
    void JumpAndFall()
    {
        if (isGround && (variableJoystick.Vertical >0.5))
        {
            m_animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x , 13f);
        }
    }
    void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(transform.position + Vector3.down * 0.8f, 0.05f, groundLayer);
        m_animator.SetBool("isGround", isGround);
    }


    public void StepSound()
    {
        audioSource.PlayOneShot(step);
    }

    public void Attack()
    {
        audioSource.PlayOneShot(attk);
        StartCoroutine(LightsOn());
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4.0f, enemyLayer);
        foreach(Collider2D col in colliders) Destroy(col.gameObject);
    }

    IEnumerator LightsOn()
    {
        Light[] lights = GetComponentsInChildren<Light>();
        foreach (Light light in lights) light.enabled = true;
        yield return new WaitForSeconds(0.5f);
        foreach (Light light in lights) light.enabled = false;
    }
}
