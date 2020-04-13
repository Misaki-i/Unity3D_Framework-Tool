/****************************************************
    文件：ExcelBuild.cs
	功能：创建ScriptableObject文件
*****************************************************/
using UnityEngine;

using System.IO;
using UnityEditor;


public class ExcelPath
{
    /// 存放excel表文件夹的的路径
    public static readonly string excelsFolderPath = Application.dataPath + "/Excels/";

    /// 存放Excel转化CS文件的文件夹路径
    public static readonly string assetPath = "Assets/Resources/DataAssets/";
}

public class ExcelBuild : Editor
{
    [MenuItem("表格工具/更新数据表格")]
    public static void CreateAllBattleAsset()
    {

        //===》需要读取的Excel表格
        string battleCfgName = ExcelPath.excelsFolderPath + "Info.xlsx";

        //===》读取的表
        string Enemy = "怪物";

        InfoCfg cfg = ScriptableObject.CreateInstance<InfoCfg>();

        //===》赋值
        cfg.enemyArray = ExcelTool.CreateEnemyData(battleCfgName, Enemy);



        //确保文件夹存在
        if (!Directory.Exists(ExcelPath.assetPath))
        {
            Directory.CreateDirectory(ExcelPath.assetPath);
        }

        //asset文件的路径 要以"Assets/..."开始，否则CreateAsset会报错
        string assetPath = string.Format("{0}{1}.asset", ExcelPath.assetPath, "Info");
        //生成一个Asset文件
        AssetDatabase.CreateAsset(cfg, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("=========》生成BattleInfo数据成功");
    }
}
