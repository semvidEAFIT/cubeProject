using UnityEngine;
using System.Collections;

public class VectorHelper{

	public static Vector3 Multiply(Vector3 v1, Vector3 v2){
		return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
	}
	
	public static bool Eq(Vector3 v1, Vector3 v2){
		return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
	}
	
	public static Vector3 Abs(Vector3 vector){
		return new Vector3(
			Mathf.Abs(vector.x),
			Mathf.Abs(vector.y),
			Mathf.Abs(vector.z));
	}
	
	public static Vector3 BinaryInverse(Vector3 vector){
		Vector3 vector2 = Abs(vector);
		if (vector2.x == 0f){
			vector2.x = 1f;
		}else{
			vector2.x = 0f;
		}
		if (vector2.y == 0f){
			vector2.y = 1f;
		}else{
			vector2.y = 0f;
		}
		if (vector2.z == 0f){
			vector2.z = 1f;
		}else{
			vector2.z = 0f;
		}
		return vector2;
	}
}
