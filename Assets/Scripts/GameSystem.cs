using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSystem
{
    [Inject]
    readonly SignalBus _signalBus;

    public enum GameStateEnum
    {
        Srart,
        Game,
        Fail,
    }

    private GameStateEnum _currentState = GameStateEnum.Srart;
    public GameStateEnum GameState => _currentState;
    public bool IsOnGame => _currentState == GameStateEnum.Game;

    public void StartGame()
    {
        if (_currentState == GameStateEnum.Srart)
        {
            _currentState = GameStateEnum.Game;
        }
    }

    public void GameFail()
    {
        if (_currentState == GameStateEnum.Game)
        {
            _currentState = GameStateEnum.Fail;
        }
    }

    public void RestartGame()
    {
        if (_currentState == GameStateEnum.Fail)
        {
            _currentState = GameStateEnum.Srart;
            _signalBus.Fire<RestartSignal>();
        }
    }
}
