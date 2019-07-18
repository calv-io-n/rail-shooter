using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    List<Waypoint> currentPath = new List<Waypoint>();
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    [SerializeField] Waypoint startPos;
    [SerializeField] Waypoint endPos;

    private bool isPathing = true;
    Waypoint searchCenter;
    Color color;


    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    
    private void PathFind()
    {
        queue.Enqueue(startPos);

        while(queue.Count > 0 && isPathing)
        {
            searchCenter = queue.Dequeue();
            // removed from the queue is considered explored
            if (searchCenter.Equals(endPos))
            {
                Debug.Log("Endpoint Reached."); // todo: remove this when running
                isPathing = false;
            }
            ExploreNeighbors(searchCenter);
            searchCenter.isExplored = true;
        }
    }

    public List<Waypoint> GetCurrentPath()
    {
        LoadBlocks();
        PathFind();  // todo: rename breadth first search
        CreatePath();
        return currentPath;
    }
    
    private void CreatePath()
    {
        Waypoint temp = endPos;
        currentPath.Add(temp);
        do
        {
            temp = temp.exploredFrom;
            currentPath.Add(temp);
        }
        while (!temp.Equals(startPos));
        currentPath.Reverse();
        print(currentPath);

    }

    private void ExploreNeighbors(Waypoint from)
    {
        // Stop if ending spot has been found
        if (!isPathing) { return; }

        // Temp variable for up, right, down, left
        Vector2Int directed;
        foreach (Vector2Int direction in directions)
        {
            //Current position + One unit up, right, down, left
            directed = direction + from.GetGridPos();
            // If there is a block/gridspace that exists on the map
            if (grid.ContainsKey(directed))
            {
                // Get from the environment grid dictionary
                Waypoint neighbor = grid[directed];
                // if neighbor is not explored add to queue, if it hasn't already been added, dont add it again
                if (!neighbor.isExplored && !queue.Contains(neighbor))
                {
                    // Enqueue to the breadth search first queue
                    queue.Enqueue(neighbor);
                    // Update the waypoint history to include it's origin (current de-queue)
                    neighbor.exploredFrom = searchCenter;
                    print("Queuing: " + directed.x + ", " + directed.y);
                }
            }
        }
    }

    private void LoadBlocks()
    {
        // will be an array, var used to wildcard the type
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            if (!grid.ContainsKey(waypoint.GetGridPos()))
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
                // print("Added");

                color = Color.grey;
                if (waypoint.GetGridPos().Equals(startPos.GetGridPos()))
                {
                    color = Color.cyan;
                }
                else if (waypoint.GetGridPos().Equals(endPos.GetGridPos()))
                {
                    color = Color.yellow;
                }

                waypoint.SetTopColor(color);
       
            }
            else
            {
                print("Duplicate " + waypoint);
            }
        }
        print("Environment contains: " + grid.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
