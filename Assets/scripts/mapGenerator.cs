﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour {
    public enum DrawMode
    {
        NoiseMap, ColorMap, Mesh
    }
    public DrawMode drawMode;
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public int mapWidth;
	public int mapHeight;
	public float scale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions; 

	public void GenerateMap(){
		float [,] noiseMap = Noise.GeneratorNoiseMap( mapWidth, mapHeight, seed, scale, octaves,persistance, lacunarity, offset);
        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x< mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                   if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
           
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
        if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
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

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}
