using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Ball;
using Framework;
using Framework.Interfaces;
using Input;
using Objects;
using Player.Movement;
using UnityEngine;
using Player;
using Player.Class;
using Score;
using Object = System.Object;

[SuppressMessage("ReSharper", "Unity.IncorrectMonoBehaviourInstantiation")]
public class Main : MonoBehaviour
{
    //services
    private IScoreTracker _scoreTracker;
    private IObjectTracker _objectTracker;
    
    //players
    [SerializeField] private Transform playerLSpawnLocation;
    [SerializeField] private Transform playerRSpawnLocation;
    [SerializeField] private GameObject playerPrefab;
    
    //ball
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnLocation;
    private BallBehaviour _ball;
    
    private List<IUpdatable> _updateAbles = new();
    
    private void Awake()
    {
        CreateServices();
    }

    private void Start()
    {
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
    
    private void CreateServices()
    { 
        _scoreTracker = ServiceLocator<IScoreTracker>.Provide(new ScoreTracker());

        _objectTracker = ServiceLocator<IObjectTracker>.Provide(new ObjectTracker());
    }
    
    private void SpawnPlayers()
    {
        var playerL = SpawnObject(playerPrefab, playerLSpawnLocation.position, Quaternion.identity);
        var characterL = new CharacterBuilder()
            .AddCharacterStats(new CharacterStats(1.5f, 10f, new  Vector2(0.5f, 5f)))
            .AddClass(new RangerClass())
            .AddMovement(new PlayerMovement
                (new MoveCommand(1, KeyCode.W), new MoveCommand(-1, KeyCode.S), playerL.transform))
            .AddInputCommands(new InputHandler())
            .Build();
        _objectTracker.Add(playerL, characterL);
        _updateAbles.Add(characterL);
        
        var playerR = SpawnObject(playerPrefab, playerRSpawnLocation.position, Quaternion.identity);
        var characterR = new CharacterBuilder()
            .AddCharacterStats(new CharacterStats(1.5f, 10f, new  Vector2(0.5f, 5f)))
            .AddClass(new WizardClass())
            .AddMovement(new PlayerMovement(new MoveCommand(1, KeyCode.UpArrow), new MoveCommand(-1, KeyCode.DownArrow) ,playerR.transform))
            .AddInputCommands(new InputHandler())
            .Build();
        _objectTracker.Add(playerR, characterR);
        _updateAbles.Add(characterR);
    }
    
    private void SpawnBall()
    {
        var ball = SpawnObject(ballPrefab, ballSpawnLocation.position, Quaternion.identity);
        _ball = new BallBehaviour(new Vector2(3f, 4f), ball.GetComponent<Rigidbody2D>(), _scoreTracker);
        _objectTracker.Add(ball, _ball);
        _updateAbles.Add(_ball);
    }

    private GameObject SpawnObject(GameObject prefab, Vector3 position,  Quaternion rotation, params object[] components)
    {
        var obj = Instantiate(prefab, position, rotation);
        _objectTracker.Add(obj, components);
        return obj;
    }
}
