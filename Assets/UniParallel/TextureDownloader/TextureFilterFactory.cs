using System;

public static class TextureFilterFactory
{
    public static ITextureFilter CreateRedFilter()
    {
        return new RedFilter();
    }

    public static ITextureFilter CreateBlueFilter()
    {
        return new BlueFilter();
    }

    public static ITextureFilter CreateGreenFilter()
    {
        return new GreenFilter();
    }

    public static ITextureFilter CreateEdgeFilter()
    {
        return new EdgeFilter();
    }

    public static ITextureFilter CreatePassThroughFilter()
    {
        return new PassThroughFilter();
    }

}

