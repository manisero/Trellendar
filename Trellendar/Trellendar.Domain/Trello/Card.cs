﻿using System;

namespace Trellendar.Domain.Trello
{
    public class Card
    {
        public string Id { get; set; }

        public string IdList { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public DateTime? Due { get; set; }
    }
}
