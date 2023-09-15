using Game.Networking;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuCreateLobbyButton : MonoBehaviour
    {
        [SerializeField]
        private Button _createLobbyButton;

        private void Awake()
        {
            _createLobbyButton.OnClickAsObservable().Subscribe(_ => CreateLobby());
        }

        private void CreateLobby()
        {
            GameNetworkManager.Instance.StartLobbyAsHost();
        }
    }
}
