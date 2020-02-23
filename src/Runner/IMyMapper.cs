using System;
using System.Collections.Generic;
using System.Text;

namespace Runner
{
    public interface IMyMapper
    {
        void Register<TFrom, TTo>();

        TTo Map<TFrom, TTo>(TFrom source);
    }
}
