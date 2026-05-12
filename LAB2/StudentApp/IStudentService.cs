using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp
{
    /// <summary>
    /// Interface định nghĩa các nghiệp vụ của tầng Logic.
    /// </summary>
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(string id);
        Task AddAsync(string name, string email, string address, int age, string grade);
        Task<bool> UpdateAsync(string id, string name, string email, string address, int age, string grade);
        Task<bool> DeleteAsync(string id);

        Task<List<Student>> SearchByNameAsync(string keyword);
        Task<List<Student>> SearchByAddressAsync(string keyword);
        Task<List<Student>> SearchByGradeAsync(string grade);
    }
}
