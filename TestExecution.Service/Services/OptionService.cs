using AutoMapper;
using TestExecution.Data.IRepositories;
using TestExecution.Domain.Entities;
using TestExecution.Service.DTOs.Option;
using TestExecution.Service.Exceptions;
using TestExecution.Service.Interfaces;

namespace TestExecution.Service.Services
{
    public class OptionService : IOptionService
    {
        private readonly IRepository<Option> _optionRepository;
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public OptionService(IRepository<Option> optionRepository,
                             IMapper mapper,
                             IQuestionService questionService)
        {
            _optionRepository = optionRepository;
            _mapper = mapper;
            _questionService = questionService;
        }

        public async Task<OptionFromResultDto> CreateAsync(OptionFromCreateDto dto)
        {
            var question = _questionService.GetByIdAsync(dto.QuestionId);
            if (question is null)
                throw new TestCustomException(404, "Savol mavjud emas");

            var optionMap = _mapper.Map<Option>(dto);
            var createOption = await _optionRepository.CreateAsync(optionMap);
            return _mapper.Map<OptionFromResultDto>(createOption);
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var option = await _optionRepository.GetByIdAsync(Id);
            if (option is null)
                throw new TestCustomException(404, "variantlar topilmadi ");

            var deleteOption = await _optionRepository.DeleteAsync(Id);
            return true;
        }

        public async Task<IEnumerable<OptionFromResultDto>> GetAllAsync()
        {
            var options = await _optionRepository.GetAllAsync();
            if (options is null)
                throw new TestCustomException(404, "variantlar mavjud emas ");

            return _mapper.Map<IEnumerable<OptionFromResultDto>>(options);
        }

        public async Task<OptionFromResultDto> GetByIdAsync(Guid Id)
        {
            var option = await _optionRepository.GetByIdAsync(Id);
            if (option is null)
                throw new TestCustomException(404, "variantlar topilmadi ");

            return _mapper.Map<OptionFromResultDto>(option);
        }

        public async Task<OptionFromResultDto> UpdateAsync(Guid Id, OptionFromUpdateDto dto)
        {
            var option = await _optionRepository.GetByIdAsync(Id);
            if (option is null)
                throw new TestCustomException(404, "variantlar topilmadi ");

            var mapUpdate = _mapper.Map(dto, option);
            var updateOption = await _optionRepository.UpdateAsync(mapUpdate);

            return _mapper.Map<OptionFromResultDto>(updateOption);
        }
    }
}
