using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using TrelloManagmentSystem.DTO;

namespace TrelloManagmentSystem
{
	public class Profiles: Profile
	{
		public Profiles() {
			CreateMap<ResetPasswordDto, ResetPasswordRequest>().ReverseMap();

		}
	}
}
