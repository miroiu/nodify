﻿using System.ComponentModel;
using System.Windows;

namespace Nodify.Calculator
{
    public class CalculatorOperationViewModel : OperationViewModel
    {
        public CalculatorViewModel InnerCalculator { get; } = new CalculatorViewModel();

        private OperationViewModel InnerOutput { get; } = new OperationViewModel
        {
            Title = "Output Parameters",
            Input = { new ConnectorViewModel() },
            Location = new Point(500, 300)
        };

        private CalculatorInputOperationViewModel InnerInput { get; } = new CalculatorInputOperationViewModel
        {
            Title = "Input Parameters",
            Location = new Point(300, 300)
        };

        public CalculatorOperationViewModel()
        {
            InnerCalculator.Operations.Add(InnerInput);
            InnerCalculator.Operations.Add(InnerOutput);

            Output = new ConnectorViewModel();

            InnerOutput.Input[0].PropertyChanged += OnOutputValueChanged;

            InnerInput.Output.ForEach(x => Input.Add(new ConnectorViewModel
            {
                Title = x.Title
            }));

            InnerInput.Output
                .WhenAdded(x => Input.Add(new ConnectorViewModel
                {
                    Title = x.Title
                }))
                .WhenRemoved(x => Input.RemoveOne(i => i.Title == x.Title));
        }

        protected override void OnInputValueChanged()
        {
            for (var i = 0; i < Input.Count; i++)
            {
                InnerInput.Output[i].Value = Input[i].Value;
            }
        }

        private void OnOutputValueChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (Output != null && propertyChangedEventArgs.PropertyName == nameof(ConnectorViewModel.Value))
            {
                Output.Value = InnerOutput.Input[0].Value;
            }
        }
    }
}
