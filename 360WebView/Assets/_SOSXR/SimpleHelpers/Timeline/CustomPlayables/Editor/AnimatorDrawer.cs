/*

[CustomPropertyDrawer(typeof (AnimatorBehaviour))]
public class AnimatorDrawer : PropertyDrawer
{
    private readonly List<string> parameterOptions = new List<string>();
    private bool listIsPopulated = false;

    private const string selectParameter = "select Parameter...";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var clip = property.serializedObject.targetObject as AnimatorClip;

        if (!clip)
        {
            return;
        }

        var clipTemplate = clip.template;
        if (clipTemplate == null)
        {
            return;
        }

        if (clipTemplate.trackBinding == null)
        {
            return;
        }

        if (clip as BoolClip)
        {
            CreateBoolClipInspector(clipTemplate);
        }
        else if (clip as TriggerClip)
        {
            CreateTriggerClipInspector(clipTemplate);
        }
    }


    private void CreateBoolClipInspector(AnimatorBehaviour template)
    {
        var isSelected = template.boolName != "";

        AddParametersToList(template, isSelected, AnimatorControllerParameterType.Bool);

        var optionIndex = SelectedOptionIndex(template.boolName, isSelected);

        EditorGUI.BeginChangeCheck();
        var selection = EditorGUILayout.Popup(optionIndex, parameterOptions.ToArray());

        if (EditorGUI.EndChangeCheck())
        {
            template.boolName = parameterOptions[selection];
            template.boolIndex = Animator.StringToHash(parameterOptions[selection]);
        }

        if (template.boolName != "")
        {
            template.boolValue = EditorGUILayout.Toggle("'" + template.boolName + "' value", template.boolValue);
            template.resetBool = EditorGUILayout.Toggle("Reset '" + template.boolName + "' to original value", template.resetBool);
        }
    }


    private void CreateTriggerClipInspector(AnimatorBehaviour template)
    {
        var isSelected = template.triggerName != "";

        AddParametersToList(template, isSelected, AnimatorControllerParameterType.Trigger);

        var optionIndex = SelectedOptionIndex(template.triggerName, isSelected);

        EditorGUI.BeginChangeCheck();
        var selection = EditorGUILayout.Popup(optionIndex, parameterOptions.ToArray());

        if (EditorGUI.EndChangeCheck())
        {
            template.triggerName = parameterOptions[selection];
            template.triggerIndex = Animator.StringToHash(parameterOptions[selection]);
        }

        if (template.triggerName != "")
        {
            template.triggerOnce = EditorGUILayout.Toggle("Trigger '" + template.triggerName + "' only once", template.triggerOnce);
            template.forceTriggerClipLength = EditorGUILayout.Toggle("Force '" + template.triggerName + "' clip duration", template.forceTriggerClipLength);
        }

        GUILayout.Space(15f);
    }




    private int SelectedOptionIndex(string parameterName, bool isSelected)
    {
        var optionIndex = 0;

        if (isSelected)
        {
            parameterOptions.Remove(selectParameter);
            optionIndex = parameterOptions.FindIndex(n => n.Contains(parameterName));
        }

        return optionIndex;
    }


    private void AddParametersToList(AnimatorBehaviour template, bool isSelected, AnimatorControllerParameterType parameterType)
    {
        if (!listIsPopulated)
        {
            if (!isSelected)
            {
                parameterOptions.Add(selectParameter);
            }

            var anim = template.trackBinding;

            for (int i = 0; i < anim.parameterCount; i++)
            {
                if (anim.GetParameter(i).type == parameterType)
                {
                    parameterOptions.Add(anim.GetParameter(i).name);
                }
            }

            listIsPopulated = true;
        }
    }



}
*/

