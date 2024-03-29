﻿using MediatR;
using System;

namespace Meetups.Aplication.Meetups.Commands.UpdateMeetup
{
    //everything needed to update the meetup
    public class UpdateMeetupCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Speaker { get; set; }
        public string Place { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime MeetupDate { get; set; }
        public DateTime EditTime { get; set; }
    }
}
