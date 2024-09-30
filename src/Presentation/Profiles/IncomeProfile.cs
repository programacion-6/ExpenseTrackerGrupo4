using AutoMapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Application.DTOs;

public class IncomeProfile : Profile
{
    public IncomeProfile()
    {
        CreateMap<IncomeDto, Income>();
        CreateMap<Income, IncomeResponseDto>();
    }
}
