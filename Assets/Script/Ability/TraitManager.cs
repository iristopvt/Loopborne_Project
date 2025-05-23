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
    public List<TraitData> traitList;    
    public List<TraitData> debuffList;    

    [Header("UI 프리팹")]
    public GameObject traitSlotPrefab;

    [Header("부모 컨테이너")]
    public Transform traitParent;
    public Transform debuffParent;

    [Header("포인트 설정")]
    public int maxPoints = 5;
    private int currentPoints;

    [SerializeField] private TMP_Text pointText;


    public List<TraitData> selectedTraits = new List<TraitData>();
    public List<TraitData> selectedDebuffs = new List<TraitData>();

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
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

  
    public void OnClickStart()
    {
        if (selectedTraits.Count == 0 && selectedDebuffs.Count == 0)
        {
            Debug.LogWarning("선택된 특성이나 디버프가 없습니다.");
            return;
        }

        SceneManager.LoadScene("SampleScene");  
    }
   
}
