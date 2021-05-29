using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSpriteDictionary", menuName = "ScriptableObjects/MonsterSpriteDictionary", order = 0)]
public class MonsterSpriteDictionary : ScriptableObject {

    [System.Serializable]
    public struct MonstImagePair {
        public string monst_type;
        public Sprite monst_image;
    }

    [Tooltip("Add new element for every monster type")]
    public List<MonstImagePair> monstImage;
    [Tooltip("Image that will be used when no matching monster type is found")]
    public Sprite defaultImage;

    public Dictionary<string, Sprite> _monstTypeToImage = new Dictionary<string, Sprite>();

    public void OnEnable() {
        foreach (MonstImagePair item in monstImage) {
            _monstTypeToImage[item.monst_type] = item.monst_image;
        }
    }

    /// <summary>
    /// Returns the sprite that is corresponding to the type of monster, or null if the
    /// type could not be found.
    /// </summary>
    /// <param name="monsterType"></param>
    /// <returns></returns>
    public Sprite GetSpriteFromType(string monsterType) {
  
        // try to see if the sprite already exists in the sprite dictionary and return it
        if (_monstTypeToImage.ContainsKey(monsterType)) {
            return _monstTypeToImage[monsterType];
        }

        // if all else fails, return a default image for unknown monster type
        return defaultImage;
    }

}
