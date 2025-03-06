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
        private readonly IRepository<Question> _questionRepository;
        private readonly IMapper _mapper;

        public OptionService(IRepository<Option> optionRepository,
                             IMapper mapper,
                             IRepository<Question> questionRepository)
        {
            _optionRepository = optionRepository;
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        public async Task<OptionFromResultDto> CreateAsync(OptionFromCreateDto dto)
        {
            var question = await _questionRepository.GetByIdAsync(dto.QuestionId);
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
            var options =  _optionRepository.GetAll().Select(o=>new OptionFromResultDto
            {
                QuestionId = o.QuestionId,
                Text = o.Text,
                IsCorrect = o.IsCorrect,
            });
            if (options is null)
               ;

            return options ?? throw new TestCustomException(404, "variantlar mavjud emas ");
        }

        public async Task<OptionFromResultDto> GetByIdAsync(Guid Id)
        {
            var option = await _optionRepository.GetByIdAsync(Id);
            if (option is null)
                throw new TestCustomException(404, "variantlar topilmadi ");

            var optionDto = new OptionFromResultDto
            {
                QuestionId = option.QuestionId,
                Text = option.Text,
                IsCorrect = option.IsCorrect,
            };

            return optionDto;
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
