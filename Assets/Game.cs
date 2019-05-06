using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] int row;
    [SerializeField] int line;
    [SerializeField] Transform center;
    [SerializeField] Node m_node;
    [SerializeField] Camera camera;
    Node[,] nodesPanel;
    List<Node> EmptyNodes = new List<Node>();
    // Start is called before the first frame update
    void Start()
    {
        nodesPanel = new Node[line,row];
        CreatePanel();
        SetNum();
        GetBrother();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreatePanel()
    {
        if (row <= 0 || line <= 0) return;
        Vector3 center_point = new Vector3(row/2f,line/2f,0);
        int num = 0;
        for(int i = 0; i < row; i ++)
        {
            for (int j = 0; j < line; j++)
            {
                num++;
                Node go = Instantiate(m_node,center.transform);
                go.transform.localPosition = new Vector3(j*100 - line * 100/2f + 50,i*100 - row *100/2f +50,0);
                go.ID = 0;
                nodesPanel[j,i] = go;
                EmptyNodes.Add(go);
            }
        }
    }
    public void SetNum()
    {
        if (EmptyNodes == null || EmptyNodes.Count <= 0) return;
        int randomCount = Random.Range(1,3);
        for (int i = 0; i < randomCount; i++)
        {
            int index = Random.Range(0,EmptyNodes.Count);
            EmptyNodes[index].ID = 1;
            EmptyNodes.RemoveAt(index);
        }
    }
    public void GetBrother()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < line; j++)
            {
                Node item = nodesPanel[j, i];
                item.shang = i+1 >= row ?null: nodesPanel[j, i + 1];
                item.xia = i -1 <0 ? null : nodesPanel[j, i - 1];
                item.zuo = j - 1 < 0 ? null : nodesPanel[j -1, i];
                item.you = j +1 >= line ? null : nodesPanel[j+1, i];
            }
        }
    }
}
 