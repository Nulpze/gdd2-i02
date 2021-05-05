using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Set this speed via the Unity Inspector Window
    public float speed = 1f;
    public GameObject bulletPrefab;
    public TMP_Text scoreText;
    public GameObject wonLostPanel;
    public TMP_Text wonLostText;
    public float shootCooldown = 0.3f;

    private int _score = 0;
    private GameObject echoCat;
    private float shootTimer;

    private void Start()
    {
        echoCat = GetComponent<FadeCatEffect>().echo;
        shootTimer = shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            HandleShooting();
        }
    }
    
    void HandleMovement()
    {
        // Get the horizontal axis.
        // By default this is mapped to the arrow keys but also to A and D.
        // The value is in the range -1 to 1
        float horz = Input.GetAxis("Horizontal");
        float dir = horz;
        if (horz < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            echoCat.transform.rotation = Quaternion.Euler(0, 180, 0);

            dir *= -1;
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            echoCat.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // Mulitply with speed factor
        dir *= speed;
        // Convert from meters per frame to meters per second
        dir *= Time.deltaTime;
        
        // Move translation along the object's z-axis
        transform.Translate(dir, 0, 0);
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Spawn Bullet
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            shootTimer = shootCooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Player - OnTriggerEnter2D: {other.gameObject.name}");
        if (other.CompareTag("Enemy"))
        {
            // game lost
            Lost();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log($"Player - OnCollisionEnter2D: {other.gameObject.name}");
    }

    public void IncreaseScore(int value = 1)
    {
        _score += value;
        scoreText.text = $"Score: {_score}";
        if (_score == GameManager.MaxScore)
        {
            Won();
        }
    }

    public void Won()
    {
        wonLostPanel.SetActive(true);
        wonLostText.text = "You Won!";
    }

    public void Lost()
    {
        wonLostPanel.SetActive(true);
        wonLostText.text = "You Lost!";
    }
}
