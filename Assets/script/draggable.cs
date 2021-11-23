using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Image))]
//[RequireComponent(typeof(ScalebyDrag))]
public class draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Vector2 draggingOffset = new Vector2(0.0f, 40.0f);

    private GameObject draggingObject;
    private RectTransform canvasRectTransform;
    private Transform createdImages;
    public GameObject prefab;

    private void Awake()
    {
        createdImages = GameObject.Find("Canvas/page/Clothes/createdImages").transform;


    }



    private void UpdateDraggingObjectPos(PointerEventData pointerEventData)
    {
        if (draggingObject != null)
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


    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (draggingObject != null)
        {
            Destroy(draggingObject);
        }

        Image sourceImage = GetComponent<Image>();

        //GameObject DraggingPrefab = Instantiate(prefab, createdImages);

        draggingObject = new GameObject("Dragging Object");
        draggingObject.transform.SetParent(createdImages);
        draggingObject.transform.SetAsLastSibling();
        draggingObject.transform.localScale = Vector3.one;
        draggingObject.AddComponent<ScalebyDrag>();




        CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        Image draggingImage = draggingObject.AddComponent<Image>();
        //draggingObject.AddComponent<DraggAndDrop>();
        draggingImage.sprite = sourceImage.sprite;
        draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
        draggingImage.color = sourceImage.color;
        draggingImage.material = sourceImage.material;



        canvasRectTransform = draggingImage.canvas.transform as RectTransform;

        UpdateDraggingObjectPos(pointerEventData);

    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        UpdateDraggingObjectPos(pointerEventData);


    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        //ins
    }


}




