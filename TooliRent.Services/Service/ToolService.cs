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
        public async Task<ToolDetailDTO?> GetByIdAsync(int id)
        {
            var tool = await _toolRepository.GetByIdAsync(id);

            if (tool == null)
                return null;

            return _mapper.Map<ToolDetailDTO>(tool);
        }
        public async Task<List<ToolSummaryDTO>> GetAllAsync()
        {
            var tools = await _toolRepository.GetAllAsync();
            return _mapper.Map<List<ToolSummaryDTO>>(tools);
        }

        public async Task<List<ToolSummaryDTO>> GetAvailableAsync()
        {
            var tools = await _toolRepository.GetAvailableAsync();
            return _mapper.Map<List<ToolSummaryDTO>>(tools);
        }

        public async Task<List<ToolSummaryDTO>> GetByCategoryAsync(int categoryId)
        {
            var tools = await _toolRepository.GetByCategoryAsync(categoryId);
            return _mapper.Map<List<ToolSummaryDTO>>(tools);
        }

        public async Task<List<ToolSelectDTO>> GetForSelectAsync()
        {
            var tools = await _toolRepository.GetAvailableAsync();
            return _mapper.Map<List<ToolSelectDTO>>(tools);
        }

        public async Task<ToolDetailDTO> CreateAsync(CreateToolDto dto)
        {
            var tool = _mapper.Map<Tool>(dto);

            var createdTool = await _toolRepository.AddAsync(tool);

            // Hämta med Category inkluderat för korrekt mapping
            var toolWithCategory = await _toolRepository.GetByIdAsync(createdTool.Id);

            return _mapper.Map<ToolDetailDTO>(toolWithCategory);
        }

        public async Task UpdateAsync(int id, UpdateToolDTO dto)
        {
            var existingTool = await _toolRepository.GetByIdAsync(id);

            if (existingTool == null)
                throw new ArgumentException("Tool not found");

            _mapper.Map(dto, existingTool);
            await _toolRepository.UpdateAsync(existingTool);
        }

        public async Task UpdateStatusAsync(int id, UpdateToolStatusDTO dto)
        {
            var existingTool = await _toolRepository.GetByIdAsync(id);

            if (existingTool == null)
                throw new ArgumentException("Tool not found");

            _mapper.Map(dto, existingTool);
            await _toolRepository.UpdateAsync(existingTool);
        }

        public async Task DeleteAsync(int id)
        {
            var existingTool = await _toolRepository.GetByIdAsync(id);

            if (existingTool == null)
                throw new ArgumentException("Tool not found");

            await _toolRepository.DeleteAsync(id);
        }
    }
}
