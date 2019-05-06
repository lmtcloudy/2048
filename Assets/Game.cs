using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] int row;
    [SerializeField] int line;
    [SerializeField] Transform center;//棋盘放置容器
    [SerializeField] Node m_node;//预制体
    [SerializeField] Camera camera;
    Node[,] nodesPanel;//棋盘元素存放
    public List<Node> EmptyNodes = new List<Node>();//棋盘中没有数字的元素列表
    public List<Node> NotEmptyNodes = new List<Node>();//棋盘中有数字的元素列表
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
    //生成棋盘
    public void CreatePanel()
    {
        if (row <= 0 || line <= 0) return;
        int num = 0;
        for(int i = 0; i < row; i ++)
        {
            for (int j = 0; j < line; j++)
            {
                num++;
                Node go = Instantiate(m_node,center.transform);
                go.transform.localPosition = new Vector3(j*100 - line * 100/2f + 50,i*100 - row *100/2f +50,0);
                go.ID = 0;
                go.position = new int[] { j,i };//设置位置
                nodesPanel[j,i] = go;
                EmptyNodes.Add(go);
            }
        }
    }
    //每次刷新数字
    public void SetNum()
    {
        if (EmptyNodes == null || EmptyNodes.Count <= 0) return;
        int randomCount = Random.Range(1,3);
        for (int i = 0; i < randomCount; i++)
        {
            int index = Random.Range(0,EmptyNodes.Count);
            EmptyNodes[index].ID = 1;
            NotEmptyNodes.Add(EmptyNodes[index]);
            EmptyNodes.RemoveAt(index);
        }
    }
    //获取每个棋盘元素的周围元素
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
    public void TapShang()
    {
        SetNum();
        NotEmptyNodes.Sort((x,y)=> {
            if(x.position[1] > y.position[1])
            {
                return 1;
            }
            return 0;
        });
    }
}
 