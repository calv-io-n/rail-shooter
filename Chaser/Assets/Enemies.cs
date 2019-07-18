using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] int pointsWorth = 12;

    //Drag and drop for runtime stores of objects
    [SerializeField] Transform parent;

    ScoreBoard scoreBoard;
 
    // Start is called before the first frame update
    void Start()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(pointsWorth);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
