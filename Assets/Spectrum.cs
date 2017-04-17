using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;

public class Spectrum : MonoBehaviour {

    private AudioSource audioSource;
    private UILineRenderer uiLine;
    public int amplitude;
    private int index;
    private float x;
    private float interval;
    public Vector3[] myPoints;
    public RectTransform audioRect;
    public CropHandle leftHandle;

    void Awake()
    {
        uiLine = GetComponent<UILineRenderer>();
        audioSource = GetComponent<AudioSource>();
        float[] spectrum = new float[ audioSource.clip.samples * audioSource.clip.channels ];
        audioSource.clip.GetData( spectrum, 0 );

        myPoints = new Vector3[ 1001 ];
        uiLine.m_points = new Vector2[ 1001 ];
        int scale = spectrum.Length / 1001;

        interval = transform.parent.GetComponent<RectTransform>().sizeDelta.x / 1000;

        for ( int i = 0; i < spectrum.Length; i = i + scale )
        {
            try
            {
                myPoints[ index ] = new Vector3( x, spectrum[ i ] * amplitude, i );
                uiLine.m_points[ index ] = new Vector2( x, spectrum[ i ] * amplitude );
                index++;
                x = x + interval;
            }
            catch
            {
                Debug.Log( "Index out of range: " + index );
            }
            
        }        
        uiLine.SetAllDirty();
    }           

    void CreateCrop()
    {        
        float width = transform.parent.GetComponent<RectTransform>().sizeDelta.x;
        float diff = leftHandle.rectTrans.anchoredPosition.x - leftHandle.initialPos.x;
        int clipSamples = audioSource.clip.samples * audioSource.clip.channels;
        int samplesCropOffset = ( int )( clipSamples * diff / width );

        float[] samples = new float[ clipSamples ];

        audioSource.clip.GetData( samples, 0 );
        audioSource.clip.SetData( samples, samplesCropOffset );

    }

    void OnGUI()
    {
        if ( GUI.Button( new Rect( 0, 0, 100, 50 ), "Play" ) )
        {
            CreateCrop();
            GetComponent<AudioSource>().Play();

        }
    }
}
