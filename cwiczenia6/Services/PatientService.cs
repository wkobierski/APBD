using cwiczenia6.Data;
using cwiczenia6.DTOs;
using cwiczenia6.Entities;
using cwiczenia6.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace cwiczenia6.Services;

public class PatientService : IPatientService
{
    private readonly HospitalContext _context;

    public PatientService(HospitalContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetPatientDto>> GetPatients(string? search)
    {
        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var pattern = $"%{search}%";
            query = query.Where(p =>
                EF.Functions.Like(p.FirstName, pattern) ||
                EF.Functions.Like(p.LastName, pattern));
        }

        return await query
            .Select(p => new GetPatientDto
            {
                Pesel = p.Pesel,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Sex = p.Sex == 1 ? "Male" : "Female",
                Admissions = p.Admissions.Select(a => new GetAdmissionDto
                {
                    Id = a.Id,
                    AdmissionDate = a.AdmissionDate,
                    DischargeDate = a.DischargeDate,
                    Ward = new GetWardDto
                    {
                        Id = a.Ward.Id,
                        Name = a.Ward.Name,
                        Description = a.Ward.Description
                    }
                }).ToList(),
                BedAssignments = p.BedAssignments.Select(ba => new GetBedAssignmentDto
                {
                    Id = ba.Id,
                    From = ba.From,
                    To = ba.To,
                    Bed = new GetBedDto
                    {
                        Id = ba.Bed.Id,
                        BedType = new GetBedTypeDto
                        {
                            Id = ba.Bed.BedType.Id,
                            Name = ba.Bed.BedType.Name,
                            Description = ba.Bed.BedType.Description
                        },
                        Room = new GetRoomDto
                        {
                            Id = ba.Bed.Room.Id,
                            HasTv = ba.Bed.Room.HasTv == 1,
                            Ward = new GetWardDto
                            {
                                Id = ba.Bed.Room.Ward.Id,
                                Name = ba.Bed.Room.Ward.Name,
                                Description = ba.Bed.Room.Ward.Description
                            }
                        }
                    }
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<GetBedAssignmentDto> AssignBed(string pesel, NewBedAssignmentDto dto)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Pesel == pesel);
        if (patient is null)
            throw new NotFoundException($"Patient with PESEL '{pesel}' was not found.");

        var ward = await _context.Wards.FirstOrDefaultAsync(w => w.Name == dto.Ward);
        if (ward is null)
            throw new NotFoundException($"Ward '{dto.Ward}' was not found.");

        var bedType = await _context.BedTypes.FirstOrDefaultAsync(bt => bt.Name == dto.BedType);
        if (bedType is null)
            throw new NotFoundException($"Bed type '{dto.BedType}' was not found.");

        var candidateBeds = await _context.Beds
            .Include(b => b.BedType)
            .Include(b => b.Room).ThenInclude(r => r.Ward)
            .Include(b => b.BedAssignments)
            .Where(b => b.BedTypeId == bedType.Id && b.Room.WardId == ward.Id)
            .ToListAsync();

        if (candidateBeds.Count == 0)
            throw new NotFoundException(
                $"There are no beds of type '{dto.BedType}' in ward '{dto.Ward}'.");

        var requestedTo = dto.To ?? DateTime.MaxValue;
        var freeBed = candidateBeds.FirstOrDefault(b =>
            b.BedAssignments.All(a =>
                a.From >= requestedTo || (a.To ?? DateTime.MaxValue) <= dto.From));

        if (freeBed is null)
            throw new NotFoundException(
                $"No free bed of type '{dto.BedType}' in ward '{dto.Ward}' is available for the requested period.");

        var assignment = new BedAssignment
        {
            PatientPesel = pesel,
            BedId = freeBed.Id,
            From = dto.From,
            To = dto.To
        };

        _context.BedAssignments.Add(assignment);
        await _context.SaveChangesAsync();

        return new GetBedAssignmentDto
        {
            Id = assignment.Id,
            From = assignment.From,
            To = assignment.To,
            Bed = new GetBedDto
            {
                Id = freeBed.Id,
                BedType = new GetBedTypeDto
                {
                    Id = freeBed.BedType.Id,
                    Name = freeBed.BedType.Name,
                    Description = freeBed.BedType.Description
                },
                Room = new GetRoomDto
                {
                    Id = freeBed.Room.Id,
                    HasTv = freeBed.Room.HasTv == 1,
                    Ward = new GetWardDto
                    {
                        Id = freeBed.Room.Ward.Id,
                        Name = freeBed.Room.Ward.Name,
                        Description = freeBed.Room.Ward.Description
                    }
                }
            }
        };
    }
}
