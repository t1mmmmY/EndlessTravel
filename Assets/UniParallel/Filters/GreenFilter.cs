using System;

public class GreenFilter:ITextureFilter
{
    public UnityEngine.Color[] ApplyFilter(UnityEngine.Color[] sourceColors, int width, int height)
    {
        for (int i = 0; i < sourceColors.Length; i++)
            {
                sourceColors[i].r = 0;
                sourceColors[i].b = 0;
            }
        return sourceColors;
    }
}
