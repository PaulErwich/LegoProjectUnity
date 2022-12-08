using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBlockResources : MonoBehaviour
{
    public Object[] block_prefabs;

    // Start is called before the first frame update
    void Start()
    {
        block_prefabs = Resources.LoadAll("BlockPrefabs");
        //Debug.Log(gameObjects);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
