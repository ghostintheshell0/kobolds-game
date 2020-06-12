using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
	public string Name;
	public int Ore;
	public int Level;
	public int Escapes;
	public int Deads;
	public Color SkinColor;
	public float HeadSize;
	public int WallsDestroyed;
	public int WallsDestroyedInCurrentGame;
	public List<int> Hats = new List<int>();
	public int CurrentHatIndex;
}