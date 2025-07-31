using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int maxGhosts = 5;
    [SerializeField] private Transform spawnPoint;

    private int counter = 1;

    private List<GameObject> ghostList = new List<GameObject>();

    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not assigned in the Ghost script.");
            return;
        }
        SpawnGhosts();
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
            GameObject ghost = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            ghost.SetActive(true);
            ghostList.Add(ghost);

            var pc = ghost.GetComponent<PlayerController>();
            if (pc != null)
                pc.enabled = (i == 0); // Enable controller only for the first ghost

            var rb = ghost.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.simulated = (i == 0); // Enable physics only for the first ghost
        }
    }

    void ActivateGhosts()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            GameObject ghost = ghostList[i];
            var pc = ghost.GetComponent<PlayerController>();
            var rb = ghost.GetComponent<Rigidbody2D>();

            bool isActiveGhost = (i == counter);

            if (pc != null)
                pc.enabled = isActiveGhost;

            if (rb != null)
                rb.simulated = isActiveGhost;
        }

        counter++;
        if (counter >= maxGhosts)
            counter = 0;
    }
    void ActivateGhostsBack()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            GameObject ghost = ghostList[i];
            var pc = ghost.GetComponent<PlayerController>();
            var rb = ghost.GetComponent<Rigidbody2D>();

            bool isActiveGhost = (i == counter);

            if (pc != null)
                pc.enabled = isActiveGhost;

            if (rb != null)
                rb.simulated = isActiveGhost;
        }

        counter--;
        if (counter < 0)
            counter = maxGhosts - 1;
    }
}
