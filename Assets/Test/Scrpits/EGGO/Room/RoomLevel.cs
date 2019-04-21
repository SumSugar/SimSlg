using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLevel : MonoBehaviour {

    /// <summary>
    /// The prefab for communicating placement in the scene
    /// </summary>
    public RoomPlacementGhost towerGhostPrefab;

    /// <summary>
    /// The parent tower controller of this tower
    /// </summary>
    protected Room m_ParentTower;

    /// <summary>
    /// The physics layer mask that the tower searches on
    /// </summary>
    public LayerMask mask { get; protected set; }

    /// <summary>
    /// Initialises the Effects attached to this object
    /// </summary>
    public virtual void Initialize(Room tower, LayerMask enemyMask)
    {
        mask = enemyMask;
        m_ParentTower = tower;
    }
}
