using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesPage : MonoBehaviour
{
    [SerializeField]
    private Transform createdImage;

    private void OnDisable()
    {
        for (int i = 0; i < createdImage.childCount; i++)
        {
            Destroy(createdImage.GetChild(i).gameObject);
        }
    }    
}
