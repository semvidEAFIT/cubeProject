using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Vector3EqualityComparer : IEqualityComparer<Vector3>{

    public bool Equals(Vector3 v1, Vector3 v2)
    {
        return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z; 
    }

    public int GetHashCode(Vector3 obj)
    {
        string xcomp = (obj.x >= 0) ? "0" + ((int)obj.x).ToString() : "1" + Mathf.Abs((int)obj.x).ToString(); //0 Positivo o 0, 1 Negativo
        string ycomp = (obj.y >= 0) ? "0" + ((int)obj.y).ToString() : "1" + Mathf.Abs(((int)obj.y)).ToString(); //0 Positivo o 0, 1 Negativo
        string zcomp = (obj.z >= 0) ? "0" + ((int)obj.z).ToString() : "1" + Mathf.Abs((int)obj.z).ToString(); //0 Positivo o 0, 1 Negativo
        string hashCode = xcomp + ycomp + zcomp;
        return (int.Parse(hashCode));
    }
}
