using Foundation;
using ObjCRuntime;

namespace SciChart.iOS.Binding
{
    // Match
    [Native]
    public enum SCIDataType : long
    {
        Double = 0,
        Float = 1,
        Int = 2
    }

    [Native]
    public enum SCIAxisAlignment : ulong
    {
        Auto = 0,
        Left = 1,
        Right = 2,
        Top = 3,
        Bottom = 4
    }

    public enum SCIHorizontalAnchorPoint : long
    {
        Left,
        Center,
        Right
    }

    public enum SCIVerticalAnchorPoint : long
    {
        Top,
        Center,
        Bottom
    }

    [Native]
    public enum SCIAutoRange : long
    {
        Never = 0,
        Once = 1,
        Always = 2
    }

    [Native]
    public enum SCIRangeClipMode : long
    {
        MinMax = 0,
        Min = 1,
        Max = 2
    }

}
