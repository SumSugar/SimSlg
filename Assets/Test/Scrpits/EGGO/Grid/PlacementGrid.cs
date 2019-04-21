using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Utilities;

public class PlacementGrid : MonoBehaviour {
    public Cell[] placementCells;

    public enum TowerFitStatus
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

    /// <summary>
    /// Tests whether the indicated cell range represents a valid placement location.
    /// </summary>
    /// <param name="gridPos">The grid location</param>
    /// <param name="size">The size of the item</param>
    /// <returns>Whether the indicated range is valid for placement.</returns>
    //public TowerFitStatus Fits(IntVector2 gridPos, IntVector2 size)
    //{
    //    // If the tile size of the tower exceeds the dimensions of the placement area, immediately decline placement.
    //    if ((size.x > dimensions.x) || (size.y > dimensions.y))
    //    {
    //        return TowerFitStatus.OutOfBounds;
    //    }

    //    IntVector2 extents = gridPos + size;

    //    // Out of range of our bounds
    //    if ((gridPos.x < 0) || (gridPos.y < 0) ||
    //        (extents.x > dimensions.x) || (extents.y > dimensions.y))
    //    {
    //        return TowerFitStatus.OutOfBounds;
    //    }

    //    // Ensure there are no existing towers within our tile silhuette.
    //    for (int y = gridPos.y; y < extents.y; y++)
    //    {
    //        for (int x = gridPos.x; x < extents.x; x++)
    //        {
    //            if (m_AvailableCells[x, y])
    //            {
    //                return TowerFitStatus.Overlaps;
    //            }
    //        }
    //    }

    //    // If we've got this far, we've got a valid position.
    //    return TowerFitStatus.Fits;
    //}
}
