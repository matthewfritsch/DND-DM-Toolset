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
    // TODO: Add monster image dictionary
    // public MonsterImage monsterImageDictionary;

    private Color defaultColor = new Color(0.4f, 0f, .15f, .4f);
    private Color hoverColor = new Color(.4f, 0f, .15f, .5f);

    // Local reference to the combatant that this InitiativePanel represents
    // ? If PlayerInfo is being modified in multiple locations, do we need to consider race conditions
    private BeingInfo managedCombatant;
    private GameObject playerName, characterName, characterClass, characterArmor, characterInitiative, characterHealth, characterImage;
    // Any variables that can be modified in combat and need to be reset when combat finishes
    // The modification amount
    private short modInitiative = 0, modAC = 0;

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

    public void SetInitiativePanel(BeingInfo combatant) {
        managedCombatant = combatant;
        if (managedCombatant.GetType() == typeof(MonsterInfo)) {
            var monster = (MonsterInfo) managedCombatant;

            // ? Set to the monster's challenge rating?
            playerName.GetComponent<Text>().text = monster.getChallengeRating().ToString();//= monster.getMonsterName();
            characterName.GetComponent<Text>().text = monster.getMonsterName();//= managedCombatant.getCharacterName();
            characterClass.GetComponent<Text>().text = monster.getType();
            characterArmor.GetComponent<Text>().text = managedCombatant.getAC().ToString();
            characterInitiative.GetComponent<Text>().text = managedCombatant.getInitiative().ToString();

            characterHealth.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", managedCombatant.getCurrentHP(), managedCombatant.getHP());
            characterHealth.GetComponentInChildren<Image>().fillAmount = (managedCombatant.getCurrentHP()/managedCombatant.getCurrentHP());

            // TODO: Change to read from a monster image dictionary
            characterImage.GetComponent<Image>().sprite = classImageDictionary.GetClassImage(monster.getType());

            return;
        }

        // Combatant is assumed to be a player
        // Debug.Log(combatant.getPlayerName());
        var player = (PlayerInfo) managedCombatant;
        playerName.GetComponent<Text>().text = player.getPlayerName();
        characterName.GetComponent<Text>().text = player.getCharacterName();
        characterClass.GetComponent<Text>().text = player.getCharacterClass();
        characterArmor.GetComponent<Text>().text = managedCombatant.getAC().ToString();
        characterInitiative.GetComponent<Text>().text = managedCombatant.getInitiative().ToString();

        characterHealth.GetComponentInChildren<Text>().text = string.Format("{0}/{1}", managedCombatant.getCurrentHP(), managedCombatant.getHP());
        characterHealth.GetComponentInChildren<Image>().fillAmount = (managedCombatant.getCurrentHP()/managedCombatant.getCurrentHP());

        characterImage.GetComponent<Image>().sprite = classImageDictionary.GetClassImage(player.getCharacterClass());
    }

    // Only show the modification if something other than zero
    public void ModifyInitiative(short change) {
        modInitiative += change;
        string initString = modInitiative == 0 ? managedCombatant.getInitiative().ToString() :
            string.Format("{0}{1}", managedCombatant.getInitiative().ToString(), modInitiative.ToString());

        characterInitiative.GetComponent<Text>().text = initString;
    }

    public void ModifyArmorClass(short change) {
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
    public BeingInfo GetCombatant() {
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
