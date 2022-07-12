using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Nodify.Calculator
{
    public static class OperationFactory
    {
        public static List<OperationInfoViewModel> GetOperationsInfo(Type container)
        {
            List<OperationInfoViewModel> result = new List<OperationInfoViewModel>();

            foreach (var method in container.GetMethods())
            {
                if (method.IsStatic)
                {
                    OperationInfoViewModel op = new OperationInfoViewModel
                    {
                        Title = method.Name
                    };

                    var attr = method.GetCustomAttribute<OperationAttribute>();
                    var para = method.GetParameters();

                    bool generateInputNames = true;

                    op.Type = OperationType.Normal;

                    if (para.Length == 2)
                    {
                        var delType = typeof(Func<double, double, double>);
                        var del = (Func<double, double, double>)Delegate.CreateDelegate(delType, method);

                        op.Operation = new BinaryOperation(del);
                    }
                    else if (para.Length == 1)
                    {
                        if (para[0].ParameterType.IsArray)
                        {
                            op.Type = OperationType.Expando;

                            var delType = typeof(Func<double[], double>);
                            var del = (Func<double[], double>)Delegate.CreateDelegate(delType, method);

                            op.Operation = new ParamsOperation(del);
                            op.MaxInput = int.MaxValue;
                        }
                        else
                        {
                            var delType = typeof(Func<double, double>);
                            var del = (Func<double, double>)Delegate.CreateDelegate(delType, method);

                            op.Operation = new UnaryOperation(del);
                        }
                    }
                    else if (para.Length == 0)
                    {
                        var delType = typeof(Func<double>);
                        var del = (Func<double>)Delegate.CreateDelegate(delType, method);

                        op.Operation = new ValueOperation(del);
                    }

                    if (attr != null)
                    {
                        op.MinInput = attr.MinInput;
                        op.MaxInput = attr.MaxInput;
                        generateInputNames = attr.GenerateInputNames;
                    }
                    else
                    {
                        op.MinInput = (uint)para.Length;
                        op.MaxInput = (uint)para.Length;
                    }

                    foreach (var param in para)
                    {
                        op.Input.Add(generateInputNames ? param.Name : null);
                    }

                    for (int i = op.Input.Count; i < op.MinInput; i++)
                    {
                        op.Input.Add(null);
                    }

                    result.Add(op);
                }
            }

            return result;
        }

        public static OperationViewModel GetOperation(OperationInfoViewModel info)
        {
            var input = info.Input.Select(i => new ConnectorViewModel
            {
                Title = i
            });

            switch (info.Type)
            {
                case OperationType.Expression:
                    return new ExpressionOperationViewModel
                    {
                        Title = info.Title,
                        Output = new ConnectorViewModel(),
                        Operation = info.Operation,
                        Expression = "1 + sin {a} + cos {b}"
                    };

                case OperationType.Calculator:
                    return new CalculatorOperationViewModel
                    {
                        Title = info.Title,
                        Operation = info.Operation,
                    };

                case OperationType.Expando:
                    var o = new ExpandoOperationViewModel
                    {
                        MaxInput = info.MaxInput,
                        MinInput = info.MinInput,
                        Title = info.Title,
                        Output = new ConnectorViewModel(),
                        Operation = info.Operation
                    };

                    o.Input.AddRange(input);
                    return o;

                case OperationType.Group:
                    return new OperationGroupViewModel
                    {
                        Title = info.Title,
                    };

                case OperationType.Graph:
                    return new OperationGraphViewModel
                    {
                        Title = info.Title,
                        DesiredSize = new Size(420, 250)
                    };

                default:
                {
                    var op = new OperationViewModel
                    {
                        Title = info.Title,
                        Output = new ConnectorViewModel(),
                        Operation = info.Operation
                    };

                    op.Input.AddRange(input);
                    return op;
                }
            }
        }
    }
}
