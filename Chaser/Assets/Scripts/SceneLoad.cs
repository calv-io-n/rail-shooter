using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SceneLoader", 5.0f);
    }

    private void SceneLoader()
    {
        SceneManager.LoadScene(1);
    }
}
