using UnityEngine;
using System.Collections;

public class CropHandle : MonoBehaviour {

    public RectTransform shadow;
    public RectTransform otherHolder;
    public bool left;
    public Vector2 initialPos;
    public RectTransform rectTrans;
    public RectTransform audioRect;

    void Awake()
    {
        rectTrans = transform.GetComponent<RectTransform>();
        if ( left )
            rectTrans.anchoredPosition = new Vector2( audioRect.anchoredPosition.x - audioRect.sizeDelta.x / 2, audioRect.anchoredPosition.y );
        else
            rectTrans.anchoredPosition = new Vector2( audioRect.anchoredPosition.x + audioRect.sizeDelta.x / 2, audioRect.anchoredPosition.y );
        initialPos = rectTrans.anchoredPosition;
        shadow.anchoredPosition = initialPos;
    }

    public void Drag()
    {
        rectTrans.anchoredPosition = new Vector2( Input.mousePosition.x, transform.GetComponent<RectTransform>().anchoredPosition.y );
        float diff = 0;
        if ( left )
        {
            rectTrans.anchoredPosition = new Vector2( Mathf.Clamp( rectTrans.anchoredPosition.x, initialPos.x, otherHolder.anchoredPosition.x ), rectTrans.anchoredPosition.y );
            diff = rectTrans.anchoredPosition.x - shadow.anchoredPosition.x;
        }
        else
        {
            rectTrans.anchoredPosition = new Vector2( Mathf.Clamp( rectTrans.anchoredPosition.x, otherHolder.anchoredPosition.x, initialPos.x ), rectTrans.anchoredPosition.y );
            diff = rectTrans.anchoredPosition.x - shadow.anchoredPosition.x;
        }
        shadow.sizeDelta = new Vector2( Mathf.Abs( diff ), shadow.sizeDelta.y );
    }



}
