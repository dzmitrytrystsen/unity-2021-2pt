using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstacles;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;

    private void Start()
    {
        List<GameObject> randomObstacles = GenerateRandomItemsFromList<GameObject>(_obstacles, Random.Range(4, _obstacles.Count));

        foreach (GameObject obstacle in randomObstacles)
            obstacle.SetActive(true);

        _navMeshSurface.BuildNavMesh();
        _player.SetActive(true);
        _enemy.SetActive(true);
    }

    private IEnumerator WaitAndBuildNavMesh()
    {
        yield return new WaitForSeconds(0.5f);

        _navMeshSurface.BuildNavMesh();
    }

    private List<T> GenerateRandomItemsFromList<T>(List<T> list, int number)
    {
        List<T> tmpList = new List<T>(list);

        List<T> newList = new List<T>();

        while (newList.Count < number && tmpList.Count > 0)
        {
            int index = Random.Range(0, tmpList.Count);
            newList.Add(tmpList[index]);
            tmpList.RemoveAt(index);
        }

        return newList;
    }
}
