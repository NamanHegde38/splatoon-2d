#define PSR_FULL
#if DEMO
#undef PSR_FULL
#endif
using UnityEngine;
using UnityEditor;
using System.IO;

namespace sr
{
  [System.Serializable]
  public class ReplaceItemChar : ReplaceItem<DynamicTypeChar, char>
  {

    public override void Draw()
    {
#if PSR_FULL
      GUILayout.BeginHorizontal();
      float lw = EditorGUIUtility.labelWidth;
      EditorGUIUtility.labelWidth = SRWindow.compactLabelWidth;
      
      string strValue = EditorGUILayout.TextField(Keys.Replace, replaceValue.ToString());
      EditorGUIUtility.labelWidth = lw; // i love stateful gui! :(

      char newValue = replaceValue;
      if(strValue.Length > 0)
      {
        newValue = System.Convert.ToChar(strValue[0]);
      }
      if(replaceValue != newValue)
      {
        replaceValue = newValue;
        SRWindow.Instance.PersistCurrentSearch();
      }
      drawSwap();
      GUILayout.EndHorizontal();
#endif
    }

    protected override void replace(SearchJob job, SerializedProperty prop, SearchResult result)
    {
#if PSR_FULL
      prop.intValue = replaceValue;
      result.replaceStrRep = replaceValue.ToString();
#endif
    }
  }

}