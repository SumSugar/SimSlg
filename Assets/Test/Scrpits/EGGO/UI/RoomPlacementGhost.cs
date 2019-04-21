using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlacementGhost : MonoBehaviour {

    public Room controller { get; private set; }
    public GameObject radiusVisualizer;
    public float radiusVisualizerHeight = 0.02f;
    public float dampSpeed = 0.075f;
    public Material material;
    public Material invalidPositionMaterial;
    protected MeshRenderer[] m_MeshRenderers;
    protected Vector3 m_MoveVel;
    protected Vector3 m_TargetPosition;
    protected bool m_ValidPos;
    public Collider ghostCollider { get; private set; }

    public virtual void Initialize(Room room)
    {
        m_MeshRenderers = GetComponentsInChildren<MeshRenderer>();
        controller = room;
        if (GameUI.instanceExists)
        {
            //GameUI.instance.SetupRadiusVisualizer(controller, transform);
        }
        ghostCollider = GetComponent<Collider>();
        m_MoveVel = Vector3.zero;
        m_ValidPos = false;
    }

    /// <summary>
    /// Show this ghost
    /// </summary>
    public virtual void Show()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            m_MoveVel = Vector3.zero;

            m_ValidPos = false;
        }
    }

    /// <summary>
    /// Hide this ghost
    /// </summary>
    public virtual void Hide()
    {
        Debug.LogError("Pause");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Moves this ghost to a given world position
    /// </summary>
    /// <param name="worldPosition">The new position to move to in world space</param>
    /// <param name="rotation">The new rotation to adopt, in world space</param>
    /// <param name="validLocation">Whether or not this position is valid. Ghost may display differently
    /// over invalid locations</param>
    public virtual void Move(Vector3 worldPosition, Quaternion rotation, bool validLocation)
    {
        m_TargetPosition = worldPosition;
        if (!m_ValidPos)
        {
            // Immediately move to the given position
            m_ValidPos = true;
            transform.position = m_TargetPosition;
        }

        transform.rotation = rotation;
        foreach (MeshRenderer meshRenderer in m_MeshRenderers)
        {
            meshRenderer.sharedMaterial = validLocation ? material : invalidPositionMaterial;
        }
    }

    /// <summary>
    /// Damp the movement of the ghost
    /// </summary>
    protected virtual void Update()
    {
        Vector3 currentPos = transform.position;

        if (Vector3.SqrMagnitude(currentPos - m_TargetPosition) > 0.01f)
        {
            currentPos = Vector3.SmoothDamp(currentPos, m_TargetPosition, ref m_MoveVel, dampSpeed);

            transform.position = currentPos;
        }
        else
        {
            m_MoveVel = Vector3.zero;
        }
    }
}
