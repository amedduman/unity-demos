using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueReader : MonoBehaviour
{
    [SerializeField] Material mat;
    // Start is called before the first frame update
    void Start()
    {
        var m = mat.GetFloat("_mValue");

        Debug.Log(m);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [ContextMenu("get values")]
    public void getValues()
    {
        var m = mat.GetFloat("_mValue");
        Debug.Log(m);
    }
}
