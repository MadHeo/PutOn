using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{



    [SerializeField]
    private Vector2 draggingOffset = new Vector2(0.0f, 40.0f);
    private GameObject draggingObject;
    private RectTransform canvasRectTransform;

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
        if(draggingObject != null)
        {
            Destroy(draggingObject);
        }

        Image sourceImage = GetComponent<Image>();

        draggingObject = new GameObject("Dragging Object");
        draggingObject.transform.SetParent(sourceImage.canvas.transform);
        draggingObject.transform.SetAsLastSibling();
        draggingObject.transform.localScale = Vector3.one;

        CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        Image draggingImage = draggingObject.AddComponent<Image>();

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
        Destroy(draggingObject);
    }

}

