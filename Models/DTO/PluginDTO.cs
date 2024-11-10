namespace AwesomeCSharpNvim.Models.DTO;
public record PluginDTO
{
	public required string Name { get; set; }
	public string? Description { get; set; }
	public required string Url { get; set; }
}