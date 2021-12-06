using UnityEngine;
using UnityEngine.UI;


public class ShopConfirmationViewController : ViewController
{
    [SerializeField] private GameObject UserInfo;

    public override string Title { get { return "CONFIRMATION"; } }

    public void UpdateContent(ShopItemData itemData)
    {

        //messageLabel.text = string.Format("Buy {0} for {1} coins?",
        //    itemData.name, itemData.price.ToString());
    }

    public void OnPressConfirmButton()
    {
        string title = "알림";
        string message = "회원정보를 확인하시겠습니까 ?";

        AlertViewController.Show(title, message, new AlertViewOptions
        {cancelButtonTitle = "CANCEL", cancelButtonDelegate = () =>
        {
            Debug.Log("Cancel.");
        },

        okButtonTitle = "OK", okButtonDelegate = () =>
        {
            Debug.Log("OK.");
            UserInfo.SetActive(true);
            gameObject.SetActive(false);
        },

        });

    }
}
