using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;
using RubyShowType = jp.netsis.RubyTextCallback.RubyTextConstants.RubyShowType;

namespace jp.netsis.RubyTextCallback
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RubyTextElementCallback : IRubyText, IDisposable
    {
        private const string CTOR_ARGS_ERROR = "The argument specified is not appropriate.";

        [Tooltip("v offset ruby. (em, px, %).")]
        private string _rubyVerticalOffset = "1em";
        
        [Tooltip("ruby scale. (1=100%)")]
        private float _rubyScale = 0.5f;

        [Tooltip("The height of the ruby line can be specified. (em, px, %).")]
        private string _rubyLineHeight = "";

        [Tooltip("ruby show type.")]
        private RubyShowType _rubyShowType = RubyShowType.RUBY_ALIGNMENT;

        [Tooltip("Affects only BASE_NO_OVERRAP_RUBY_ALIGNMENT ruby margin.")] 
        private float _rubyMargin = 10;

        private bool _isRightToLeftText;

        private string uneditedText;

        private IVisualElementScheduledItem changeEventScheduledItem;

        private long intervalMs;

        public TextElement textElement { get; private set; }

        public RubyTextElementCallback(TextElement textElement,long intervalMs = 5)
        {
            if (textElement == null)
            {
                throw new ArgumentException(CTOR_ARGS_ERROR);
            }
            
            this.textElement = textElement;
            this.textElement.RegisterCallback<ChangeEvent<string>>(this.OnChangeEvent);
            this.intervalMs = intervalMs;
        }

        private void OnChangeEvent(ChangeEvent<string> onChangeEvent)
        {
            this.uneditedText = onChangeEvent.newValue;

            // TODO : Searching for a better way
            this.changeEventScheduledItem = this.textElement.schedule.Execute(
                () =>
                {
                    // Uninitialized state is font size 0.
                    if (this.textElement.resolvedStyle.fontSize == 0)
                    {
                        return;
                    }
                    
                    this.OnTextChanged();
                    this.changeEventScheduledItem.Pause();
                }).Every(this.intervalMs);
        }

        public void OnTextChanged()
        {
            string replacedText = this.ReplaceRubyTags(this.uneditedText);

            if (this.textElement is INotifyValueChanged<string> notifyValueChanged)
            {
                notifyValueChanged.SetValueWithoutNotify(replacedText);
            }
        }

        /// <summary>
        /// replace ruby tags.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>replaced str</returns>
        private string ReplaceRubyTags(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            // Replace <ruby> tags text layout.
            float nonBreakSpaceW = this.GetPreferredValues("\u00A0a").x - this.GetPreferredValues("a").x;
            float hiddenSpaceW = nonBreakSpaceW * this.rubyScale;
            float fontSizeScale = 1f;

            int dir = this.isRightToLeftText ? -1 : 1;
            str = this.ReplaceRubyTags(str, dir, fontSizeScale, hiddenSpaceW);

            return str;
        }
        
        public void Dispose()
        {
            if (this.textElement != null)
            {
                this.textElement.UnregisterCallback<ChangeEvent<string>>(this.OnChangeEvent);
            }
            this.textElement = null;
        }

        #region IRubyText Implementation
        public bool enableAutoSizing => false; // TODO : unsupported
        public bool isOrthographic => true; // TODO : unsupported

        // TODO : unsupported
        public bool isRightToLeftText
        {
            get => this._isRightToLeftText;
            set => this._isRightToLeftText = value;
        } 
        public Vector2 GetPreferredValues(string str)
        {
            return this.textElement.MeasureTextSize(str,
                0, VisualElement.MeasureMode.Undefined,
                0, VisualElement.MeasureMode.Undefined);
        }

        public string rubyVerticalOffset => this._rubyVerticalOffset;
        public float rubyScale => this._rubyScale;
        public string rubyLineHeight => this._rubyLineHeight;
        public RubyShowType rubyShowType => this._rubyShowType;
        public float rubyMargin => this._rubyMargin;

        #endregion
    }
}
