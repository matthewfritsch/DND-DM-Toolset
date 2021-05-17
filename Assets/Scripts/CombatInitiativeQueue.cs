using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    /** 
     *      Contains all classes that need to be accessed globally, and should not have
     *      more than a single copy active.
     *      Read here if interested : http://wiki.unity3d.com/index.php/Singleton
     **/
    [System.Serializable]
    public class CombatInitiativeQueue : Singleton<CombatInitiativeQueue>, ISaveable {
        // Prevent use of class outside of singleton
        protected CombatInitiativeQueue() {}

        // // The initiative tab prefab
        // public GameObject initiativeTab;
        // // An empty gameobject used to keep the hierarchy clean while running
        // public Transform initiativeQueueTabStorage;

        // Would change, except for some nice methods in playerinfolist
        protected PlayerInfoList _playersInCombat = new PlayerInfoList();
        protected BeingInfoList _monstersInCombat = new BeingInfoList();

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
        sd.s_combatants = GetCombatants();
    }

    public void LoadFromSaveData(SaveData sd) {
        EndCombat();
        foreach (BeingInfo combatant in sd.s_combatants) {
            AddToCombat(combatant);
        }
    }
}