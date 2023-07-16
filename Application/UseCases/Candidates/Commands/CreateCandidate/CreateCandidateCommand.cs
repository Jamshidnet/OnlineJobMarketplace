using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Students.Models;
using OnlineJobMarketplace.Domein.Entities;
using OnlineJobMarketplace.Domein.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineJobMarketplace.Application.UseCases.Skills.Commands.CreateSkill.CreateSkill;

public record CreateCandidateCommand : IRequest<CandidateDtoWithSkills>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
     public DateTime Birthdate { get; set; }


    [RegularExpression(@"^\+998(33|9[0-9])\d{7}$", ErrorMessage = " Invalid PhoneNumber style. ")]
    public string PhoneNumber { get; set; }
    public decimal ExperienceDuration { get; set; }

    public string Education { get; set; }

    public IFormFile? ImageFile { get; set; }
    public Gender Gender { get; set; }
    public ICollection<Guid> Skills { get; set; }
}
public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, CandidateDtoWithSkills>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public CreateCandidateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CandidateDtoWithSkills> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {

        FilterIfCandidateExsists(request.PhoneNumber);
        ICollection<Skill>? skills = FilterIfAllSkillsExsist(request.Skills);

        Candidate candidate = _mapper.Map<Candidate>(request);
        candidate.Skills = skills;
        candidate.ImageName = SaveImage(request.ImageFile);

        await _dbContext.Candidates.AddAsync(candidate);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CandidateDtoWithSkills>(candidate);
    }

    private ICollection<Skill> FilterIfAllSkillsExsist(ICollection<Guid> skills)
    {
        List<Skill> maybeSkills = new();
        foreach (Guid Id in skills)
        {
            var skill = _dbContext.Skills.FirstOrDefault(c => c.Id == Id)
                ?? throw new NotFoundException($" There is no skill with this {Id} id. ");
            maybeSkills.Add(skill);
        }

        return maybeSkills;
    }

    private void FilterIfCandidateExsists(string? PhoneNumber)
    {
        Candidate? candidate = _dbContext.Candidates.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);

        if (candidate is not null)
        {
            throw new AlreadyExsistsException(
                " There is a  candidate with this phonenumber. Phonenumber should be unique.  ");
        }
    }

    private string SaveImage(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            // Image fayli mavjud emas yoki bo'sh
            return string.Empty;
        }

        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
        string filePath = Path.Combine("images", uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            imageFile.CopyTo(fileStream);
        }

        return uniqueFileName;
    }
}
