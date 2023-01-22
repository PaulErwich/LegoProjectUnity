using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocation : MonoBehaviour
{
    public GameObject held_block = null;
    public Status current_status = Status.Unavailable;
    public Vector3Int grid_position;

    public bool selected;

    private Color c1 = Color.white;
    private Color c2 = Color.red;
    private Color c3 = Color.green;
    private Color current;

    // Start is called before the first frame update
    void Start()
    {
        current = c1;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            switch(current_status)
            {
                case Status.Occupied:
                {
                        current = c2;
                        break;
                    }
                case Status.Available:
                    {
                        current = c3;
                        break;
                    }
                case Status.Unavailable:
                    {
                        current = c2;
                        break;
                    }
            }
        }
        else
        {
            current = c1;
        }

        foreach (Transform child_transform in transform)
        {
            GameObject child = child_transform.gameObject;
            LineRenderer line = child.GetComponent<LineRenderer>();
            line.SetColors(current, current);
            if (selected)
            {
                line.SetWidth(0.1f, 0.1f);
            }
            else
            {
                line.SetWidth(0.025f, 0.025f);
            }
            
        }
    }
}

public enum Status {Available, Unavailable, Occupied};