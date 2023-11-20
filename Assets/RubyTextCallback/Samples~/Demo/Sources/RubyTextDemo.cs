using System;
using System.Collections.Generic;
using jp.netsis.RubyTextCallback;
using UnityEngine;
using UnityEngine.UIElements;

public class RubyTextDemo : MonoBehaviour
{
    private List<RubyTextElementCallback> rubyTextElementCallbacks = new ();
    void Start()
    {
        UIDocument uiDocument = this.GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;
        List<TextElement> textElements = root.Query<TextElement>().ToList();

        foreach (TextElement textElement in textElements)
        {
            RubyTextElementCallback rubyTextElementCallback = new RubyTextElementCallback(textElement);
            textElement.text = "<ruby=おたまじゃくし>蛞</ruby><ruby=にほんご>日本語</ruby>は<ruby=むずか>難</ruby>しい<ruby=にほんご>日本語</ruby>の<ruby=にほんご>日本語</ruby>による<ruby=にほんご>日本語</ruby>のための<ruby=にほんご>日本語</ruby>ですルビテスト";
            this.rubyTextElementCallbacks.Add(rubyTextElementCallback);
        }
    }

    // Update is called once per frame
    void OnDestroy()
    {
        foreach (RubyTextElementCallback rubyTextElementCallback in this.rubyTextElementCallbacks)
        {
            rubyTextElementCallback.Dispose();
        }
        this.rubyTextElementCallbacks.Clear();
    }
}
