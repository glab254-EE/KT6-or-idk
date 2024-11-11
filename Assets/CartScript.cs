using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header(" Set-up, primary. ")]
    [SerializeField] private TMP_Text sumtext;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject reference;
    [SerializeField] private Button mimimibutton;
    [SerializeField] private Button refreshbutton;
    [Header(" Other Set-Up, secondary. ")]
    [SerializeField] private float minpricechange = 2;
    [SerializeField] private float maxpricechange = 2;
    [field:Space(5)]
    [field:Header(" Cart Itself. ")]
    [SerializeField] private CartItem[] costs;
    [SerializeField] private List<CartItem> CurrentCart;
    private void ClearGUI()
    {
        foreach (Transform t in parent)
        {
            GameObject.Destroy(t.gameObject);
        }
    }
    private void PrintList()
    {
        double sum = 0;
        foreach (CartItem item in CurrentCart)
        {
            GameObject newa = Instantiate(reference,parent);
            if (newa != null)
            {
                if (newa.transform.Find("Name") != null)
                {
                    newa.transform.Find("Name").GetComponent<TMP_Text>().text = item.Name;
                    if (newa.transform.Find("Cost") != null)
                    {
                        newa.transform.Find("Cost").GetComponent<TMP_Text>().text = item.Cost.ToString()+"$";
                        sum += item.Cost;
                    }
                }
                else DestroyImmediate(newa);
            }
        }
        sumtext.text = $"{sum}$";
    }
    private void Regenlist()
    {
        CurrentCart.Clear();
        foreach (CartItem item in costs)
        {
            int randomnum = UnityEngine.Random.Range(-1, 4);
            if (randomnum > 0)
            {
                for (int i = 0; i < randomnum; i++)
                {
                    CurrentCart.Add(item);
                }
            }
        }
    }
    private void Changeprices()
    {
        foreach (CartItem item in costs)
        {
            double resultedchange = UnityEngine.Random.Range(minpricechange,maxpricechange);
            item.Cost=item.Cost*resultedchange;
            foreach (CartItem item1 in CurrentCart)
            {
                if (item1.Name == item.Name && item1.Cost != item.Cost)
                {
                    item1.Cost = item.Cost;
                }
            } 
        }
    }
    private void SleepButtonOnClick()
    {
        Changeprices();
        ClearGUI();
        PrintList();
    }
    private void RegenButtonOnClick()
    {
        Regenlist();
        ClearGUI();
        PrintList();
    }
    void Start()
    {
        Regenlist();
        PrintList();
        refreshbutton.onClick.AddListener(RegenButtonOnClick);
        mimimibutton.onClick.AddListener(SleepButtonOnClick);
    }
}
[Serializable]
public class CartItem
{
    [SerializeField] public string Name { get; private set; }
    [SerializeField] public double Cost;
}