using UnityEngine;
using UnityEngine.UI;


public class ShopConfirmationViewController : ViewController
{
    [SerializeField] private Text messageLabel;
    [SerializeField] private GameObject UserInfo;

    public override string Title { get { return "CONFIRMATION"; } }

    public void UpdateContent(ShopItemData itemData)
    {

        messageLabel.text = string.Format("Buy {0} for {1} coins?",
            itemData.name, itemData.price.ToString());
    }

    public void OnPressConfirmButton()
    {
        string title = "알림";
        string message = messageLabel.text;

        AlertViewController.Show(title, message, new AlertViewOptions
        {cancelButtonTitle = "CANCEL", cancelButtonDelegate = () =>
        {
            Debug.Log("Cancel.");
        },

        okButtonTitle = "OK", okButtonDelegate = () =>
        {
            Debug.Log("OK.");
            UserInfo.SetActive(true);

        },

        });

    }
}
