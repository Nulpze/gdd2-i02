using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int initialHealth = 0;
    private float speed = 0f;
    private int health = 0;
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.002f;
    public float shake_intensity = .3f;

    private float temp_shake_intensity = 0;

    public void Awake()
    {
        speed = Random.Range(1f, 50f);
        float scale = Random.Range(0.2f, 0.7f);
        transform.localScale = new Vector3(scale, scale, 0);
        health = (int)(scale * 10 / 2);
        initialHealth = health;
        GameManager.MaxScore += initialHealth;
    }

    public void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * speed);
        if (temp_shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);
            temp_shake_intensity -= shake_decay;
        }
    }

    void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        temp_shake_intensity = shake_intensity;
    }

    public bool Damage()
    {
        health--;
        GameManager.Instance.ExplosionEffect(transform.position);
        Shake();
        if (health > 0)
        {
            transform.localScale = transform.localScale / 1.5f;
            return false;
        }
        Destroy(gameObject, 0.5f);
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameManager.Instance.PlayDestroyEnemy();
        }
    }
}
