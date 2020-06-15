using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityQuickSheet;
using System.Linq;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(Story01))]
public class Story01Editor : BaseExcelEditor<Story01>
{	    
    public override bool Load()
    {
        Story01 targetData = target as Story01;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<Story01Data>().ToArray();
            targetData.dataList = query.Deserialize<Story01Data>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
