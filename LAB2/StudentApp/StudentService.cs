using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp
{
    /// <summary>
    /// Tầng Logic – xử lý nghiệp vụ, trung gian giữa UI và Repository.
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _repo;

        public StudentService(StudentRepository repo)
        {
            _repo = repo;
        }

        // ── CRUD ────────────────────────────────────────────────────────────────

        public async Task<List<Student>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Student?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task AddAsync(string name, string email, string address, int age, string grade)
        {
            var student = new Student
            {
                Name    = name,
                Email   = email,
                Address = address,
                Age     = age,
                Grade   = grade
            };
            await _repo.AddAsync(student);
        }

        public async Task<bool> UpdateAsync(string id, string name, string email, string address, int age, string grade)
        {
            var updated = new Student
            {
                Name    = name,
                Email   = email,
                Address = address,
                Age     = age,
                Grade   = grade
            };
            return await _repo.UpdateAsync(id, updated);
        }

        public async Task<bool> DeleteAsync(string id) => await _repo.DeleteAsync(id);

        // ── Search ──────────────────────────────────────────────────────────────

        public async Task<List<Student>> SearchByNameAsync(string keyword)    => await _repo.SearchByNameAsync(keyword);
        public async Task<List<Student>> SearchByAddressAsync(string keyword) => await _repo.SearchByAddressAsync(keyword);
        public async Task<List<Student>> SearchByGradeAsync(string grade)     => await _repo.SearchByGradeAsync(grade);
    }
}
