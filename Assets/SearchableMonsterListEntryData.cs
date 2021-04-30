using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This is a class that identifies key GameObjects in a prefab
public class SearchableMonsterListEntryData : MonoBehaviour
{
	// Unity Objects
	public Text MonsterName;
	public Text MonsterHealth;
	public Text MonsterArmorClass;
	public TMP_InputField MonsterCount;

	// Unity Methods
	void Awake()
	{
		MonsterCount.contentType = TMP_InputField.ContentType.IntegerNumber;
	}
}
