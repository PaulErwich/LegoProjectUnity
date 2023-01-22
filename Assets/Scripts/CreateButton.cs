using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setupButton(GameObject block)
    {
        this.GetComponentInChildren<TextMeshPro>().text = "wtf";
    }
}
