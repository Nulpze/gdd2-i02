using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speedX = 1f;
    public float speedY = 0.5f;
    
    private float time;
    private bool moveRight = true; // as soon as trigger collider from right/left screen bounds is hit -> reverse right to left and vice versa

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        var horz = speedX * Time.deltaTime * (moveRight ? 1 : -1);
        
        // as the enemies are parented to this GameObject, we can move all of them by just moving this GameObject
        // move right/left
        transform.Translate(horz, 0f, 0f);        
    }

    void MoveGridYDown()
    {
        transform.Translate(0f, -speedY, 0f);        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"EnemyMovement - OnTriggerEnter2D: {other.gameObject.name}");
        if (moveRight && other.gameObject.CompareTag("Right"))
        { // hit right screen bounds collider
            moveRight = false;
            MoveGridYDown();
        }
        else if (!moveRight && other.gameObject.CompareTag("Left"))
        { // hit left screen bounds collider
            MoveGridYDown();
            moveRight = true;
        }
    }
}
