using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{

    // type of filter
    public enum FilterType { Simple, Ridgid };
    public FilterType filterType;

    // options

    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RidgidNoiseSettings ridgidNoiseSettings;

    // for simple noise 
    [System.Serializable]
    public class SimpleNoiseSettings
    {
        // strength of the noise
        public float strength = 1;

        // number of layers of noise
        [Range(1, 8)]
        public int numLayers = 1;

        public float baseRoughness = 1;

        // determines how much the frequency of each octave differs from the one below it
        public float roughness = 2;

        // determines how quickly the amplitudes diminish for successive octaves
        public float persistence = .5f;

        // determines the offset of each octave
        public Vector3 centre;

        // this looks height of the noise as a whole
        public float minValue;
    }

    [System.Serializable]
    public class RidgidNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultiplier = .8f;
    }



}