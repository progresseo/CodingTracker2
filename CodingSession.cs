﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker2
{
    class CodingSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Duration { get; set; }

    }
}
