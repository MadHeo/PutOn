using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetManager
{
    private static Dictionary<string, Dictionary<string, Sprite>> spriteSheets =
        new Dictionary<string, Dictionary<string, Sprite>>();

    public static void Load(string path)
    {
        if(!spriteSheets.ContainsKey(path))
        {
            spriteSheets.Add(path, new Dictionary<string, Sprite>());
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        foreach(Sprite sprite in sprites)
        {
            if(!spriteSheets[path].ContainsKey(sprite.name))
            {
                spriteSheets[path].Add(sprite.name, sprite);
            }
        }
    }

    public static Sprite GetSpriteByName(string path, string name)
    {
        if(spriteSheets.ContainsKey(path) && spriteSheets[path].ContainsKey(name))
        {
            return spriteSheets[path][name];
        }
        return null;
    }


}
