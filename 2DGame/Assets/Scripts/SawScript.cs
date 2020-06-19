using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.time * 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();

        //stop
        collision.gameObject.GetComponent<PlayerController>().enabled = false;
        //fly away
        Vector2 dir = collision.transform.position - transform.position;
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce((dir.x > 0 ? Vector2.right : Vector2.left) * 100f, ForceMode2D.Impulse);
        //lose state
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Lose();
        Destroy(collision.gameObject, 1f);
    }


}
