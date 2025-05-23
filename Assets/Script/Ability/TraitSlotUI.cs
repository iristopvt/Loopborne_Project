using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TraitSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI nameText;
    public Image backgroundImage; 
    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;

    [HideInInspector] public TraitData data;
    private bool isSelected = false;

    public void SetTrait(TraitData _data)
    {
        data = _data;
        nameText.text = data.traitName;
        backgroundImage.color = normalColor; 
    }

    public void OnClick()
    {
        if (!isSelected)
        {
            if (!TraitManager.Instance.CanSelect(data.pointCost))
                return;

            isSelected = true;
            TraitManager.Instance.ModifyPoints(data.pointCost);
            TraitManager.Instance.AddTrait(data); 

            backgroundImage.color = selectedColor;
        }
        else
        {
            isSelected = false;
            TraitManager.Instance.ModifyPoints(-data.pointCost);
            TraitManager.Instance.RemoveTrait(data); 
            backgroundImage.color = normalColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
   
        TooltipUI.Instance.ShowTooltip(data.description, GetComponent<RectTransform>());

    }

    public void OnPointerExit(PointerEventData eventData)
    {
      

        TooltipUI.Instance.HideTooltip();

    }



}