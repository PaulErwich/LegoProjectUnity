using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    [SerializeField]
    public BlockData data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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