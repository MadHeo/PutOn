using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggAndDrop : MonoBehaviour, IDragHandler
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public Vector3 LoadedPos;
    float startPosx;
    float startPosY;
    bool isBeingHeld = false;
    public bool isInLine;
    float timelinePosY;

    private Vector2 draggingOffset = new Vector2(0.0f, 40.0f);
    private GameObject draggingObject;
    private RectTransform canvasRectTransform;





    //private void Start()
    //{
    //    LoadedPos = this.transform.position;
    //}
    //private void Update()
    //{
    //    if (isBeingHeld)
    //    {
    //        Vector2 mousePos;
    //        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //        this.gameObject.transform.position = new Vector2(mousePos.x - startPosx, mousePos.y - startPosY);
    //    }
    //}


    #region 마우스 드래그앤 드롭



    //private void OnMouseDown()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //        spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
    //        Vector3 mousePos;
    //        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    //        startPosx = mousePos.x - this.transform.position.x;
    //        startPosY = mousePos.y - this.transform.position.y;


    //        isBeingHeld = true;

    //    }
    //}

    //private void OnMouseUp()
    //{
    //    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    //    isBeingHeld = false;

    //    if (isInLine)
    //        this.gameObject.transform.position = new Vector3(this.gameObject.transform.localPosition.x, timelinePosY, -1f);
    //    else
    //        this.gameObject.transform.position = LoadedPos;
    //}



    public void OnDrag(PointerEventData pointerEventData)
    {

        Vector3 screenPos = pointerEventData.position + draggingOffset;


        Camera camera = pointerEventData.pressEventCamera;
        Vector3 newPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, screenPos, camera, out newPos))
        {
            draggingObject.transform.position = newPos;
            draggingObject.transform.rotation = canvasRectTransform.rotation;
        }
    }



    #endregion

}
