using System;
using UnityEngine;

public static class CoordinateMapper
{
    /// <summary>
    /// Given a pixel position will return a normalized local x-y position of a mesh hit by a raycast through the camera at the given position.
    /// </summary>
    /// <returns>The x-y tuple representing the screen position in mesh-local space, or null if out of range.</returns>
    /// <param name="screenPosition">Screen position.</param>
    public static Tuple<float, float> ScreenPositionToMeshPosition(Vector3 screenPosition)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            // Gets the bounds in local space
            Bounds b = hit.transform.gameObject.GetComponent<MeshFilter>().mesh.bounds;

            // Transforms the hit point into the local space of the mesh
            Vector3 localHitPoint = hit.transform.InverseTransformPoint(hit.point);

            // Gets the hit position in local space ranging from -0.5 to 0.5 for both x and y
            Vector3 differenceVector = localHitPoint - b.center;

            // Normalizes the x and y coordinates to between 0 and 1.
            // N.B this is purely a position and is not a normalized direction vector i.e. the magnitude of the vector is not particularly useful
            Vector2 normalizedLocalHitPosition = new Vector2(differenceVector.x, differenceVector.y) + new Vector2(0.5f, 0.5f);
            return new Tuple<float, float>(normalizedLocalHitPosition.x, normalizedLocalHitPosition.y);
        }

        return null;
    }
}
