using Places.DTO;
using System.Collections.Generic;

namespace Places.BLL.Services.Interfaces
{
    public interface IWorkScheduleServices
    {
        void SaveWorkSchedule(WorkScheduleDTO workScheduleDTO);
        void SaveWorkSchedule(List<WorkScheduleDTO> workScheduleDTO);
        void  UpdateWorkSchedule(WorkScheduleDTO workScheduleDTO);
        void DeleteWorkSchedule(int id);
        WorkScheduleDTO GetById(int  id);
        List<WorkScheduleDTO> GetByPlace(int placeId);
        List<FacilitiesDTO> GetDaysOfWeek();
    }
}