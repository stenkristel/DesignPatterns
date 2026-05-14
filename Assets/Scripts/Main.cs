using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BuilderPattern;
using Input;
using Interfaces;
using NUnit.Framework;
using Player;
using UnityEngine;

[SuppressMessage("ReSharper", "Unity.IncorrectMonoBehaviourInstantiation")]
public class Main : MonoBehaviour
{
    //player
    [SerializeField] private Transform playerLSpawnLocation;
    [SerializeField] private Transform playerRSpawnLocation;
    [SerializeField] private GameObject playerPrefab;
    private Character _playerL;
    private Character _playerR;
    
    private List<IUpdatable> _updateAbles = new();
    
    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        var playerL = Instantiate(playerPrefab, playerLSpawnLocation.position, Quaternion.identity);
        _playerL = new CharacterBuilder()
            .AddCharacterStats(new CharacterStats(1.5f, 10f, new  Vector2(0.5f, 5f)))
            .AddClass(new RangerClass())
            .AddMovement(new PlayerMovement
                (new MoveCommand(1, KeyCode.W), new MoveCommand(-1, KeyCode.S)
                    ,playerL.transform, 10))
            .AddInputCommands(new InputHandler())
            .Build();
        playerL.AddComponent<Character>().Paste(_playerL);
        _updateAbles.Add(_playerL);
        
        var playerR = Instantiate(playerPrefab, playerRSpawnLocation.position, Quaternion.identity);
        _playerR = new CharacterBuilder()
            .AddCharacterStats(new CharacterStats(1.5f, 10f, new  Vector2(0.5f, 5f)))
            .AddClass(new WizardClass())
            .AddMovement(new PlayerMovement(new MoveCommand(1, KeyCode.UpArrow), new MoveCommand(-1, KeyCode.DownArrow)
                ,playerR.transform, 10))
            .AddInputCommands(new InputHandler())
            .Build();
        playerR.AddComponent<Character>().Paste(_playerR);
        _updateAbles.Add(_playerR);
    }

    private void Update()
    {
        foreach (var updateAble in _updateAbles)
        {
            updateAble.OnUpdate();
        }
    }
}
