using Places.BLL.Services.Interfaces;
using Places.DAL.Repositories;
using Places.DTO;
using AutoMapper;
using Places.Domain;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Places.BLL.Services
{
    public class WorkScheduleServices : IWorkScheduleServices
    {
       WorkScheduleRepository _repository;
        public WorkScheduleServices(WorkScheduleRepository repository)
        {
            _repository = repository;
        }
        public void SaveWorkSchedule(WorkScheduleDTO workScheduleDTO)
        {

            var newWorkSchedule = Mapper.Map<WorkSchedule>(workScheduleDTO);
            _repository.Add(newWorkSchedule);
            _repository.SaveChanges();

        }

        public void SaveWorkSchedule(List<WorkScheduleDTO> workScheduleDTOs)
        {
          
                var newWorkSchedule = Mapper.Map<List<WorkScheduleDTO> , List<WorkSchedule> >(workScheduleDTOs);
            _repository.AddRange(newWorkSchedule);

            _repository.SaveChanges();

        }

        public List<WorkScheduleDTO> GetByPlace(int placeId)
        {
            var workSchedules =_repository.GetByPlace(placeId).ToList();
            var WorkScheduleDTOs = Mapper.Map<List<WorkSchedule>, List<WorkScheduleDTO>>(workSchedules);
            return WorkScheduleDTOs;
        }

        public List<FacilitiesDTO> GetDaysOfWeek()
        {
            var daysOfWeek = new List<FacilitiesDTO>();
            daysOfWeek.AddRange (new List<FacilitiesDTO>() { 
                new FacilitiesDTO() { Id = 1 , Name = "Monday" },
                new FacilitiesDTO() { Id = 2, Name = "Tuesday" },
                new FacilitiesDTO() { Id = 3, Name = "Wendnesday" },
                new FacilitiesDTO() { Id = 4, Name = "Thirsday" },
                new FacilitiesDTO() { Id = 5, Name = "Friday" },
                new FacilitiesDTO() { Id = 6, Name = "Sutarday" },
                new FacilitiesDTO() { Id = 7, Name = "Sunday" },

    });
            return daysOfWeek;
        }

        public WorkScheduleDTO GetById(int  id)
        {
            var workSchedule = Mapper.Map<WorkScheduleDTO>(_repository.GetById(id));
            return workSchedule;
        }

        public void UpdateWorkSchedule(WorkScheduleDTO workScheduleDTO)
        {
            var newWorkSchedule = Mapper.Map<WorkSchedule>(workScheduleDTO);

           _repository.Update(newWorkSchedule);
            _repository.SaveChanges();
        }

        public void DeleteWorkSchedule(int id)
        {         
            _repository.Delete(id);
            _repository.SaveChanges();


        }
    }
}
