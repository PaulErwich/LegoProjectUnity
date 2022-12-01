using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLine : MonoBehaviour
{

    LineRenderer line;

    public Material material;
    //GameObject attached_to;

    public int GRID_X = 4;
    public int GRID_Y = 4;
    public int GRID_Z = 4;

    public int Segment_X = 5;
    public int Segment_Y = 5;
    public int Segment_Z = 5;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //attached_to = this.
        line = gameObject.AddComponent<LineRenderer>();

        line.useWorldSpace = false;
        line.positionCount = GRID_X * Segment_X + GRID_Y * Segment_Y + GRID_Z * Segment_Z;
        line.material = material;
        line.textureMode = LineTextureMode.RepeatPerSegment;
        line.widthMultiplier = 0.1f;

        float x = 0;
        float y = 0;
        float z = 0;

        int index = 0;
        for (x = 0; x < 1; x += 0.2f)
        {
            Vector3 position = new Vector3(x, y, z);
            Debug.Log(position);
            line.SetPosition(index, position);
            index++;
        }

        for (z = 0; z < 1; z += 0.2f)
        {
            Vector3 position = new Vector3(x, y, z);
            Debug.Log(position);
            line.SetPosition(index, position);
            index++;
        }

        for (x = 1; x > 0; x -= 0.2f)
        {
            Vector3 position = new Vector3(x, y, z);
            Debug.Log(position);
            line.SetPosition(index, position);
            index++;
        }

        for (z = 1; z > 0; z -= 0.2f)
        {
            Vector3 position = new Vector3(x, y, z);
            Debug.Log(position);
            line.SetPosition(index, position);
            index++;
        }


        /*
        int current_index = 0;
        for (int i = 0; i < GRID_X; i++)
        {
            for (int j = 0; j < Segment_X; j++)
            {
                Vector3 position = new Vector3(0.2f * j, i, 0);
                line.SetPosition(current_index, position);
                current_index++;
            }
        }*/
        /*
        for (int i = 0; i < GRID_Y; i++)
        {
            for (int j = 0; j < GRID_Z; j++)
            {
                for (int k = 0; k < GRID_X; k++)
                {
                    int index = i * GRID_Z + j * GRID_X + k;
                    line.SetPosition(index, new Vector3(i / 5, j / 5, k / 5));
                }
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
