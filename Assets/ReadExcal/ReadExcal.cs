using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Excel;
using System.Data;
using System.IO;

public class ReadExcal : MonoBehaviour
{

    public Text text;

    public void Start()
    {
        Read();
    }
    private void Read()
    {

        string path = Application.streamingAssetsPath + "/item.xlsx";
        Debug.Log("读取路径："+path);
        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream); //读取*.xls
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);  //读取*.xlsx

        text.text = "";
        while (excelReader.Read())
        {
            for (int i = 0; i < excelReader.FieldCount; i++)
            {
                string value = excelReader.IsDBNull(i) ? "null" : excelReader.GetString(i);

                text.text += value + "|";
            }
            text.text += "一行" + "\n";
        }


        DataSet result = excelReader.AsDataSet();
        int columns = result.Tables[0].Columns.Count;//获取列数
        int rows = result.Tables[0].Rows.Count;//获取行数
        Debug.Log(columns);
        Debug.Log(rows);
        //从第二行开始读
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                string nvalue = result.Tables[0].Rows[i][j].ToString();
                Debug.Log(nvalue);
            }
        }
    }
}
