using AwesomeCSharpNvim.Models;
using AwesomeCSharpNvim.Models.DTO;

namespace AwesomeCSharpNvim.Persistence.Repositories.Interfaces;
public interface IPluginRepository
{
	Task AddAsync(PluginDTO plugin);
	Task<List<Plugin>> GetAllAsync();
	Task<Plugin> GetByIdAsync(int id);
	Task UpdateAsync(int id, PluginDTO plugin);
	Task RemoveByIdAsync(int id);
}