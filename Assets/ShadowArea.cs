using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowArea : MonoBehaviour
{
    public int shadowCounter = 0;
    public Sensor sensorScript;
    private Renderer playerRenderer;
    private Color originalColor;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name + " Player entered the shadow area");
            shadowCounter++;
            if (shadowCounter > 0)
            {
                if (sensorScript != null)
                {
                    sensorScript.CheckSensorStatus();
                }

                if (playerRenderer != null)
                {
                    playerRenderer.material.color = Color.green;
                }
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name + " Player exited the shadow area");
            shadowCounter--;
            if (shadowCounter <= 0)
            {
                if (sensorScript != null)
                {
                    sensorScript.CheckSensorStatus();
                }
                
                if (playerRenderer != null)
                {
                    playerRenderer.material.color = originalColor;
                }
            }
            
        }
    }
}