using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;

    private Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        transform.Rotate(new Vector3(0, 0, 90));
    }

    // Update is called once per frame
    void Update()
    {
        var movementY = speed * Time.deltaTime;
        // The rocket is rotated so use x axis to move up
        transform.Translate(movementY, 0f, 0f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Bullet - OnTriggerEnter: {other.name}");
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            // hit enemy, hence increase score
            if (enemy.Damage())
            {
                player.IncreaseScore(enemy.initialHealth);
            }
            Destroy(gameObject);
        }
        if (other.CompareTag("Top"))
        { // bullet self-destroys when hitting top screen bounds collider
            Destroy(gameObject);
        }
    }
}
