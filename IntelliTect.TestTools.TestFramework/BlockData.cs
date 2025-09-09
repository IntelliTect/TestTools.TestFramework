using System;
using System.Collections.Generic;

namespace IntelliTect.TestTools.TestFramework;

public interface IBlockData
{
    public List<KeyValuePair<Type, object?>> Data { get; }
}

public class BlockData<T1, T2>(T1 data1, T2 data2) : IBlockData
{
    static BlockData()
    {
        ValidateData.ValidateUniqueTypes(typeof(T1), typeof(T2));
    }

    public List<KeyValuePair<Type, object?>> Data { get; } = 
    [
        new KeyValuePair<Type, object?>(typeof(T1), data1),
        new KeyValuePair<Type, object?>(typeof(T2), data2)
    ];
}

public class BlockData<T1, T2, T3>(T1 data1, T2 data2, T3 data3) : IBlockData
{
    static BlockData()
    {
        ValidateData.ValidateUniqueTypes(typeof(T1), typeof(T2), typeof(T3));
    }

    public List<KeyValuePair<Type, object?>> Data { get; } =
    [
        new KeyValuePair<Type, object?>(typeof(T1), data1),
        new KeyValuePair<Type, object?>(typeof(T2), data2),
        new KeyValuePair<Type, object?>(typeof(T3), data3)
    ];
}

public class BlockData<T1, T2, T3, T4>(T1 data1, T2 data2, T3 data3, T4 data4) : IBlockData
{
    static BlockData()
    {
        ValidateData.ValidateUniqueTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
    }

    public List<KeyValuePair<Type, object?>> Data { get; } =
    [
        new KeyValuePair<Type, object?>(typeof(T1), data1),
        new KeyValuePair<Type, object?>(typeof(T2), data2),
        new KeyValuePair<Type, object?>(typeof(T3), data3),
        new KeyValuePair<Type, object?>(typeof(T4), data4)
    ];
}

internal static class  ValidateData
{
    internal static void ValidateUniqueTypes(params Type[] types)
    {
        HashSet<Type> seenTypes = [];
        foreach(Type type in types)
        {
            if (!seenTypes.Add(type))
            {
                throw new InvalidOperationException($"Duplicate type found: {type.Name} appears multiple times. BlockData must use different types to avoid unexpected behavior by the TestCase DI Container.");
            }
        }
    }
}