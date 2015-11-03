using UnityEngine;
using System.Collections;

public class RenderToPlane : MonoBehaviour
{
	// go to http://junity3d.blogspot.ca/ for more sample images
	private const string TextureUrl = "http://3.bp.blogspot.com/-OPFt55Cueek/U54MnSCqg9I/AAAAAAAAAAs/iRiLHLPZYmk/s1600/sample_image.png";
	//private const string TextureUrl = "http://3.bp.blogspot.com/-uouJ7Me02Sk/U6JMd14hjCI/AAAAAAAAABM/M28b1P-07pA/s1600/nature1.png";
    private TextureDownloader mTextureDownloader;
    void Start()
    {
        mTextureDownloader = new TextureDownloader(GetFilter(), TextureUrl);
    }
	
    void Update()
    { 
        Texture2D texture = mTextureDownloader.GetFilteredTexture();
        if (texture != null)
        {
            GetComponent<Renderer>().material.mainTexture = texture;
        }                     
    }

     private ITextureFilter GetFilter()
    {
        if (gameObject.name.Equals("RedFiltered"))
        {
            return TextureFilterFactory.CreateRedFilter();
        }

        if (gameObject.name.Equals("GreenFiltered"))
        {
            return TextureFilterFactory.CreateGreenFilter();
        }

        if (gameObject.name.Equals("BlueFiltered"))
        {
            return TextureFilterFactory.CreateBlueFilter();
        }

        if (gameObject.name.Equals("EdgeFiltered"))
        {
            return TextureFilterFactory.CreateEdgeFilter();
        }

        return TextureFilterFactory.CreatePassThroughFilter();
    }
}
