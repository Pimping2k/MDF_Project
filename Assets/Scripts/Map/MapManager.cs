using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor.Searcher;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public GameObject canvas;
    public Transform mapArea;
    public GameObject nodePrefab;
    public int nodeCount = 10;
    public float mapWidth = 10f;
    public float mapHeight = 10f;

    public List<Transform> cotainersMapNodes = new List<Transform>();

    public Transform playerFigure;
    public List<MapNode> mapNodes = new List<MapNode>();
    public MapNode currentNode;

    private void Start()
    {
        GenerateMap();
        InitializeMap();

        foreach (var mapNode in mapNodes)
        {
            mapNode.OnMapNodeSelected += MoveToNode;
        }
    }

    private void GenerateMap()
    {
        for (int i = 0; i < nodeCount; i++)
        {
            GameObject nodeObject = Instantiate(nodePrefab, cotainersMapNodes[i].transform);
            MapNode node = nodeObject.GetComponent<MapNode>();
            mapNodes.Add(node);
        }

        for (int i = 0; i < mapNodes.Count - 1; i++)
        {
            if (i == mapNodes.Count - 1)
            {
                break;
            }

            if (i == 0)
            {
                mapNodes[i].AddConnection(mapNodes[i + 1]);
                mapNodes[i].AddConnection(mapNodes[i + 2]);
                mapNodes[i].AddConnection(mapNodes[i + 3]);
                continue;
            }

            if (i + 1 < mapNodes.Count)
                mapNodes[i].AddConnection(mapNodes[i + 1]);
            if (i + 2 < mapNodes.Count && Random.value > 0.5f)
                mapNodes[i].AddConnection(mapNodes[i + 2]);
            if (i % 2 == 0 && i + 3 < mapNodes.Count)
                mapNodes[i].AddConnection(mapNodes[i + 3]);
        }
    }

    private void UpdateAccessibleNodes()
    {
        foreach (var node in mapNodes)
        {
            node.isAccessible = false;
        }

        foreach (var connectedMapNode in currentNode.ConnectedMapNodes)
        {
            connectedMapNode.isAccessible = true;
        }
    }

    private void InitializeMap()
    {
        currentNode = mapNodes[0];
        playerFigure.transform.position = currentNode.transform.position;
        currentNode.isAccessible = false;

        UpdateAccessibleNodes();
    }

    private void MoveToNode(MapNode targetNode)
    {
        playerFigure.DOMove(targetNode.transform.position, 2f).OnComplete((() =>
        {
            SceneManager.LoadScene(targetNode.levelIndex);
        }));
    }
}