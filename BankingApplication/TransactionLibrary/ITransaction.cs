﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionEngine
{
    public interface ITransaction
    {
        void Execute();

        void UndoExecute();
    }
}