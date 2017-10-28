using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour {
	public int mapWidth;
	public int mapHeight;
	public float scale;

    public int octaves;
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

	public void GenerateMap(){
		float [,] noiseMap = Noise.GeneratorNoiseMap( mapWidth, mapHeight, seed, scale, octaves,persistance, lacunarity, offset);
        NoiseDisplay display = FindObjectOfType<NoiseDisplay>();
        display.DrawNoiseMap(noiseMap);
	}

    private void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
    }
}
