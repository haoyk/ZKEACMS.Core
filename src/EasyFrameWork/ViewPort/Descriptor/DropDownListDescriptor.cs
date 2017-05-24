/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Constant;
using Easy.Modules.DataDictionary;
using System;
using System.Collections.Generic;
using System.Text;
using Easy.ViewPort.Validator;
using System.Reflection;
using Easy.Extend;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Easy.ViewPort.Descriptor
{
    public class DropDownListDescriptor : BaseDescriptor<DropDownListDescriptor>
    {
        private IDictionary<string, string> _data;
        private Func<Dictionary<string, string>> _souceFunc;
        public DropDownListDescriptor(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.DropDownList;
            this.TemplateName = "DropDownList";
        }


        public IDictionary<string, string> OptionItems
        {
            get
            {
                if (_souceFunc != null)
                {
                    _data = _souceFunc.Invoke();
                }
                if (this.SourceType == SourceType.Dictionary)
                {
                    IDataDictionaryService dicService = ServiceLocator.GetService<IDataDictionaryService>();
                    if (dicService != null)
                    {
                        _data = new Dictionary<string, string>();

                        var dicts = dicService.Get(m => m.DicName == this.SourceKey).ToList();
                        foreach (DataDictionaryEntity item in dicts)
                        {
                            this._data.Add(item.DicValue, item.Title);
                        }
                    }

                }
                return _data ?? (_data = new Dictionary<string, string>());
            }
        }
        public SourceType SourceType { get; set; }
        public string SourceKey { get; set; }
        public string GetOptions()
        {
            var builder = new StringBuilder();
            foreach (var item in OptionItems)
            {
                builder.AppendFormat("<option value='{0}'>{1}</option>", item.Key, item.Value);
            }
            return builder.ToString();
        }

        public DropDownListDescriptor DataSource(IDictionary<string, string> Data)
        {
            this._data = Data;
            return this;
        }

        public DropDownListDescriptor DataSource<T>()
        {
            Type dataType = typeof(T);
            if (!dataType.GetTypeInfo().IsEnum)
            {
                throw new Exception(dataType.FullName + ",不是枚举类型。");
            }
            string[] text = Enum.GetNames(dataType);

            for (int i = 0; i < text.Length; i++)
            {
                this._data.Add(Enum.Format(dataType, Enum.Parse(dataType, text[i], true), "d"), text[i]);
            }
            return this;
        }
        public DropDownListDescriptor DataSource(string Url)
        {
            if (this.Properties.ContainsKey("DataSource"))
            {
                this.Properties["DataSource"] = Url;
            }
            else
            {
                this.Properties.Add("DataSource", Url);
            }
            return this;
        }
        public DropDownListDescriptor DataSource(string dictionaryType, SourceType sourceType)
        {
            this.SourceKey = dictionaryType;
            this.SourceType = sourceType;
            return this;
        }

        public DropDownListDescriptor DataSource(SourceType type)
        {
            this.SourceKey = this.ModelType.Name + "@" + this.Name;
            this.SourceType = type;
            return this;
        }

        public DropDownListDescriptor DataSource(Func<Dictionary<string, string>> souceFunc)
        {
            _souceFunc = souceFunc;
            return this;
        }
    }
}
