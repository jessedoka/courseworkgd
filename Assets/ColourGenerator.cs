using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGenerator
{

    ColourSettings settings;
    Texture2D texture;
    const int textureResolution = 50;
    INoiseFilter biomeNoiseFilter;

    public void UpdateSettings(ColourSettings settings)
    {
        // access the settings
        this.settings = settings;


        if (texture == null || texture.height != settings.biomeColourSettings.biomes.Length)
        {
            // Tutorial 7: Texturing 

            // create a new texture with the height of the number of biomes in the settings  
            texture = new Texture2D(textureResolution, settings.biomeColourSettings.biomes.Length);
        }

        // create new noise texture
        biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(settings.biomeColourSettings.noise);
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        // using the shader property, set the elevation min and max, setting it between 0 and 1

        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    // get the biome index from the point on the unit sphere
    public float BiomePercentFromPoint(Vector3 pointOnSphere)
    {
        // get the height percent from the point on the unit sphere

        float heightPercent = (pointOnSphere.y + 1) / 2f;

        // Using Noise Strength and Noise Offset, using the height percent to evaluate the biome noise filter 

        // height += (noise offset - biome noise filter) * noise strength

        // Biome should be dependent on the biome noise not the planet noise

        heightPercent += (biomeNoiseFilter.Evaluate(pointOnSphere) - settings.biomeColourSettings.noiseOffset) * settings.biomeColourSettings.noiseStrength;

        float biomeIndex = 0;

        // number of biomes
        int numBiomes = settings.biomeColourSettings.biomes.Length;

        // blend range is the blend amount divided by 2 plus a small number
        float blendRange = settings.biomeColourSettings.blendAmount / 2f + .001f;

        // for each biome
        for (int i = 0; i < numBiomes; i++)
        {
            // dst is the height percent minus the start height of the biome
            float dst = heightPercent - settings.biomeColourSettings.biomes[i].startHeight;

            // determine where the biome is in the blend range
            float weight = Mathf.InverseLerp(-blendRange, blendRange, dst);

            // if the weight is less than 0, then the biome is not in the blend range
            biomeIndex *= 1 - weight;
            biomeIndex += i * weight;
        }

        // clamping the biome index between 0 and the number of biomes

        return biomeIndex / Mathf.Max(1, numBiomes - 1);
    }

    public void UpdateColours()
    {
        Color[] colours = new Color[texture.width * texture.height];

        int colourIndex = 0;

        foreach (var biome in settings.biomeColourSettings.biomes)
        {
            for (int i = 0; i < textureResolution; i++)
            {
                // get the colour from the gradient
                Color gradientCol = biome.gradient.Evaluate(i / (textureResolution - 1f));

                // get the tint colour from the biome
                Color tintCol = biome.tint;

                // set the colour at the current index of teh colours array to the gradient colour tinted by the biome 

                colours[colourIndex] = gradientCol * (1 - biome.tintPercent) + tintCol * biome.tintPercent;

                colourIndex++;
            }
        }

        texture.SetPixels(colours);

        texture.Apply();

        settings.planetMaterial.SetTexture("_texture", texture);
    }
}