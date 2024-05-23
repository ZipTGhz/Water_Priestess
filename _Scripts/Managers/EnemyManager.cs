using System.Collections;
using UnityEngine;

public class EnemyManager : CustomMonoBehaviour
{
    public static EnemyManager Instance;

    public GameObject[] enemy;

    [SerializeField]
    private float respawnTime = 5f;

    private bool[] isCountDown;

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
            isCountDown = new bool[enemy.Length];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        for (int i = 0; i < enemy.Length; ++i)
        {
            if (isCountDown[i] == false && enemy[i].activeInHierarchy == false)
                StartCoroutine(DelayReSpawn(i));
        }
    }

    private IEnumerator DelayReSpawn(int index)
    {
        isCountDown[index] = true;
        yield return new WaitForSeconds(respawnTime);
        enemy[index].SetActive(true);
        enemy[index].GetComponent<EnemyController>().ReSpawn();
        isCountDown[index] = false;
    }
}
