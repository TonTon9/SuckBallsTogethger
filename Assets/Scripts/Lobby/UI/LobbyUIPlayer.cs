using Game.Networking.Player;
using Mirror;
using TMPro;
using UnityEngine;

namespace Game.Lobby
{

    public class LobbyUIPlayer : NetworkBehaviour
    {
        [SyncVar (hook = nameof(OnNickNameChanged))]
        public string PlayerName;
        
        [SyncVar]
        public GameNetworkPlayer ownPlayer;

        [SerializeField]
        private TextMeshProUGUI _playerNickName;

        [SerializeField] private GameObject _allyMarker;
        [SerializeField] private GameObject _demonMarker;
        
        private void OnNickNameChanged(string ondValue, string newValue)
        {
            _playerNickName.text = newValue;
        }
        
        public void Init(string name, GameNetworkPlayer player)
        {
            PlayerName = name;
            ownPlayer = player;
            InitDataByPlayer();
        }

        private void Start()
        {
            InitDataByPlayer();
        }

        private void InitDataByPlayer()
        {
            ownPlayer.OnPlayerChangeSide += OnPlayerChangeSide;
            transform.SetParent(LobbyUIPlayersHolder.Instance.transform);
            transform.localScale = Vector3.one;
        }

        
        private void OnPlayerChangeSide(GameNetworkPlayer.PlayerSide side)
        {
            if (isServer)
            {
                ChangeMarker(side);
            } else
            {
                ChangeModelRpc(side);
            }
        }

        [Command]
        private void ChangeModelRpc(GameNetworkPlayer.PlayerSide side)
        {
            ChangeMarker(side);
        }

        [ClientRpc]
        private void ChangeMarker(GameNetworkPlayer.PlayerSide side)
        {
            if (side == GameNetworkPlayer.PlayerSide.Ally)
            {
                _allyMarker.SetActive(true);
                _demonMarker.SetActive(false);
            } else
            {
                _allyMarker.SetActive(false);
                _demonMarker.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            NetworkServer.Destroy(gameObject);
            if (ownPlayer != null)
            {
                ownPlayer.OnPlayerChangeSide -= ChangeMarker;
            }
        }
    }

}