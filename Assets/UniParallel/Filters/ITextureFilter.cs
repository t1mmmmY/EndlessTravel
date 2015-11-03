using System;
using UnityEngine;

public interface ITextureFilter
{
    Color[] ApplyFilter(Color[] sourceColors, int width, int height);
}


