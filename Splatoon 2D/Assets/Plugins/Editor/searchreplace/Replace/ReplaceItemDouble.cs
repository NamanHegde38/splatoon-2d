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
  public class ReplaceItemDouble : ReplaceItem<DynamicTypeDouble, double>
  {

    protected override double drawEditor()
    {
      return EditorGUILayout.DoubleField(Keys.Replace, replaceValue);
    }

    protected override void replace(SearchJob job, SerializedProperty prop, SearchResult result)
    {
 #if PSR_FULL
      prop.doubleValue = replaceValue;
      result.replaceStrRep = replaceValue.ToString();
 #endif
    }
  }



}