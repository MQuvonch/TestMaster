﻿using TestExecution.Service.DTOs.UserAttempt;

namespace TestExecution.Service.Interfaces;

public interface IUserAttemptService
{
    Task<IEnumerable<UserAttemptFromResultDto>> GetAllAsync(Guid TestId);
    Task<UserAttemptFromResultDto> GetByIdAsync(Guid Id);
    Task<UserAttemptFromResultDto> FinishTest(UserAttemptFromCreateDto dto);
    Task<UserAttemptFromResultDto> UpdateAsync(Guid Id, UserAttemptFromUpdateDto dto);
    Task<bool> DeleteAsync(Guid Id);

}
