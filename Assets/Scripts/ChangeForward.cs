using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChangeForward : MonoBehaviour
{
    [Inject]
    private GameSystem _gameSystem;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            switch (_gameSystem.GameState)
            {
                case GameSystem.GameStateEnum.Srart:
                    _gameSystem.StartGame();
                    break;
                case GameSystem.GameStateEnum.Game:
                    transform.forward = transform.right;
                    break;
                case GameSystem.GameStateEnum.Fail:
                    _gameSystem.RestartGame();
                    break;
            }
        }
    }
}
