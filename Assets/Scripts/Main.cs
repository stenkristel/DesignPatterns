using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Ball;
using Framework;
using Framework.Interfaces;
using Input;
using Player.Movement;
using UnityEngine;
using Player;
using Player.Class;
using Score;
using Unity.VisualScripting;

[SuppressMessage("ReSharper", "Unity.IncorrectMonoBehaviourInstantiation")]
public class Main : MonoBehaviour
{
    //players
    [SerializeField] private Transform playerLSpawnLocation;
    [SerializeField] private Transform playerRSpawnLocation;
    [SerializeField] private GameObject playerPrefab;
    private Character _playerL;
    private Character _playerR;
    
    //ball
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnLocation;
    private BallBehaviour _ball;
    
    private List<IUpdatable> _updateAbles = new();
    
    private void Awake()
    {
        CreateObservers();
        SpawnPlayers();
        SpawnBall();
    }

    private void Update()
    {
        foreach (var updateAble in _updateAbles)
        {
            updateAble.OnUpdate();
        }
    }
    
    private void CreateObservers()
    {
        ServiceLocator<ScoreTracker>.Provide(new ScoreTracker());
    }
    
    private void SpawnPlayers()
    {
        var playerL = Instantiate(playerPrefab, playerLSpawnLocation.position, Quaternion.identity);
        _playerL = new CharacterBuilder()
            .AddCharacterStats(new CharacterStats(1.5f, 10f, new  Vector2(0.5f, 5f)))
            .AddClass(new RangerClass())
            .AddMovement(new PlayerMovement
                (new MoveCommand(1, KeyCode.W), new MoveCommand(-1, KeyCode.S),playerL.transform))
            .AddInputCommands(new InputHandler())
            .Build();
        playerL.AddComponent<Character>().Paste(_playerL);
        _updateAbles.Add(_playerL);
        
        var playerR = Instantiate(playerPrefab, playerRSpawnLocation.position, Quaternion.identity);
        _playerR = new CharacterBuilder()
            .AddCharacterStats(new CharacterStats(1.5f, 10f, new  Vector2(0.5f, 5f)))
            .AddClass(new WizardClass())
            .AddMovement(new PlayerMovement(new MoveCommand(1, KeyCode.UpArrow), new MoveCommand(-1, KeyCode.DownArrow) ,playerR.transform))
            .AddInputCommands(new InputHandler())
            .Build();
        playerR.AddComponent<Character>().Paste(_playerR);
        _updateAbles.Add(_playerR);
    }
    
    private void SpawnBall()
    {
        var ball = Instantiate(ballPrefab, ballSpawnLocation.position, Quaternion.identity);
        _ball = new BallBehaviour(new Vector2(3f, 4f), ball.GetComponent<Rigidbody2D>(), ServiceLocator<ScoreTracker>.GetItem);
        var ballComp = ball.AddComponent<BallBehaviour>();
        ballComp.Paste(_ball);
        _updateAbles.Add(ballComp);
    }
}
