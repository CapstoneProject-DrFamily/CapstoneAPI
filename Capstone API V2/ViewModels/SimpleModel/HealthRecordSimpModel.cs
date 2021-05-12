using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels.SimpleModel
{
    public class HealthRecordSimpModel
    {
        public int Id { get; set; }
        public string ConditionAtBirth { get; set; }
        public double? BirthWeight { get; set; }
        public double? BirthHeight { get; set; }
        public string BirthDefects { get; set; }
        public string OtherDefects { get; set; }
        public string MedicineAllergy { get; set; }
        public string ChemicalAllergy { get; set; }
        public string FoodAllergy { get; set; }
        public string OtherAllergy { get; set; }
        public string Disease { get; set; }
        public string Cancer { get; set; }
        public string Tuberculosis { get; set; }
        public string OtherDiseases { get; set; }
        public string Hearing { get; set; }
        public string Eyesight { get; set; }
        public string Hand { get; set; }
        public string Leg { get; set; }
        public string Scoliosis { get; set; }
        public string CleftLip { get; set; }
        public string OtherDisabilities { get; set; }
        public string SurgeryHistory { get; set; }
        public string MedicineAllergyFamily { get; set; }
        public string ChemicalAllergyFamily { get; set; }
        public string FoodAllergyFamily { get; set; }
        public string OtherAllergyFamily { get; set; }
        public string DiseaseFamily { get; set; }
        public string CancerFamily { get; set; }
        public string TuberculosisFamily { get; set; }
        public string OtherDiseasesFamily { get; set; }
        public string Other { get; set; }
        public string SmokingFrequency { get; set; }
        public string DrinkingFrequency { get; set; }
        public string DrugFrequency { get; set; }
        public string ActivityFrequency { get; set; }
        public string ExposureElement { get; set; }
        public string ContactTime { get; set; }
        public string ToiletType { get; set; }
        public string OtherRisks { get; set; }
        public int PatientId { get; set; }
        public bool? Disable { get; set; }
    }
}
