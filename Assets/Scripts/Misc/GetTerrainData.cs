using NaughtyAttributes;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
public class GetTerrainData : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    string heightPath = "Assets/Textures/HeightMap";
    string normalPath = "Assets/Textures/NormalMap";

    [Button]
    void GetHeighMap()
    {
        TerrainData terrainData = terrain.terrainData;

        int resolution = terrainData.heightmapResolution;

        RenderTexture heightMap = terrainData.heightmapTexture;

        RenderTexture.active = heightMap;
        Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
        texture.Apply();
        RenderTexture.active = null;

        byte[] pngData = texture.EncodeToPNG();
        File.WriteAllBytes(heightPath + DateTime.Now.ToString().Replace("/", "_").Replace(":", "-") + ".png", pngData);
        AssetDatabase.ImportAsset(heightPath + DateTime.Now.ToString().Replace("/", "_").Replace(":", "-") + ".png");

        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }
}