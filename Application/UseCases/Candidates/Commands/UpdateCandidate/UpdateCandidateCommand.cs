using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineJobMarketplace.Application.Common.Exceptions;
using OnlineJobMarketplace.Application.Common.Interfaces;
using OnlineJobMarketplace.Application.UseCases.Students.Models;
using OnlineJobMarketplace.Domein.Entities;
using OnlineJobMarketplace.Domein.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineJobMarketplace.Application.UseCases.Candidates.Commands.UpdateCandidate;

public class UpdateCandidateCommand : IRequest<CandidateDtoWithSkills>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
    public DateTime Birthdate { get; set; }


    [RegularExpression(@"^\+998(33|9[0-9])\d{7}$", ErrorMessage = " Invalid PhoneNumber style. ")]
    public string PhoneNumber { get; set; }
    public decimal ExperienceDuration { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Education { get; set; }

    public Gender Gender { get; set; }
    public ICollection<Guid> Skills { get; set; }

}
public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, CandidateDtoWithSkills>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public UpdateCandidateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CandidateDtoWithSkills> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        Candidate candidate = await FilterIfCandidateExsists(request.Id);
        IEnumerable<Skill> skills = FilterifSkillIdsAreAvialible(request.Skills);

         candidate = _mapper.Map<Candidate>(request);
        candidate.Skills = skills.ToArray();
        candidate.ImageName = SaveImage(request.ImageFile);

        _dbContext.Candidates.Update(candidate);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CandidateDtoWithSkills>(candidate);
    }

    private IEnumerable<Skill> FilterifSkillIdsAreAvialible(ICollection<Guid> candidateIds)
    {
        foreach (var Id in candidateIds)
            yield return _dbContext.Skills.Find(Id)
                ?? throw new NotFoundException(
                    $" there is no skill with this {Id} id. ");
    }

    private async Task<Candidate> FilterIfCandidateExsists(Guid id)
    {
        Candidate? candidate = await _dbContext.Candidates.Include("Skills")
            .FirstOrDefaultAsync(x => x.Id == id);

        return candidate
            ?? throw new NotFoundException(
                " there is no candidate with this id. ");
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
