using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    public event Action<MapNode> OnMapNodeSelected;
    public List<MapNode> ConnectedMapNodes;
    public int levelIndex;
    public bool isAccessible = true;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnSelectedNode);
    }

    private void OnSelectedNode()
    {
        if (isAccessible)
        {
            isAccessible = false;
            OnMapNodeSelected?.Invoke(this);
        }
    }

    public void AddConnection(MapNode node)
    {
        if (!ConnectedMapNodes.Contains(node))
        {
            ConnectedMapNodes.Add(node);
            node.ConnectedMapNodes.Add(this);
        }
    }

    public void RemoveConnection(MapNode node)
    {
        if (ConnectedMapNodes.Contains(node))
        {
            ConnectedMapNodes.Remove(node);
            node.ConnectedMapNodes.Remove(this);
        }
    }
    
    
}
