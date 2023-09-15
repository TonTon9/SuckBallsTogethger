using Game.Networking;
using Mirror;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Lobby
{
    public class StartBeDemonLobbyButton : MonoBehaviour
    {
        [SerializeField]
        private Button _beDemonButton;

        private void Awake()
        {
            _beDemonButton.OnClickAsObservable().Subscribe(_ => BeDemon());
        }

        private void BeDemon()
        {
            foreach (var player in GameNetworkManager.Instance.NetworkPlayers)
            {
                if (player.isOwned)
                {
                    player.SetIsDemon(true);
                    player.ChangeView(true);
                }
            }
        }
    }
}
