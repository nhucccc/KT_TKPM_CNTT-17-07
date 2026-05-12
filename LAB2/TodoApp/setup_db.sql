-- Chạy script này trên SQL Server trước khi chạy ứng dụng
-- Mở SQL Server Management Studio (SSMS) => New Query => Paste và Execute

CREATE DATABASE TodoDB;
GO

USE TodoDB;
GO

CREATE TABLE Todos (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Title       NVARCHAR(255)     NOT NULL,
    IsCompleted BIT               NOT NULL DEFAULT 0
);
GO

-- Dữ liệu mẫu (tuỳ chọn)
INSERT INTO Todos (Title, IsCompleted) VALUES
    (N'Learning software architecture', 1),
    (N'Làm bài tập Lab 2', 0);
GO
