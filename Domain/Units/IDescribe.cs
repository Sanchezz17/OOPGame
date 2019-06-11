using System;

namespace Domain.Units
{
    public interface IDescribe
    {
        Type Type { get; }
        int Price { get; }
        UnitParameters Parameters { get; }
    }
}