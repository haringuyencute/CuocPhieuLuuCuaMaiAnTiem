using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : MonoBehaviour
{
 
    GameManager gameManager;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Character"))
        {
            Destroy(gameObject);
            gameManager.TakeWatermelon();
            Debug.LogError("K");
        }
    }

}
