﻿namespace TrelloManagmentSystem.DTO
{
	public class ResetPasswordDto
	{
		public string Email { get; set; }
		public string NewPassword { get; set; }
		public string ResetCode { get; set; }
	}
}
