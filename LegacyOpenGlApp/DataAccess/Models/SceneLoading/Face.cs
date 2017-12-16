using System.Collections.Generic;

namespace LegacyOpenGlApp.DataAccess.Models.SceneLoading
{
	public class Face
	{
		public IList<Indice> Indices { get; set; }

		public int IndexCount => Indices.Count;
	}
}