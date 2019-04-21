using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using System;
using Core.Utilities;
using Core.Input;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// A game UI wrapper for a pointer that also contains raycast information
/// </summary>
public struct UIPointer
{
    /// <summary>
    /// The pointer info
    /// </summary>
    public PointerInfo pointer;

    /// <summary>
    /// The ray for this pointer
    /// </summary>
    public Ray ray;

    /// <summary>
    /// The raycast hit object into the 3D scene
    /// </summary>
    public RaycastHit? raycast;

    /// <summary>
    /// True if this pointer started over a UI element or anything the event system catches
    /// </summary>
    public bool overUI;

    public Vector3 HitPosition;
}

public class GameUI :  Singleton<GameUI>
{
    /// <summary>
    /// The states the UI can be in
    /// </summary>
    public enum State
    {
        /// <summary>
        /// The game is in its normal state. Here the player can pan the camera, select units and towers
        /// </summary>
        Normal,

        /// <summary>
        /// The game is in 'build mode'. Here the player can pan the camera, confirm or deny placement
        /// </summary>
        Building,

        /// <summary>
        /// The game is Paused. Here, the player can restart the level, or quit to the main menu
        /// </summary>
        Paused,

        /// <summary>
        /// The game is over and the level was failed/completed
        /// </summary>
        GameOver,

        /// <summary>
        /// The game is in 'build mode' and the player is dragging the ghost tower
        /// </summary>
        BuildingWithDrag
    }


    /// <summary>
    /// Our cached camera reference
    /// </summary>
    Camera m_Camera;
    BuildManager m_BuildManager;
    Vector3 m_GridPosition;
    public Grid m_grid; 
    public LayerMask gridOnPlaneMask; // 网格MASK

    /// <summary>
    /// Gets the current UI state
    /// </summary>
    public State state { get; private set; }

    /// <summary>
    /// Current tower placeholder. Will be null if not in the <see cref="State.Building" /> state.
    /// </summary>
    RoomPlacementGhost m_CurrentRoom;

    /// <summary>
    /// Fires when the <see cref="State"/> changes
    /// should only allow firing when TouchUI is used
    /// </summary>
    public event Action<State, State> stateChanged;

    /// <summary>
    /// Gets the current selected tower
    /// </summary>
    public Room currentSelectedRoom { get; private set; }

    /// <summary>
    /// Placement area ghost tower is currently on
    /// </summary>
    public GameObject m_currenGridArea;

    /// <summary>
    /// Tracks if the ghost is in a valid location and the player can afford it
    /// </summary>
    bool m_GhostPlacementPossible;

    /// <summary>
    /// Gets whether a tower has been selected
    /// </summary>
    public bool isRoomSelected
    {
        get { return currentSelectedRoom != null; }
    }

