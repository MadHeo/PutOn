using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPrefab : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;

    void Start()
    {
        GameObject DraggingObject = Instantiate(prefab);
        DraggingObject.GetComponent<Image>();

        //DraggingObject.transform.position = 
        //DraggingObject.Agent();
        DraggingObject.SetActive(true);

    }
}
