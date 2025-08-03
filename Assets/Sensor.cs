using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sensor : MonoBehaviour
{
    public List<GameObject> sensorList;
    [SerializeField] private GameObject door;
    public AudioSource sensorAudioSource;
    public AudioClip sensorSoundClip;

    // A private variable to track the state of the door.
    // We'll initialize it to true because the door starts as open (active).
    private bool isDoorOpen = true;

    void Update()
    {
        CheckSensorStatus();
    }

    public void CheckSensorStatus()
    {
        int activeSensorCount = 0;
        for (int i = 0; i < sensorList.Count; i++)
        {
            ShadowArea sa = sensorList[i].GetComponent<ShadowArea>();
            // Here we just count how many sensors are currently active (shadowCounter == 1)
            if (sa != null && sa.shadowCounter == 1)
            {
                activeSensorCount++;
            }
            //Debug.Log(activeSensorCount);
        }

        // Now we check if the count of active sensors matches the total number of sensors.
        // This is the condition for closing the door.
        if (activeSensorCount >= sensorList.Count)
        {
            // The door needs to be closed.
            door.SetActive(false);

            // Check if the door's state has just changed.
            // We only want to play the sound once when the door closes.
            if (isDoorOpen == true)
            {
                // Play the sound on this transition.
                sensorAudioSource.PlayOneShot(sensorSoundClip);
                
                // Update the state variable to reflect the new state.
                isDoorOpen = false;
            }
        }
        else
        {
            // The door needs to be opened.
            door.SetActive(true);

            // Check if the door's state has just changed.
            // We only want to play the sound once when the door opens.
            // You can add a different sound here for opening the door if you have one.
            if (isDoorOpen == false)
            {
                // Play the sound on this transition.
                sensorAudioSource.PlayOneShot(sensorSoundClip); 
                
                // Update the state variable to reflect the new state.
                isDoorOpen = true;
            }
        }
    }
}