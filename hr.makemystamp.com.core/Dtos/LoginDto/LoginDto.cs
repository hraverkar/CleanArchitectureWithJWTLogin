﻿namespace hr.makemystamp.com.core.Dtos.LoginDto
{
    public record LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
