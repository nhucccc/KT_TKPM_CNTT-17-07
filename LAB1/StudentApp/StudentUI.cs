using System;
using System.Collections.Generic;

namespace StudentApp
{
    /// <summary>
    /// Tầng UI – hiển thị menu và tương tác với người dùng qua Console.
    /// </summary>
    public class StudentUI
    {
        private readonly StudentService _service = new();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowHeader("QUẢN LÝ SINH VIÊN");
                ShowMainMenu();

                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1": ShowAllStudents(); break;
                    case "2": AddStudent(); break;
                    case "3": EditStudent(); break;
                    case "4": DeleteStudent(); break;
                    case "5": SearchMenu(); break;
                    case "0": Console.WriteLine("Tạm biệt!"); return;
                    default:
                        PrintError("Lựa chọn không hợp lệ!");
                        break;
                }

                Pause();
            }
        }

        // ── Menu ─────────────────────────────────────────────────────────────────

        private void ShowMainMenu()
        {
            Console.WriteLine("1. Hiển thị danh sách sinh viên");
            Console.WriteLine("2. Thêm sinh viên");
            Console.WriteLine("3. Sửa thông tin sinh viên");
            Console.WriteLine("4. Xoá sinh viên");
            Console.WriteLine("5. Tìm kiếm sinh viên");
            Console.WriteLine("0. Thoát");
            Console.Write("\nChọn chức năng: ");
        }

        // ── Display ──────────────────────────────────────────────────────────────

        private void ShowAllStudents()
        {
            var list = _service.GetAllStudents();
            ShowHeader("DANH SÁCH SINH VIÊN");

            if (list.Count == 0)
            {
                Console.WriteLine("Chưa có sinh viên nào.");
                return;
            }

            PrintTableHeader();
            foreach (var s in list)
                PrintTableRow(s);
            PrintTableFooter();

            Console.WriteLine($"\nTổng số: {list.Count} sinh viên.");
        }

        private void ShowStudentList(List<Student> list, string title)
        {
            ShowHeader(title);
            if (list.Count == 0)
            {
                Console.WriteLine("Không tìm thấy sinh viên nào.");
                return;
            }

            PrintTableHeader();
            foreach (var s in list)
                PrintTableRow(s);
            PrintTableFooter();

            Console.WriteLine($"\nTìm thấy: {list.Count} sinh viên.");
        }

        // ── CRUD Actions ─────────────────────────────────────────────────────────

        private void AddStudent()
        {
            ShowHeader("THÊM SINH VIÊN MỚI");

            string name    = ReadNonEmpty("Họ và tên: ");
            string email   = ReadNonEmpty("Email: ");
            string address = ReadNonEmpty("Địa chỉ: ");
            int age        = ReadInt("Tuổi: ", 1, 120);
            string grade   = ReadGrade();

            var student = _service.AddStudent(name, email, address, age, grade);
            PrintSuccess($"Đã thêm sinh viên: [{student.Id}] {student.Name}");
        }

        private void EditStudent()
        {
            ShowHeader("SỬA THÔNG TIN SINH VIÊN");

            int id = ReadInt("Nhập ID sinh viên cần sửa: ", 1, int.MaxValue);
            var existing = _service.GetStudentById(id);

            if (existing == null)
            {
                PrintError($"Không tìm thấy sinh viên với ID = {id}");
                return;
            }

            Console.WriteLine($"Đang sửa: {existing}");
            Console.WriteLine("(Nhấn Enter để giữ nguyên giá trị cũ)\n");

            string name    = ReadOrKeep("Họ và tên",  existing.Name);
            string email   = ReadOrKeep("Email",      existing.Email);
            string address = ReadOrKeep("Địa chỉ",    existing.Address);
            int age        = ReadIntOrKeep("Tuổi",    existing.Age);
            string grade   = ReadGradeOrKeep(existing.Grade);

            bool ok = _service.UpdateStudent(id, name, email, address, age, grade);
            if (ok) PrintSuccess("Cập nhật thành công!");
            else    PrintError("Cập nhật thất bại.");
        }

        private void DeleteStudent()
        {
            ShowHeader("XOÁ SINH VIÊN");

            int id = ReadInt("Nhập ID sinh viên cần xoá: ", 1, int.MaxValue);
            var existing = _service.GetStudentById(id);

            if (existing == null)
            {
                PrintError($"Không tìm thấy sinh viên với ID = {id}");
                return;
            }

            Console.WriteLine($"Sinh viên cần xoá: {existing}");
            Console.Write("Bạn có chắc chắn muốn xoá? (y/n): ");
            string confirm = Console.ReadLine() ?? "";

            if (confirm.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                bool ok = _service.DeleteStudent(id);
                if (ok) PrintSuccess("Đã xoá sinh viên.");
                else    PrintError("Xoá thất bại.");
            }
            else
            {
                Console.WriteLine("Đã huỷ thao tác xoá.");
            }
        }

        // ── Search ───────────────────────────────────────────────────────────────

        private void SearchMenu()
        {
            Console.Clear();
            ShowHeader("TÌM KIẾM SINH VIÊN");
            Console.WriteLine("1. Tìm theo ID");
            Console.WriteLine("2. Tìm theo Tên");
            Console.WriteLine("3. Tìm theo Địa chỉ");
            Console.WriteLine("4. Tìm theo Xếp loại (Grade)");
            Console.WriteLine("0. Quay lại");
            Console.Write("\nChọn: ");

            string choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1": SearchById(); break;
                case "2": SearchByName(); break;
                case "3": SearchByAddress(); break;
                case "4": SearchByGrade(); break;
                case "0": return;
                default: PrintError("Lựa chọn không hợp lệ!"); break;
            }
        }

        private void SearchById()
        {
            int id = ReadInt("Nhập ID cần tìm: ", 1, int.MaxValue);
            var student = _service.GetStudentById(id);

            if (student == null)
                PrintError($"Không tìm thấy sinh viên với ID = {id}");
            else
            {
                ShowHeader("KẾT QUẢ TÌM KIẾM");
                PrintTableHeader();
                PrintTableRow(student);
                PrintTableFooter();
            }
        }

        private void SearchByName()
        {
            Console.Write("Nhập tên cần tìm: ");
            string keyword = Console.ReadLine() ?? "";
            var result = _service.SearchByName(keyword);
            ShowStudentList(result, $"KẾT QUẢ TÌM THEO TÊN: \"{keyword}\"");
        }

        private void SearchByAddress()
        {
            Console.Write("Nhập địa chỉ cần tìm: ");
            string keyword = Console.ReadLine() ?? "";
            var result = _service.SearchByAddress(keyword);
            ShowStudentList(result, $"KẾT QUẢ TÌM THEO ĐỊA CHỈ: \"{keyword}\"");
        }

        private void SearchByGrade()
        {
            string grade = ReadGrade();
            var result = _service.SearchByGrade(grade);
            ShowStudentList(result, $"KẾT QUẢ TÌM THEO XẾP LOẠI: \"{grade}\"");
        }

        // ── Table Helpers ─────────────────────────────────────────────────────────

        private void PrintTableHeader()
        {
            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"{"ID",-5} {"Họ và tên",-20} {"Tuổi",-6} {"Email",-25} {"Địa chỉ",-15} {"Xếp loại",-8}");
            Console.WriteLine(new string('-', 85));
        }

        private void PrintTableRow(Student s)
        {
            Console.WriteLine($"{s.Id,-5} {s.Name,-20} {s.Age,-6} {s.Email,-25} {s.Address,-15} {s.Grade,-8}");
        }

        private void PrintTableFooter()
        {
            Console.WriteLine(new string('-', 85));
        }

        // ── Input Helpers ─────────────────────────────────────────────────────────

        private string ReadNonEmpty(string prompt)
        {
            string value;
            do
            {
                Console.Write(prompt);
                value = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(value))
                    PrintError("Không được để trống!");
            } while (string.IsNullOrWhiteSpace(value));
            return value.Trim();
        }

        private int ReadInt(string prompt, int min, int max)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out result) && result >= min && result <= max)
                    return result;
                PrintError($"Vui lòng nhập số nguyên hợp lệ ({min} - {max}).");
            }
        }

        private string ReadGrade()
        {
            string[] valid = { "A", "B", "C", "D", "F" };
            string grade;
            do
            {
                Console.Write("Xếp loại (A/B/C/D/F): ");
                grade = (Console.ReadLine() ?? "").Trim().ToUpper();
                if (Array.IndexOf(valid, grade) < 0)
                    PrintError("Xếp loại phải là A, B, C, D hoặc F!");
            } while (Array.IndexOf(valid, grade) < 0);
            return grade;
        }

        private string ReadOrKeep(string label, string current)
        {
            Console.Write($"{label} [{current}]: ");
            string input = Console.ReadLine() ?? "";
            return string.IsNullOrWhiteSpace(input) ? current : input.Trim();
        }

        private int ReadIntOrKeep(string label, int current)
        {
            while (true)
            {
                Console.Write($"{label} [{current}]: ");
                string input = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(input)) return current;
                if (int.TryParse(input, out int result) && result >= 1) return result;
                PrintError("Vui lòng nhập số nguyên hợp lệ.");
            }
        }

        private string ReadGradeOrKeep(string current)
        {
            string[] valid = { "A", "B", "C", "D", "F" };
            while (true)
            {
                Console.Write($"Xếp loại (A/B/C/D/F) [{current}]: ");
                string input = (Console.ReadLine() ?? "").Trim().ToUpper();
                if (string.IsNullOrWhiteSpace(input)) return current;
                if (Array.IndexOf(valid, input) >= 0) return input;
                PrintError("Xếp loại phải là A, B, C, D hoặc F!");
            }
        }

        // ── UI Helpers ────────────────────────────────────────────────────────────

        private void ShowHeader(string title)
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"  {title}");
            Console.WriteLine(new string('=', 50));
        }

        private void PrintSuccess(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✔ {msg}");
            Console.ResetColor();
        }

        private void PrintError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✘ {msg}");
            Console.ResetColor();
        }

        private void Pause()
        {
            Console.WriteLine("\nNhấn Enter để tiếp tục...");
            Console.ReadLine();
        }
    }
}
