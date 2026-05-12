using System;

namespace StudentApp
{
    /// <summary>
    /// Đại diện cho một sinh viên trong hệ thống.
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Age { get; set; }

        // Grade: A, B, C, D, F
        public string Grade { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[{Id}] {Name} | {Age} tuổi | {Email} | {Address} | Xếp loại: {Grade}";
        }

        /// <summary>
        /// Chuyển đối tượng thành chuỗi để lưu file (dùng dấu | làm phân cách).
        /// </summary>
        public string ToFileString()
        {
            return $"{Id}|{Name}|{Email}|{Address}|{Age}|{Grade}";
        }

        /// <summary>
        /// Tạo đối tượng Student từ chuỗi đọc trong file.
        /// </summary>
        public static Student FromFileString(string line)
        {
            var parts = line.Split('|');
            if (parts.Length < 6)
                throw new FormatException($"Dòng dữ liệu không hợp lệ: {line}");

            return new Student
            {
                Id      = int.Parse(parts[0]),
                Name    = parts[1],
                Email   = parts[2],
                Address = parts[3],
                Age     = int.Parse(parts[4]),
                Grade   = parts[5]
            };
        }
    }
}
