using Game.Networking;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Lobby
{
    public class StartBeAllyLobbyButton : MonoBehaviour
    {
        [SerializeField]
        private Button _beAllyButton;

        private void Awake()
        {
            _beAllyButton.OnClickAsObservable().Subscribe(_ => BeAlly());
        }

        private void BeAlly()
        {
            foreach (var player in GameNetworkManager.Instance.NetworkPlayers)
            {
                if (player.isOwned)
                {
                    player.SetIsDemon(false);
                    player.ChangeView(false);
                }
            }
        }
    }

}
