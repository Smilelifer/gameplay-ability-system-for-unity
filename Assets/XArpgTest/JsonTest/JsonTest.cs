using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    [Serializable]
    public class TestCat
    {
        public string name;
        public int age;
        public bool isMale;
        public Vector3 position;
        public List<string> hobbies;
        
        public override string ToString()
        {
            return "Name: " + name + "\nAge: " + age + "\nIsMale: " + isMale + "\nPosition: " + position + "\nHobbies: " + string.Join(", ", hobbies);
        }
    }
    
    public string JsonPath = "Assets/XArpgTest/JsonTest/";
    public TestCat cat;
    
    [Button]
    public void ExportJson()
    {
        string json = JsonMapper.ToJson(cat);
        
        //把json字符串写入文件
        using (StreamWriter sw = new StreamWriter(JsonPath + "cat.json"))
        {
            sw.Write(json);
        }
        AssetDatabase.Refresh();
    }

    [Button]
    public void ImportJson()
    {
        //读取json字符串
        string json = "";
        using (StreamReader sr = new StreamReader(JsonPath + "cat.json"))
        {
            json = sr.ReadToEnd();
            //反序列化为对象
            TestCat cat = JsonMapper.ToObject<TestCat>(json);

            //打印对象属性
            Debug.Log(cat.ToString());
        }
    }
}
