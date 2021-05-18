using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpriteDictionary : Singleton<MonsterSpriteDictionary> {
    /// <summary>
    /// This dictionary maps monster types to the filename of a sprite
    /// </summary>
    private static Dictionary<string, string> resourceDictionary;

    /// <summary>
    /// This dictionary maps monster types to the corresponding loaded sprite
    /// </summary>
    private static Dictionary<string, Sprite> spriteDictionary;

    
    /// <summary>
    /// This should be called in every function (except for this one) to initialize
    /// anything that needs initialization
    /// </summary>
    public static void Init() {
        // initialize fields
        if (resourceDictionary == null) {
            resourceDictionary = MakeResourceDictionary();
        }
        if (spriteDictionary == null) {
            spriteDictionary = new Dictionary<string, Sprite>();
        }
    }

    /// <summary>
    /// Returns the sprite that is corresponding to the type of monster, or null if the
    /// type could not be found.
    /// </summary>
    /// <param name="monsterType"></param>
    /// <returns></returns>
    public static Sprite GetSpriteFromType(string monsterType) {
        // Possibly initiatialize the dictionaries
        Init();

        // try to see if the sprite already exists in the sprite dictionary and return it
        if (spriteDictionary.ContainsKey(monsterType)) {
            return spriteDictionary[monsterType];
        }

        // otherwise, try to see if it exists in the resource dictionary and load it
        if (resourceDictionary.ContainsKey(monsterType)) {
            // use the filename
            string filename = resourceDictionary[monsterType];

            // load the sprite
            Sprite sprite = Resources.Load<Sprite>(filename);

            // if the load succeeds, then store it in the sprite dictionary and return it
            if (sprite != null) {
                spriteDictionary[monsterType] = sprite;
                return sprite;
            }
            // if it fails, log an error and return null
            else {
                Debug.LogError($"MonsterSpriteDictionary: Could not find sprite associated with filename {filename}.");
                return null;
            }
        }

        // if all else fails, return null (since the monsterType might not be valid)
        return null;
    }

    private static Dictionary<string, string> MakeResourceDictionary() {
        // create the resource dictionary
        Dictionary<string, string> resourceDictionary = new Dictionary<string, string>();

        // path prefix (if needed)
        string pathPrefix = "TestingImages/CharacterBases/";

        // add entries to map monster types to sprite filenames
        resourceDictionary.Add("Beast",         pathPrefix + "centaur_brown_m");
        resourceDictionary.Add("Plant",         pathPrefix + "naga_lightgreen_f");
        resourceDictionary.Add("Construct",     pathPrefix + "ghoul");
        resourceDictionary.Add("Humanoid",      pathPrefix + "human_f");
        resourceDictionary.Add("Fiend (Devil)", pathPrefix + "demonspawn_red_f");
        resourceDictionary.Add("Undead",        pathPrefix + "deep_dwarf_m");
        resourceDictionary.Add("Aberration",    pathPrefix + "kenku_winged_f");
        resourceDictionary.Add("Fiend (Demon)", pathPrefix + "demonspawn_black_m");
        resourceDictionary.Add("Fey",           pathPrefix + "draconian_green_f");
        resourceDictionary.Add("Elemental",     pathPrefix + "merfolk_f");
        resourceDictionary.Add("Dragon",        pathPrefix + "draconian_m");
        resourceDictionary.Add("Monstrosity",   pathPrefix + "draconian_purple_f");
        resourceDictionary.Add("Ooze",          pathPrefix + "troll_f");
        resourceDictionary.Add("Giant",         pathPrefix + "demigod_m");
        resourceDictionary.Add("Celestial",     pathPrefix + "draconian_pale_m");
        resourceDictionary.Add("Fiend",         pathPrefix + "mummy_f");

        // return that resource dictionary
        return resourceDictionary;
    }
}
