﻿using System;

namespace Domain.Units
{
    public class DescribeObject
    {
        public Type Type { get; }
        public int Price { get; }
        public UnitParameters Parameters { get; set; }

        public DescribeObject(Type type, int price, UnitParameters parameters)
        {
            Type = type;
            Price = price;
            Parameters = parameters;
        }
    }
}
