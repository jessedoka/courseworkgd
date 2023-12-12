using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{
    // choice of noise settings
    NoiseSettings.SimpleNoiseSettings settings;
    Noise noise = new Noise();


    public SimpleNoiseFilter(NoiseSettings.SimpleNoiseSettings settings)
    {
        // set the settings
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        // loop through the layers

        for (int i = 0; i < settings.numLayers; i++)
        {

            // calculate the noise value
            float v = noise.Evaluate(point * frequency + settings.centre);

            // add the noise value to the noise value
            noiseValue += (v + 1) * .5f * amplitude;

            // increase the frequency and decrease the amplitude
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        // makes sure the noise value is always positive
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);

        
        return noiseValue * settings.strength;
    }
}