using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class LoadBlockResources : MonoBehaviour
{
    public GameObject[] block_prefabs;
    public GameObject button;

    //private GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        block_prefabs = Resources.LoadAll("BlockPrefabs", typeof(GameObject)).Cast<GameObject>().ToArray();
        Debug.Log(block_prefabs);

        foreach (GameObject block in block_prefabs)
        {
            GameObject next_button = Instantiate(button, new Vector3(0, 0, 0), Quaternion.identity);
            //next_button.GetComponent<CreateButton>().setupButton(block);
            //next_button.GetComponentInChildren<TextMeshPro>().color = Color.green;
            next_button.transform.SetParent(this.transform);
            //buttons.Append<GameObject>(next_button);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
