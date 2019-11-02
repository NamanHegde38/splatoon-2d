using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class EditorCompileTimer : MonoBehaviour {

    private double _compileStartTime;
    private bool _isCompiling;
    
    private void Update() {
        if (_isCompiling) {
            if (!EditorApplication.isCompiling) {
                _isCompiling = false;
                CompileFinished();
            }            
        }

        else {
            if (EditorApplication.isCompiling) {
                _isCompiling = true;
                CompileStarted();
            }
        }
    }

    private void CompileStarted() {
        Debug.Log("Compile started ...");
        _compileStartTime = EditorApplication.timeSinceStartup;
    }

    private void CompileFinished() {
        var compileTime = EditorApplication.timeSinceStartup - _compileStartTime;
        Debug.Log("Compile finished " + compileTime.ToString("F2") + "s");
    }
}
