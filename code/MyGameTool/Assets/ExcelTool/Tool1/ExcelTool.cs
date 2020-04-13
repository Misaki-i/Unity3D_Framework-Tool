/****************************************************
    文件：ExcelTool.cs
	功能：表格读取工具类
*****************************************************/


/*
 
    PS ： 不推荐， 还是= = 打包成AssetBundle的好，

    ScriptableObject 读取不好容易修改里面的值！！！！

*/

using System.Data;
using UnityEngine;
using System.Collections.Generic;
using Excel;
using System.IO;

public class ExcelTool : MonoBehaviour
{
    static readonly int StartRow = 4;               //根据表格中定义， 开始读取的行数(第五行开始)



    //==========》 读取Enemy配置数据
    public static EnemyProps[] CreateEnemyData(string filePath, string tableName)
    {
        //获得表数据
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = ReadExcel(filePath, tableName, ref columnNum, ref rowNum);
        EnemyProps[] array = new EnemyProps[rowNum - StartRow];
        for (int i = StartRow; i < rowNum; i++)
        {
            EnemyProps enemy = new EnemyProps();
            //解析每列的数据
            //enemy.id = GameDataUtils.ReadInt(collect[i][0]);
            //enemy.enmeyType = (EnemyType)GameDataUtils.ReadInt(collect[i][0]);
            //enemy.enemyName = collect[i][1].ToString();
            //enemy.des = collect[i][2].ToString();
            //enemy.iconPath = collect[i][3].ToString();
            //enemy.checkRange = GetDistance(collect[i][4]);
            //enemy.attack = GameDataUtils.ReadFloat(collect[i][5]);
            //enemy.adRange = GetDistance(collect[i][6]);
            //enemy.adSpeed = GameDataUtils.ReadFloat(collect[i][7]);
            //enemy.adInterval = GameDataUtils.ReadFloat(collect[i][8]);
            //enemy.moveSpeed = GameDataUtils.ReadFloat(collect[i][9]) / 100;
            //enemy.defense = GameDataUtils.ReadFloat(collect[i][10]);
            //enemy.hp = GameDataUtils.ReadFloat(collect[i][11]);
            //enemy.exp = GameDataUtils.ReadFloat(collect[i][12]);

            array[i - StartRow] = enemy;
        }
        return array;
    }


    // etc.............










    /// <summary>
    /// 读取excel文件内容
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="tableName">表的名字</param>
    /// <param name="columnNum">行数</param>
    /// <param name="rowNum">列数</param>
    /// <returns></returns>
    static DataRowCollection ReadExcel(string filePath, string tableName, ref int columnNum, ref int rowNum)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        //Tables[0] 下标0表示excel文件中第一张表的数据
        columnNum = result.Tables[tableName].Columns.Count;
        rowNum = result.Tables[tableName].Rows.Count;

        //判断有效行数
        DataRowCollection tempData = result.Tables[tableName].Rows;
        for (int i = StartRow; i < rowNum; i++)
        {
            if (tempData[i][0].ToString() == "")
            {
                rowNum = i;
                break;
            }
        }
        return result.Tables[tableName].Rows;
    }

}
