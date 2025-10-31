using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonMini : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            Destroy(gameObject);
        }
    }
}
