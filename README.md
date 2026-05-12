# KT_TKPM_CNTT 17-07

## Cấu trúc project

```
├── LAB1/
│   ├── TodoApp/        # Bài mẫu - One-Tier, lưu file .txt
│   └── StudentApp/     # Bài tập - One-Tier, lưu file .txt
└── LAB2/
    ├── TodoApp/        # Bài mẫu - Two-Tier, SQL Server + Dapper
    └── StudentApp/     # Bài tập - Two-Tier, MongoDB
```

---

## LAB 1 — One-Tier Architecture

### TodoApp (Bài mẫu)
- Không cần cài thêm gì
- Mở `LAB1/TodoApp/TodoApp.sln` bằng Visual Studio 2022
- Nhấn `Ctrl+F5` để chạy
- Dữ liệu lưu tại `bin/Debug/net8.0/todos.txt`

### StudentApp (Bài tập)
- Không cần cài thêm gì
- Mở `LAB1/StudentApp/StudentApp.sln` bằng Visual Studio 2022
- Nhấn `Ctrl+F5` để chạy
- Dữ liệu lưu tại `bin/Debug/net8.0/students.txt`

---

## LAB 2 — Two-Tier Architecture

### TodoApp (Bài mẫu) — SQL Server + Dapper

#### Yêu cầu
- SQL Server (bất kỳ instance nào)
- Visual Studio 2022

#### Setup Database
1. Mở **SSMS** → kết nối SQL Server
2. Mở file `LAB2/TodoApp/setup_db.sql` → Execute
3. Sẽ tạo database `TodoDB` và bảng `Todos`

#### Sửa Connection String
Mở `LAB2/TodoApp/Program.cs`, sửa dòng connection string cho đúng tên server:
```csharp
"Server=<TÊN_SERVER>;Database=TodoDB;Integrated Security=true;TrustServerCertificate=true"
```

#### Chạy
- Mở `LAB2/TodoApp/TodoApp.sln` → `Ctrl+F5`

---

### StudentApp (Bài tập) — MongoDB

#### Yêu cầu
- MongoDB Community 8.x (chạy ở `localhost:27017`)
- Visual Studio 2022

#### Cài MongoDB (nếu chưa có)
1. Tải tại https://www.mongodb.com/try/download/community
2. Cài với option **"Install MongoD as a Service"**
3. Kiểm tra: `Get-Service MongoDB` → Status = Running

#### Chạy
- Mở `LAB2/StudentApp/StudentApp.sln` → `Ctrl+F5`
- Database `StudentDB` tự tạo khi thêm sinh viên đầu tiên

---

## Chức năng

### LAB1 & LAB2 — StudentApp
| Chức năng | Mô tả |
|-----------|-------|
| 1. Hiển thị | Danh sách sinh viên dạng bảng |
| 2. Thêm | Nhập Name, Email, Address, Age, Grade |
| 3. Sửa | Sửa thông tin, Enter để giữ nguyên |
| 4. Xoá | Xoá có xác nhận y/n |
| 5. Tìm kiếm | Theo ID / Name / Address / Grade |
