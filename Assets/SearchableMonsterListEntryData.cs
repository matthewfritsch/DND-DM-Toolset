using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This is a class that identifies key GameObjects in a prefab
public class SearchableMonsterListEntryData : MonoBehaviour
{
	// Unity Objects
	public Text monsterName;
    public Text monsterHealth;
    public Text monsterChallengeRating;
	public Text monsterArmorClass;
	public TMP_InputField monsterCount;
    public Image monsterImage;

	// Unity Methods
	void Awake() {
		monsterCount.contentType = TMP_InputField.ContentType.IntegerNumber;
	}

    /// <summary>
    /// Formats itself when provided with a monster
    /// </summary>
    /// <param name="monster"></param>
    public void FormatUsingMonster(MonsterInfo monster) {
        // monster name
        monsterName.text = monster.getMonsterName();

        // monster health
        monsterHealth.text =
            string.Format("HP: {0} / {1}", monster.getCurrentHP(), monster.getHP());

        // monster armor class
        monsterArmorClass.text = string.Format("Armor Class: {0}", monster.getAC());

        // monster challenge rating
        monsterChallengeRating.text = $"CR: {monster.getChallengeRating()}";

        // monster sprite
        Sprite possibleSprite = MonsterSpriteDictionary.GetSpriteFromType(monster.getType());
        if (possibleSprite != null) {
            monsterImage.sprite = possibleSprite;
        }
    }

    /// <summary>
    /// Formats itself when provided with a monster. This also accepts a count parameter,
    /// implying that the monsterCount gameobject should be active
    /// </summary>
    /// <param name="monster"></param>
    /// <param name="count"></param>
    public void FormatUsingMonster(MonsterInfo monster, int count) {
        // Format everything else
        FormatUsingMonster(monster);

        // Then do the formatting with the count info
        monsterCount.gameObject.SetActive(true);
        monsterCount.text = count.ToString();
    }
}
