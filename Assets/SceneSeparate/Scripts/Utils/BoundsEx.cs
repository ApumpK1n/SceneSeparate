using UnityEngine;
using System.Collections;

public static class BoundsEx
{
    /// <summary>
    /// 绘制包围盒
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="color"></param>
    public static void DrawBounds(this Bounds bounds, Color color)
    {
        Gizmos.color = color;

        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

    /// <summary>
    /// 判断包围盒是否包含另一个包围盒
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="compareTo"></param>
    /// <returns></returns>
    public static bool IsBoundsContainsAnotherBounds(this Bounds bounds, Bounds compareTo)
    {
        if (
            !bounds.Contains(compareTo.center +
                             new Vector3(-compareTo.size.x / 2, compareTo.size.y / 2, -compareTo.size.z / 2)))
            return false;
        if (
            !bounds.Contains(compareTo.center + new Vector3(compareTo.size.x / 2, compareTo.size.y / 2, -compareTo.size.z / 2)))
            return false;
        if (!bounds.Contains(compareTo.center + new Vector3(compareTo.size.x / 2, compareTo.size.y / 2, compareTo.size.z / 2)))
            return false;
        if (
            !bounds.Contains(compareTo.center + new Vector3(-compareTo.size.x / 2, compareTo.size.y / 2, compareTo.size.z / 2)))
            return false;
        if (
            !bounds.Contains(compareTo.center +
                             new Vector3(-compareTo.size.x / 2, -compareTo.size.y / 2, -compareTo.size.z / 2)))
            return false;
        if (
            !bounds.Contains(compareTo.center +
                             new Vector3(compareTo.size.x / 2, -compareTo.size.y / 2, -compareTo.size.z / 2)))
            return false;
        if (
            !bounds.Contains(compareTo.center + new Vector3(compareTo.size.x / 2, -compareTo.size.y / 2, compareTo.size.z / 2)))
            return false;
        if (
            !bounds.Contains(compareTo.center +
                             new Vector3(-compareTo.size.x / 2, -compareTo.size.y / 2, compareTo.size.z / 2)))
            return false;
        return true;
    }

    /// <summary>
    /// 合并两个包围盒
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="boundsUnion"></param>
    /// <returns></returns>
    public static Bounds Union(this Bounds bounds, Bounds boundsUnion)
    {
        if (boundsUnion.size == Vector3.zero) return bounds;
        if (bounds.size == Vector3.zero) return boundsUnion;

        Vector3 min = Vector3.Min(bounds.min, boundsUnion.min);
        Vector3 max = Vector3.Max(bounds.max, boundsUnion.max);

        Bounds result = new Bounds(Vector3.zero, Vector3.zero);
        result.SetMinMax(min, max);
        return result;
    }

    /// <summary>
    /// 判断AABB哪个轴的长度最长
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    public static int MaxExtent(this Bounds bounds)
    {
        Vector3 d = bounds.max - bounds.min;
        if (d.x > d.y && d.x > d.z)
            return 0; //x
        else if (d.y > d.z)
            return 1; //y
        else
            return 2; //z
    }
}
