using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState { Preparation, Combat, Resolution }

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;
    public GameState currentState;
    
    [Header("Round Settings")]
    public int currentRound = 1;      // 라운드 표시용
    public Text roundText;           // 라운드 UI 연결

    [Header("Timer Settings")]
    public float preparationTime = 15f;
    public float combatTime = 30f;
    public Text timerText;
    public Text statusText;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            UpdateRoundUI(); // 라운드 텍스트 업데이트
            yield return StartCoroutine(PreparationPhase());
            yield return StartCoroutine(CombatPhase());
            yield return StartCoroutine(ResolutionPhase());
            currentRound++; // 라운드 증가
        }
    }

    IEnumerator PreparationPhase()
    {
        currentState = GameState.Preparation;
        UpdateStatusUI("준비 단계 :");

        float timer = preparationTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI(timer);
            yield return null;
        }
    }

    IEnumerator CombatPhase()
    {
        currentState = GameState.Combat;
        UpdateStatusUI("전투 시작 :");

        float timer = combatTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI(timer);
            yield return null;
        }
    }

    IEnumerator ResolutionPhase()
    {
        currentState = GameState.Resolution;
        UpdateStatusUI("라운드 종료");

        //  
        if (ShopManager.Instance != null)
        {
            ShopManager.Instance.gold += 5;
            ShopManager.Instance.RefreshShop(); // 상점 자동 리롤
            // ShopManager 내부의 골드 UI 업데이트 함수를 호출하기 위해 
            // ShopManager에 public 함수를 만들어두는 것이 좋습니다.
        }

        yield return new WaitForSeconds(3f);
    }

    void UpdateTimerUI(float time)
    {
        if (timerText != null) timerText.text = Mathf.CeilToInt(time).ToString();
    }

    void UpdateStatusUI(string status)
    {
        if (statusText != null) statusText.text = status;
    }

    void UpdateRoundUI()
    {
        if (roundText != null) roundText.text = "ROUND " + currentRound;
    }
}