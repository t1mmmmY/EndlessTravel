using System;
using JUnityImage;

public class EdgeFilter:ITextureFilter
{
    public UnityEngine.Color[] ApplyFilter(UnityEngine.Color[] sourceColors, int width, int height)
    {
        ImageFilters filters = new ImageFilters();

		return filters.PrewittFilter(sourceColors, width, height);
    }
}

