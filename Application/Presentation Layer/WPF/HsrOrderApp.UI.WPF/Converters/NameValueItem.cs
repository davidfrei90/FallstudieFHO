namespace HsrOrderApp.UI.WPF.Converters
{
    public class NameValueItem
    {
        public NameValueItem(object value, string name)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}