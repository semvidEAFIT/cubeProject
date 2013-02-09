using UnityEngine;
using System.Collections;

public class Vector3Int {

	private int xInt,yInt,zInt;
	
	#region Constructors
	public Vector3Int(int x, int y, int z){
		this.xInt = x;
		this.yInt = y;
		this.zInt = z;
	}
	
	public Vector3Int(Vector3 position) : this(Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z)){
		
	}
	#endregion
		
	#region Gets and Sets
	public int x {
		get {
			return this.xInt;
		}
		set {
			xInt = value;
		}
	}

	public int y {
		get {
			return this.yInt;
		}
		set {
			yInt = value;
		}
	}

	public int z {
		get {
			return this.zInt;
		}
		set {
			zInt = value;
		}
	}
	
	public Vector3 PositionFloats{
		get {
			return new Vector3(xInt,yInt,zInt);
		}
	}

	#endregion
	
	public static Vector3Int getVector(Vector3 vector){
		return new Vector3Int(vector);
	}
	
	public static bool VEquals(Vector3Int v1, Vector3Int v2){
		return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
	}
}
