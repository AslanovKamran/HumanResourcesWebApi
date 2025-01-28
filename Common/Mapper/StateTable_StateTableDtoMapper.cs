using HumanResourcesWebApi.Models.Domain;
using HumanResourcesWebApi.Models.DTO;

namespace HumanResourcesWebApi.Common.Mapper
{
    public static class StateTable_StateTableDtoMapper
    {
        public static StateTableInfoDTO MapDto(StateTable obj)
        {
            var result = new StateTableInfoDTO();

            result.Id = obj.Id;
            result.OrganizationStructureFullName = obj.OrganizationStructure.FullName;
            result.Name = obj.Name;
            result.Degree = obj.Degree;
            result.UnitCount = obj.UnitCount;
            result.MonthlySalaryFrom = obj.MonthlySalaryFrom;
            result.HourlySalary = obj.HourlySalary;
            result.MonthlySalaryExtra = obj.MonthlySalaryExtra;
            result.OccupiedPostCount = obj.OccupiedPostCount;
            result.DocumentNumber = obj.DocumentNumber;
            result.DocumentDate = obj.DocumentDate;
            result.StateWorkType = obj.StateWorkType.Type;
            result.HarmfulnessCoefficient = obj.HarmfulnessCoefficient;
            result.WorkHours = obj.WorkHours;
            result.WorkHoursSaturday = obj.WorkHoursSaturday;
            result.TabelPosition = obj.TabelPosition;
            result.TabelPriority = obj.TabelPriority;
            result.IsCanceled = obj.IsCanceled;

            return result;
        }
    }
}
