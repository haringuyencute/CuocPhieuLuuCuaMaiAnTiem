using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int playerHealth = 3; // Máu c?a nhân v?t
    public int playerScore = 0;  // ?i?m c?a nhân v?t
    public Transform respawnPoint; // ?i?m h?i sinh
    public Text HeartText;
    public Text WatermelonText;
    private int numWatermalon = 0;
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // ??t GameManager hi?n t?i làm instance
            DontDestroyOnLoad(gameObject); // Không phá hu? khi chuy?n c?nh
        }
        else
        {
            Destroy(gameObject); // N?u ?ã t?n t?i, phá hu? GameObject m?i
        }
    }
    
    public void TakeDamage()
    {
        playerHealth--;
        HeartText.text = playerHealth.ToString();
        if (playerHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }

    public void PlayerDie()
    {
        Time.timeScale = 0;
    }

    public void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = respawnPoint.position;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    public void TakeWatermelon()
    {
        Debug.LogError("K nhat dc");
        numWatermalon++;
        WatermelonText.text = numWatermalon.ToString();
    }
}
