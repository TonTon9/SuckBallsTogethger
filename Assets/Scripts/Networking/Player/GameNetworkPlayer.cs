using System;
using Mirror;
using UnityEngine;

namespace Game.Networking.Player
{
    public class GameNetworkPlayer : NetworkBehaviour
    {
        public enum PlayerSide
        {
            Ally,
            Demon
        }
        
        [SyncVar]
        public string playerName;
        
        [SyncVar]
        public bool isDemon;
        
        public event Action<PlayerSide> OnPlayerChangeSide = delegate {};
        public event Action OnPlayerChangeSideServer = delegate {};
        public static event Action StartChoosingCharacters = delegate {};

        public override void OnStartClient()
        {
            DontDestroyOnLoad(gameObject);
            GameNetworkManager.Instance.NetworkPlayers.Add(this);
            base.OnStartClient();
        }
        
        public override void OnStopClient() {
            base.OnStopClient();
            GameNetworkManager.Instance.NetworkPlayers.Remove(this);
        }

        [Command]
        public void SetIsDemon(bool isDemon)
        {
            this.isDemon = isDemon;
            ServerOnPlayerChangeSide();
        }

        [Server]
        private void ServerOnPlayerChangeSide()
        {
            OnPlayerChangeSideServer.Invoke();
        }

        public void ChangeView(bool isDemon)
        {
            if (isDemon)
            {
                OnPlayerChangeSide?.Invoke(PlayerSide.Demon);
            } else
            {
                OnPlayerChangeSide?.Invoke(PlayerSide.Ally);
            }
        }
        
        [ClientRpc]
        public void StartChoosingCharacter()
        {
            StartChoosingCharacters?.Invoke();
        }
    }
}
