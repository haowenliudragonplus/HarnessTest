using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace DiffFileGenerator
{
    public class Group1
    {
        public int __Id;
        public string __Name;
        public int __Uid;
        public int _Next;
        public string __Pathid;
    }

    public class Group2
    {
        public string m_OnCollision;
        public int pos_x;
        public int pos_y;
        public int pos_z;
        public string spriteName;
        public float amount;
        public int type;
    }

    public class Group3
    {
        public string m_PersistentCalls;
        public int[] m_rewardList;
    }

    public class Pattern1
    {
        public int id;
        public Group1 group1;
        public Group2 group2;
    }

    public class Pattern2
    {
        public int id;
        public Group2 group2;
        public List<Group3> LastGroups;
    }

    public class Pattern3
    {
        public string m_TypeName;
        public List<Group2> types;
        public bool visible;
    }

    public static class DiffFileGenerator
    {
        public static string DiffFileDir = Application.dataPath + "/../../DiffFile/" + Application.productName.Replace(" ", "") + "_GameConfig";

        [MenuItem(EditorConst.GenerateDiffFile, priority = EditorConst.Priority_GenerateDiffFile)]
        static void GenerateDiffFile()
        {
            string upperFolderDir = Path.GetDirectoryName(DiffFileDir);
            IOUtils.DeleteDirectory(upperFolderDir);
            IOUtils.CreateDirectory(DiffFileDir);

            for (int i = 0; i < 31111; i++)
            {
                string filePath = Path.Combine(DiffFileDir, Application.productName.Replace(" ", "") + "_data" + (i + 1) + ".txt");
                float p = Random.Range(0, 1f);
                if (p < 0.2f)
                {
                    Pattern1 p1 = new Pattern1();
                    p1.id = i;
                    p1.group1 = randomOneGroup1();
                    p1.group2 = randomOneGroup2();
                    var str = JsonConvert.SerializeObject(p1);
                    SaveFile(filePath, str);
                }
                else if (p >= 0.2 && p < 0.7f)
                {
                    Pattern2 p2 = new Pattern2();
                    p2.group2 = randomOneGroup2();
                    p2.id = i;
                    p2.LastGroups = new List<Group3>();
                    int lastL = Random.Range(0, 4);
                    for (int j = 0; j < lastL; j++)
                    {
                        p2.LastGroups.Add(randomOneGroup3());
                    }
                    var str = JsonConvert.SerializeObject(p2);
                    SaveFile(filePath, str);
                }
                else
                {
                    Pattern3 p3 = new Pattern3();
                    p3.types = new List<Group2>();
                    int l = Random.Range(1, 4);
                    for (int j = 0; j < l; j++)
                    {
                        p3.types.Add(randomOneGroup2());
                    }
                    p3.visible = !(Random.Range(0, 1f) > 0.5f);

                    int nameLength = Random.Range(4, 30);
                    StringBuilder names = new StringBuilder();
                    for (int j = 0; j < nameLength; j++)
                    {
                        names.Append(getRandomLetter());
                    }

                    p3.m_TypeName = names.ToString();
                    var str = JsonConvert.SerializeObject(p3);
                    SaveFile(filePath, str);
                }
            }
        }

        static void SaveFile(string filePath, string content)
        {
            string dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        static Group1 randomOneGroup1()
        {
            Group1 res = new Group1();

            res.__Id = res.GetHashCode();
            int nameLength = Random.Range(4, 30);
            StringBuilder names = new StringBuilder();
            for (int i = 0; i < nameLength; i++)
            {
                names.Append(getRandomLetter());
            }

            res.__Name = names.ToString();
            res.__Pathid = Random.Range(int.MinValue, int.MaxValue).ToString();
            res.__Uid = Random.Range(int.MinValue, int.MaxValue);
            res._Next = Random.Range(0, 150000);
            return res;
        }

        static Group2 randomOneGroup2()
        {
            Group2 res = new Group2();

            res.amount = Random.Range(0, 0f);
            res.pos_x = Random.Range(0, 10);
            res.pos_y = Random.Range(0, 10);
            res.pos_z = Random.Range(-10, 100);
            res.type = Random.Range(0, 3);
            StringBuilder names = new StringBuilder();
            int nameLength = Random.Range(4, 30);
            for (int i = 0; i < nameLength; i++)
            {
                names.Append(getRandomLetter());
            }
            res.spriteName = names.ToString();
            return res;
        }

        static Group3 randomOneGroup3()
        {
            Group3 res = new Group3();
            int randomL = Random.Range(0, 10);
            res.m_rewardList = new int[randomL];
            for (int i = 0; i < randomL; i++)
            {
                res.m_rewardList[i] = Random.Range(0, 394);
            }
            StringBuilder names = new StringBuilder();
            int nameLength = Random.Range(4, 30);
            for (int i = 0; i < nameLength; i++)
            {
                names.Append(getRandomLetter());
            }
            res.m_PersistentCalls = names.ToString();
            return res;
        }

        private static readonly string[] letters = new[]
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
            "v", "w", "x", "y", "z", "-", "_", "@", "#", " "
        };
        static string getRandomLetter()
        {
            var res = letters[Random.Range(0, letters.Length)];
            if (Random.Range(0, 1f) > 0.5f)
            {
                return res.ToUpper();
            }
            return res;
        }
    }
}