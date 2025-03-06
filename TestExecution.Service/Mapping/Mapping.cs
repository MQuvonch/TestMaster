using AutoMapper;
using TestExecution.Domain.Entities;
using TestExecution.Service.DTOs.Option;
using TestExecution.Service.DTOs.Question;
using TestExecution.Service.DTOs.Test;
using TestExecution.Service.DTOs.User;
using TestExecution.Service.DTOs.UserAttempt;

namespace TestExecution.Service.Mapping;

public class Mapping : Profile
{
    public Mapping()
    {
        //User
        CreateMap<User, RegistrForCreationDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        //Test
        CreateMap<Test, TestForCreateDto>().ReverseMap();
        CreateMap<Test, TestForUpdateDto>().ReverseMap();
        CreateMap<Test, TestForResultDto>()
            .ForMember(dest=>dest.Questions,opt=>opt.MapFrom(src=>src.Questions));


        //Question
        CreateMap<Question, QuestionFromCreateDto>().ReverseMap();
        CreateMap<Question, QuestionFromUpdateDto>().ReverseMap();
        CreateMap<Question, QuestionFromResultDto>().ReverseMap();

        //Option
        CreateMap<Option, OptionFromCreateDto>().ReverseMap();
        CreateMap<Option, OptionFromUpdateDto>().ReverseMap();
        CreateMap<Option, OptionFromResultDto>().ReverseMap();

        //UserAttempt
        CreateMap<UserAttempt,UserAttemptFromCreateDto>().ReverseMap();
        CreateMap<UserAttempt,UserAttemptFromUpdateDto>().ReverseMap();
        CreateMap<UserAttempt, UserAttemptFromResultDto>().ReverseMap();

        //UserAnswer
        CreateMap<UserAnswer, UserAttemptFromCreateDto>().ReverseMap();
        CreateMap<UserAnswer, UserAttemptFromUpdateDto>().ReverseMap();
        CreateMap<UserAnswer, UserAttemptFromResultDto>().ReverseMap();
    }
}
