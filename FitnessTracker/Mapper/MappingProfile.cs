using FitnessTracker.Model;
using AutoMapper;
using FitnessTracker.DTO;
namespace FitnessTracker.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDTO>().ReverseMap();
			CreateMap<RunningActivity, RunningActivityDTO>().ReverseMap();
		}
	}
}
