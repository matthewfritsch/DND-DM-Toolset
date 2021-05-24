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
    [SerializeField]
    private TextMeshProUGUI charAlignment;
    [SerializeField]
    private TextMeshProUGUI charSize;
    [SerializeField]
    private TextMeshProUGUI charHP;
    [SerializeField]
    private TextMeshProUGUI charSTR;
    [SerializeField]
    private TextMeshProUGUI charDEX;
    [SerializeField]
    private TextMeshProUGUI charCON;
    [SerializeField]
    private TextMeshProUGUI charINT;
    [SerializeField]
    private TextMeshProUGUI charWIS;
    [SerializeField]
    private TextMeshProUGUI charCHA;
    public void setFields(string chName, string chClass, string chArmor, float currHealth, Alignment alignment, Size size, string chHP) {
        charName.text = chName;
        charClass.text = chClass;
        charArmor.text = chArmor;
        healthBar.value = currHealth;
        charAlignment.text = alignment.ToString();
        charSize.text = size.ToString();
        charHP.text = chHP;
    }

    public void setStats(string STR, string DEX, string CON, string INT, string WIS, string CHA) {
        charSTR.text = STR;
        charDEX.text = DEX;
        charCON.text = CON;
        charINT.text = INT;
        charWIS.text = WIS;
        charCHA.text = CHA;
    }
}
