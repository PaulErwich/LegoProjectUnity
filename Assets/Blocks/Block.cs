using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    [SerializeField]
    public BlockData data;

    private void Start()
    {
        if (data.name == "Wheel")
        {
            this.gameObject.transform.Rotate(0, 90, 0);
        }
    }

    public void Placed(GameObject parent)
    {
        transform.parent = parent.transform;
    }
}

[Serializable]
public class BlockData
{
    public List<Vector3Int> position_vec = new List<Vector3Int>();
    public List<Vector3Int> connect_points = new List<Vector3Int>();
    public string name;
    public BlockType block_type;
}

public enum BlockType {Engine, Block, Weapon, Wheel};