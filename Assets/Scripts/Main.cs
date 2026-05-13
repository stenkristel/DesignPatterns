using System;
using System.Collections.Generic;
using BuilderPattern;
using Input;
using Player;
using UnityEngine;

public class Main : MonoBehaviour
{
    //player
    [SerializeField] private Transform playerLSpawnLocation;
    [SerializeField] private Transform playerRSpawnLocation;
    [SerializeField] private GameObject playerPrefab;
    private GameObject _playerL;
    private GameObject _playerR;
    
    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        _playerL = Instantiate(playerPrefab, playerLSpawnLocation.position, Quaternion.identity);
        var characterL = new CharacterBuilder()
            .AddCharacterStats(new CharacterStats(1.5f, 10f, new  Vector2(0.5f, 5f)))
            .AddClass(new RangerClass())
            .AddMovement(new PlayerMovement())
            .AddInputCommands(new InputHandler())
            .Build();
        _playerL.AddComponent<Character>().Paste(characterL);

        _playerR = Instantiate(playerPrefab, playerRSpawnLocation.position, Quaternion.identity);
        var characterR = new CharacterBuilder()
            .AddCharacterStats(new CharacterStats(2f, 7.5f, new Vector2(0.5f, 5f)))
            .AddClass(new WizardClass())
            .Build();
        _playerR.AddComponent<Character>().Paste(characterR);
    }
}
