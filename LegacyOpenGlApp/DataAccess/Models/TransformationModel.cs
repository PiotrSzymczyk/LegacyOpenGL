using System;
using LegacyOpenGlApp.Primitives;

namespace LegacyOpenGlApp.DataAccess.Models
{
    public class TransformationModel
    {
	    public Transform Transform { get; set; }

	    public float X { get; set; }

	    public float Y { get; set; }

	    public float Z { get; set; }

	    public override string ToString() => string.Format("{0} ( X: {1}{4}, Y: {2}{4}, Z: {3}{4} )", Transform, X, Y, Z, GetSymbol());

	    private char? GetSymbol()
	    {
		    switch (Transform)
		    {
			    case Transform.Rotate: return '\xB0';
			    case Transform.Scale: return '\x25';
			    default: return null;
		    }
	    }
    }
}
