using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garage : MonoBehaviour
{
    public int GRID_X = 5;
    public int GRID_Y = 5;
    public int GRID_Z = 5;

    public GameObject grid_location;

    public Block block;

    public GameObject current_block;

    public Vector3Int current_vec;

    

    // Start is called before the first frame update
    void Start()
    {
        Vector3 start = new Vector3(Mathf.Round(GRID_X / 2),
            Mathf.Round(GRID_Y / 2), Mathf.Round(GRID_Z / 2));

        current_vec = Vector3Int.RoundToInt(start);
        Debug.Log(current_vec);

        for (int i = 0; i < GRID_Y; i++)
        {
            for (int j = 0; j < GRID_Z; j++)
            {
                for (int k = 0; k < GRID_X; k++)
                {
                    Vector3 spawn_point = this.transform.position;
                    spawn_point += new Vector3(k * 1, i * 1, j * 1);
                    Instantiate(grid_location, spawn_point, Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
