using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RectTransform는 필수로 존재한다. 
[RequireComponent(typeof(RectTransform))]
public class ViewController : MonoBehaviour {
    private RectTransform cachedRectTransform;

    public RectTransform CachedRectTransform
    {
        get
        {
            if(cachedRectTransform == null)
            { cachedRectTransform = GetComponent<RectTransform>();  }
            return cachedRectTransform;
        }
    }

    public virtual string Title { get { return ""; }  set { } }
}
