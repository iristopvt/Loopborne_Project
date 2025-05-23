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


 💻 코드 예시: 아이템 시스템  


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

🛠️ 기술적 시행착오 및 해결 방안  
  
❗ 충돌 판정 방식 차이 (언리얼 vs 유니티)  
상황:  
언리얼 엔진에서는 플레이어 공격 시 CapsuleCollision을 동적으로 생성하여 방향성과 범위를 갖춘 공격 판정을 구현.  
Unity에서는 자연스러운 타격 타이밍과 애니메이션 연계를 우선시하여, 캐릭터 본에 BoxCollider를 부착하고 공격 애니메이션 타이밍에 맞춰 콜라이더를 활성화/비활성화하는 방식으로 충돌을 처리.  
이 방식은 공격 타이밍을 정밀하게 제어할 수 있고, 충돌 범위 역시 세밀하게 조정 가능하여 실제 전투의 타격감을 효과적으로 전달.  

❗ 몬스터 충돌 애니메이션 타이밍 조정  
상황:  
몬스터가 돌진 후 복귀하는 애니메이션에서, 플레이어와의 충돌 시점을 정확히 일치시키는 데 어려움이 발생.  

구현 방식:  
애니메이션과 충돌 판정의 정밀한 타이밍 제어를 위해, **애니메이션 이벤트(Animation Event)**를 활용하여 돌진 구간에서만 BoxCollider를 활성화하고 복귀 시점에는 비활성화.  
이 방식은 타격 판정의 정확도를 높이고, 실제 전투에서의 피격 반응을 정교하게 조정하는 데 효과적.  



📚 프로젝트를 통해 배운 점  
Unity 엔진의 애니메이션, 충돌 판정, UI 구성 등 게임 개발의 핵심 구조를 직접 설계하고 구현하면서 개발 전반에 대한 이해도를 높일 수 있었음  

언리얼 엔진과의 구조적 차이를 비교하며, 게임 시스템 설계 방식의 다양성과 구현 접근 방식의 유연함을 경험  

특성/디버프 시스템, 인벤토리, 몬스터 AI 등 주요 기능을 직접 구현하며, C# 기반의 구조적 프로그래밍과 문제 해결 능력을 학습  

