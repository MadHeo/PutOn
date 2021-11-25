using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScalebyDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
    }
}