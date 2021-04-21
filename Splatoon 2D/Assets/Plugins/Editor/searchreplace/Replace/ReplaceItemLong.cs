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
  public class ReplaceItemLong : ReplaceItem<DynamicTypeLong, long>
  {

    protected override long drawEditor()
    {
      return EditorGUILayout.LongField(Keys.Replace, replaceValue);
    }

    protected override void replace(SearchJob job, SerializedProperty prop, SearchResult result)
    {
#if PSR_FULL
      prop.longValue = replaceValue;
      result.replaceStrRep = replaceValue.ToString();
#endif
    }
  }

}