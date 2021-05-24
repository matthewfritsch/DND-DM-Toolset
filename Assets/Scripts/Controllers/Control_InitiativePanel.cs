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
    [Tooltip("A ScriptableObject that holds the class name, class image dictionary")]
    public CharClassImage classImageDictionary;
	[Tooltip("A ScriptableObject that holds the monster type, monster image dictionary")]
	public MonsterSpriteDictionary monsterImageDictionary;

    public GameObject deletionToggle;

    // stat block visual prefab reference
    public GameObject statBlockVisual;

    private Color defaultColor = new Color(0.4f, 0f, .15f, .4f);
    private Color hoverColor = new Color(.4f, 0f, .15f, .5f);

    // Local reference to the combatant that this InitiativePanel represents
    // ? If PlayerInfo is being modified in multiple locations, do we need to consider race conditions
    private BeingInfo managedCombatant;
    private GameObject playerName, characterName, characterClass, characterArmor, characterInitiative, characterHealth, characterImage;
    // Any variables that can be modified in combat and need to be reset when combat finishes
    // The modification amount
    private short modInitiative = 0, modAC = 0;

    //for visual stat block visual
    private GameObject statBlockInstance;

    /// <summary>
    ///     Get references to all children that can be modified and find the toggle that can delete it
    /// </summary>
    public void Awake() {
        playerName = transform.Find("PlayerName").gameObject;
        characterName = transform.Find("CharName").gameObject;
        characterClass = transform.Find("CharClass").gameObject;
        characterArmor = transform.Find("CharArmor/Val_Armor").gameObject;
        characterInitiative = transform.Find("CharInit/Val_Init").gameObject;
        characterHealth = transform.Find("HealthDisplay").gameObject;
        characterImage = transform.Find("CharImage").gameObject;

        deletionToggle = GameObject.FindWithTag("DeletionToggle").gameObject;
    }

    // ? PlayerInfo could have some variable that marks that there has been a change
    // void Update() {
    //     check if PlayerInfo has been marked as changed
    //     update displays to match
    // }

    // Update mouse position to enable stat block visual to follow mouse
    private void Update() {
        if (statBlockInstance) {
            statBlockInstance.transform.position = Input.mousePosition;
        }
    }

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
            characterImage.GetComponent<Image>().sprite = monsterImageDictionary.GetSpriteFromType(monster.getType());

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
        // If there is no deletion toggle, or if there is one and it is on, then allow deletion on click
        if (!deletionToggle || deletionToggle.GetComponent<Toggle>().isOn) {
            gameObject.SendMessageUpwards("KillCombatant", gameObject);
            Destroy(statBlockInstance);
        }
    }

    // Detect if mouse hovers over InitativeTab
    public void OnPointerEnter(PointerEventData pointerEventData) {
        // TODO: Fancy Visual stuff when mouse is over a tab
        gameObject.GetComponent<Image>().color = hoverColor;

        // instantiate stat block visual
        Vector2 startPos = gameObject.transform.position;
        Quaternion startRot = gameObject.transform.rotation;
        statBlockInstance = Instantiate(statBlockVisual, startPos, startRot, GameObject.FindGameObjectWithTag("Canvas").transform);

        string charName = characterName.GetComponent<Text>().text;
        string charClass = characterClass.GetComponent<Text>().text;
        string charArmor = characterArmor.GetComponent<Text>().text;
        Alignment charAlignment = managedCombatant.getAlignment();
        Size charSize = managedCombatant.getSize();
        string charHP = managedCombatant.getHP().ToString();
        string charSTR = managedCombatant.getStat_STR().ToString();
        string charDEX = managedCombatant.getStat_DEX().ToString();
        string charCON = managedCombatant.getStat_CON().ToString();
        string charINT = managedCombatant.getStat_INT().ToString();
        string charWIS = managedCombatant.getStat_WIS().ToString();
        string charCHA = managedCombatant.getStat_CHA().ToString();

        // set appropriate fields on stat block visual
        statBlockInstance.GetComponent<SBVFieldSetter>().setFields(charName, charClass, charArmor, managedCombatant.getCurrentHP() / managedCombatant.getCurrentHP(), charAlignment, charSize, charHP);
        statBlockInstance.GetComponent<SBVFieldSetter>().setStats(charSTR, charDEX, charCON, charINT, charWIS, charCHA);
        
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        gameObject.GetComponent<Image>().color = defaultColor;
        Destroy(statBlockInstance);
    }
}
