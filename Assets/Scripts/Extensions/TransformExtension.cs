using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    /// <summary>
    /// Compute the bounding volume of a collection of elements
    /// </summary>
    /// <param name="things">collection of elements with transforms</param>
    /// <returns>Bounding volume containing all the elements</returns>
    public static Bounds CalculateBounds(IEnumerable<MonoBehaviour> things)
    {
        var enumerator = things.GetEnumerator();
        enumerator.MoveNext();
        Transform first = enumerator.Current.transform;
        Bounds collector = new Bounds(first.position, Vector3.zero);
        foreach(MonoBehaviour b in things)
        {
            collector.Encapsulate(b.transform.CalculateBounds());
        }
        return collector;
    }

    /// <returns>Compute the bounding volume of a given transform and all its children</returns>
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

        foreach (Renderer renderer in root.GetComponents<Renderer>())
        {
            Bounds bounds = renderer.bounds;
            minX = Mathf.Min(bounds.min.x, minX);
            minY = Mathf.Min(bounds.min.y, minY);
            minZ = Mathf.Min(bounds.min.z, minZ);

            maxX = Mathf.Max(bounds.max.x, maxX);
            maxY = Mathf.Max(bounds.max.y, maxY);
            maxZ = Mathf.Max(bounds.max.z, maxZ);
        }

        foreach (Renderer renderer in root.GetComponentsInChildren<Renderer>())
        {
            Bounds bounds = renderer.bounds;
            minX = Mathf.Min(bounds.min.x, minX);
            minY = Mathf.Min(bounds.min.y, minY);
            minZ = Mathf.Min(bounds.min.z, minZ);

            maxX = Mathf.Max(bounds.max.x, maxX);
            maxY = Mathf.Max(bounds.max.y, maxY);
            maxZ = Mathf.Max(bounds.max.z, maxZ);
        }

        root.rotation = currentRotation;

        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, (minZ + maxZ) / 2);
        Vector3 size = new Vector3((maxX - minX), (maxY - minY), (maxZ - minZ));

        return new Bounds(center, size);
    }
}