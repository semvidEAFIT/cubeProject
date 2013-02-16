using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeHelper
{
	public static Vector3 GetTopPosition (Vector3 position)
	{
		bool isFree = !Level.Singleton.ContainsElement (position);
		if (!isFree) {
			while (!isFree) {
				position.y += 1.0f;
				isFree = !Level.Singleton.ContainsElement (position);
			}
		} else {
			while (isFree) {
				position.y -= 1.0f;
				isFree = !Level.Singleton.ContainsElement (position);
				if (position.y < 0) {
					break;
				}
			}
			position.y += 1.0f;
		}

		return position;
	}

	public static bool IsFree (Vector3 position)
	{
		return !Level.Singleton.ContainsElement (position);
	}

	public static Vector3 GetLastPositionInDirection (Vector3 position, Vector3 direction)
	{
		int diff = GetDifferenceInDirection (position, direction.normalized);
		if (diff >= 0) {
			return position + (direction * (diff - 1));
		} else {
			throw new UnityException ("No encontro cubo en esa direccion: " + direction);
		}
	}
	
	public static Entity GetEntityInPosition (Vector3 position)
	{
		if (Level.Singleton.ContainsElement (position)) {
			return Level.Singleton.getEntity (position);
		} else {
			return null;
		}
	}

	public static Vector3[] GetPositionsAround (Vector3 position)
	{
		List<Vector3> positionsAround = new List<Vector3> ();
		positionsAround.Add (position + Vector3.up);
		positionsAround.Add (position + Vector3.down);
		positionsAround.Add (position + Vector3.right);
		positionsAround.Add (position + Vector3.left);
		positionsAround.Add (position + Vector3.forward);
		positionsAround.Add (position + Vector3.back);
		return positionsAround.ToArray ();
	}

	public static Entity[] GetEntitiesAround (Vector3 position)
	{
		Vector3[] positionsAround = GetPositionsAround (position);
		List<Entity> entitiesAround = new List<Entity> ();
		foreach (Vector3 v in positionsAround) {
			if (!IsFree (v)) {
				entitiesAround.Add (GetEntityInPosition (v));
			}
		}
		return entitiesAround.ToArray ();
	}
	
	public static int GetDifferenceInDirection (Vector3 position, Vector3 direction)
	{
		direction = direction.normalized;
		List<Vector3> inColumn = new List<Vector3> ();
		Vector3 valueFinder = VectorHelper.BinaryInverse (direction);
		valueFinder = VectorHelper.Abs (valueFinder);
		Vector3 columnPosition = VectorHelper.Multiply (position, valueFinder);
		Vector3 valueGetter = VectorHelper.Abs (direction);
		float min = -1;
		foreach (Vector3Int key in Level.Singleton.Entities.Keys) {
			Vector3 keyV = key.PositionFloats;
			if (!(position == keyV)
				&& VectorHelper.Eq (VectorHelper.Multiply (keyV, valueFinder), columnPosition)) {
				Vector3 directionTemp = keyV - position;
				float diff = Vector3.Dot (direction, directionTemp);
				if (diff > 0) {
					if (min == -1) {
						min = diff;
					} else {
						min = Mathf.Min (min, diff);
					}
				}
			}
		}
		return Mathf.RoundToInt (min);
	}
}
