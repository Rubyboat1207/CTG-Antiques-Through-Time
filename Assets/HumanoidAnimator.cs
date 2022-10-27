using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReadOnlyAttribute : PropertyAttribute
    {
    
    }
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    
        public override void OnGUI(Rect position,
                                    SerializedProperty property,
                                    GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

[RequireComponent(typeof(Animator))]
public class HumanoidAnimator : MonoBehaviour
{
    [Header("Debug Info: ")]
    [ReadOnly] [SerializeField] Vector3 lastPos;
    [ReadOnly] [SerializeField] Vector3 curPos;
    [ReadOnly] [SerializeField] Vector3 positionDelta;
    Animator animator;

    private void Start() {
        lastPos = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;

        positionDelta = lastPos - curPos;

        Vector3 velocity = GetVelocity();

        animator.SetFloat("X_Vel", velocity.x);
        //End of frame
        lastPos = curPos;
    }

    Vector3 GetVelocity() {
        return positionDelta / Time.deltaTime;
    }
}
