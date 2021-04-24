using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchableMonsterList : MonoBehaviour
{
	// Unity Objects
	[SerializeField]
	private Button toggleMenuButton;
	[SerializeField]
	private GameObject scrollView;

	// Fields
	private PlayerInfoList dummyData;

	// Unity Methods
	void Update()
	{
	}

	// Methods
	public void ToggleScrollView()
	{
		scrollView.SetActive(!scrollView.activeInHierarchy);
	}
}
