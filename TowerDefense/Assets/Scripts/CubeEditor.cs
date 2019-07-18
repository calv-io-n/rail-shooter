using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
//Note that this shows that the cube editor is dependent on the Waypoint Component (Script)
//THis line adds script anytime we apply the cube editor script
[RequireComponent(typeof(Waypoint))]
public class CubeEditor: MonoBehaviour
{
    
    TextMesh textMesh;
    Waypoint waypoint;

    void Awake()
    {
        //Get waypoint component early in the flow of control
        waypoint = GetComponent<Waypoint>();
    }

    void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }
    // Update is called once per frame
    void Update()
    {
        //This is for labelling the text mesh
        NameUpdate();

        //This is the decoupled snapping editor script
        SnappingResolution();
    }

    private void NameUpdate()
    {
        string textlabel = waypoint.GetGridPos().x + ", " + waypoint.GetGridPos().y;
        textMesh.text = textlabel;
        this.name = textlabel;
    }

    private void SnappingResolution()
    {
        transform.position = new Vector3(waypoint.GetGridPos().x * Waypoint.gridScale, 0.0f, waypoint.GetGridPos().y * Waypoint.gridScale);
    }
}
