using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCatEffect : MonoBehaviour
{
    public float startTimeBetweenSpawns;
    public GameObject echo;

    private float timeBetweenSpawns;

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenSpawns <= 0)
        {
            GameObject temp = Instantiate(echo, transform.position, echo.transform.rotation);
            Destroy(temp, 1f);
            timeBetweenSpawns = startTimeBetweenSpawns;
        } else
        {
            timeBetweenSpawns -= Time.deltaTime;
        }
    }
}
