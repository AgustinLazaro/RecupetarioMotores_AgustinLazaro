using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Pool del Jugador")]
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private int playerPoolSize = 20;
    private Queue<GameObject> playerBulletPool = new Queue<GameObject>();

    [Header("Pool del Enemigo")]
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private int enemyPoolSize = 20;
    private Queue<GameObject> enemyBulletPool = new Queue<GameObject>();

    #region ciclos de vida
    private void Awake()
    {
        InitializeSingleton();
        InitializeAllPools();
    }
    #endregion

    #region initialize
    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAllPools()
    {
        InitializePool(playerBulletPool, playerBulletPrefab, playerPoolSize);
        InitializePool(enemyBulletPool, enemyBulletPrefab, enemyPoolSize);
    }

    private void InitializePool(Queue<GameObject> pool, GameObject prefab, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    #endregion

    #region obtener balas
    public GameObject GetPlayerBullet()
    {
        return GetBullet(playerBulletPool, playerBulletPrefab);
    }

    public GameObject GetEnemyBullet()
    {
        return GetBullet(enemyBulletPool, enemyBulletPrefab);
    }

    private GameObject GetBullet(Queue<GameObject> pool, GameObject prefab)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return Instantiate(prefab, transform);
    }
    #endregion

    #region devolver balas
    public void ReturnBullet(GameObject obj, bool isPlayerBullet)
    {
        obj.SetActive(false);
        if (isPlayerBullet)
        {
            playerBulletPool.Enqueue(obj);
        }
        else
        {
            enemyBulletPool.Enqueue(obj);
        }
    }
    #endregion
}