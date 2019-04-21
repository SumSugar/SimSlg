using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulidSiderbar : MonoBehaviour {
    public List<Room> configurations;
    public RoomPlacementGhost roomGhostPrefab;
    public RoomSpawnButton roomSpawnButton;
    /// <summary>
    /// Initialize the tower spawn buttons
    /// </summary>
    protected virtual void Start()
    {
        //if (!LevelManager.instanceExists)
        //{
        //    Debug.LogError("[UI] No level manager for tower list");
        //}
        //foreach (Tower tower in LevelManager.instance.towerLibrary)
        //{
        foreach (Room room in configurations)
        {
            RoomSpawnButton button = Instantiate(roomSpawnButton, transform);
            //Room room = new Room();
            button.InitializeButton(room);
            button.buttonTapped += OnButtonTapped;
        }
    }

    /// <summary>
    /// Sets the GameUI to build mode with the <see cref="towerData"/>
    /// </summary>
    /// <param name="towerData"></param>
    void OnButtonTapped(Room roomData)
    {
        var gameUI = GameUI.instance;
        if (gameUI.isBuilding)
        {
            //gameUI.CancelGhostPlacement();
        }
        gameUI.SetToBuildMode(roomData);
    }



    /// <summary>
    /// Unsubscribes from all the tower spawn buttons
    /// </summary>
    void OnDestroy()
    {
        RoomSpawnButton[] childButtons = GetComponentsInChildren<RoomSpawnButton>();

        foreach (RoomSpawnButton towerButton in childButtons)
        {
            towerButton.buttonTapped -= OnButtonTapped;
        }
    }

    /// <summary>
    /// Called by start wave button in scene
    /// </summary>
    public void StartWaveButtonPressed()
    {
        //if (LevelManager.instanceExists)
        //{
        //    LevelManager.instance.BuildingCompleted();
        //}
    }

    /// <summary>
    /// Debug button to add currency
    /// </summary>
    /// <param name="amount">How much to add</param>
    public void AddCurrency(int amount)
    {
        //if (LevelManager.instanceExists)
        //{
        //    LevelManager.instance.currency.AddCurrency(amount);
        //}
    }
}
