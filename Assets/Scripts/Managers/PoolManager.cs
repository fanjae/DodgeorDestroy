using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Key값에 Type은 특수한 타입

    /*
     * Dictionary<Type, Queue<Component>>
     Type               Queue
    Bullet              Bullet 대기열
    Enemy               Enemy 대기열
    EnemyBullet         EnemyBullet 대기열
    -----------------------------------------
    public class Bullet : MonoBehaviour
    public class Enemy : MonoBehaviour
    typeof()

    Type bulletType = typeof(Bullet) - > 출력을 하면 -> bullet
    Type Enemy = typeof(Enemy) -> 출력을 하면 -> enemy

    Queue<Bullet> bulletPool;
    Queue<Enemy> enemyPool;

    */

    // Pool Manager는 클래스의 타입만 보고 구분을 한다.
    // 같은 Bullet도 프리팹이 2개면 문제가 생길 수 있다.
    // 이 경우, int를 사용해도 된다. -> 대신, int를 사용하는 경우 instance ID 값이 있다.
    // 인스턴스 ID는 고유한 값으로 되어 있다.

    // Bullet bullet = Managers.Pool.GetPool(bulletprefab);
    // GameObject obj = Managers.Pool.GetPool(bulletprefab);
    // Bullet bullet = obj.GetComponent<Bullet>



    // 타입별 부모 오브젝트용
    private Dictionary<int, Queue<Component>> poolDictionary = new Dictionary<int, Queue<Component>>();
    private Dictionary<int, Transform> poolParents = new Dictionary<int, Transform>();

    private Dictionary<Component, int> objectToPoolKey = new Dictionary<Component, int>();

    // 모든 풀링 오브젝트를 정리할 최상위 부모
    private Transform poolRoot;

    private void Awake()
    {
        CreatePoolRoot();
    }
    private void CreatePoolRoot()
    {
        // PoolRoot 라는 빈 게임 오브젝트 생성
        GameObject rootObj = new GameObject("PoolRoot");

        // PoolRoot를 PoolManager 오브젝트의 자식으로 설정
        rootObj.transform.SetParent(transform);

        // 생성한 poolRoot의 트랜스폼을 저장
        poolRoot = rootObj.transform;
    }

    // 오브젝트를 미리 생성해 두는 것
    public void PreloadPool<T>(T prefab, int count) where T : Component
    {
        // 현재 프리팹의 타입을 가져온다.
        // 예를 들어 Bullet 프리팹이면 typeof(T)는 Bullet

        int key = prefab.GetInstanceID();

        CreatePool(key, prefab.name);

        for(int i = 0; i < count; i++)
        {
            // 프리팹을 이용해서 생성하자.
            T obj = Instantiate(prefab);

            // 아직 사용하지 않을거니까 끄자.
            obj.gameObject.SetActive(false);

            // 하이어라키 정리를 위해 부모 밑으로 가자
            obj.transform.SetParent(poolParents[key]);

            objectToPoolKey[obj] = key;

            // 생성한 오브젝트를 해당 타입의 큐에 저장하자.
            poolDictionary[key].Enqueue(obj);
        }
    }
    // 풀에서 오브젝트를 꺼내는 녀석
    public T GetPool<T>(T prefab) where T : Component
    {
        int key = prefab.GetInstanceID();

        CreatePool(key, prefab.name);

        T obj = null;

        if (poolDictionary[key].Count > 0)
        {
            obj = poolDictionary[key].Dequeue() as T;
        }
        else
        {
            obj = Instantiate(prefab);
            obj.transform.SetParent(poolParents[key]);

            objectToPoolKey[obj] = key;
        }

        obj.gameObject.SetActive(true);

        return obj;
    }
    // 사용이 끝난 오브젝트를 다시 풀에 넣는 녀석
    public void ReturnPool<T>(T obj) where T : Component
    {
        if (!objectToPoolKey.TryGetValue(obj, out int key)) // PoolManager에서 생성된 오브젝트가 아닌 경우
        {
            Destroy(obj.gameObject);
            return;
        }

        // 오브젝트를 비활성화 한다.
        obj.gameObject.SetActive(false);

        // 타입별 부모 밑으로 이동한다.
        if (obj.transform.parent != poolParents[key])
        {
            obj.transform.SetParent(poolParents[key]);
        }
        // Queue에 다시 저장한다.
        poolDictionary[key].Enqueue(obj);
    }

    // 해당 타입의 풀이 이미 있으면 아무것도 하지 말고 없으면
    // 큐와 부모 오브젝트를 만들자.
    private void CreatePool(int key, string prefabName)
    {
        if (poolDictionary.ContainsKey(key))
        {
            return;
        }

        poolDictionary.Add(key, new Queue<Component>());
        CreatePoolParent(key, prefabName);
    }

    // 타입별 부모 오브젝트 생성 
    private void CreatePoolParent(int key, string prefabName)
    {
        GameObject parentObj = new GameObject($"{prefabName}_{key}");

        parentObj.transform.SetParent(poolRoot);

        poolParents.Add(key, parentObj.transform);
    }
}
