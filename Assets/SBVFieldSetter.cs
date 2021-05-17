using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SBVFieldSetter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI charName;
    [SerializeField]
    private TextMeshProUGUI charClass;
    [SerializeField]
    private TextMeshProUGUI charArmor;
    [SerializeField]
    private Slider healthBar;
    public void setFields(string chName, string chClass, string chArmor, float currHealth) {
        charName.text = chName;
        charClass.text = chClass;
        charArmor.text = chArmor;
        healthBar.value = currHealth;
    }
}
