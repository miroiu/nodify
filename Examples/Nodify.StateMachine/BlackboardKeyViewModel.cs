namespace Nodify.StateMachine
{
    public class BlackboardKeyViewModel : ObservableObject
    {
        public string? PropertyName { get; set; }

        private string _name = "New key";
        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SetProperty(ref _name, value);
                }
            }
        }

        private BlackboardKeyType _type;
        public BlackboardKeyType Type
        {
            get => _type;
            set
            {
                if (SetProperty(ref _type, value))
                {
                    Value = GetDefaultValue(_type);
                }
            }
        }

        private object? _value = BoxValue.False;
        public object? Value
        {
            get => _value;
            set => SetProperty(ref _value, GetRealValue(value));
        }

        private object? GetRealValue(object? value)
        {
            if (value is string str)
            {
                switch (Type)
                {
                    case BlackboardKeyType.Boolean:
                        if (bool.TryParse(str, out var b))
                        {
                            value = b;
                        }
                        break;

                    case BlackboardKeyType.Integer:
                        if (int.TryParse(str, out var i))
                        {
                            value = i;
                        }
                        break;

                    case BlackboardKeyType.Double:
                        if (double.TryParse(str, out var d))
                        {
                            value = d;
                        }
                        break;

                    case BlackboardKeyType.String:
                    case BlackboardKeyType.Object:
                        value = str;
                        break;
                }
            }

            return value;
        }

        public static object? GetDefaultValue(BlackboardKeyType type)
            => type switch
            {
                BlackboardKeyType.Boolean => BoxValue.False,
                BlackboardKeyType.Integer => BoxValue.Int0,
                BlackboardKeyType.Double => BoxValue.Double0,
                BlackboardKeyType.String => null,
                BlackboardKeyType.Object => null,
                _ => null
            };
    }
}
