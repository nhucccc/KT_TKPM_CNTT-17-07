using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentApp
{
    /// <summary>
    /// Tầng Data – chịu trách nhiệm đọc/ghi file và quản lý danh sách sinh viên trong bộ nhớ.
    /// </summary>
    public class StudentRepository
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1;
        private readonly string _filePath = "students.txt";

        public StudentRepository()
        {
            LoadFromFile();
        }

        // ── CRUD ────────────────────────────────────────────────────────────────

        public List<Student> GetAll() => _students;

        public Student? GetById(int id) =>
            _students.FirstOrDefault(s => s.Id == id);

        public Student Add(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);
            SaveToFile();
            return student;
        }

        public bool Update(int id, Student updated)
        {
            var existing = _students.FirstOrDefault(s => s.Id == id);
            if (existing == null) return false;

            existing.Name    = updated.Name;
            existing.Email   = updated.Email;
            existing.Address = updated.Address;
            existing.Age     = updated.Age;
            existing.Grade   = updated.Grade;

            SaveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            var item = _students.FirstOrDefault(s => s.Id == id);
            if (item == null) return false;

            _students.Remove(item);
            SaveToFile();
            return true;
        }

        // ── Search ──────────────────────────────────────────────────────────────

        public List<Student> SearchByName(string keyword) =>
            _students.Where(s => s.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Student> SearchByAddress(string keyword) =>
            _students.Where(s => s.Address.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Student> SearchByGrade(string grade) =>
            _students.Where(s => s.Grade.Equals(grade, StringComparison.OrdinalIgnoreCase)).ToList();

        // ── File I/O ─────────────────────────────────────────────────────────────

        private void LoadFromFile()
        {
            if (!File.Exists(_filePath)) return;

            foreach (var line in File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var student = Student.FromFileString(line);
                    _students.Add(student);
                    if (student.Id >= _nextId) _nextId = student.Id + 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Cảnh báo] Bỏ qua dòng lỗi: {ex.Message}");
                }
            }
        }

        private void SaveToFile()
        {
            File.WriteAllLines(_filePath, _students.Select(s => s.ToFileString()));
        }
    }
}
