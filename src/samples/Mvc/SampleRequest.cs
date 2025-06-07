using System.ComponentModel.DataAnnotations;

namespace Mvc;

public class SampleRequest
{
    public string String { get; set; } = null!;
    public bool Bool { get; set; }
    public byte Byte { get; set; }
    public sbyte Sbyte { get; set; }
    public char Char { get; set; }
    public decimal Decimal { get; set; }
    public double Double { get; set; }
    public float Float { get; set; }
    public int Int { get; set; }
    public uint Uint { get; set; }
    public long Long { get; set; }
    public ulong Ulong { get; set; }
    public short Short { get; set; }
    public ushort UShort { get; set; }
    public DateTime DateTime { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public DateOnly DateOnly { get; set; }
    public TimeOnly TimeOnly { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public object Object { get; set; } = null!;
    public dynamic Dynamic { get; set; } = null!;
    public int[] Array { get; set; } = null!;
    public List<int> List { get; set; } = null!;
    public IEnumerable<int> IEnumerable { get; set; } = null!;
    public Dictionary<int, string> Dictionary { get; set; } = null!;
    public Guid Guid { get; set; }
    [EmailAddress]
    public string Email { get; set; } = null!;
}