using System;
using UnityEngine;
using UniParallelGeneric;
using System.Threading;
using System.Net;

public class TextureDownloader
{
    private Texture2D mFilteredTexture;
    private byte[] mTextureData;
    private Color[] mTextureColors;
    private bool mFiltered;
    private int mWidth;
    private int mHeight;

    private readonly ITextureFilter mTextureFilter;

    /// <summary>
    /// Initializes the background download of the texture in textureUrl
    /// It will also apply the filter after the download is complete.
    /// By using PTaskScheduler, all textures are downloaded and filtered in parallel
    /// </summary>
    /// <param name="textureFilter">Texture filter.</param>
    /// <param name="textureUrl">Texture URL.</param>
    public TextureDownloader(ITextureFilter textureFilter, string textureUrl)
    {
        mTextureFilter = textureFilter;
        mTextureData = null;
        mFilteredTexture = null;
        mTextureColors = null;
        mFiltered = false;
        StartTextureProcessing(textureUrl);

    }
        
    /// <summary>
    /// Gets the downloaded texture after applying the selected filter
    /// Returns null if the texture hasn't been downloaded or filtered yet
    /// </summary>
    /// <returns>The filtered texture.</returns>
    public Texture2D GetFilteredTexture()
    {
        if (mTextureData != null && mFilteredTexture==null)
        {         
            GetColorsFromDownloadedTexture();            
        }
                
        if (mFiltered)
        {
            mFilteredTexture.SetPixels(mTextureColors);
            mFilteredTexture.Apply();
            mFiltered = false;
        }
        return mFilteredTexture;
    }

    private void GetColorsFromDownloadedTexture()
    {
        mFilteredTexture = new Texture2D(4, 4);
        mFilteredTexture.LoadImage(mTextureData);
        mTextureData = null;
        mTextureColors = mFilteredTexture.GetPixels();
        mWidth = mFilteredTexture.width;
        mHeight = mFilteredTexture.height;
        Debug.Log("Texture loaded");


        PTaskScheduler taskScheduler = TaskSchedulerHelper.GetTaskScheduler();
        IUniTask task = new ImmediateTask(FilterTexture);
        taskScheduler.Enqueue(task);
    }


    private void StartTextureProcessing(string textureUrl)
    {
        PTaskScheduler taskScheduler = TaskSchedulerHelper.GetTaskScheduler();
        IUniTask task = new ImmediateTask( 
            ()=>DownloadAndFilter(textureUrl)
        );
        taskScheduler.Enqueue(task);
    }

    private void DownloadAndFilter(string textureUrl)
    {
        WebClient client = new WebClient ();
        mTextureData = client.DownloadData(textureUrl);
        Debug.Log("Texture " + textureUrl + " downloaded, bytes="+mTextureData.Length);       
    }

    private void FilterTexture()
    {
        if (mTextureFilter != null)
        {
            mTextureColors = mTextureFilter.ApplyFilter(mTextureColors, mWidth,  mHeight);
            Debug.Log("Filter applied, texture width "+mWidth+" texture height "+mHeight);
            mFiltered = true;
        }
    }
}


