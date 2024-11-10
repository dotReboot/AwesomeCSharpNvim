using Microsoft.EntityFrameworkCore;
using AwesomeCSharpNvim.Models;
using AwesomeCSharpNvim.Models.DTO;
using AwesomeCSharpNvim.Models.Exceptions;
using AwesomeCSharpNvim.Persistence.Repositories.Interfaces;

namespace AwesomeCSharpNvim.Persistence.Repositories;
internal class PluginRepository : IPluginRepository
{
	private readonly ApplicationDbContext _context;
	public PluginRepository(ApplicationDbContext context)
	{
		_context = context;
	}
	public async Task AddAsync(PluginDTO pluginDTO)
	{
		var plugin = new Plugin
		{
			Name = pluginDTO.Name,
			Description = pluginDTO.Description,
			Url = pluginDTO.Url
		};
		_context.Plugins.Add(plugin);
		await _context.SaveChangesAsync();
	}
	public async Task<List<Plugin>> GetAllAsync() => await _context.Plugins.ToListAsync();
	public async Task<Plugin> GetByIdAsync(int id) => await _context.Plugins.FindAsync(id) ?? throw new NotFoundException();
	public async Task UpdateAsync(int id, PluginDTO pluginDTO)
	{
		var plugin = await GetByIdAsync(id);
		plugin.Name = pluginDTO.Name;
		plugin.Description = pluginDTO.Description;
		plugin.Url = pluginDTO.Url;
		_context.Plugins.Update(plugin);
		await _context.SaveChangesAsync();
	}
	public async Task RemoveByIdAsync(int id)
	{
		var plugin = await GetByIdAsync(id);
		_context.Plugins.Remove(plugin);
		await _context.SaveChangesAsync();
	}
}