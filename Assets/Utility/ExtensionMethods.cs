using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{

    public static GameObject Spawn(this GameObject obj, Vector3 pos)
    {
        return GameObject.Instantiate<GameObject>(obj, pos, Quaternion.identity);
    }

    public static Vector2 MaakepRotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }


}


public static class LayerConstants
{
    //public static int Enemies = 8;
    //public static int Players = 9;
    public static string EnemyProjectiles = "EnemyProjectiles";
    public static string PlayerProjectiles = "PlayerProjectiles";
    public static string IgnoreProjectiles = "IgnoreProjectiles";

    public static LayerMask GetLayer(string name)
    {
        return LayerMask.NameToLayer(name);
    }

    public static int GetAllExceptLayers(params string[] names)
    {
        int i = 0;
        foreach (var name in names)
        {
            i += GetLayer(name);
        }
        return ~(1 << i);
    }

    public static int GetOnlyLayer(params string[] names)
    {
        int i = 0;
        foreach (var name in names)
        {
            i += GetLayer(name);
        }
        return (1 << i);
    }
}

