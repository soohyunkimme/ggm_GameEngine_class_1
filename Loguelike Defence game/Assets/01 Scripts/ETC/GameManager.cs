using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("플레이어")]
    public float hpLevel;
    public float maxManaLevel;
    public float manaRengen;

    public float damageLevel;

    public float costDown;
    public float moveSpeed;

    public float skill_1Level = 1;
    public float skill_2Level = 1;
    public float skill_3Level = 1;

    public int gold = 0;

    [Header("적")]
    public List<GameObject> enemies = new List<GameObject>();
    public int enemylevel = 1;

    [Header("화면 전환")]
    public Image fade;
    public CanvasGroup store;
    public CanvasGroup gameUI;
    public CanvasGroup gameMenu;
    public CanvasGroup gameClear;

    public GameObject player;

    private bool ischange = false;

    private float time;
    private Color color;

    private int scene;

    private StoreScript storeScript;

    public Text goldText;

    private void Awake()
    {
        instance = this;
    }

    //화면 전환
    public void SceneChange(int _scene)
    {
        if (!ischange)
        {
            ischange = true;
            scene = _scene;
            StartCoroutine(FadeOut());
        }

    }

    IEnumerator FadeOut()
    {
        color = fade.color;
        while (fade.color.a < 0.9f)
        {
            color.a = Mathf.Lerp(color.a, 1, Time.deltaTime / 0.5f);
            fade.color = color;

            yield return null;
        }

        SetPanel();
    }

    private void SetPanel()
    {
        gameMenu.alpha = 0f;
        gameMenu.interactable = false;
        gameMenu.blocksRaycasts = false;
        gameClear.alpha = 0f;
        gameClear.interactable = false;
        gameClear.blocksRaycasts = false;

        switch (scene)
        {
            case 1:
                SetStore();
                break;
            case 2:
                SetGameStart();
                break;
            default:
                Debug.Log("오류");
                break;
        }

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        color = fade.color;
        while (fade.color.a > 0.01f)
        {
            color.a = Mathf.Lerp(color.a, 0, Time.deltaTime / 0.5f);
            fade.color = color;

            yield return null;
        }
        ischange = false;
    }

    private void SetStore()
    {
        SpawnEnemy spawn = GetComponent<SpawnEnemy>();
        spawn.StopSpawn();


        player.SetActive(false);

        foreach (var item in enemies)//리슽트 청소
        {
            Destroy(item);
        }
        enemies.Clear();

        store.alpha = 1f;
        store.interactable = true;
        store.blocksRaycasts = true;

        storeScript = GetComponent<StoreScript>();
        storeScript.SetCost();
        storeScript.SetText();
    }

    private void SetGameStart()
    {
        SpawnEnemy spawn = GetComponent<SpawnEnemy>();
        spawn.SetSpawn();


        gameUI.alpha = 1f;
        gameUI.blocksRaycasts = true;

        store.alpha = 0f;
        store.interactable = false;
        store.blocksRaycasts = false;

        player.SetActive(true);
    }

    public void ExitGameMenu()
    {
        SceneChange(1);
    }

    public void SetGold()
    {
        goldText.text = $"{gold} G";
    }

    public void GameClear()
    {
        gameUI.alpha = 0f;
        gameUI.blocksRaycasts = false;

        gameClear.alpha = 1f;
        gameClear.interactable = true;
        gameClear.blocksRaycasts = true;

        enemylevel++;

        SpawnEnemy spawn = GetComponent<SpawnEnemy>();
        spawn.StopSpawn();

        foreach (var item in enemies) 
        {
            Destroy(item);
        }
        enemies.Clear();
    }
}