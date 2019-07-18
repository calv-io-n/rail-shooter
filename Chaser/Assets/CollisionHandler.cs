using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float leveLoadDelay = 1f;
    [SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider collider)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadLevel", leveLoadDelay);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
