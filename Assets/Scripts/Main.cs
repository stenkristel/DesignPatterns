using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Ball;
using Border;
using Enums;
using Framework;
using Framework.Interfaces;
using Input;
using Objects;
using Player.Movement;
using UnityEngine;
using Player;
using Player.Class;
using Score;
using BoxCollider = Collision.BoxCollider;
using Object = System.Object;

[SuppressMessage("ReSharper", "Unity.IncorrectMonoBehaviourInstantiation")]
public class Main : MonoBehaviour
{
    [Header("Services")]
    private IScoreTracker _scoreTracker;
    private IObjectTracker _objectTracker;
    
    [Header("Players")]
    [SerializeField] private Transform playerLSpawnLocation;
    [SerializeField] private Transform playerRSpawnLocation;
    [SerializeField] private GameObject playerPrefab;
    
    [Header("ball")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballSpawnLocation;
    private BallMovement _ball;
    
    [Header("Borders & goals")]
    [SerializeField] private GameObject[] horizontalBorders;
    [SerializeField] private GameObject leftGoal;
    [SerializeField] private GameObject rightGoal;
    
    private void Awake()
    {
        CreateServices();
    }

    private void Start()
    {
        SpawnPlayers();
        SpawnBall();
        GiveComponents();
    }

    private void Update()
    {
        _objectTracker.OnUpdate();
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
        _objectTracker.Add(playerL, characterL, new VerticleBorder());
        
        var playerR = SpawnObject(playerPrefab, playerRSpawnLocation.position, Quaternion.identity);
        var characterR = new CharacterBuilder()
            .AddCharacterStats(new CharacterStats(1.5f, 10f, new  Vector2(0.5f, 5f)))
            .AddClass(new WizardClass())
            .AddMovement(new PlayerMovement(new MoveCommand(1, KeyCode.UpArrow), new MoveCommand(-1, KeyCode.DownArrow) ,playerR.transform))
            .AddInputCommands(new InputHandler())
            .Build();
        _objectTracker.Add(playerR, characterR, new VerticleBorder());
    }
    
    private void SpawnBall()
    {
        var ball = SpawnObject(ballPrefab, ballSpawnLocation.position, Quaternion.identity);
        var boxCollider = new BoxCollider(ball);
        _ball = new BallMovement(new Vector2(3f, 4f), ball.GetComponent<Rigidbody2D>(), _scoreTracker, boxCollider, _objectTracker);
        _objectTracker.Add(ball, _ball, boxCollider);
    }
    
    private void GiveComponents()
    {
        foreach (var border in horizontalBorders)
        {
            _objectTracker.Add(border, new HorizontalBorder());
        }

        _objectTracker.Add(leftGoal, new GoalBorder(Players.PlayerL));
        _objectTracker.Add(rightGoal, new GoalBorder(Players.PlayerR));
    }

    private GameObject SpawnObject(GameObject prefab, Vector3 position,  Quaternion rotation, params object[] components)
    {
        var obj = Instantiate(prefab, position, rotation);
        _objectTracker.Add(obj, components);
        return obj;
    }
}
