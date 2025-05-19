using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TraitManager : MonoBehaviour
{
    public static TraitManager Instance;

    [Header("Trait 설정")]
    public List<TraitData> traitList;     // 특성 목록
    public List<TraitData> debuffList;    // 디버프 목록

    [Header("UI 프리팹")]
    public GameObject traitSlotPrefab;

    [Header("부모 컨테이너")]
    public Transform traitParent;
    public Transform debuffParent;

    [Header("포인트 설정")]
    public int maxPoints = 5;
    private int currentPoints;

    [SerializeField] private TMP_Text pointText;

    // 👉 선택된 데이터 저장용
    public List<TraitData> selectedTraits = new List<TraitData>();
    public List<TraitData> selectedDebuffs = new List<TraitData>();

    void Awake()
    {
        // 중복 방지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentPoints = maxPoints;
        UpdatePointUI();

        SpawnSlots(traitList, traitParent);
        SpawnSlots(debuffList, debuffParent);
    }

    void SpawnSlots(List<TraitData> list, Transform parent)
    {
        foreach (var data in list)
        {
            GameObject slotObj = Instantiate(traitSlotPrefab, parent);
            TraitSlotUI slot = slotObj.GetComponent<TraitSlotUI>();
            slot.SetTrait(data);

            // 버튼 클릭 연결
            Button button = slotObj.GetComponent<Button>();
            if (button != null)
                button.onClick.AddListener(slot.OnClick);
        }
    }

    public bool CanSelect(int cost)
    {
        return currentPoints - cost >= 0;
    }

    public void ModifyPoints(int cost)
    {
        currentPoints -= cost;
        UpdatePointUI();
    }

    void UpdatePointUI()
    {
        pointText.text = $"포인트: {currentPoints}";
    }
    public void AddTrait(TraitData data)
    {
        if (traitList.Contains(data))
            selectedTraits.Add(data);
        else if (debuffList.Contains(data))
            selectedDebuffs.Add(data);
    }

    public void RemoveTrait(TraitData data)
    {
        if (traitList.Contains(data))
            selectedTraits.Remove(data);
        else if (debuffList.Contains(data))
            selectedDebuffs.Remove(data);
    }

    // 👉 씬 전환용 함수 (버튼에서 연결)
    public void OnClickStart()
    {
        if (selectedTraits.Count == 0 && selectedDebuffs.Count == 0)
        {
            Debug.LogWarning("선택된 특성이나 디버프가 없습니다.");
            return;
        }

        Debug.Log("게임 시작: 선택 특성 개수 " + selectedTraits.Count + ", 디버프 개수 " + selectedDebuffs.Count);
        Debug.Log("씬 전환 시도 - TraitManager 인스턴스 유지 여부 확인: " + (TraitManager.Instance != null));

        SceneManager.LoadScene("SampleScene");  // 실제 씬 이름으로 교체
    }
   
}
