using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject slotPanel;
    [SerializeField] private GameObject description;
    [SerializeField] private Text desText;

    private GameObject prefab;
    private List<GameObject> slotObjs;

    private void Awake()
    {
        int j = 0;
        slotObjs = new List<GameObject>();
        prefab = Resources.Load("Slot") as GameObject;

        int slotCount = DataManager.instance.BaseRating.Count + DataManager.instance.NecessaryRating.Count;
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(prefab);
            slot.transform.SetParent(slotPanel.transform);
            slotObjs.Add(slot);
        }

        foreach (string name in DataManager.instance.BaseRating)
        {
            slotObjs[j].GetComponent<Image>().preserveAspect = true;
            slotObjs[j].GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
            slotObjs[j].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slotObjs[j].AddComponent<Button>();
            Button btn = slotObjs[j].GetComponent<Button>();
            btn.onClick.AddListener( delegate{ OnClickSlot(name); });
            j++;
        }

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        description.SetActive(false);

        SetSlotImage();
    }

    private void SetSlotImage()
    {
    }

    private void OnClickSlot(string name)
    {
        description.SetActive(true);
        desText.text = name;
    }
}
