

public static class NoiseFilterFactory
{

    public static INoiseFilter CreateNoiseFilter(NoiseSettings settings)
    {
        // Switching between the different types of noise filters
        
        switch (settings.filterType)
        {
            case NoiseSettings.FilterType.Simple:
                return new SimpleNoiseFilter(settings.simpleNoiseSettings);
            case NoiseSettings.FilterType.Ridgid:
                return new RidgidNoiseFilter(settings.ridgidNoiseSettings);
        }
        return null;
    }
}