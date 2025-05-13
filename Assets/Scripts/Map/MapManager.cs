using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor.Experimental;
using UnityEditor.Searcher;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float branchingChance = 0.9f;
    public Transform mapArea;
    public GameObject enemyNodePrefab;
    public GameObject eventNodePrefab;
    public GameObject storeNodePrefab;
    public GameObject startNodePrefab;
    public GameObject endNodePrefab;
    public int nodeCount = 10;

    public GameObject MapNodeContainerPrefab;
    public List<GameObject> containersMapNodes = new List<GameObject>();

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
        float startXPos = 200f;
        float startYPos = 300f;
        int rowsCount = 3;
        for (int i = 0; i < nodeCount; i++)
        {
            var nodeContainerInstance = Instantiate(MapNodeContainerPrefab, mapArea.transform);

            if (i == 0)
            {
                nodeContainerInstance.transform.position = new Vector3(startXPos, startYPos);
                containersMapNodes.Add(nodeContainerInstance);
                continue;
            }

            if (i == nodeCount - 1)
            {
                nodeContainerInstance.transform.position = new Vector3(200f + startXPos, 300f);
                containersMapNodes.Add(nodeContainerInstance);
                break;
            }

            int currentRow = i % rowsCount;

            switch (currentRow)
            {
                case 0:
                    nodeContainerInstance.transform.position = new Vector3(
                        startXPos + Random.Range(90f, 120f),
                        startYPos + Random.Range(60f, 100f));
                    break;
                case 1:
                    nodeContainerInstance.transform.position = new Vector3(
                        startXPos + Random.Range(90f, 110f),
                        startYPos + Random.Range(-5f, 10f));
                    break;
                case 2:
                    nodeContainerInstance.transform.position = new Vector3(
                        startXPos + Random.Range(90f, 130f),
                        startYPos + Random.Range(-100f, -70f));
                    break;
            }

            if (currentRow == rowsCount - 1)
            {
                startXPos += Random.Range(100f, 130f);
            }

            containersMapNodes.Add(nodeContainerInstance);
        }

        GameObject[] nodePrefabs = { enemyNodePrefab, storeNodePrefab, eventNodePrefab, eventNodePrefab, eventNodePrefab, eventNodePrefab };
        for (int i = 0; i < nodeCount; i++)
        {
            GameObject nodePrefab;
            
            if (i == 0)
                nodePrefab = startNodePrefab;
            else if (i == nodeCount - 1)
                nodePrefab = endNodePrefab;
            else
            {
                int id = Random.Range(0, nodePrefabs.Length);
                nodePrefab = nodePrefabs[id];
                List<GameObject> nodePrefAbsList = nodePrefabs.ToList();
                nodePrefAbsList.Remove(nodePrefab);
                nodePrefabs = nodePrefAbsList.ToArray();
            }
            
            GameObject nodeObject = Instantiate(nodePrefab, containersMapNodes[i].transform);
            MapNode node = nodeObject.GetComponent<MapNode>();
            mapNodes.Add(node);
        }

        for (int i = 0; i < nodeCount; i++)
        {
            if (i == 0)
            {
                mapNodes[i].AddConnection(mapNodes[i + 1]);
                mapNodes[i].AddConnection(mapNodes[i + 2]);
                mapNodes[i].AddConnection(mapNodes[i + 3]);
                continue;
            }

            if (i == nodeCount - 1)
            {
                if (i - 1 >= 0) mapNodes[i].AddConnection(mapNodes[i - 1]);
                if (i - 2 >= 0) mapNodes[i].AddConnection(mapNodes[i - 2]);
                if (i - 3 >= 0) mapNodes[i].AddConnection(mapNodes[i - 3]);
            }

            if (i % 2 == 0)
            {
                if (i + 3 < mapNodes.Count())
                    mapNodes[i].AddConnection(mapNodes[i + 3]);
                if (i + 2 < mapNodes.Count() && Random.value < branchingChance)
                    mapNodes[i].AddConnection(mapNodes[i + 2]);
            }
            else if (i % 3 == 0)
            {
                if (i + 3 < mapNodes.Count())
                    mapNodes[i].AddConnection(mapNodes[i + 3]);
                if (i + 2 < mapNodes.Count() && Random.value < branchingChance)
                    mapNodes[i].AddConnection(mapNodes[i + 2]);
            }
            else
            {
                if (i + 3 < mapNodes.Count())
                    mapNodes[i].AddConnection(mapNodes[i + 3]);
                if (i + 4 < mapNodes.Count() && Random.value < branchingChance)
                    mapNodes[i].AddConnection(mapNodes[i + 4]);
                if (i + 5 < mapNodes.Count() && Random.value < branchingChance)
                    mapNodes[i].AddConnection(mapNodes[i + 5]);
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