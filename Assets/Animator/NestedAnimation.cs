using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class RenameWindow : EditorWindow
{
    public string CaptionText { get; set; }
    public string ButtonText { get; set; }
    public string NewName { get; set; }
    public System.Action<string>
        OnClickButtoneDelegate{ get; set; }

    void OnGUI()
    {
        NewName = EditorGUILayout.TextField(CaptionText, NewName);
        if (GUILayout.Button(ButtonText))
        {
            if (OnClickButtoneDelegate !=null)
            {
                OnClickButtoneDelegate.Invoke(NewName.Trim());
            }

            Close();
            GUIUtility.ExitGUI();
        }
    }
}

public class NestedAnimation : MonoBehaviour
{
    [MenuItem("Assets/Create/Nested Animation")]

    public static void Create()
    {
        AnimatorController selectedAnimatorController = Selection.activeObject as AnimatorController;

        if(selectedAnimatorController == null)
        {
            Debug.LogWarning("No animator contoller selected.");
            return;
        }

        RenameWindow renameWindow = EditorWindow.GetWindow<RenameWindow>("Create Nested Animation");
        renameWindow.CaptionText = "New Animation Name";
        renameWindow.NewName = "New Clip";
        renameWindow.ButtonText = "Create";

        renameWindow.OnClickButtoneDelegate = (string newName) =>
        {
            if (string.IsNullOrEmpty(newName))
            {
                Debug.LogWarning("Invalid name.");
                return;
            }

            AnimationClip animationCLip = AnimatorController.AllocateAnimatorClip(newName);

            AssetDatabase.AddObjectToAsset(animationCLip, selectedAnimatorController);

            AssetDatabase.ImportAsset(
                AssetDatabase.GetAssetPath(selectedAnimatorController));

        };
    }


    [MenuItem("Assets/Delete Sub Asset")]
    public static void Delete()
    {
        Object[] selectedAssets = Selection.objects;
        if(selectedAssets.Length <1)
        {
            Debug.LogWarning("No sub asset selected.");
            return;
        }

        foreach (Object asset in selectedAssets)
        {
            if(AssetDatabase.IsSubAsset(asset))
            {
                string path = AssetDatabase.GetAssetPath(asset);
                DestroyImmediate(asset, true);

                AssetDatabase.ImportAsset(path);
            }
        }
    }

   
}
