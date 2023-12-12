using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{

    ShapeSettings settings;
    INoiseFilter[] noiseFilters;
    public MinMax elevationMinMax;

    public void UpdateSettings(ShapeSettings settings)
    {
        this.settings = settings;

        // create an array of noise filters
        noiseFilters = new INoiseFilter[settings.noiseLayers.Length];

        // loop through the noise layers
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            // create a noise filter for each noise layer
            noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }

        // create a new elevation min max
        elevationMinMax = new MinMax();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        // edge case for if there are no noise layers

        if (noiseFilters.Length > 0)
        {

            // evaluate the first noise filter
            firstLayerValue = noiseFilters[0].Evaluate(pointOnSphere);
            if (settings.noiseLayers[0].enabled)
            {
                // if the first noise layer is enabled, set the elevation to the first layer value
                elevation = firstLayerValue;
            }
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            // if the noise layer is enabled
            if (settings.noiseLayers[i].enabled)
            {

                // if the noise layer is set to use the first layer as a mask
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;

                // add the noise filter to the elevation
                elevation += noiseFilters[i].Evaluate(pointOnSphere) * mask;
            }
        }

        // r * (1 + elevation)
        elevation = settings.planetRadius * (1 + elevation);

        // add the elevation to the elevation min max
        elevationMinMax.AddValue(elevation);
        return pointOnSphere * elevation;
    }
}
