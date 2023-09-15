using System;
using Game.Networking;
using Game.Networking.Player;
using Mirror;
using TMPro;
using UnityEngine;

namespace Game.Lobby
{
    public class LobbyPlayer : NetworkBehaviour
    {
        [SyncVar (hook = nameof(OnNickNameChanged))]
        public string PlayerName;
        
        [SyncVar]
        public GameNetworkPlayer ownPlayer;

        [SerializeField]
        private TextMeshProUGUI _playerNickName;

        [SerializeField] private GameObject _allyModel;
        [SerializeField] private GameObject _demonModel;

        public void Init(string name, GameNetworkPlayer player)
        {
            PlayerName = name;
            ownPlayer = player;
            InitDataByPlayer();
        }

        private void OnNickNameChanged(string ondValue, string newValue)
        {
            _playerNickName.text = newValue;
        }

        private void Start()
        {
            InitDataByPlayer();
        }

        private void InitDataByPlayer()
        {
            ownPlayer.OnPlayerChangeSide += OnPlayerChangeSide;   
        }

        
        private void OnPlayerChangeSide(GameNetworkPlayer.PlayerSide side)
        {
            if (isServer)
            {
                ChangeModel(side);
            } else
            {
                ChangeModelRpc(side);
            }
        }

        [Command]
        private void ChangeModelRpc(GameNetworkPlayer.PlayerSide side)
        {
            ChangeModel(side);
        }

        [ClientRpc]
        private void ChangeModel(GameNetworkPlayer.PlayerSide side)
        {
            if (side == GameNetworkPlayer.PlayerSide.Ally)
            {
                _allyModel.SetActive(true);
                _demonModel.SetActive(false);
            } else
            {
                _allyModel.SetActive(false);
                _demonModel.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            if (ownPlayer != null)
            {
                ownPlayer.OnPlayerChangeSide -= ChangeModel;
            }
        }
    }
}
