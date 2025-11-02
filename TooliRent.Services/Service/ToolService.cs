using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRent.Core.Interfaces.IService;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.DTO_s.ToolsDTOs;
using TooliRent.Models;
using AutoMapper;

namespace TooliRent.Services.Service
{
    public class ToolService : IToolService
    {
        public readonly IToolRepository _toolRepository;
        private readonly IMapper _mapper;

        public ToolService(IToolRepository toolRepository, IMapper mapper)
        {
            _toolRepository = toolRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToolDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var tools = await _toolRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ToolDTO>>(tools);
        }

        public async Task<ToolDTO?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var tool = await _toolRepository.GetByIdAsync(id);
            if (tool == null)
                return null;

            return _mapper.Map<ToolDTO>(tool);
        }

        public async Task<ToolDTO> CreateAsync(CreateToolDTO create, CancellationToken ct = default)
        {
            var tool = _mapper.Map<Tool>(create);
            tool.IsAvailable = true; // tool is avaiablöel when created

            var createdTool = await _toolRepository.CreateAsync(tool);
            return _mapper.Map<ToolDTO>(createdTool);
        }

        public async Task<ToolDTO?> UpdateAsync(int id, UpdateToolDTO update, CancellationToken ct = default)
        {
            var existingTool = await _toolRepository.GetByIdAsync(id);
            if (existingTool == null)
                return null;

            _mapper.Map(update, existingTool);
            await _toolRepository.UpdateAsync(existingTool);

            return _mapper.Map<ToolDTO>(existingTool);
        }
        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var tool = await _toolRepository.GetByIdAsync(id);
            if (tool == null)
                throw new KeyNotFoundException($"Tool with ID {id} not found.");

            await _toolRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ToolDTO>> GetAvailableAsync(CancellationToken ct = default)
        {
            var tools = await _toolRepository.GetAvailableAsync();
            return _mapper.Map<IEnumerable<ToolDTO>>(tools);
        }

        public async Task<IEnumerable<ToolDTO>> GetByCategoryAsync(int categoryId, CancellationToken ct = default)
        {
            var tools = await _toolRepository.GetByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<ToolDTO>>(tools);
        }
    }
}
