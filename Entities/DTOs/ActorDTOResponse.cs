﻿using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities.DTOs
{
    public class ActorDTOResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Image { get; set; }
    }
}
