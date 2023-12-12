using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    // radius of the planet
    public float planetRadius = 1;

    // number of layers, the more layers the more detailed the planet will be
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;

        // use the first layer as a mask
        public bool useFirstLayerAsMask;

        // settings for the noise
        public NoiseSettings noiseSettings;
    }
}