using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Garage : MonoBehaviour
{
    public int GRID_X = 5;
    public int GRID_Y = 5;
    public int GRID_Z = 5;

    public GameObject grid_location;
    public GameObject current_block;
    public GameObject engine_block;

    public GameObject car_prefab;

    public GameObject car;

    public Vector3Int current_vec;

    public List<GameObject> grid_locations;

    public Cinemachine.CinemachineVirtualCamera free_camera;
    public GameObject spring_arm;

    private const float threshold = 0.01f;
    private float _rotationVelocity = 10.0f;

    private Vector3Int engine_point;

    public Vector3 vehicle_spawn_point = new Vector3(5, 5, 5);

    public static Garage instance;

    public GameObject[] block_prefabs;
    public int current_block_number = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        block_prefabs = Resources.LoadAll("BlockPrefabs", typeof(GameObject)).Cast<GameObject>().ToArray();
        //garage_actions.enabled = false;

        Vector3 start = new Vector3(Mathf.Round(GRID_X / 2),
            Mathf.Round(GRID_Y / 2), Mathf.Round(GRID_Z / 2));

        current_vec = Vector3Int.RoundToInt(start);
        engine_point = current_vec;
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
                    grid_locations[getIndex3D(pos)].GetComponent<GridLocation>().grid_position = pos;
                }
            }
        }
        Vector3 engine_spawn = grid_locations[getIndex3D(current_vec)].transform.position;
        //Debug.Log(engine_spawn);
        grid_locations[getIndex3D(current_vec)].GetComponent<GridLocation>().held_block = Instantiate(engine_block, engine_spawn, Quaternion.identity);
        setAvailable3D(grid_locations[getIndex3D(current_vec)].GetComponent<GridLocation>().held_block.GetComponent<Block>().data.position_vec,
            grid_locations[getIndex3D(current_vec)].GetComponent<GridLocation>().held_block.GetComponent<Block>().data.connect_points);

        car = Instantiate(car_prefab, engine_spawn, Quaternion.identity);
        car.transform.parent = grid_locations[getIndex3D(current_vec)].GetComponent<GridLocation>().held_block.transform;
        //.parent = car.transform;

        free_camera = this.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        spring_arm.transform.position = grid_locations[getIndex3D(current_vec)].transform.position;
        //free_camera.LookAt = grid_locations[getIndex3D(current_vec)].transform;
        grid_locations[getIndex3D(current_vec)].GetComponent<GridLocation>().selected = true;
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
                grid_locations[getIndex3D(new_pos)].GetComponent<GridLocation>().current_status = Status.Occupied;
            }
        }

        foreach(Vector3Int pos in connections)
        {
            Vector3Int new_pos = pos + current_vec;
            int index = getIndex3D(new_pos);
            if (withinRange3D(new_pos))
            {
                if (grid_locations[index].GetComponent<GridLocation>().current_status != Status.Occupied)
                {
                    grid_locations[index].GetComponent<GridLocation>().current_status = Status.Available;
                }
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

    public void OnMoveUpDown(InputValue value)
    {
        Debug.Log("move up/down");
        Vector3Int new_pos = current_vec;
        new_pos.y += Mathf.RoundToInt(value.Get<float>());
        if (withinRange3D(new_pos))
        {
            grid_locations[getIndex3D(current_vec)].GetComponent<GridLocation>().selected = false;
            current_vec = new_pos;
            if (current_block)
            {
                current_block.transform.position = this.transform.position + current_vec;
            }
            
            Debug.Log("changed vector");
        }
    }

    public void OnMoveForwardBackward(InputValue value)
    {
        Debug.Log("move forward/backward");
        Vector3Int new_pos = current_vec;
        new_pos.z += Mathf.RoundToInt(value.Get<float>());
        if (withinRange3D(new_pos))
        {
            grid_locations[getIndex3D(current_vec)].GetComponent<GridLocation>().selected = false;
            current_vec = new_pos;
            if (current_block)
            {
                current_block.transform.position = this.transform.position + current_vec;
            }
            Debug.Log("changed vector");
        }
    }

    public void OnMoveLeftRight(InputValue value)
    {
        Debug.Log("move left/right");
        Vector3Int new_pos = current_vec;
        new_pos.x += Mathf.RoundToInt(value.Get<float>());
        if (withinRange3D(new_pos))
        {
            grid_locations[getIndex3D(current_vec)].GetComponent<GridLocation>().selected = false;
            current_vec = new_pos;
            if (current_block)
            {
                current_block.transform.position = this.transform.position + current_vec;
            }
            Debug.Log("changed vector");
        }
    }

    public void OnChangeBlock(InputValue value)
    {
        Debug.Log("change");
        current_block_number += Mathf.RoundToInt(value.Get<float>());
        if (current_block_number >= block_prefabs.Length)
        {
            current_block_number = 1;
        }
        else if (current_block_number < 1)
        {
            current_block_number = block_prefabs.Length - 1;
        }
        Destroy(current_block);
        current_block = null;
        Vector3 spawn_pos = grid_locations[getIndex3D(current_vec)].transform.position;
        current_block = Instantiate(block_prefabs[current_block_number], spawn_pos, Quaternion.identity);
    }

    public void OnGarageLook(InputValue value)
    {
        Vector2 new_rotation = value.Get<Vector2>();

        if (new_rotation.sqrMagnitude >= threshold)
        {
            spring_arm.transform.Rotate(new Vector3(new_rotation.y, new_rotation.x, 0)
                * Time.deltaTime * _rotationVelocity);
        }
    }

    public void OnPlaceBlock()
    {
        if (current_block)
        {
            Block block_data = current_block.GetComponent<Block>();

            foreach (Vector3Int point in block_data.data.position_vec)
            {
                Vector3Int new_pos = current_vec + point;
                if (!withinRange3D(new_pos))
                {
                    return;
                }
            }

            foreach (Vector3Int point in block_data.data.position_vec)
            {
                Vector3Int new_pos = current_vec + point;
                if (grid_locations[getIndex3D(new_pos)].GetComponent<GridLocation>().current_status == Status.Available)
                {
                    break;
                }
                else
                {
                    return;
                }

            }

            setAvailable3D(block_data.data.position_vec, block_data.data.connect_points);

            foreach (Vector3Int point in block_data.data.position_vec)
            {
                Vector3Int new_pos = current_vec + point;
                grid_locations[getIndex3D(current_vec)].GetComponent<GridLocation>().held_block = current_block;
            }

            block_data.Placed(grid_locations[getIndex3D(engine_point)].GetComponent<GridLocation>().held_block);
            current_block = null;
        }
    }

    public void OnSpawnBlock()
    {
        if (!current_block)
        {
            Debug.Log("successful block spawn");
            Vector3 spawn_pos = grid_locations[getIndex3D(current_vec)].transform.position;
            current_block = Instantiate(block_prefabs[current_block_number], spawn_pos, Quaternion.identity);
        }
    }

   

    public void OnBuildVehicle()
    {
        grid_locations[getIndex3D(engine_point)].GetComponent<GridLocation>().held_block.GetComponent<Rigidbody>()
            .useGravity = true;
        grid_locations[getIndex3D(engine_point)].GetComponent<GridLocation>().held_block.GetComponent<Rigidbody>()
            .isKinematic = false;
        Debug.Log(vehicle_spawn_point);
        grid_locations[getIndex3D(engine_point)].GetComponent<GridLocation>().held_block.transform.position = vehicle_spawn_point;
        Debug.Log(grid_locations[getIndex3D(engine_point)].GetComponent<GridLocation>().held_block.transform.position);
    }

    public void EnterGarage()
    {
        grid_locations[getIndex3D(engine_point)].GetComponent<GridLocation>().held_block.GetComponent<Rigidbody>()
            .useGravity = false;
        grid_locations[getIndex3D(engine_point)].GetComponent<GridLocation>().held_block.GetComponent<Rigidbody>()
            .isKinematic = true;
        vehicle_spawn_point = car.transform.position;
        grid_locations[getIndex3D(engine_point)].GetComponent<GridLocation>().held_block.transform.position = grid_locations[getIndex3D(engine_point)].transform.position;
        grid_locations[getIndex3D(engine_point)].GetComponent<GridLocation>().held_block.transform.rotation = Quaternion.identity;
    }
}
