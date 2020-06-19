using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<MaceScript>().FallDown();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        gameObject.GetComponentInParent<MaceScript>().FallDown();
    }
}
