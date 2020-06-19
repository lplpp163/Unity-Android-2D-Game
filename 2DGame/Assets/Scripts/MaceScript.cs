using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceScript : MonoBehaviour
{
    bool isFalling = false;
    bool isFloating = false;

    Vector3 begPos, stopPos;
    AudioSource audioSource;

    private void Start()
    {
        begPos = transform.position;
        stopPos = transform.position + Vector3.down * 8;
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        FallAndFloat();
    }

    void FallAndFloat()
    {
        if (isFalling)
        {
            transform.position = Vector3.MoveTowards(transform.position, stopPos, Time.deltaTime * 10);
            if (transform.position == stopPos)
            {
                isFalling = false;
                isFloating = true;
            }
        }

        if (!isFalling && isFloating)
        {
            transform.position = Vector3.MoveTowards(transform.position, begPos, Time.deltaTime * 2);
            if (transform.position == begPos) isFloating = false;
        }
    }

    //lose
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //stop
        collision.gameObject.GetComponent<PlayerController>().enabled = false;
        //fly away
        Vector2 dir = collision.transform.position - transform.position;
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce((dir.x > 0 ? Vector2.right : Vector2.left) * 100f, ForceMode2D.Impulse);
        //lose state
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Lose();
        Destroy(collision.gameObject, 1f);
    }

    public void FallDown()
    {
        if (!isFalling && !isFloating)
        {
            audioSource.PlayDelayed(0.6f);
            isFalling = true;
        }
            
    }
}
