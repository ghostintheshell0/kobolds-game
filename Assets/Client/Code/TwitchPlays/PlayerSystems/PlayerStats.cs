using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
	public string Name;
	public int CurrentOre;
	public int TotalOre;
	public int Level;
	public int Escapes;
	public int Deads;
	public float HeadSize;
	public int WallsDestroyed;
	public int WallsDestroyedInCurrentGame;
	public List<int> Hats;
	public int CurrentHatIndex;
}