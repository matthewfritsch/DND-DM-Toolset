using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

    /** 
     *      Contains all classes that need to be accessed globally, and should not have
     *      more than a single copy active.
     *      Read here if interested : http://wiki.unity3d.com/index.php/Singleton
     **/
    [System.Serializable]
    public class CombatInitiativeQueue : Singleton<CombatInitiativeQueue>, ISaveable {
        // Would change, except for some nice methods in playerinfolist
        protected PlayerInfoList _playersInCombat = new PlayerInfoList();
        protected BeingInfoList _monstersInCombat = new BeingInfoList();
        // private GameObject initiativeQueue;

        // Prevent use of class outside of singleton
        protected CombatInitiativeQueue() {}
        public void Awake() {
            // GlobalSaveManager.Instance.RegisterToBeSaved(this);
            // initiativeQueue = GameObject.FindWithTag("InitiativeList").gameObject;
        }

        // // The initiative tab prefab
        // public GameObject initiativeTab;
        // // An empty gameobject used to keep the hierarchy clean while running
        // public Transform initiativeQueueTabStorage;

        /// <returns>
        /// True in case of successfully adding combatant to fight.
        /// Players cannot have more than one instance the same characters
        /// </returns>
        public bool AddToCombat(BeingInfo combatant) {
            if (combatant.GetType() == typeof(MonsterInfo)) {
                _monstersInCombat.addBeing(combatant);
                return true;
            }

            // Assume that combatant is of type Player
            if (!_playersInCombat.containsPlayer((PlayerInfo)combatant)) {
                _playersInCombat.addPlayer((PlayerInfo)combatant);
                return true;
            
            }

            // The combatant that was to be added was a duplicate of a player
            return false;
        }

        public List<BeingInfo> GetPlayersInCombat() {
            List<BeingInfo> result = new List<BeingInfo>();
            result.AddRange(_playersInCombat.getList());
            return result;
        }

        public List<BeingInfo> GetMonstersInCombat() {
            return _monstersInCombat.getList();
        }

        public List<BeingInfo> GetCombatants() {
            List<BeingInfo> result = new List<BeingInfo>();
            result.AddRange(_playersInCombat.getList());
            result.AddRange(_monstersInCombat.getList());

            return result;
        }

        public void RemoveCombatant(BeingInfo combatant) {
            if (combatant.GetType() == typeof(MonsterInfo)) {
                _monstersInCombat.delBeing(combatant);
                return;
            }

            // Try to remove from monster list first, then try player list
            _playersInCombat.delPlayer((PlayerInfo)combatant);
        }

        // ? This could be used to return some information? Players that survived?
        public void EndCombat() {
            _playersInCombat.clearList();
            _monstersInCombat.clearList();
        }

    // Inherited from ISaveable, turns the players/monsters in combat into saveable data as a string
    public string CreateSaveData() {
        // get all of the combatants
        List<BeingInfo> allCombatants = GetCombatants();

        // save them into a BeingInfoList
        BeingInfoList allCombatantsBIL = new BeingInfoList();
        foreach (BeingInfo combatant in allCombatants) {
            allCombatantsBIL.addBeing(combatant);
        }

        // return the save data
        string saveData = JsonUtility.ToJson(allCombatantsBIL);
        return saveData;
    }

    public void PopulateSaveData(SaveData sd) {
        foreach (PlayerInfo c_player in _playersInCombat.getList()) {
            sd.s_player_combatants.Add(c_player.GetHashCode());
        }

        List<MonsterInfo> monList = _monstersInCombat.getList().Cast<MonsterInfo>().ToList();
        foreach (MonsterInfo c_monster in monList) {
            sd.s_monster_combatants.Add(c_monster);
        }
    }

    public void LoadFromSaveData(SaveData sd) {
        // Remove any stragglers from some other save
        EndCombat();
        foreach (MonsterInfo monst in sd.s_monster_combatants) {
            AddToCombat(ObjectCopier.DeepClone<MonsterInfo>(monst));
        }

        foreach (int playerId in sd.s_player_combatants) {
            // ? Potential problem if GlobalPlayers is loaded after InitiativeQueue
            // ? in that players wont be loaded into queue
            PlayerInfo pC = GlobalPlayers.Instance.GetPlayerInfo(playerId);
            Assert.IsNotNull(pC);
            AddToCombat(pC);
        }
    }
}