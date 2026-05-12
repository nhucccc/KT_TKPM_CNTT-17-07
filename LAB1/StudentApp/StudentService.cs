using System.Collections.Generic;

namespace StudentApp
{
    /// <summary>
    /// Tầng Logic – xử lý nghiệp vụ, trung gian giữa UI và Repository.
    /// </summary>
    public class StudentService
    {
        private readonly StudentRepository _repo = new();

        // ── CRUD ────────────────────────────────────────────────────────────────

        public List<Student> GetAllStudents() => _repo.GetAll();

        public Student? GetStudentById(int id) => _repo.GetById(id);

        public Student AddStudent(string name, string email, string address, int age, string grade)
        {
            var student = new Student
            {
                Name    = name,
                Email   = email,
                Address = address,
                Age     = age,
                Grade   = grade
            };
            return _repo.Add(student);
        }

        public bool UpdateStudent(int id, string name, string email, string address, int age, string grade)
        {
            var updated = new Student
            {
                Name    = name,
                Email   = email,
                Address = address,
                Age     = age,
                Grade   = grade
            };
            return _repo.Update(id, updated);
        }

        public bool DeleteStudent(int id) => _repo.Delete(id);

        // ── Search ──────────────────────────────────────────────────────────────

        public List<Student> SearchByName(string keyword)    => _repo.SearchByName(keyword);
        public List<Student> SearchByAddress(string keyword) => _repo.SearchByAddress(keyword);
        public List<Student> SearchByGrade(string grade)     => _repo.SearchByGrade(grade);
    }
}
