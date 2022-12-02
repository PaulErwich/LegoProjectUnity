using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirdLocation : MonoBehaviour
{
    public GameObject held_block = null;
    public Status current_status = Status.Unavailable;
    public Vector3Int grid_position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Status {Available, Unavailable, Occupied};