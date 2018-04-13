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
    private GameManager gm;

    private void Awake()
    {
        int j = 0;
        slotObjs = new List<GameObject>();
        prefab = Resources.Load("Slot") as GameObject;
        gm = FindObjectOfType<GameManager>();

        int slotCount = DataManager.instance.BaseRating.Count + DataManager.instance.NecessaryRating.Count;

        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(prefab);
            slot.transform.SetParent(slotPanel.transform);
            slotObjs.Add(slot);
        }

        foreach (string name in DataManager.instance.BaseRating)
        {
            Transform obj = slotObjs[j].transform.Find("Image");
            obj.GetComponent<Image>().preserveAspect = true;
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
            obj.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slotObjs[j].AddComponent<Button>();
            Button btn = slotObjs[j].GetComponent<Button>();
            btn.onClick.AddListener( delegate{ OnClickSlot(name + "(항시)"); });
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
        int count = DataManager.instance.BaseRating.Count;

        for (int i = 0; i< gm.SuccessList.Count; i++)
        {
            Transform obj = slotObjs[i + count].transform.Find("Image");
            string name = gm.SuccessList[i];

            obj.GetComponent<Image>().preserveAspect = true;
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
            obj.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slotObjs[i + count].AddComponent<Button>();
            Button btn = slotObjs[i + count].GetComponent<Button>();
            btn.onClick.AddListener(delegate { OnClickSlot(name + "(필수)"); });
        }
    }

    private void OnClickSlot(string name)
    {
        description.SetActive(true);
        desText.text = name;
    }
}
