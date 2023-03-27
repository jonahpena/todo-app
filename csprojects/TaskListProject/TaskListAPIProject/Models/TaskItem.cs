﻿namespace TaskListAPIProject.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public bool Completed { get; set; }
    }
}
