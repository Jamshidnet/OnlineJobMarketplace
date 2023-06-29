using AutoMapper;
using JobPaymentSystem.Application.UseCases.Jobs.Commands.CreateJob;
using OnlineJobMarketplace.Application.UseCases.Companies.Models;
using OnlineJobMarketplace.Application.UseCases.Jobs.Models;
using OnlineJobMarketplace.Application.UseCases.Skills.Commands.CreateSkill.CreateSkill;
using OnlineJobMarketplace.Application.UseCases.Skills.Models;
using OnlineJobMarketplace.Application.UseCases.Students.Models;
using OnlineJobMarketplace.Application.UseCases.Submissions.Commands.CreateSubmission;
using OnlineJobMarketplace.Application.UseCases.Submissions.Models;
using OnlineJobMarketplace.Domein.Entities;
using OnlineSkillMarketplace.Application.UseCases.Skills.Commands.CreateSkill;
using OnlineSkillMarketplace.Application.UseCases.Skills.Commands.UpdateSkill;

namespace OnlineJobMarketplace.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company,CompanyDto >().ReverseMap();
        CreateMap<Company,CompanyWithJobsDto >().ReverseMap();
        CreateMap<CreateCompanyCommand,Company >().ReverseMap();

        CreateMap<Skill,SkillDto >().ReverseMap();
        CreateMap<CreateSkillCommand,Skill >().ReverseMap();
        CreateMap<UpdateSkillCommand, Skill>();

        CreateMap<Candidate,CandidateDto >().ReverseMap();
        CreateMap<Candidate,CandidateDtoWithSkills >().ReverseMap();
        CreateMap<CreateCandidateCommand,Candidate >().ReverseMap();

        CreateMap<Submission,SubmissionDto >().ReverseMap();
        CreateMap<CreateSubmissionCommand,Submission >().ReverseMap();

        CreateMap<Job,JobDto >().ReverseMap();
        CreateMap<Job,JobWithRequiredSkillsDto >().ReverseMap();
        CreateMap<CreateJobCommand,Job >().ReverseMap();

    }
}

