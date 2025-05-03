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

        // 버튼 클릭 이벤트 등록
        targetButton.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        StartCoroutine(ClickEffect());
    }

    IEnumerator ClickEffect()
    {
        // 작게 만들기
        targetButton.transform.localScale = originalScale * scaleDownSize;

        yield return new WaitForSeconds(effectDuration);

        // 원래 크기로 복원
        targetButton.transform.localScale = originalScale;
    }
}
