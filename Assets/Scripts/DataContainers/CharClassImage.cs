using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Scriptable Objects are to save data which will be used in run time,
/// data which is not attatched to any object

[CreateAssetMenu(fileName = "CharClassImage", menuName = "ScriptableObjects/CharClassImage", order = 0)]
public class CharClassImage : ScriptableObject {
    //     // Ensures that it can be displayed in the editor
    [System.Serializable]
    public struct ClassImagePair {
        public string class_name;
        public Sprite class_image;

    }
    [Tooltip("Add new element for every class")]
    public List<ClassImagePair> classImage;
    // public ClassImagePair[] classImage;
    [Tooltip("Image that will be used when no matching class is found")]
    public Sprite defaultImage;

    public Dictionary<string, Sprite> _classNameToImage = new Dictionary<string, Sprite>();

    public void OnEnable() {
        // Debug.Log("Loading images...");
    // public CharClassImage() {
        foreach (ClassImagePair item in classImage) {
			_classNameToImage[item.class_name] = item.class_image;
		}
        // Debug.Log("Loaded " + _classNameToImage.Count.ToString() + " items");
    }
            // classImage.ToDictionary(pair => pair.class_name, pair => pair.class_image);
    //     // classImagePair.Select((item, index) => new {item.class_name, item.class_image})
    //                 //   .ToDictionary(pair => );

    public Sprite GetClassImage(string className) {
        // Debug.Log("Getting sprite for " + className);
        if (_classNameToImage.ContainsKey(className.ToLower())) {
            // Debug.Log("Found!");
            return _classNameToImage[className.ToLower()];
        }
        // Debug.Log("Not Found!");
        return defaultImage;
    }
}
