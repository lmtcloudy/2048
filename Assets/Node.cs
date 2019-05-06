using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    private int id = 0;
    [SerializeField] Text text;
    private bool isEmpty;
    public Node shang;
    public Node xia;
    public Node zuo;
    public Node you;

    public int ID
    {
        get { return id; }
        set { id = value; text.text = id == 0 ? "" : id.ToString(); }
    }
    public bool IsEmpty
    {
        get { return id == 0 ? true : false; }
    }
}
