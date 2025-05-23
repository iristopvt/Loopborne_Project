# Loopborne_Project

📅 프로젝트 정보  
기간: 2025년 5월 2일 ~ 2025년 5월 21일  
인원: 총 1명   

📌 프로젝트 개요  
본 프로젝트는 Unity 엔진을 활용하여 3주간 1인 개발로 제작한 3D 액션 RPG 게임입니다.  
게임 기획부터 시스템 구현, UI 구성까지 전반적인 개발 과정을 직접 경험하며 Unity 및 C#을 집중적으로 학습하였습니다.  
GitHub를 통한 버전 관리를 병행하며, 다양한 기능을 직접 구현하고 구조화하는 데 초점을 맞추었습니다.  



🎮 초기 기획
- 게임 시작 시 특성/디버프 선택 (포인트 시스템)
- 선택 결과에 따른 스탯/스킬 변화
- 드래그 앤 드롭 인벤토리와 장비 장착
- 스킬과 레벨업 시스템
- 마을-필드-보스 구조의 작은 오픈 월드


🔍 클래스 구조도  
Game Core  
├── CharacterStats : 스탯 기반 클래스  
│ ├── PlayerStatManager : 플레이어 스탯 관리  
│ └── MonsterStat : 몬스터 스탯  
├── PlayerController : 플레이어 입력 및 행동 제어  
│ └── PlayerAttackHitBox : 공격 판정 처리  
├── MonsterAIBase : 몬스터 AI 기본 클래스  
│ ├── SkeletonAI : 스켈레톤 몬스터 AI  
│ └── SlimeAI : 슬라임 몬스터 AI  
└── MonsterHpUI : 몬스터 체력 UI  
  
System  
├── Inventory : 인벤토리 시스템  
│ ├── Slot : 인벤토리 슬롯  
│ └── DragSlot : 드래그 슬롯  
├── EquipmentManager : 장비 관리 시스템  
│ └── EquipSlot : 장비 슬롯  
├── Item : 아이템 정보 (ScriptableObject)  
│ ├── ItemPickUp : 아이템 획득 처리  
│ └── DropItemData : 드랍 데이터  
└── ShockwaveSkill : 스킬 시스템  
  
UI  
├── StatUIManager : 스탯 UI 관리  
│ └── StatButtonEffect : 버튼 효과  
├── TooltipUI : 툴팁 UI  
└── TraitManager : 어빌리트 시스템  
  ├── TraitData : 어빌리티 데이터 (ScriptableObject)  
  └── TraitSlotUI : 어빌리티 슬롯 UI  
  
Camera  
└── FollowCamera : 추적 카메라  


 🧩 주요 기능
- 특성/디버프 선택을 통한 전략적 커스터마이징
- 몬스터 처치 및 경험치에 따른 레벨업 시스템
- 마우스 드래그 기반 인벤토리 UI

📌  이 코드는 ScriptableObject 기반 아이템 정의로, 
장비 및 소모품 데이터를 분리하고 다양한 아이템 타입을 구조화하여 인벤토리 시스템에서 활용됩니다.

   
```csharp
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;            
    [TextArea]
    public string itemDesc;           
    public Sprite itemImage;           
    public GameObject itemPrefab;     
    public ItemType itemType;           
    public EquipmentType equipmentType; 


    public int damage;
    public float attackSpeed;


    public ConsumableType consumableType;
    public int amount;                  

    public enum ItemType
    {
        Equipment,
        Consumable,
        Ingredient,
        ETC
    }

    public enum EquipmentType
    {
        None,
        Weapon,
        Armor,
        Helmet,
        Pants
    }

    public enum ConsumableType
    {
        None,
        HealthPotion,
        ManaPotion
        
    }
}
```

🛠️ 오류 상황 및 해결 방안

❗ 오류 상황: GitHub 협업 중 충돌 발생

프로젝트 초기에 GitHub Desktop을 통해 협업을 진행하던 중, 팀원 간 소통 부족으로 동시에 main 브랜치에 merge를 시도
이로 인해 코드 충돌이 발생, 일부 파일이 꼬이고 게임 실행에 오류가 발생
✅ 해결 방안: 역할 분담 + Git 사용 방식 개선

주간 회의를 통해 각자의 진행 상황을 공유
역할 분담 : 역할별로 브런치를 만들어 작업
pull 전 습관화: push 전에 항상 pull 받고, 충돌 여부를 확인
Develop 브랜치에서 병합 및 버그 테스트 후 Main 브랜치로 반영
📚 프로젝트를 통해 배운 점
🤝 협업 경험

GitHub 충돌 상황을 통해 브랜치 전략과 소통의 중요성 체감
feature 브랜치, 주간 회의, 역할 분담 등을 통한 협업 능력 향상
충돌 해결과 PR 경험을 통해 실전 Git 협업 방식 학습
⚙️ 기술 역량 강화

언리얼 엔진 기반 C++ 클래스 구조 설계 및 컴포넌트 활용
Behavior Tree를 활용한 NPC AI 상태 머신 구현
UI 시스템과 델리게이트 연동을 통해 이벤트 기반 처리 구현
💡 종합적 성장

제한된 기간 안에서 기획 → 구현 → 문제 해결을 경험
시스템 구조와 역할 분배의 중요성을 실무적으로 학습
