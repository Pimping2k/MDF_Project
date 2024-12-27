using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Collections;
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
            mapNodes[i].AddConnection(mapNodes[i + 1]);
        }

        int additionalConnections = Random.Range(nodeCount / 2, nodeCount);
        for (int i = 0; i < additionalConnections; i++)
        {
            MapNode nodeA = mapNodes[Random.Range(0, mapNodes.Count)];
            MapNode nodeB = mapNodes[Random.Range(0, mapNodes.Count)];

            if (nodeA != nodeB)
            {
                nodeA.AddConnection(nodeB);
            }
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
        currentNode.isAccessible = true;

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