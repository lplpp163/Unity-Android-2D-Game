using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectChest : MonoBehaviour
{
    public GameObject chestEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            //stop player
            collision.gameObject.GetComponent<PlayerController>().enabled = false;

            //open chest
            GetComponent<Animator>().SetBool("open",true);
            Instantiate(chestEffect, transform.position, Quaternion.identity);

            //win state
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().Win();

            Destroy(gameObject, 1f);
        }
    }

}
