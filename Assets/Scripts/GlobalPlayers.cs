using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GlobalPlayers : Singleton<GlobalPlayers>, ISaveable {
        // Prevent use of class outside of singleton
        protected GlobalPlayers() {}

        public PlayerInfoList list = new PlayerInfoList();

        public void Awake() {
            // Register self as somthing that needs to be saved
            GlobalSaveManager.Instance.RegisterToBeSaved(this);
        }

        // TODO: remove/replace
        public string CreateSaveData() {return "";}

        public void PopulateSaveData(SaveData sd) {
            foreach (PlayerInfo item in list.getList()) {
                sd.s_characters.Add(item);
            }
        }

        public void LoadFromSaveData(SaveData sd) {
            foreach (PlayerInfo player in sd.s_characters) {
                list.addPlayer(player);
            }
        }

    }