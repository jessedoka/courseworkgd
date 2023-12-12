using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidNoiseFilter : INoiseFilter
{

    NoiseSettings.RidgidNoiseSettings settings;
    Noise noise = new Noise();

    public RidgidNoiseFilter(NoiseSettings.RidgidNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        // simplify the math

        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for(int i = 0; i < settings.numLayers; i++)
        {
            // calculate the noise value
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.centre));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplier);

            // add the noise value to the noise value
            noiseValue += v * amplitude;

            // increase the frequency and decrease the amplitude
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        // makes sure the noise value is always positive
        noiseValue = noiseValue - settings.minValue;
        return noiseValue * settings.strength;
    }
}