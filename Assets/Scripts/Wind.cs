using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private float windDuration;
    public float windRotation;
    private float windSpeed;

    public GameObject windPos;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        WindChange();
    }

    void FixedUpdate()
    {
        if(windRotation <= 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void WindChange()
    {
        // 1 = right | 2 = left
        int rotDirection = Random.Range(0,3);
        if(rotDirection == 0)
        {
            windRotation = Random.Range(-5f, 5f);
        }
        else if(rotDirection == 1)
        {
            windRotation = Random.Range(10f, 20f);
        }
        else
        {
            windRotation = Random.Range(-10f, -20f);
        }
        windSpeed = Random.Range(0f, 5f);
        windDuration = Random.Range(3f, 7f);
        Invoke("WindChange",windDuration);
    }
    public float ReturnWindSpeed()
    {
        return windSpeed;
    }
    public float ReturnWindRotation()
    {
        return windRotation;
    }
    public float ReturnWindDuration()
    {
        return windDuration;
    }

}
