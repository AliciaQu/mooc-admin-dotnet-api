# 课程章节模块集成说明

## 📋 已完成的工作

本次已成功将课程章节管理模块集成到您的项目中，严格按照现有项目的 DDD 分层架构 + CQRS 模式进行开发。

## 📁 创建的文件清单

### 1. Mooc.Shared 层（共享层）
- ✅ `Mooc.Shared/Enum/SwaggerGroup.cs` - **已更新**，添加了 `CourseChapterService = 4`
- ✅ `Mooc.Shared/Entity/CourseChapter/CourseChapterEntityConsts.cs` - 实体常量定义

### 2. Mooc.Model 层（数据模型层）
- ✅ `Mooc.Model/Entity/CourseChapter/CourseChapter.cs` - 课程章节实体类

### 3. Mooc.Application.Contracts 层（应用服务契约层）
#### DTOs
- ✅ `Mooc.Application.Contracts/CourseChapter/Dto/CourseChapterOutputDto.cs`
- ✅ `Mooc.Application.Contracts/CourseChapter/Dto/CreateOrUpdateCourseChapterBaseInputDto.cs`
- ✅ `Mooc.Application.Contracts/CourseChapter/Dto/CreateCourseChapterInputDto.cs`
- ✅ `Mooc.Application.Contracts/CourseChapter/Dto/UpdateCourseChapterInputDto.cs`

#### 服务接口
- ✅ `Mooc.Application.Contracts/CourseChapter/ICourseChapterReadService.cs`
- ✅ `Mooc.Application.Contracts/CourseChapter/ICourseChapterCreateService.cs`
- ✅ `Mooc.Application.Contracts/CourseChapter/ICourseChapterUpdateService.cs`
- ✅ `Mooc.Application.Contracts/CourseChapter/ICourseChapterDeleteService.cs`

### 4. Mooc.Application 层（应用服务实现层）
- ✅ `Mooc.Application/CourseChapter/CourseChapterProfile.cs` - AutoMapper 映射配置
- ✅ `Mooc.Application/CourseChapter/CourseChapterReadService.cs`
- ✅ `Mooc.Application/CourseChapter/CourseChapterCreateService.cs`
- ✅ `Mooc.Application/CourseChapter/CourseChapterUpdateService.cs`
- ✅ `Mooc.Application/CourseChapter/CourseChapterDeleteService.cs`

### 5. MoocWebApi 层（Web API 层）
- ✅ `MoocWebApi/Controllers/CourseChapter/CourseChapterController.cs` - 课程章节控制器

## 🎯 核心功能

### API 端点列表

所有 API 端点都在 `CourseChapterService` Swagger 分组下：

| 方法 | 路由 | 功能 | 参数 |
|------|------|------|------|
| GET | `/api/CourseChapter/GetAsync/{id}` | 获取章节详情 | id: 章节ID |
| GET | `/api/CourseChapter/GetPageAsync` | 获取章节列表（分页）| FilterPagedResultRequestDto |
| GET | `/api/CourseChapter/GetByCourseIdAsync/{courseId}` | 获取课程的所有章节 | courseId: 课程ID |
| POST | `/api/CourseChapter/CreateAsync` | 创建章节 | CreateCourseChapterInputDto |
| POST | `/api/CourseChapter/UpdateAsync` | 更新章节 | UpdateCourseChapterInputDto |
| DELETE | `/api/CourseChapter/DeleteAsync/{id}` | 删除章节 | id: 章节ID |
| POST | `/api/CourseChapter/UpdateOrderAsync` | 更新章节顺序 | id, newOrder |
| POST | `/api/CourseChapter/ToggleStatusAsync` | 切换章节状态 | id |

### 实体字段说明

CourseChapter 实体包含以下字段：

| 字段名 | 类型 | 说明 | 必填 |
|--------|------|------|------|
| Id | long | 章节ID（主键）| ✓ |
| CourseId | long | 课程ID（关联到 Terence 的课程模块）| ✓ |
| ChapterName | string(200) | 章节名称 | ✓ |
| Description | string(1000) | 章节描述 | - |
| OrderIndex | int | 章节顺序 | ✓ |
| Duration | int | 章节时长（分钟）| ✓ |
| IsActive | bool | 是否启用 | ✓ |
| IsFree | bool | 是否免费试看 | ✓ |
| VideoUrl | string(500) | 视频URL | - |
| MaterialUrl | string(500) | 资料URL | - |
| CreatedAt | DateTime | 创建时间 | ✓ |
| CreatedBy | long | 创建人ID | ✓ |
| UpdatedAt | DateTime? | 更新时间 | - |
| UpdatedBy | long? | 更新人ID | - |

## 🚀 下一步操作

### 1. 数据库迁移

在项目根目录执行以下命令创建数据库迁移：

```bash
# 如果使用 Package Manager Console（Visual Studio）
Add-Migration AddCourseChapterEntity
Update-Database

# 如果使用 dotnet CLI
dotnet ef migrations add AddCourseChapterEntity --project Mooc.Model
dotnet ef database update --project Mooc.Model
```

### 2. 构建项目

```bash
dotnet build
```

