using System;
using Game.Networking;
using Mirror;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Lobby
{
    public class StartSelectCharactersInButton : MonoBehaviour
    {
        [SerializeField]
        private Button _startSelectCharactersButton;

        private bool _canPlay;

        private void Awake()
        {
            if (NetworkServer.active)
            {
                _startSelectCharactersButton.OnClickAsObservable().Subscribe(_ => StartSelectCharacters());
            } else
            {
                gameObject.SetActive(false);
            }
        }

        private void CheckOnAvailableToStart()
        {
            int countOfDemons = 0;
            foreach (var player in GameNetworkManager.Instance.NetworkPlayers)
            {
                if (player.isDemon)
                {
                    countOfDemons++;
                }
            }
            if (countOfDemons == 1)
            {
                _canPlay = true;
            } else
            {
                _canPlay = false;
            }
        }

        private void StartSelectCharacters()
        {
            CheckOnAvailableToStart();
            if (_canPlay)
            {
                foreach (var player in GameNetworkManager.Instance.NetworkPlayers)
                {
                    if (player.isServer)
                    {
                        player.StartChoosingCharacter();
                    }
                }
            } else
            {
                Debug.Log("Не можем запускать");
            }
        }
    }
}
