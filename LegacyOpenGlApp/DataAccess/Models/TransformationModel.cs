using LegacyOpenGlApp.Primitives;

namespace LegacyOpenGlApp.DataAccess.Models
{
    public class TransformationModel
    {
	    public Transform Transform { get; set; }

	    public float X { get; set; }

	    public float Y { get; set; }

	    public float Z { get; set; }

	    public override string ToString() => $"{Transform} ( X: {X}{_symbol}, Y: {Y}{_symbol}, Z: {Z}{_symbol} )";

	    private char? _symbol => Transform == Transform.Rotate ? '\xB0' : (char?) null;

    }
}
