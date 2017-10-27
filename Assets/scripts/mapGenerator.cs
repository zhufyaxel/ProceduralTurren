using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour {
	public int mapWidth;
	public int mapHeight;
	public float scale;

	public void GenerateMap(){
		float [,] noiseMap = Noise.GeneratorNoiseMap( mapWidth, mapHeight, scale);
	}
}
