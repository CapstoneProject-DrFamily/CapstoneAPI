using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone_API_V2.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone_API_V2.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly FamilyDoctorContext _context;

        public PrescriptionRepository(FamilyDoctorContext context)
        {
            _context = context;
        }

        public async Task<List<Prescription>> GetAllPrescription()
        {
            var result = await _context.Prescriptions
                .Include(prescription => prescription.PrescriptionDetails)
                .ThenInclude(prescriptionDetail => prescriptionDetail.Medicine).ToListAsync();

            return result;
        }

        public async Task<Prescription> GetPrescriptionByID(int prescriptionID)
        {
            var result = await _context.Prescriptions.Where(prescription => prescription.PrescriptionId == prescriptionID)
                .Include(prescription => prescription.PrescriptionDetails)
                .ThenInclude(prescriptionDetail => prescriptionDetail.Medicine).SingleOrDefaultAsync();

            return result;
        }
    }
}
