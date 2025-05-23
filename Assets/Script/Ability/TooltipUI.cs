using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    public GameObject panel;
    public Text descriptionText;

    private bool isShowing = false;
    private Vector3 offset = new Vector3(100, 0);

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }
    void Update()
    {
     
        if (isShowing)
        {
            Vector3 mousePos = Input.mousePosition;
            panel.transform.position = mousePos + offset;
        }
    }

  
    public void ShowTooltip(string description, RectTransform targetRect)
    {
        descriptionText.text = description;

      
        Vector3 basePos = targetRect.position;
        Vector2 size = targetRect.rect.size;
        Vector3 offset = new Vector3(size.x * 0.5f, -size.y * 0.4f, 0f);  

        panel.transform.position = basePos + offset;
        panel.SetActive(true);
    }

    public void HideTooltip()
    {
        panel.SetActive(false);
    }
}