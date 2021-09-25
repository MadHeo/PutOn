using UnityEngine;
using UnityEngine.UI;


public class ShopItemData
{
    public string iconName;
    public string name;
    public int price;
    public string description;
}

public class ShopItemTableViewCell : TableViewCell<ShopItemData>
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Text nameLabel;
    [SerializeField] private Text priceLabel;


    public override void UpdateContent(ShopItemData itemData)
    {
        nameLabel.text = itemData.name;
        priceLabel.text = itemData.ToString();

        iconImage.sprite = 
            SpriteSheetManager.GetSpriteByName("IconAtlas", itemData.iconName);
    }
    

}