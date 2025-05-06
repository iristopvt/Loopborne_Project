using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatButtonEffect : MonoBehaviour
{
    [SerializeField] private Button targetButton;
    [SerializeField] private float scaleDownSize = 0.9f;
    [SerializeField] private float effectDuration = 0.1f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = targetButton.transform.localScale;

        targetButton.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        StartCoroutine(ClickEffect());
    }

    IEnumerator ClickEffect()
    {
       
        targetButton.transform.localScale = originalScale * scaleDownSize;

        yield return new WaitForSeconds(effectDuration);

       
        targetButton.transform.localScale = originalScale;
    }
}
