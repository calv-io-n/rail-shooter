using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float dwellTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetCurrentPath();
        StartCoroutine(Mover(path));
    }


    IEnumerator Mover(List<Waypoint> path)
    {
        Debug.Log("Starting Patrol");
        yield return new WaitForSeconds(dwellTime);

        foreach (Waypoint block in path)
        {
            transform.position = new Vector3(block.transform.position.x, transform.position.y, block.transform.position.z);
            yield return new WaitForSeconds(dwellTime);
        }

        Debug.Log("Finishing Patrol");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
