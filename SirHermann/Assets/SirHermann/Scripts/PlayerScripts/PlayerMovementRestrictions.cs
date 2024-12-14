using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementRestrictions
{
    private PlayerMovement _playerMovement;

    public PlayerMovementRestrictions(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public void DecidePlayerRestrictions()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == Scenes.HouseScene.ToString())
        {
            _playerMovement.SetCanCrouch(false);
            _playerMovement.SetCanJump(false);
        }

    }

}
