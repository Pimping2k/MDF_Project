using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    [SerializeField] private Image line;
    public event Action<MapNode> OnMapNodeSelected;
    public List<MapNode> ConnectedMapNodes;
    public int levelIndex;
    public bool isAccessible = true;

    private Vector3 startPos;
    private RectTransform lineRectTransform;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnSelectedNode);
        if (line!=null)
        {
            startPos = this.transform.position;
        }
        
        DrawLine();
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

    private void DrawLine()
    {
        foreach (var mapNode in ConnectedMapNodes)
        {
            var lineInstance = Instantiate(line, this.transform);
            lineRectTransform = lineInstance.GetComponent<RectTransform>();
            
            Vector3 targetPos = mapNode.transform.position;
            float distance = Vector3.Distance(startPos, targetPos);
            lineRectTransform.sizeDelta = new Vector2(distance, lineRectTransform.sizeDelta.y);

            Vector3 direction = (targetPos - startPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            lineRectTransform.rotation = Quaternion.Euler(0, 0, angle);

            lineRectTransform.position = (startPos + targetPos) / 2f;
        }
    }
}