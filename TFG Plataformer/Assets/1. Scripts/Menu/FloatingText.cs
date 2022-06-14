using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float destroyTime = 2f;
    public string text;

    private RectTransform floatingTextPosition;

    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = text;
        floatingTextPosition = GetComponent<RectTransform>();
    }

    void Update()
    {
        floatingTextPosition.anchoredPosition = new Vector2(floatingTextPosition.anchoredPosition.x, floatingTextPosition.anchoredPosition.y + .25f);
        if(floatingTextPosition.anchoredPosition.y >= -200)
        {
            Destroy(gameObject);
        }
    }
}
