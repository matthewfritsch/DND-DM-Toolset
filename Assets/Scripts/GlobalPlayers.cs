using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GlobalPlayers : Singleton<GlobalPlayers> {
        // Prevent use of class outside of singleton
        protected GlobalPlayers() {}

        public PlayerInfoList list = new PlayerInfoList();

    }