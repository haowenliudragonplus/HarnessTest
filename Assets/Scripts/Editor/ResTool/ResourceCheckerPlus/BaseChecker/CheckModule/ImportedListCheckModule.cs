using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


namespace ResourceCheckerPlus
{
	public class ImportedListCheckModule : CustomCheckModule
	{
		public enum ImportType
		{
			All,
			Actor,
			Effect,
			GameObject,
			Material,
			Movie,
			SkinModel,
			Sound,
			StreamingObject,
			Texture,
			UI
		}
		private ImportType selectedType = ImportType.All;
		public static string[] importTypeStr = new string[] { ImportType.All.ToString(),ImportType.Actor.ToString(), ImportType.Effect.ToString(), ImportType.GameObject.ToString(), ImportType.Material.ToString(), ImportType.Movie.ToString(), ImportType.SkinModel.ToString(), ImportType.Sound.ToString(), ImportType.StreamingObject.ToString(), ImportType.Texture.ToString(), ImportType.UI.ToString() };

		public List<Object> objectsList = new List<Object>();
		public override void ShowCommonSideBarContent()
		{
            checkPrefabDetailReference = GUILayout.Toggle(checkPrefabDetailReference, checkPrefabDetailRefContent);

            selectedType = (ImportType)EditorGUILayout.Popup( "导入类型选择", (int) selectedType, importTypeStr );
			if ( GUILayout.Button( "从资源表中导入", GUILayout.Width( ResourceCheckerPlus.instance.checkerConfig.sideBarWidth ) ) )
			{
				ImportResourceList();
				CheckerInterface.CheckResource( objectsList.ToArray() );

			}
			if ( GUILayout.Button( "载入所有筛选", GUILayout.Width( ResourceCheckerPlus.instance.checkerConfig.sideBarWidth ) ) )
			{
				CheckerInterface.ApplyCheckFilter();
			}
		}

		private void ImportResourceList()
		{
			objectsList.Clear();

//            TableCtrl.Instance.InitTableUnPacker();
//			Table_Resource[] tableRes = TableCtrl.Instance.Table_ResourceDic;
//			
//			Debug.Log( "tableRes.length==" + tableRes.Length );
//			for ( int i = 0; i < tableRes.Length; ++i )
//			{
//				Table_Resource res = tableRes[i];
//				if ( res.type.Equals( "Level" ) )
//					continue;
//				if ( selectedType != ImportType.All && res.type.Equals( selectedType.ToString() )  || selectedType == ImportType.All )
//				{
//					Object obj = Resources.Load( res.path );
//					objectsList.Add( obj );
//				}
//				else continue;
//			}
//			Debug.Log( "objectslist.count==" + objectsList.Count );
		}
	}
}
