using Microsoft.EntityFrameworkCore;
using Patient.Domain.Entities;
using Patient.Domain.Interfaces.Repositories;
using Patient.Infrastructure.DatabaseContexts;

namespace Patient.Infrastructure.Repositories;

public sealed class PatientClientRepository(
    PatientDbContext dbContext)
    : IPatientClientRepository,
    IPatientClientRepositoryFacade,
    IDisposable
{
    private DbSet<PatientClient> DbContextSet => dbContext.Set<PatientClient>();

    public async Task<bool> AddAsync(PatientClient patientClient, CancellationToken cancellationToken)
    {
        await DbContextSet.AddAsync(patientClient, cancellationToken);

        return await SaveChangesAsync(cancellationToken);
    }

    public Task<List<PatientClient>> GetAllAsync(CancellationToken cancellationToken) =>
        DbContextSet.AsNoTracking().Include(p => p.ContactInfo).ToListAsync(cancellationToken);

    public Task<PatientClient?> GetByIdAsync(int id, bool asNoTracking, CancellationToken cancellationToken)
    {
        var query = (IQueryable<PatientClient>)DbContextSet;

        if (asNoTracking)
        {
            query = DbContextSet.AsNoTracking();
        }

        return query.Include(p => p.ContactInfo).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task<string?> GetEmailByIdAsync(int id, CancellationToken cancellationToken) =>
        DbContextSet.AsNoTracking()
                    .Where(p => p.Id == id)
                    .Select(p => p.ContactInfo.Email)
                    .FirstOrDefaultAsync(cancellationToken);

    public Task<bool> UpdateAsync(PatientClient patientClient, CancellationToken cancellationToken)
    {
        dbContext.Entry(patientClient.ContactInfo).State = EntityState.Modified;
        dbContext.Entry(patientClient).State = EntityState.Modified;

        return SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        dbContext.Dispose();

        GC.SuppressFinalize(this);
    }

    private async Task<bool> SaveChangesAsync(CancellationToken cancellationToken) =>
        await dbContext.SaveChangesAsync(cancellationToken) > 0;
}
