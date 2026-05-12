using System;

namespace TodoAppV2
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return $"[{(IsCompleted ? "x" : " ")}] {Id} : {Title}";
        }
    }
}