### 3. 运行项目

```bash
dotnet run --project MoocWebApi
```

### 4. 访问 Swagger 文档

运行项目后，访问 Swagger 文档（通常是 `https://localhost:5001/swagger` 或 `http://localhost:5000/swagger`），您会看到新增的 `CourseChapterService` 分组。

## 📝 代码说明

### 架构特点

1. **CQRS 模式**：将读取（Read）、创建（Create）、更新（Update）、删除（Delete）操作分离到不同的服务中
2. **DDD 分层**：严格遵循 Domain、Application、Presentation 分层
3. **依赖注入**：所有服务都实现了 `ITransientDependency` 接口，自动注册到 DI 容器
4. **AutoMapper**：使用 Profile 配置对象映射
5. **BaseEntity 继承**：实体继承 `BaseEntity` 获得 `Id` 字段

### 特殊功能

1. **按课程查询章节**
   ```csharp
   GET /api/CourseChapter/GetByCourseIdAsync/{courseId}
   ```
   返回指定课程的所有章节，按 `OrderIndex` 排序

2. **更新章节顺序**
   ```csharp
   POST /api/CourseChapter/UpdateOrderAsync?id=1
   Body: 5
   ```
   将章节 1 的顺序改为 5

3. **切换章节状态**
   ```csharp
   POST /api/CourseChapter/ToggleStatusAsync?id=1
   ```
   切换章节的启用/禁用状态

4. **支持搜索**
   在 `GetPageAsync` 中，可以通过 `Filter` 参数搜索章节名称或描述

## ⚠️ 注意事项

### 1. 用户ID 获取（TODO）

在以下文件中，有 TODO 注释需要实现当前用户ID的获取：

- `CourseChapterCreateService.cs` (第 19 行)
- `CourseChapterUpdateService.cs` (第 17、30、47 行)

目前临时使用 `entity.CreatedBy = 1;`，需要根据您的认证机制获取实际用户ID。

**建议实现方式：**

```csharp
// 可能需要注入 IHttpContextAccessor 或自定义的用户服务
private readonly ICurrentUserService _currentUserService;

// 然后在方法中使用
entity.CreatedBy = _currentUserService.GetCurrentUserId();
```

### 2. 与课程模块的关联

课程章节通过 `CourseId` 字段关联到 Terence 的课程模块。建议：

1. **添加外键约束**（在数据库迁移中）：
   ```csharp
   migrationBuilder.AddForeignKey(
       name: "FK_CourseChapter_Course",
       table: "CourseChapters",
       column: "CourseId",
       principalTable: "Courses",
       principalColumn: "Id",
       onDelete: ReferentialAction.Cascade);
   ```

2. **协调 API 接口**：
   - 与 Terence 确认课程ID的获取方式
   - 确保创建章节时课程ID是有效的

### 3. 异常处理

当前的异常处理比较简单，建议：

1. 添加自定义异常类
2. 使用全局异常过滤器
3. 返回统一的错误响应格式

### 4. 权限控制

当前 Controller 没有添加授权特性，建议根据需求添加：

```csharp
[Authorize] // 需要登录
[Authorize(Roles = "Admin")] // 需要管理员权限
```

## 🔗 与其他模块的协作

### 与课程模块（Terence）
- **依赖关系**：课程章节依赖课程模块
- **接口对接**：通过 `CourseId` 关联
- **建议**：在课程详情 API 中，可以调用 `GetByCourseIdAsync` 返回课程的所有章节

### 与开课内容（AIDEN）
- **可能的关联**：开课内容可能需要关联到具体的章节
- **建议**：AIDEN 可以通过 `CourseChapter.Id` 进行关联

## 📊 数据库表结构（预期）

迁移后会创建 `CourseChapters` 表：

```sql
CREATE TABLE CourseChapters (
    Id BIGINT PRIMARY KEY IDENTITY,
    CourseId BIGINT NOT NULL,
    ChapterName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000),
    OrderIndex INT NOT NULL,
    Duration INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    IsFree BIT NOT NULL DEFAULT 0,
    VideoUrl NVARCHAR(500),
    MaterialUrl NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL,
    CreatedBy BIGINT NOT NULL,
    UpdatedAt DATETIME2,
    UpdatedBy BIGINT,
    
    -- 建议添加索引
    INDEX IX_CourseChapters_CourseId (CourseId),
    INDEX IX_CourseChapters_OrderIndex (CourseId, OrderIndex)
);
```

## ✅ 测试检查清单

在完成数据库迁移后，建议进行以下测试：

- [ ] 创建章节
- [ ] 获取章节详情
- [ ] 获取章节列表（分页）
- [ ] 根据课程ID获取章节列表
- [ ] 更新章节
- [ ] 更新章节顺序
- [ ] 切换章节状态
- [ ] 删除章节
- [ ] 搜索章节（通过 Filter 参数）

## 🎉 总结

所有代码已按照您现有项目的架构风格完成集成，无需额外的结构调整。只需执行数据库迁移并编译运行即可使用。

如有任何问题或需要调整，请随时告诉我！

---
**模块负责人：Bob**  
**创建日期：2026-01-10**
