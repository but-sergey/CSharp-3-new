﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTest.Infrastructure.Commands.Base;

namespace WpfTest.Infrastructure.Commands
{
    class LambdaCommand : Command
    {
        private readonly Action<object> _Execute;

        private readonly Func<object, bool> _CanExecute;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        protected override bool CanExecute(object p) => _CanExecute?.Invoke(p) ?? true;

        protected override void Execute(object p) => _Execute(p);
    }
}
