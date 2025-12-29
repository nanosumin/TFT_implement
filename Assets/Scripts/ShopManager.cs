using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Interest Settings")]
    public int maxInterest = 5;


    [Header("Economy")]
    public int gold = 20;
    public Text goldText; // 골드 표시 UI

    [Header("Shop UI")]
    public Transform shopContainer; // 인스펙터에 넣으려면
    public GameObject shopSlotPrefab; // 상점 칸 프리팹

    private List<string> unitPool = new List<string> { "warrior_01", "archer_01" };





    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        UpdateGoldUI();
        RefreshShop();
    }

    // 상점 리롤 (새 유닛 뽑기)
    public void RefreshShop()
    {
        // 기존 상점 아이템 제거
        foreach (Transform child in shopContainer)
        {
            Destroy(child.gameObject);
        }

        // 5개의 무작위 유닛 생성
        for (int i = 0; i < 5; i++)
        {
            string randomId = unitPool[Random.Range(0, unitPool.Count)];
            CreateShopSlot(randomId);
        }
    }

    void CreateShopSlot(string unitId)
    {
        GameObject slot = Instantiate(shopSlotPrefab, shopContainer);

        // 버튼에 구매 기능 연결 (임시로 텍스트만 설정)
        slot.GetComponentInChildren<Text>().text = unitId;
        slot.GetComponent<Button>().onClick.AddListener(() => BuyUnit(unitId, slot));
    }

    public void BuyUnit(string unitId, GameObject slot)
    {
        // 일단은 1원 고정 => 유닛 데이터에서 불러와야함
        int cost = 1;

        if (gold >= cost)
        {
            gold -= cost;
            UpdateGoldUI();

            // 유닛을 대기석(0, 0, 0 근처)에 생성
            UnitSpawner.Instance.SpawnUnit(unitId, new Vector3(Random.Range(-2, 2), 0.5f, -2f));
            // 대기석은 추가로 구현할 예정
            // 구매한 상점 칸 제거
            Destroy(slot);
            // 구매한 상점 칸만 제거하는 기능은 추후 구현 예정
        }
    }

    public void CalculateInterest()
    {
        //라운드가 끝날 때 돈이 들어와야함
        int baseMoney = 5;

        int interestEarned = gold / 10;
        if (interestEarned > maxInterest) interestEarned = maxInterest;


        gold += (baseMoney + interestEarned);
        UpdateGoldUI();
        
        }

        void UpdateGoldUI()
        {
            if (goldText != null) goldText.text = "Gold: " + gold;
        }
    }
