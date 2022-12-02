using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Garage : MonoBehaviour
{
    public int GRID_X = 5;
    public int GRID_Y = 5;
    public int GRID_Z = 5;

    public GameObject grid_location;
    public GameObject current_block;
    public GameObject engine_block;

    public Vector3Int current_vec;

    public List<GameObject> grid_locations;

    public Cinemachine.CinemachineVirtualCamera free_camera;

    public InputActionMap input_map;
    

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
                    grid_locations.Add(Instantiate(grid_location, spawn_point, Quaternion.identity));
                    Vector3Int pos = new Vector3Int(k, i, j);
                    grid_locations[getIndex3D(pos)].GetComponent<GirdLocation>().grid_position = pos;
                }
            }
        }
        Vector3 engine_spawn = grid_locations[getIndex3D(current_vec)].transform.position;
        //Debug.Log(engine_spawn);
        grid_locations[getIndex3D(current_vec)].GetComponent<GirdLocation>().held_block = Instantiate(engine_block, engine_spawn, Quaternion.identity);
        setAvailable3D(grid_locations[getIndex3D(current_vec)].GetComponent<GirdLocation>().held_block.GetComponentInChildren<Block>().data.position_vec,
            grid_locations[getIndex3D(current_vec)].GetComponent<GirdLocation>().held_block.GetComponentInChildren<Block>().data.connect_points);

        free_camera = this.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3Int new_pos = current_vec;
            new_pos.x += 1;
            if (withinRange3D(new_pos))
            {
                current_vec = new_pos;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            current_vec.x -= 1;
        }

        free_camera.LookAt = grid_locations[getIndex3D(current_vec)].transform;
    }

    int getIndex3D(Vector3Int vector)
    {
        //Debug.Log(vector);
        int index = (vector.y * (GRID_Z * GRID_X)) + (vector.z * GRID_X) + vector.x;
        //Debug.Log(index);
        return index;
    }

    void setAvailable3D(List<Vector3Int> current, List<Vector3Int> connections)
    {
        foreach(Vector3Int pos in current)
        {
            Vector3Int new_pos = pos + current_vec;
            if (withinRange3D(new_pos))
            {
                grid_locations[getIndex3D(new_pos)].GetComponent<GirdLocation>().current_status = Status.Occupied;
            }
        }

        foreach(Vector3Int pos in connections)
        {
            Vector3Int new_pos = pos + current_vec;
            if (withinRange3D(new_pos))
            {
                grid_locations[getIndex3D(new_pos)].GetComponent<GirdLocation>().current_status = Status.Available;
            }
        }
    }

    bool withinRange3D(Vector3Int position)
    {
        if (position.x < 0 || position.x >= GRID_X)
        {
            return false;
        }
        if (position.y < 0 || position.y >= GRID_Y)
        {
            return false;
        }
        if (position.z < 0 || position.z >= GRID_Z)
        {
            return false;
        }

        return true;
    }
}
