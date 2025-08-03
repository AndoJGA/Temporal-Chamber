using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ghost : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int maxGhosts = 5;
    [SerializeField] private List<Transform> spawnPoint;
    [SerializeField] private TextMeshProUGUI ghostCounterText;
    [SerializeField] private Color[] ghostColors;
    [SerializeField] private GameObject ghostImage;
    [SerializeField] private AudioSource ghostAudioSource;

    private int counter = 0;
    private List<GameObject> ghostList = new List<GameObject>();

    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not assigned in the Ghost script.");
            return;
        }
        SpawnGhosts();
        if (ghostCounterText == null)
        {
            Debug.LogError("Ghost counter text is not assigned in the Ghost script.");
            return;
        }
        ghostCounterText.text = "Ghost: " + (counter + 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ActivateGhosts();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ActivateGhostsBack();
        }
    }

    void SpawnGhosts()
    {
        for (int i = 0; i < maxGhosts; i++)
        {
            GameObject ghost = Instantiate(playerPrefab, spawnPoint[i].position, Quaternion.identity);
            ghost.SetActive(true);
            ghostList.Add(ghost);

            if (i < ghostColors.Length)
            {
                var renderer = ghost.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = ghostColors[i];
                }
            }

            var pc = ghost.GetComponent<PlayerController>();
            if (pc != null)
                pc.enabled = (i == 0);
        }

        // Set the ghostImage color to match the initial active ghost (ghost 0)
        if (ghostImage != null && ghostColors.Length > 0)
        {
            ghostImage.GetComponent<Image>().color = ghostColors[0];
        }
    }

    void ActivateGhosts()
    {
        counter++;
        if (counter >= maxGhosts)
        {
            counter = 0;
        }

        for (int i = 0; i < ghostList.Count; i++)
        {
            GameObject ghost = ghostList[i];
            var pc = ghost.GetComponent<PlayerController>();
            bool isActiveGhost = (i == counter);
            ghostAudioSource.Play();
            if (pc != null)
                pc.enabled = isActiveGhost;
        }

        // Set the ghostImage color to match the newly active ghost
        if (ghostImage != null && counter < ghostColors.Length)
        {
            ghostImage.GetComponent<Image>().color = ghostColors[counter];
        }

        ghostCounterText.text = "Ghost: " + (counter + 1);
    }

    void ActivateGhostsBack()
    {
        counter--;
        if (counter < 0)
            counter = maxGhosts - 1;

        for (int i = 0; i < ghostList.Count; i++)
        {
            GameObject ghost = ghostList[i];
            var pc = ghost.GetComponent<PlayerController>();
            bool isActiveGhost = (i == counter);
            ghostAudioSource.Play();
            if (pc != null)
                pc.enabled = isActiveGhost;
        }

        // Set the ghostImage color to match the newly active ghost
        if (ghostImage != null && counter < ghostColors.Length)
        {
            ghostImage.GetComponent<Image>().color = ghostColors[counter];
        }

        ghostCounterText.text = "Ghost: " + (counter + 1);
    }
}