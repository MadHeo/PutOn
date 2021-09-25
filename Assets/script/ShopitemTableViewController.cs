using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ShopitemTableViewController : TableViewController<ShopItemData>
{
    [SerializeField] private NavigationViewController navigationView;
    [SerializeField] private ShopDetailViewController detailView;

    public override string Title { get { return "SHOP"; } }

    
    private void LoadData()
    {

        tableData = new List<ShopItemData>()
        {
            new ShopItemData{iconName = "drink", name = "WATER",
                price=100, description="Noting else, just water."},
            new ShopItemData {iconName="drink2", name="SODA",
                price=150, description="Sugar free and low calorie"},
            new ShopItemData {iconName="drink3", name="COFFEE",
                price=200, description="How would you like your coffee?"},
            new ShopItemData {iconName="drink4", name="ENERGY DRINK",
                price=300, description="It will give you wings."},
            new ShopItemData {iconName="drink5", name="BEER",
                price=500, description="It's a drink for grown-ups."},

        };

        UpdateContentSize();
    }

    protected override float CellHeightAtIndex(int index)
    {
        if (index >= 0 && index <= tableData.Count - 1)
        {
            if (tableData[index].price >= 1000)
            {
                return 240.0f;
            }
            if (tableData[index].price >= 500)
            {
                return 160.0f;
            }
        }
        return 128.0f;
    }

    protected override void Awake()
    {
        base.Awake();

        SpriteSheetManager.Load("IconAtlas");
    }

    

    protected override void Start()
    {
        if(navigationView != null)
        {
            navigationView.Push(this);
        }
        //base.Start();

        //LoadData();
    }

    public void OnPressCell(ShopItemTableViewCell cell)
    {
        if (navigationView != null)
        {
            detailView.UpdateContent(tableData[cell.DataIndex]);

            navigationView.Push(detailView);
        }
    }
}
