using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageControl : MonoBehaviour {

    [SerializeField] private Toggle indicatorBase;
    private List<Toggle> indicators = new List<Toggle>();

    private void Awake()
    {
        indicatorBase.gameObject.SetActive(false);
    }

    public void SetNumberOfPages(int number)
    {
        if(indicators.Count < number)
        {
            for (int i = indicators.Count; i < number; i++)
            {
                Toggle indicator = Instantiate(indicatorBase) as Toggle;
                indicator.gameObject.SetActive(true);
                indicator.transform.SetParent(indicatorBase.transform.parent);
                indicator.transform.localScale = indicatorBase.transform.localScale;
                indicator.isOn = false;
                indicators.Add(indicator);
            }
        }
        else if(indicators.Count > number)
        {
            for(int i = indicators.Count - 1; i>=number; i--)
            {
                Destroy(indicators[i].gameObject);
                indicators.RemoveAt(i);
            }
        }
    }

    //현재 페이지를 설정하는 메서드 
    public void SetCurrentPage(int index)
    {
        if(index >= 0 && index <= indicators.Count - 1)
        {
            indicators[index].isOn = true;
        }
    }
}
