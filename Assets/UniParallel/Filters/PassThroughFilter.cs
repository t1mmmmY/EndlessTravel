using System;
using UnityEngine;

public class PassThroughFilter: ITextureFilter
{
    public UnityEngine.Color[] ApplyFilter(UnityEngine.Color[] sourceColors, int width, int height)
    {
        return sourceColors;
    }
}

