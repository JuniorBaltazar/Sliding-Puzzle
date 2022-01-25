using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    /// <summary>
    /// Compute the bounding volume of a given transform
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static Bounds CalculateBounds(this Transform root)
    {
        Quaternion currentRotation = root.rotation;
        root.rotation = Quaternion.identity;

        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float minZ = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;
        float maxZ = float.MinValue;

        Renderer renderer = root.GetComponent<Renderer>();

        Bounds bounds = renderer.bounds;
        minX = Mathf.Min(bounds.min.x, minX);
        minY = Mathf.Min(bounds.min.y, minY);
        minZ = Mathf.Min(bounds.min.z, minZ);

        maxX = Mathf.Max(bounds.max.x, maxX);
        maxY = Mathf.Max(bounds.max.y, maxY);
        maxZ = Mathf.Max(bounds.max.z, maxZ);

        root.rotation = currentRotation;

        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, (minZ + maxZ) / 2);
        Vector3 size = new Vector3((maxX - minX), (maxY - minY), (maxZ - minZ));

        return new Bounds(center, size);
    }
}