using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    /** 
     *      Contains all classes that need to be accessed globally, and should not have
     *      more than a single copy active.
     *      Read here if interested : http://wiki.unity3d.com/index.php/Singleton
     **/

    public class CombatInitiativeQueue : Singleton<CombatInitiativeQueue> {
        // Prevent use of class outside of singleton
        protected CombatInitiativeQueue() {}

        // // The initiative tab prefab
        // public GameObject initiativeTab;
        // // An empty gameobject used to keep the hierarchy clean while running
        // public Transform initiativeQueueTabStorage;


        protected PlayerInfoList _playersInCombat = new PlayerInfoList();
        // TODO: Replace list type with monster specific one
        protected PlayerInfoList _monstersInCombat = new PlayerInfoList();
        
        public void AddPlayerToCombat(PlayerInfo player) {
            // Debug.Log("Adding player " + player.getPlayerName());
            _playersInCombat.addPlayer(player);
            // return CreateInitiativeTab(player);
        }

        // TODO: Replace parameter with monster specific
        public void AddMonsterToCombat(PlayerInfo monster) {
            _monstersInCombat.addPlayer(monster);
        }

        // TODO: Replace return with common parent type
        public List<PlayerInfo> GetPlayersInCombat() {
            return _playersInCombat.getList();
        }

        // TODO: Replace return with common parent type
        public List<PlayerInfo> GetMonstersInCombat() {
            return _monstersInCombat.getList();
        }

        // TODO: Replace return with common parent type
        public List<PlayerInfo> GetCombatants() {
            return _playersInCombat.getList().Concat(_monstersInCombat.getList()).ToList();
        }

        // TODO: Replace with common parent type
        public void RemoveCombatant(PlayerInfo combatant) {
            // Try to remove from monster list first, then try player list
            if (!_monstersInCombat.delPlayer(combatant)) {
                _playersInCombat.delPlayer(combatant);
            }
        }
        // public void RemovePlayerFromCombat(PlayerInfo player) {
        //     _playersInCombat.delPlayer(player);
        // }

        // // TODO: Replace list type with monster specific one
        // public void RemoveMonsterFromCombat(PlayerInfo monster) {
        //     _monstersInCombat.delPlayer(monster);
        // }

        // ? This could be used to return some information? Players that survived?
        public void EndCombat() {
            _playersInCombat.clearList();
            _monstersInCombat.clearList();
        }

        // // TODO: Replace PlayerInfo with player/monster parent class
        // private GameObject CreateInitiativeTab(PlayerInfo combatant) {
        //     GameObject newTab = Instantiate(initiativeTab);
        //     newTab.transform.SetParent(initiativeQueueTabStorage);

        //     // newTab.transform.Find

        //     return newTab;

        // }

    }