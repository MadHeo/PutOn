using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(ScrollRect))]
public class TableViewController<T> : ViewController
{
    protected List<T> tableData = new List<T>();
    [SerializeField] private RectOffset padding;
    [SerializeField] private float spacingHeight = 4.0f;

    private ScrollRect cachedScrollRect;
    public ScrollRect CachedScrollRect
    {
        get
        {
            if (cachedScrollRect = null)
            {
                cachedScrollRect = GetComponent<ScrollRect>();
            }
            return cachedScrollRect;
        }
    }

    protected virtual void Awake()
    {

        
    }

    


    protected virtual float CellHeightAtIndex(int index)
    {
        return 0.0f;
    }

    protected void UpdateContentSize()
    {
        float contentHeight = 0.0f;
        for (int i = 0; i < tableData.Count; i++)
        {
            contentHeight += CellHeightAtIndex(i);
            if (i > 0) { contentHeight += spacingHeight; }
        }

        Vector2 sizeDelta = CachedScrollRect.content.sizeDelta;
        sizeDelta.y = padding.top + contentHeight + padding.bottom;
        CachedScrollRect.content.sizeDelta = sizeDelta;
    }

    [SerializeField] private GameObject cellBase;
    private LinkedList<TableViewCell<T>> cells = new LinkedList<TableViewCell<T>>();

    protected virtual void Start()
    {
        cellBase.SetActive(false);
    }



    private TableViewCell<T> CreateCellForIndex(int index)
    {
        GameObject obj = Instantiate(cellBase) as GameObject;
        obj.SetActive(true);
        TableViewCell<T> cell = obj.GetComponent<TableViewCell<T>>();

        Vector3 scale = cell.transform.localScale;
        Vector2 sizeDelta = cell.CachedRectTransform.sizeDelta;
        Vector2 offsetMin = cell.CachedRectTransform.offsetMin;
        Vector2 offsetMax = cell.CachedRectTransform.offsetMax;

        cell.transform.SetParent(cellBase.transform.parent);

        cell.transform.localScale = scale;
        cell.CachedRectTransform.sizeDelta = sizeDelta;
        cell.CachedRectTransform.offsetMin = offsetMin;
        cell.CachedRectTransform.offsetMax = offsetMax;

        UpdateCellForIndex(cell, index);
        cells.AddLast(cell);

        return cell;
    }

    private void UpdateCellForIndex(TableViewCell<T> cell, int index)
    {
        cell.DataIndex = index;

        if (cell.DataIndex >= 0 && cell.DataIndex <= tableData.Count - 1)
        {
            cell.gameObject.SetActive(true);
            cell.UpdateContent(tableData[cell.DataIndex]);
            cell.Height = CellHeightAtIndex(cell.DataIndex);
        }
        else
        {
            cell.gameObject.SetActive(false);
        }
    }

    private Rect visibleRect;
    [SerializeField] private RectOffset visibleRectPadding;

    private void UpdateVisibleRect()
    {
        visibleRect.x = CachedScrollRect.content.anchoredPosition.x + visibleRectPadding.left;
        visibleRect.y = -CachedScrollRect.content.anchoredPosition.y + visibleRectPadding.top;

        visibleRect.width = CachedRectTransform.rect.width +
            visibleRectPadding.left + visibleRectPadding.right;
        visibleRect.height = CachedRectTransform.rect.height +
            visibleRectPadding.top + visibleRectPadding.bottom;
    }

    protected void UpdateContents()
    {
        UpdateContentSize();
        UpdateVisibleRect();

        if (cells.Count < 1)
        {
            Vector2 cellTop = new Vector2(0.0f, -padding.top);
            for (int i = 0; i < tableData.Count; i++)
            {
                float cellHeight = CellHeightAtIndex(i);
                Vector2 cellBottom = cellTop + new Vector2(0.0f, -cellHeight);
                if ((cellTop.y <= visibleRect.y &&
                    cellTop.y >= visibleRect.y - visibleRect.height) ||
                        (cellBottom.y <= visibleRect.y &&
                        cellBottom.y >= visibleRect.y - visibleRect.height))
                {
                    TableViewCell<T> cell = CreateCellForIndex(i);
                    cell.Top = cellTop;
                    break;
                }
                cellTop = cellBottom + new Vector2(0.0f, spacingHeight);
            }
            FillVisibleRectWithCells();
        }
        else
        {
            LinkedListNode<TableViewCell<T>> node = cells.First;
            UpdateCellForIndex(node.Value, node.Value.DataIndex);
            node = node.Next;

            while (node != null)
            {
                UpdateCellForIndex(node.Value, node.Previous.Value.DataIndex + 1);
                node.Value.Top =
                    node.Previous.Value.Bottom + new Vector2(0.0f, -spacingHeight);
                node = node.Next;
            }

            FillVisibleRectWithCells();
        }
    }

    private void FillVisibleRectWithCells()
    {
        if (cells.Count < 1)
        {
            return;
        }

        TableViewCell<T> lastCell = cells.Last.Value;
        int nextCellDataIndex = lastCell.DataIndex + 1;
        Vector2 nextCellTop = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);

        while (nextCellDataIndex < tableData.Count &&
            nextCellTop.y >= visibleRect.y - visibleRect.height)
        {
            TableViewCell<T> cell = CreateCellForIndex(nextCellDataIndex);
            cell.Top = nextCellTop;

            lastCell = cell;
            nextCellDataIndex = lastCell.DataIndex + 1;
            nextCellTop = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);
        }
    }

    private Vector2 prevScrollPos;
    protected virtual void start()
    {


        CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);

    }

    public void OnScrollPosChanged(Vector2 scrollPos)
    {
        UpdateVisibleRect();

        ReuseCells((scrollPos.y < prevScrollPos.y) ? 1 : -1);

        prevScrollPos = scrollPos;
    }

    private void ReuseCells(int scrollDirection)
    {
        if (cells.Count < 1)
        {
            return;
        }
        if (scrollDirection > 0)
        {
            TableViewCell<T> firstCell = cells.First.Value;
            while (firstCell.Bottom.y > visibleRect.y)
            {
                TableViewCell<T> lastCell = cells.Last.Value;
                UpdateCellForIndex(firstCell, lastCell.DataIndex + 1);
                firstCell.Top = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);

                cells.AddLast(firstCell);
                cells.RemoveFirst();
                firstCell = cells.First.Value;
            }

            FillVisibleRectWithCells();
        }
        else if (scrollDirection < 0)
        {
            TableViewCell<T> lastCell = cells.Last.Value;
            while (lastCell.Top.y < visibleRect.y - visibleRect.height)
            {
                TableViewCell<T> firstCell = cells.First.Value;
                UpdateCellForIndex(lastCell, firstCell.DataIndex - 1);
                lastCell.Bottom = firstCell.Top + new Vector2(0.0f, spacingHeight);

                cells.AddFirst(lastCell);
                cells.RemoveLast();
                lastCell = cells.Last.Value;
            }
        }
    }
}




