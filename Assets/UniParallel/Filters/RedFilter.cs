using System;

public class RedFilter: ITextureFilter
{
    public UnityEngine.Color[] ApplyFilter(UnityEngine.Color[] sourceColors, int width, int height)
    {
        for (int i = 0; i < sourceColors.Length; i++)
        {
                sourceColors[i].b = 0;
                sourceColors[i].g = 0;
        }
        return sourceColors;
    }
}
