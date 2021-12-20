using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ui.Models
{
	public class Task
	{
		public Task()
		{
			Id = 0;
			Name = "";
			ShortDescription = "";
			DetailedDescription = "";
			Solution = "";
			TableName = "";
			// AuthorId = 0;
		}

		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("shortDescription")]
		public string ShortDescription { get; set; }
		[JsonPropertyName("detailedDescription")]
		public string DetailedDescription { get; set; }
		[JsonPropertyName("solution")]
		public string Solution { get; set; }
		[JsonPropertyName("tableName")]
		public string TableName { get; set; }
		
		// public DateTime CreationTime { get; set; } = DateTime.Now;

		[JsonPropertyName("authorId")]
		public int AuthorId { get; set; } // Внешний ключ.
		[JsonPropertyName("done")]
		public bool Resolved { get; set; } // Внешний ключ.
	}
}