    /// <summary>
    /// Gets whether certain build operations are valid
    /// </summary>
    public bool isBuilding
    {
        get
        {
            return state == State.Building || state == State.BuildingWithDrag;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        m_BuildManager = GetComponent<BuildManager>();
        state = State.Normal;
        m_Camera = GetComponent<Camera>();
        
    }

    /// <summary>
    /// Changes the state and fires <see cref="stateChanged"/>
    /// </summary>
    /// <param name="newState">The state to change to</param>
    /// <exception cref="ArgumentOutOfRangeException">thrown on an invalid state</exception>
    void SetState(State newState)
    {
        if (state == newState)
        {
            return;
        }
        State oldState = state;
        if (oldState == State.Paused || oldState == State.GameOver)
        {
            Time.timeScale = 1f;
        }

        switch (newState)
        {
            case State.Normal:
                break;
            case State.Building:
                break;
            case State.BuildingWithDrag:
                break;
            case State.Paused:
            case State.GameOver:
                if (oldState == State.Building)
                {
                    //CancelGhostPlacement();
                }
                Time.timeScale = 0f;
                break;
            default:
                throw new ArgumentOutOfRangeException("newState", newState, null);
        }
        state = newState;
        Debug.LogError(state);
        if (stateChanged != null)
        {
            stateChanged(oldState, state);
        }
    }
    /// <summary>
    /// Changes the mode to drag
    /// </summary>
    /// <param name="towerToBuild">
    /// The tower to build
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Throws exception when trying to change to Drag mode when not in Normal Mode
    /// </exception>

    /// <summary>
    /// Sets the UI into a build state for a given tower
    /// </summary>
    /// <param name="towerToBuild">
    /// The tower to build
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Throws exception trying to enter Build Mode when not in Normal Mode
    /// </exception>
    public void SetToBuildMode([NotNull] Room roomToBuild)
    {
        if (state != State.Normal)
        {
            throw new InvalidOperationException("Trying to enter Build mode when not in Normal mode");
        }

        //if (m_CurrentTower != null)
        //{
        //    // Destroy current ghost
        //    CancelGhostPlacement();
        //}
        SetUpGhostRoom(roomToBuild);
        SetState(State.Building);
        
    }

    /// <summary>
    /// Creates and hides the tower and shows the buildInfoUI
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Throws exception if the <paramref name="towerToBuild"/> is null
    /// </exception>
    void SetUpGhostRoom([NotNull] Room roomToBuild)
    {
        //if (roomToBuild == null)
        //{
        //    throw new ArgumentNullException("towerToBuild");
        //}

        m_CurrentRoom = Instantiate(roomToBuild.roomGhostPrefab);
        m_CurrentRoom.Initialize(roomToBuild);
        m_CurrentRoom.Hide();

        ////activate build info
        //if (buildInfoUI != null)
        //{
        //    buildInfoUI.Show(roomToBuild);
        //}
    }

    /// <summary>
    /// Raycast onto tower placement areas
    /// </summary>
    /// <param name="pointer">The pointer we're testing</param>
    protected void GridOnPlaneMaskRaycast(ref UIPointer pointer)
    {
        pointer.raycast = null;

        if (pointer.overUI)
        {
            //Pointer is over UI, so no valid position
            return;
        }

        // Raycast onto placement area layer
        RaycastHit hit;
        if (Physics.Raycast(pointer.ray, out hit, float.MaxValue, gridOnPlaneMask))
        {
            Debug.LogError("HIT");
            pointer.raycast = hit;
            pointer.HitPosition = hit.point;
        }
    }

    public void TryPlaceRoom(PointerInfo pointerInfo)
    {
        UIPointer pointer = WrapPointer(pointerInfo);
        // Do nothing if we're over UI
        if (pointer.overUI)
        {
            Debug.LogError("ui");
            return;
        }
        PlanRoom(pointer);
    }

    /// <summary>
    /// Used to buy the tower during the build phase
    /// Checks currency and calls <see cref="PlaceGhost" />
    /// <exception cref="InvalidOperationException">
    /// Throws exception when not in a build mode or when tower is not a valid position
    /// </exception>
    /// </summary>
    public void PlanRoom(UIPointer pointer)
    {
        if (!isBuilding)
        {
            throw new InvalidOperationException("Trying to buy towers when not in a Build Mode");
        }
        //if (m_CurrentTower == null || !IsGhostAtValidPosition())
        //{
        //    return;
        //}
        GridOnPlaneMaskRaycast(ref pointer);
        if (!pointer.raycast.HasValue || pointer.raycast.Value.collider == null)
        {
            CancelGhostPlacement();
            return;
        }
        //int cost = m_CurrentTower.controller.purchaseCost;
        //bool successfulPurchase = LevelManager.instance.currency.TryPurchase(cost);
        //if (successfulPurchase)
        //{
        PlaceGhost(pointer);
        //m_BuildManager.SelecetRoomRange(pointer);
        //}
    }


    /// <summary>
    /// Place the ghost at the pointer's position
    /// </summary>
    /// <param name="pointer">The pointer to place the ghost at</param>
    /// <exception cref="InvalidOperationException">If we're not in the correct state</exception>
    protected void PlaceGhost(UIPointer pointer)
    {
        if (m_CurrentRoom == null || !isBuilding)
        {
            throw new InvalidOperationException(
                "Trying to position a tower ghost while the UI is not currently in a building state.");
        }

        MoveGhost(pointer);

        if (m_currenGridArea != null)
        {
            RoomFitStatus fits = m_grid.Fits(m_GridPosition, m_CurrentRoom.controller.m_size);

            if (fits == RoomFitStatus.Fits)
            {
                //Place the ghost
                Room controller = m_CurrentRoom.controller;
                Room createdRoom = Instantiate(controller);
                createdRoom.Initialize(m_currenGridArea, m_GridPosition);
                CancelGhostPlacement();
            }
        }
    }

    /// <summary>
    /// Creates a new UIPointer holding data object for the given pointer position
    /// </summary>
    protected UIPointer WrapPointer(PointerInfo pointerInfo)
    {
        return new UIPointer
        {
            overUI = IsOverUI(pointerInfo),
            pointer = pointerInfo,
            ray = m_Camera.ScreenPointToRay(pointerInfo.currentPosition)
        };
    }

    /// <summary>
    /// Checks whether a given pointer is over any UI
    /// </summary>
    /// <param name="pointerInfo">The pointer to test</param>
    /// <returns>True if the event system reports this pointer being over UI</returns>
    protected bool IsOverUI(PointerInfo pointerInfo)
    {
        int pointerId;
        EventSystem currentEventSystem = EventSystem.current;

        // Pointer id is negative for mouse, positive for touch
        var cursorInfo = pointerInfo as MouseCursorInfo;
        var mbInfo = pointerInfo as MouseButtonInfo;
        var touchInfo = pointerInfo as TouchInfo;

        if (cursorInfo != null)
        {
            pointerId = PointerInputModule.kMouseLeftId;
        }
        else if (mbInfo != null)
        {
            // LMB is 0, but kMouseLeftID = -1;
            pointerId = -mbInfo.mouseButtonId - 1;
        }
        else if (touchInfo != null)
        {
            pointerId = touchInfo.touchId;
        }
        else
        {
            throw new ArgumentException("Passed pointerInfo is not a TouchInfo or MouseCursorInfo", "pointerInfo");
        }

        return currentEventSystem.IsPointerOverGameObject(pointerId);
    }
    /// <summary>
    /// Cancel placing the ghost
    /// </summary>
    public void CancelGhostPlacement()
    {
        if (!isBuilding)
        {
            throw new InvalidOperationException("Can't cancel out of ghost placement when not in the building state.");
        }

        //if (buildInfoUI != null)
        //{
        //    buildInfoUI.Hide();
        //}
        Destroy(m_CurrentRoom.gameObject);
        m_CurrentRoom = null;
        SetState(State.Normal);
        //DeselectTower();
    }

    /// <summary>
    /// Position the ghost tower at the given pointer
    /// </summary>
    /// <param name="pointerInfo">The pointer we're using to position the tower</param>
    /// <param name="hideWhenInvalid">Optional parameter for configuring if the ghost is hidden when in an invalid location</param>
    public void TryMoveGhost(PointerInfo pointerInfo, bool hideWhenInvalid = true)
    {
        if (m_CurrentRoom == null)
        {
            throw new InvalidOperationException("Trying to move the tower ghost when we don't have one");
        }

        UIPointer pointer = WrapPointer(pointerInfo);
        // Do nothing if we're over UI
        if (pointer.overUI && hideWhenInvalid)
        {
            m_CurrentRoom.Hide();
            return;
        }
        MoveGhost(pointer, hideWhenInvalid);
    }

    /// <summary>
    /// Move the ghost to the pointer's position
    /// </summary>
    /// <param name="pointer">The pointer to place the ghost at</param>
    /// <param name="hideWhenInvalid">Optional parameter for whether the ghost should be hidden or not</param>
    /// <exception cref="InvalidOperationException">If we're not in the correct state</exception>
    protected void MoveGhost(UIPointer pointer, bool hideWhenInvalid = false)
    {
        if (m_CurrentRoom == null || !isBuilding)
        {
            throw new InvalidOperationException(
                "Trying to position a tower ghost while the UI is not currently in the building state.");
        }

        // Raycast onto placement layer
        GridOnPlaneMaskRaycast(ref pointer);

        if (pointer.raycast != null)
        {
            MoveGhostWithRaycastHit(pointer.raycast.Value);
        }
        else
        {
            MoveGhostOntoWorld(pointer.ray, hideWhenInvalid);
        }
    }

    /// <summary>
    /// Move ghost with successful raycastHit onto m_PlacementAreaMask
    /// </summary>
    protected virtual void MoveGhostWithRaycastHit(RaycastHit raycast)
    {
        //// We successfully hit one of our placement areas
        //// Try and get a placement area on the object we hit
        m_currenGridArea = raycast.collider.gameObject;
        if (m_currenGridArea == null)
        {
            Debug.LogError("There is not an IPlacementArea attached to the collider found on the m_PlacementAreaMask");
            return;
        }
        m_GridPosition = raycast.point;
        RoomFitStatus fits = m_grid.Fits(m_GridPosition, m_CurrentRoom.controller.m_size);

        m_CurrentRoom.Show();
        m_GhostPlacementPossible = fits == RoomFitStatus.Fits;
        m_CurrentRoom.Move(raycast.point,
                            m_currenGridArea.transform.rotation,
                            m_GhostPlacementPossible);
        Debug.LogError("LookHERE");
    }

    /// <summary>
    /// Move ghost with the given ray
    /// </summary>
    protected virtual void MoveGhostOntoWorld(Ray ray, bool hideWhenInvalid)
    {
        //m_CurrentArea = null;

        //if (!hideWhenInvalid)
        //{
        //    RaycastHit hit;
        //    // check against all layers that the ghost can be on
        //    Physics.SphereCast(ray, sphereCastRadius, out hit, float.MaxValue, ghostWorldPlacementMask);
        //    if (hit.collider == null)
        //    {
        //        return;
        //    }
        //    m_CurrentTower.Show();
        //    m_CurrentTower.Move(hit.point, hit.collider.transform.rotation, false);
        //}
        //else
        //{
            m_CurrentRoom.Hide();
        //}
    }
}
