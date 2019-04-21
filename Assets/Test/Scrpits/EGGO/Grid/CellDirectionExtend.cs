using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomFitStatus
{
    /// <summary>
    /// Tower fits in this location
    /// </summary>
    Fits,

    /// <summary>
    /// Tower overlaps another tower in the placement area
    /// </summary>
    Overlaps,

    /// <summary>
    /// Tower exceeds bounds of the placement area
    /// </summary>
    OutOfBounds
}
public enum CellDirection
{
    N, NE, E, SE, S, SW, W, NW
}

public static class CellDirectionExtend {
    public static CellDirection Opposite(this CellDirection _direction)
    {
        return (int)_direction < 4 ? (_direction + 4) : (_direction - 4);
    }
}
