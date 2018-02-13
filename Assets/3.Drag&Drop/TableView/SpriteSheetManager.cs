using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetManager {
    //스프라이트 시트에 포함된 스프라이트를 캐시하는 딕셔너리
    private static Dictionary<string, Dictionary<string, Sprite>> spriteSheets = new Dictionary<string, Dictionary<string, Sprite>>();

    //스프라이트 시트에 포함된 스프라이트를 읽어 들여 캐시하는 메서드
    public static void Load(string path)
    {
        if (!spriteSheets.ContainsKey(path))
        {
            spriteSheets.Add(path, new Dictionary<string, Sprite>());
        }

        //스프라이트를 읽어 들여 이름과 관련지어 캐싱
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        foreach(Sprite sprite in sprites)
        {
            if(!spriteSheets[path].ContainsKey(sprite.name))
            {
                spriteSheets[path].Add(sprite.name, sprite);
            }
        }
    }

    //스프라이트 이름을 통해 스프라이트 시트에 포함된 스프라이트를 반환하는 메서드
    public static Sprite GetSpriteByName(string path, string name)
    {
        if(spriteSheets.ContainsKey(path) && spriteSheets[path].ContainsKey(name))
        {
            return spriteSheets[path][name];
        }
        return null;
    }
}
