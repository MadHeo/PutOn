using UnityEngine;
using UnityEngine.UI;


public class ShopDetailViewController : ViewController
{
    [SerializeField] private NavigationViewController navigationView;

    [SerializeField] private Image iconImage;
    [SerializeField] private Text nameLabel;
    [SerializeField] private Text descriptionLabel;
    [SerializeField] private Text priceLabel;
    [SerializeField] private ShopConfirmationViewController confirmationView;

    public void OnPressBuyButton()
    {
        confirmationView.UpdateContent(itemData);
        navigationView.Push(confirmationView);
    }
    private ShopItemData itemData;

    public override string Title
    {
        get { return (itemData != null) ? itemData.name : ""; }
    }

    public void UpdateContent(ShopItemData itemData)
    {
        this.itemData = itemData;

        iconImage.sprite =
            SpriteSheetManager.GetSpriteByName("IconAtlas", itemData.iconName);
        nameLabel.text = itemData.name;
        priceLabel.text = itemData.price.ToString();
        descriptionLabel.text = itemData.description;
    }
    
}

