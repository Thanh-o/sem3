1. Tạo project webapi:dotnet new webapi -n ten_project
2. Cài đăt thư viện cho Entity Framework( data model)
 dotnet add package Pomele.EntityFrameWorkCore.MySql (Pomelo MySQL Provider)
 dotnet add package Microsort.EntityFrameworkCore.Tools

 3. Đồng bộ hóa với Database (Tạo Migration)
 dotnet ef migrations add InitialCreate
 cập nhật vào database 
 dotnet ef database update