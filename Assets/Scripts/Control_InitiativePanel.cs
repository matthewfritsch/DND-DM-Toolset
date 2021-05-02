using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
///     Used to make changes to any InitiativePanel prefabs.
/// </summary>
// To call methods attached to the prefab, you must first GetComponent<Control_InitiativePanel>()
// If multiple methods will be called in sequenece, recommend to store the component in a temp var
public class Control_InitiativePanel : MonoBehaviour,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    // private Color defaultColor = gameObject.GetComponent<Image>().color;
    public CharClassImage classImageDictionary;

    private Color defaultColor = new Color(0.4f, 0f, .15f, .4f);
    private Color hoverColor = new Color(.4f, 0f, .15f, .5f);

    // Local reference to the combatant that this InitiativePanel represents
    // ? If PlayerInfo is being modified in multiple locations, do we need to consider race conditions
    private PlayerInfo managedCombatant;
    private GameObject playerName, characterName, characterClass, characterArmor, characterInitiative, characterHealth, characterImage;
    // Any variables that can be modified in combat and need to be reset when combat finishes
    // The modification amount
    private int modInitiative = 0, modAC = 0;

    /// <summary>
    ///     Get references to all children that can be modified
    /// </summary>
    public void Awake() {
        playerName = transform.Find("PlayerName").gameObject;
        characterName = transform.Find("CharName").gameObject;
        characterClass = transform.Find("CharClass").gameObject;
        characterArmor = transform.Find("CharArmor/Val_Armor").gameObject;
        characterInitiative = transform.Find("CharInit/Val_Init").gameObject;
        characterHealth = transform.Find("HealthDisplay").gameObject;
        characterImage = transform.Find("CharImage").gameObject;
    }

    // ? PlayerInfo could have some variable that marks that there has been a change
    // void Update() {
    //     check if PlayerInfo has been marked as changed
    //     update displays to match
    // }

    // TODO: Set parameter as parent type
    public void SetInitiativePanel(PlayerInfo combatant) {
        managedCombatant = combatant;
        // Debug.Log(combatant.getPlayerName());
        playerName.GetComponent<Text>().text = managedCombatant.getPlayerName();
        characterName.GetComponent<Text>().text = managedCombatant.getCharacterName();
        characterClass.GetComponent<Text>().text = managedCombatant.getCharacterClass();
        characterArmor.GetComponent<Text>().text = managedCombatant.getAC().ToString();
        characterInitiative.GetComponent<Text>().text = managedCombatant.getInitiative().ToString();

        characterHealth.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", managedCombatant.getCurrentHP(), managedCombatant.getHP());
        characterHealth.GetComponentInChildren<Image>().fillAmount = (managedCombatant.getCurrentHP()/managedCombatant.getCurrentHP());

        characterImage.GetComponent<Image>().sprite = classImageDictionary.GetClassImage(managedCombatant.getCharacterClass());

    }

    // Only show the modification if something other than zero
    public void ModifyInitiative(int change) {
        modInitiative += change;
        string initString = modInitiative == 0 ? managedCombatant.getInitiative().ToString() :
            string.Format("{0}{1}", managedCombatant.getInitiative().ToString(), modInitiative.ToString());

        characterInitiative.GetComponent<Text>().text = initString;
    }

    public void ModifyArmorClass(int change) {
        modAC += change;
        string acString = modAC == 0 ? managedCombatant.getAC().ToString() :
            string.Format("{0}{1}", managedCombatant.getAC().ToString(), modAC.ToString());
        
        characterArmor.GetComponent<Text>().text = acString;
    }

    // Positive health changes are heals, negative is damange
    public void ModifyCurrentHealth(short healthChange) {
        short newCurrentHP = (short) (managedCombatant.getCurrentHP() + healthChange);

        // ? Check to see if newCurrentHP is less than zero
        if (newCurrentHP > managedCombatant.getHP()) {
            newCurrentHP = managedCombatant.getHP();
        }
        
        managedCombatant.setCurrentHP(newCurrentHP);

        characterHealth.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", managedCombatant.getCurrentHP(), managedCombatant.getHP());
        characterHealth.GetComponentInChildren<Image>().fillAmount = (managedCombatant.getCurrentHP()/managedCombatant.getHP());
    }

    /// <summary>
    /// Called at the end of combat, whenever all status changes are cleared
    /// </summary>
    public void ResetModifications() {
        ResetArmor();
        ResetInitiative();
    }
    
    public void ResetInitiative() {
        modInitiative = 0;
        characterInitiative.GetComponent<Text>().text = managedCombatant.getInitiative().ToString();
    }

    public void ResetArmor() {
        modAC = 0;
        characterArmor.GetComponent<Text>().text = managedCombatant.getAC().ToString();
    }

    // TODO: Replace return with common parent type
    public PlayerInfo GetCombatant() {
        return managedCombatant;
    }

    // EVENT CONTROLLERS //

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData) {
        // Debug.Log(this.name + " Game Object Clicked, ID: " + gameObject.GetInstanceID().ToString());
        gameObject.SendMessageUpwards("KillCombatant", gameObject);
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.    
    }

    // Detect if mouse hovers over InitativeTab
    public void OnPointerEnter(PointerEventData pointerEventData) {
        // TODO: Fancy Visual stuff when mouse is over a tab
        gameObject.GetComponent<Image>().color = hoverColor;

    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        gameObject.GetComponent<Image>().color = defaultColor;
    }
}
