using Microsoft.EntityFrameworkCore;
using Patient.Domain.Entities;
using Patient.Infrastructure.DatabaseContexts;
using Patient.Infrastructure.Interfaces.Repositories;

namespace Patient.Infrastructure.Repositories;
public sealed class PatientClientRepository(PatientDbContext dbContext) : IPatientClientRepository, IDisposable
{
    private DbSet<PatientClient> DbContextSet => dbContext.Set<PatientClient>();

    public async Task<bool> AddAsync(PatientClient patientClient)
    {
        await DbContextSet.AddAsync(patientClient);

        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(PatientClient patientClient)
    {
        dbContext.Entry(patientClient.ContactInfo).State = EntityState.Modified;
        dbContext.Entry(patientClient).State = EntityState.Modified;

        return await SaveChangesAsync();
    }

    public Task<PatientClient?> GetByIdAsync(int id, bool asNoTracking)
    {
        var query = (IQueryable<PatientClient>)DbContextSet;

        if (asNoTracking)
            query = DbContextSet.AsNoTracking();

        return query.Include(p => p.ContactInfo).FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Dispose()
    {
        dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    private async Task<bool> SaveChangesAsync() =>
        await dbContext.SaveChangesAsync() > 0;
}
