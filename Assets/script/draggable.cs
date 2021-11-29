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

    //private GameObject draggingObject;
    private GameObject draggingPrefab;

    public RectTransform canvasRectTransform;
    private Transform createdImages;
    public GameObject prefab;
    private Sprite sprite;

    private void Awake()
    {
        createdImages = GameObject.Find("Canvas/page/Clothes/createdImages").transform;
        sprite = GetComponent<Image>().sprite;

        

    }



    private void UpdateDraggingObjectPos(PointerEventData pointerEventData)
    {
        if (draggingPrefab != null)
        {
            Vector3 screenPos = pointerEventData.position + draggingOffset;

            Camera camera = pointerEventData.pressEventCamera;
            Vector3 newPos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, screenPos, camera, out newPos))
            {
                draggingPrefab.transform.position = newPos;
                draggingPrefab.transform.rotation = canvasRectTransform.rotation;
            }

            //if (draggingPrefab != null)
            //{
            //    Destroy(draggingPrefab);
            //}
        }

        


    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {


        //Image sourceImage = GetComponent<Image>();

        draggingPrefab = Instantiate(prefab, createdImages);
        Image image = draggingPrefab.GetComponent<Image>();
        image.sprite = sprite;


        

        //draggingObject = new GameObject("Dragging Object");
        //draggingObject.transform.SetParent(createdImages);
        //draggingObject.transform.SetAsLastSibling();
        //draggingObject.transform.localScale = Vector3.one;
        //draggingObject.AddComponent<ScalebyDrag>();




        //CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
        //canvasGroup.blocksRaycasts = false;

        //Image draggingImage = draggingObject.AddComponent<Image>();
        ////draggingObject.AddComponent<DraggAndDrop>();
        //draggingImage.sprite = sourceImage.sprite;
        //draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
        //draggingImage.color = sourceImage.color;
        //draggingImage.material = sourceImage.material;



        //canvasRectTransform = image.canvas.transform as RectTransform;

        UpdateDraggingObjectPos(pointerEventData);

    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        UpdateDraggingObjectPos(pointerEventData);


       

    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        //Destroy(draggingPrefab);
    }


}




