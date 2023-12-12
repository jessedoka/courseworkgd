using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettings : ScriptableObject
{
    // refrence to the planet material
    public Material planetMaterial;

    // refrence to the biome colour settings
    public BiomeColourSettings biomeColourSettings;


    [System.Serializable]
    public class BiomeColourSettings
    {

        // array of biomes that I can create 
        public Biome[] biomes;

        // settings for the noise
        public NoiseSettings noise;

        // offset of the noise
        public float noiseOffset;

        // strength of the noise, with higher strength, the noise will be more visible
        public float noiseStrength;

        // blend amount of the noise
        [Range(0, 1)]
        public float blendAmount;

        [System.Serializable]
        public class Biome
        {
            
            public Gradient gradient;
            public Color tint;
            [Range(0, 1)]
            public float startHeight;
            [Range(0, 1)]
            public float tintPercent;
        }
    }
}