using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GlobalPlayers : Singleton<GlobalPlayers>, ISaveable {
        // Prevent use of class outside of singleton
        protected GlobalPlayers() {}

        private PlayerInfoList list = new PlayerInfoList();
        public List<string> playerNames = new List<string>();

        public void Awake() {
            // Register self as somthing that needs to be saved
            // GlobalSaveManager.Instance.RegisterToBeSaved(this);
        }

        public void AddPlayer(PlayerInfo newPlayer) {
            Debug.Log($"Adding {newPlayer.getPlayerName()}");
            list.addPlayer(newPlayer);
            playerNames.Add(newPlayer.getPlayerName());
        }

        public void RemovePlayer(PlayerInfo removedPlayer) {
            list.delPlayer(removedPlayer);
            playerNames.Remove(removedPlayer.getPlayerName());
        }

        public PlayerInfo GetPlayerInfo(string playerName) {
            return list.getPlayerInfo(playerName);
        }

        public PlayerInfo GetPlayerInfo(int playerHash) {
            foreach (PlayerInfo player in list.getList()) {
                if (player.GetHashCode() == playerHash) return player;
            }
            return null;
        }

        // TODO: remove/replace
        public string CreateSaveData() {return "";}

        public void PopulateSaveData(SaveData sd) {

            foreach (PlayerInfo item in list.getList()) {
                Debug.Log($"Saving player: {item.getPlayerName()}");
                sd.s_characters.Add(ObjectCopier.DeepClone<PlayerInfo>(item));
            }
        }

        // TODO: Fix bug where the add new player menu needs to be opened before loaded players
        // TODO:      can be added to the combat queue
        public void LoadFromSaveData(SaveData sd) {
            list.clearList();
            playerNames.Clear();
            Debug.Log("Loading players...");
            foreach (PlayerInfo p in sd.s_characters) {
            //     PlayerInfo tmpPlayer = new PlayerInfo(
            //         p.getPlayerName(),
            //         p.getCharacterName(),
            //         p.getCharacterClass(),
            //         p.getAC(),
            //         p.getHP(),
            //         p.getInitiative()
            //     );
            //     tmpPlayer.setCurrentHP(p.getCurrentHP());
            //     tmpPlayer.setStatusCondition(p.getStatusCondition());
            //     tmpPlayer.setAlignment(p.getAlignment());
            //     tmpPlayer.setSize(p.getSize());
            //     // TODO: all other stats
                PlayerInfo tmpPlayer = ObjectCopier.DeepClone<PlayerInfo>(p);
                AddPlayer(tmpPlayer);
                // playerNames.Add(p.getPlayerName());
            }
        }

    }