using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{

    public int index;
    public GameObject coinEffect;
    public GameManager gm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(coinEffect, transform.position, Quaternion.identity);
            gm.Coin++;

            gm.CoinExt = gm.CoinExt.Remove(index, 1);
            gm.CoinExt = gm.CoinExt.Insert(index,"0");
            Destroy(gameObject);
        }
    }
}
