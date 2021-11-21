//using UnityEngine;
//using System.Collections;

//public class TouchScript : MonoBehaviour
//{
//    private float initTouchDist; // Touch Point 1 and 2's Distance
//    private string TouchStatus;
//    // Use this for initialization
//    void Start()
//    {
//        TouchStatus = "Idle";
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.touchCount > 1) // Touch Point Count : over 2
//        {
//            if (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) > initTouchDist)
//            {
//                TouchStatus = "Magnifying";
//            }

//            else if (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) < initTouchDist)
//            {
//                TouchStatus = "Reducing";
//            }

//            initTouchDist = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
//        }
//        else
//            TouchStatus = "Idle";
//    }

//    void OnGUI()
//    {
//        Rect GUIPos = new Rect(0, 100, 100, 100);
//        GUI.Label(GUIPos, "TouchStatus : \n   " + TouchStatus);
//    }
//}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScalebyDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    private float rotationRate = 3.0f;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");

        transform.Rotate(eventData.position.y * rotationRate,
                                 -eventData.position.x * rotationRate, 0, Space.World);
        // get the user touch inpun
        //foreach (Touch touch in Input.touches)
        //{
        //    Debug.Log("Touching at: " + touch.position);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Debug.Log("Touch phase began at: " + touch.position);
        //    }
        //    else if (touch.phase == TouchPhase.Moved)
        //    {
        //        Debug.Log("Touch phase Moved");
        //        transform.Rotate(touch.deltaPosition.y * rotationRate,
        //                         -touch.deltaPosition.x * rotationRate, 0, Space.World);
        //    }
        //    else if (touch.phase == TouchPhase.Ended)
        //    {
        //        Debug.Log("Touch phase Ended");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
    }
}